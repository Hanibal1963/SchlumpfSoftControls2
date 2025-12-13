Imports SchlumpfSoft.Controls.WizardControl

Public Class Form1

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

    End Sub

    Private Sub Wizard1_BeforeSwitchPages(sender As Object, e As SchlumpfSoft.Controls.WizardControl.BeforeSwitchPagesEventArgs) Handles Wizard1.BeforeSwitchPages
        Dim unused = MessageBox.Show(Me, $"neuer Index: {e.NewIndex} / alter Index: {e.OldIndex}", $"Vor dem Seitenwechsel",
                                     MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Wizard1_AfterSwitchPages(sender As Object, e As SchlumpfSoft.Controls.WizardControl.AfterSwitchPagesEventArgs) Handles Wizard1.AfterSwitchPages
        Dim unused = MessageBox.Show(Me, $"neuer Index: {e.NewIndex} / alter Index: {e.OldIndex}", $"Nach dem Seitenwechsel",
                                     MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Wizard1_Cancel(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Wizard1.Cancel
        Dim unused = MessageBox.Show(Me, $"Aktion abgebrochen", $"Der Benutzer hat die Aktion beendet.",
                                     MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Wizard1_Finish(sender As Object, e As EventArgs) Handles Wizard1.Finish
        Dim unused = MessageBox.Show(Me, $"Sie haben den Assistente abgeschlossen.", $"Aktion abgeschlossen",
                                     MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Wizard1_Help(sender As Object, e As EventArgs) Handles Wizard1.Help
        Dim pageindex As Integer = CType(sender, Wizard).Pages.IndexOf(CType(sender, Wizard).SelectedPage) + 1
        Dim unused = MessageBox.Show(Me, $"Die Hilfe für die Seite {pageindex} des Assistenten.", $"Hilfe",
                                     MessageBoxButtons.OK, MessageBoxIcon.Question)
    End Sub

End Class
