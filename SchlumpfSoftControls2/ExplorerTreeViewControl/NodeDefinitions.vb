' *************************************************************************************************
' NodeDefinitions.vb
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
        ''' <remarks>Diese Eigenschaft verwendet <see cref="DriveInfo"/>, um den Typ des Laufwerks zu ermitteln.</remarks>
        Public ReadOnly Property DriveType As System.IO.DriveType
            Get
                Return New System.IO.DriveInfo(Me.Tag.ToString()).DriveType
            End Get
        End Property

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see cref="SchlumpfSoft.Controls.ExplorerTreeViewControl.DriveNode"/>. 
        ''' </summary>
        ''' <param name="Drive">Laufwerk für welches diese Instanz erstellt werden soll als <see cref="DriveInfo"/></param>
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

    ''' <summary>
    ''' Stellt einen Knoten für einen Ordner im ExplorerTreeViewControl dar.
    ''' </summary>
    ''' <remarks>
    ''' Dieser Knoten speichert den angezeigten Namen sowie den vollständigen Pfad des
    ''' Ordners und kann Unterordner dynamisch laden.
    ''' </remarks>
    Friend Class FolderNode : Inherits System.Windows.Forms.TreeNode

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
                Return Me.Tag.ToString()
            End Get
        End Property

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see
        ''' cref="SchlumpfSoft.Controls.ExplorerTreeViewControl.FolderNode"/>.
        ''' </summary>
        ''' <param name="Text">Text der den Name des Ordners wiederspiegelt.</param>
        ''' <param name="FullPath">Der komplette Pfad auf dem Datenträger für diesen
        ''' Ordner.</param>
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

        ''' <summary>
        ''' Lädt die Unterordner des Knotens und fügt sie dem Knoten hinzu.
        ''' </summary>
        ''' <remarks>
        ''' Diese Methode wird aufgerufen, wenn der Knoten erweitert wird, um die
        ''' Unterordner des Ordners zu laden.
        ''' </remarks>
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

    ''' <summary>
    ''' Stellt einen TreeNode für einen speziellen Windows-Ordner (z.B. Desktop,
    ''' Dokumente, Downloads) dar.
    ''' </summary>
    ''' <remarks>
    ''' Dieser Knoten lädt und zeigt die Unterordner des jeweiligen Spezialordners an.
    ''' </remarks>
    Friend Class SpecialFolderNode : Inherits System.Windows.Forms.TreeNode

        ''' <summary>
        ''' Gibt den vollständigen Pfad des Knotens zurück.
        ''' </summary>
        ''' <remarks>Diese Eigenschaft gibt den Pfad des speziellen Ordners zurück, der im Tag gespeichert ist. </remarks>
        Public Overloads ReadOnly Property FullPath As String
            Get
                Return Me.Tag.ToString()
            End Get
        End Property

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see
        ''' cref="SchlumpfSoft.Controls.ExplorerTreeViewControl.SpecialFolderNode"/>.
        ''' </summary>
        ''' <param name="Text">Text der den Ordner</param>
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

        ''' <summary>
        ''' Lädt die Unterordner des speziellen Ordners und fügt sie dem Knoten hinzu.
        ''' </summary>
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
