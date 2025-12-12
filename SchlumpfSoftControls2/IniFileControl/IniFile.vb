' *************************************************************************************************
' IniFile.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace IniFileControl

    ''' <summary>
    ''' Steuerelement zum Verwalten von INI - Dateien
    ''' </summary>
    ''' <example>
    ''' <code><![CDATA[Dim ini As New IniFileControl.IniFile()
    ''' ini.FilePath = "C:\\Temp"
    ''' ini.FileName = "config.ini"
    ''' ini.CreateNewFile()
    ''' ini.SaveFile(System.IO.Path.Combine(ini.FilePath, ini.FileName))]]></code>
    ''' </example>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Steuerelement zum Verwalten von INI - Dateien")>
    <System.Drawing.ToolboxBitmap(GetType(IniFileControl.IniFile), "IniFile.bmp")>
    <System.ComponentModel.ToolboxItem(True)>
    Public Class IniFile : Inherits System.ComponentModel.Component

#Region "Variablen"

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0032:Automatisch generierte Eigenschaft verwenden", Justification:="<Ausstehend>")>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Unnötige Unterdrückung entfernen", Justification:="<Ausstehend>")>
        Private _FileName As String = $"neue Datei.ini" ' Name der Datei (nur der Dateiname, ohne Pfad)

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0032:Automatisch generierte Eigenschaft verwenden", Justification:="<Ausstehend>")>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Unnötige Unterdrückung entfernen", Justification:="<Ausstehend>")>
        Private _FilePath As String = String.Empty ' Verzeichnis, in dem die INI-Datei liegt (ohne Dateiname)

        Private _FileContent() As String = {$""} ' Aktueller Dateiinhalt als Zeilenpuffer (so, wie er gespeichert/geladen wird)

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0032:Automatisch generierte Eigenschaft verwenden", Justification:="<Ausstehend>")>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Unnötige Unterdrückung entfernen", Justification:="<Ausstehend>")>
        Private _CommentPrefix As Char = ";"c ' Prefixzeichen für Kommentarzeilen (typisch ';', alternativ denkbar '#')

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0032:Automatisch generierte Eigenschaft verwenden", Justification:="<Ausstehend>")>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Unnötige Unterdrückung entfernen", Justification:="<Ausstehend>")>
        Private _AutoSave As Boolean = False ' Wenn True, werden Änderungen an den internen Strukturen automatisch auf die Datei geschrieben

        Private _FileComment As New System.Collections.Generic.List(Of String) ' Kommentarzeilen am Anfang der Datei (ohne Prefixzeichen)

        Private _Sections As New System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.Dictionary(Of String, String)) ' Abschnitte mit Einträgen: Abschnittsname -> (Eintragsname -> Wert)

        Private _SectionsComments As New System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.List(Of String)) ' Abschnittskommentare: Abschnittsname -> Liste der Kommentarzeilen (ohne Prefix)

        Private _CurrentSectionName As String = $"" ' Name des Abschnitts, der beim Parsen gerade verarbeitet wird (Parserzustand)

        Private _FileSaved As Boolean = False ' Status, ob der aktuelle Zustand auf Datenträger gespeichert ist

#End Region

