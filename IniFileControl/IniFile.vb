' *************************************************************************************************
' IniFile.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports SchlumpfSoft.Controls.Attribute

Namespace IniFileControl

    ' weitere Infos:
    ' <Browsable> - https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.browsableattribute?view=netframework-4.7.2
    ' <Category> - https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.categoryattribute?view=netframework-4.7.2
    ' <Description> - https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.descriptionattribute?view=netframework-4.7.2

    '''' <summary>
    '''' Steuerelement zum Verwalten von INI - Dateien
    '''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Steuerelement zum Verwalten von INI - Dateien")>
    <ToolboxBitmap(GetType(IniFileControl.IniFile), "IniFile.bmp")>
    <ToolboxItem(True)>
    Public Class IniFile

        Inherits Component

#Region "Definition der Variablen"

        Private _FileName As String
        Private _FilePath As String
        Private _FileContent() As String
        Private _CommentPrefix As Char
        Private _AutoSave As Boolean = False
        Private _FileComment As List(Of String)
        Private _Sections As Dictionary(Of String, Dictionary(Of String, String))
        Private _SectionsComments As Dictionary(Of String, List(Of String))
        Private _CurrentSectionName As String
        Private _FileSaved As Boolean = False

#End Region

#Region "Definition der Ereignisse"

        ''' <summary>
        ''' Wird ausgelöst wenn sich der Dateiinhalt geändert hat.
        ''' </summary>
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
        <Browsable(False)>
        Public ReadOnly Property FileSaved As Boolean
            Get
                Return Me._FileSaved
            End Get
        End Property

        '''' <summary>
        '''' Gibt das Prefixzeichen für Kommentare zurück oder legt dieses fest.
        '''' </summary>
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

        Public Sub New()
            Me._FileName = $"neue Datei.ini"
            Me._FilePath = String.Empty
            Me._FileContent = {$""}
            Me._CommentPrefix = ";"c
            Me._FileComment = New List(Of String)
            Me._Sections = New Dictionary(Of String, Dictionary(Of String, String))
            Me._SectionsComments = New Dictionary(Of String, List(Of String))
            Me._CurrentSectionName = $""
            Me._FileSaved = True
        End Sub

#Region "öffentliche Funktionen"

        ''' <summary>
        ''' Erzeugt eine neue Datei mit Beispielinhalt
        ''' </summary>
        ''' <param name="CommentPrefix">
        ''' Prefixzeichen für Kommentare.
        ''' </param>
        ''' <remarks>
        ''' Wenn kein Prefixzeichen angegeben wird, wird Standardmäßig das Semikolon verwendet.
        ''' </remarks>
        Public Sub CreateNewFile(CommentPrefix As Char)
            Me._CommentPrefix = If(CommentPrefix = CChar(""), ";"c, CommentPrefix) ' Prefixzeichen für Kommentare festlegen (Standard wenn nicht festgelegt)
            ' Dateiinhalt erzeugen
            Dim content As String =
                $"{Me._CommentPrefix} INI - Datei Beispiel {vbCrLf}" &
                $"{Me._CommentPrefix} Diese Datei wurde von " &
                $"{My.Application.Info.AssemblyName} erzeugt{vbCrLf}" &
                $"{vbCrLf}" &
                $"[Allgemein]{vbCrLf}" &
                $"{Me._CommentPrefix} Anwendungsname und Version{vbCrLf}" &
                $"AppName = MeineApp{vbCrLf}" &
                $"Version = 1.0.0{vbCrLf}" &
                $"{vbCrLf}" &
                $"[Datenbank]{vbCrLf}" &
                $"{Me._CommentPrefix} Einstellungen zur Datenbank{vbCrLf}" &
                $"Server = localhost{vbCrLf}" &
                $"Port = 3306{vbCrLf}" &
                $"Benutzername = admin{vbCrLf}" &
                $"Passwort = geheim{vbCrLf}" &
                $"{vbCrLf}" &
                $"[Logging]{vbCrLf}" &
                $"{Me._CommentPrefix} Einstellungen zum Logging{vbCrLf}" &
                $"LogLevel = Debug{vbCrLf}" &
                $"LogDatei = logs / app.log{vbCrLf}"
            Me._FileContent = content.Split(CChar(vbCrLf))
            Me.ParseFileContent() ' Dateiinhalt analysieren
            Me._FileSaved = False ' Datei als ungespeichert markieren
            RaiseEvent FileContentChanged(Me, EventArgs.Empty) ' Ereignis auslösen
        End Sub

        ''' <summary>
        ''' Lädt die angegebene Datei
        ''' </summary>
        ''' <param name="FilePathAndName">
        ''' Name und Pfad der Datei die geladen werden soll.
        ''' </param>
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
        Public Function GetFileContent() As String()

            Return Me._FileContent

        End Function

        ''' <summary>
        ''' Gibt den Dateikommentar zurück
        ''' </summary>
        Public Function GetFileComment() As String()

            Return Me._FileComment.ToArray

        End Function

        ''' <summary>
        ''' Setzt den Dateikommentar.
        ''' </summary>
        ''' <param name="CommentLines">
        ''' Die Zeilen des Dateikommentars.
        ''' </param>
        Public Sub SetFileComment(CommentLines() As String)

            ' alten Dateikommentar löschen
            Me._FileComment.Clear()
            ' neuen Dateikommentar übenehmen
            Me._FileComment.AddRange(CommentLines)
            ' Änderungen übernehmen
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
        Public Sub SetSectionComment(Name As String, CommentLines() As String)

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
        Public Sub SetEntryValue(Section As String, Entry As String, Value As String)

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
        Private Sub AddFileCommentLine(LineContent As String)
            Dim line = LineContent.Substring(1, LineContent.Length - 1).Trim ' Prefix und eventuelle Leerzeichen am Anfang und Ende entfernen
            Me._FileComment.Add(line) ' Zeile in den Dateikommentar übernehmen
        End Sub

        ''' <summary>
        ''' Initialisiert die Variablen für den Parser
        ''' </summary>
        Private Sub InitParseVariables()
            Me._FileComment = New List(Of String)
            Me._Sections = New Dictionary(Of String, Dictionary(Of String, String))
            Me._SectionsComments = New Dictionary(Of String, List(Of String))
        End Sub

        Private Sub InitializeComponent()
        End Sub

#End Region

    End Class

End Namespace