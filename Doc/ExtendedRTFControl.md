# ExtendedRTFControl

Erweiterte RichTextBox für Windows Forms mit komfortablen Formatierungs- und Abfrage-Hilfen (Schriftgröße, Stil-Flags, Farben, Einzüge, Ausrichtung) sowie Redraw-Suppression (flackerreduziertes Batch-Update).

Namespace: `ExtendedRTFControl`  
Hauptklasse: `ExtendedRTF` (erbt von `System.Windows.Forms.RichTextBox`)

- Toolbox: Über Attribute für die Toolbox vorbereitet (`ProvideToolboxControlAttribute`, `ToolboxItem(True)`, `ToolboxBitmap`).
- Mischzustände: Abfragen liefern, wo sinnvoll, `Nothing` (Nullable), wenn die Auswahl uneinheitlich formatiert ist.
- Redraw-Suppression: Internes, verschachtelbares Batching mittels `WM_SETREDRAW` verringert Flackern bei Massenänderungen.
- Ereignis-Steuerung: Interne Scans unterdrücken `SelectionChanged`, um UI-Flackern/Feedback-Schleifen zu vermeiden.

> **Hinweis:** 
>
>Die Konstante `MIN_FONT_SIZE` wird verwendet, muss aber im Projekt definiert sein (z. B. als `Private Const MIN_FONT_SIZE As Single = 6.0F` in der Klasse).
> 

---

## Überblick

`ExtendedRTF` erweitert die Standard-`RichTextBox` um:
- Bequeme Toggle-Methoden für Stil-Flags (Fett, Kursiv, Unterstrichen, Durchgestrichen).
- Einheitliche Abfragen/Setzen von Schriftgröße, Absatz-Einzug (links), Vorder-/Hintergrundfarbe.
- Absatz-Ausrichtung und Bullet-Aufzählung.
- Robuste Erkennung von Mischzuständen in Selektionen (per Zeichen-Scan).
- Flackerreduzierte Batch-Operationen (intern), Font-Reuse-Cache zur Reduzierung von GDI-Handles.

---

## Hinweise zu Leistung und Verhalten

- Per-Zeichen-Operationen: Für Mischzustandserkennung und mehrteilige Stiländerungen müssen Zeichen iteriert werden (WinForms-RTF bietet hierzu keine vollständige Multi-Range-API).
- Redraw-Suppression: Reduziert Flackern bei Massenänderungen spürbar.
- GDI-Handle-Reduktion: Interner Font-Cache bündelt identische Fonts und entsorgt temporäre Instanzen am Blockende.
- Sehr große Selektionen: Per-Zeichen-Scans können spürbar werden; vermeiden Sie unnötig häufige UI-Aktualisierung währenddessen.

---

## Bekannte Einschränkungen

- `SelectionForeColor`/`SelectionBackColor` melden keinen Mischzustand.
- `MIN_FONT_SIZE` muss im Projekt definiert sein; andernfalls wirft `SelectionFontSize` beim Setzen ggf. eine Ausnahme.
- Nur Windows Forms (Windows), Aufrufe vom UI-Thread erforderlich.
- Absatzweite Features (Bullet, Einzug, Alignment) wirken auf Absatzebene; bei Teilselektionen mit mehreren Absätzen können Mischzustände auftreten.
