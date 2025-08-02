# ExplorerTreeView Control

## Übersicht

Die `ExplorerTreeView`-Klasse stellt ein spezialisiertes Steuerelement zur Anzeige und Navigation der Verzeichnisstruktur des Computers bereit.

**Namespace:** `SchlumpfSoft.Controls.ExplorerTreeViewControl`  
**Basisklasse:** `UserControl`

---

## Konstruktor

### New()

**Beschreibung:** Initialisiert das ExplorerTreeView-Steuerelement. Lädt die erforderlichen Bilder und setzt den Wurzelknoten des TreeViews.

**Funktionalität:**
- Aufruf von `InitializeComponent()`
- Laden der Icons für verschiedene Knotentypen
- Erstellen und Setzen des Computer-Wurzelknotens

---

## Ereignisse (Events)

### SelectedPathChanged

**Beschreibung:** Wird ausgelöst, wenn sich der ausgewählte Pfad ändert. Ermöglicht reaktive Programmierung für andere Anwendungsteile.

**Verwendung:** Ermöglicht es anderen Steuerelementen oder Logikteilen, auf Pfadänderungen zu reagieren.

**Browsable:** `True` (im Designer sichtbar)

---

## Eigenschaften (Properties)

### SelectedPath (ReadOnly)

**Beschreibung:** Gibt den vollständigen Pfad des ausgewählten Knotens zurück.

**Kategorie:** Nicht im Designer verfügbar (`Browsable(False)`)

**Rückgabe:** Vollständiger Pfad als String oder leerer String für Computer-Knoten

### LineColor

**Beschreibung:** Bestimmt die Farbe der Linien zwischen den Knoten im TreeView.

**Kategorie:** Behavior

**Get/Set:** Direkte Weiterleitung an das interne TreeView-Steuerelement

### ShowLines

**Beschreibung:** Bestimmt, ob Linien zwischen den Knoten angezeigt werden.

**Kategorie:** Behavior

**Standardwert:** Abhängig von TreeView-Standardeinstellungen

### ShowPlusMinus

**Beschreibung:** Bestimmt, ob Plus- und Minuszeichen zum Erweitern/Reduzieren von Knoten angezeigt werden.

**Kategorie:** Behavior

**Funktionalität:** Steuert die Sichtbarkeit der Expand/Collapse-Symbole

### ShowRootLines

**Beschreibung:** Bestimmt, ob Linien zwischen den Stammknoten angezeigt werden.

**Kategorie:** Behavior

**Anwendung:** Beeinflusst die visuelle Darstellung auf oberster Ebene

### Indent

**Beschreibung:** Bestimmt den Abstand für das Einrücken der einzelnen Ebenen von untergeordneten Knoten.

**Kategorie:** Behavior

**Einheit:** Pixel

**Verwendung:** Steuert die hierarchische Darstellung der Ordnerstruktur

### ItemHeight

**Beschreibung:** Bestimmt die Höhe der einzelnen Knoten im TreeView.

**Kategorie:** Appearance

**Einheit:** Pixel

**Anwendung:** Beeinflusst die Zeilenhöhe der Baumstruktur

---

## Überschriebene Eigenschaften (Overrides)

### BackColor

**Beschreibung:** Bestimmt die Hintergrundfarbe des Steuerelements.

**Kategorie:** Appearance

**Besonderheit:** Wird sowohl auf das UserControl als auch auf das interne TreeView angewandt

### ForeColor

**Beschreibung:** Bestimmt die Vordergrundfarbe für die Textanzeige.

**Kategorie:** Appearance

**Besonderheit:** Wird sowohl auf das UserControl als auch auf das interne TreeView angewandt

### Font

**Beschreibung:** Bestimmt die Schriftart für den Text im Steuerelement.

**Kategorie:** Appearance

**Besonderheit:** Wird sowohl auf das UserControl als auch auf das interne TreeView angewandt

---

## Ausgeblendete Eigenschaften

### BackgroundImage

**Beschreibung:** Ausgeblendet, da für dieses Steuerelement nicht relevant.

**Sichtbarkeit:** `Browsable(False)`, `EditorBrowsable(EditorBrowsableState.Never)`

### BackgroundImageLayout

**Beschreibung:** Ausgeblendet, da für dieses Steuerelement nicht relevant.

**Sichtbarkeit:** `Browsable(False)`, `EditorBrowsable(EditorBrowsableState.Never)`

---

## Funktionale Merkmale

### Automatische Aktualisierung
Das Steuerelement überwacht Dateisystemänderungen automatisch und aktualisiert die Anzeige entsprechend.

