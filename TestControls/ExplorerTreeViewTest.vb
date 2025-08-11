

Public Class ExplorerTreeViewTest

    Private Sub ExplorerTreeView1_SelectedPathChanged(sender As Object, e As EventArgs) Handles _
        ExplorerTreeView1.SelectedPathChanged

        Dim selpath As String = CType(sender, SchlumpfSoft.Controls.ExplorerTreeViewControl.ExplorerTreeView).SelectedPath
        Label1.Text = $"aktuell ausgewählter Pfad:{vbCrLf}{selpath}"

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles _
        Button1.Click

        Dim result As DialogResult = FolderBrowserDialog1.ShowDialog
        If result = DialogResult.OK Then
            ExplorerTreeView1.ExpandPath(FolderBrowserDialog1.SelectedPath)
        End If

    End Sub

End Class
