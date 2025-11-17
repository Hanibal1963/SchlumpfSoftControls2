' *************************************************************************************************
' FileSearch.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace FileSearchControl

    ''' <summary>
    ''' Führt eine (optionale rekursive) asynchrone Dateisuche anhand eines Suchmusters aus und meldet Ergebnisse sowie Fortschritt über Ereignisse.
    ''' </summary>
    ''' <remarks>
    ''' <para>Die Suche läuft in einem ThreadPool-Thread (via <see cref="Task.Run(System.Action)"/>). </para>
    ''' <para>Events werden (über <see cref="SimpleProgress(Of T)"/>) typischerweise auf
    ''' dem Ersteller- Synchronisierungskontext (z. B. UI-Thread) aufgerufen, sofern beim Start vorhanden. </para>
    ''' <para>Ein erneuter Aufruf von <see cref="StartSearchAsync"/> beendet eine laufende Suche vor Start der neuen. </para>
    ''' <para>Es erfolgt eine doppelte Enumeration (Zählen + Iterieren), was bei sehr vielen Dateien Performance kosten kann.</para>
    ''' </remarks>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Führt eine (optionale rekursive) asynchrone Dateisuche anhand eines Suchmusters aus und meldet Ergebnisse sowie Fortschritt über Ereignisse.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(FileSearchControl.FileSearch), "FileSearch.bmp")>
    Public Class FileSearch : Inherits System.ComponentModel.Component

