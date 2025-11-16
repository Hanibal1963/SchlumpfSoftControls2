' *************************************************************************************************
' IniFile.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

' TODO: Code noch überarbeiten

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO

'Imports SchlumpfSoft.Controls.Attribute
Imports Microsoft.VisualBasic

Namespace IniFileControl

    '''' <summary>
    '''' Steuerelement zum Verwalten von INI - Dateien
    '''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Steuerelement zum Verwalten von INI - Dateien")>
    <ToolboxBitmap(GetType(IniFileControl.IniFile), "IniFile.bmp")>
    <ToolboxItem(True)>
    Public Class IniFile : Inherits Component

#Region "Definition der Variablen"

        ' Name der Datei (nur der Dateiname, ohne Pfad)
        Private _FileName As String = $"neue Datei.ini"
        ' Verzeichnis, in dem die INI-Datei liegt (ohne Dateiname)
        Private _FilePath As String = String.Empty
        ' Aktueller Dateiinhalt als Zeilenpuffer (so, wie er gespeichert/geladen wird)
        Private _FileContent() As String = {$""}
        ' Prefixzeichen für Kommentarzeilen (typisch ';', alternativ denkbar '#')
        Private _CommentPrefix As Char = ";"c
        ' Wenn True, werden Änderungen an den internen Strukturen automatisch auf die Datei geschrieben
        Private _AutoSave As Boolean = False
        ' Kommentarzeilen am Anfang der Datei (ohne Prefixzeichen)
        Private _FileComment As New List(Of String)
        ' Abschnitte mit Einträgen: Abschnittsname -> (Eintragsname -> Wert)
        Private _Sections As New Dictionary(Of String, Dictionary(Of String, String))
        ' Abschnittskommentare: Abschnittsname -> Liste der Kommentarzeilen (ohne Prefix)
        Private _SectionsComments As New Dictionary(Of String, List(Of String))
        ' Name des Abschnitts, der beim Parsen gerade verarbeitet wird (Parserzustand)
        Private _CurrentSectionName As String = $""
        ' Status, ob der aktuelle Zustand auf Datenträger gespeichert ist
        Private _FileSaved As Boolean = False

#End Region

#Region "Definition der Ereignisse"

        ''' <summary>
        ''' Wird ausgelöst wenn sich der Dateiinhalt geändert hat.
        ''' </summary>
        ''' <remarks>
        ''' Dieses Ereignis wird nach jeder Änderung an der internen Struktur
        ''' (Add/Rename/Delete/Set) ausgelöst, unabhängig davon, ob AutoSave aktiv ist.
        ''' </remarks>
        <Description("Wird ausgelöst wenn sich der Dateiinhalt geändert hat.")>
        Public Event FileContentChanged(sender As Object, e As EventArgs)

        ''' <summary>
        ''' Wird ausgelöst wenn beim anlegen eines neuen Abschnitts oder 
        ''' umbnennen eines Abschnitts der Name bereits vorhanden ist.
        ''' </summary>
        <Description("Wird ausgelöst wenn beim anlegen eines neuen Abschnitts oder umbnennen eines Abschnitts der Name bereits vorhanden ist.")>
        Public Event SectionNameExist(sender As Object, e As EventArgs)

        ''' <summary>
        ''' Wird ausgelöst wenn beim anlegen eines neuen Eintrags oder 
        ''' umbenennen eines Eintrags der Name bereitsvorhanden ist.
        ''' </summary>
        <Description("Wird ausgelöst wenn beim anlegen eines neuen Eintrags oder umbenennen eines Eintrags der Name bereits vorhanden ist.")>
        Public Event EntryNameExist(sender As Object, e As EventArgs)

#End Region

