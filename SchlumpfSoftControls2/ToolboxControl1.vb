Imports System.Windows.Forms

<ProvideToolboxControl("SchlumpfSoftControls2.ToolboxControl1", False)>
Public Class ToolboxControl1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MessageBox.Show(String.Format(System.Globalization.CultureInfo.CurrentUICulture, "We are inside {0}.button1_Click()", Me.ToString()))
    End Sub

End Class
