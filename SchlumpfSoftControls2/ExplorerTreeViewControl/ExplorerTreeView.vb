' *************************************************************************************************
' 
' ExplorerTreeView.vb
' Copyright (c) 2025 by Andreas Sauer 
'
' Kurzbeschreibung:
' 
' Stellt ein Steuerelement zur Anzeige und Navigation der Verzeichnisstruktur des Computers bereit.
'
' *************************************************************************************************


#Region "Importe"

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports SchlumpfSoft.Controls.DriveWatcherControl

#End Region

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Stellt ein Steuerelement zur Anzeige und Navigation der Verzeichnisstruktur des
    ''' Computers bereit.
    ''' </summary>
    ''' <remarks>
    ''' Ermöglicht die Auswahl von Laufwerken, speziellen Ordnern und Unterordnern in
    ''' einer hierarchischen Baumstruktur.
    ''' </remarks>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Stellt ein Steuerelement zur Anzeige und Navigation der Verzeichnisstruktur des Computers bereit.")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(ExplorerTreeViewControl.ExplorerTreeView), "ExplorerTreeViewControl.ExplorerTreeView.bmp")>
    Public Class ExplorerTreeView

        Inherits UserControl

#Region "Interne Variablen"

        ''' <summary>
        ''' Zwischenspeicher für den Pfad des aktuell ausgewählten Knotens.
        ''' </summary>
        Private _SelectedPath As String = String.Empty

        ''' <summary>
        ''' Liste der zu überwachenden Verzeichnisse.
        ''' </summary>
        Private _FileSystemWatchers As New Dictionary(Of String, FileSystemWatcher)

#End Region

#Region "Definition der öffentlichen Ereignisse"

        ''' <summary>
        ''' Ereignis, das ausgelöst wird, wenn sich der ausgewählte Pfad geändert hat.
        ''' </summary>
        ''' <remarks>
        ''' <para>Dieses Ereignis wird verwendet, um andere Steuerelemente oder Logik zu
        ''' benachrichtigen, wenn der Benutzer einen anderen Knoten im TreeView auswählt.
        ''' </para>
        ''' <para>Es ermöglicht eine reaktive Programmierung, bei der andere Teile der
        ''' Anwendung auf Änderungen im ausgewählten Pfad reagieren können.</para>
        ''' </remarks>
        <Description("wird ausgelöst wenn sich der ausgewähte Pfad geändert hat.")>
        <Browsable(True)>
        Public Event SelectedPathChanged(sender As Object, e As EventArgs)

#End Region