#Region "Definition neuer Eigenschaften"

        ''' <summary>
        ''' Zeigt an ob die Datei gespeichert wurde
        ''' </summary>
        ''' <remarks>
        ''' True bedeutet, dass der aktuelle Zustand auf Datenträger geschrieben wurde
        ''' (entweder durch expliziten Aufruf von SaveFile oder automatisch, wenn AutoSave=True).
        ''' </remarks>
        <Browsable(False)>
        Public ReadOnly Property FileSaved As Boolean
            Get
                Return Me._FileSaved
            End Get
        End Property

        '''' <summary>
        '''' Gibt das Prefixzeichen für Kommentare zurück oder legt dieses fest.
        '''' </summary>
        ''' <remarks>
        ''' Wird beim Erzeugen/Analysieren der Datei verwendet. Änderungen wirken sich
        ''' auf die Ausgabe in CreateFileContent aus. Beim Parsen wird das jeweils
        ''' aktuell gesetzte Prefix zur Erkennung von Kommentarzeilen herangezogen.
        ''' </remarks>
        <Browsable(True)>
        <Category("Design")>
        <Description("Gibt das Prefixzeichen für Kommentare zurück oder legt dieses fest.")>
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
        <Browsable(True)>
        <Category("Design")>
        <Description("Gibt den aktuellen Dateiname zurück oder legt diesen fest")>
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
        <Browsable(True)>
        <Category("Design")>
        <Description("Gibt den Pfad zur INI-Datei zurück oder legt diesen fest.")>
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
        ''' True legt fest das Änderungen automatisch gespeichert werden.
        ''' Bei False bleibt der Status unsaved, bis SaveFile explizit aufgerufen wird.
        ''' </remarks>
        <Browsable(True)>
        <Category("Design")>
        <Description("Legt das Speicherverhalten der Klasse fest.")>
        Public Property AutoSave As Boolean
            Get
                Return Me._AutoSave
            End Get
            Set
                Me._AutoSave = Value
            End Set
        End Property

#End Region