#Region "Ereignisse"

        ''' <summary>
        ''' Wird ausgelöst wenn sich der Dateiinhalt geändert hat.
        ''' </summary>
        ''' <remarks>
        ''' Dieses Ereignis wird nach jeder Änderung an der internen Struktur
        ''' (Add/Rename/Delete/Set) ausgelöst, unabhängig davon, ob AutoSave aktiv ist.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[AddHandler ini.FileContentChanged, Sub(s, e) Console.WriteLine("Inhalt geändert")
        ''' ini.SetEntryValue("Allgemein", "Version", "1.2.3")]]></code>
        ''' </example>
        <System.ComponentModel.Description("Wird ausgelöst wenn sich der Dateiinhalt geändert hat.")>
        Public Event FileContentChanged(sender As Object, e As System.EventArgs)

        ''' <summary>
        ''' Wird ausgelöst wenn beim anlegen eines neuen Abschnitts oder umbnennen eines
        ''' Abschnitts der Name bereits vorhanden ist.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[AddHandler ini.SectionNameExist, Sub(s, e) Console.WriteLine("Abschnittsname existiert")
        ''' ini.AddSection("Allgemein")
        ''' ini.AddSection("Allgemein") ' triggert Ereignis]]></code>
        ''' </example>
        <System.ComponentModel.Description("Wird ausgelöst wenn beim anlegen eines neuen Abschnitts oder umbnennen eines Abschnitts der Name bereits vorhanden ist.")>
        Public Event SectionNameExist(sender As Object, e As System.EventArgs)

        ''' <summary>
        ''' Wird ausgelöst wenn beim anlegen eines neuen Eintrags oder umbenennen eines
        ''' Eintrags der Name bereitsvorhanden ist.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[AddHandler ini.EntryNameExist, Sub(s, e) Console.WriteLine("Eintragsname existiert")
        ''' ini.AddSection("Allgemein")
        ''' ini.AddEntry("Allgemein", "AppName")
        ''' ini.AddEntry("Allgemein", "AppName") ' triggert Ereignis]]></code>
        ''' </example>
        <System.ComponentModel.Description("Wird ausgelöst wenn beim anlegen eines neuen Eintrags oder umbenennen eines Eintrags der Name bereits vorhanden ist.")>
        Public Event EntryNameExist(sender As Object, e As System.EventArgs)

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Zeigt an ob die Datei gespeichert wurde
        ''' </summary>
        ''' <remarks>
        ''' True bedeutet, dass der aktuelle Zustand auf Datenträger geschrieben wurde
        ''' (entweder durch expliziten Aufruf von SaveFile oder automatisch, wenn
        ''' AutoSave=True).
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[If ini.FileSaved Then
        '''     Console.WriteLine("Gespeichert")
        ''' End If]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(False)>
        Public ReadOnly Property FileSaved As Boolean
            Get
                Return Me._FileSaved
            End Get
        End Property

        ''' <summary>
        ''' Gibt das Prefixzeichen für Kommentare zurück oder legt dieses fest.
        ''' </summary>
        ''' <remarks>
        ''' Wird beim Erzeugen/Analysieren der Datei verwendet.<br/>
        ''' Änderungen wirken sich auf die Ausgabe in CreateFileContent aus.<br/>
        ''' Beim Parsen wird das jeweils aktuell gesetzte Prefix zur Erkennung von
        ''' Kommentarzeilen herangezogen.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[ini.CommentPrefix = "#"c
        ''' ini.CreateNewFile()]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Design")>
        <System.ComponentModel.Description("Gibt das Prefixzeichen für Kommentare zurück oder legt dieses fest.")>
        Public Property CommentPrefix As Char
            Get
                Return Me._CommentPrefix
            End Get
            Set
                Me._CommentPrefix = Value
            End Set
        End Property

        ''' <summary>
        ''' Gibt den aktuellen Dateiname zurück oder legt diesen fest
        ''' </summary>
        ''' <remarks>
        ''' Der Name wird beim Speichern/Laden mit dem Pfad kombiniert.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[ini.FileName = "settings.ini"]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Design")>
        <System.ComponentModel.Description("Gibt den aktuellen Dateiname zurück oder legt diesen fest")>
        Public Property FileName As String
            Set(value As String)
                Me._FileName = value
            End Set
            Get
                Return Me._FileName
            End Get
        End Property

        ''' <summary>
        ''' Gibt den Pfad zur INI-Datei zurück oder legt diesen fest.
        ''' </summary>
        ''' <remarks>
        ''' Beim Speichern/Laden wird der Pfad mit dem Dateinamen kombiniert.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[ini.FilePath = "C:\\Temp"]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Design")>
        <System.ComponentModel.Description("Gibt den Pfad zur INI-Datei zurück oder legt diesen fest.")>
        Public Property FilePath As String
            Get
                Return Me._FilePath
            End Get
            Set
                Me._FilePath = Value
            End Set
        End Property

        ''' <summary>
        ''' Legt das Speicherverhalten der Klasse fest.
        ''' </summary>
        ''' <remarks>
        ''' True legt fest das Änderungen automatisch gespeichert werden.<br/>
        ''' Bei False bleibt der Status unsaved, bis SaveFile explizit aufgerufen wird.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[
        ''' ini.AutoSave = True
        ''' ini.SetFileComment(New String() {"Kommentar"}) ' wird automatisch gespeichert]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Design")>
        <System.ComponentModel.Description("Legt das Speicherverhalten der Klasse fest.")>
        Public Property AutoSave As Boolean
            Get
                Return Me._AutoSave
            End Get
            Set
                Me._AutoSave = Value
            End Set
        End Property

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Erzeugt eine neue Datei mit Beispielinhalt und Standard-Kommentarprefix
        ''' </summary>
        ''' <remarks>
        ''' Diese Methode ruft <seealso cref="CreateNewFile(Char)"/> mit "Nothing" auf.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[ini.CreateNewFile()
        ''' Dim lines = ini.GetFileContent()]]></code>
        ''' </example>
        ''' <seealso cref="CreateNewFile(Char)">CreateNewFile</seealso>
        Public Sub CreateNewFile()
            Me.CreateNewFile(Nothing)
        End Sub

        ''' <summary>
        ''' Erzeugt eine neue Datei mit Beispielinhalt
        ''' </summary>
        ''' <remarks>
        ''' Wenn kein Prefixzeichen angegeben wird, wird Standardmäßig das Semikolon
        ''' verwendet.<br/>
        ''' Diese Methode erstellt eine Beispielstruktur mit den Abschnitten [Allgemein],
        ''' [Datenbank], [Logging].
        ''' </remarks>
        ''' <param name="CommentPrefix">Prefixzeichen für Kommentare oder "Nothing".</param>
        ''' <example>
        ''' <code><![CDATA[ini.CreateNewFile("#"c)]]></code>
        ''' </example>
        Public Sub CreateNewFile(CommentPrefix As Char)

            ' wenn Prefixzeichen nicht angegeben wurde -> Standardwert verwenden
            If CommentPrefix = Nothing Then
                Me.CommentPrefix = ";"c
            Else
                ' ansonsten angegebenes Prefix übernehmen
                Me.CommentPrefix = CommentPrefix
            End If

            ' Dateiinhalt erzeugen (Beispielinhalt)
            Dim content As String =
                $"{Me._CommentPrefix} INI - Datei Beispiel {vbCrLf}" &
                $"{Me._CommentPrefix} Diese Datei wurde von {My.Application.Info.AssemblyName} erzeugt{vbCrLf}{vbCrLf}" &
                $"[Allgemein]{vbCrLf}" &
                $"{Me._CommentPrefix} Anwendungsname und Version{vbCrLf}" &
                $"AppName = MeineApp{vbCrLf}" &
                $"Version = 1.0.0{vbCrLf}{vbCrLf}" &
                $"[Datenbank]{vbCrLf}" &
                $"{Me._CommentPrefix} Einstellungen zur Datenbank{vbCrLf}" &
                $"Server = localhost{vbCrLf}" &
                $"Port = 3306{vbCrLf}" &
                $"Benutzername = admin{vbCrLf}" &
                $"Passwort = geheim{vbCrLf}{vbCrLf}" &
                $"[Logging]{vbCrLf}" &
                $"{Me._CommentPrefix} Einstellungen zum Logging{vbCrLf}" &
                $"LogLevel = Debug{vbCrLf}" &
                $"LogDatei = logs / app.log{vbCrLf}"

            Me._FileContent = content.Split(CChar(vbCrLf)) ' Rohinhalt in Zeilenpuffer übertragen
            Me.ParseFileContent() ' Dateiinhalt analysieren und interne Strukturen aufbauen
            Me._FileSaved = False ' Datei als ungespeichert markieren, da im Speicher verändert
            RaiseEvent FileContentChanged(Me, System.EventArgs.Empty) ' Änderungen signalisieren

        End Sub

        ''' <summary>
        ''' Lädt die angegebene Datei
        ''' </summary>
        ''' <remarks>
        ''' Diese Überladung setzt Pfad und Dateiname und ruft anschließend LoadFile() ohne
        ''' Parameter auf.
        ''' </remarks>
        ''' <param name="FilePathAndName">Name und Pfad der Datei die geladen werden
        ''' soll.</param>
        ''' <example>
        ''' <code><![CDATA[ini.LoadFile("C:\\Temp\\settings.ini")]]></code>
        ''' </example>
        Public Sub LoadFile(FilePathAndName As String)

            ' Parameter überprüfen
            If String.IsNullOrWhiteSpace(FilePathAndName) Then
                Throw New System.ArgumentException("Der Parameter FilePathAndName darf nicht NULL oder ein Leerraumzeichen sein.", NameOf(FilePathAndName))
            End If

            ' Pfad und Name der Datei merken
            Me._FilePath = System.IO.Path.GetDirectoryName(FilePathAndName)
            Me._FileName = System.IO.Path.GetFileName(FilePathAndName)
            Me.LoadFile()

        End Sub

        ''' <summary>
        ''' Lädt die Datei die in <see cref="FilePath"/> angegeben wurde.
        ''' </summary>
        ''' <remarks>
        ''' Liest alle Zeilen, parst sie in die internen Strukturen, markiert den Zustand
        ''' als gespeichert und löst das FileContentChanged-Ereignis aus.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[ini.FilePath = "C:\\Temp"
        ''' ini.FileName = "settings.ini"
        ''' ini.LoadFile()]]></code>
        ''' </example>
        Public Sub LoadFile()
            ' Datei laden mit Fehlerbehandlung
            Dim filepathandname As String = System.IO.Path.Combine(Me._FilePath, Me._FileName)
            Try
                Me._FileContent = System.IO.File.ReadAllLines(filepathandname)
                Me.ParseFileContent() ' Dateiinhalt analysieren
                Me._FileSaved = True ' Datei als gespeichert markieren
                RaiseEvent FileContentChanged(Me, System.EventArgs.Empty) ' Ereignis auslösen
            Catch ex As System.IO.IOException
                Throw New System.IO.IOException($"Fehler beim laden der Datei {filepathandname}.") ' Fehlerbehandlung für IO-Fehler
            End Try
        End Sub

        ''' <summary>
        ''' Speichert die angegebene Datei.
        ''' </summary>
        ''' <remarks>
        ''' Setzt Pfad und Dateiname und ruft SaveFile() ohne Parameter auf.
        ''' </remarks>
        ''' <param name="FilePathAndName">Name und Pfad der Datei die gespeichert werden
        ''' soll.</param>
        ''' <example>
        ''' <code><![CDATA[ini.SaveFile("C:\\Temp\\settings.ini")]]></code>
        ''' </example>
        Public Sub SaveFile(FilePathAndName As String)
            'Parameter überprüfen
            If String.IsNullOrWhiteSpace(FilePathAndName) Then
                Throw New System.ArgumentException("Der Parameter FilePathAndName darf nicht NULL oder ein Leerraumzeichen sein.", NameOf(FilePathAndName))
            End If
            'Pfad und Name der Datei merken
            Me._FilePath = System.IO.Path.GetDirectoryName(FilePathAndName)
            Me._FileName = System.IO.Path.GetFileName(FilePathAndName)
            Me.SaveFile()
        End Sub

        ''' <summary>
        ''' Gibt den Dateiinhalt zurück
        ''' </summary>
        ''' <remarks>
        ''' Dies ist der aktuelle, generierte Rohinhalt (Zeilen), so wie er gespeichert
        ''' werden würde.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[Dim lines = ini.GetFileContent()
        ''' For Each l In lines : Console.WriteLine(l) : Next]]></code>
        ''' </example>
        Public Function GetFileContent() As String()

            Return Me._FileContent

        End Function

        ''' <summary>
        ''' Gibt den Dateikommentar zurück
        ''' </summary>
        ''' <remarks>
        ''' Die Kommentarzeilen werden ohne Prefixzeichen zurückgegeben. Beim Erzeugen des
        ''' Datei-Inhalts wird das Prefix automatisch vorangestellt.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[Dim comment = ini.GetFileComment()]]></code>
        ''' </example>
        Public Function GetFileComment() As String()

            Return Me._FileComment.ToArray

        End Function

        ''' <summary>
        ''' Setzt den Dateikommentar.
        ''' </summary>
        ''' <remarks>
        ''' Die übergebenen Zeilen sollten keine Prefixzeichen enthalten. Nach dem Setzen
        ''' wird der Dateiinhalt neu aufgebaut (und ggf. gespeichert, wenn AutoSave=True).
        ''' </remarks>
        ''' <param name="CommentLines">Die Zeilen des Dateikommentars.</param>
        ''' <example>
        ''' <code><![CDATA[ini.SetFileComment(New String() {"Dies ist eine Konfigurationsdatei.", "Bitte nicht editieren."})]]></code>
        ''' </example>
        Public Sub SetFileComment(CommentLines() As String)

            ' alten Dateikommentar löschen
            Me._FileComment.Clear()
            ' neuen Dateikommentar übenehmen
            Me._FileComment.AddRange(CommentLines)
            ' Änderungen übernehmen (rekonstruiert _FileContent und speichert ggf.)
            Me.ChangeFileContent()

        End Sub

        ''' <summary>
        ''' Ruft die Abschnittsnamen ab.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[Dim sections = ini.GetSectionNames()]]></code>
        ''' </example>
        Public Function GetSectionNames() As String()
            Dim names As New System.Collections.Generic.List(Of String)
            For Each name As String In Me._Sections.Keys
                names.Add(name)
            Next
            Return names.ToArray
        End Function

        ''' <summary>
        ''' Gibt die Namen der Einträge eines Abschnitts zurück
        ''' </summary>
        ''' <param name="SectionName">Abschnittsname</param>
        ''' <returns>
        ''' Eintragsliste oder Nothing falls <paramref name="SectionName"/> nicht existiert.
        ''' </returns>
        ''' <example>
        ''' <code><![CDATA[Dim entries = ini.GetEntryNames("Allgemein")]]></code>
        ''' </example>
        Public Function GetEntryNames(SectionName As String) As String()
            Dim result() As String = Nothing ' Variable für Rückgabewert
            ' wenn Abschnitt existiert -> Eintagsliste erstellen
            If Me._Sections.ContainsKey(SectionName) Then
                Dim names As New System.Collections.Generic.List(Of String)
                For Each name As String In Me._Sections.Item(SectionName).Keys
                    names.Add(name)
                Next
                result = names.ToArray
            End If
            Return result  ' Eintragsliste oder Nothing zurück
        End Function

        ''' <summary>
        ''' Fügt einen neuen Abschnitt hinzu.
        ''' </summary>
        ''' <remarks>
        ''' Löst SectionNameExist aus und bricht ab, wenn der Abschnitt bereits existiert.
        ''' </remarks>
        ''' <param name="Name">Name des neuen Abschnitts</param>
        ''' <example>
        ''' <code><![CDATA[ini.AddSection("Logging")]]></code>
        ''' </example>
        Public Sub AddSection(Name As String)
            ' Prüfen ob der Name vorhanden ist
            If Me._Sections.ContainsKey(Name) Then
                ' Ereignis auslösen und beenden
                RaiseEvent SectionNameExist(Me, System.EventArgs.Empty)
                Exit Sub
            End If
            Me.AddNewSection(Name) ' neuen Abschnitt erstellen
            Me.ChangeFileContent() ' Änderungen übernehmen
        End Sub

        ''' <summary>
        ''' Fügt einen neuen Eintrag in die Liste der Eintragsnamen ein.
        ''' </summary>
        ''' <remarks>
        ''' Der Abschnitt muss existieren, andernfalls kommt es zu einer Ausnahme.<br/>
        ''' Bei Namenskonflikt wird EntryNameExist ausgelöst und abgebrochen.
        ''' </remarks>
        ''' <param name="Section">Abschnitt in den der Eintrag eingefügt werden
        ''' soll.</param>
        ''' <param name="Name">Name des Eintrags.</param>
        ''' <example>
        ''' <code><![CDATA[ini.AddEntry("Allgemein", "AppName")]]></code>
        ''' </example>
        Public Sub AddEntry(Section As String, Name As String)
            ' Prüfen ob der Name vorhanden ist
            If Me._Sections.Item(Section).ContainsKey(Name) Then
                ' Ereignis auslösen und beenden
                RaiseEvent EntryNameExist(Me, System.EventArgs.Empty)
                Exit Sub
            End If
            Me.AddNewEntry(Section, Name) ' neuen Eintrag erstellen
            Me.ChangeFileContent() ' Änderungen übernehmen
        End Sub

        ''' <summary>
        ''' Benennt einen Abschnitt um.
        ''' </summary>
        ''' <remarks>
        ''' Es werden sowohl der Abschnitt (Werte) als auch sein Kommentar umgehängt.<br/>
        ''' Bei Namenskonflikt wird SectionNameExist ausgelöst.
        ''' </remarks>
        ''' <param name="OldName">alter Name des Abschnitts</param>
        ''' <param name="NewName">neuer name des Abschnitts</param>
        ''' <example>
        ''' <code><![CDATA[ini.RenameSection("Allgemein", "General")]]></code>
        ''' </example>
        Public Sub RenameSection(OldName As String, NewName As String)
            ' Ist der neue Name bereits vorhanden?
            If Me._Sections.ContainsKey(NewName) Then
                ' Ereignis auslösen und beenden
                RaiseEvent SectionNameExist(Me, System.EventArgs.Empty)
                Exit Sub
            End If
            Me.RenameSectionValue(OldName, NewName) ' Name-Wert-Paar des Abschnitts umbenennen
            Me.RenameSectionComment(OldName, NewName) ' Name-Kommentar-Paar umbenennen
            Me.ChangeFileContent()  ' Änderungen übernehmen
        End Sub

        ''' <summary>
        ''' Benennt einen Eintrag in einem Abschnitt um.
        ''' </summary>
        ''' <remarks>
        ''' Der Abschnitt muss existieren.<br/>
        ''' Bei Namenskonflikt wird EntryNameExist ausgelöst.
        ''' </remarks>
        ''' <param name="Section">Abschnitt der den Eintrag enthält.</param>
        ''' <param name="Oldname"></param>
        ''' <param name="NewName">Neuer Name des Eintrags.</param>
        ''' <example>
        ''' <code><![CDATA[ini.RenameEntry("Allgemein", "AppName", "ApplicationName")]]></code>
        ''' </example>
        Public Sub RenameEntry(Section As String, Oldname As String, NewName As String)
            ' Ist der neue Name bereits vorhanden? 
            If Me._Sections.Item(Section).ContainsKey(NewName) Then
                ' Ereignis auslösen und beenden
                RaiseEvent EntryNameExist(Me, System.EventArgs.Empty)
                Exit Sub
            End If
            Me.RenameEntryvalue(Section, Oldname, NewName) ' Name-Wert-Paar des Eintrags umbenennen
            Me.ChangeFileContent()  ' Änderungen übernehmen
        End Sub

        ''' <summary>
        ''' Löscht einen Abschnitt
        ''' </summary>
        ''' <remarks>
        ''' Entfernt auch den dazugehörigen Abschnittskommentar.
        ''' </remarks>
        ''' <param name="Name">Name des Abschnittes</param>
        ''' <example>
        ''' <code><![CDATA[ini.DeleteSection("Logging")]]></code>
        ''' </example>
        Public Sub DeleteSection(Name As String)

            ' Abschnitt aus den Listen für Abschnitte und Abschnittskommentare entfernen
            Dim unused = Me._Sections.Remove(Name)
            Dim unused1 = Me._SectionsComments.Remove(Name)
            ' Änderungen übernehmen
            Me.ChangeFileContent()

        End Sub

        ''' <summary>
        ''' Löscht einen Eintrag aus einem Abschnitt.
        ''' </summary>
        ''' <param name="Section">Abschnitt aus dem der Eintrag gelöscht werden
        ''' soll.</param>
        ''' <param name="Entry">Eintrag der gelöscht werden soll.</param>
        ''' <example>
        ''' <code><![CDATA[ini.DeleteEntry("Allgemein", "Version")]]></code>
        ''' </example>
        Public Sub DeleteEntry(Section As String, Entry As String)

            ' Eintrag aus der Liste der Einträge entfernen
            Dim unused = Me._Sections.Item(Section).Remove(Entry)
            ' Änderungen übernehmen
            Me.ChangeFileContent()

        End Sub

        ''' <summary>
        ''' Gibt die Kommentarzeilen für einen Abschnitt zurück
        ''' </summary>
        ''' <param name="SectionName">Name des Abschnitts</param>
        ''' <returns>
        ''' Kommentar für <paramref name="SectionName"/> oder Nothing wenn kein Kommentar
        ''' existiert.
        ''' </returns>
        ''' <example>
        ''' <code><![CDATA[Dim comment = ini.GetSectionComment("Allgemein")]]></code>
        ''' </example>
        Public Function GetSectionComment(SectionName As String) As String()

            ' Variable für Rückgabewert
            Dim result() As String = Nothing
            ' wenn Abschnitt einen Kommentar enthält -> Kommentar holen 
            If Me._SectionsComments.ContainsKey(SectionName) Then
                result = Me._SectionsComments.Item(SectionName).ToArray
            End If
            ' Kommentar oder Nothing zurück
            Return result

        End Function

        ''' <summary>
        ''' Gibt den Wert eines Eintrags aus einem Abschnitt zurück
        ''' </summary>
        ''' <remarks>
        ''' Erwartet, dass Abschnitt und Eintrag existieren.<br/>
        ''' Andernfalls kann eine Ausnahme geworfen werden.<br/>
        ''' Bei leerem Abschnitts- und Eintragsnamen wird ein leerer String zurückgegeben.
        ''' </remarks>
        ''' <param name="Section">Abschnitt aus dem der Wert des Eintrags gelesen werden
        ''' soll.</param>
        ''' <param name="Entry">Eintrag dessen Wert gelesen werden soll.</param>
        ''' <returns>
        ''' Wert des Eintrags.
        ''' </returns>
        ''' <example>
        ''' <code><![CDATA[Dim value = ini.GetEntryValue("Allgemein", "AppName")]]></code>
        ''' </example>
        Public Function GetEntryValue(Section As String, Entry As String) As String

            ' wenn Abschnitt und Eintrag existieren -> Wert zurückgeben ansonsten Null
            Dim result = If(
                String.IsNullOrEmpty(Section) AndAlso String.IsNullOrEmpty(Entry),
                String.Empty,
                Me._Sections.Item(Section).Item(Entry))
            ' Ergebnis zurückgeben
            Return result

        End Function

        ''' <summary>
        ''' Setzt den Kommentar für einen Abschnitt.
        ''' </summary>
        ''' <remarks>
        ''' Die übergebenen Zeilen sollten ohne Prefixzeichen sein.
        ''' </remarks>
        ''' <param name="Name">Name des Abschnitts.</param>
        ''' <param name="CommentLines">Kommentarzeilen</param>
        ''' <example>
        ''' <code><![CDATA[ini.SetSectionComment("Allgemein", New String() {"Basisdaten"})]]></code>
        ''' </example>
        Public Sub SetSectionComment(Name As String, CommentLines() As String)

            ' Fehlerprüfung
            If String.IsNullOrEmpty(Name) Then
                Dim unused = System.Windows.Forms.MessageBox.Show(
                    $"Es wurde kein Abschnitt ausgewählt!",
                    $"Fehler",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error)
                Exit Sub
            End If

            ' geänderten Abschnittskommentar übernehmen
            Me._SectionsComments.Item(Name).Clear()
            Me._SectionsComments.Item(Name).AddRange(CommentLines)

            ' Änderungen übernehmen
            Me.ChangeFileContent()

        End Sub

        ''' <summary>
        ''' Setzt den Wert eines Eintrags in einem Abschnitt.
        ''' </summary>
        ''' <remarks>
        ''' Der Abschnitt und der Eintrag müssen existieren.
        ''' </remarks>
        ''' <param name="Section">Abschnitt in dem der Wert eines Eintrags geändert werden
        ''' soll.</param>
        ''' <param name="Entry">Eintrag dessen Wert geändert werden soll.</param>
        ''' <param name="Value">Der geänderte Wert.</param>
        ''' <example>
        ''' <code><![CDATA[ini.SetEntryValue("Allgemein", "Version", "1.2.3")]]></code>
        ''' </example>
        Public Sub SetEntryValue(Section As String, Entry As String, Value As String)

            ' Fehlerprüfung
            If String.IsNullOrEmpty(Section) Then
                Dim unused = System.Windows.Forms.MessageBox.Show(
                    $"Es wurde kein Eintrag ausgewählt!",
                    $"Fehler",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error)
                Exit Sub
            End If

            ' geänderten Wert übenehmen
            Me._Sections.Item(Section).Item(Entry) = Value

            ' Änderungen übernehmen
            Me.ChangeFileContent()

        End Sub

