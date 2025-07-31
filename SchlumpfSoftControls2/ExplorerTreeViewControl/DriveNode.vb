' *************************************************************************************************
' 
' DriveNode.vb
' Copyright (c) 2025 by Andreas Sauer 
'
' Kurzbeschreibung:
' 
' Repräsentiert einen Knoten für ein Laufwerk im ExplorerTreeViewControl
'
' *************************************************************************************************

Imports System
Imports System.IO
Imports System.Windows.Forms

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Repräsentiert einen Knoten für ein Laufwerk im ExplorerTreeViewControl
    ''' </summary>
    Friend Class DriveNode : Inherits TreeNode

        ''' <summary>
        ''' Gibt den vollständigen Pfad des Knotens zurück
        ''' </summary>
        ''' <remarks>
        ''' Diese Eigenschaft gibt den Pfad des Laufwerks zurück, das im Tag gespeichert ist.
        ''' </remarks>
        Public Overloads ReadOnly Property FullPath As String
            Get
                Return Tag.ToString()
            End Get
        End Property

        ''' <summary>
        ''' Gibt den Laufwerkstyp des Knotens zurück
        ''' </summary>
        ''' <remarks>
        ''' Diese Eigenschaft verwendet die DriveInfo-Klasse, um den Typ des Laufwerks zu ermitteln.
        ''' </remarks>
        Public ReadOnly Property DriveType As DriveType
            Get
                Return New DriveInfo(Tag.ToString()).DriveType
            End Get
        End Property

        Public Sub New(Drive As DriveInfo)
            ' Setzt die Eigenschaften des Knotens basierend auf dem Laufwerk
            SetPropertys(Drive)
            ' Leert die Knoten, um Platz für Unterordner zu schaffen
            Nodes.Clear()
            ' Füge einen Platzhalterknoten hinzu, der später durch die Unterordner ersetzt wird
            Dim unused = Nodes.Add(New TreeNode($"Ordner laden ..."))
        End Sub

        ''' <summary>
        ''' Lädt die Unterordner des Laufwerks
        ''' </summary>
        Friend Sub LoadSubfolders()
            Try
                Dim drive As New DriveInfo(FullPath)
                If drive.IsReady Then
                    For Each dir As String In IO.Directory.GetDirectories(FullPath)
                        Dim unused = Nodes.Add(New FolderNode(IO.Path.GetFileName(dir), dir))
                    Next
                End If
            Catch ex As UnauthorizedAccessException
                ' Zugriff verweigert – Ordner wird übersprungen
            End Try
        End Sub

        ''' <summary>
        ''' Ermittelt den ImageKey für den Knoten basierend auf dem Laufwerkstyp
        ''' </summary>
        ''' <param name="drive"></param>
        Private Function GetImageKey(drive As DriveInfo) As String
            Dim result As String = String.Empty
            Select Case drive.DriveType
                Case DriveType.Fixed : result = If(IsSystemDrive(drive), "DriveSystem", "DriveFixed")
                Case DriveType.CDRom : result = "DriveCDROM"
                Case DriveType.Removable : result = If(IsFloppyDrive(drive), "DriveFloppy", "DriveRemovable")
                Case DriveType.Network : result = "DriveNetwork"
                Case DriveType.Ram : result = "DriveRamDisk"
                Case DriveType.NoRootDirectory : result = "kein Root-Verzeichnis"
                Case DriveType.Unknown : result = "DriveUnknown"
            End Select
            Return result
        End Function

        ''' <summary>
        ''' Ermittelt den Laufwerksnamen ohne den abschließenden Backslash
        ''' </summary>
        ''' <param name="drive">
        ''' Das Laufwerk, dessen Name ermittelt werden soll.
        ''' </param>
        Private Function GetDriveName(drive As DriveInfo) As String
            ' Der Laufwerksname endet mit einem Backslash, der entfernt werden muss
            Return drive.Name.Substring(0, drive.Name.Length - 1)
        End Function

        ''' <summary>
        ''' Ermittelt das Laufwerkslabel
        ''' </summary>
        ''' <param name="drive">
        ''' Das Laufwerk, dessen Label ermittelt werden soll.
        ''' </param>
        Private Function GetVolumeLabel(drive As DriveInfo) As String
            Dim result As String
            If drive.IsReady Then
                ' Wenn das Laufwerk bereit ist, wird das Label ermittelt.
                ' Wenn das laufwerk kein Label hat, wird der LaufwerksTyp als Label benutzt
                result = If(String.IsNullOrEmpty(drive.VolumeLabel), GetDriveTypeString(drive), drive.VolumeLabel)
            Else
                ' Wenn das Laufwerk nicht bereit ist, wird der Laufwerkstyp als Label benutzt
                result = GetDriveTypeString(drive)
            End If
            Return result
        End Function

        ''' <summary>
        ''' Ermittelt den Laufwerkstyp als String
        ''' </summary>
        ''' <param name="drive">
        ''' Das Laufwerk, dessen Typ ermittelt werden soll.
        ''' </param>
        Private Function GetDriveTypeString(drive As DriveInfo) As String
            Dim result As String = String.Empty
            Select Case drive.DriveType
                Case DriveType.Fixed : result = $"Lokaler Datenträger"
                Case DriveType.CDRom : result = $"CD-Laufwerk"
                Case DriveType.Removable : result = If(IsFloppyDrive(drive), $"Diskettenlaufwerk", $"Wechselmedium")
                Case DriveType.Network : result = $"Netzlaufwerk"
                Case DriveType.Ram : result = $"Ramlaufwerk"
                Case DriveType.NoRootDirectory : result = $"kein Root-Verzeichnis"
                Case DriveType.Unknown : result = $"Unbekanntes Laufwerk"
            End Select
            Return result
        End Function

        ''' <summary>
        ''' Ermittelt ob das Laufwerk ein Diskettenlaufwerk ist
        ''' </summary>
        ''' <param name="drive">
        ''' Das Laufwerk, welches auf den Typ FloppyDrive geprüft werden soll.
        ''' </param>
        Private Function IsFloppyDrive(drive As DriveInfo) As Boolean
            Dim result As Boolean = False
            ' Ermitteln ob das Laufwerk das Diskettenlaufwerk A ode B ist
            If drive.Name.StartsWith($"a", StringComparison.OrdinalIgnoreCase) Or
               drive.Name.StartsWith($"b", StringComparison.OrdinalIgnoreCase) Then
                result = True
            End If
            Return result
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
        ''' Setzt die Eigenschaften des Knotens basierend auf dem Laufwerk
        ''' </summary>
        ''' <param name="Drive">
        ''' Das Laufwerk, dessen Eigenschaften gesetzt werden sollen.
        ''' </param>
        Private Sub SetPropertys(Drive As DriveInfo)
            Text = $"{GetVolumeLabel(Drive)} ({GetDriveName(Drive)})"
            Tag = Drive.Name
            ImageKey = GetImageKey(Drive)
            SelectedImageKey = GetImageKey(Drive)
        End Sub

    End Class

End Namespace
