' *************************************************************************************************
' DriveEventArgs.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace DriveWatcherControl

    ''' <summary>
    ''' Übergibt die Argumente für das hinzugefügte Laufwerk.
    ''' </summary>
    Public Class DriveAddedEventArgs

        ''' <summary>
        ''' Ruft den Namen eines Laufwerks ab, z.B. C:\.
        ''' </summary>
        Public Property DriveName As String

        ''' <summary>
        ''' Ruft die Volumebezeichnung eines Laufwerks ab oder legt diese fest.
        ''' </summary>
        Public Property VolumeLabel As String

        ''' <summary>
        ''' Gibt die Gesamtmenge an verfügbarem freiem Speicherplatz in Bytes an, die auf einem Laufwerk verfügbar ist.
        ''' </summary>
        Public Property AvailableFreeSpace As Long

        ''' <summary>
        ''' Ruft die Gesamtmenge an freiem Speicherplatz in Bytes ab, die auf einem Laufwerk verfügbar ist.
        ''' </summary>
        Public Property TotalFreeSpace As Long

        ''' <summary>
        ''' Ruft die Gesamtgröße des Speicherplatzes in Bytes auf einem Laufwerk ab.
        ''' </summary>
        Public Property TotalSize As Long

        ''' <summary>
        ''' Ruft den Namen des Dateisystems ab, z. B. NTFS oder FAT32.
        ''' </summary>
        Public Property DriveFormat As String

        ''' <summary>
        ''' Ruft den Laufwerkstyp ab, wie z. B. CD-ROM, Wechseldatenträger, Netzlaufwerk oder lokales Festplattenlaufwerk.
        ''' </summary>
        Public Property DriveType As System.IO.DriveType

        ''' <summary> 
        ''' Ruft einen Wert ab, der angibt, ob ein Laufwerk bereit ist.
        ''' </summary>
        Public Property IsReady As Boolean

    End Class

    ''' <summary>
    ''' Übergibt das entfernte Laufwerk.
    ''' </summary>
    Public Class DriveRemovedEventArgs

        ''' <summary>
        ''' Ruft den Namen eines Laufwerks ab, z.B. C:\.
        ''' </summary>
        Public Property DriveName As String

    End Class

End Namespace
