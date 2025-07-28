

Public Class ExplorerTreeViewTest

    Private Sub ExplorerTreeView1_SelectedPathChanged(sender As Object, e As EventArgs) Handles ExplorerTreeView1.SelectedPathChanged

        Dim selpath As String = CType(sender, SchlumpfSoft.Controls.ExplorerTreeViewControl.ExplorerTreeView).SelectedPath

#If DEBUG Then
        Debug.Print($"ausgewählter Pfad: {selpath}")
#End If

    End Sub

End Class