#Region "Öffentliche Eigenschaften"

        ''' <summary>
        ''' Gibt den vollständigen Pfad des ausgewählten Knotens zurück.
        ''' </summary>
        ''' <remarks>
        ''' <para>Diese Eigenschaft wird aktualisiert, wenn ein Knoten im TreeView
        ''' ausgewählt wird. </para>
        ''' <para>Sie ermöglicht den Zugriff auf den Pfad des aktuell ausgewählten Knotens,
        ''' was für weitere Operationen wie das Öffnen oder Bearbeiten von Dateien und
        ''' Ordnern nützlich ist.</para>
        ''' </remarks>
        <Description("Gibt den vollständigen Pfad des ausgewählten Knotens zurück.")>
        <Browsable(False)>
        Public ReadOnly Property SelectedPath As String
            Get
                Return _SelectedPath
            End Get
        End Property

        ''' <summary>
        ''' Gibt die Farbe der Linien zwischen den Knoten zurück oder legt diese fest.
        ''' </summary>
        <Category("Behavior")>
        <Description("Gibt die Farbe der Linien zwischen den Knoten zurück oder legt diese fest.")>
        <Browsable(True)>
        Public Property LineColor As Color
            Get
                Return TV.LineColor
            End Get
            Set(value As Color)
                TV.LineColor = value
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob Linien zwischen den Knoten angezeigt werden.
        ''' </summary>
        <Category("Behavior")>
        <Description("Gibt an, ob Linien zwischen den Knoten angezeigt werden.")>
        <Browsable(True)>
        Public Property ShowLines As Boolean
            Get
                Return TV.ShowLines
            End Get
            Set(value As Boolean)
                TV.ShowLines = value
            End Set
        End Property

        ''' <summary>
        ''' Legt fest ob die Plus- und Minuszeichen zum Anzeigen von Unterknoten angezeigt werden.
        ''' </summary>
        <Category("Behavior")>
        <Description("Legt fest ob die Plus- und Minuszeichen zum Anzeigen von Unterknoten angezeigt werden.")>
        <Browsable(True)>
        Public Property ShowPlusMinus As Boolean
            Get
                Return TV.ShowPlusMinus
            End Get
            Set(value As Boolean)
                TV.ShowPlusMinus = value
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob Linien zwischen den Stammknoten angezeigt werden.
        ''' </summary>
        <Category("Behavior")>
        <Description("Gibt an, ob Linien zwischen den Stammknoten angezeigt werden.")>
        <Browsable(True)>
        Public Property ShowRootLines As Boolean
            Get
                Return TV.ShowRootLines
            End Get
            Set(value As Boolean)
                TV.ShowRootLines = value
            End Set
        End Property

        ''' <summary>
        ''' Ruft den Abstand für das Einrücken der einzelnen Ebenen von untergeordneten Strukturknoten ab oder legt diesen fest.
        ''' </summary>
        <Category("Behavior")>
        <Description("Ruft den Abstand für das Einrücken der einzelnen Ebenen von untergeordneten Strukturknoten ab oder legt diesen fest.")>
        <Browsable(True)>
        Public Property Indent As Integer
            Get
                Return TV.Indent
            End Get
            Set(value As Integer)
                TV.Indent = value
            End Set
        End Property

        ''' <summary>
        ''' Ruft die Höhe des jeweiligen Strukturknotens im Strukturansicht-Steuerelement ab oder legt diese fest.
        ''' </summary>
        <Category("Appearance")>
        <Description("Ruft die Höhe des jeweiligen Strukturknotens im Strukturansicht-Steuerelement ab oder legt diese fest.")>
        <Browsable(True)>
        Public Property ItemHeight As Integer
            Get
                Return TV.ItemHeight
            End Get
            Set(value As Integer)
                TV.ItemHeight = value
            End Set
        End Property

        ''' <summary>
        ''' Legt die Hintergrundfarbe für das Steuerelement fest oder gibt diese zurück.
        ''' </summary>
        <Category("Appearance")>
        <Description("Legt die Hintergrundfarbe für das Steuerelement fest oder gibt diese zurück.")>
        <Browsable(True)>
        Public Overrides Property BackColor As Color
            Get
                Return MyBase.BackColor
            End Get
            Set(value As Color)
                MyBase.BackColor = value
                TV.BackColor = value
            End Set
        End Property

        ''' <summary>
        ''' Legt die Vordergrundfarbe für das Anzeigen von Text fest oder gibt diese zurück.
        ''' </summary>
        <Category("Appearance")>
        <Description("Legt die Vordergrundfarbe für das Anzeigen von Text fest oder gibt diese zurück.")>
        <Browsable(True)>
        Public Overrides Property ForeColor As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As Color)
                MyBase.ForeColor = value
                TV.ForeColor = value
            End Set
        End Property

        ''' <summary>
        ''' Legt die Schriftart für den Text im Steuerelement fest oder gibt diese zurück.
        ''' </summary>
        ''' <returns></returns>
        <Category("Appearance")>
        <Description("Legt die Schriftart für den Text im Steuerelement fest oder gibt diese zurück.")>
        <Browsable(True)>
        Public Overrides Property Font As Font
            Get
                Return MyBase.Font
            End Get
            Set(value As Font)
                MyBase.Font = value
                TV.Font = value
            End Set
        End Property

#End Region

