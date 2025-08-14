' *************************************************************************************************
' SpecialFolderNode.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Stellt einen TreeNode für einen speziellen Windows-Ordner (z.B. Desktop, Dokumente, Downloads) dar.
    ''' Dieser Knoten lädt und zeigt die Unterordner des jeweiligen Spezialordners an.
    ''' </summary>
    Friend Class SpecialFolderNode : Inherits TreeNode

        ''' <summary>
        ''' Gibt den vollständigen Pfad des Knotens zurück.
        ''' </summary>
        ''' <remarks>
        ''' Diese Eigenschaft gibt den Pfad des speziellen Ordners zurück, der im Tag gespeichert ist.
        ''' </remarks>
        Public Overloads ReadOnly Property FullPath As String
            Get
                Return Tag.ToString()
            End Get
        End Property

        Public Sub New(Text As String)
            ' Setzt die Eigenschaften des Knotens basierend auf dem Text
            SetProperties(Text)
            ' Leert die Knoten, um Platz für Unterordner zu schaffen
            Nodes.Clear()
            ' Füge einen Platzhalterknoten hinzu, der später durch die Unterordner ersetzt wird
            Dim unused = Nodes.Add(New TreeNode($"Ordner laden ..."))
        End Sub

        ''' <summary>
        ''' Lädt die Unterordner des speziellen Ordners und fügt sie dem Knoten hinzu.
        ''' </summary>
        Public Sub LoadSubfolders()
            Try
                For Each dir As String In IO.Directory.GetDirectories(FullPath)
                    Dim unused = Nodes.Add(New FolderNode(IO.Path.GetFileName(dir), dir))
                Next
            Catch ex As UnauthorizedAccessException
                ' Zugriff verweigert – Ordner wird übersprungen
            End Try
        End Sub

        ''' <summary>
        ''' Setzt die Eigenschaften des Knotens basierend auf dem Text.
        ''' </summary>
        ''' <param name="Text"></param>
        Private Sub SetProperties(Text As String)
            Me.Text = Text
            Tag = GetFolderPath(Text)
            Dim key As String = NodeHelpers.GetImageKey(Text)
            ImageKey = key
            SelectedImageKey = key
        End Sub

    End Class

End Namespace
