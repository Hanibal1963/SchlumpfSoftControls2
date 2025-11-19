' *************************************************************************************************
' ConstantDefinitions.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

' TODO: Code noch überarbeiten

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Definiert die Keykonstanten für Lauwerkstypen
    ''' </summary>
    Friend Module DriveTypeKeyConstantDefinitions

        Public Const DRIVETYPE_FIXED As String = "Fixed"
        Public Const DRIVETYPE_CDROM As String = "CDROM"
        Public Const DRIVETYPE_REMOVABLE As String = "Removable"
        Public Const DRIVETYPE_NETWORK As String = "Network"
        Public Const DRIVETYPE_RAM As String = "RamDisk"
        Public Const DRIVETYPE_NOROOT As String = "NoRoot"
        Public Const DRIVETYPE_UNKNOWN As String = "Unknown"
        Public Const DRIVETYPE_SYSTEM As String = "System"
        Public Const DRIVETYPE_FLOPPY As String = "Floppy"

    End Module

    ''' <summary>
    ''' Definition der Konstanten für die Anzeigenamen der Laufwerkstypen
    ''' </summary>
    Friend Module DriveDescConstantDefinitions

        Public Const DRIVE_DESC_FIXED As String = "Lokaler Datenträger"
        Public Const DRIVE_DESC_CDROM As String = "CD-Laufwerk"
        Public Const DRIVE_DESC_FLOPPY As String = "Diskettenlaufwerk"
        Public Const DRIVE_DESC_REMOVABLE As String = "Wechselmedium"
        Public Const DRIVE_DESC_NETWORK As String = "Netzlaufwerk"
        Public Const DRIVE_DESC_RAM As String = "Ramlaufwerk"
        Public Const DRIVE_DESC_NOROOT As String = "kein Root-Verzeichnis"
        Public Const DRIVE_DESC_UNKNOWN As String = "Unbekanntes Laufwerk"

    End Module

    ''' <summary>
    ''' Definiert die Keykonstanten für Ordnernamen
    ''' </summary>
    Friend Module FolderNamesKeyConstantDefinitions

        Public Const FOLDER_COMPUTER As String = "Computer"
        Public Const FOLDER_DESKTOP As String = "Desktop"
        Public Const FOLDER_DOKUMENTE As String = "Dokumente"
        Public Const FOLDER_DOWNLOADS As String = "Downloads"
        Public Const FOLDER_MUSIK As String = "Musik"
        Public Const FOLDER_BILDER As String = "Bilder"
        Public Const FOLDER_VIDEOS As String = "Videos"
        Public Const FOLDER_FOLDER As String = "Folder"

    End Module

    ''' <summary>
    ''' Definiert die Keykonstanten für Symbolbezeichnungen
    ''' </summary>
    Friend Module IconKeyConstantDefinitions

        Public Const ICON_COMPUTER As String = "Computer"
        Public Const ICON_FOLDER_DESKTOP As String = "FolderDesktop"
        Public Const ICON_FOLDER_DOCUMENTS As String = "FolderDocuments"
        Public Const ICON_FOLDER_DOWNLOADS As String = "FolderDownloads"
        Public Const ICON_FOLDER_MUSIC As String = "FolderMusic"
        Public Const ICON_FOLDER_PICTURES As String = "FolderPictures"
        Public Const ICON_FOLDER_VIDEOS As String = "FolderVideos"
        Public Const ICON_FOLDER_FOLDER As String = "Folder"
        Public Const ICON_DRIVE_SYSTEM As String = "DriveSystem"
        Public Const ICON_DRIVE_FIXED As String = "DriveFixed"
        Public Const ICON_DRIVE_CDROM As String = "DriveCDROM"
        Public Const ICON_DRIVE_FLOPPY As String = "DriveFloppy"
        Public Const ICON_DRIVE_REMOVABLE As String = "DriveRemovable"
        Public Const ICON_DRIVE_NETWORK As String = "DriveNetwork"
        Public Const ICON_DRIVE_RAM As String = "DiveRamDisk"
        Public Const ICON_DRIVE_NOROOT As String = "DriveNoRoot"
        Public Const ICON_DRIVE_UNKNOWN As String = "DriveUnknown"

    End Module

End Namespace
