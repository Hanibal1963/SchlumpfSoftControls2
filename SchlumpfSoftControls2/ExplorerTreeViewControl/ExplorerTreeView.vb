' *************************************************************************************************
' 
' ExplorerTreeView.vb
' Copyright (c) 2025 by Andreas Sauer 
'
' Kurzbeschreibung:
' 
' *************************************************************************************************

Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.VisualStudio.PlatformUI
Imports SchlumpfSoft.Controls.DriveWatcherControl

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Stellt ein Steuerelement zur Anzeige und Navigation der Verzeichnisstruktur des Computers bereit.
    ''' Ermöglicht die Auswahl von Laufwerken, speziellen Ordnern und Unterordnern in einer hierarchischen Baumstruktur.
    ''' </summary>
    Public Class ExplorerTreeView : Inherits UserControl

        Private _SelectedPath As String = String.Empty

        ''' <summary>
        ''' Ereignis, das ausgelöst wird, wenn sich der ausgewählte Pfad ändert.
        ''' Dieses Ereignis wird verwendet, um andere Steuerelemente oder Logik zu benachrichtigen,
        ''' wenn der Benutzer einen anderen Knoten im TreeView auswählt.
        ''' Es ermöglicht eine reaktive Programmierung, bei der andere Teile der Anwendung auf Änderungen im ausgewählten Pfad reagieren können.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Public Event SelectedPathChanged(sender As Object, e As EventArgs)

        ''' <summary>
        ''' Gibt den aktuell ausgewählten Pfad im TreeView zurück.
        ''' Diese Eigenschaft wird aktualisiert, wenn ein Knoten im TreeView ausgewählt wird.
        ''' Sie ermöglicht den Zugriff auf den Pfad des aktuell ausgewählten Knotens,
        ''' was für weitere Operationen wie das Öffnen oder Bearbeiten von Dateien und Ordnern nützlich ist.
        ''' </summary>
        Public ReadOnly Property SelectedPath As String
            Get
                Return _SelectedPath
            End Get
        End Property

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

        ''' <summary>
        ''' Setzt den Wurzelknoten des TreeViews.
        ''' </summary>
        ''' <remarks>
        ''' Der Wurzelknoten ist ein ComputerNode, der die Basis für die Anzeige von Laufwerken und speziellen Ordnern bildet.
        ''' Dieser Knoten wird beim Laden des Steuerelements erstellt und hinzugefügt.
        ''' </remarks>
        Private Sub SetRootNode()
            TV.Nodes.Clear()
            Dim rootnode As New ComputerNode
            rootnode.ImageKey = $"Computer"
            rootnode.SelectedImageKey = $"Computer"
            TV.Nodes.Add(rootnode)
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
        ''' Behandelt das BeforeExpand-Ereignis des TreeViews.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks>
        ''' Dieses Ereignis wird ausgelöst, bevor ein Knoten erweitert wird.
        ''' Abhängig vom Typ des Knotens werden die entsprechenden Unterordner geladen.
        ''' Die verschiedenen Knotentypen (ComputerNode, SpecialFolderNode, DriveNode, FolderNode) werden unterschieden,
        ''' um die passenden Unterordner zu laden und anzuzeigen.
        ''' 
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
        ''' Lädt die Unterordner eines Ordners, wenn der Knoten erweitert wird.
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks>
        ''' Diese Methode wird aufgerufen, wenn ein Ordnerknoten im TreeView erweitert wird.
        ''' Sie leert die vorhandenen Knoten und lädt die Unterordner des Ordners neu.
        ''' 
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
        ''' 
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
        ''' Setzt den aktuell ausgewählten Pfad basierend auf dem ausgewählten Knoten im TreeView.
        ''' </summary>
        ''' <param name="node"></param>
        ''' <remarks>
        ''' Diese Methode wird aufgerufen, wenn ein Knoten im TreeView ausgewählt wird.
        ''' Sie aktualisiert die _SelectedPath-Eigenschaft basierend auf dem Typ des ausgewählten Knotens.
        ''' Dadurch wird sichergestellt, dass der aktuell ausgewählte Pfad immer korrekt ist und andere Teile der Anwendung darauf reagieren können.
        ''' </remarks
        Private Sub SetSelectedPath(node As TreeNode)
            If TV.SelectedNode IsNot Nothing Then
                Select Case TV.SelectedNode.GetType
                    Case GetType(ComputerNode) : _SelectedPath = String.Empty
                    Case GetType(DriveNode) : _SelectedPath = CType(TV.SelectedNode, DriveNode).FullPath
                    Case GetType(SpecialFolderNode) : _SelectedPath = CType(TV.SelectedNode, SpecialFolderNode).FullPath
                    Case GetType(FolderNode) : _SelectedPath = CType(TV.SelectedNode, FolderNode).FullPath
                End Select
            End If
            RaiseEvent SelectedPathChanged(Me, EventArgs.Empty)
        End Sub

        ''' <summary>
        ''' Behandelt das AfterSelect-Ereignis des TreeViews.
        ''' Dieses Ereignis wird ausgelöst, wenn ein Knoten im TreeView ausgewählt wird.
        ''' Es aktualisiert den aktuell ausgewählten Pfad basierend auf dem ausgewählten Knoten.
        ''' 
        ''' Diese Methode ist wichtig, um sicherzustellen, dass der aktuell ausgewählte Pfad immer korrekt ist,
        ''' und ermöglicht anderen Teilen der Anwendung, auf Änderungen im ausgewählten Pfad zu reagieren.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub TV_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TV.AfterSelect
            SetSelectedPath(e.Node)
        End Sub

        Private Sub DW_DriveAdded(sender As Object, e As DriveAddedEventArgs) Handles DW.DriveAdded

#If DEBUG Then
            Debug.Print($"ExplorerTreeView.DriveAdded: Name={e.DriveName} - Typ={e.DriveType} - Format={e.DriveFormat} - Volume={e.VolumeLabel}")
#End If


            'TODO: Das hinzugefügte Laufwerk wird am Ende hinzugefügt (nicht hilfreich)
            TV.Nodes.Item(0).Nodes.Add(New DriveNode(New DriveInfo(e.DriveName)))






        End Sub

        Private Sub DW_DriveRemoved(sender As Object, e As DriveRemovedEventArgs) Handles DW.DriveRemoved
            ' Durchlaufe alle Knoten des Computer-Knotens (Wurzelknoten)
            For Each obj As Object In TV.Nodes.Item(0).Nodes
                ' Überprüfe, ob der aktuelle Knoten ein DriveNode ist
                If TypeOf obj Is DriveNode Then
                    ' Konvertiere den Knoten in einen DriveNode
                    Dim drn As DriveNode = CType(obj, DriveNode)
                    ' Überprüfe, ob der Tag des DriveNode mit dem Namen des entfernten Laufwerks übereinstimmt
                    If drn.Tag.ToString() = e.DriveName Then
                        ' Entferne den DriveNode aus der Liste
                        drn.Remove()
                    End If
                End If
            Next
        End Sub

    End Class

End Namespace