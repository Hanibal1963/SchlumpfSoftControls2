

Public Class ExplorerTreeViewTest

    Private Sub ExplorerTreeView1_SelectedPathChanged(sender As Object, e As EventArgs) Handles ExplorerTreeView1.SelectedPathChanged

        Dim selpath As String = CType(sender, SchlumpfSoft.Controls.ExplorerTreeViewControl.ExplorerTreeView).SelectedPath
        Label1.Text = $"aktuell ausgewählter Pfad:{vbCrLf}{selpath}"

    End Sub

End Class
