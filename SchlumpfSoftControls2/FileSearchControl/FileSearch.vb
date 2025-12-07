' *************************************************************************************************
' FileSearch.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace FileSearchControl

    ''' <summary>
    ''' Führt eine optionale rekursive, asynchrone Dateisuche anhand eines Suchmusters
    ''' aus und meldet Ergebnisse sowie Fortschritt über Ereignisse (<see
    ''' cref="FileFound"/>, <see cref="ProgressChanged"/> und <see
    ''' cref="SearchCompleted"/>).
    ''' </summary>
    ''' <remarks>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Die Suche läuft in einem ThreadPool-Thread und nutzt <see
    ''' cref="SimpleProgress(Of T)"/> zum Marshallen der Ereignisse auf den
    ''' Synchronisierungskontext des Aufrufers (z. B. UI-Thread), sofern
    ''' vorhanden.</description>
    '''  </item>
    ''' </list>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Ein erneuter Aufruf von <see cref="StartSearchAsync"/> bricht
    ''' eine laufende Suche kooperativ ab und startet sofort neu.<br/>
    ''' Kurzzeitig können noch Ereignisse der vorherigen Suche eintreffen.</description>
    '''  </item>
    ''' </list>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Die Implementierung zählt zunächst alle passenden Dateien und
    ''' iteriert anschließend darüber (doppelte Enumeration), was bei sehr großen
    ''' Datenmengen die Laufzeit erhöhen kann.</description>
    '''  </item>
    ''' </list>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Fehler (z. B. fehlende Zugriffsrechte, ungültige Pfade) werden
    ''' über <see cref="ErrorOccurred"/> gemeldet und beenden die Suche.<br/>
    ''' Ein kooperativer Abbruch erfolgt über <see
    ''' cref="System.Threading.CancellationToken"/>.</description>
    '''  </item>
    ''' </list>
    ''' </remarks>
    ''' <example>
    ''' <para>Beispiel (Windows Forms):</para>
    ''' <code><![CDATA[' Erstellen und konfigurieren
    '''  Dim search As New FileSearchControl.FileSearch()
    '''  search.StartPath = "C:\Temp"
    '''  search.SearchPattern = "*.txt"
    '''  search.SearchInSubfolders = True
    '''  
    '''  ' Ereignisse abonnieren
    '''  AddHandler search.FileFound, Sub(sender, filePath)
    '''      ' Einzelnes Ergebnis verarbeiten (z. B. in eine ListBox einfügen)
    '''      ListBox1.Items.Add(filePath)
    '''  End Sub
    '''  
    '''  AddHandler search.ProgressChanged, Sub(sender, e)
    '''      ' Fortschritt anzeigen (Percent, Found, Total)
    '''      ProgressBar1.Value = Math.Max(0, Math.Min(100, e.Percent))
    '''      LabelStatus.Text = $"Gefunden: {e.Found}/{e.Total} ({e.Percent}%)"
    '''  End Sub
    '''  
    '''  AddHandler search.ErrorOccurred, Sub(sender, ex)
    '''      ' Fehler melden und ggf. abbrechen
    '''      MessageBox.Show($"Fehler: {ex.Message}", "Dateisuche", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '''  End Sub
    '''  
    '''  AddHandler search.SearchCompleted, Sub(sender, canceled)
    '''      If canceled Then
    '''          LabelStatus.Text = "Suche abgebrochen."
    '''      Else
    '''          LabelStatus.Text = "Suche abgeschlossen."
    '''      End If
    '''  End Sub
    '''  
    '''  ' Suche starten (asynchron)
    '''  Await search.StartSearchAsync()
    '''  
    '''  ' ... zu einem späteren Zeitpunkt: Suche kooperativ abbrechen
    '''  search.StopSearch()
    '''  ]]></code>
    ''' <para><br/>
    ''' </para>
    ''' </example>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Führt eine (optionale rekursive) asynchrone Dateisuche anhand eines Suchmusters aus und meldet Ergebnisse sowie Fortschritt über Ereignisse.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(FileSearchControl.FileSearch), "FileSearch.bmp")>
    Public Class FileSearch : Inherits System.ComponentModel.Component

