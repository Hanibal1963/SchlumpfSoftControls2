' *************************************************************************************************
' DriveNode.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.IO
Imports System.Windows.Forms

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Repräsentiert einen Knoten für ein Laufwerk im ExplorerTreeViewControl
    ''' </summary>
    Friend Class DriveNode

        Inherits TreeNode

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
        Public ReadOnly Property DriveType As DriveType
            Get
                Return New DriveInfo(Tag.ToString()).DriveType
            End Get
        End Property

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see cref="SchlumpfSoft.Controls.ExplorerTreeViewControl.DriveNode"/>. 
        ''' </summary>
        ''' <param name="Drive">Laufwerk für welches diese Instanz erstellt werden soll als <see cref="DriveInfo"/></param>
        ''' <remarks></remarks>
        <Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
        Public Sub New(Drive As DriveInfo)

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
            Nodes.Add(New TreeNode("Ordner laden ..."))

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
        <Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Der Ausdruckswert wird niemals verwendet.", Justification:="<Ausstehend>")>
        Public Sub LoadSubfolders()

            Try

                ' Erstellt ein DriveInfo-Objekt für das aktuelle Laufwerk
                Dim drive As New DriveInfo(FullPath)

                ' Prüft, ob das Laufwerk bereit ist (z. B. CD eingelegt, Netzwerk verbunden)
                If drive.IsReady Then

                    ' Durchläuft alle Unterverzeichnisse des Laufwerks
                    For Each dir As String In IO.Directory.GetDirectories(FullPath)

                        ' Fügt jeden gefundenen Ordner als FolderNode dem Knoten hinzu
                        Nodes.Add(New FolderNode(IO.Path.GetFileName(dir), dir))

                    Next

                End If

            Catch ex As UnauthorizedAccessException
                ' Zugriff verweigert – Ordner wird übersprungen, keine Fehlermeldung

            Catch ex As IOException
                ' IO-Fehler – z.B. Laufwerk nicht verfügbar, keine Fehlermeldung

            Catch ex As Exception
                ' Allgemeiner Fehler – optional loggen, keine Fehlermeldung

            End Try

        End Sub

    End Class

End Namespace
