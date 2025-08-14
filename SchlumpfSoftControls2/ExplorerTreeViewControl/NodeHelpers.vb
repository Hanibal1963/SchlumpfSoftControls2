' *************************************************************************************************
' NodeHelpers.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Definiert Hilfsfunktionen für die DriveNode-Klasse
    ''' </summary>
    ''' <remarks></remarks>
    Friend Module NodeHelpers

        ''' <summary>
        ''' Dictionary mit Zuordnungen von DriveType-Werten zu entsprechenden Zeichenfolgen.
        ''' </summary>
        ''' <remarks>
        ''' Dieses Dictionary wird verwendet, um den Typ eines Laufwerks in eine menschenlesbare Zeichenfolge zu übersetzen.
        ''' </remarks>
        Private ReadOnly driveTypeMappings As New Dictionary(Of DriveType, String) From {
            {DriveType.Fixed, "Fixed"},
            {DriveType.CDRom, "CDROM"},
            {DriveType.Removable, "Removable"},
            {DriveType.Network, "Network"},
            {DriveType.Ram, "RamDisk"},
            {DriveType.NoRootDirectory, "NoRoot"},
            {DriveType.Unknown, "Unknown"}
        }

        ''' <summary>
        ''' Dictionary mit Zuordnungen von speziellen Ordnernamen zu ihren vollständigen Pfaden.
        ''' </summary>
        ''' <remarks>
        ''' Dieses Dictionary wird verwendet, um den vollständigen Pfad eines speziellen Ordners basierend auf seinem Namen zu ermitteln.
        ''' </remarks>
        Private ReadOnly folderMappings As New Dictionary(Of String, String) From {
            {"Desktop", Environment.GetFolderPath(Environment.SpecialFolder.Desktop)},
            {"Dokumente", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)},
            {"Downloads", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads")},
            {"Musik", Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)},
            {"Bilder", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)},
            {"Videos", Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)}
        }

        ''' <summary>
        ''' Dictionary mit Zuordnungen von Ordnernamen oder Laufwerkstypen zu ImageKeys
        ''' </summary>
        ''' <remarks>
        ''' Dieses Dictionary wird verwendet, um die Imagekeys für Ordner und Laufwerke zu ermitteln.
        ''' </remarks>
        Private ReadOnly imageKeyMappings As New Dictionary(Of String, String) From {
            {"Computer", "Computer"},
            {"Desktop", "FolderDesktop"},
            {"Dokumente", "FolderDocuments"},
            {"Downloads", "FolderDownloads"},
            {"Musik", "FolderMusic"},
            {"Bilder", "FolderPictures"},
            {"Videos", "FolderVideos"},
            {"Folder", "Folder"},
            {"System", "DriveSystem"},
            {"Fixed", "DriveFixed"},
            {"CDROM", "DriveCDROM"},
            {"Floppy", "DriveFloppy"},
            {"Removable", "DriveRemovable"},
            {"Network", "DriveNetwork"},
            {"RamDisk", "DiveRamDisk"},
            {"NoRoot", "DriveNoRoot"},
            {"Unknown", "DriveUnknown"}
        }

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Ermittelt den Laufwerksnamen ohne den abschließenden Backslash
        ''' </summary>
        ''' <param name="drive">
        ''' Das Laufwerk, dessen Name ermittelt werden soll.
        ''' </param>
        Public Function GetDriveName(drive As DriveInfo) As String
            ' Der Laufwerksname endet mit einem Backslash, der entfernt werden muss
            Return drive.Name.Substring(0, drive.Name.Length - 1)
        End Function

        ''' <summary>
        ''' Ermittelt das Laufwerkslabel
        ''' </summary>
        ''' <param name="drive">
        ''' Das Laufwerk, dessen Label ermittelt werden soll.
        ''' </param>
        Public Function GetVolumeLabel(drive As DriveInfo) As String
            Dim result As String
            If drive.IsReady Then
                ' Wenn das Laufwerk bereit ist, wird das Label ermittelt.
                ' Wenn das laufwerk kein Label hat, wird der LaufwerksTyp als Label benutzt
                If String.IsNullOrEmpty(drive.VolumeLabel) Then
                    result = GetDriveTypeDescription(drive)
                Else
                    result = drive.VolumeLabel
                End If
            Else
                ' Wenn das Laufwerk nicht bereit ist, wird der Laufwerkstyp als Label benutzt
                result = GetDriveTypeDescription(drive)
            End If
            Return result
        End Function

        ''' <summary>
        ''' Ermittelt den String der den Laufwerkstyp darstellt.
        ''' </summary>
        ''' <param name="Drive">Das Laufwerk dessen Typ ermittelt werden soll.</param>
        ''' <returns>
        ''' Die Zeichenfolge die den laufwerkstyp darstellt oder eine leere Zeichenfolge.
        ''' </returns>
        Public Function GetDriveTypeString(Drive As DriveInfo) As String
            ' Überprüfen, ob das angegebene Laufwerk ein Systemlaufwerk ist.
            ' Systemlaufwerke sind in der Regel die primären Laufwerke, auf denen das Betriebssystem installiert ist.
            If IsSystemDrive(Drive) Then
                Return "System"
            End If
            ' Überprüfen, ob das angegebene Laufwerk ein Diskettenlaufwerk ist.
            ' Diskettenlaufwerke sind veraltete Speichermedien, die selten verwendet werden.
            If IsFloppyDrive(Drive) Then
                Return "Floppy"
            End If
            ' Versuchen, den Laufwerkstyp (DriveType) aus der vordefinierten Mapping-Tabelle zu ermitteln.
            ' Die Mapping-Tabelle ordnet DriveType-Werte (z. B. Fixed, CDRom) entsprechenden Zeichenfolgen zu.
            If driveTypeMappings.ContainsKey(Drive.DriveType) Then
                Return driveTypeMappings(Drive.DriveType)
            End If
            ' Wenn der Laufwerkstyp nicht erkannt wird, wird eine leere Zeichenfolge zurückgegeben.
            Return String.Empty
        End Function

        ''' <summary>
        ''' Ermittelt den vollständigen Pfad des speziellen Ordners basierend auf dem angezeigtem Text.
        ''' </summary>
        ''' <param name="Text"></param>
        ''' <returns></returns>
        Public Function GetFolderPath(Text As String) As String
            Return If(folderMappings.ContainsKey(Text), folderMappings(Text), String.Empty)
        End Function

        ''' <summary>
        ''' Gibt den ImageKey für den angegebenen Namen zurück.
        ''' </summary>
        ''' <param name="IconTypeString">Der Name (z. B. Ordner- oder
        ''' Laufwerksname).</param>
        ''' <returns>
        ''' Der zugehörige ImageKey oder eine leere Zeichenfolge.
        ''' </returns>
        Public Function GetImageKey(IconTypeString As String) As String
            If imageKeyMappings.ContainsKey(IconTypeString) Then
                Return imageKeyMappings(IconTypeString)
            Else
                Return String.Empty ' Standardwert für unbekannte Schlüssel
            End If
        End Function

#End Region

#Region "Interne Methoden"

        ''' <summary>
        ''' Ermittelt ob das Laufwerk ein Diskettenlaufwerk ist
        ''' </summary>
        ''' <param name="drive">
        ''' Das Laufwerk, welches auf den Typ FloppyDrive geprüft werden soll.
        ''' </param>
        Private Function IsFloppyDrive(drive As DriveInfo) As Boolean
            ' Ermitteln ob das Laufwerk das Diskettenlaufwerk A ode B ist
            If drive.Name.StartsWith($"a", StringComparison.OrdinalIgnoreCase) Or
               drive.Name.StartsWith($"b", StringComparison.OrdinalIgnoreCase) Then
                Return True
            End If
            Return False
        End Function

        ''' <summary>
        ''' Ermittelt ob das Laufwerk das Systemlaufwerk ist
        ''' </summary>
        ''' <param name="drive">
        ''' Das Laufwerk, welches auf den Typ SystemDrive geprüft werden soll.
        ''' </param>
        Private Function IsSystemDrive(drive As DriveInfo) As Boolean
            Dim result As Boolean
            Dim systemdrive As String = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.Windows))
            If String.Equals(drive.Name, systemdrive, StringComparison.OrdinalIgnoreCase) Then
                result = True
            End If
            Return result
        End Function

        ''' <summary>
        ''' Ermittelt den Laufwerkstyp als String.
        ''' </summary>
        ''' <param name="drive">
        ''' Das Laufwerk, dessen Typ ermittelt werden soll.
        ''' </param>
        ''' <returns>
        ''' Eine Beschreibung des Laufwerkstyps, z. B. "Lokaler Datenträger" oder "CD-Laufwerk".
        ''' </returns>
        Private Function GetDriveTypeDescription(drive As DriveInfo) As String
            Select Case drive.DriveType
                Case DriveType.Fixed
                    Return "Lokaler Datenträger"
                Case DriveType.CDRom
                    Return "CD-Laufwerk"
                Case DriveType.Removable
                    Return If(IsFloppyDrive(drive), "Diskettenlaufwerk", "Wechselmedium")
                Case DriveType.Network
                    Return "Netzlaufwerk"
                Case DriveType.Ram
                    Return "Ramlaufwerk"
                Case DriveType.NoRootDirectory
                    Return "kein Root-Verzeichnis"
                Case DriveType.Unknown
                    Return "Unbekanntes Laufwerk"
            End Select
            Return String.Empty
        End Function

#End Region

    End Module

End Namespace