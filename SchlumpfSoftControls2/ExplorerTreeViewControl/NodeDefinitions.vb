' *************************************************************************************************
' NodeDefinitions.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

' TODO: Code noch überarbeiten

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

            'Setze den Text des Knotens mit dem Computernamen
            Text = $"Dieser Computer ({System.Environment.MachineName})"

            ' Setze das Icon für den Knoten
            Dim key As String = GetImageKey(ICON_COMPUTER)
            ImageKey = key
            SelectedImageKey = key

            ' Leert die Knoten, um Platz für spezielle Ordner und Laufwerke zu schaffen
            Nodes.Clear()

            ' Füge Platzhalterknoten hinzu, die später durch spezielle Ordner und Laufwerke ersetzt werden
            Nodes.AddRange({
                New System.Windows.Forms.TreeNode("Spezielle Ordner laden ..."),
                New System.Windows.Forms.TreeNode("Laufwerke laden ...")
                })

        End Sub

        ''' <summary>
        ''' Lädt die speziellen Ordner und fügt sie dem Knoten hinzu.
        ''' </summary>
        Public Sub LoadSpecialFolders()

            ' Füge spezielle Ordner wie Desktop, Dokumente, Downloads usw. als Knoten hinzu
            Nodes.AddRange({
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
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
        Public Sub LoadDrives()

            ' Iteriere über alle verfügbaren Laufwerke und füge sie als Knoten hinzu
            For Each drive As System.IO.DriveInfo In System.IO.DriveInfo.GetDrives()

                Dim driveNode As New DriveNode(drive)
                Nodes.Add(driveNode)

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
                Return Tag.ToString()
            End Get
        End Property

        ''' <summary>
        ''' Gibt den Laufwerkstyp des Knotens zurück
        ''' </summary>
        ''' <remarks>Diese Eigenschaft verwendet <see cref="DriveInfo"/>, um den Typ des Laufwerks zu ermitteln.</remarks>
        Public ReadOnly Property DriveType As System.IO.DriveType
            Get
                Return New System.IO.DriveInfo(Tag.ToString()).DriveType
            End Get
        End Property

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see cref="SchlumpfSoft.Controls.ExplorerTreeViewControl.DriveNode"/>. 
        ''' </summary>
        ''' <param name="Drive">Laufwerk für welches diese Instanz erstellt werden soll als <see cref="DriveInfo"/></param>
        ''' <remarks></remarks>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
        Public Sub New(Drive As System.IO.DriveInfo)

            ' Setzt den Text des Knotens auf das Laufwerkslabel und den Laufwerksnamen (z. B. "Lokaler Datenträger (C:)").
            Text = $"{GetVolumeLabel(Drive)} ({GetDriveName(Drive)})"

            ' Speichert den Laufwerksnamen (z. B. "C:\") im Tag des Knotens.
            Tag = Drive.Name

            ' Ermittelt den Laufwerkstyp als String (z. B. "Lokaler Datenträger", "CD-Laufwerk").
            Dim drivetypestring As String = GetDriveTypeString(Drive)

            ' Ermittelt den Schlüssel für das Symbol basierend auf dem Laufwerkstyp.
            Dim key As String = GetImageKey(drivetypestring)

            ' Setzt das Symbol des Knotens (ImageKey) und das Symbol für den ausgewählten Zustand (SelectedImageKey).
            ImageKey = key
            SelectedImageKey = key

            ' Leert die Knoten, um Platz für Unterordner zu schaffen
            Nodes.Clear()

            ' Füge einen Platzhalterknoten hinzu, der später durch die Unterordner ersetzt wird
            Nodes.Add(New System.Windows.Forms.TreeNode("Ordner laden ..."))

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
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
        Public Sub LoadSubfolders()

            Try

                ' Erstellt ein DriveInfo-Objekt für das aktuelle Laufwerk
                Dim drive As New System.IO.DriveInfo(FullPath)

                ' Prüft, ob das Laufwerk bereit ist (z. B. CD eingelegt, Netzwerk verbunden)
                If drive.IsReady Then

                    ' Durchläuft alle Unterverzeichnisse des Laufwerks
                    For Each dir As String In System.IO.Directory.GetDirectories(FullPath)

                        ' Fügt jeden gefundenen Ordner als FolderNode dem Knoten hinzu
                        Nodes.Add(New FolderNode(System.IO.Path.GetFileName(dir), dir))

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
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
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
            Nodes.Add(New System.Windows.Forms.TreeNode("Ordner laden ..."))

        End Sub

        ''' <summary>
        ''' Lädt die Unterordner des Knotens und fügt sie dem Knoten hinzu.
        ''' </summary>
        ''' <remarks>
        ''' Diese Methode wird aufgerufen, wenn der Knoten erweitert wird, um die
        ''' Unterordner des Ordners zu laden.
        ''' </remarks>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
        Public Sub LoadSubfolders()

            Try

                ' Durchlaufe alle Unterverzeichnisse des aktuellen Ordners
                For Each dir As String In System.IO.Directory.GetDirectories(FullPath)

                    ' Füge für jedes Unterverzeichnis einen neuen FolderNode hinzu
                    Nodes.Add(New FolderNode(System.IO.Path.GetFileName(dir), dir))

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
                Return Tag.ToString()
            End Get
        End Property

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see
        ''' cref="SchlumpfSoft.Controls.ExplorerTreeViewControl.SpecialFolderNode"/>.
        ''' </summary>
        ''' <param name="Text">Text der den Ordner</param>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
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
            Nodes.Add(New System.Windows.Forms.TreeNode("Ordner laden ..."))

        End Sub

        ''' <summary>
        ''' Lädt die Unterordner des speziellen Ordners und fügt sie dem Knoten hinzu.
        ''' </summary>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
        Public Sub LoadSubfolders()

            ' Versucht, die Unterordner des angegebenen Spezialordners zu laden
            Try

                ' Durchläuft alle Verzeichnisse (Unterordner) im Pfad des Spezialordners
                For Each dir As String In System.IO.Directory.GetDirectories(FullPath)

                    ' Fügt für jeden gefundenen Unterordner einen neuen FolderNode zum aktuellen Knoten hinzu
                    ' IO.Path.GetFileName(dir) extrahiert den Ordnernamen aus dem vollständigen Pfad
                    ' "dir" ist der vollständige Pfad des Unterordners
                    Nodes.Add(New FolderNode(System.IO.Path.GetFileName(dir), dir))

                Next

            Catch ex As System.UnauthorizedAccessException
                ' Falls der Zugriff auf einen Ordner verweigert wird, wird die Ausnahme abgefangen
                ' und der entsprechende Ordner übersprungen, ohne die Anwendung zu unterbrechen

            End Try

        End Sub

    End Class

End Namespace
