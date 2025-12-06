' *************************************************************************************************
' ComputerNode.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Repräsentiert den Knoten für "Dieser Computer" im ExplorerTreeViewControl.
    ''' </summary>
    ''' <remarks>
    ''' Dieser Knoten enthält spezielle Ordner und Laufwerke des Computers.
    ''' </remarks>
    Friend Class ComputerNode : Inherits System.Windows.Forms.TreeNode

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see cref="SchlumpfSoft.Controls.ExplorerTreeViewControl.ComputerNode"/>. 
        ''' </summary>
        ''' <remarks></remarks>        
        Public Sub New()

            Me.Text = $"Dieser Computer ({System.Environment.MachineName})" 'Setze den Text des Knotens mit dem Computernamen
            Dim key As String = GetImageKey(ICON_COMPUTER) ' Setze das Icon für den Knoten
            Me.ImageKey = key
            Me.SelectedImageKey = key
            Me.Nodes.Clear() ' Leert die Knoten, um Platz für spezielle Ordner und Laufwerke zu schaffen
            ' Füge Platzhalterknoten hinzu, die später durch spezielle Ordner und Laufwerke ersetzt werden
            Me.Nodes.AddRange({
                New System.Windows.Forms.TreeNode("Spezielle Ordner laden ..."),
                New System.Windows.Forms.TreeNode("Laufwerke laden ...")
                })

        End Sub

        ''' <summary>
        ''' Lädt die speziellen Ordner und fügt sie dem Knoten hinzu.
        ''' </summary>
        Public Sub LoadSpecialFolders()

            ' Füge spezielle Ordner wie Desktop, Dokumente, Downloads usw. als Knoten hinzu
            Me.Nodes.AddRange({
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
        Public Sub LoadDrives()

            ' Iteriere über alle verfügbaren Laufwerke und füge sie als Knoten hinzu
            For Each drive As System.IO.DriveInfo In System.IO.DriveInfo.GetDrives()
                Dim driveNode As New DriveNode(drive)
                Dim unused = Me.Nodes.Add(driveNode)
            Next

        End Sub

    End Class

End Namespace

