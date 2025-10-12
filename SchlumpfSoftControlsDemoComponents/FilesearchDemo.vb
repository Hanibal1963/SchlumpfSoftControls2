
Imports SchlumpfSoft.Controls

Public Class FileSearchDemo

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Me.TextBox1.Text = Me.FileSearch1.StartPath
        Me.TextBox2.Text = Me.FileSearch1.SearchPattern
        Me.CheckBox1.Checked = Me.FileSearch1.SearchInSubfolders
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As DialogResult = Me.FolderBrowserDialog1.ShowDialog()
        If result = DialogResult.OK Then
            Me.TextBox1.Text = Me.FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.FileSearch1.StopSearch()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.TextBox3.Clear()
        With Me.FileSearch1
            .StartPath = Me.TextBox1.Text
            .SearchPattern = Me.TextBox2.Text
            .SearchInSubfolders = Me.CheckBox1.Checked
        End With
        Dim unused = Me.FileSearch1.StartSearchAsync()
    End Sub

    Private Sub FileSearch1_FileFound(sender As Object, datei As String) Handles FileSearch1.FileFound
        Me.TextBox3.Text &= datei & vbCrLf
    End Sub

    Private Sub FileSearch1_ErrorOccurred(sender As Object, fehler As Exception) Handles FileSearch1.ErrorOccurred
        Dim unused = MsgBox(fehler.Message)
    End Sub

    Private Sub FileSearch1_ProgressChanged(sender As Object, e As FileSearchControl.FileSearchEventArgs) Handles FileSearch1.ProgressChanged
        Me.Label3.Text = $"{e.Found} Dateien von {e.Total} Dateien gefunden ({e.Percent}%)"
    End Sub

    Private Sub FileSearch1_SearchCompleted(sender As Object, abgebrochen As Boolean) Handles FileSearch1.SearchCompleted
        If abgebrochen = False Then
            Dim unused = MsgBox("Suche beendet")
        Else
            Dim unused = MsgBox("Suche abgebrochen")
        End If
    End Sub

End Class