#Region "Variablen"

        ' Komponenten-Container, der vom Designer verwendet wird, um untergeordnete Komponenten zu verwalten.
        ' Wird nur zur Laufzeit der Designumgebung benötigt und beim Dispose bereinigt.
        Private components As System.ComponentModel.IContainer

        ' Token-Quelle zur Signalisierung eines kooperativen Abbruchs an den laufenden Such-Task.
        ' Wird bei jedem Suchstart neu erzeugt und ersetzt eine vorherige Instanz.
        Friend _CancellationSource As System.Threading.CancellationTokenSource

        ' Startpfad für die aktuelle Suche (Standard: String.Empty).
        Friend _StartPath As String = String.Empty

        ' Das aktuell konfigurierte Suchmuster (Standard: *.*).
        Friend _SearchPattern As String = $"*.*"

        ' Gibt an, ob eine rekursive Suche über alle Unterordner erfolgt (Standard: False).
        Friend _SearchInSubfolders As Boolean = False

#End Region

#Region "Ereignisse"

        ''' <summary>
        ''' Wird für jede gefundene Datei ausgelöst.
        ''' </summary>
        ''' <remarks>
        ''' Dieses Ereignis wird unmittelbar gemeldet, sobald eine Datei gefunden
        ''' wurde.<br/>
        ''' Es wird vom internen <c>SimpleProgress(Of String)</c> auf den Synchronisierungskontext (z. B. UI) gemarshallt.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Pfad in eine ListBox einfügen
        ''' AddHandler search.FileFound, Sub(sender, filePath)
        '''     ListBox1.Items.Add(filePath)
        ''' End Sub]]></code>
        ''' </example>
        <System.ComponentModel.Description("")>
        Public Event FileFound(sender As Object, file As String)

        ''' <summary>
        ''' Wird nach Abschluss oder Abbruch der Suche ausgelöst.
        ''' </summary>
        ''' <remarks>
        ''' Das Ereignis signalisiert das Ende der laufenden Suche.<br/>
        ''' Bei kooperativem Abbruch ist <c>cancel=True</c>.<br/>
        ''' Kurzzeitig können noch Ereignisse der vorherigen Suche eintreffen, wenn
        ''' unmittelbar neu gestartet wurde.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Status aktualisieren
        ''' AddHandler search.SearchCompleted, Sub(sender, canceled)
        '''     LabelStatus.Text = If(canceled, "Suche abgebrochen.", "Suche abgeschlossen.")
        ''' End Sub]]></code>
        ''' </example>
        <System.ComponentModel.Description("Wird für jede gefundene Datei ausgelöst.")>
        Public Event SearchCompleted(sender As Object, cancel As Boolean)

        ''' <summary>
        ''' Meldet aufgetretene Fehler (z. B. fehlende Zugriffsrechte, ungültiger Pfad).
        ''' </summary>
        ''' <remarks>
        ''' Bei Fehlern während der Enumeration wird die Suche beendet und der Fehler
        ''' gemeldet.<br/>
        ''' Typische Ursachen: <see cref="System.UnauthorizedAccessException"/>, <see
        ''' cref="System.IO.DirectoryNotFoundException"/>.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Fehlerdialog anzeigen
        ''' AddHandler search.ErrorOccurred, Sub(sender, ex)
        '''     MessageBox.Show($"Fehler: {ex.Message}", "Dateisuche", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''' End Sub]]></code>
        ''' </example>
        <System.ComponentModel.Description("Meldet aufgetretene Fehler (z. B. fehlende Zugriffsrechte, ungültiger Pfad).")>
        Public Event ErrorOccurred(sender As Object, [error] As System.Exception)

        ''' <summary>
        ''' Meldet aggregierte Fortschrittsinformationen (Anzahl gefunden, Gesamtzahl,
        ''' Prozent).
        ''' </summary>
        ''' <remarks>
        ''' <see cref="FileSearchEventArgs"/> enthält <c>Percent</c>, <c>Found</c> und <c>Total</c>.<br/>
        ''' Die Werte werden nach jeder gefundenen Datei aktualisiert und auf den
        ''' Synchronisierungskontext gemarshallt.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: ProgressBar und Label aktualisieren
        ''' AddHandler search.ProgressChanged, Sub(sender, info)
        '''     ProgressBar1.Value = Math.Max(0, Math.Min(100, info.Percent))
        '''     LabelStatus.Text = $"Gefunden: {info.Found}/{info.Total} ({info.Percent}%)"
        ''' End Sub]]></code>
        ''' </example>
        <System.ComponentModel.Description("Meldet aggregierte Fortschrittsinformationen (Anzahl gefunden, Gesamtzahl, Prozent).")>
        Public Event ProgressChanged(sender As Object, e As FileSearchEventArgs)

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Startverzeichnis der Suche (Initialwert ist <c>String.Empty</c>).
        ''' </summary>
        ''' <remarks>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Änderungen wirken sich auf nachfolgende Aufrufe von <see
        ''' cref="StartSearchAsync"/> aus.</description>
        '''  </item>
        '''  <item>
        '''   <description>Ungültige oder nicht erreichbare Pfade führen zu <see
        ''' cref="ErrorOccurred"/> mit einer entsprechenden Ausnahme.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        ''' <value>
        ''' Der absolute oder relative Pfad, ab dem die Dateisuche beginnt.
        ''' </value>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Suche im Benutzer-Dokumente-Ordner starten
        ''' Dim search As New FileSearchControl.FileSearch()
        ''' search.StartPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        ''' search.SearchPattern = "*.pdf"
        ''' search.SearchInSubfolders = False
        ''' Await search.StartSearchAsync()]]></code>
        ''' </example>
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
        ''' Suchmuster (z. B. <c>*.txt</c>). Standardwert ist <c>*.*</c>.
        ''' </summary>
        ''' <remarks>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Mehrere Muster werden nicht unterstützt; für komplexere Filterung
        ''' eigenes Matching nachladen.</description>
        '''  </item>
        '''  <item>
        '''   <description>Einige Dateisysteme können die Groß-/Kleinschreibung ignorieren;
        ''' das Verhalten ist abhängig vom System.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        ''' <value>
        ''' Ein Muster mit Platzhaltern gemäß <see
        ''' cref="System.IO.Directory.EnumerateFiles(String, String,
        ''' System.IO.SearchOption)"/>.
        ''' </value>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Nur Logdateien eines Ordners sammeln
        ''' Dim search As New FileSearchControl.FileSearch()
        ''' search.StartPath = "C:\Logs"
        ''' search.SearchPattern = "app-*.log"
        ''' search.SearchInSubfolders = True
        ''' AddHandler search.FileFound, Sub(_, p) Debug.WriteLine(p)
        ''' Await search.StartSearchAsync()]]></code>
        ''' </example>
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
        ''' Gibt an, ob die Suche rekursiv in Unterordnern erfolgen soll. Standardwert ist <c>False</c>.
        ''' </summary>
        ''' <remarks>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Rekursion kann die Laufzeit bei tiefen Strukturen deutlich
        ''' erhöhen.</description>
        '''  </item>
        '''  <item>
        '''   <description>Zugriffsprobleme in Unterordnern werden über <see
        ''' cref="ErrorOccurred"/> gemeldet und beenden die Suche.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        ''' <value>
        ''' <c>True</c>, wenn auch Unterordner durchsucht werden, sonst <c>False</c>.
        ''' </value>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Rekursive Suche nach Bilddateien
        ''' Dim search As New FileSearchControl.FileSearch()
        ''' search.StartPath = "D:\Fotos"
        ''' search.SearchPattern = "*.jpg"
        ''' search.SearchInSubfolders = True
        ''' Dim count As Integer = 0
        ''' AddHandler search.FileFound, Sub(_, p) count += 1
        ''' AddHandler search.SearchCompleted, Sub(_, canceled)
        '''     Debug.WriteLine($"Fertig. Gefunden: {count}. Abgebrochen: {canceled}")
        ''' End Sub
        ''' Await search.StartSearchAsync()]]></code>
        ''' </example>
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
        ''' <remarks>
        ''' Initialisiert interne Ressourcen über <see cref="InitializeComponent"/> und setzt Standardwerte (z. B. <c>StartPath=String.Empty</c>, <c>SearchPattern=*.*</c>, <c>SearchInSubfolders=False</c>).
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Komponente zur Laufzeit erzeugen und verwenden
        ''' Dim search As New FileSearchControl.FileSearch()
        ''' search.StartPath = "C:\Temp"
        ''' search.SearchPattern = "*.txt"
        ''' AddHandler search.FileFound, Sub(_, p) Debug.WriteLine(p)
        ''' Await search.StartSearchAsync()]]></code>
        ''' </example>
        Public Sub New()
            MyBase.New()
            Me.InitializeComponent() ' Dieser Aufruf ist für den Komponenten-Designer erforderlich.
        End Sub

        ''' <summary>
        ''' Erstellt eine neue Instanz und fügt sie optional einem übergebenen
        ''' Komponenten-Container hinzu.
        ''' </summary>
        ''' <remarks>
        ''' Ermöglicht Designtime-Komposition in Windows Forms; bei Übergabe eines
        ''' Containers wird die Instanz diesem hinzugefügt und dort verwaltet.
        ''' </remarks>
        ''' <param name="container">Ein vorhandener <see cref="System.ComponentModel.IContainer"/>, dem die Instanz hinzugefügt wird, oder <c>Nothing</c>.</param>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: In einen bestehenden Container einfügen (z. B. bei DI)
        ''' Dim container As New System.ComponentModel.Container()
        ''' Dim search As New FileSearchControl.FileSearch(container)
        ''' search.StartPath = "D:\Data"
        ''' search.SearchPattern = "*.csv"
        ''' Await search.StartSearchAsync()]]></code>
        ''' </example>
        <System.Diagnostics.DebuggerNonUserCode()>
        Public Sub New(ByVal container As System.ComponentModel.IContainer)
            MyClass.New()
            container?.Add(Me) ' Erforderlich für die Unterstützung des Windows.Forms-Klassenkompositions-Designers
        End Sub

        ''' <summary>
        ''' Startet eine neue asynchrone Dateisuche gemäß den aktuell gesetzten Parametern.
        ''' </summary>
        ''' <remarks>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Bei erneutem Aufruf während eine Suche läuft, wird die vorherige
        ''' kooperativ abgebrochen.</description>
        '''  </item>
        ''' </list>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Ergebnisse und Fortschritt werden über <see cref="FileFound"/>,
        ''' <see cref="ProgressChanged"/> und <see cref="SearchCompleted"/>
        ''' gemeldet.</description>
        '''  </item>
        ''' </list>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Fehler während der Enumeration lösen <see cref="ErrorOccurred"/>
        ''' aus und beenden die Suche.</description>
        '''  </item>
        ''' </list>
        ''' <para><b>Hinweis:</b><br/>
        ''' Die Implementierung enumeriert die Ergebnisse zweimal (für <c>Total</c> und Verarbeitung).</para>
        ''' </remarks>
        ''' <returns>
        ''' Ein <see cref="System.Threading.Tasks.Task"/>, das den Abschluss der
        ''' Suchoperation repräsentiert.
        ''' </returns>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Suche mit Fortschritt in WinForms
        ''' Dim search As New FileSearchControl.FileSearch()
        ''' search.StartPath = "C:\Logs"
        ''' search.SearchPattern = "app-*.log"
        ''' search.SearchInSubfolders = True
        ''' AddHandler search.ProgressChanged, Sub(_, info)
        '''     ProgressBar1.Value = Math.Max(0, Math.Min(100, info.Percent))
        '''     LabelStatus.Text = $"Gefunden: {info.Found}/{info.Total} ({info.Percent}%)"
        ''' End Sub
        ''' AddHandler search.ErrorOccurred, Sub(_, ex)
        '''     MessageBox.Show($"Fehler: {ex.Message}", "Suche", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''' End Sub
        ''' Await search.StartSearchAsync()]]></code>
        ''' </example>
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
        ''' <remarks>
        ''' Der Abbruch ist kooperativ; bereits ausgelöste Events (<see
        ''' cref="FileFound"/>/<see cref="ProgressChanged"/>) können noch kurzfristig
        ''' eintreffen.<br/>
        ''' Ein Abschluss wird über <see cref="SearchCompleted"/> mit <c>cancel=True</c> signalisiert.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Suche via Button abbrechen
        ''' Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        '''     search.StopSearch()
        ''' End Sub]]></code>
        ''' </example>
        Public Sub StopSearch()
            Me._CancellationSource?.Cancel()
        End Sub

        ''' <summary>
        ''' Gibt die von der Komponente verwendeten Ressourcen frei.
        ''' </summary>
        ''' <remarks>
        ''' Ruft die Basisklassenimplementierung auf und bereinigt den
        ''' Komponenten-Container.<br/>
        ''' Bei <c>disposing=True</c> werden verwaltete Ressourcen (z. B. <see cref="components"/>) freigegeben.
        ''' </remarks>
        ''' <param name="disposing"><c>True</c>, wenn verwaltete Ressourcen freigegeben werden sollen; andernfalls <c>False</c>.</param>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Ordnungsgemäße Entsorgung in einem Form
        ''' Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        '''     MyBase.OnFormClosed(e)
        '''     search?.Dispose()
        ''' End Sub]]></code>
        ''' </example>
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

        ' Initialisiert die Komponente für den Designer und legt interne Container-Strukturen an.
        ' Wird automatisch von den Konstruktoren aufgerufen.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
        End Sub

#End Region

    End Class

End Namespace