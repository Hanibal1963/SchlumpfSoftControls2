' *************************************************************************************************
' DriveNode.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Repräsentiert einen Knoten für ein Laufwerk im ExplorerTreeViewControl
    ''' </summary>
    Friend Class DriveNode : Inherits System.Windows.Forms.TreeNode

        ''' <summary>
        ''' Gibt den vollständigen Pfad des Knotens zurück
        ''' </summary>
        ''' <remarks>Diese Eigenschaft gibt den Pfad des Laufwerks zurück, das im Tag gespeichert ist.</remarks>
        Public Overloads ReadOnly Property FullPath As String
            Get
                Return Me.Tag.ToString()
            End Get
        End Property

        ''' <summary>
        ''' Gibt den Laufwerkstyp des Knotens zurück
        ''' </summary>
        ''' <remarks>Diese Eigenschaft verwendet <see cref="system.IO.DriveInfo"/>, um den Typ des Laufwerks zu ermitteln.</remarks>
        Public ReadOnly Property DriveType As System.IO.DriveType
            Get
                Return New System.IO.DriveInfo(Me.Tag.ToString()).DriveType
            End Get
        End Property

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see cref="SchlumpfSoft.Controls.ExplorerTreeViewControl.DriveNode"/>. 
        ''' </summary>
        ''' <param name="Drive">Laufwerk für welches diese Instanz erstellt werden soll als <see cref="system.IO.DriveInfo"/></param>
        ''' <remarks></remarks>
        Public Sub New(Drive As System.IO.DriveInfo)

            Me.Text = $"{GetVolumeLabel(Drive)} ({GetDriveName(Drive)})" ' Setzt den Text des Knotens auf das Laufwerkslabel und den Laufwerksnamen (z. B. "Lokaler Datenträger (C:)").
            Me.Tag = Drive.Name ' Speichert den Laufwerksnamen (z. B. "C:\") im Tag des Knotens.
            Dim drivetypestring As String = GetDriveTypeString(Drive) ' Ermittelt den Laufwerkstyp als String (z. B. "Lokaler Datenträger", "CD-Laufwerk").
            Dim key As String = GetImageKey(drivetypestring) ' Ermittelt den Schlüssel für das Symbol basierend auf dem Laufwerkstyp.
            ' Setzt das Symbol des Knotens (ImageKey) und das Symbol für den ausgewählten Zustand (SelectedImageKey).
            Me.ImageKey = key
            Me.SelectedImageKey = key
            Me.Nodes.Clear()  ' Leert die Knoten, um Platz für Unterordner zu schaffen
            Dim unused = Me.Nodes.Add(New System.Windows.Forms.TreeNode("Ordner laden ...")) ' Füge einen Platzhalterknoten hinzu, der später durch die Unterordner ersetzt wird

        End Sub

        ''' <summary>
        ''' Lädt die Unterordner des Laufwerks
        ''' </summary>
        ''' <remarks>
        ''' <para>Diese Methode überprüft, ob das Laufwerk bereit ist, und lädt dann alle
        ''' Unterordner als FolderNode-Knoten. </para>
        ''' <para>Fehler wie Zugriffsverletzungen oder IO-Probleme werden abgefangen und
        ''' führen nicht zum Abbruch.</para>
        ''' </remarks>
        Public Sub LoadSubfolders()

            Try
                Dim drive As New System.IO.DriveInfo(Me.FullPath) ' Erstellt ein DriveInfo-Objekt für das aktuelle Laufwerk
                ' Prüft, ob das Laufwerk bereit ist (z. B. CD eingelegt, Netzwerk verbunden)
                If drive.IsReady Then
                    ' Durchläuft alle Unterverzeichnisse des Laufwerks
                    For Each dir As String In System.IO.Directory.GetDirectories(Me.FullPath)
                        Dim unused = Me.Nodes.Add(New FolderNode(System.IO.Path.GetFileName(dir), dir)) ' Fügt jeden gefundenen Ordner als FolderNode dem Knoten hinzu
                    Next
                End If
            Catch ex As System.UnauthorizedAccessException
                ' Zugriff verweigert – Ordner wird übersprungen, keine Fehlermeldung
            Catch ex As System.IO.IOException
                ' IO-Fehler – z.B. Laufwerk nicht verfügbar, keine Fehlermeldung
            Catch ex As System.Exception
                ' Allgemeiner Fehler – optional loggen, keine Fehlermeldung
            End Try

        End Sub

    End Class

End Namespace

