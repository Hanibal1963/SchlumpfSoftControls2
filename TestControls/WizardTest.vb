
Imports System.ComponentModel
Imports SchlumpfSoft.Controls.WizardControl

Public Class WizardTest

    Private Sub Wizard1_BeforeSwitchPages(sender As Object, e As BeforeSwitchPagesEventArgs) Handles Wizard1.BeforeSwitchPages
        MessageBox.Show($"Die Seite {Wizard1.Pages(e.NewIndex).Name} soll aufgerufen werden.")
    End Sub

    Private Sub Wizard1_AfterSwitchPages(sender As Object, e As AfterSwitchPagesEventArgs) Handles Wizard1.AfterSwitchPages
        MessageBox.Show($"Die Seite {Wizard1.Pages(e.OldIndex).Name} wurde verlassen.")
    End Sub

    Private Sub Wizard1_Help(sender As Object, e As EventArgs) Handles Wizard1.Help
        MessageBox.Show($"Hilfe wurde aufgerufen")
    End Sub

    Private Sub Wizard1_Cancel(sender As Object, e As CancelEventArgs) Handles Wizard1.Cancel
        MessageBox.Show($"Wizard wurde abgebrochen")
    End Sub

    Private Sub Wizard1_Finish(sender As Object, e As EventArgs) Handles Wizard1.Finish
        MessageBox.Show($"Wizard wurde abgeschlossen")
    End Sub

End Class
