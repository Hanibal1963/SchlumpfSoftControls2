Imports System.IO

Public Class Form1
    Private Sub ExplorerTreeView1_SelectedPathChanged(sender As Object, e As SchlumpfSoft.Controls.ExplorerTreeViewControl.SelectedPathChangedEventArgs) Handles ExplorerTreeView1.SelectedPathChanged
        Dim selpath As String = e.SelectedPath ' aktuell ausgewählten Pfad anzeigen
        Dim text As String = $"{Me.TextBox.Text}{vbCrLf}Aktuell ausgewählter Pfad: {selpath}"
        Me.TextBox.Text = text
        Me.ListView.Items.Clear() ' Liste leeren
        Try
            For Each dir As String In Directory.GetDirectories(selpath)  ' Unterverzeichnisse auslesen
                Dim unused1 = Me.ListView.Items.Add(Me.GetName(dir))
            Next
            For Each file As String In Directory.GetFiles(selpath) ' Dateien auslesen
                Dim unused2 = Me.ListView.Items.Add(Me.GetName(file))
            Next
        Catch ex As IOException
        End Try
    End Sub

    Private Sub ButtonSearchPath_Click(sender As Object, e As EventArgs) Handles ButtonSearchPath.Click
        Dim result As DialogResult = Me.FolderBrowserDialog1.ShowDialog(Me)
        If result = DialogResult.OK Then
            Me.LabelPath.Text = Me.FolderBrowserDialog1.SelectedPath
            Me.ExplorerTreeView1.ExpandPath(Me.FolderBrowserDialog1.SelectedPath)
        End If
    End Sub

    Private Function GetName(FileOrDirectory As String) As String
        Return Path.GetFileName(FileOrDirectory)
    End Function

End Class
