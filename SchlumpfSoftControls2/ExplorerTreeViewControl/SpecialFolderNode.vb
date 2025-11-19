' *************************************************************************************************
' SpecialFolderNode.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

' TODO: Code noch überarbeiten

Imports System
'Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Stellt einen TreeNode für einen speziellen Windows-Ordner (z.B. Desktop,
    ''' Dokumente, Downloads) dar.
    ''' </summary>
    ''' <remarks>
    ''' Dieser Knoten lädt und zeigt die Unterordner des jeweiligen Spezialordners an.
    ''' </remarks>
    Friend Class SpecialFolderNode : Inherits TreeNode

        ''' <summary>
        ''' Gibt den vollständigen Pfad des Knotens zurück.
        ''' </summary>
        ''' <remarks>Diese Eigenschaft gibt den Pfad des speziellen Ordners zurück, der im Tag gespeichert ist. </remarks>
        Public Overloads ReadOnly Property FullPath As String
            Get
                Return Tag.ToString()
            End Get
        End Property

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see
        ''' cref="SchlumpfSoft.Controls.ExplorerTreeViewControl.SpecialFolderNode"/>.
        ''' </summary>
        ''' <param name="Text">Text der den Ordner</param>
        <Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
        Public Sub New(Text As String)

            ' Setzt den angezeigten Text des Knotens auf den übergebenen Namen des Spezialordners
            Me.Text = Text

            ' Speichert den vollständigen Pfad des Spezialordners im Tag-Property des Knotens
            ' Die Methode GetSpezialFolderPath(Text) ermittelt den Pfad basierend auf dem Namen des Spezialordners (z.B. "Desktop")
            Tag = GetSpezialFolderPath(Text)

            ' Ermittelt den Schlüssel für das anzuzeigende Symbol (ImageKey) anhand des Ordnernamens
            ' Die Hilfsmethode NodeHelpers.GetImageKey(Text) liefert einen passenden Schlüssel für die Bildliste
            Dim key As String = GetImageKey(Text)

            ' Setzt das Symbol des Knotens auf den ermittelten Schlüssel
            ImageKey = key
            SelectedImageKey = key

            ' Entfernt alle vorhandenen untergeordneten Knoten, um Platz für die später geladenen Unterordner zu schaffen
            Nodes.Clear()

            ' Fügt einen Platzhalterknoten hinzu, der dem Benutzer anzeigt, dass die Unterordner noch geladen werden
            ' Dieser Platzhalter wird später durch die tatsächlichen Unterordner ersetzt, sobald diese geladen wurden
            Nodes.Add(New TreeNode("Ordner laden ..."))

        End Sub

        ''' <summary>
        ''' Lädt die Unterordner des speziellen Ordners und fügt sie dem Knoten hinzu.
        ''' </summary>
        <Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
        Public Sub LoadSubfolders()

            ' Versucht, die Unterordner des angegebenen Spezialordners zu laden
            Try

                ' Durchläuft alle Verzeichnisse (Unterordner) im Pfad des Spezialordners
                For Each dir As String In IO.Directory.GetDirectories(FullPath)

                    ' Fügt für jeden gefundenen Unterordner einen neuen FolderNode zum aktuellen Knoten hinzu
                    ' IO.Path.GetFileName(dir) extrahiert den Ordnernamen aus dem vollständigen Pfad
                    ' "dir" ist der vollständige Pfad des Unterordners
                    Nodes.Add(New FolderNode(IO.Path.GetFileName(dir), dir))

                Next

            Catch ex As UnauthorizedAccessException
                ' Falls der Zugriff auf einen Ordner verweigert wird, wird die Ausnahme abgefangen
                ' und der entsprechende Ordner übersprungen, ohne die Anwendung zu unterbrechen

            End Try

        End Sub

    End Class

End Namespace