#End Region

#Region "interne Methoden"

        ' Übenimmt die Ändeungen uns speichert die Datei
        Private Sub ChangeFileContent()
            Me.CreateFileContent()  ' Dateiinhalt neu erzeugen 
            If Me._AutoSave Then ' wenn automatisch speichern aktiv
                Me.SaveFile() ' Änderung speichern
            Else
                Me._FileSaved = False 'ansonsten Datei als ungespeichert markieren
            End If
            RaiseEvent FileContentChanged(Me, System.EventArgs.Empty) ' Ereignis auslösen
        End Sub

        ' Speichert die in FilePath angegebene Datei.
        Private Sub SaveFile()
            Dim filepathandname As String = System.IO.Path.Combine(Me._FilePath, Me._FileName)
            System.IO.File.WriteAllLines(filepathandname, Me._FileContent) ' Dateiinhalt auf Datenträger schreiben
            Me._FileSaved = True ' Datei als gespeichert markieren
        End Sub

        ' Fügt einen neuen Abschnitt hinzu.
        Private Sub AddNewSection(Name As String)
            Me._Sections.Add(Name, New System.Collections.Generic.Dictionary(Of String, String)) ' Name-Wert-Paar hinzufügen
            Me._SectionsComments.Add(Name, New System.Collections.Generic.List(Of String)) ' Name-Kommentar-Paar hinzufügen
        End Sub

        ' fügt einen neuen Eintrag in einen Abschnitt ein.
        Private Sub AddNewEntry(Section As String, Name As String)
            Me._Sections.Item(Section).Add(Name, $"")
        End Sub

        ' Benennt das Key-Comment-Paar eines Abschnitts um.
        Private Sub RenameSectionComment(OldName As String, newName As String)
            Dim oldcomment = Me._SectionsComments.Item(OldName) ' alten Kommentar speichern
            Dim unused1 = Me._SectionsComments.Remove(OldName) ' Abschnitt entfernen
            Me._SectionsComments.Add(newName, oldcomment) ' neuen Abschnitt mit altem Kommentar erstellen
        End Sub

        ' Benennt das Key-Value-Paar eines Abschnitts um.
        Private Sub RenameSectionValue(OldName As String, NewName As String)
            Dim oldvalue = Me._Sections.Item(OldName) ' alten Wert speichern
            Dim unused = Me._Sections.Remove(OldName) ' Abschnitt entfernen
            Me._Sections.Add(NewName, oldvalue) ' neuen Abschnitt mit altem Wert erstellen
        End Sub

        ' Benennt einen Eintrag in einem Abschnitt um.
        Private Sub RenameEntryvalue(Section As String, OldName As String, NewName As String)
            Dim oldvalue = Me._Sections.Item(Section).Item(OldName) ' alten Wert speichern
            Dim unused = Me._Sections.Item(Section).Remove(OldName) ' Eintrag entfernen
            Me._Sections.Item(Section).Add(NewName, oldvalue) ' neuen Eintrag mit altem Wert erstellen
        End Sub

        ' Erzeugt den Dateiinhalt
        Private Sub CreateFileContent()
            Dim filecontent As New System.Collections.Generic.List(Of String) ' Zeilenliste
            For Each line As String In Me._FileComment ' Dateikommentarzeilen anfügen
                filecontent.Add(Me._CommentPrefix & $" " & line)
            Next
            filecontent.Add($"") ' Leerzeile anfügen
            For Each sectionname As String In Me._Sections.Keys ' alle Abschnitte durchlaufen
                filecontent.Add($"[" & sectionname & $"]") ' Abschnittsname hinzufügen
                For Each commentline As String In Me._SectionsComments.Item(sectionname) ' Zeilen des Abschnittskommentars durchlaufen
                    filecontent.Add(Me._CommentPrefix & $" " & commentline) ' Kommentarzeile einfügen
                Next
                Dim entryline As String
                For Each entryname As String In Me._Sections.Item(sectionname).Keys ' alle Eintragszeilen durchlaufen
                    entryline = entryname & $" = " & Me._Sections.Item(sectionname).Item(entryname)  ' Eintragszeile erzeugen und einfügen
                    filecontent.Add(entryline)
                Next
                filecontent.Add($"") ' Leerzeile anfügen
            Next
            Me._FileContent = filecontent.ToArray ' Dateiinhalt erzeugen
        End Sub

        ' analysiert den Dateiinhalt
        Private Sub ParseFileContent()
            Me.InitParseVariables() ' Variablen initialisieren
            Me._CurrentSectionName = $"" ' aktueller Abschnittsname
            For Each line As String In Me._FileContent ' alle Zeilen des Dateiinhaltes durchlaufen
                line = line.Trim ' Leerzeichen am Anfang und Ende der Zeile entfernen
                Me.LineAnalyse(line) ' aktuelle Zeile analysieren
            Next
        End Sub

        ' Analysiert eine Zeile.
        Private Sub LineAnalyse(LineContent As String)
            If (String.IsNullOrEmpty(Me._CurrentSectionName)) And LineContent.StartsWith(Me._CommentPrefix) Then
                Me.AddFileCommentLine(LineContent) ' noch kein Abschnitt gefunden und Zeile startet mit Prefix -> Dateikommentar hinzufügen
            ElseIf LineContent.StartsWith("[") And LineContent.EndsWith("]") Then
                Me.AddSectionNameLine(LineContent) ' Zeile enthält eckige Klammern -> Abschnittsname hinzufügen
            ElseIf (Not String.IsNullOrEmpty(Me._CurrentSectionName)) And LineContent.StartsWith(Me._CommentPrefix) Then
                Me.AddSectionCommentLine(LineContent)  ' aktueller Abschnitt und Zeile startet mit Prefix -> Abschnittskommentar hinzufügen
            ElseIf (Not String.IsNullOrEmpty(Me._CurrentSectionName)) And LineContent.Contains("=") Then
                Me.AddEntryLine(LineContent) ' aktueller Abschnitt und Zeile enthält Gleichheitszeichen -> Eintrag hinzufügen
            End If
        End Sub

        ' fügt einen Eintrag hinzu
        Private Sub AddEntryLine(LineContent As String)
            ' Eintagszeile in Name und Wert trennen
            Dim name As String = LineContent.Split("="c)(0).Trim
            Dim value As String = LineContent.Split("="c)(1).Trim
            Me._Sections.Item(Me._CurrentSectionName).Add(name, value)  ' Eintrag hinzufügen
        End Sub

        ' fügt eine Abschnittskommentarzeile hinzu
        Private Sub AddSectionCommentLine(LineContent As String)
            Dim line As String = LineContent.Substring(1, LineContent.Length - 1).Trim ' Prefix und eventuelle Leerzeichen am Anfang und Ende entfernen
            Me._SectionsComments.Item(Me._CurrentSectionName).Add(line) ' Kommentarzeile hinzufügen
        End Sub

        ' fügt einen Abschnittsname hinzu
        Private Sub AddSectionNameLine(LineContent As String)
            Dim line = LineContent.Substring(1, LineContent.Length - 2).Trim ' Klammern und eventuelle Leerzeichen am Anfang und Ende entfernen
            Me._CurrentSectionName = line ' Abschnittsname merken
            ' neuen Abschnitt erstellen
            Me._Sections.Add(Me._CurrentSectionName, New System.Collections.Generic.Dictionary(Of String, String))
            Me._SectionsComments.Add(Me._CurrentSectionName, New System.Collections.Generic.List(Of String))
        End Sub

        ' fügt eine Dateikommentarzeile hinzu
        Private Sub AddFileCommentLine(LineContent As String)
            Dim line = LineContent.Substring(1, LineContent.Length - 1).Trim ' Prefix und eventuelle Leerzeichen am Anfang und Ende entfernen
            Me._FileComment.Add(line) ' Zeile in den Dateikommentar übernehmen
        End Sub

        ' Initialisiert die Variablen für den Parser
        Private Sub InitParseVariables()
            Me._FileComment = New System.Collections.Generic.List(Of String)
            Me._Sections = New System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.Dictionary(Of String, String))
            Me._SectionsComments = New System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.List(Of String))
        End Sub

        ' Platzhalter für Designer-Unterstützung (keine Implementierung notwendig)
        Private Sub InitializeComponent()
        End Sub

#End Region

    End Class

End Namespace