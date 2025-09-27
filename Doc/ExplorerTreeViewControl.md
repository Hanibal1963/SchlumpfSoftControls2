# ExplorerTreeViewControl

Eine wiederverwendbare WinForms-Komponente zur Anzeige und Navigation der Windows Verzeichnisstruktur (ähnlich dem linken Bereich des Windows Explorers). Unterstützt Laufwerke, spezielle Benutzerordner (Desktop, Dokumente, Downloads, Musik, Bilder, Videos) sowie die rekursive Navigation durch Unterordner. Änderungen am Dateisystem (neue / gelöschte / umbenannte Ordner, Laufwerks-Hotplug) werden dynamisch erkannt.

---

## Inhaltsverzeichnis

1. [Ziel & Überblick](#1-ziel--überblick)
2. [Hauptfunktionen](#2-hauptfunktionen-features)
3. [Architektur & Struktur]
4. [Kernklassen & Module](#4-kernklassen--module)
    - [ExplorerTreeView](#41-explorertreeview)
    - [Node-Klassen](#42-node-klassen)
    - [Helper](#43-helper)
    - [Ereignisargs](#44-ereignisargs)
5. [Öffentliche API (Properties / Methoden / Events)](#5-öffentliche-api)
    - [Eigenschaften (Properties)](#51-eigenschaften-properties)
    - [Methoden (Methods)](#52-methoden-methods)
    - [Ereignisse (Events)](#53-ereignisse-events)
    - [Lebenszyklus (Lifecycle)](#54-lebenszyklus-lifecycle)
6. [Ereignisfluss (Event Flow)](#6-ereignisfluss-event-flow)
7. [Lebenszyklus & Ressourcenverwaltung](#7-lebenszyklus--ressourcenverwaltung)
8. [Threading & Synchronisation](#8-threading--synchronisation)
9. [Performance-Aspekte](#9-performance-aspekte)
10. [Fehler- & Ausnahmebehandlung](#10-fehler--ausnahmebehandlung)
11. [Einsatz / Integration im Projekt](#11-einsatz--integration-im-projekt)
12. [Beispielcode](#12-beispielcode)
    - [Einfaches Formular](#121-einfaches-formular)
    - [Dynamische Reaktion](#122-dynamische-reaktion)
    - [Fehlerrobuster Expand-Aufruf](#123-fehlerrobuster-expand-aufruf)
13. [Erweiterbarkeit (Extension Points)](#13-erweiterbarkeit-extension-points)
14. [Bekannte Einschränkungen](#14-bekannte-einschränkungen)
15. [Mögliche Erweiterungen (Roadmap-Ideen)](#15-mögliche-erweiterungen-roadmap)
16. [Qualitätsaspekte & Coding-Guidelines](#16-qualitätsaspekte--coding-guidelines)
17. [Anhang: Interne Methode / Responsibility Map (Kurzreferenz)](#17-anhang-interne-methode--responsibility-map-kurzreferenz)
18. [Kurze FAQ](#18-kurze-faq)
19. [Weitere Literatur](#19-weitere-literatur)

---

<a name="1-ziel--überblick"></a>
## 1. Ziel & Überblick

Das `ExplorerTreeViewControl` stellt ein benutzerfreundliches Steuerelement zur Verfügung, um Endanwendern die Navigation im Dateisystem bereitzustellen – ohne selbst komplexe Logik für Laufwerksbeobachtung, Ordnerauflistung oder Icon-Zuordnung implementieren zu müssen.

Schwerpunkte:

- Dynamisches Lazy Loading (Ordner erst beim Aufklappen laden)
- Automatische Aktualisierung bei Datei-/Ordneränderungen mittels `FileSystemWatcher`
- Laufwerks-Hotplug-Unterstützung via `DriveWatcherControl`
- Einheitliche Icon-Verwendung über definierte Konstanten
- Ereignisbasierte Rückmeldung des aktuell ausgewählten Pfades

---

<a name="2-hauptfunktionen-features"></a>
## 2. Hauptfunktionen (Features)

- Darstellung von:
- 
  - "Dieser Computer" als Wurzel
  - Lokale / Wechsel / Netzwerk / RAM / CD / (System-)Laufwerke
  - Spezielle Benutzerordner (Desktop, Dokumente, Downloads, Musik, Bilder, Videos)
- Automatische Sortierung & Einfügen neu angeschlossener Laufwerke
- Entfernen von Laufwerken in Echtzeit
- On-Demand Laden von Unterordnern
- Ereignis `SelectedPathChanged` bei Auswahländerung
- Optionale visuelle Anpassung (Linienfarbe, Schrift, Farben, Einrückung, etc.)
- Ressourcen-schonend: FileSystemWatcher nur für geöffnete (expandierte) Pfade

---

<a name="3-architektur--struktur"></a>
## 3. Architektur & Struktur

Logische Schichten:

- UI-Schicht: `ExplorerTreeView` (UserControl, hostet internes `TreeView` + ggf. unterstützende Komponenten wie ImageList, DriveWatcher)
- Modellierung des Dateisystems über spezialisierte `TreeNode`-Abkömmlinge:
- 
  - `ComputerNode`
  - `DriveNode`
  - `SpecialFolderNode`
  - `FolderNode`
  
- Helper-/Utility-Module zur Kapselung wiederkehrender Aufgaben:

  - `ExplorerTreeViewHelpers` (Pfad-Segmentierung, Node-Suche, Pfadermittlung)
  - `NodeHelpers` (Icon-/Pfad-/Typ-Ermittlung)
  - Konstanten-Module (DriveType-, Icon-, Folder-Namenszuordnung)
- Ereignisdatentransfer: `SelectedPathChangedEventArgs`

Dynamik:

- Erstbefüllung: Root → Platzhalter
- Beim Expand: Platzhalter entfernen, reale Unterstruktur lesen
- FileSystemWatcher: bei Expand anlegen, bei Collapse freigeben
- DriveWatcher: fügt / entfernt `DriveNode`-Instanzen am Root

---

<a name="4-kernklassen--module"></a>
## 4. Kernklassen & Module

<a name="41-explorertreeview"></a>
### 4.1 `ExplorerTreeView`

Zentrale Steuerelementklasse. Verwaltet TreeView, Ereignisse, FileSystemWatcher-Dictionary, Styling und Öffentliche API.

<a name="42-node-klassen"></a>
### 4.2 Node-Klassen

- `ComputerNode`: Root-Knoten, lädt SpecialFolders + Drives
- `DriveNode`: Einzelnes Laufwerk, lädt erste Ordnerebene
- `SpecialFolderNode`: Windows SpecialFolder (z. B. Desktop) → lädt Unterordner
- `FolderNode`: Allgemeiner Verzeichnis-Knoten

Alle Node-Klassen arbeiten mit Lazy Loading (Platzhalterknoten) zur Performance-Optimierung.

<a name="43-helper"></a>
### 4.3 Helper

- `ExplorerTreeViewHelpers`: `GetPathSegments`, `FindNodeByPath`, `GetDirectoryPath`
- `NodeHelpers`: Laufwerks-/Ordner-Logik (Label, Typ, Bildschlüssel, Spezialpfade)
- Konstanten-Module: Trennen Magic Strings / Keys von Logik

<a name="44-ereignisargs"></a>
### 4.4 Ereignisargs

- `SelectedPathChangedEventArgs`: Liefert den aktuellen Pfad, kann leer sein (z. B. Root)

---

<a name="5-öffentliche-api"></a>
## 5. Öffentliche API

<a name="51-eigenschaften-properties"></a>
### 5.1 Eigenschaften (Public)

- `LineColor As Color`: Linienfarbe im TreeView
- `ShowLines As Boolean`: Anzeige von Verbindungs-Linien
- `ShowPlusMinus As Boolean`: Anzeige Plus/Minus-Indikatoren
- `ShowRootLines As Boolean`: Linien zwischen Root-Knoten
- `Indent As Integer`: Einrückung pro Ebene
- `ItemHeight As Integer`: Knoten-Höhe
- `BackColor As Color`: Hintergrundfarbe (überschrieben + synchron aufs interne TreeView)
- `ForeColor As Color`: Textfarbe (dito)
- `Font As Font`: Schriftart (dito)

Ausgeblendete (nicht relevant für Control-Funktion): `Text`, `BackgroundImage`, `BackgroundImageLayout` (Browsable False / EditorBrowsable Never)

<a name="52-methoden-methods"></a>
### 5.2 Methoden (Public)

- `ExpandPath(path As String) As Boolean`
  - Öffnet rekursiv Knoten bis zum angegebenen Pfad (falls vorhanden)
  - Rückgabe: True bei Erfolg, sonst False

  <a name="53-ereignisse-events"></a>
### 5.3 Ereignisse

- `SelectedPathChanged(sender, e As SelectedPathChangedEventArgs)`
  - Ausgelöst nach Auswahl eines Knotens
  - `e.SelectedPath` = leer bei Root oder vollständiger Pfad bei Drive/Special/Ordner

<a name="54-lebenszyklus-lifecycle"></a>
### 5.4 Lebenszyklus

- `Dispose(disposing As Boolean)` überschrieben: FileSystemWatcher + interne Komponenten werden freigegeben

---

<a name="6-ereignisfluss-event-flow"></a>
## 6. Ereignisfluss (Event Flow)

1. User expandiert einen Knoten → `TV_BeforeExpand` → `LoadSubfolders(node)`
2. Nach Expand → `TV_AfterExpand` → FileSystemWatcher für Pfad anlegen
3. Collapse → `TV_AfterCollapse` → FileSystemWatcher + Unter-Watcher entfernen
4. Auswahl ändert sich → `TV_AfterSelect` → `SelectedPathChanged` raised
5. FileSystemWatcher feuert (Created / Deleted / Renamed) → `FSW_DirectoryChanged` → Unterordner neu laden
6. DriveWatcher feuert (`DriveAdded` / `DriveRemoved`) → Root-Knoten angepasst

---

<a name="7-lebenszyklus--ressourcenverwaltung"></a>
## 7. Lebenszyklus & Ressourcenverwaltung

- FileSystemWatcher werden nur für aktuell expandierte Pfade erstellt
- Beim Collapse oder Dispose: Entfernen der Watcher, Entfernen der Handler, `Dispose()` der FileSystemWatcher
- `DriveWatcherControl` (DW) und interne Ressourcen (ImageList, TreeView) werden im `Dispose` freigegeben

---

<a name="8-threading--synchronisation"></a>
## 8. Threading & Synchronisation

- `FSW_DirectoryChanged` prüft `InvokeRequired` und marshalt in den UI-Thread, bevor TreeView manipuliert wird
- Laufwerksereignisse (DriveWatcher) werden direkt im UI-Kontext erwartet (falls nicht, entsprechende Invoke-Strategie ergänzbar)

---

<a name="9-performance-aspekte"></a>
## 9. Performance-Aspekte

- Lazy Loading reduziert Startzeit und Speicherbedarf
- Minimierte Anzahl aktiver FileSystemWatcher (nur expandierte Ebenen)
- Keine tiefe Rekursion über komplette Laufwerksbäume
- Ereignisbasierter Refresh statt permanenter Polling-Mechanismen

Optimierungspotenzial:

- Caching von bereits eingelesenen Strukturen (optional)
- Asynchrones Vorladen im Hintergrund (Task-basierte Erweiterung)

---

<a name="10-fehler--ausnahmebehandlung"></a>
## 10. Fehler- & Ausnahmebehandlung

- Datei-/IO-Zugriffe in Node-Ladevorgängen mit `Try/Catch` abgesichert (Unterdrückung bei `UnauthorizedAccessException`, `IOException`)
- Fehler beim Anlegen von FileSystemWatcher werden geloggt (`Debug.WriteLine`) statt Exception-Fluss nach außen
- Robust gegen temporär nicht verfügbare Laufwerke

Empfehlung bei Integration: Optional Logging-Interface injizierbar machen.

---

<a name="11-einsatz--integration-im-projekt"></a>
## 11. Einsatz / Integration im Projekt

1. Projekt `ExplorerTreeViewControl` referenzieren (oder DLL kompilieren und referenzieren)
2. Control erscheint im Toolbox-Bereich (Attribut `[ProvideToolboxControl("SchlumpfSoft Controls", False)]`)
3. Auf ein Form ziehen
4. Ereignis binden:

   ```vb
   Private Sub ExplorerTreeView1_SelectedPathChanged(sender As Object, e As SelectedPathChangedEventArgs) _
       Handles ExplorerTreeView1.SelectedPathChanged
       LabelAktuellerPfad.Text = If(String.IsNullOrWhiteSpace(e.SelectedPath), "<Root>", e.SelectedPath)
   End Sub
   ```
5. Optional: Styling-Eigenschaften setzen (z. B. BackColor, Font)
6. Pfad programmatisch öffnen (falls z. B. Startpfad gewünscht):

   ```vb
   ExplorerTreeView1.ExpandPath("C:\\Users\\<Name>\\Documents")
   ```

---

<a name="12-beispielcode"></a>
## 12. Beispielcode

<a name="121-einfaches-formular"></a>
### 12.1 Einfaches Formular

```vb
Public Class FrmExplorerDemo

    Private Sub FrmExplorerDemo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Optional: Startpfad expandieren
        ExplorerTreeView1.ExpandPath("C:\\")
    End Sub

    Private Sub ExplorerTreeView1_SelectedPathChanged(sender As Object, e As SelectedPathChangedEventArgs) _
        Handles ExplorerTreeView1.SelectedPathChanged
        TxtPath.Text = If(String.IsNullOrEmpty(e.SelectedPath), "Root (Dieser Computer)", e.SelectedPath)
    End Sub

End Class
```

<a name="122-dynamische-reaktion"></a>
### 12.2 Dynamische Reaktion

```vb
Private Sub ExplorerTreeView1_SelectedPathChanged(sender As Object, e As SelectedPathChangedEventArgs) _
    Handles ExplorerTreeView1.SelectedPathChanged
    If Directory.Exists(e.SelectedPath) Then
        FileListBox.Items.Clear()
        For Each f In Directory.GetFiles(e.SelectedPath)
            FileListBox.Items.Add(Path.GetFileName(f))
        Next
    End If
End Sub
```

<a name="123-fehlerrobuster-expand-aufruf"></a>
### 12.3 Fehlerrobuster Expand-Aufruf

```vb
If Not ExplorerTreeView1.ExpandPath(userPfad) Then
    MessageBox.Show($"Pfad nicht gefunden: {userPfad}")
End If
```

---

<a name="13-erweiterbarkeit-extension-points"></a>
## 13. Erweiterbarkeit (Extension Points)

| Bereich | Idee |
|--------|------|
| Icons | Zusätzliche ImageKeys / dynamische Icon-Auflösung via Shell API |
| Kontextmenü | Rechtsklick-Kontextaktionen (Öffnen, Löschen, Eigenschaften) |
| Drag & Drop | Datei-/Ordneroperationen ermöglichen |
| Filter | Anzeige bestimmter Ordner einschränken (Whitelist/Blacklist) |
| Mehrfachauswahl | Erweiterung auf `TreeView.CheckBoxes` + Aggregation |
| Caching | Wiederverwendung bereits geladener Unterstrukturen |
| Asynchronität | Async-Laden großer Verzeichnisse |
| Internationalisierung | Ressourcen für UI-Texte (Platzhalterknoten) |

---

<a name="14-bekannte-einschränkungen"></a>
## 14. Bekannte Einschränkungen

- Keine Anzeige von Dateien (nur Ordnerstruktur)
- Keine Fehlerdialoge (Silent-Failure bei Access-Denied)
- Keine Unterscheidung symbolischer Links / Junctions
- Spezialordner-Namen in Deutsch fest verdrahtet
- Keine direkte Shell-Integration (Kontextmenüs, Spezialicons, Overlays)

---

<a name="15-mögliche-erweiterungen-roadmap"></a>
## 15. Mögliche Erweiterungen (Roadmap)

- Optionaler Datei-Knotenmodus
- Unterstützung für Favoriten / zuletzt verwendete Ordner
- Suchfunktion innerhalb der Baumstruktur
- Shell32 / Windows API für echte Explorer-Icons
- Optionale Persistenz (zuletzt geöffnete Knoten rekonstruieren)
- Virtuelle Knoten (Cloud Provider / Remote Quellen)

---

<a name="16-qualitätsaspekte--coding-guidelines"></a>
## 16. Qualitätsaspekte & Coding-Guidelines

- Konsistente Benennung: Deutsche Beschreibungen + klare Semantik
- Aufteilung in kleine, klar fokussierte Helper-Methoden
- Vermeidung von duplizierter Logik (z. B. zentrale Mappings in Dictionaries)
- Ressourcen-Freigabe sauber über `Dispose`
- UI-Thread-Sicherheit durch `Invoke`-Prüfung

Empfehlungen für Weiterentwicklung:

- Unit-Tests für Helper-Module (`NodeHelpers`, `ExplorerTreeViewHelpers`)
- Integration einer Logging-Abstraktion
- Analyse mittels Code-Analyse / Roslyn-Regeln

---

<a name="17-anhang-interne-methode--responsibility-map-kurzreferenz"></a>
## 17. Anhang: Interne Methode / Responsibility Map (Kurzreferenz)

| Element | Aufgabe |
|---------|--------|
| `ExplorerTreeView.SetRootNode()` | Root initialisieren |
| `ExplorerTreeView.LoadImages()` | ImageList befüllen |
| `ExplorerTreeView.LoadSubfolders(node)` | Lazy Loading je nach Knotentyp |
| `ExplorerTreeView.CreateFileSystemWatcher(path)` | Watcher anlegen |
| `ExplorerTreeView.RemoveFileSystemWatchers(path)` | Watcher entfernen (rekursiv) |
| `ExplorerTreeView.FSW_DirectoryChanged` | Refresh betroffener Knoten |
| `ExplorerTreeView.ExpandPath(path)` | Pfad selektiv expandieren & selektieren |
| `DriveNode.LoadSubfolders()` | 1. Ebene eines Laufwerks laden |
| `FolderNode.LoadSubfolders()` | Unterordner laden |
| `SpecialFolderNode.LoadSubfolders()` | Unterordner Spezialordner |
| `ComputerNode.LoadSpecialFolders()` | Desktop / Dokumente / usw. |
| `ComputerNode.LoadDrives()` | Systemlaufwerke einlesen |
| `NodeHelpers.*` | Mappings (Icons, Pfade, Typen) |
| `ExplorerTreeViewHelpers.*` | Suche / Pfadsegmentierung |

---

<a name="18-kurze-faq"></a>
## 18. Kurze FAQ
**Warum werden manche Ordner nicht angezeigt?**
Zugriff evtl. verweigert (Permissions) – still unterdrückt.

**Warum werden keine Dateien angezeigt?**
Designentscheidung: Fokus auf Ordnernavigation. Erweiterbar.

**Wie erkenne ich das Root-Level?**
`SelectedPathChangedEventArgs.SelectedPath` ist leer.

**Wie verhindere ich FileSystemWatcher-Last?**
Nicht benötigte Knoten einklappen (Watcher werden entfernt).

---

<a name="19-weitere-literatur"></a>
## 19. Weitere Literatur

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
