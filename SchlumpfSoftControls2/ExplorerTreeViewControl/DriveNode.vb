' *************************************************************************************************
' DriveNode.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.IO
Imports System.Windows.Forms

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Repräsentiert einen Knoten für ein Laufwerk im ExplorerTreeViewControl
    ''' </summary>
    Friend Class DriveNode

        Inherits TreeNode

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
            SetProperties(Drive)
            ' Leert die Knoten, um Platz für Unterordner zu schaffen
            Nodes.Clear()
            ' Füge einen Platzhalterknoten hinzu, der später durch die Unterordner ersetzt wird
            Dim unused = Nodes.Add(New TreeNode($"Ordner laden ..."))
        End Sub

        ''' <summary>
        ''' Lädt die Unterordner des Laufwerks
        ''' </summary>
        Public Sub LoadSubfolders()
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
        ''' Setzt die Eigenschaften des Knotens basierend auf dem Laufwerk
        ''' </summary>
        ''' <param name="Drive">
        ''' Das Laufwerk, dessen Eigenschaften gesetzt werden sollen.
        ''' </param>
        ''' <summary>
        ''' Setzt die Eigenschaften des Knotens basierend auf dem übergebenen Laufwerk.
        ''' </summary>
        ''' <param name="Drive">
        ''' Das Laufwerk, dessen Eigenschaften verwendet werden sollen.
        ''' </param>
        Private Sub SetProperties(Drive As DriveInfo)
            ' Setzt den Text des Knotens auf das Laufwerkslabel und den Laufwerksnamen (z. B. "Lokaler Datenträger (C:)").
            Text = $"{NodeHelpers.GetVolumeLabel(Drive)} ({NodeHelpers.GetDriveName(Drive)})"
            ' Speichert den Laufwerksnamen (z. B. "C:\") im Tag des Knotens.
            Tag = Drive.Name
            ' Ermittelt den Laufwerkstyp als String (z. B. "Lokaler Datenträger", "CD-Laufwerk").
            Dim drivetypestring As String = NodeHelpers.GetDriveTypeString(Drive)
            ' Ermittelt den Schlüssel für das Symbol basierend auf dem Laufwerkstyp.
            Dim key As String = NodeHelpers.GetImageKey(drivetypestring)
            ' Setzt das Symbol des Knotens (ImageKey) und das Symbol für den ausgewählten Zustand (SelectedImageKey).
            ImageKey = key
            SelectedImageKey = key
        End Sub

    End Class

End Namespace