#Region "öffentliche Funktionen"

        ''' <summary>
        ''' Erzeugt eine neue Datei mit Beispielinhalt und Standard-Kommentarprefix
        ''' </summary>
        ''' <remarks>
        ''' Diese Methode ruft <seealso cref="CreateNewFile(Char)"/> mit  "Nothing" auf.
        ''' </remarks>
        Public Sub CreateNewFile()
            Me.CreateNewFile(Nothing)
        End Sub

        ''' <summary>
        ''' Erzeugt eine neue Datei mit Beispielinhalt
        ''' </summary>
        ''' <param name="CommentPrefix">
        ''' Prefixzeichen für Kommentare oder "Nothing".
        ''' </param>
        ''' <remarks>
        ''' Wenn kein Prefixzeichen angegeben wird, wird Standardmäßig das Semikolon verwendet.
        ''' Diese Methode erstellt eine Beispielstruktur mit den Abschnitten [Allgemein], [Datenbank], [Logging].
        ''' </remarks>
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
            ' Rohinhalt in Zeilenpuffer übertragen
            Me._FileContent = content.Split(CChar(vbCrLf))
            Me.ParseFileContent() ' Dateiinhalt analysieren und interne Strukturen aufbauen
            Me._FileSaved = False ' Datei als ungespeichert markieren, da im Speicher verändert
            RaiseEvent FileContentChanged(Me, EventArgs.Empty) ' Änderungen signalisieren
        End Sub

        ''' <summary>
        ''' Lädt die angegebene Datei
        ''' </summary>
        ''' <param name="FilePathAndName">
        ''' Name und Pfad der Datei die geladen werden soll.
        ''' </param>
        ''' <remarks>
        ''' Diese Überladung setzt Pfad und Dateiname und ruft anschließend LoadFile() ohne Parameter auf.
        ''' </remarks>
        Public Sub LoadFile(FilePathAndName As String)

            'Parameter überprüfen
            If String.IsNullOrWhiteSpace(FilePathAndName) Then
                Throw New ArgumentException("Der Parameter FilePathAndName darf nicht NULL oder ein Leerraumzeichen sein.", NameOf(FilePathAndName))
            End If
            'Pfad und Name der Datei merken
            Me._FilePath = Path.GetDirectoryName(FilePathAndName)
            Me._FileName = Path.GetFileName(FilePathAndName)
            Me.LoadFile()

        End Sub

        ''' <summary>
        ''' Lädt die Datei die in <see cref="FilePath"/> angegeben wurde.
        ''' </summary>
        ''' <remarks>
        ''' Liest alle Zeilen, parst sie in die internen Strukturen, markiert den Zustand als gespeichert
        ''' und löst das FileContentChanged-Ereignis aus.
        ''' </remarks>
        Public Sub LoadFile()

            ' Datei laden mit Fehlerbehandlung
            Dim filepathandname As String = Path.Combine(Me._FilePath, Me._FileName)
            Try
                Me._FileContent = IO.File.ReadAllLines(filepathandname)
                ' Dateiinhalt analysieren
                Me.ParseFileContent()
                ' Datei als gespeichert markieren
                Me._FileSaved = True
                ' Ereignis auslösen
                RaiseEvent FileContentChanged(Me, EventArgs.Empty)

            Catch ex As IOException

                ' Fehlerbehandlung für IO-Fehler
                Throw New IOException($"Fehler beim laden der Datei {filepathandname}.")
            End Try

        End Sub

        ''' <summary>
        ''' Speichert die angegebene Datei.
        ''' </summary>
        ''' <param name="FilePathAndName">
        ''' Name und Pfad der Datei die gespeichert werden soll.
        ''' </param>
        ''' <remarks>
        ''' Setzt Pfad und Dateiname und ruft SaveFile() ohne Parameter auf.
        ''' </remarks>
        Public Sub SaveFile(FilePathAndName As String)

            'Parameter überprüfen
            If String.IsNullOrWhiteSpace(FilePathAndName) Then
                Throw New ArgumentException("Der Parameter FilePathAndName darf nicht NULL oder ein Leerraumzeichen sein.", NameOf(FilePathAndName))
            End If
            'Pfad und Name der Datei merken
            Me._FilePath = Path.GetDirectoryName(FilePathAndName)
            Me._FileName = Path.GetFileName(FilePathAndName)
            Me.SaveFile()

        End Sub

        ''' <summary>
        ''' Gibt den Dateiinhalt zurück
        ''' </summary>
        ''' <remarks>
        ''' Dies ist der aktuelle, generierte Rohinhalt (Zeilen), so wie er gespeichert werden würde.
        ''' </remarks>
        Public Function GetFileContent() As String()

            Return Me._FileContent

        End Function

        ''' <summary>
        ''' Gibt den Dateikommentar zurück
        ''' </summary>
        ''' <remarks>
        ''' Die Kommentarzeilen werden ohne Prefixzeichen zurückgegeben.
        ''' Beim Erzeugen des Datei-Inhalts wird das Prefix automatisch vorangestellt.
        ''' </remarks>
        Public Function GetFileComment() As String()

            Return Me._FileComment.ToArray

        End Function

        ''' <summary>
        ''' Setzt den Dateikommentar.
        ''' </summary>
        ''' <param name="CommentLines">
        ''' Die Zeilen des Dateikommentars.
        ''' </param>
        ''' <remarks>
        ''' Die übergebenen Zeilen sollten keine Prefixzeichen enthalten.
        ''' Nach dem Setzen wird der Dateiinhalt neu aufgebaut (und ggf. gespeichert, wenn AutoSave=True).
        ''' </remarks>
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
        Public Function GetSectionNames() As String()

            Dim names As New List(Of String)
            For Each name As String In Me._Sections.Keys
                names.Add(name)
            Next
            Return names.ToArray

        End Function

        ''' <summary>
        ''' Gibt die Namen der Einträge eines Abschnitts zurück
        ''' </summary>
        ''' <param name="SectionName">
        ''' Abschnittsname
        ''' </param>
        ''' <returns>
        ''' Eintragsliste oder Nothing falls <paramref name="SectionName"/> nicht existiert.
        ''' </returns>
        Public Function GetEntryNames(SectionName As String) As String()

            'Variable für Rückgabewert
            Dim result() As String = Nothing
            'wenn Abschnitt existiert -> Eintagsliste erstellen
            If Me._Sections.ContainsKey(SectionName) Then

                Dim names As New List(Of String)
                For Each name As String In Me._Sections.Item(SectionName).Keys
                    names.Add(name)
                Next
                result = names.ToArray

            End If

            'Eintragsliste oder Nothing zurück
            Return result

        End Function

        ''' <summary>
        ''' Fügt einen neuen Abschnitt hinzu.
        ''' </summary>
        ''' <param name="Name">
        ''' Name des neuen Abschnitts
        ''' </param>
        ''' <remarks>
        ''' Löst SectionNameExist aus und bricht ab, wenn der Abschnitt bereits existiert.
        ''' </remarks>
        Public Sub AddSection(Name As String)

            ' Prüfen ob der Name vorhanden ist
            If Me._Sections.ContainsKey(Name) Then
                ' Ereignis auslösen und beenden
                RaiseEvent SectionNameExist(Me, EventArgs.Empty)
                Exit Sub

            End If
            ' neuen Abschnitt erstellen
            Me.AddNewSection(Name)
            ' Änderungen übernehmen
            Me.ChangeFileContent()

        End Sub

        ''' <summary>
        ''' Fügt einen neuen Eintrag in die Liste der Eintragsnamen ein.
        ''' </summary>
        ''' <param name="Section">
        ''' Abschnitt in den der Eintrag eingefügt werden soll.
        ''' </param>
        ''' <param name="Name">
        ''' Name des Eintrags.
        ''' </param>
        ''' <remarks>
        ''' Der Abschnitt muss existieren, andernfalls kommt es zu einer Ausnahme.
        ''' Bei Namenskonflikt wird EntryNameExist ausgelöst und abgebrochen.
        ''' </remarks>
        Public Sub AddEntry(Section As String, Name As String)

            ' Prüfen ob der Name vorhanden ist
            If Me._Sections.Item(Section).ContainsKey(Name) Then
                ' Ereignis auslösen und beenden
                RaiseEvent EntryNameExist(Me, EventArgs.Empty)
                Exit Sub

            End If
            ' neuen Eintrag erstellen
            Me.AddNewEntry(Section, Name)
            ' Änderungen übernehmen
            Me.ChangeFileContent()

        End Sub

        ''' <summary>
        ''' Benennt einen Abschnitt um.
        ''' </summary>
        ''' <param name="OldName">
        ''' alter Name des Abschnitts
        ''' </param>
        ''' <param name="NewName">
        ''' neuer name des Abschnitts
        ''' </param>
        ''' <remarks>
        ''' Es werden sowohl der Abschnitt (Werte) als auch sein Kommentar umgehängt.
        ''' Bei Namenskonflikt wird SectionNameExist ausgelöst.
        ''' </remarks>
        Public Sub RenameSection(OldName As String, NewName As String)

            ' Ist der neue Name bereits vorhanden?
            If Me._Sections.ContainsKey(NewName) Then
                ' Ereignis auslösen und beenden
                RaiseEvent SectionNameExist(Me, EventArgs.Empty)
                Exit Sub

            End If
            ' Name-Wert-Paar des Abschnitts umbenennen
            Me.RenameSectionValue(OldName, NewName)
            ' Name-Kommentar-Paar umbenennen
            Me.RenameSectionComment(OldName, NewName)
            ' Änderungen übernehmen
            Me.ChangeFileContent()

        End Sub

        ''' <summary>
        ''' Benennt einen Eintrag in einem Abschnitt um.
        ''' </summary>
        ''' <param name="Section">
        ''' Abschnitt der den Eintrag enthält.
        ''' </param>
        ''' <param name="NewName">
        ''' Neuer Name des Eintrags.
        ''' </param>
        ''' <remarks>
        ''' Der Abschnitt muss existieren. Bei Namenskonflikt wird EntryNameExist ausgelöst.
        ''' </remarks>
        Public Sub RenameEntry(Section As String, Oldname As String, NewName As String)

            ' Ist der neue Name bereits vorhanden? 
            If Me._Sections.Item(Section).ContainsKey(NewName) Then
                ' Ereignis auslösen und beenden
                RaiseEvent EntryNameExist(Me, EventArgs.Empty)
                Exit Sub

            End If
            ' Name-Wert-Paar des Eintrags umbenennen
            Me.RenameEntryvalue(Section, Oldname, NewName)
            ' Änderungen übernehmen
            Me.ChangeFileContent()

        End Sub

        ''' <summary>
        ''' Löscht einen Abschnitt
        ''' </summary>
        ''' <param name="Name">
        ''' Name des Abschnittes
        ''' </param>
        ''' <remarks>
        ''' Entfernt auch den dazugehörigen Abschnittskommentar.
        ''' </remarks>
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
        ''' <param name="Section">
        ''' Abschnitt aus dem der Eintrag gelöscht werden soll.
        ''' </param>
        ''' <param name="Entry">
        ''' Eintrag der gelöscht werden soll.
        ''' </param>
        Public Sub DeleteEntry(Section As String, Entry As String)

            ' Eintrag aus der Liste der Einträge entfernen
            Dim unused = Me._Sections.Item(Section).Remove(Entry)
            ' Änderungen übernehmen
            Me.ChangeFileContent()

        End Sub

        ''' <summary>
        ''' Gibt die Kommentarzeilen für einen Abschnitt zurück
        ''' </summary>
        ''' <param name="SectionName">
        ''' Name des Abschnitts
        ''' </param>
        ''' <returns>
        ''' Kommentar für <paramref name="SectionName"/> oder Nothing wenn kein Kommentar existiert.
        ''' </returns>
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
        ''' <param name="Section">
        ''' Abschnitt aus dem der Wert des Eintrags gelesen werden soll.
        ''' </param>
        ''' <param name="Entry">
        ''' Eintrag dessen Wert gelesen werden soll.
        ''' </param>
        ''' <returns>
        ''' Wert des Eintrags.
        ''' </returns>
        ''' <remarks>
        ''' Erwartet, dass Abschnitt und Eintrag existieren. Andernfalls kann eine Ausnahme geworfen werden.
        ''' Bei leerem Abschnitts- und Eintragsnamen wird ein leerer String zurückgegeben.
        ''' </remarks>
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
        ''' <param name="Name">
        ''' Name des Abschnitts.
        ''' </param>
        ''' <param name="CommentLines">
        ''' Kommentarzeilen
        ''' </param>
        ''' <remarks>
        ''' Die übergebenen Zeilen sollten ohne Prefixzeichen sein.
        ''' </remarks>
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
        ''' <param name="Section">
        ''' Abschnitt in dem der Wert eines Eintrags geändert werden soll.
        ''' </param>
        ''' <param name="Entry">
        ''' Eintrag dessen Wert geändert werden soll.
        ''' </param>
        ''' <param name="Value">
        ''' Der geänderte Wert.
        ''' </param>
        ''' <remarks>
        ''' Der Abschnitt und der Eintrag müssen existieren.
        ''' </remarks>
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

