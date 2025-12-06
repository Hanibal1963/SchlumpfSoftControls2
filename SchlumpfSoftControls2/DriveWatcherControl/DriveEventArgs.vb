' *************************************************************************************************
' DriveEventArgs.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace DriveWatcherControl

    ''' <summary>
    ''' Stellt Ereignisdaten für ein hinzugefügtes Laufwerk bereit.
    ''' </summary>
    ''' <remarks>
    ''' Diese Argumente werden typischerweise mit einem Ereignis wie <c>DriveWatcher.DriveAdded</c> übergeben und enthalten grundlegende Metadaten des erkannten Laufwerks (z. B. Name, Volumebezeichnung, Dateisystem, Typ und Speichergrößen in Bytes).
    ''' </remarks>
    ''' <example>
    ''' <code language="vb"><![CDATA[Imports System
    ''' Imports SchlumpfSoftControls2.DriveWatcherControl
    '''  
    ''' Module ExampleDriveAdded
    '''     ' Ein einfaches Beispiel, wie ein DriveWatcher das Ereignis auslösen könnte
    '''     Public Event DriveAdded As EventHandler(Of DriveAddedEventArgs)
    '''  
    '''     Sub Main()
    '''         ' Abonnieren des Ereignisses
    '''         AddHandler DriveAdded, AddressOf OnDriveAdded
    '''  
    '''         ' Simulieren eines hinzugefügten Laufwerks (z. B. USB-Stick)
    '''         Dim args = New DriveAddedEventArgs() With {
    '''             .DriveName = "E:\",
    '''             .VolumeLabel = "USB_STICK",
    '''             .AvailableFreeSpace = 10_000_000_000,
    '''             .TotalFreeSpace = 12_000_000_000,
    '''             .TotalSize = 64_000_000_000,
    '''             .DriveFormat = "NTFS",
    '''             .DriveType = IO.DriveType.Removable,
    '''             .IsReady = True
    '''         }
    '''  
    '''         ' Auslösen des Ereignisses
    '''         RaiseEvent DriveAdded(Nothing, args)
    '''     End Sub
    '''  
    '''     Private Sub OnDriveAdded(sender As Object, e As DriveAddedEventArgs)
    '''         Console.WriteLine($"Neues Laufwerk: {e.DriveName} ({e.VolumeLabel})")
    '''         Console.WriteLine($"Format: {e.DriveFormat}, Typ: {e.DriveType}, Bereit: {e.IsReady}")
    '''         Console.WriteLine($"Freier Speicher (verfügbar): {e.AvailableFreeSpace} Bytes")
    '''         Console.WriteLine($"Freier Speicher (gesamt): {e.TotalFreeSpace} Bytes")
    '''         Console.WriteLine($"Gesamtgröße: {e.TotalSize} Bytes")
    '''     End Sub
    ''' End Module]]></code>
    ''' </example>
    Public Class DriveAddedEventArgs

        ''' <summary>
        ''' Ruft den Namen eines Laufwerks ab oder legt ihn fest, z. B. <c>C:\</c>.
        ''' </summary>
        ''' <remarks>
        ''' Der Wert entspricht typischerweise <see cref="System.IO.DriveInfo.Name"/>.
        ''' </remarks>
        Public Property DriveName As String

        ''' <summary>
        ''' Ruft die Volumebezeichnung eines Laufwerks ab oder legt diese fest.
        ''' </summary>
        ''' <remarks>
        ''' Entspricht der vom Betriebssystem gemeldeten Bezeichnung (z. B. „System“, „USB_STICK“).
        ''' </remarks>
        Public Property VolumeLabel As String

        ''' <summary>
        ''' Ruft die Gesamtmenge an verfügbarem freiem Speicherplatz in Bytes ab oder legt sie fest,
        ''' die für den aktuellen Benutzer auf dem Laufwerk verfügbar ist.
        ''' </summary>
        ''' <remarks>
        ''' Dieser Wert kann von <see cref="TotalFreeSpace"/> abweichen, wenn Kontingente gelten.
        ''' </remarks>
        Public Property AvailableFreeSpace As Long

        ''' <summary>
        ''' Ruft die Gesamtmenge an freiem Speicherplatz in Bytes ab oder legt sie fest,
        ''' die auf dem Laufwerk verfügbar ist (unabhängig von Benutzerkontingenten).
        ''' </summary>
        Public Property TotalFreeSpace As Long

        ''' <summary>
        ''' Ruft die Gesamtgröße des Speicherplatzes in Bytes auf einem Laufwerk ab oder legt sie fest.
        ''' </summary>
        Public Property TotalSize As Long

        ''' <summary>
        ''' Ruft den Namen des Dateisystems ab oder legt ihn fest, z. B. <c>NTFS</c> oder <c>FAT32</c>.
        ''' </summary>
        Public Property DriveFormat As String

        ''' <summary>
        ''' Ruft den Laufwerkstyp ab oder legt ihn fest, z. B. <see cref="System.IO.DriveType.CDRom"/>,
        ''' <see cref="System.IO.DriveType.Removable"/>, <see cref="System.IO.DriveType.Network"/> oder
        ''' <see cref="System.IO.DriveType.Fixed"/>.
        ''' </summary>
        Public Property DriveType As System.IO.DriveType

        ''' <summary> 
        ''' Ruft einen Wert ab oder legt diesen fest, der angibt, ob ein Laufwerk bereit ist (Medien vorhanden, lesbar).
        ''' </summary>
        Public Property IsReady As Boolean

    End Class

    ''' <summary>
    ''' Stellt Ereignisdaten für ein entferntes Laufwerk bereit.
    ''' </summary>
    ''' <remarks>
    ''' Diese Argumente werden typischerweise mit einem Ereignis wie <c>DriveWatcher.DriveRemoved</c> übergeben und enthalten mindestens den Namen des entfernten Laufwerks.
    ''' </remarks>
    ''' <example>
    ''' <code language="vb"><![CDATA[Imports System
    ''' Imports SchlumpfSoftControls2.DriveWatcherControl
    '''  
    ''' Module ExampleDriveRemoved
    '''     Public Event DriveRemoved As EventHandler(Of DriveRemovedEventArgs)
    '''  
    '''     Sub Main()
    '''         AddHandler DriveRemoved, AddressOf OnDriveRemoved
    '''  
    '''         Dim args = New DriveRemovedEventArgs() With {
    '''             .DriveName = "E:\"
    '''         }
    '''  
    '''         ' Auslösen des Ereignisses, wenn das Laufwerk entfernt wurde
    '''         RaiseEvent DriveRemoved(Nothing, args)
    '''     End Sub
    '''  
    '''     Private Sub OnDriveRemoved(sender As Object, e As DriveRemovedEventArgs)
    '''         Console.WriteLine($"Laufwerk entfernt: {e.DriveName}")
    '''     End Sub
    ''' End Module]]></code>
    ''' </example>
    Public Class DriveRemovedEventArgs

        ''' <summary>
        ''' Ruft den Namen des Laufwerks ab oder legt ihn fest, z. B. <c>C:\</c>.
        ''' </summary>
        Public Property DriveName As String

    End Class

End Namespace