#Region "ausgeblendete Eigenschaften"

        ''' <summary>
        ''' ausgeblendet da nicht relevant
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImage As Image
            Get
                Return MyBase.BackgroundImage
            End Get
            Set(value As Image)
                MyBase.BackgroundImage = value
            End Set
        End Property

        ''' <summary>
        ''' ausgeblendet da nicht relevant
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImageLayout As ImageLayout
            Get
                Return MyBase.BackgroundImageLayout
            End Get
            Set(value As ImageLayout)
                MyBase.BackgroundImageLayout = value
            End Set
        End Property

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Konstruktor für das ExplorerTreeView-Steuerelement.
        ''' </summary>
        ''' <remarks>
        ''' <para>Dieser Konstruktor initialisiert das Steuerelement und lädt die
        ''' erforderlichen Bilder. </para>
        ''' <para>Außerdem wird der Wurzelknoten des TreeViews gesetzt, um die Struktur des
        ''' Steuerelements zu definieren.</para>
        ''' </remarks>
        Public Sub New()
            ' Dieser Aufruf ist für den Designer erforderlich.
            InitializeComponent()
            ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            LoadImages()
            ' Setzt den Wurzelknoten des TreeViews
            SetRootNode()
        End Sub

        ''' <summary>
        ''' Öffnet und selektiert den Knoten zum angegebenen Verzeichnispfad.
        ''' </summary>
        ''' <remarks>
        ''' Funktioniert auch bei noch nicht geladenen Unterknoten.
        ''' </remarks>
        ''' <param name="Path">Vollständiger Pfad der göffnet werden soll.</param>
        ''' <returns>
        ''' <see langword="true"/>, wenn der Knoten gefunden wurde, sonst <see
        ''' langword="false"/>
        ''' </returns>
        Public Function ExpandPath(Path As String) As Boolean
            ' Ist der Pfad ungültig oder leer, wird keine weitere Verarbeitung durchgeführt.
            If String.IsNullOrWhiteSpace(Path) Then Return False
            ' Startet mit einem leeren Pfad (wird schrittweise aufgebaut)
            Dim lastpath As String = String.Empty
            ' Beginnt beim Wurzelknoten ("Dieser Computer") und öffnet diesen
            Dim lastnode As TreeNode = TV.Nodes.Item(0)
            lastnode.Expand()
            Dim foundNode As TreeNode
            ' Zerlegt den Zielpfad in einzelne Segmente (z.B. "C:", "Benutzer", "Name", "Dokumente")
            For Each pathsegment As String In GetPathSegments(Path)
                ' Fügt das aktuelle Segment zum bisherigen Pfad hinzu
                lastpath = IO.Path.Combine(lastpath, pathsegment)
                ' Sucht unter den Kindknoten von lastnode nach einem Knoten mit dem aktuellen Pfad
                foundNode = FindNodeByPath(lastnode.Nodes, lastpath)
                ' Falls kein passender Knoten gefunden wurde, bricht die Methode ab (Pfad existiert nicht im TreeView)
                If IsNothing(foundNode) Then Return False
                ' Expandiert den gefundenen Knoten, damit dessen Unterknoten geladen werden
                foundNode.Expand()
                ' Setzt lastnode auf den gefundenen Knoten, um in der nächsten Iteration darunter weiterzusuchen
                lastnode = foundNode
            Next
            ' Nach der Schleife: lastnode ist der Knoten, der dem Zielpfad entspricht
            ' Setzt diesen Knoten als ausgewählt im TreeView
            TV.SelectedNode = lastnode
            ' Gibt zurück, dass der Knoten erfolgreich gefunden und selektiert wurde
            Return True
        End Function

#End Region