#Region "interne Funktionen"

        ''' <summary>
        ''' Übenimmt die Ändeungen uns speichert die Datei
        ''' </summary>
        ''' <remarks>
        ''' - Baut den Rohinhalt neu auf.
        ''' - Speichert automatisch, wenn AutoSave=True, sonst markiert als nicht gespeichert.
        ''' - Löst anschließend FileContentChanged aus.
        ''' </remarks>
        Private Sub ChangeFileContent()
            Me.CreateFileContent()  ' Dateiinhalt neu erzeugen 
            If Me._AutoSave Then ' wenn automatisch speichern aktiv
                Me.SaveFile() ' Änderung speichern
            Else
                Me._FileSaved = False 'ansonsten Datei als ungespeichert markieren
            End If
            RaiseEvent FileContentChanged(Me, EventArgs.Empty) ' Ereignis auslösen
        End Sub

        ''' <summary>
        ''' Speichert die in <see cref="FilePath"/> angegebene Datei.
        ''' </summary>
        ''' <remarks>
        ''' Schreibt _FileContent zeilenweise auf Datenträger. IO-Ausnahmen werden nicht abgefangen.
        ''' </remarks>
        Private Sub SaveFile()
            Dim filepathandname As String = Path.Combine(Me._FilePath, Me._FileName)
            IO.File.WriteAllLines(filepathandname, Me._FileContent) ' Dateiinhalt auf Datenträger schreiben
            Me._FileSaved = True ' Datei als gespeichert markieren
        End Sub

        ''' <summary>
        ''' Fügt einen neuen Abschnitt hinzu.
        ''' </summary>
        ''' <param name="Name">
        ''' Name des neuen Abschnitts
        ''' </param>
        ''' <remarks>
        ''' Interne Hilfsfunktion ohne Ereignisauslösung oder Persistierung.
        ''' </remarks>
        Private Sub AddNewSection(Name As String)
            Me._Sections.Add(Name, New Dictionary(Of String, String)) ' Name-Wert-Paar hinzufügen
            Me._SectionsComments.Add(Name, New List(Of String)) ' Name-Kommentar-Paar hinzufügen
        End Sub

        ''' <summary>
        ''' fügt einen neuen Eintrag in einen Abschnitt ein.
        ''' </summary>
        ''' <param name="Section">
        ''' Name des Abschnitts in den der neue Eintrag eingefügt werden soll.
        ''' </param>
        ''' <param name="Name">
        ''' Name des neuen Eintrags.
        ''' </param>
        ''' <remarks>
        ''' Interne Hilfsfunktion ohne Ereignisauslösung oder Persistierung.
        ''' </remarks>
        Private Sub AddNewEntry(Section As String, Name As String)
            Me._Sections.Item(Section).Add(Name, $"")
        End Sub

        ''' <summary>
        ''' Benennt das Key-Comment-Paar eines Abschnitts um.
        ''' </summary>
        ''' <param name="oldName">
        ''' alter Name
        ''' </param>
        ''' <param name="newName">
        ''' neuer Name
        ''' </param>
        ''' <remarks>
        ''' Verschiebt den vorhandenen Kommentar auf den neuen Abschnittsnamen.
        ''' </remarks>
        Private Sub RenameSectionComment(OldName As String, newName As String)
            ' alten Kommentar speichern, Abschnitt entfernen und
            ' neuen Abschnitt mit altem Kommentar erstellen
            Dim oldcomment = Me._SectionsComments.Item(OldName)
            Dim unused1 = Me._SectionsComments.Remove(OldName)
            Me._SectionsComments.Add(newName, oldcomment)
        End Sub

        ''' <summary>
        ''' Benennt das Key-Value-Paar eines Abschnitts um.
        ''' </summary>
        ''' <param name="OldName">
        ''' alter Name
        ''' </param>
        ''' <param name="NewName">
        ''' neuer Name
        ''' </param>
        ''' <remarks>
        ''' Verschiebt alle Einträge (Name->Wert) vom alten auf den neuen Abschnittsnamen.
        ''' </remarks>
        Private Sub RenameSectionValue(OldName As String, NewName As String)
            ' alten Wert speichern, Abschnitt entfernen und
            ' neuen Abschnitt mit altem Wert erstellen
            Dim oldvalue = Me._Sections.Item(OldName)
            Dim unused = Me._Sections.Remove(OldName)
            Me._Sections.Add(NewName, oldvalue)
        End Sub

        ''' <summary>
        ''' Benennt einen Eintrag in einem Abschnitt um.
        ''' </summary>
        ''' <param name="Section">
        ''' Abschnitt in dem der Eintrag umbenannt werden soll.
        ''' </param>
        ''' <param name="OldName">
        ''' Alter Eintragsname.
        ''' </param>
        ''' <param name="NewName">
        ''' Neuer Eintragsname.
        ''' </param>
        ''' <remarks>
        ''' Übernimmt den bisherigen Wert unter dem neuen Namen.
        ''' </remarks>
        Private Sub RenameEntryvalue(Section As String, OldName As String, NewName As String)
            ' alten Wert speichern, Eintrag entfernen und
            ' neuen Eintrag mit altem Wert erstellen
            Dim oldvalue = Me._Sections.Item(Section).Item(OldName)
            Dim unused = Me._Sections.Item(Section).Remove(OldName)
            Me._Sections.Item(Section).Add(NewName, oldvalue)
        End Sub

        ''' <summary>
        ''' Erzeugt den Dateiinhalt
        ''' </summary>
        ''' <remarks>
        ''' Baut die _FileContent-Zeilen in folgender Reihenfolge:
        ''' - Dateikommentar (mit Prefix), Leerzeile,
        ''' - je Abschnitt: [Name], Abschnittskommentare (mit Prefix), Einträge "Key = Value", Leerzeile.
        ''' </remarks>
        Private Sub CreateFileContent()
            Dim filecontent As New List(Of String) ' Zeilenliste
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

        ''' <summary>
        ''' analysiert den Dateiinhalt
        ''' </summary>
        ''' <remarks>
        ''' Setzt die internen Strukturen anhand der in _FileContent enthaltenen Zeilen neu auf.
        ''' - Entfernt führende/trailing Whitespaces pro Zeile.
        ''' - Erkennt Dateikommentare (vor erstem Abschnitt), Abschnittsnamen, Abschnittskommentare und Einträge.
        ''' </remarks>
        Private Sub ParseFileContent()
            Me.InitParseVariables() ' Variablen initialisieren
            Me._CurrentSectionName = $"" ' aktueller Abschnittsname
            For Each line As String In Me._FileContent ' alle Zeilen des Dateiinhaltes durchlaufen
                line = line.Trim ' Leerzeichen am Anfang und Ende der Zeile entfernen
                Me.LineAnalyse(line) ' aktuelle Zeile analysieren
            Next
        End Sub

        ''' <summary>
        ''' Analysiert eine Zeile.
        ''' </summary>
        ''' <param name="LineContent">
        ''' Zeileninhalt
        ''' </param>
        ''' <remarks>
        ''' Reihenfolge der Prüfungen ist wichtig:
        ''' - Dateikommentar (nur vor erstem Abschnitt)
        ''' - Abschnittsname in [Klammern]
        ''' - Abschnittskommentar (mit Prefix) innerhalb eines Abschnitts
        ''' - Eintragszeile (Key=Value) innerhalb eines Abschnitts
        ''' Hinweis: In VB.NET sollten Strings mit "=" verglichen werden, nicht mit "Is".
        ''' </remarks>
        Private Sub LineAnalyse(LineContent As String)
            If Me._CurrentSectionName Is $"" And LineContent.StartsWith(Me._CommentPrefix) Then
                ' noch kein Abschnitt gefunden und Zeile startet mit Prefix -> Dateikommentar hinzufügen
                Me.AddFileCommentLine(LineContent)
            ElseIf LineContent.StartsWith($"[") And LineContent.EndsWith($"]") Then
                ' Zeile enthält eckige Klammern -> Abschnittsname hinzufügen
                Me.AddSectionNameLine(LineContent)
            ElseIf Me._CurrentSectionName IsNot $"" And LineContent.StartsWith(Me._CommentPrefix) Then
                ' aktueller Abschnitt und Zeile startet mit Prefix -> Abschnittskommentar hinzufügen
                Me.AddSectionCommentLine(LineContent)
            ElseIf Me._CurrentSectionName IsNot $"" And LineContent.Contains($"=") Then
                ' aktueller Abschnitt und Zeile enthält Gleichheitszeichen -> Eintrag hinzufügen
                Me.AddEntryLine(LineContent)
            End If
        End Sub

        ''' <summary>
        ''' fügt einen Eintrag hinzu
        ''' </summary>
        ''' <param name="LineContent">
        ''' Zeileninhalt
        ''' </param>
        ''' <remarks>
        ''' Erwartet das Format "Name = Wert". Teile links/rechts von '=' werden getrimmt.
        ''' </remarks>
        Private Sub AddEntryLine(LineContent As String)
            ' Eintagszeile in Name und Wert trennen
            Dim name As String = LineContent.Split("="c)(0).Trim
            Dim value As String = LineContent.Split("="c)(1).Trim
            Me._Sections.Item(Me._CurrentSectionName).Add(name, value)  ' Eintrag hinzufügen
        End Sub

        ''' <summary>
        ''' fügt eine Abschnittskommentarzeile hinzu
        ''' </summary>
        ''' <param name="LineContent">
        ''' Zeileninhalt
        ''' </param>
        ''' <remarks>
        ''' Entfernt das Prefixzeichen und trimmt den verbleibenden Text.
        ''' </remarks>
        Private Sub AddSectionCommentLine(LineContent As String)
            Dim line As String = LineContent.Substring(1, LineContent.Length - 1).Trim ' Prefix und eventuelle Leerzeichen am Anfang und Ende entfernen
            Me._SectionsComments.Item(Me._CurrentSectionName).Add(line) ' Kommentarzeile hinzufügen
        End Sub

        ''' <summary>
        ''' fügt einen Abschnittsname hinzu
        ''' </summary>
        ''' <param name="LineContent">
        ''' Zeileninhalt
        ''' </param>
        ''' <remarks>
        ''' Entfernt die umschließenden eckigen Klammern und erstellt die Strukturen für den neuen Abschnitt.
        ''' </remarks>
        Private Sub AddSectionNameLine(LineContent As String)
            Dim line = LineContent.Substring(1, LineContent.Length - 2).Trim ' Klammern und eventuelle Leerzeichen am Anfang und Ende entfernen
            Me._CurrentSectionName = line ' Abschnittsname merken
            ' neuen Abschnitt erstellen
            Me._Sections.Add(Me._CurrentSectionName, New Dictionary(Of String, String))
            Me._SectionsComments.Add(Me._CurrentSectionName, New List(Of String))
        End Sub

        ''' <summary>
        ''' fügt eine Dateikommentarzeile hinzu
        ''' </summary>
        ''' <param name="LineContent">
        ''' Zeileninhalt
        ''' </param>
        ''' <remarks>
        ''' Entfernt das Prefixzeichen und trimmt den verbleibenden Text. Gilt nur vor dem ersten Abschnitt.
        ''' </remarks>
        Private Sub AddFileCommentLine(LineContent As String)
            Dim line = LineContent.Substring(1, LineContent.Length - 1).Trim ' Prefix und eventuelle Leerzeichen am Anfang und Ende entfernen
            Me._FileComment.Add(line) ' Zeile in den Dateikommentar übernehmen
        End Sub

        ''' <summary>
        ''' Initialisiert die Variablen für den Parser
        ''' </summary>
        ''' <remarks>
        ''' Setzt die Strukturen auf leere Sammlungen zurück. Vor dem erneuten Aufbau via Parse.
        ''' </remarks>
        Private Sub InitParseVariables()
            Me._FileComment = New List(Of String)
            Me._Sections = New Dictionary(Of String, Dictionary(Of String, String))
            Me._SectionsComments = New Dictionary(Of String, List(Of String))
        End Sub

        ' Platzhalter für Designer-Unterstützung (keine Implementierung notwendig)
        Private Sub InitializeComponent()
        End Sub

#End Region

    End Class

End Namespace