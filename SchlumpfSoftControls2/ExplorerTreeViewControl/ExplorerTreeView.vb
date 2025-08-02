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

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports SchlumpfSoft.Controls.DriveWatcherControl

Namespace ExplorerTreeViewControl

    ' CategoryAttribute Klasse: https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.categoryattribute?view=netframework-4.7.2

    ''' <summary>
    ''' Stellt ein Steuerelement zur Anzeige und Navigation der Verzeichnisstruktur des Computers bereit.
    ''' Ermöglicht die Auswahl von Laufwerken, speziellen Ordnern und Unterordnern in einer hierarchischen Baumstruktur.
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Stellt ein Steuerelement zur Anzeige und Navigation der Verzeichnisstruktur des Computers bereit.")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(ExplorerTreeViewControl.ExplorerTreeView), "ExplorerTreeViewControl.ExplorerTreeView.bmp")>
    Public Class ExplorerTreeView : Inherits UserControl

#Region "Interne Variablen"

        Private _SelectedPath As String = String.Empty
        Private _FileSystemWatchers As New Dictionary(Of String, FileSystemWatcher)

#End Region

#Region "Definition der öffentlichen Ereignisse"

        ''' <summary>
        ''' Ereignis, das ausgelöst wird, wenn sich der ausgewählte Pfad ändert.
        ''' Dieses Ereignis wird verwendet, um andere Steuerelemente oder Logik zu benachrichtigen,
        ''' wenn der Benutzer einen anderen Knoten im TreeView auswählt.
        ''' Es ermöglicht eine reaktive Programmierung, bei der andere Teile der Anwendung auf Änderungen im ausgewählten Pfad reagieren können.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        <Description("")>
        <Browsable(True)>
        Public Event SelectedPathChanged(sender As Object, e As EventArgs)

#End Region

