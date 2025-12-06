' *************************************************************************************************
' FolderNode.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ExplorerTreeViewControl

    ' Stellt einen Knoten für einen Ordner im ExplorerTreeViewControl dar.
    ' Dieser Knoten speichert den angezeigten Namen sowie den vollständigen Pfad des
    ' Ordners und kann Unterordner dynamisch laden.
    Friend Class FolderNode : Inherits System.Windows.Forms.TreeNode

        Public Overloads ReadOnly Property FullPath As String
            Get
                Return Me.Tag.ToString()
            End Get
        End Property

        Public Sub New(Text As String, FullPath As String)
            Me.Text = Text ' Setzt den angezeigten Namen des Knotens
            Me.Tag = FullPath ' Speichert den vollständigen Pfad des Ordners im Tag-Property
            ' Holt den Bildschlüssel für das Ordner-Icon und weist ihn zu
            Dim key As String = GetImageKey(ICON_FOLDER_FOLDER)
            Me.ImageKey = key
            Me.SelectedImageKey = key
            Me.Nodes.Clear()  ' Leert die Knoten, um Platz für Unterordner zu schaffen
            Dim unused = Me.Nodes.Add(New System.Windows.Forms.TreeNode("Ordner laden ...")) ' Füge einen Platzhalterknoten hinzu, der später durch die Unterordner ersetzt wird
        End Sub

        Public Sub LoadSubfolders()
            Try
                ' Durchlaufe alle Unterverzeichnisse des aktuellen Ordners
                For Each dir As String In System.IO.Directory.GetDirectories(Me.FullPath)
                    Dim unused = Me.Nodes.Add(New FolderNode(System.IO.Path.GetFileName(dir), dir)) ' Füge für jedes Unterverzeichnis einen neuen FolderNode hinzu
                Next
            Catch ex As System.UnauthorizedAccessException
                ' Zugriff verweigert – Ordner wird übersprungen
                ' Hier könnte optional Logging oder eine Benutzerbenachrichtigung erfolgen
            End Try
        End Sub

    End Class

End Namespace
