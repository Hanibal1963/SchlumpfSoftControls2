Public Class Form1

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Me.TextBox1.Text = Me.FileSearch1.StartPath
        Me.TextBoxSearchPattern.Text = Me.FileSearch1.SearchPattern
        Me.CheckBoxSearchInSubFolders.Checked = Me.FileSearch1.SearchInSubfolders
        Me.ButtonStop.Enabled = False
    End Sub

    Private Sub ButtonSelectFolder_Click(sender As Object, e As EventArgs) Handles ButtonSelectFolder.Click
        Dim result As DialogResult = Me.FolderBrowserDialog1.ShowDialog()
        If result = DialogResult.OK Then
            Me.TextBox1.Text = Me.FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub ButtonStop_Click(sender As Object, e As EventArgs) Handles ButtonStop.Click
        Me.FileSearch1.StopSearch()
    End Sub

    Private Sub ButtonStart_Click(sender As Object, e As EventArgs) Handles ButtonStart.Click

        With Me.FileSearch1
            .StartPath = Me.TextBox1.Text
            .SearchPattern = Me.TextBoxSearchPattern.Text
            .SearchInSubfolders = Me.CheckBoxSearchInSubFolders.Checked
        End With
        Me.TextBox3.Clear()
        Me.ButtonStop.Enabled = True
        Me.ButtonStart.Enabled = False
        Dim unused = Me.FileSearch1.StartSearchAsync
        Me.LabelStatus.Text = $"Suche läuft..."
        Application.DoEvents()
    End Sub

    Private Sub FileSearch1_FileFound(sender As Object, datei As String) Handles FileSearch1.FileFound
        Me.TextBox3.Text &= datei & vbCrLf
    End Sub

    Private Sub FileSearch1_SearchCompleted(sender As Object, abgebrochen As Boolean) Handles FileSearch1.SearchCompleted
        Me.ButtonStart.Enabled = True
        Me.ButtonStop.Enabled = False
        If abgebrochen = False Then
            Dim unused = MsgBox("Suche beendet")
        Else
            Dim unused = MsgBox("Suche abgebrochen")
        End If
    End Sub

    Private Sub FileSearch1_ErrorOccurred(sender As Object, fehler As Exception) Handles FileSearch1.ErrorOccurred
        Me.ButtonStart.Enabled = True
        Me.ButtonStop.Enabled = False
        Dim unused = MsgBox(fehler.Message)
    End Sub

    Private Sub FileSearch1_ProgressChanged(sender As Object, e As SchlumpfSoft.Controls.FileSearchControl.FileSearchEventArgs) Handles FileSearch1.ProgressChanged
        Me.LabelStatus.Text = $"{e.Found} Dateien von {e.Total} Dateien gefunden ({e.Percent}%)"
    End Sub

End Class