#Region "Öffentliche Eigenschaften"

        ''' <summary>
        '''Gibt den vollständigen Pfad des ausgewählten Knotens zurück.
        ''' Diese Eigenschaft wird aktualisiert, wenn ein Knoten im TreeView ausgewählt wird.
        ''' Sie ermöglicht den Zugriff auf den Pfad des aktuell ausgewählten Knotens,
        ''' was für weitere Operationen wie das Öffnen oder Bearbeiten von Dateien und Ordnern nützlich ist.
        ''' </summary>
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
            Set
                TV.ShowLines = Value
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
            Set
                TV.ShowPlusMinus = Value
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
            Set
                TV.ShowRootLines = Value
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
            Set
                TV.Indent = Value
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
            Set
                TV.ItemHeight = Value
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

        ''' <summary>
        ''' Konstruktor für das ExplorerTreeView-Steuerelement.
        ''' </summary>
        ''' <remarks>
        ''' Dieser Konstruktor initialisiert das Steuerelement und lädt die erforderlichen Bilder.
        ''' Außerdem wird der Wurzelknoten des TreeViews gesetzt, um die Struktur des Steuerelements zu definieren.
        ''' </remarks>
        Public Sub New()
            ' Dieser Aufruf ist für den Designer erforderlich.
            InitializeComponent()
            ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            LoadImages()
            ' Setzt den Wurzelknoten des TreeViews
            SetRootNode()
        End Sub

#Region "Interne Hilfsroutinen"

        ''' <summary>
        ''' Setzt den Wurzelknoten des TreeViews.
        ''' </summary>
        ''' <remarks>
        ''' Der Wurzelknoten ist ein ComputerNode, der die Basis für die Anzeige von Laufwerken und speziellen Ordnern bildet.
        ''' Dieser Knoten wird beim Laden des Steuerelements erstellt und hinzugefügt.
        ''' </remarks>
        Private Sub SetRootNode()
            TV.Nodes.Clear()
            Dim rootnode As New ComputerNode With {.ImageKey = $"Computer", .SelectedImageKey = $"Computer"}
            Dim unused = TV.Nodes.Add(rootnode)
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
        Private Function GetPath(node As TreeNode) As String
            Dim result As String = String.Empty
            Select Case node.GetType
                Case GetType(ComputerNode) : result = String.Empty ' "Dieser Computer" hat keinen Pfad
                Case GetType(DriveNode) : result = CType(node, DriveNode).FullPath ' Gibt den Laufwerksbuchstaben zuück
                Case GetType(SpecialFolderNode) : result = CType(node, SpecialFolderNode).FullPath ' Gibt den Pfad für Spezialordner zurück
                Case GetType(FolderNode) : result = CType(node, FolderNode).FullPath ' Gibt den Pfad für alle anderen ordner zurück
            End Select
            Return result
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
        Private Sub LoadFoldersSubfolders(e As TreeViewCancelEventArgs)
            ' Typumwandlung des Knotens in FolderNode
            Dim node As FolderNode = CType(e.Node, FolderNode)
            ' Leeren der Knoten, um sie neu zu laden
            node.Nodes.Clear()
            ' Laden der Unterordner des Ordners
            node.LoadSubfolders()
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
        Private Sub LoadDriveSubfolders(e As TreeViewCancelEventArgs)
            ' Typumwandlung des Knotens in DriveNode
            Dim node As DriveNode = CType(e.Node, DriveNode)
            ' Leeren der Knoten, um sie neu zu laden
            node.Nodes.Clear()
            ' Laden der Unterordner des Laufwerks
            node.LoadSubfolders()
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
        Private Sub LoadSpecialFoldersSubfolders(e As TreeViewCancelEventArgs)
            ' Typumwandlung des Knotens in SpecialFolderNode
            Dim node As SpecialFolderNode = CType(e.Node, SpecialFolderNode)
            ' Leeren der Knoten, um sie neu zu laden
            node.Nodes.Clear()
            ' Laden der Unterordner des speziellen Ordners
            node.LoadSubfolders()
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
        Private Sub LoadRootKindNodes(e As TreeViewCancelEventArgs)
            ' Leeren der Knoten, um sie neu zu laden
            CType(e.Node, ComputerNode).Nodes.Clear()
            ' Laden der speziellen Ordner
            CType(e.Node, ComputerNode).LoadSpecialFolders()
            ' Laden der Laufwerke
            CType(e.Node, ComputerNode).LoadDrives()
        End Sub

        ''' <summary>
        ''' Erstellt einen FileSystemWatcher für den angegebenen Pfad, um Änderungen zu überwachen
        ''' </summary>
        ''' <param name="FolderPath">Der zu überwachende Verzeichnispfad</param>
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
                ' Event-Handler hinzufügen
                AddHandler FSW.Created, AddressOf FSW_DirectoryChanged
                AddHandler FSW.Deleted, AddressOf FSW_DirectoryChanged
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
        ''' </summary>
        ''' <param name="FolderPath"></param>
        Private Sub RemoveFileSystemWatchers(FolderPath As String)
            ' Sammle alle zu entfernenden Pfade (das Verzeichnis und alle Unterverzeichnisse)
            Dim toRemove As New List(Of String)
            For Each watcherPath In _FileSystemWatchers.Keys
                ' Prüfe, ob watcherPath gleich Path oder ein Unterverzeichnis davon ist
                If watcherPath.Equals(FolderPath, StringComparison.OrdinalIgnoreCase) OrElse
                watcherPath.StartsWith(FolderPath & IO.Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) Then
                    toRemove.Add(watcherPath)
                End If
            Next
            ' Entferne alle gefundenen Watcher
            For Each watcherPath In toRemove
                ' Watcher deaktivieren
                Dim watcher = _FileSystemWatchers(watcherPath)
                watcher.EnableRaisingEvents = False
                ' Event-Handler entfernen
                RemoveHandler watcher.Created, AddressOf FSW_DirectoryChanged
                RemoveHandler watcher.Deleted, AddressOf FSW_DirectoryChanged
                RemoveHandler watcher.Renamed, AddressOf FSW_DirectoryChanged
                ' Ressourcen freigeben
                watcher.Dispose()
                ' Watcher aus der Sammlung entfernen
                _FileSystemWatchers.Remove(watcherPath)
            Next
        End Sub

        ''' <summary>
        ''' Sucht rekursiv im gesamten TreeView nach einem Knoten mit dem angegebenen Verzeichnispfad.
        ''' </summary>
        ''' <param name="Nodes">Die NodesCollection, in der gesucht werden soll (z.B. TV.Nodes)</param>
        ''' <param name="SearchPath">Der zu suchende Pfad</param>
        ''' <returns>Der gefundene TreeNode oder Nothing</returns>
        Private Function FindNodeByPath(Nodes As TreeNodeCollection, SearchPath As String) As TreeNode
            For Each node As TreeNode In Nodes
                If String.Equals(GetPath(node), SearchPath, StringComparison.OrdinalIgnoreCase) Then Return node
                ' Rekursiv in den Unterknoten suchen
                Dim found As TreeNode = FindNodeByPath(node.Nodes, SearchPath)
                If found IsNot Nothing Then Return found
            Next
            Return Nothing
        End Function

