# ExplorerTreeView Control

Das Steuerelement ist ein spezielles TreeView Control, dass die Struktur des Verzeichnisbaums des Windows Explorers nachahmt. 

Es zeigt die Ordnerstruktur und Laufwerke in einem Baumdiagramm an. Das Control kann verwendet werden, um Laufwerke und Ordner in einem Verzeichnisbaum anzuzeigen.

---

## Einführung

Diese Steuerelement habe ich für ein anderes Projekt entwickelt um dessen Code zu vereinfachen. 

---

## neue Eigenschaften Funktionen und Ereignisse

<details>
<summary>Eigenschaften</summary>

- **SelectedPath** - Gibt den vollständigen Pfad des ausgewählten Knotens zurück.
- **LineColor** - Gibt die Farbe der Linien zwischen den Knoten zurück oder legt diese fest.
- **ShowLines** - Gibt an, ob Linien zwischen den Knoten angezeigt werden.
- **ShowPlusMinus** - Legt fest ob die Plus- und Minuszeichen zum Anzeigen von Unterknoten angezeigt werden.
- **ShowRootLines** - Gibt an, ob Linien zwischen den Stammknoten angezeigt werden.
- **ItemHeight** - Ruft die Höhe des jeweiligen Strukturknotens im Strukturansicht-Steuerelement ab oder legt diese fest.
- **ForeColor** - Legt die Vordergrundfarbe für das Anzeigen von Text fest oder gibt diese zurück.
- **BackColor** - Legt die Hintergrundfarbe für das Steuerelement fest oder gibt diese zurück.
- **Indent** - Ruft den Abstand für das Einrücken der einzelnen Ebenen von untergeordneten Strukturknoten ab oder legt diesen fest.
- **Font** - Legt die Schriftart des Textes im Steuerelement fest oder gibt diese zurück.

</details>

<details> 
<summary>Funktionen</summary>

- **ExpandPath** - Öffnet den angegebenen Pfad im TreeView-Steuerelement.

</details>

<details>
<summary>Ereignisse</summary>

- **SelectedPathChanged** - Wird ausgelöst, wenn sich der ausgewählte Pfad geändert hat.

</details>

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


