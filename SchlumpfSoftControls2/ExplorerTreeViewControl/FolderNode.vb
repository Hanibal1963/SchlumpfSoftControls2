' *************************************************************************************************
' FolderNode.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.Windows.Forms

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Stellt einen Knoten für einen Ordner im ExplorerTreeViewControl dar.
    ''' </summary>
    ''' <remarks>
    ''' Dieser Knoten speichert den angezeigten Namen sowie den vollständigen Pfad des
    ''' Ordners und kann Unterordner dynamisch laden.
    ''' </remarks>
    Friend Class FolderNode : Inherits TreeNode

        ''' <summary>
        ''' <para>Gibt den vollständigen Pfad des Knotens zurück. Diese Eigenschaft gibt den
        ''' Pfad des Ordners zurück, der im Tag gespeichert ist.</para>
        ''' <para>Sie wird verwendet, um auf den Ordner zuzugreifen, wenn Unterordner
        ''' geladen oder andere Operationen durchgeführt werden müssen.</para>
        ''' </summary>
        ''' <remarks>
        ''' <para>Diese Eigenschaft ist eine Überladung der Standard-TreeNode-Eigenschaft
        ''' FullPath. </para>
        ''' <para>Sie gibt den Pfad des Ordners zurück, der im Tag des Knotens gespeichert
        ''' ist.</para>
        ''' </remarks>
        Public Overloads ReadOnly Property FullPath As String
            Get
                Return Tag.ToString()
            End Get
        End Property

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see
        ''' cref="SchlumpfSoft.Controls.ExplorerTreeViewControl.FolderNode"/>.
        ''' </summary>
        ''' <param name="Text">Text der den Name des Ordners wiederspiegelt.</param>
        ''' <param name="FullPath">Der komplette Pfad auf dem Datenträger für diesen
        ''' Ordner.</param>
        <Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
        Public Sub New(Text As String, FullPath As String)

            ' Setzt den angezeigten Namen des Knotens
            Me.Text = Text

            ' Speichert den vollständigen Pfad des Ordners im Tag-Property
            Tag = FullPath

            ' Holt den Bildschlüssel für das Ordner-Icon und weist ihn zu
            Dim key As String = GetImageKey(ICON_FOLDER_FOLDER)
            ImageKey = key
            SelectedImageKey = key

            ' Leert die Knoten, um Platz für Unterordner zu schaffen
            Nodes.Clear()

            ' Füge einen Platzhalterknoten hinzu, der später durch die Unterordner ersetzt wird
            Nodes.Add(New TreeNode("Ordner laden ..."))

        End Sub

        ''' <summary>
        ''' Lädt die Unterordner des Knotens und fügt sie dem Knoten hinzu.
        ''' </summary>
        ''' <remarks>
        ''' Diese Methode wird aufgerufen, wenn der Knoten erweitert wird, um die
        ''' Unterordner des Ordners zu laden.
        ''' </remarks>
        <Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
        Public Sub LoadSubfolders()

            Try

                ' Durchlaufe alle Unterverzeichnisse des aktuellen Ordners
                For Each dir As String In IO.Directory.GetDirectories(FullPath)

                    ' Füge für jedes Unterverzeichnis einen neuen FolderNode hinzu
                    Nodes.Add(New FolderNode(IO.Path.GetFileName(dir), dir))

                Next

            Catch ex As UnauthorizedAccessException
                ' Zugriff verweigert – Ordner wird übersprungen
                ' Hier könnte optional Logging oder eine Benutzerbenachrichtigung erfolgen

            End Try

        End Sub

    End Class

End Namespace
