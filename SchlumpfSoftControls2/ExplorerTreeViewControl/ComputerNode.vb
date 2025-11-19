' *************************************************************************************************
' ComputerNode.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

' TODO: Code noch überarbeiten

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

            'Setze den Text des Knotens mit dem Computernamen
            Text = $"Dieser Computer ({Environment.MachineName})"

            ' Setze das Icon für den Knoten
            Dim key As String = GetImageKey(ICON_COMPUTER)
            ImageKey = key
            SelectedImageKey = key

            ' Leert die Knoten, um Platz für spezielle Ordner und Laufwerke zu schaffen
            Nodes.Clear()

            ' Füge Platzhalterknoten hinzu, die später durch spezielle Ordner und Laufwerke ersetzt werden
            Nodes.AddRange({
                New TreeNode("Spezielle Ordner laden ..."),
                New TreeNode("Laufwerke laden ...")
                })

        End Sub

        ''' <summary>
        ''' Lädt die speziellen Ordner und fügt sie dem Knoten hinzu.
        ''' </summary>
        Public Sub LoadSpecialFolders()

            ' Füge spezielle Ordner wie Desktop, Dokumente, Downloads usw. als Knoten hinzu
            Nodes.AddRange({
                  New SpecialFolderNode("Desktop"),
                  New SpecialFolderNode("Dokumente"),
                  New SpecialFolderNode("Downloads"),
                  New SpecialFolderNode("Musik"),
                  New SpecialFolderNode("Bilder"),
                  New SpecialFolderNode("Videos")})
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

    End Class

End Namespace
