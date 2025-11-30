

Public Class Form1

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        Me.InitializeComponent()
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
    End Sub

    Private Sub DriveWatcher1_DriveAdded(sender As Object, e As SchlumpfSoft.Controls.DriveWatcherControl.DriveAddedEventArgs) Handles DriveWatcher1.DriveAdded
        Me.Label_Result.Text = String.Empty
        Me.Label_Result.Text &= $"Das Laufwerk {e.DriveName} wurde hinzugefügt.{vbCrLf}"
        Me.Label_Result.Text &= $"Der Datenträger hat die Bezeichnung {e.VolumeLabel} und ist vom Typ {e.DriveType}.{vbCrLf} "
        Me.Label_Result.Text &= $"Das Format ist {e.DriveFormat} und der gesamte Speicherplatz beträgt {e.TotalSize} Bytes.{vbCrLf}"
        If e.IsReady Then
            Me.Label_Result.Text &= $"Das Laufwerk ist bereit und der freie Speicherplatz beträgt {e.TotalFreeSpace} Bytes."
        Else
            Me.Label_Result.Text &= $"Das Laufwerk ist nicht bereit"
        End If
    End Sub

    Private Sub DriveWatcher1_DriveRemoved(sender As Object, e As SchlumpfSoft.Controls.DriveWatcherControl.DriveRemovedEventArgs) Handles DriveWatcher1.DriveRemoved
        Me.Label_Result.Text = $"Das Laufwerk {e.DriveName} wurde entfernt."
    End Sub

End Class
