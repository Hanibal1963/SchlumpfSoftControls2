' *************************************************************************************************
' FolderNode.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.Windows.Forms

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Stellt einen Knoten für einen Ordner im ExplorerTreeViewControl dar.
    ''' Dieser Knoten speichert den angezeigten Namen sowie den vollständigen Pfad des Ordners
    ''' und kann Unterordner dynamisch laden.
    ''' </summary>
    Friend Class FolderNode : Inherits TreeNode

        ''' <summary>
        ''' Gibt den vollständigen Pfad des Knotens zurück.
        ''' Diese Eigenschaft gibt den Pfad des Ordners zurück, der im Tag gespeichert ist.
        ''' Sie wird verwendet, um auf den Ordner zuzugreifen, wenn Unterordner geladen oder andere Operationen durchgeführt werden müssen.
        ''' </summary>
        ''' <remarks>
        ''' Diese Eigenschaft ist eine Überladung der Standard-TreeNode-Eigenschaft FullPath.
        ''' Sie gibt den Pfad des Ordners zurück, der im Tag des Knotens gespeichert ist.
        ''' </remarks>
        ''' <returns></returns>
        Public Overloads ReadOnly Property FullPath As String
            Get
                Return Tag.ToString()
            End Get
        End Property

        Public Sub New(Text As String, FullPath As String)
            ' Setzt die Eigenschaften des Knotens basierend auf dem Text und dem vollständigen Pfad
            SetProperties(Text, FullPath)
            ' Leert die Knoten, um Platz für Unterordner zu schaffen
            Nodes.Clear()
            ' Füge einen Platzhalterknoten hinzu, der später durch die Unterordner ersetzt wird
            Dim unused = Nodes.Add(New TreeNode($"Ordner laden ..."))
        End Sub

        ''' <summary>
        ''' Lädt die Unterordner des Knotens und fügt sie dem Knoten hinzu.
        ''' Diese Methode wird aufgerufen, wenn der Knoten erweitert wird, um die Unterordner des Ordners zu laden.
        ''' </summary>
        ''' <remarks>
        ''' Diese Methode versucht, alle Unterordner des Ordners zu laden, der im Tag des Knotens gespeichert ist.
        ''' Bei Zugriff verweigert (UnauthorizedAccessException) wird der Ordner übersprungen.
        ''' </remarks
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
        ''' Setzt die Eigenschaften des Knotens basierend auf dem Text und des Pfades.
        ''' </summary>
        ''' <param name="Text">
        ''' Name des Orners, der im Knoten angezeigt wird.
        ''' </param>
        ''' <param name="FullPath">
        ''' Vollständiger Pfad des Ordners, der im Tag des Knotens gespeichert wird.
        ''' Dies ermöglicht den Zugriff auf den Ordnerpfad, wenn der Knoten ausgewählt wird oder Unterordner geladen werden müssen.
        ''' </param>
        Private Sub SetProperties(Text As String, FullPath As String)
            Me.Text = Text
            Tag = FullPath
            Dim key As String = NodeHelpers.GetImageKey("Folder")
            ImageKey = key
            SelectedImageKey = key
        End Sub

    End Class

End Namespace