#Region "Interne Hilfsroutinen"

        ''' <summary>
        ''' Zerlegt einen vollständigen Verzeichnispfad in seine einzelnen Segmente.
        ''' Beispiel: "C:\Benutzer\Name\Dokumente" wird zu {"C:", "Benutzer", "Name", "Dokumente"}.
        ''' Dies ist notwendig, um im TreeView schrittweise durch die Knotenstruktur zu navigieren.
        ''' </summary>
        ''' <param name="Path">
        ''' Der vollständige Verzeichnispfad, der zerlegt werden soll.
        ''' </param>
        ''' <returns>
        ''' Eine Liste der einzelnen Pfadsegmente, beginnend mit dem Wurzelverzeichnis.
        ''' </returns>
        Private Shared Function GetPathSegments(Path As String) As List(Of String)
            ' Erzeugt ein DirectoryInfo-Objekt für den angegebenen Pfad
            Dim dirInfo As New DirectoryInfo(Path)
            ' Liste, in der die einzelnen Segmente gesammelt werden
            Dim result As New List(Of String)
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
        ''' Setzt den Wurzelknoten des TreeViews.
        ''' </summary>
        ''' <remarks>
        ''' Diese Methode entfernt alle vorhandenen Knoten im TreeView und fügt einen neuen Wurzelknoten vom Typ ComputerNode hinzu.
        ''' Der Wurzelknoten repräsentiert "Dieser Computer" und bildet die Basis für die Anzeige von Laufwerken und speziellen Ordnern.
        ''' Nach dem Hinzufügen wird der Wurzelknoten automatisch erweitert, sodass dessen Unterknoten (z.B. Laufwerke) sichtbar sind.
        ''' </remarks>
        Private Sub SetRootNode()
            ' Alle vorhandenen Knoten im TreeView entfernen
            TV.Nodes.Clear()
            ' Neuen Wurzelknoten vom Typ ComputerNode erstellen und das passende Icon zuweisen
            Dim rootnode As New ComputerNode With {.ImageKey = $"Computer", .SelectedImageKey = $"Computer"}
            ' Den Wurzelknoten zum TreeView hinzufügen
            Dim unused = TV.Nodes.Add(rootnode)
            ' Den Wurzelknoten automatisch erweitern, damit die Unterknoten angezeigt werden
            TV.Nodes.Item(0).Expand()
        End Sub

        ''' <summary>
        ''' Lädt die Bilder für die Knoten des TreeViews.
        ''' </summary>
        ''' <remarks>
        ''' Diese Methode fügt die benötigten Icons in die ImageList des TreeViews ein.
        ''' Die Bilder stammen aus den Ressourcen der Anwendung und repräsentieren verschiedene Arten von Knoten wie Computer, Laufwerke und Ordner.
        ''' Jedes Icon wird mit einem eindeutigen Schlüssel versehen, der später zum Setzen der Knotenbilder verwendet wird.
        ''' Diese Methode wird einmalig beim Initialisieren des Steuerelements aufgerufen, um sicherzustellen, dass alle benötigten Bilder vorhanden sind.
        ''' </remarks>
        Private Sub LoadImages()
            TV.ImageList.Images.Clear()
            TV.ImageList.Images.Add($"Computer", My.Resources.Computer)
            TV.ImageList.Images.Add($"DriveSystem", My.Resources.DriveSystem)
            TV.ImageList.Images.Add($"DriveFixed", My.Resources.DriveFixed)
            TV.ImageList.Images.Add($"DriveCDROM", My.Resources.DriveCDRom)
            TV.ImageList.Images.Add($"DriveRemovable", My.Resources.DriveRemovable)
            TV.ImageList.Images.Add($"DriveNetwork", My.Resources.DriveNetwork)
            TV.ImageList.Images.Add($"DriveRamDisk", My.Resources.DriveRamDisk)
            TV.ImageList.Images.Add($"DriveFloppy", My.Resources.DriveDisk)
            TV.ImageList.Images.Add($"DriveUnknown", My.Resources.DriveUnknown)
            TV.ImageList.Images.Add($"Folder", My.Resources.Folder)
            TV.ImageList.Images.Add($"FolderDesktop", My.Resources.FolderDesktop)
            TV.ImageList.Images.Add($"FolderDocuments", My.Resources.FolderDocuments)
            TV.ImageList.Images.Add($"FolderDownloads", My.Resources.FolderDownloads)
            TV.ImageList.Images.Add($"FolderMusic", My.Resources.FolderMusic)
            TV.ImageList.Images.Add($"FolderPictures", My.Resources.FolderPictures)
        End Sub

        ''' <summary>
        ''' Ermittelt den Verzeichnispfad basierend auf dem ausgewählten Knoten im TreeView.
        ''' </summary>
        ''' <param name="node"></param>
        Private Function GetDirectoryPath(node As TreeNode) As String
            Dim path As String = String.Empty
            Select Case True
                Case TypeOf node Is ComputerNode
                    ' "Dieser Computer" hat keinen Pfad
                    path = String.Empty
                Case TypeOf node Is DriveNode
                    ' Gibt den Laufwerksbuchstaben zurück
                    path = CType(node, DriveNode).FullPath
                Case TypeOf node Is SpecialFolderNode
                    ' Gibt den Pfad für Spezialordner zurück
                    path = CType(node, SpecialFolderNode).FullPath
                Case TypeOf node Is FolderNode
                    ' Gibt den Pfad für alle anderen Ordner zurück
                    path = CType(node, FolderNode).FullPath
            End Select
            Return path
        End Function

        ''' <summary>
        ''' Lädt die Unterordner eines Ordners, wenn der Knoten erweitert wird.
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks>
        ''' Diese Methode wird aufgerufen, wenn ein Ordnerknoten im TreeView erweitert wird.
        ''' Sie leert die vorhandenen Knoten und lädt die Unterordner des Ordners neu.
        ''' Diese Methode ist entscheidend für die Anzeige der Unterordner eines Ordners im TreeView.
        ''' Sie sorgt dafür, dass die Struktur des TreeViews dynamisch erweitert wird, wenn der Benutzer einen Ordner öffnet.
        ''' Dadurch wird eine effiziente Navigation durch die Ordnerstruktur ermöglicht.
        ''' </remarks>
        Private Sub LoadFoldersSubfolders(Node As TreeNode)
            ' Typumwandlung des Knotens in FolderNode
            Dim foldernode As FolderNode = CType(Node, FolderNode)
            ' Leeren der Knoten, um sie neu zu laden
            foldernode.Nodes.Clear()
            ' Laden der Unterordner des Ordners
            foldernode.LoadSubfolders()
        End Sub

        ''' <summary>
        ''' Lädt die Unterordner eines Laufwerks, wenn der Knoten erweitert wird.
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks>
        ''' Diese Methode wird aufgerufen, wenn ein Laufwerksknoten im TreeView erweitert wird.
        ''' Sie leert die vorhandenen Knoten und lädt die Unterordner des Laufwerks neu.
        ''' Diese Methode ist entscheidend für die Anzeige der Unterordner von Laufwerken im TreeView.
        ''' Sie sorgt dafür, dass die Struktur des TreeViews dynamisch erweitert wird, wenn der Benutzer ein Laufwerk öffnet.
        ''' Dadurch wird eine effiziente Navigation durch die Ordnerstruktur ermöglicht.
        ''' </remarks>
        Private Sub LoadDriveSubfolders(Node As TreeNode)
            ' Typumwandlung des Knotens in DriveNode
            Dim drivenode As DriveNode = CType(Node, DriveNode)
            ' Leeren der Knoten, um sie neu zu laden
            drivenode.Nodes.Clear()
            ' Laden der Unterordner des Laufwerks
            drivenode.LoadSubfolders()
        End Sub

        ''' <summary>
        ''' Lädt die Unterordner eines speziellen Ordners, wenn der Knoten erweitert wird.
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks>
        ''' Diese Methode wird aufgerufen, wenn ein spezieller Ordnerknoten im TreeView erweitert wird.
        ''' Sie leert die vorhandenen Knoten und lädt die Unterordner des speziellen Ordners neu.
        ''' 
        ''' Diese Methode ist entscheidend für die Anzeige der Unterordner von speziellen Ordnern wie "Desktop", "Dokumente" usw.
        ''' Sie sorgt dafür, dass die Struktur des TreeViews dynamisch erweitert wird, wenn der Benutzer einen speziellen Ordner öffnet.
        ''' </remarks>
        Private Sub LoadSpecialFoldersSubfolders(Node As TreeNode)
            ' Typumwandlung des Knotens in SpecialFolderNode
            Dim spezialfoldernode As SpecialFolderNode = CType(Node, SpecialFolderNode)
            ' Leeren der Knoten, um sie neu zu laden
            spezialfoldernode.Nodes.Clear()
            ' Laden der Unterordner des speziellen Ordners
            spezialfoldernode.LoadSubfolders()
        End Sub

        ''' <summary>
        ''' Lädt die speziellen Ordner und Laufwerke, wenn der Computer-Knoten erweitert wird.
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks>
        ''' Diese Methode wird aufgerufen, wenn der Computer-Knoten im TreeView erweitert wird.
        ''' Sie leert die vorhandenen Knoten und lädt die speziellen Ordner sowie die Laufwerke neu. 
        ''' 
        ''' Diese Methode ist entscheidend für die initiale Struktur des TreeViews, 
        ''' da sie die Basis für die Anzeige der verfügbaren Laufwerke und Ordner bildet.
        ''' </remarks>
        Private Sub LoadRootKindNodes(Node As TreeNode)
            ' Typumwandlung des Knotens in Computernode
            Dim computernode As ComputerNode = CType(Node, ComputerNode)
            ' Leeren der Knoten, um sie neu zu laden
            computernode.Nodes.Clear()
            ' Laden der speziellen Ordner
            computernode.LoadSpecialFolders()
            ' Laden der Laufwerke
            computernode.LoadDrives()
        End Sub

        ''' <summary>
        ''' Erstellt einen FileSystemWatcher für den angegebenen Pfad, um Änderungen zu überwachen
        ''' </summary>
        ''' <param name="FolderPath">
        ''' Der zu überwachende Verzeichnispfad
        ''' </param>
        Private Sub CreateFileSystemWatcher(FolderPath As String)
            ' Prüfen ob Verzeichnispfad nicht leer und vorhanden ist
            If String.IsNullOrEmpty(FolderPath) OrElse Not Directory.Exists(FolderPath) Then Return
            ' Prüfen, ob bereits ein FileSystemWatcher für diesen Pfad existiert
            If _FileSystemWatchers.ContainsKey(FolderPath) Then Return

            Try
                ' Neuen FileSystemWatcher erstellen
                Dim FSW As New FileSystemWatcher(FolderPath) With {
                    .NotifyFilter = NotifyFilters.DirectoryName,' Verzeichnisse überwachen
                    .IncludeSubdirectories = False ' Nur das aktuelle Verzeichnis überwachen
                    }
                ' Event-Handler für neu erstellten Ordner hinzufügen
                AddHandler FSW.Created, AddressOf FSW_DirectoryChanged
                ' Event-Handler für gelöschten Ordner hinzufügen
                AddHandler FSW.Deleted, AddressOf FSW_DirectoryChanged
                ' Event-Handler für umbenannten Ordner hinzufügen
                AddHandler FSW.Renamed, AddressOf FSW_DirectoryChanged
                ' Watcher in die Sammlung einfügen
                _FileSystemWatchers.Add(FolderPath, FSW)
                ' Watcher aktivieren
                FSW.EnableRaisingEvents = True
            Catch ex As Exception
                ' Fehlerbehandlung (z.B. unzureichende Berechtigungen)
                Debug.WriteLine($"Fehler beim Erstellen des FileSystemWatchers: {ex.Message}")
            End Try
        End Sub

        ''' <summary>
        ''' Entfernt alle FileSystemWatcher für das angegebene Verzeichnis und alle Unterverzeichnisse.
        ''' Dies ist wichtig, um Ressourcen freizugeben und zu verhindern, dass nicht mehr benötigte Watcher weiterhin Ereignisse auslösen.
        ''' </summary>
        ''' <param name="FolderPath">
        ''' Das Verzeichnis, für das die zugehörigen Watcher entfernt werden sollen.
        ''' </param>
        Private Sub RemoveFileSystemWatchers(FolderPath As String)
            ' Erstelle eine Liste, in der alle zu entfernenden Watcher-Pfade gesammelt werden
            Dim toRemove As New List(Of String)
            ' Finde alle Watcher, die auf das angegebene Verzeichnis oder dessen Unterverzeichnisse zeigen
            FindWatchersToRemove(FolderPath, toRemove)
            ' Entferne und entsorge alle gefundenen Watcher
            RemoveAndDisposeWatchers(toRemove)
        End Sub

        ''' <summary>
        ''' Sucht alle FileSystemWatcher-Pfade heraus, die entfernt werden sollen.
        ''' Ein Pfad wird dann zur Liste hinzugefügt, wenn er entweder exakt dem angegebenen Verzeichnis entspricht
        ''' oder ein Unterverzeichnis davon ist. Dies ist die Vorbereitung, um alle betroffenen Watcher später zu entfernen.
        ''' </summary>
        ''' <param name="FolderPath">
        ''' Das Verzeichnis, für das die zu entfernenden Watcher gesucht werden.
        ''' </param>
        ''' <param name="toRemove">
        ''' Liste, in die alle zu entfernenden Watcher-Pfade eingetragen werden.
        ''' </param>
        Private Sub FindWatchersToRemove(FolderPath As String, toRemove As List(Of String))
            ' Durchlaufe alle aktuell überwachten Verzeichnispfade
            For Each watcherPath In _FileSystemWatchers.Keys
                ' Prüfe, ob der Pfad exakt übereinstimmt oder ein Unterverzeichnis des angegebenen Verzeichnisses ist
                If watcherPath.Equals(FolderPath, StringComparison.OrdinalIgnoreCase) OrElse
                   watcherPath.StartsWith(FolderPath & IO.Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) Then
                    ' Füge den Pfad der Liste der zu entfernenden Watcher hinzu
                    toRemove.Add(watcherPath)
                End If
            Next
        End Sub

        ''' <summary>
        ''' Entfernt und entsorgt alle FileSystemWatcher-Objekte, deren Pfade in der übergebenen Liste enthalten sind.
        ''' Dies ist notwendig, um Ressourcen freizugeben und zu verhindern, dass nicht mehr benötigte Watcher weiterhin Ereignisse auslösen.
        ''' </summary>
        ''' <param name="toRemove">
        ''' Liste der Verzeichnispfade, deren Watcher entfernt und entsorgt werden sollen.
        ''' </param>
        Private Sub RemoveAndDisposeWatchers(toRemove As List(Of String))
            ' Durchlaufe alle zu entfernenden Watcher-Pfade
            For Each watcherPath In toRemove
                ' Hole den zugehörigen FileSystemWatcher aus dem Dictionary
                Dim watcher = _FileSystemWatchers(watcherPath)
                ' Deaktiviere die Ereignisauslösung
                watcher.EnableRaisingEvents = False
                ' Entferne alle zugehörigen Event-Handler, um Speicherlecks zu vermeiden
                RemoveWatcherHandlers(watcher)
                ' Gib die Ressourcen des Watchers frei
                watcher.Dispose()
                ' Entferne den Watcher aus der internen Sammlung
                Dim unused = _FileSystemWatchers.Remove(watcherPath)
            Next
        End Sub

        ''' <summary>
        ''' Entfernt die Event-Handler für die Ereignisse Created, Deleted und Renamed vom angegebenen FileSystemWatcher.
        ''' Dies ist notwendig, um Speicherlecks und unerwünschte Ereignisauslösungen zu vermeiden,
        ''' wenn ein Watcher nicht mehr benötigt wird und entsorgt werden soll.
        ''' </summary>
        ''' <param name="watcher">
        ''' Der FileSystemWatcher, von dem die Handler entfernt werden sollen.
        ''' </param>
        Private Sub RemoveWatcherHandlers(watcher As FileSystemWatcher)
            ' Entfernt den Handler für das Created-Ereignis (neues Verzeichnis wurde erstellt)
            RemoveHandler watcher.Created, AddressOf FSW_DirectoryChanged
            ' Entfernt den Handler für das Deleted-Ereignis (Verzeichnis wurde gelöscht)
            RemoveHandler watcher.Deleted, AddressOf FSW_DirectoryChanged
            ' Entfernt den Handler für das Renamed-Ereignis (Verzeichnis wurde umbenannt)
            RemoveHandler watcher.Renamed, AddressOf FSW_DirectoryChanged
        End Sub

        ''' <summary>
        ''' Sucht rekursiv im gesamten TreeView nach einem Knoten mit dem angegebenen Verzeichnispfad.
        ''' </summary>
        ''' <param name="Nodes">
        ''' Die NodesCollection, in der gesucht werden soll (z.B. TV.Nodes)
        ''' </param>
        ''' <param name="SearchPath">
        ''' Der zu suchende Pfad
        ''' </param>
        ''' <returns>
        ''' Der gefundene TreeNode oder Nothing
        ''' </returns>
        Private Function FindNodeByPath(Nodes As TreeNodeCollection, SearchPath As String) As TreeNode
            ' Durchlaufe alle Knoten in der aktuellen Knotenliste
            For Each node As TreeNode In Nodes
                ' Vergleiche den Pfad des aktuellen Knotens mit dem gesuchten Pfad (Groß-/Kleinschreibung wird ignoriert)
                If String.Equals(GetDirectoryPath(node), SearchPath, StringComparison.OrdinalIgnoreCase) Then
                    Return node ' Passender Knoten gefunden, diesen zurückgeben
                End If
                ' Wenn nicht gefunden, rekursiv in den Unterknoten weitersuchen
                Dim found As TreeNode = FindNodeByPath(node.Nodes, SearchPath)
                If found IsNot Nothing Then
                    ' Passenden Knoten in den Unterknoten gefunden, diesen zurückgeben
                    Return found
                End If
            Next
            ' Kein passender Knoten gefunden, Nothing zurückgeben
            Return Nothing
        End Function

