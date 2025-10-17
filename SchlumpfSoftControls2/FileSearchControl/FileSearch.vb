' *************************************************************************************************
' FileSearch.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System.Linq

Namespace FileSearchControl

    ''' <summary>
    ''' Führt eine (optionale rekursive) asynchrone Dateisuche anhand eines Suchmusters aus und meldet Ergebnisse sowie Fortschritt über Ereignisse.
    ''' </summary>
    ''' <remarks>
    ''' <para>Die Suche läuft in einem ThreadPool-Thread (via <see cref="Task.Run(System.Action)"/>). </para>
    ''' <para>Events werden (über <see cref="SimpleProgress(Of T)"/>) typischerweise auf
    ''' dem Ersteller- Synchronisierungskontext (z. B. UI-Thread) aufgerufen, sofern beim Start vorhanden. </para>
    ''' <para>Ein erneuter Aufruf von <see cref="StartSearch"/> beendet eine laufende Suche vor Start der neuen. </para>
    ''' <para>Es erfolgt eine doppelte Enumeration (Zählen + Iterieren), was bei sehr vielen Dateien Performance kosten kann.</para>
    ''' </remarks>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Führt eine (optionale rekursive) asynchrone Dateisuche anhand eines Suchmusters aus und meldet Ergebnisse sowie Fortschritt über Ereignisse.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(FileSearchControl.FileSearch), "FileSearch.bmp")>
    Public Class FileSearch

        ''' <summary>
        ''' Wird für jede gefundene Datei ausgelöst.
        ''' </summary>
        ''' <param name="sender">Die auslösende Instanz (immer diese Klasse).</param>
        ''' <param name="file">Vollständiger Dateipfad.</param>
        <System.ComponentModel.Description("")>
        Public Event FileFound(sender As Object, file As String)

        ''' <summary>
        ''' Wird nach Abschluss oder Abbruch der Suche ausgelöst.
        ''' </summary>
        ''' <param name="sender">Die auslösende Instanz.</param>
        ''' <param name="cancel"><c>True</c>, wenn die Suche vorzeitig abgebrochen wurde.</param>
        <System.ComponentModel.Description("Wird für jede gefundene Datei ausgelöst.")>
        Public Event SearchCompleted(sender As Object, cancel As Boolean)

        ''' <summary>
        ''' Meldet aufgetretene Fehler (z. B. fehlende Zugriffsrechte, ungültiger Pfad).
        ''' </summary>
        ''' <param name="sender">Die auslösende Instanz.</param>
        ''' <param name="error">Ausgelöste Ausnahme.</param>
        <System.ComponentModel.Description("Meldet aufgetretene Fehler (z. B. fehlende Zugriffsrechte, ungültiger Pfad).")>
        Public Event ErrorOccurred(sender As Object, [error] As System.Exception)

        ''' <summary>
        ''' Meldet aggregierte Fortschrittsinformationen (Anzahl gefunden, Gesamtzahl, Prozent).
        ''' </summary>
        ''' <param name="sender">Die auslösende Instanz.</param>
        ''' <param name="e">Fortschrittsdaten der aktuellen Suche.</param>
        <System.ComponentModel.Description("Meldet aggregierte Fortschrittsinformationen (Anzahl gefunden, Gesamtzahl, Prozent).")>
        Public Event ProgressChanged(sender As Object, e As FileSearchEventArgs)

        ''' <summary>
        ''' Startverzeichnis der Suche (Initialwert ist String.Empty).
        ''' </summary>
        ''' <returns></returns>
        <System.ComponentModel.Description("Startverzeichnis der Suche (Initialwert ist String.Empty).")>
        <System.ComponentModel.Category("Behavior")>
        Public Property StartPath As String
            Get
                Return _StartPath
            End Get
            Set(value As String)
                _StartPath = value
            End Set
        End Property

        ''' <summary>
        ''' Suchmuster (z. B. *.txt). (Standardwert ist "*.*").
        ''' </summary>
        ''' <returns></returns>
        <System.ComponentModel.Description("Suchmuster (z. B. *.txt). (Standardwert ist "" * .* "")")>
        <System.ComponentModel.Category("Behavior")>
        Public Property SearchPattern As String
            Get
                Return _SearchPattern
            End Get
            Set(value As String)
                _SearchPattern = value
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob die Suche rekursiv in Unterordnern erfolgen soll. (Standardwert ist False).
        ''' </summary>
        ''' <returns></returns>
        <System.ComponentModel.Description("Gibt an, ob die Suche rekursiv in Unterordnern erfolgen soll. (Standardwert ist False).")>
        <System.ComponentModel.Category("Behavior")>
        Public Property SearchInSubfolders As Boolean
            Get
                Return _SearchInSubfolders
            End Get
            Set(value As Boolean)
                _SearchInSubfolders = value
            End Set
        End Property

        Public Sub New()
            MyBase.New()
            'Dieser Aufruf ist für den Komponenten-Designer erforderlich.
            Me.InitializeComponent()
        End Sub

        ''' <summary>
        ''' Initialisiert eine neue Instanz der <see cref="FileSearch"/> Klasse.
        ''' </summary>
        ''' <remarks>
        ''' <para>Bei erneutem Aufruf während eine Suche läuft, wird die vorherige zuerst
        ''' abgebrochen. </para>
        ''' </remarks>
        Public Async Function StartSearchAsync() As System.Threading.Tasks.Task

            ' Evtl. laufende Suche abbrechen (kooperativ).
            '  Es wird NICHT auf deren Abschluss gewartet. Dadurch ist ein schneller Neustart möglich,
            '  birgt aber das Risiko, dass kurze Zeit noch Events der alten Suche eintreffen.
            _CancellationSource?.Cancel()

            ' Neue CancellationTokenSource erzeugen.
            _CancellationSource = New System.Threading.CancellationTokenSource()
            Dim token = _CancellationSource.Token

            ' Progress-Objekt für EINZELNE Dateien.
            ' Jede gefundene Datei führt zu einem sofortigen Event (FileFound).
            ' SimpleProgress sorgt für Marshallen auf den ursprünglichen Synchronisierungskontext (UI).
            Dim progressFile = New SimpleProgress(Of String)(
                Sub(datei)
                    RaiseEvent FileFound(Me, datei)
                End Sub)

            ' Progress-Objekt für aggregierte Statuswerte (Prozent / Anzahl).
            ' Dieses Event wird nach jeder gefundenen Datei aktualisiert.
            Dim progressStatus = New SimpleProgress(Of FileSearchEventArgs)(
                Sub(info)
                    RaiseEvent ProgressChanged(Me, info)
                End Sub)

            Try
                ' Auslagerung der potentiell langen, blockierenden Dateisystem-Enumeration
                ' auf einen ThreadPool-Thread. Das CancellationToken wird übergeben, um
                ' Abbruch auf Task-Ebene (z. B. bei ThrowIfCancellationRequested) zu ermöglichen.
                Await System.Threading.Tasks.Task.Run(Sub()

                                                          ' Rekursionsmodus bestimmen.
                                                          Dim optionen As System.IO.SearchOption = If(SearchInSubfolders, System.IO.SearchOption.AllDirectories, System.IO.SearchOption.TopDirectoryOnly)

                                                          ' Lazy-Enumeration aller passenden Dateien. Achtung:
                                                          '  Directory.EnumerateFiles löst erst beim Durchlaufen der Aufzählung IO-Zugriffe aus.
                                                          Dim allfiles As System.Collections.Generic.IEnumerable(Of String) = System.IO.Directory.EnumerateFiles(StartPath, SearchPattern, optionen)

                                                          ' Performance-Hinweis: Count() zwingt vollständige Aufzählung VOR der eigentlichen Verarbeitung.
                                                          ' Dadurch wird das gesamte Dateiset zweimal traversiert (hier: für Count und unten für die Schleife).
                                                          ' Alternative Strategien:
                                                          ' - Dateien einmal in eine Liste materialisieren (ToList) und dann Count + Schleife durchführen.
                                                          ' - Fortschritt ohne Total (z. B. with -1) oder mit dynamischer Schätzung ausgeben.
                                                          Dim total As Integer = allfiles.Count()

                                                          Dim found As Integer = 0

                                                          ' Zweite Enumeration: tatsächliche Verarbeitung jeder Datei.
                                                          For Each datei In allfiles

                                                              ' Kooperativer Abbruch: Schleife wird verlassen,
                                                              ' Events bis hierher bleiben gültig.
                                                              If token.IsCancellationRequested Then Exit For

                                                              found += 1

                                                              ' Sofortiges Melden der einzelnen Datei.
                                                              progressFile.Report(datei)

                                                              ' Prozentberechnung. Schutz gegen Division durch 0 falls total=0.
                                                              Dim percent As Integer = 0
                                                              If total > 0 Then
                                                                  percent = CInt(found / total * 100)
                                                              End If

                                                              ' Aggregierten Status melden.
                                                              progressStatus.Report(New FileSearchEventArgs With {.Percent = percent, .Found = found, .Total = total})

                                                              ' Hinweis: Kein try/catch innerhalb der Schleife.
                                                              ' Tritt ein IO-Fehler (z. B. während Enumeration) auf, wird er
                                                              ' vom äußeren Catch abgefangen und beendet die gesamte Suche.
                                                              ' Für "best effort" Verhalten pro Datei könnte man hier
                                                              ' differenzierter behandeln.
                                                          Next

                                                      End Sub, token)

                ' Abschluss signalisieren. Wenn IsCancellationRequested True ist,
                ' bedeutet dies: Schleife wurde vorzeitig verlassen.
                RaiseEvent SearchCompleted(Me, token.IsCancellationRequested)

            Catch ex As System.OperationCanceledException
                ' Falls innerhalb der Task-Pipeline eine echte CancellationException geworfen wurde.
                RaiseEvent SearchCompleted(Me, True)

            Catch ex As System.UnauthorizedAccessException
                ' Zugriffsrechte fehlten (z. B. Systemordner). Suche wird komplett abgebrochen.
                RaiseEvent ErrorOccurred(Me, ex)

            Catch ex As System.IO.DirectoryNotFoundException
                ' Startpfad oder Teilpfade existieren nicht.
                RaiseEvent ErrorOccurred(Me, ex)

            Catch ex As System.Exception
                ' Generischer Fehlerfall (IO, Pfadlänge, etc.).
                RaiseEvent ErrorOccurred(Me, ex)
            End Try

        End Function

        ''' <summary>
        ''' Bricht eine eventuell laufende Suche ab.
        ''' </summary>
        ''' <remarks>
        ''' Der Abbruch ist kooperativ; bereits ausgelöste Events (FileFound/Progress) können noch kurzfristig eintreffen.
        ''' </remarks>
        Public Sub StopSearch()
            _CancellationSource?.Cancel()
        End Sub

    End Class

End Namespace