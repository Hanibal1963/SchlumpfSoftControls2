' *************************************************************************************************
' DriveNode.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ExplorerTreeViewControl

    ' Repräsentiert einen Knoten für ein Laufwerk im ExplorerTreeViewControl
    Friend Class DriveNode : Inherits System.Windows.Forms.TreeNode

        Public Overloads ReadOnly Property FullPath As String
            Get
                Return Me.Tag.ToString()
            End Get
        End Property

        Public ReadOnly Property DriveType As System.IO.DriveType
            Get
                Return New System.IO.DriveInfo(Me.Tag.ToString()).DriveType
            End Get
        End Property

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

