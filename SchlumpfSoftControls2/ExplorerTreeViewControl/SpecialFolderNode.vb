' *************************************************************************************************
' 
' SpecialFolderNode.vb
' Copyright (c) 2025 by Andreas Sauer 
'
' Kurzbeschreibung:
' 
' *************************************************************************************************


Imports System
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
            SetPropertys(Text)
            ' Leert die Knoten, um Platz für Unterordner zu schaffen
            Nodes.Clear()
            ' Füge einen Platzhalterknoten hinzu, der später durch die Unterordner ersetzt wird
            Nodes.Add(New TreeNode($"Ordner laden ..."))
        End Sub

        ''' <summary>
        ''' Lädt die Unterordner des speziellen Ordners und fügt sie dem Knoten hinzu.
        ''' </summary>
        Friend Sub LoadSubfolders()
            Try
                For Each dir As String In IO.Directory.GetDirectories(FullPath)
                    Nodes.Add(New FolderNode(IO.Path.GetFileName(dir), dir))
                Next
            Catch ex As UnauthorizedAccessException
                ' Zugriff verweigert – Ordner wird übersprungen
            End Try
        End Sub

        ''' <summary>
        ''' Setzt die Eigenschaften des Knotens basierend auf dem Text.
        ''' </summary>
        ''' <param name="Text"></param>
        Private Sub SetPropertys(Text As String)
            Me.Text = Text
            Tag = GetFolderPath(Text)
            ImageKey = GetImageKey(Text)
            SelectedImageKey = GetImageKey(Text)
        End Sub

        ''' <summary>
        ''' Ermittelt den ImageKey für den Knoten basierend auf dem Text des speziellen Ordners.
        ''' </summary>
        ''' <param name="Text"></param>
        ''' <returns></returns>
        Private Function GetImageKey(Text As String) As String
            Dim result As String = String.Empty
            Select Case Text
                Case $"Desktop" : result = $"FolderDesktop"
                Case $"Dokumente" : result = $"FolderDocuments"
                Case $"Downloads" : result = $"FolderDownloads"
                Case $"Musik" : result = $"FolderMusic"
                Case $"Bilder" : result = $"FolderPictures"
                Case $"Videos" : result = $"FolderVideos"
            End Select
            Return result
        End Function

        ''' <summary>
        ''' Ermittelt den vollständigen Pfad des speziellen Ordners basierend auf dem Text.
        ''' </summary>
        ''' <param name="Text"></param>
        ''' <returns></returns>
        Private Function GetFolderPath(Text As String) As String
            Dim result As String = String.Empty
            Select Case Text
                Case $"Desktop" : result = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                Case $"Dokumente" : result = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                Case $"Downloads" : result = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\Downloads"
                Case $"Musik" : result = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)
                Case $"Bilder" : result = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
                Case $"Videos" : result = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)
            End Select
            Return result
        End Function

    End Class

End Namespace
