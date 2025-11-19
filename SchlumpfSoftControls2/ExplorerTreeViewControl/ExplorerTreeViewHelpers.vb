' *************************************************************************************************
' ExplorerTreeViewHelpers.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

' TODO: Code noch überarbeiten

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Definiert Hilfmethoden für <seealso cref="ExplorerTreeView"/>.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Module ExplorerTreeViewHelpers

        ''' <summary>
        ''' Zerlegt einen vollständigen Verzeichnispfad in seine einzelnen Segmente.
        ''' </summary>
        ''' <remarks>
        ''' <para>Beispiel: </para>
        ''' <para>"C:\Benutzer\Name\Dokumente" wird zu {"C:", "Benutzer", "Name",
        ''' "Dokumente"}. </para>
        ''' <para>Dies ist notwendig, um im TreeView schrittweise durch die Knotenstruktur
        ''' zu navigieren.</para>
        ''' </remarks>
        ''' <param name="Path">Der vollständige Verzeichnispfad, der zerlegt werden
        ''' soll.</param>
        ''' <returns>
        ''' Eine Liste der einzelnen Pfadsegmente, beginnend mit dem Wurzelverzeichnis.
        ''' </returns>
        Public Function GetPathSegments(Path As String) As System.Collections.Generic.List(Of String)

            ' Erzeugt ein DirectoryInfo-Objekt für den angegebenen Pfad
            Dim dirInfo As New System.IO.DirectoryInfo(Path)

            ' Liste, in der die einzelnen Segmente gesammelt werden
            Dim result As New System.Collections.Generic.List(Of String)

            ' Solange das DirectoryInfo-Objekt gültig ist und einen Namen hat
            While dirInfo IsNot Nothing AndAlso Not String.IsNullOrEmpty(dirInfo.Name)

                ' Fügt den aktuellen Verzeichnisnamen am Anfang der Liste ein
                ' (so bleibt die Reihenfolge von Wurzel zu Blatt erhalten)
                result.Insert(0, dirInfo.Name)

                ' Geht ein Verzeichnis nach oben (Parent)
                dirInfo = dirInfo.Parent

            End While

            ' Gibt die Liste der Segmente zurück
            Return result

        End Function

        ''' <summary>
        ''' Sucht rekursiv im gesamten TreeView nach einem Knoten mit dem angegebenen
        ''' Verzeichnispfad.
        ''' </summary>
        ''' <param name="Nodes">Die NodesCollection, in der gesucht werden soll (z.B.
        ''' TV.Nodes)</param>
        ''' <param name="SearchPath">Der zu suchende Pfad</param>
        ''' <returns>
        ''' Der gefundene TreeNode oder Nothing
        ''' </returns>
        Public Function FindNodeByPath(Nodes As System.Windows.Forms.TreeNodeCollection, SearchPath As String) As System.Windows.Forms.TreeNode

            ' Durchlaufe alle Knoten in der aktuellen Knotenliste
            For Each node As System.Windows.Forms.TreeNode In Nodes

                ' Vergleiche den Pfad des aktuellen Knotens mit dem gesuchten Pfad (Groß-/Kleinschreibung wird ignoriert)
                If String.Equals(GetDirectoryPath(node), SearchPath, System.StringComparison.OrdinalIgnoreCase) Then

                    ' Passender Knoten gefunden, diesen zurückgeben
                    Return node

                End If

                ' Wenn nicht gefunden, rekursiv in den Unterknoten weitersuchen
                Dim found As System.Windows.Forms.TreeNode = FindNodeByPath(node.Nodes, SearchPath)
                If found IsNot Nothing Then

                    ' Passenden Knoten in den Unterknoten gefunden, diesen zurückgeben
                    Return found

                End If

            Next

            ' Kein passender Knoten gefunden, Nothing zurückgeben
            Return Nothing

        End Function

        ''' <summary>
        ''' Ermittelt den Verzeichnispfad basierend auf dem ausgewählten Knoten im TreeView.
        ''' </summary>
        ''' <param name="node"></param>
        Public Function GetDirectoryPath(node As System.Windows.Forms.TreeNode) As String

            Select Case True

                Case TypeOf node Is ComputerNode
                    ' "Dieser Computer" hat keinen Pfad
                    Return String.Empty

                Case TypeOf node Is DriveNode
                    ' Gibt den Laufwerksbuchstaben zurück
                    Return CType(node, DriveNode).FullPath

                Case TypeOf node Is SpecialFolderNode
                    ' Gibt den Pfad für Spezialordner zurück
                    Return CType(node, SpecialFolderNode).FullPath

                Case TypeOf node Is FolderNode
                    ' Gibt den Pfad für alle anderen Ordner zurück
                    Return CType(node, FolderNode).FullPath

                Case Else
                    '
                    Return String.Empty

            End Select

        End Function

    End Module

End Namespace