#End Region

#Region "Ereignisbehandlung FileSystemWatcher"

        ''' <summary>
        ''' Wird aufgerufen, wenn sich der Inhalt eines überwachten Verzeichnisses geändert hat (z.B. Ordner wurde erstellt, gelöscht oder umbenannt).
        ''' Aktualisiert die betroffenen Knoten im TreeView, damit die Anzeige immer dem aktuellen Dateisystem entspricht.
        ''' </summary>
        ''' <param name="sender">
        ''' Der FileSystemWatcher, der das Ereignis ausgelöst hat.
        ''' </param>
        ''' <param name="e">
        ''' Informationen über die Änderung (z.B. Pfad, Art der Änderung).
        ''' </param>
        Private Sub FSW_DirectoryChanged(sender As Object, e As FileSystemEventArgs)
            ' Prüfen, ob der Methodenaufruf aus einem anderen Thread als dem UI-Thread kommt.
            ' Falls ja, Methode erneut im UI-Thread ausführen (wichtig für Thread-Sicherheit bei UI-Elementen).
            If TV.InvokeRequired Then
                Dim unused = TV.Invoke(New MethodInvoker(Sub() FSW_DirectoryChanged(sender, e)))
                Return
            End If
            ' Sucht im TreeView den Knoten, dessen Pfad dem überwachten Verzeichnis entspricht.
            Dim changednode As TreeNode = FindNodeByPath(TV.Nodes, CType(sender, FileSystemWatcher).Path)
            ' Je nach Knotentyp (normaler Ordner oder spezieller Ordner) werden die Unterknoten neu geladen,
            ' damit neue, gelöschte oder umbenannte Unterordner sofort angezeigt werden.
            Select Case True
                Case TypeOf changednode Is DriveNode
                    ' Unterknoten des Laufwerks leeren und neu laden
                    LoadDriveSubfolders(changednode)
                Case TypeOf changednode Is SpecialFolderNode
                    ' Unterknoten des speziellen Ordners leeren und neu laden
                    LoadSpecialFoldersSubfolders(changednode)
                Case TypeOf changednode Is FolderNode
                    ' Unterknoten des Ordners leeren und neu laden
                    LoadFoldersSubfolders(changednode)
            End Select
        End Sub