#Region "Variablendefinition"

        ''' <summary>
        ''' Komponenten-Container, der vom Designer verwendet wird, um untergeordnete Komponenten zu verwalten.
        ''' </summary>
        ''' <remarks>Wird nur zur Laufzeit der Designumgebung benötigt und beim Dispose bereinigt.</remarks>
        Private components As System.ComponentModel.IContainer

        ''' <summary>
        ''' Token-Quelle zur Signalisierung eines kooperativen Abbruchs an den laufenden Such-Task.
        ''' </summary>
        ''' <remarks>Wird bei jedem Suchstart neu erzeugt und ersetzt eine vorherige Instanz.</remarks>
        Friend _CancellationSource As System.Threading.CancellationTokenSource

        ''' <summary>
        ''' Startpfad für die aktuelle Suche (Standard: <c>String.Empty</c>).
        ''' </summary>
        Friend _StartPath As String = String.Empty

        ''' <summary>
        ''' Das aktuell konfigurierte Suchmuster (Standard: <c>*.*</c>).
        ''' </summary>
        Friend _SearchPattern As String = $"*.*"

        ''' <summary>
        ''' Gibt an, ob eine rekursive Suche über alle Unterordner erfolgt (Standard: <c>False</c>).
        ''' </summary>
        Friend _SearchInSubfolders As Boolean = False

#End Region

#Region "Öffentliche Ereignisse"

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

#End Region

#Region "neue Eigenschaften"

        ''' <summary>
        ''' Startverzeichnis der Suche (Initialwert ist String.Empty).
        ''' </summary>
        ''' <value>Der absolute oder relative Pfad, ab dem die Dateisuche beginnt.</value>
        ''' <remarks>Änderungen wirken sich auf nachfolgende Aufrufe von <see cref="StartSearchAsync"/> aus.</remarks>
        <System.ComponentModel.Description("Startverzeichnis der Suche (Initialwert ist String.Empty).")>
        <System.ComponentModel.Category("Behavior")>
        Public Property StartPath As String
            Get
                Return Me._StartPath
            End Get
            Set(value As String)
                Me._StartPath = value
            End Set
        End Property

        ''' <summary>
        ''' Suchmuster (z. B. *.txt). (Standardwert ist "*.*").
        ''' </summary>
        ''' <value>Ein Muster mit Platzhaltern gemäß <see cref="System.IO.Directory.EnumerateFiles(String, String, System.IO.SearchOption)"/>.</value>
        ''' <remarks>Mehrere Muster werden nicht unterstützt; für komplexere Filterung eigenes Matching vornehmen.</remarks>
        <System.ComponentModel.Description("Suchmuster (z. B. *.txt). Standardwert: *.*")>
        <System.ComponentModel.Category("Behavior")>
        Public Property SearchPattern As String
            Get
                Return Me._SearchPattern
            End Get
            Set(value As String)
                Me._SearchPattern = value
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob die Suche rekursiv in Unterordnern erfolgen soll. (Standardwert ist False).
        ''' </summary>
        ''' <value><c>True</c>, wenn auch Unterordner durchsucht werden, sonst <c>False</c>.</value>
        ''' <remarks>Kann Performance und Dauer der Suche stark verlängern bei tiefen Verzeichnisstrukturen.</remarks>
        <System.ComponentModel.Description("Gibt an, ob die Suche rekursiv in Unterordnern erfolgen soll. (Standardwert ist False).")>
        <System.ComponentModel.Category("Behavior")>
        Public Property SearchInSubfolders As Boolean
            Get
                Return Me._SearchInSubfolders
            End Get
            Set(value As Boolean)
                Me._SearchInSubfolders = value
            End Set
        End Property

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Erstellt eine neue Instanz der <see cref="FileSearch"/> Komponente.
        ''' </summary>
        ''' <remarks>Verwendet <see cref="InitializeComponent"/> zur Designer-Initialisierung.</remarks>
        Public Sub New()
            MyBase.New()
            Me.InitializeComponent() ' Dieser Aufruf ist für den Komponenten-Designer erforderlich.
        End Sub

        ''' <summary>
        ''' Erstellt eine neue Instanz und fügt sie optional einem übergebenen Komponenten-Container hinzu.
        ''' </summary>
        ''' <param name="container">Ein vorhandener <see cref="System.ComponentModel.IContainer"/>, dem die Instanz hinzugefügt wird, oder <c>Nothing</c>.</param>
        ''' <remarks>Ermöglicht Designtime-Komposition in Windows Forms.</remarks>
        <System.Diagnostics.DebuggerNonUserCode()>
        Public Sub New(ByVal container As System.ComponentModel.IContainer)
            MyClass.New()
            container?.Add(Me) ' Erforderlich für die Unterstützung des Windows.Forms-Klassenkompositions-Designers
        End Sub

        ''' <summary>
        ''' Startet eine neue asynchrone Dateisuche gemäß den aktuell gesetzten Parametern.
        ''' </summary>
        ''' <returns>Ein <see cref="System.Threading.Tasks.Task"/>, das den Abschluss der Suchoperation repräsentiert.</returns>
        ''' <remarks>
        ''' <para>Bei erneutem Aufruf während eine Suche läuft, wird die vorherige zuerst kooperativ abgebrochen.</para>
        ''' <para>Ergebnisse und Fortschritt werden über die Ereignisse <see cref="FileFound"/>, <see cref="ProgressChanged"/> und <see cref="SearchCompleted"/> gemeldet.</para>
        ''' <para>Fehler während der Enumeration lösen <see cref="ErrorOccurred"/> aus und beenden die Suche.</para>
        ''' </remarks>
        Public Async Function StartSearchAsync() As System.Threading.Tasks.Task

            ' Evtl. laufende Suche abbrechen (kooperativ).
            '  Es wird NICHT auf deren Abschluss gewartet. Dadurch ist ein schneller Neustart möglich,
            '  birgt aber das Risiko, dass kurze Zeit noch Events der alten Suche eintreffen.
            Me._CancellationSource?.Cancel()

            ' Neue CancellationTokenSource erzeugen.
            Me._CancellationSource = New System.Threading.CancellationTokenSource()
            Dim token = Me._CancellationSource.Token

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
                                                          Dim optionen As System.IO.SearchOption = If(Me.SearchInSubfolders, System.IO.SearchOption.AllDirectories, System.IO.SearchOption.TopDirectoryOnly)

                                                          ' Lazy-Enumeration aller passenden Dateien. Achtung:
                                                          '  Directory.EnumerateFiles löst erst beim Durchlaufen der Aufzählung IO-Zugriffe aus.
                                                          Dim allfiles As System.Collections.Generic.IEnumerable(Of String) = System.IO.Directory.EnumerateFiles(Me.StartPath, Me.SearchPattern, optionen)

                                                          ' Performance-Hinweis: Count() zwingt vollständige Aufzählung VOR der eigentlichen Verarbeitung.
                                                          ' Dadurch wird das gesamte Dateiset zweimal traversiert (hier: für Count und unten für die Schleife).
                                                          ' Alternative Strategien:
                                                          ' - Dateien einmal in eine Liste materialisieren (ToList) und dann Count + Schleife durchführen.
                                                          ' - Fortschritt ohne Total (z. B. with -1) oder mit dynamischer Schätzung ausgeben.
                                                          Dim total As Integer = System.Linq.Enumerable.Count(allfiles)

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
                RaiseEvent SearchCompleted(Me, True) ' Falls innerhalb der Task-Pipeline eine echte CancellationException geworfen wurde.

            Catch ex As System.UnauthorizedAccessException
                RaiseEvent ErrorOccurred(Me, ex) ' Zugriffsrechte fehlten (z. B. Systemordner). Suche wird komplett abgebrochen.

            Catch ex As System.IO.DirectoryNotFoundException
                RaiseEvent ErrorOccurred(Me, ex) ' Startpfad oder Teilpfade existieren nicht.

            Catch ex As System.Exception
                RaiseEvent ErrorOccurred(Me, ex) ' Generischer Fehlerfall (IO, Pfadlänge, etc.).

            End Try

        End Function

        ''' <summary>
        ''' Bricht eine eventuell laufende Suche ab.
        ''' </summary>
        ''' <remarks>Der Abbruch ist kooperativ; bereits ausgelöste Events (FileFound/Progress) können noch kurzfristig eintreffen.</remarks>
        Public Sub StopSearch()
            Me._CancellationSource?.Cancel()
        End Sub

#End Region

#Region "überschriebene Methoden"

        ''' <summary>
        ''' Gibt die von der Komponente verwendeten Ressourcen frei.
        ''' </summary>
        ''' <param name="disposing"><c>True</c>, wenn verwaltete Ressourcen freigegeben werden sollen; andernfalls <c>False</c>.</param>
        ''' <remarks>Ruft die Basisklassenimplementierung auf und bereinigt den Komponenten-Container.</remarks>
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso Me.components IsNot Nothing Then
                    Me.components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

#End Region

#Region "interne Methoden"

        ''' <summary>
        ''' Initialisiert die Komponente für den Designer und legt interne Container-Strukturen an.
        ''' </summary>
        ''' <remarks>Wird automatisch von den Konstruktoren aufgerufen.</remarks>
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
        End Sub

#End Region

    End Class

End Namespace