### Lazy Loading
Ordnerinhalte werden erst geladen, wenn der entsprechende Knoten erweitert wird.

### Laufwerk-Überwachung
Neue und entfernte Laufwerke werden automatisch erkannt und in der Baumstruktur reflektiert.

### Spezialordner-Unterstützung
Unterstützt Windows-Spezialordner wie Desktop, Dokumente, Downloads, Musik und Bilder.

### Thread-Sicherheit
Alle UI-Updates werden thread-sicher im korrekten UI-Thread ausgeführt.

---

## Verwendungsbeispiel

```<vb>
' Steuerelement erstellen 
Dim explorerTree As New ExplorerTreeView()

' Event-Handler für Pfadänderungen 
AddHandler explorerTree.SelectedPathChanged, AddressOf OnPathChanged

' Eigenschaften konfigurieren 
explorerTree.ShowLines = True 
explorerTree.ShowPlusMinus = True 
explorerTree.LineColor = Color.Gray 
explorerTree.ItemHeight = 20

' Ausgewählten Pfad abrufen 
Dim currentPath As String = explorerTree.SelectedPath

Private Sub OnPathChanged(sender As Object, e As EventArgs) 
	Dim tree As ExplorerTreeView = CType(sender, ExplorerTreeView) 
	Console.WriteLine($"Neuer Pfad: {tree.SelectedPath}") 
End Sub
```
---

## Besonderheiten

- Das Steuerelement arbeitet mit verschiedenen Knotentypen (`ComputerNode`, `DriveNode`, `SpecialFolderNode`, `FolderNode`)
- Automatische Ressourcenverwaltung für FileSystemWatcher
- Alphabetische Sortierung von Laufwerken beim Hinzufügen/Entfernen
- Unterstützung für alle gängigen Laufwerkstypen (System, Fixed, CD-ROM, Removable, Network, etc.)

Das `ExplorerTreeView`-Steuerelement bietet eine vollständige Lösung für die Dateisystem-Navigation mit Windows Explorer-ähnlicher Funktionalität.

---

## geplante Ergänzungen und Änderungen

- Die Funktion ExpandPath hinzufügen
- ~~Eine Funktion implementieren die bei Änderungen in der Ordnerstruktur die Ansicht des Controls aktualisiert.~~

---

## Weitere Literatur

- [Erstellen eines Windows Forms-Toolbox-Steuerelements](https://docs.microsoft.com/de-de/visualstudio/extensibility/creating-a-windows-forms-toolbox-control?view=vs-2022)
- [Control-Techniken: Eigenes Toolboxicon für Steuerelement](https://www.vb-paradise.de/index.php/Thread/123746-Control-Techniken-Eigenes-Toolboxicon-f%C3%BCr-Steuerelement/)
- [ExpTreeLib Version 3 - Explorer-like Navigation and Operation for your Forms](https://www.codeproject.com/Articles/422497/ExpTreeLib-Version-3-Explorer-like-Navigation-and)
- [VB - Explorer TreeView für VB.Net](https://dotnet-snippets.de/snippet/explorer-treeview-fuer-vb-net/468)
- [Introduction to TreeView Drag and Drop (VB.NET)](https://www.codeproject.com/Articles/8995/Introduction-to-TreeView-Drag-and-Drop-VB-NET)
- [TreeView/Nodes/dynamisch hinzufügen](https://www.vb-paradise.de/index.php/Thread/121678-TreeView-Nodes-dynamisch-hinzuf%C3%BCgen/)
- [TreeView Klasse](https://learn.microsoft.com/de-de/dotnet/api/system.windows.forms.treeview?view=netframework-4.7.2)
- [Vorgehensweise: Hinzufügen oder Entfernen von Knoten mit dem TreeView-Steuerelement in Windows Forms](https://learn.microsoft.com/de-de/dotnet/desktop/winforms/controls/how-to-add-and-remove-nodes-with-the-windows-forms-treeview-control?view=netframeworkdesktop-4.8)
- [Vorgehensweise: Festlegen von Symbolen für das TreeView-Steuerelement in Windows Forms](https://learn.microsoft.com/de-de/dotnet/desktop/winforms/controls/how-to-set-icons-for-the-windows-forms-treeview-control?view=netframeworkdesktop-4.8)
- [Vorgehensweise: Hinzufügen von benutzerdefinierten Daten zu einem TreeView- oder ListView-Steuerelement (Windows Forms)](https://learn.microsoft.com/de-de/dotnet/desktop/winforms/controls/add-custom-information-to-a-treeview-or-listview-control-wf?view=netframeworkdesktop-4.8)
