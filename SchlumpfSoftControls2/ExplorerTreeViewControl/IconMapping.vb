' *************************************************************************************************
' 
' IconMapping.vb
' Copyright (c) 2025 by Andreas Sauer 
'
' Kurzbeschreibung:
' 
' Klasse zum verwalten der Ordner - und Laufwerkssymbole
' 
' *************************************************************************************************

Imports System.Collections.Generic

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Verwaltet die Zuordnung von Namen (z. B. Ordnernamen oder Laufwerksnamen) zu ImageKeys.
    ''' </summary>
    Friend Class IconMapping

        ''' <summary>
        ''' Dictionary mit Zuordnungen von Ordnernamen oder Laufwerkstypen zu ImageKeys
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared ReadOnly mappings As New Dictionary(Of String, String) From {
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

        ''' <summary>
        ''' Gibt den ImageKey für den angegebenen Namen zurück.
        ''' </summary>
        ''' <param name="IconTypeString">
        ''' Der Name (z. B. Ordner- oder Laufwerksname).
        ''' </param>
        ''' <returns>
        ''' Der zugehörige ImageKey oder eine leere Zeichenfolge.
        ''' </returns>
        Public Shared Function GetImageKey(IconTypeString As String) As String
            If mappings.ContainsKey(IconTypeString) Then
                Return mappings(IconTypeString)
            Else
                Return String.Empty ' Standardwert für unbekannte Schlüssel
            End If
        End Function

    End Class


End Namespace