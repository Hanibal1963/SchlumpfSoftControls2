' *************************************************************************************************
' 
' ComputerNode.vb
' Copyright (c) 2025 by Andreas Sauer 
'
' Kurzbeschreibung:
' 
' Repräsentiert den Knoten für "Dieser Computer" im ExplorerTreeViewControl.
'
' *************************************************************************************************

Imports System
Imports System.Windows.Forms

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Repräsentiert den Knoten für "Dieser Computer" im ExplorerTreeViewControl.
    ''' </summary>
    ''' <remarks>
    ''' Dieser Knoten enthält spezielle Ordner und Laufwerke des Computers.
    ''' </remarks>
    Friend Class ComputerNode

        Inherits TreeNode

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see cref="SchlumpfSoft.Controls.ExplorerTreeViewControl.ComputerNode"/>. 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            SetPropertys()
            ' Leert die Knoten, um Platz für spezielle Ordner und Laufwerke zu schaffen
            Nodes.Clear()
            ' Füge Platzhalterknoten hinzu, die später durch spezielle Ordner und Laufwerke ersetzt werden
            Dim unused = Nodes.Add(New TreeNode($"Spezielle Ordner laden ..."))
            Dim unused1 = Nodes.Add(New TreeNode($"Laufwerke laden ..."))
        End Sub

        ''' <summary>
        ''' Lädt die speziellen Ordner und fügt sie dem Knoten hinzu.
        ''' </summary>
        Friend Sub LoadSpecialFolders()
            Dim unused = Nodes.Add(New SpecialFolderNode("Desktop"))
            Dim unused1 = Nodes.Add(New SpecialFolderNode("Dokumente"))
            Dim unused2 = Nodes.Add(New SpecialFolderNode("Downloads"))
            Dim unused3 = Nodes.Add(New SpecialFolderNode("Musik"))
            Dim unused4 = Nodes.Add(New SpecialFolderNode("Bilder"))
            Dim unused5 = Nodes.Add(New SpecialFolderNode("Videos"))
        End Sub

        ''' <summary>
        ''' Lädt die Laufwerke des Computers und fügt sie dem Knoten hinzu.
        ''' </summary>
        Friend Sub LoadDrives()
            For Each drive As IO.DriveInfo In IO.DriveInfo.GetDrives()
                Dim driveNode As New DriveNode(drive)
                Dim unused = Nodes.Add(driveNode)
            Next
        End Sub

        ''' <summary>
        ''' Setzt die Eigenschaften des Computer-Knotens.
        ''' </summary>
        ''' <remarks>
        ''' Diese Methode initialisiert den Knoten mit dem Computernamen und den
        ''' entsprechenden Icons.
        ''' </remarks>
        Private Sub SetPropertys()
            Dim computerName As String = Environment.MachineName
            ImageKey = $"Computer"
            SelectedImageKey = $"Computer"
            Text = $"Dieser Computer ({computerName})"
        End Sub

    End Class

End Namespace
