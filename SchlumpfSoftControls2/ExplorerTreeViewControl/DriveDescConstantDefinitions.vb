' *************************************************************************************************
' DriveDescConstantDefinitions.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

' TODO: Code noch überarbeiten

Namespace ExplorerTreeViewControl

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

End Namespace
