# ExplorerTreeViewControl

Eine wiederverwendbare WinForms-Komponente zur Anzeige und Navigation der Windows Verzeichnisstruktur (ähnlich dem linken Bereich des Windows Explorers). Unterstützt Laufwerke, spezielle Benutzerordner (Desktop, Dokumente, Downloads, Musik, Bilder, Videos) sowie die rekursive Navigation durch Unterordner. Änderungen am Dateisystem (neue / gelöschte / umbenannte Ordner, Laufwerks-Hotplug) werden dynamisch erkannt.

---

## Ziel & Überblick

Das `ExplorerTreeViewControl` stellt ein benutzerfreundliches Steuerelement zur Verfügung, um Endanwendern die Navigation im Dateisystem bereitzustellen – ohne selbst komplexe Logik für Laufwerksbeobachtung, Ordnerauflistung oder Icon-Zuordnung implementieren zu müssen.

Schwerpunkte:

- Dynamisches Lazy Loading (Ordner erst beim Aufklappen laden)
- Automatische Aktualisierung bei Datei-/Ordneränderungen mittels `FileSystemWatcher`
- Laufwerks-Hotplug-Unterstützung via `DriveWatcherControl`
- Einheitliche Icon-Verwendung über definierte Konstanten
- Ereignisbasierte Rückmeldung des aktuell ausgewählten Pfades

---

## Hauptfunktionen (Features)

Darstellung von:

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

## Architektur & Struktur

Logische Schichten:

- UI-Schicht: `ExplorerTreeView` (UserControl, hostet internes `TreeView` + ggf. unterstützende Komponenten wie ImageList, DriveWatcher)
- Modellierung des Dateisystems über spezialisierte `TreeNode`-Abkömmlinge:
- `ComputerNode`
- `DriveNode`
- `SpecialFolderNode`
- `FolderNode`

Dynamik:

- Erstbefüllung: Root → Platzhalter
- Beim Expand: Platzhalter entfernen, reale Unterstruktur lesen
- FileSystemWatcher: bei Expand anlegen, bei Collapse freigeben
- DriveWatcher: fügt / entfernt `DriveNode`-Instanzen am Root

---

## Ereignisfluss (Event Flow)

1. User expandiert einen Knoten → `TV_BeforeExpand` → `LoadSubfolders(node)`
2. Nach Expand → `TV_AfterExpand` → FileSystemWatcher für Pfad anlegen
3. Collapse → `TV_AfterCollapse` → FileSystemWatcher + Unter-Watcher entfernen
4. Auswahl ändert sich → `TV_AfterSelect` → `SelectedPathChanged` raised
5. FileSystemWatcher feuert (Created / Deleted / Renamed) → `FSW_DirectoryChanged` → Unterordner neu laden
6. DriveWatcher feuert (`DriveAdded` / `DriveRemoved`) → Root-Knoten angepasst

---

## Lebenszyklus & Ressourcenverwaltung

- FileSystemWatcher werden nur für aktuell expandierte Pfade erstellt
- Beim Collapse oder Dispose: Entfernen der Watcher, Entfernen der Handler, `Dispose()` der FileSystemWatcher
- `DriveWatcherControl` (DW) und interne Ressourcen (ImageList, TreeView) werden im `Dispose` freigegeben

---

## Threading & Synchronisation

- `FSW_DirectoryChanged` prüft `InvokeRequired` und marshalt in den UI-Thread, bevor TreeView manipuliert wird
- Laufwerksereignisse (DriveWatcher) werden direkt im UI-Kontext erwartet (falls nicht, entsprechende Invoke-Strategie ergänzbar)

---

## Performance-Aspekte

- Lazy Loading reduziert Startzeit und Speicherbedarf
- Minimierte Anzahl aktiver FileSystemWatcher (nur expandierte Ebenen)
- Keine tiefe Rekursion über komplette Laufwerksbäume
- Ereignisbasierter Refresh statt permanenter Polling-Mechanismen

Optimierungspotenzial:

- Caching von bereits eingelesenen Strukturen (optional)
- Asynchrones Vorladen im Hintergrund (Task-basierte Erweiterung)

---

## Fehler- & Ausnahmebehandlung

- Datei-/IO-Zugriffe in Node-Ladevorgängen mit `Try/Catch` abgesichert (Unterdrückung bei `UnauthorizedAccessException`, `IOException`)
- Fehler beim Anlegen von FileSystemWatcher werden geloggt (`Debug.WriteLine`) statt Exception-Fluss nach außen
- Robust gegen temporär nicht verfügbare Laufwerke

Empfehlung bei Integration: Optional Logging-Interface injizierbar machen.

---

## Erweiterbarkeit (Extension Points)

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

## Bekannte Einschränkungen

- Keine Anzeige von Dateien (nur Ordnerstruktur)
- Keine Fehlerdialoge (Silent-Failure bei Access-Denied)
- Keine Unterscheidung symbolischer Links / Junctions
- Spezialordner-Namen in Deutsch fest verdrahtet
- Keine direkte Shell-Integration (Kontextmenüs, Spezialicons, Overlays)

---

## Mögliche Erweiterungen (Roadmap)

- Optionaler Datei-Knotenmodus
- Unterstützung für Favoriten / zuletzt verwendete Ordner
- Suchfunktion innerhalb der Baumstruktur
- Shell32 / Windows API für echte Explorer-Icons
- Optionale Persistenz (zuletzt geöffnete Knoten rekonstruieren)
- Virtuelle Knoten (Cloud Provider / Remote Quellen)

---

## Qualitätsaspekte & Coding-Guidelines

- Konsistente Benennung: Deutsche Beschreibungen + klare Semantik
- Aufteilung in kleine, klar fokussierte Helper-Methoden
- Vermeidung von duplizierter Logik (z. B. zentrale Mappings in Dictionaries)
- Ressourcen-Freigabe sauber über `Dispose`
- UI-Thread-Sicherheit durch `Invoke`-Prüfung

Empfehlungen für Weiterentwicklung:

- Integration einer Logging-Abstraktion
- Analyse mittels Code-Analyse / Roslyn-Regeln

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
