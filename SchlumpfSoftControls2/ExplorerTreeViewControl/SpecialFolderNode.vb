' *************************************************************************************************
' SpecialFolderNode.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ExplorerTreeViewControl

    ' Stellt einen TreeNode für einen speziellen Windows-Ordner (z.B. Desktop,
    ' Dokumente, Downloads) dar.
    ' Dieser Knoten lädt und zeigt die Unterordner des jeweiligen Spezialordners an.
    Friend Class SpecialFolderNode : Inherits System.Windows.Forms.TreeNode

        Public Overloads ReadOnly Property FullPath As String
            Get
                Return Me.Tag.ToString()
            End Get
        End Property

        Public Sub New(Text As String)
            Me.Text = Text ' Setzt den angezeigten Text des Knotens auf den übergebenen Namen des Spezialordners
            ' Speichert den vollständigen Pfad des Spezialordners im Tag-Property des Knotens
            ' Die Methode GetSpezialFolderPath(Text) ermittelt den Pfad basierend auf dem Namen des Spezialordners (z.B. "Desktop")
            Me.Tag = GetSpezialFolderPath(Text)
            ' Ermittelt den Schlüssel für das anzuzeigende Symbol (ImageKey) anhand des Ordnernamens
            ' Die Hilfsmethode NodeHelpers.GetImageKey(Text) liefert einen passenden Schlüssel für die Bildliste
            Dim key As String = GetImageKey(Text)
            ' Setzt das Symbol des Knotens auf den ermittelten Schlüssel
            Me.ImageKey = key
            Me.SelectedImageKey = key
            Me.Nodes.Clear() ' Entfernt alle vorhandenen untergeordneten Knoten, um Platz für die später geladenen Unterordner zu schaffen
            ' Fügt einen Platzhalterknoten hinzu, der dem Benutzer anzeigt, dass die Unterordner noch geladen werden
            ' Dieser Platzhalter wird später durch die tatsächlichen Unterordner ersetzt, sobald diese geladen wurden
            Dim unused = Me.Nodes.Add(New System.Windows.Forms.TreeNode("Ordner laden ..."))
        End Sub

        Public Sub LoadSubfolders()
            ' Versucht, die Unterordner des angegebenen Spezialordners zu laden
            Try
                ' Durchläuft alle Verzeichnisse (Unterordner) im Pfad des Spezialordners
                For Each dir As String In System.IO.Directory.GetDirectories(Me.FullPath)
                    ' Fügt für jeden gefundenen Unterordner einen neuen FolderNode zum aktuellen Knoten hinzu
                    ' IO.Path.GetFileName(dir) extrahiert den Ordnernamen aus dem vollständigen Pfad
                    ' "dir" ist der vollständige Pfad des Unterordners
                    Dim unused = Me.Nodes.Add(New FolderNode(System.IO.Path.GetFileName(dir), dir))
                Next
            Catch ex As System.UnauthorizedAccessException
                ' Falls der Zugriff auf einen Ordner verweigert wird, wird die Ausnahme abgefangen
                ' und der entsprechende Ordner übersprungen, ohne die Anwendung zu unterbrechen
            End Try
        End Sub

    End Class

End Namespace