#End Region

#Region "Ereignisbehandlung TreeView"

        ''' <summary>
        ''' Wird ausgelöst, bevor ein Knoten erweitert wird.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks>
        ''' Abhängig vom Typ des Knotens werden die entsprechenden Unterordner geladen.
        ''' Die verschiedenen Knotentypen (ComputerNode, SpecialFolderNode, DriveNode, FolderNode) werden unterschieden,
        ''' um die passenden Unterordner zu laden und anzuzeigen.
        ''' Das Ereignis wird verwendet, um die Struktur des TreeViews dynamisch zu erweitern,
        ''' indem nur die Knoten geladen werden, die tatsächlich benötigt werden.
        ''' Dadurch wird die Leistung verbessert und die Benutzererfahrung optimiert.
        ''' </remarks>
        Private Sub TV_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles TV.BeforeExpand
            ' Knoten merken der erweitert werden soll
            Dim expandnode As TreeNode = e.Node
            ' Typ des Knotens ermitteln und entsprechend verzweigen
            Select Case True
                Case TypeOf expandnode Is ComputerNode
                    ' Spezielle Ordner und Laufwerke laden
                    LoadRootKindNodes(expandnode)
                Case TypeOf expandnode Is SpecialFolderNode
                    ' Unterordner der speziellen Ordner laden
                    LoadSpecialFoldersSubfolders(expandnode)
                Case TypeOf expandnode Is DriveNode
                    ' Unterordner des Laufwerks laden
                    LoadDriveSubfolders(expandnode)
                Case TypeOf expandnode Is FolderNode
                    ' Unterordner des Ordners laden
                    LoadFoldersSubfolders(expandnode)
            End Select
        End Sub

        ''' <summary>
        ''' Tritt ein, wenn der Strukturknoten erweitert wurde.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub TV_AfterExpand(sender As Object, e As TreeViewEventArgs) Handles TV.AfterExpand
            ' einen FileSystemWatcher für das geöffnete Verzeichnis erstellen
            Dim folderpath As String = GetDirectoryPath(e.Node)
            CreateFileSystemWatcher(folderpath)
        End Sub

        ''' <summary>
        ''' Tritt ein, wenn der Strukturknoten reduziert wurde.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub TV_AfterCollapse(sender As Object, e As TreeViewEventArgs) Handles TV.AfterCollapse
            ' den FilesystemWatcher für das geschlossene Verzeichnis und alle eventuell geöffnete Unterverzeichnisse entfernen
            Dim folderpath As String = GetDirectoryPath(e.Node)
            RemoveFileSystemWatchers(folderpath)
        End Sub

        ''' <summary>
        ''' Behandelt das AfterSelect-Ereignis des TreeViews.
        ''' Dieses Ereignis wird ausgelöst, wenn ein Knoten im TreeView ausgewählt wird.
        ''' Es aktualisiert den aktuell ausgewählten Pfad basierend auf dem ausgewählten Knoten.
        ''' Diese Methode ist wichtig, um sicherzustellen, dass der aktuell ausgewählte Pfad immer korrekt ist,
        ''' und ermöglicht anderen Teilen der Anwendung, auf Änderungen im ausgewählten Pfad zu reagieren.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub TV_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TV.AfterSelect
            ' Pfad des ausgewählten node ermitteln und Ereignis auslösen
            _SelectedPath = GetDirectoryPath(e.Node)
            RaiseEvent SelectedPathChanged(Me, EventArgs.Empty)
        End Sub

#End Region

#Region "Ereignisbehandlung DriveWatcher"

        ''' <summary>
        ''' Wird ausgeführt wenn ein neues Laufwerk hinzugefügt wurde
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub DW_DriveAdded(sender As Object, e As DriveAddedEventArgs) Handles DW.DriveAdded
            Dim newDriveNode As New DriveNode(New DriveInfo(e.DriveName)) With {.Tag = e.DriveName}
            Dim inserted As Boolean = False
            ' Durchlaufe alle Knoten des Computer-Knotens (Wurzelknoten)
            For i As Integer = 0 To TV.Nodes.Item(0).Nodes.Count - 1
                ' Überprüfe, ob der aktuelle Knoten ein DriveNode ist und alphabetisch hinter dem neuen Laufwerk liegt
                Dim currentNode As TreeNode = TV.Nodes.Item(0).Nodes(i)
                If TypeOf currentNode Is DriveNode AndAlso
                    String.Compare(
                    currentNode.Tag.ToString,
                    newDriveNode.Tag.ToString,
                    StringComparison.OrdinalIgnoreCase) > 0 Then
                    TV.Nodes.Item(0).Nodes.Insert(i, newDriveNode)
                    inserted = True
                    Exit For
                End If
            Next
            ' Falls das neue Laufwerk alphabetisch am Ende eingefügt werden muss
            If Not inserted Then
                Dim unused = TV.Nodes.Item(0).Nodes.Add(newDriveNode)
            End If
        End Sub

        ''' <summary>
        ''' Wird ausgeführt wenn ein Laufwerk entfernt wurde
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub DW_DriveRemoved(sender As Object, e As DriveRemovedEventArgs) Handles DW.DriveRemoved
            Dim drn As DriveNode
            ' Durchlaufe alle Knoten des Computer-Knotens (Wurzelknoten)
            For Each obj As Object In TV.Nodes.Item(0).Nodes
                ' Überprüfe, ob der aktuelle Knoten ein DriveNode ist
                If TypeOf obj Is DriveNode Then
                    ' Konvertiere den Knoten in einen DriveNode
                    drn = CType(obj, DriveNode)
                    ' Überprüfe, ob der Tag des DriveNode mit dem Namen des entfernten Laufwerks übereinstimmt
                    If drn.Tag.ToString() = e.DriveName Then
                        ' Entferne den DriveNode aus der Liste
                        drn.Remove()
                    End If
                End If
            Next
        End Sub

#End Region

    End Class

End Namespace