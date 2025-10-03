
Imports SchlumpfSoft.Controls
Imports SchlumpfSoft.Controls.IniFileControl

Public Class IniFileDemo

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Me.IniFile.FilePath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        If IO.File.Exists(IO.Path.Combine(Me.IniFile.FilePath, Me.IniFile.FileName)) Then
            Me.IniFile.LoadFile(IO.Path.Combine(Me.IniFile.FilePath, Me.IniFile.FileName))
        Else
            Me.IniFile.CreateNewFile()
            Me.IniFile.SaveFile(IO.Path.Combine(Me.IniFile.FilePath, Me.IniFile.FileName))
        End If
    End Sub


#Region "Ereignisse für IniFile"

    Private Sub IniFile_EntryNameExist(sender As Object, e As EventArgs) Handles IniFile.EntryNameExist
        Dim unused = MsgBox($"Der angegebene Eintrag existiert bereits.{vbCrLf}Wähle einen anderen Name.", MsgBoxStyle.Critical And MsgBoxStyle.ApplicationModal, $"Fehler!")
    End Sub

    Private Sub IniFile_ContentChanged(sender As Object, e As EventArgs) Handles IniFile.FileContentChanged
        ' Dateiinhalt anzeigen
        Me.ContentView.Lines = Me.IniFile.GetFileContent
        ' Dateikommentar anzeigen
        Me.FileCommentEdit.Comment = Me.IniFile.GetFileComment
        ' Abschnittsliste anzeigen
        Me.SectionsListEdit.ListItems = Me.IniFile.GetSectionNames
    End Sub

    Private Sub IniFile_SectionNameExist(sender As Object, e As EventArgs) Handles IniFile.SectionNameExist
        Dim unused = MsgBox($"Der eingegebene Abschnittsname existiert bereits.{vbCrLf}Wähle einen anderen Name.", MsgBoxStyle.Critical And MsgBoxStyle.ApplicationModal, $"Fehler!")
    End Sub

#End Region

#Region "Ereignisse für SectionsListEdit"

    Private Sub SectionsListEdit_ItemAdd(sender As Object, e As ListEditEventArgs) Handles SectionsListEdit.ItemAdd
        ' Abschnitt hinzufügen
        Me.IniFile.AddSection(e.NewItemName)
    End Sub

    Private Sub SectionsListEdit_ItemRemove(sender As Object, e As ListEditEventArgs) Handles SectionsListEdit.ItemRemove
        ' Abschnitt löschen
        Me.IniFile.DeleteSection(e.SelectedItem)
    End Sub

    Private Sub SectionsListEdit_ItemRename(sender As Object, e As ListEditEventArgs) Handles SectionsListEdit.ItemRename
        ' Abschnitt umbenennen
        Me.IniFile.RenameSection(e.SelectedItem, e.NewItemName)
    End Sub

    Private Sub SectionsListEdit_SelectedItemChanged(sender As Object, e As ListEditEventArgs) Handles SectionsListEdit.SelectedItemChanged
        ' Wenn Null oder nur Leerzeichen -> Werte der abhängigen Controls löschen ansonsten neue Werte laden
        If String.IsNullOrEmpty(e.SelectedItem) Then
            ' Werte für SectionCommentEdit löschen
            Me.SectionCommentEdit.SectionName = $""
            Me.SectionCommentEdit.Comment = {$""}
            ' Werte für EntryListEdit löschen
            Me.EntryListEdit.ListItems = {$""}
        Else
            ' Werte für den Abschnitt in SectionCommentEdit laden
            Me.SectionCommentEdit.SectionName = e.SelectedItem
            Me.SectionCommentEdit.Comment = Me.IniFile.GetSectionComment(e.SelectedItem)
            ' Werte für den Abschnitt in EntryListEdit laden
            Me.EntryListEdit.ListItems = Me.IniFile.GetEntryNames(e.SelectedItem)
            Me.EntryListEdit.SelectedSection = e.SelectedItem
        End If
    End Sub

#End Region

#Region "Ereignisse für EntryListEdit"

    Private Sub EntryListEdit_ItemAdd(sender As Object, e As ListEditEventArgs) Handles EntryListEdit.ItemAdd
        ' Eintrag hinzufügen
        Me.IniFile.AddEntry(e.SelectedSection, e.NewItemName)
    End Sub

    Private Sub EntryListEdit_ItemRemove(sender As Object, e As ListEditEventArgs) Handles EntryListEdit.ItemRemove
        ' Eintrag löschen
        Me.IniFile.DeleteEntry(e.SelectedSection, e.SelectedItem)
    End Sub

    Private Sub EntryListEdit_ItemRename(sender As Object, e As ListEditEventArgs) Handles EntryListEdit.ItemRename
        ' Eintrag umbenennen
        Me.IniFile.RenameEntry(e.SelectedSection, e.SelectedItem, e.NewItemName)
    End Sub

    Private Sub EntryListEdit_SelectedItemChanged(sender As Object, e As ListEditEventArgs) Handles EntryListEdit.SelectedItemChanged
        ' Eintragwert anzeigen
        With Me.EntryValueEdit
            .SelectedSection = e.SelectedSection
            .SelectedEntry = e.SelectedItem
            .Value = Me.IniFile.GetEntryValue(e.SelectedSection, e.SelectedItem)
        End With
    End Sub

#End Region

    Private Sub FileCommentEdit_CommentChanged(sender As Object, e As CommentEditEventArgs) Handles FileCommentEdit.CommentChanged
        ' Dateikommentar speichern
        Me.IniFile.SetFileComment(e.Comment)
    End Sub

    Private Sub SectionCommentEdit_CommentChanged(sender As Object, e As CommentEditEventArgs) Handles SectionCommentEdit.CommentChanged
        ' Abschnittskommentar ändern
        Me.IniFile.SetSectionComment(e.Section, e.Comment)
    End Sub

    Private Sub EntryValueEdit_ValueChanged(sender As Object, e As EntryValueEditEventArgs) Handles EntryValueEdit.ValueChanged
        ' Eintragwert ändern
        Me.IniFile.SetEntryValue(e.SelectedSection, e.SelectedEntry, e.NewValue)
    End Sub

    Private Sub CheckBoxAutoSave_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBoxAutoSave.CheckStateChanged
        Me.IniFile.AutoSave = Me.CheckBoxAutoSave.Checked
    End Sub

End Class
