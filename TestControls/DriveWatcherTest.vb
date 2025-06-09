

Imports SchlumpfSoft.Controls.DriveWatcherControl

Public Class DriveWatcherTest

    Private Sub DriveWatcher1_DriveAdded(sender As Object, e As DriveAddedEventArgs) Handles DriveWatcher1.DriveAdded

        Label2.Text = $"Das Laufwerk ""{e.DriveName}"" wurde angeschlossen.{vbCrLf}" &
                      $"Es hat die Bezeichnung ""{e.VolumeLabel}"" und ist vom Typ ""{e.DriveType}"".{vbCrLf}" &
                      $"Es ist mit ""{e.DriveFormat}"" formatiert und hat eine Gesamtkapazität von {e.TotalSize / 1024 / 1024} MB."

        If e.IsReady Then
            Label2.Text &= $"{vbCrLf}Der freie Speicherplatz beträgt {e.TotalFreeSpace / 1024 / 1024} MB."
        Else
            Label2.Text &= $"{vbCrLf}Das Laufwerk ist nicht bereit."
        End If

    End Sub

    Private Sub DriveWatcher1_DriveRemoved(sender As Object, e As DriveRemovedEventArgs) Handles DriveWatcher1.DriveRemoved
        Label2.Text = $"Das Laufwerk ""{e.DriveName}"" wurde entfernt."
    End Sub

End Class
