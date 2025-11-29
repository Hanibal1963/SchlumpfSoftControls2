' *************************************************************************************************
' Constants.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Definiert die Keykonstanten für Lauwerkstypen
    ''' </summary>
    Friend Module DriveTypeKeyConstants

        ''' <summary>
        ''' Schlüssel für Laufwerkstyp: Lokaler Datenträger
        ''' </summary>
        Public Const DRIVETYPE_FIXED As String = "Fixed"

        ''' <summary>
        ''' Schlüssel für Laufwerkstyp: CD/DVD/BD-Laufwerk
        ''' </summary>
        Public Const DRIVETYPE_CDROM As String = "CDROM"

        ''' <summary>
        ''' Schlüssel für Laufwerkstyp: Wechselmedium (z. B. USB-Stick)
        ''' </summary>
        Public Const DRIVETYPE_REMOVABLE As String = "Removable"

        ''' <summary>
        ''' Schlüssel für Laufwerkstyp: Netzlaufwerk
        ''' </summary>
        Public Const DRIVETYPE_NETWORK As String = "Network"

        ''' <summary>
        ''' Schlüssel für Laufwerkstyp: RAM-Disk
        ''' </summary>
        Public Const DRIVETYPE_RAM As String = "RamDisk"

        ''' <summary>
        ''' Schlüssel für Laufwerkstyp: Kein Root-Verzeichnis vorhanden
        ''' </summary>
        Public Const DRIVETYPE_NOROOT As String = "NoRoot"

        ''' <summary>
        ''' Schlüssel für Laufwerkstyp: Unbekannt
        ''' </summary>
        Public Const DRIVETYPE_UNKNOWN As String = "Unknown"

        ''' <summary>
        ''' Schlüssel für Laufwerkstyp: Systemlaufwerk
        ''' </summary>
        Public Const DRIVETYPE_SYSTEM As String = "System"

        ''' <summary>
        ''' Schlüssel für Laufwerkstyp: Diskettenlaufwerk
        ''' </summary>
        Public Const DRIVETYPE_FLOPPY As String = "Floppy"

    End Module

    ''' <summary>
    ''' Definition der Konstanten für die Anzeigenamen der Laufwerkstypen
    ''' </summary>
    Friend Module DriveDescConstants

        ''' <summary>
        ''' Anzeigename für Laufwerkstyp: Lokaler Datenträger
        ''' </summary>
        Public Const DRIVE_DESC_FIXED As String = "Lokaler Datenträger"

        ''' <summary>
        ''' Anzeigename für Laufwerkstyp: CD-Laufwerk
        ''' </summary>
        Public Const DRIVE_DESC_CDROM As String = "CD-Laufwerk"

        ''' <summary>
        ''' Anzeigename für Laufwerkstyp: Diskettenlaufwerk
        ''' </summary>
        Public Const DRIVE_DESC_FLOPPY As String = "Diskettenlaufwerk"

        ''' <summary>
        ''' Anzeigename für Laufwerkstyp: Wechselmedium
        ''' </summary>
        Public Const DRIVE_DESC_REMOVABLE As String = "Wechselmedium"

        ''' <summary>
        ''' Anzeigename für Laufwerkstyp: Netzlaufwerk
        ''' </summary>
        Public Const DRIVE_DESC_NETWORK As String = "Netzlaufwerk"

        ''' <summary>
        ''' Anzeigename für Laufwerkstyp: RAM-Laufwerk
        ''' </summary>
        Public Const DRIVE_DESC_RAM As String = "Ramlaufwerk"

        ''' <summary>
        ''' Anzeigename für Laufwerkstyp: Kein Root-Verzeichnis
        ''' </summary>
        Public Const DRIVE_DESC_NOROOT As String = "kein Root-Verzeichnis"

        ''' <summary>
        ''' Anzeigename für Laufwerkstyp: Unbekanntes Laufwerk
        ''' </summary>
        Public Const DRIVE_DESC_UNKNOWN As String = "Unbekanntes Laufwerk"

    End Module

    ''' <summary>
    ''' Definiert die Keykonstanten für Ordnernamen
    ''' </summary>
    Friend Module FolderNamesKeyConstants

        ''' <summary>
        ''' Schlüssel für speziellen Ordner: Computer
        ''' </summary>
        Public Const FOLDER_COMPUTER As String = "Computer"

        ''' <summary>
        ''' Schlüssel für speziellen Ordner: Desktop
        ''' </summary>
        Public Const FOLDER_DESKTOP As String = "Desktop"

        ''' <summary>
        ''' Schlüssel für speziellen Ordner: Dokumente
        ''' </summary>
        Public Const FOLDER_DOKUMENTE As String = "Dokumente"

        ''' <summary>
        ''' Schlüssel für speziellen Ordner: Downloads
        ''' </summary>
        Public Const FOLDER_DOWNLOADS As String = "Downloads"

        ''' <summary>
        ''' Schlüssel für speziellen Ordner: Musik
        ''' </summary>
        Public Const FOLDER_MUSIK As String = "Musik"

        ''' <summary>
        ''' Schlüssel für speziellen Ordner: Bilder
        ''' </summary>
        Public Const FOLDER_BILDER As String = "Bilder"

        ''' <summary>
        ''' Schlüssel für speziellen Ordner: Videos
        ''' </summary>
        Public Const FOLDER_VIDEOS As String = "Videos"

        ''' <summary>
        ''' Schlüssel für allgemeinen Ordner
        ''' </summary>
        Public Const FOLDER_FOLDER As String = "Folder"

    End Module

    ''' <summary>
    ''' Definiert die Keykonstanten für Symbolbezeichnungen
    ''' </summary>
    Friend Module IconKeyConstants

        ''' <summary>
        ''' Symbolschlüssel: Computer
        ''' </summary>
        Public Const ICON_COMPUTER As String = "Computer"

        ''' <summary>
        ''' Symbolschlüssel: Ordner Desktop
        ''' </summary>
        Public Const ICON_FOLDER_DESKTOP As String = "FolderDesktop"

        ''' <summary>
        ''' Symbolschlüssel: Ordner Dokumente
        ''' </summary>
        Public Const ICON_FOLDER_DOCUMENTS As String = "FolderDocuments"

        ''' <summary>
        ''' Symbolschlüssel: Ordner Downloads
        ''' </summary>
        Public Const ICON_FOLDER_DOWNLOADS As String = "FolderDownloads"

        ''' <summary>
        ''' Symbolschlüssel: Ordner Musik
        ''' </summary>
        Public Const ICON_FOLDER_MUSIC As String = "FolderMusic"

        ''' <summary>
        ''' Symbolschlüssel: Ordner Bilder
        ''' </summary>
        Public Const ICON_FOLDER_PICTURES As String = "FolderPictures"

        ''' <summary>
        ''' Symbolschlüssel: Ordner Videos
        ''' </summary>
        Public Const ICON_FOLDER_VIDEOS As String = "FolderVideos"

        ''' <summary>
        ''' Symbolschlüssel: Allgemeiner Ordner
        ''' </summary>
        Public Const ICON_FOLDER_FOLDER As String = "Folder"

        ''' <summary>
        ''' Symbolschlüssel: Systemlaufwerk
        ''' </summary>
        Public Const ICON_DRIVE_SYSTEM As String = "DriveSystem"

        ''' <summary>
        ''' Symbolschlüssel: Lokaler Datenträger
        ''' </summary>
        Public Const ICON_DRIVE_FIXED As String = "DriveFixed"

        ''' <summary>
        ''' Symbolschlüssel: CD/DVD/BD-Laufwerk
        ''' </summary>
        Public Const ICON_DRIVE_CDROM As String = "DriveCDROM"

        ''' <summary>
        ''' Symbolschlüssel: Diskettenlaufwerk
        ''' </summary>
        Public Const ICON_DRIVE_FLOPPY As String = "DriveFloppy"

        ''' <summary>
        ''' Symbolschlüssel: Wechselmedium
        ''' </summary>
        Public Const ICON_DRIVE_REMOVABLE As String = "DriveRemovable"

        ''' <summary>
        ''' Symbolschlüssel: Netzlaufwerk
        ''' </summary>
        Public Const ICON_DRIVE_NETWORK As String = "DriveNetwork"

        ''' <summary>
        ''' Symbolschlüssel: RAM-Disk
        ''' </summary>
        Public Const ICON_DRIVE_RAM As String = "DiveRamDisk"

        ''' <summary>
        ''' Symbolschlüssel: Kein Root-Verzeichnis
        ''' </summary>
        Public Const ICON_DRIVE_NOROOT As String = "DriveNoRoot"

        ''' <summary>
        ''' Symbolschlüssel: Unbekannter Laufwerkstyp
        ''' </summary>
        Public Const ICON_DRIVE_UNKNOWN As String = "DriveUnknown"

    End Module

End Namespace
