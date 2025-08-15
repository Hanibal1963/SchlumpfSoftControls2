' *************************************************************************************************
' ComputerNode.vb
' Copyright (c) 2025 by Andreas Sauer 
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
        <Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
        Public Sub New()
            ' Setzt die Eigenschaften des Knotens, wie Name und Icons
            SetProperties()
            ' Leert die Knoten, um Platz für spezielle Ordner und Laufwerke zu schaffen
            Nodes.Clear()
            ' Füge Platzhalterknoten hinzu, die später durch spezielle Ordner und Laufwerke ersetzt werden
            Nodes.Add(New TreeNode($"Spezielle Ordner laden ..."))
            Nodes.Add(New TreeNode($"Laufwerke laden ..."))
        End Sub

        ''' <summary>
        ''' Lädt die speziellen Ordner und fügt sie dem Knoten hinzu.
        ''' </summary>
        <Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
        Public Sub LoadSpecialFolders()
            ' Füge spezielle Ordner wie Desktop, Dokumente, Downloads usw. als Knoten hinzu
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
        <Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
        Public Sub LoadDrives()
            ' Iteriere über alle verfügbaren Laufwerke und füge sie als Knoten hinzu
            For Each drive As IO.DriveInfo In IO.DriveInfo.GetDrives()
                Dim driveNode As New DriveNode(drive)
                Nodes.Add(driveNode)
            Next
        End Sub

        ''' <summary>
        ''' Setzt die Eigenschaften des Computer-Knotens.
        ''' </summary>
        ''' <remarks>
        ''' Diese Methode initialisiert den Knoten mit dem Computernamen und den
        ''' entsprechenden Icons.
        ''' </remarks>
        Private Sub SetProperties()
            ' Hole den Namen des Computers
            Dim computerName As String = Environment.MachineName
            ' Setze das Icon für den Knoten
            Dim key As String = NodeHelpers.GetImageKey("Computer")
            ImageKey = key
            SelectedImageKey = key
            'Setze den Text des Knotens mit dem Computernamen
            Text = $"Dieser Computer ({computerName})"
        End Sub

    End Class

End Namespace
