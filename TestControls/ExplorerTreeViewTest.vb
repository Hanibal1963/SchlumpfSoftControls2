Public Class ExplorerTreeViewTest

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

    End Sub

    Private Sub ExplorerTreeView1_SelectedPathChanged(sender As Object, e As EventArgs) Handles ExplorerTreeView1.SelectedPathChanged
#If DEBUG Then
        Debug.Print($"aktuell ausgewählter Pfad:{vbCrLf}" &
                    $"{CType(sender, SchlumpfSoft.Controls.ExplorerTreeViewControl.ExplorerTreeView).SelectedPath}")
#End If
    End Sub

End Class
