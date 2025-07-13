' *************************************************************************************************
' 
' ComputerNode.vb
' Copyright (c) 2025 by Andreas Sauer 
'
' Kurzbeschreibung:
' 
' *************************************************************************************************


Imports System
Imports System.Windows.Forms

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Repräsentiert den Knoten für "Dieser Computer" im ExplorerTreeViewControl.
    ''' Dieser Knoten enthält spezielle Ordner und Laufwerke des Computers.
    ''' </summary>
    Friend Class ComputerNode : Inherits TreeNode

        Public Sub New()
            SetPropertys()
            ' Leert die Knoten, um Platz für spezielle Ordner und Laufwerke zu schaffen
            Nodes.Clear()
            ' Füge Platzhalterknoten hinzu, die später durch spezielle Ordner und Laufwerke ersetzt werden
            Nodes.Add(New TreeNode($"Spezielle Ordner laden ..."))
            Nodes.Add(New TreeNode($"Laufwerke laden ..."))
        End Sub

        ''' <summary>
        ''' Lädt die speziellen Ordner und fügt sie dem Knoten hinzu.
        ''' </summary>
        Friend Sub LoadSpecialFolders()
            Nodes.Add(New SpecialFolderNode("Desktop"))
            Nodes.Add(New SpecialFolderNode("Dokumente"))
            Nodes.Add(New SpecialFolderNode("Downloads"))
            Nodes.Add(New SpecialFolderNode("Musik"))
            Nodes.Add(New SpecialFolderNode("Bilder"))
            Nodes.Add(New SpecialFolderNode("Videos"))
        End Sub

        ''' <summary>
        ''' Lädt die Laufwerke des Computers und fügt sie dem Knoten hinzu.
        ''' </summary>
        Friend Sub LoadDrives()
            For Each drive As IO.DriveInfo In IO.DriveInfo.GetDrives()
                Dim driveNode As New DriveNode(drive)
                Nodes.Add(driveNode)
            Next
        End Sub

        ''' <summary>
        ''' Setzt die Eigenschaften des Computer-Knotens.
        ''' </summary>
        ''' <remarks>
        ''' Diese Methode initialisiert den Knoten mit dem Computernamen und den entsprechenden Icons.
        ''' </remarks>
        Private Sub SetPropertys()
            Dim computerName As String = Environment.MachineName
            ImageKey = $"Computer"
            SelectedImageKey = $"Computer"
            Text = $"Dieser Computer ({computerName})"
        End Sub

    End Class

End Namespace