#End Region

#Region "Ereignisbehandlung FileSystemWatcher"

        ''' <summary>
        ''' Wird aufgerufen wenn sich der Inhalt eines Verzeichnisses geändert hat.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub FSW_DirectoryChanged(sender As Object, e As FileSystemEventArgs)

            Dim changeddirpath As String = CType(sender, FileSystemWatcher).Path
            Dim changednode As TreeNode = FindNodeByPath(TV.Nodes, changeddirpath)

#If DEBUG Then
            Debug.Print($"Inhalt von Node {changednode.Text} hat sich geändert")
            Debug.Print($"Verzeichnispfad:{changeddirpath}")
            Select Case e.ChangeType
                Case WatcherChangeTypes.Created : Debug.Print($"{e.FullPath.Split(IO.Path.DirectorySeparatorChar).Last} wurde hinzugefügt.")
                Case WatcherChangeTypes.Deleted : Debug.Print($"{e.FullPath.Split(IO.Path.DirectorySeparatorChar).Last} wurde entfernt.")
                Case WatcherChangeTypes.Renamed : Debug.Print($"{e.FullPath.Split(IO.Path.DirectorySeparatorChar).Last} wurde umbenannt.")
            End Select
#End If

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
            Select Case True
                ' Spezielle Ordner und Laufwerke laden
                Case e.Node.GetType.Equals(GetType(ComputerNode)) : LoadRootKindNodes(e)
                ' Unterordner der speziellen Ordner laden
                Case e.Node.GetType.Equals(GetType(SpecialFolderNode)) : LoadSpecialFoldersSubfolders(e)
                ' Unterordner des Laufwerks laden
                Case e.Node.GetType.Equals(GetType(DriveNode)) : LoadDriveSubfolders(e)
                ' Unterordner des Ordners laden
                Case e.Node.GetType.Equals(GetType(FolderNode)) : LoadFoldersSubfolders(e)
            End Select
        End Sub

        ''' <summary>
        ''' Tritt ein, wenn der Strukturknoten erweitert wurde.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub TV_AfterExpand(sender As Object, e As TreeViewEventArgs) Handles TV.AfterExpand




#If DEBUG Then
            Debug.Print($"{e.Node.FullPath} wurde geöffnet")
#End If


            ' einen FileSystemWatcher für das geöffnete Verzeichnis erstellen
            Dim folderpath As String = GetPath(e.Node)
            CreateFileSystemWatcher(folderpath)
        End Sub

        ''' <summary>
        ''' Tritt ein, wenn der Strukturknoten reduziert wurde.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub TV_AfterCollapse(sender As Object, e As TreeViewEventArgs) Handles TV.AfterCollapse


#If DEBUG Then
            Debug.Print($"{e.Node.FullPath} wurde geschlossen")
#End If



            ' den FilesystemWatcher für das geschlossene Verzeichnis und alle eventuell geöffnete Unterverzeichnisse entfernen
            Dim folderpath As String = GetPath(e.Node)
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
            _SelectedPath = GetPath(e.Node)
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
                Dim currentNode As TreeNode = TV.Nodes.Item(0).Nodes(i)
                ' Überprüfe, ob der aktuelle Knoten ein DriveNode ist und alphabetisch hinter dem neuen Laufwerk liegt
                If TypeOf currentNode Is DriveNode AndAlso String.Compare(currentNode.Tag.ToString, newDriveNode.Tag.ToString, StringComparison.OrdinalIgnoreCase) > 0 Then
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