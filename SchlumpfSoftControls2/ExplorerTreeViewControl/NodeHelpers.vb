﻿' *************************************************************************************************
' NodeHelpers.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' <para>Definiert Hilfsfunktionen für die Klassen <see cref="ComputerNode"/>,
    ''' </para>
    ''' <para><see cref="SpecialFolderNode"/>, <see cref="DriveNode"/> und <see
    ''' cref="FolderNode"/></para>
    ''' </summary>
    Friend Module NodeHelpers

#Region "Definition der internen Dictionarys"

        ''' <summary>
        ''' Dictionary mit Zuordnungen von DriveType-Werten zu entsprechenden Zeichenfolgen.
        ''' </summary>
        ''' <remarks>
        ''' Dieses Dictionary wird verwendet, um den Typ eines Laufwerks in eine menschenlesbare Zeichenfolge zu übersetzen.
        ''' </remarks>
        Private ReadOnly driveTypeMappings As New Dictionary(Of DriveType, String) From {
            {DriveType.Fixed, DRIVETYPE_FIXED},
            {DriveType.CDRom, DRIVETYPE_CDROM},
            {DriveType.Removable, DRIVETYPE_REMOVABLE},
            {DriveType.Network, DRIVETYPE_NETWORK},
            {DriveType.Ram, DRIVETYPE_RAM},
            {DriveType.NoRootDirectory, DRIVETYPE_NOROOT},
            {DriveType.Unknown, DRIVETYPE_UNKNOWN}
        }

        ''' <summary>
        ''' Dictionary mit Zuordnungen von speziellen Ordnernamen zu ihren vollständigen Pfaden.
        ''' </summary>
        ''' <remarks>
        ''' Dieses Dictionary wird verwendet, um den vollständigen Pfad eines speziellen Ordners basierend auf seinem Namen zu ermitteln.
        ''' </remarks>
        Private ReadOnly folderMappings As New Dictionary(Of String, String) From {
            {FOLDER_DESKTOP, Environment.GetFolderPath(Environment.SpecialFolder.Desktop)},
            {FOLDER_DOKUMENTE, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)},
            {FOLDER_DOWNLOADS, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads")},
            {FOLDER_MUSIK, Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)},
            {FOLDER_BILDER, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)},
            {FOLDER_VIDEOS, Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)}
        }

        ''' <summary>
        ''' Dictionary mit Zuordnungen von Ordnernamen oder Laufwerkstypen zu ImageKeys
        ''' </summary>
        ''' <remarks>
        ''' Dieses Dictionary wird verwendet, um die Imagekeys für Ordner und Laufwerke zu ermitteln.
        ''' </remarks>
        Private ReadOnly imageKeyMappings As New Dictionary(Of String, String) From {
            {FOLDER_COMPUTER, ICON_COMPUTER},
            {FOLDER_DESKTOP, ICON_FOLDER_DESKTOP},
            {FOLDER_DOKUMENTE, ICON_FOLDER_DOCUMENTS},
            {FOLDER_DOWNLOADS, ICON_FOLDER_DOWNLOADS},
            {FOLDER_MUSIK, ICON_FOLDER_MUSIC},
            {FOLDER_BILDER, ICON_FOLDER_PICTURES},
            {FOLDER_VIDEOS, ICON_FOLDER_VIDEOS},
            {FOLDER_FOLDER, ICON_FOLDER_FOLDER},
            {DRIVETYPE_SYSTEM, ICON_DRIVE_SYSTEM},
            {DRIVETYPE_FIXED, ICON_DRIVE_FIXED},
            {DRIVETYPE_CDROM, ICON_DRIVE_CDROM},
            {DRIVETYPE_FLOPPY, ICON_DRIVE_FLOPPY},
            {DRIVETYPE_REMOVABLE, ICON_DRIVE_REMOVABLE},
            {DRIVETYPE_NETWORK, ICON_DRIVE_NETWORK},
            {DRIVETYPE_RAM, ICON_DRIVE_RAM},
            {DRIVETYPE_NOROOT, ICON_DRIVE_NOROOT},
            {DRIVETYPE_UNKNOWN, ICON_DRIVE_UNKNOWN}
        }

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Ermittelt den Laufwerksnamen ohne den abschließenden Backslash
        ''' </summary>
        ''' <param name="drive">Das Laufwerk, dessen Name ermittelt werden soll.</param>
        Public Function GetDriveName(drive As DriveInfo) As String

            ' Der Laufwerksname endet mit einem Backslash, der entfernt werden muss
            Return drive.Name.Substring(0, drive.Name.Length - 1)

        End Function

        ''' <summary>
        ''' Ermittelt das Laufwerkslabel
        ''' </summary>
        ''' <remarks>
        ''' <para>Wenn das Laufwerk bereit ist, wird das VolumeLabel (Laufwerksbezeichnung)
        ''' ermittelt.</para>
        ''' <para><br/>
        ''' Falls das Laufwerk kein Label besitzt (VolumeLabel ist leer oder
        ''' Nothing),</para>
        ''' <para><br/>
        ''' wird stattdessen die Beschreibung des Laufwerkstyps als Label verwendet.<br/>
        ''' </para>
        ''' </remarks>
        ''' <param name="drive">Das Laufwerk, dessen Label ermittelt werden soll.</param>
        ''' <returns>Der Volumename, der Laufwerkstyp oder leer als String. </returns>
        Public Function GetVolumeLabel(drive As DriveInfo) As String

            ' Überprüfen, ob das Laufwerk bereit ist (z. B. ob ein Medium eingelegt und lesbar ist)
            If drive.IsReady Then

                ' Wenn das Laufwerk bereit ist, wird das VolumeLabel (Laufwerksbezeichnung) ermittelt.
                ' Falls das Laufwerk kein Label besitzt (VolumeLabel ist leer oder Nothing),
                ' wird stattdessen die Beschreibung des Laufwerkstyps als Label verwendet.
                Return If(String.IsNullOrEmpty(drive.VolumeLabel), GetDriveTypeDescription(drive), drive.VolumeLabel)

            Else

                ' Wenn das Laufwerk nicht bereit ist (z. B. kein Medium eingelegt),
                ' wird die Beschreibung des Laufwerkstyps als Label verwendet.
                Return GetDriveTypeDescription(drive)

            End If

            ' Rückgabe eines leeren Strings als Fallback (sollte eigentlich nie erreicht werden)
            Return String.Empty

        End Function

        ''' <summary>
        ''' Ermittelt den String der den Laufwerkstyp darstellt.
        ''' </summary>
        ''' <param name="Drive">Das Laufwerk dessen Typ ermittelt werden soll.</param>
        ''' <returns>Die Zeichenfolge die den laufwerkstyp darstellt oder eine leere Zeichenfolge.</returns>
        Public Function GetDriveTypeString(Drive As DriveInfo) As String

            ' Überprüfen, ob das angegebene Laufwerk ein Systemlaufwerk ist.
            ' Systemlaufwerke sind in der Regel die primären Laufwerke, auf denen das Betriebssystem installiert ist.
            If IsSystemDrive(Drive) Then
                Return DRIVETYPE_SYSTEM
            End If

            ' Überprüfen, ob das angegebene Laufwerk ein Diskettenlaufwerk ist.
            ' Diskettenlaufwerke sind veraltete Speichermedien, die selten verwendet werden.
            If IsFloppyDrive(Drive) Then
                Return DRIVETYPE_FLOPPY
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
        Public Function GetSpezialFolderPath(Text As String) As String

            Return If(folderMappings.ContainsKey(Text), folderMappings(Text), String.Empty)

        End Function

        ''' <summary>
        ''' Gibt den ImageKey für den angegebenen Namen zurück.
        ''' </summary>
        ''' <param name="IconTypeString">Der Name (z. B. Ordner- oder Laufwerksname).</param>
        ''' <returns>Der zugehörige ImageKey oder eine leere Zeichenfolge. </returns>
        Public Function GetImageKey(IconTypeString As String) As String

            ' Überprüft, ob das Dictionary "imageKeyMappings" den angegebenen Schlüssel ("IconTypeString") enthält.
            ' Dies ist z. B. der Name eines Ordners oder Laufwerkstyps, für den ein passender ImageKey gesucht wird.
            If imageKeyMappings.ContainsKey(IconTypeString) Then

                ' Wenn der Schlüssel gefunden wurde, wird der zugehörige ImageKey aus dem Dictionary zurückgegeben.
                Return imageKeyMappings(IconTypeString)

            Else

                ' Falls der Schlüssel nicht existiert, wird eine leere Zeichenfolge zurückgegeben.
                ' Dies dient als Standardwert für unbekannte oder nicht zugeordnete Schlüssel.
                Return String.Empty

            End If

        End Function

#End Region

#Region "Interne Methoden"

        ''' <summary>
        ''' Ermittelt ob das Laufwerk ein Diskettenlaufwerk ist
        ''' </summary>
        ''' <param name="drive">Das Laufwerk, welches auf den Typ FloppyDrive geprüft werden soll.</param>
        Private Function IsFloppyDrive(drive As DriveInfo) As Boolean

            ' Überprüft, ob das angegebene Laufwerk ein Diskettenlaufwerk ist.
            ' Diskettenlaufwerke sind traditionell die Laufwerke "A:" und "B:" unter Windows.
            ' Die Methode prüft, ob der Name des Laufwerks mit "a" oder "b" beginnt (unabhängig von Groß-/Kleinschreibung).
            ' Dies ist eine einfache Heuristik, da Diskettenlaufwerke in modernen Systemen selten sind,
            ' aber historisch immer mit diesen Buchstaben bezeichnet wurden.
            If drive.Name.StartsWith("a", StringComparison.OrdinalIgnoreCase) Or
               drive.Name.StartsWith("b", StringComparison.OrdinalIgnoreCase) Then

                ' Wenn das Laufwerk mit "A" oder "B" beginnt, handelt es sich um ein Diskettenlaufwerk.
                Return True

            End If

            ' Wenn das Laufwerk nicht mit "A" oder "B" beginnt, ist es kein Diskettenlaufwerk.
            Return False

        End Function

        ''' <summary>
        ''' Ermittelt ob das Laufwerk das Systemlaufwerk ist
        ''' </summary>
        ''' <param name="drive">Das Laufwerk, welches auf den Typ SystemDrive geprüft werden soll.</param>
        Private Function IsSystemDrive(drive As DriveInfo) As Boolean

            ' Ermittelt das Root-Verzeichnis des Systemlaufwerks, indem der Pfad des Windows-Ordners verwendet wird.
            ' Beispiel: Wenn Windows auf "C:\Windows" installiert ist, ergibt Path.GetPathRoot(...) "C:\".
            Dim systemdrive As String = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.Windows))

            ' Vergleicht den Namen des übergebenen Laufwerks mit dem ermittelten Systemlaufwerk.
            ' String.Equals wird verwendet, um eine kulturunabhängige, nicht case-sensitive Prüfung durchzuführen.
            If String.Equals(drive.Name, systemdrive, StringComparison.OrdinalIgnoreCase) Then

                ' Wenn die Namen übereinstimmen, handelt es sich um das Systemlaufwerk.
                Return True

            End If

            ' Falls keine Übereinstimmung vorliegt, ist das Laufwerk kein Systemlaufwerk.
            Return False

        End Function

        ''' <summary>
        ''' Ermittelt den Laufwerkstyp als String.
        ''' </summary>
        ''' <param name="drive">Das Laufwerk, dessen Typ ermittelt werden soll.</param>
        ''' <returns>Eine Beschreibung des Laufwerkstyps, z. B. "Lokaler Datenträger" oder "CD-Laufwerk".</returns>
        Private Function GetDriveTypeDescription(drive As DriveInfo) As String

            Select Case drive.DriveType

                Case DriveType.Fixed : Return DRIVE_DESC_FIXED
                Case DriveType.CDRom : Return DRIVE_DESC_CDROM
                Case DriveType.Removable : Return If(IsFloppyDrive(drive), DRIVE_DESC_FLOPPY, DRIVE_DESC_REMOVABLE)
                Case DriveType.Network : Return DRIVE_DESC_NETWORK
                Case DriveType.Ram : Return DRIVE_DESC_RAM
                Case DriveType.NoRootDirectory : Return DRIVE_DESC_NOROOT
                Case DriveType.Unknown : Return DRIVE_DESC_UNKNOWN
                Case Else : Return String.Empty ' Fallback für unbekannte oder zukünftige DriveType-Werte

            End Select

        End Function

#End Region

    End Module

End Namespace