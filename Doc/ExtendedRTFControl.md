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

## Inhaltsverzeichnis

1. [Überblick](#overview)
2. [Installation und Einbindung](#installation)
3. [Schnellstart](#quickstart)
4. [API-Referenz](#api)
    - [Methoden](#methods)
    - [Eigenschaften](#properties) 
5. [Mischzustände (Tri-State-Logik)](#tri-state)
6. [Redraw-Suppression und Ereignisverhalten](#redraw-suppression)
7. [Beispiele](#examples)
    - [Toolbar-Buttons an Mischzustände koppeln](#example-code)
    - [Schriftgröße setzen mit Validierung](#example-fontsize)
    - [Absatz-Ausrichtung und Bullets](#example-paragraph)
    - [Formatierungen gezielt löschen](#example-clearformatting)
8. [Hinweise zu Leistung und Verhalten](#performance)
9. [Bekannte Einschränkungen](#limitations)

---

<a name="overview"></a>
## 1. Überblick

`ExtendedRTF` erweitert die Standard-`RichTextBox` um:
- Bequeme Toggle-Methoden für Stil-Flags (Fett, Kursiv, Unterstrichen, Durchgestrichen).
- Einheitliche Abfragen/Setzen von Schriftgröße, Absatz-Einzug (links), Vorder-/Hintergrundfarbe.
- Absatz-Ausrichtung und Bullet-Aufzählung.
- Robuste Erkennung von Mischzuständen in Selektionen (per Zeichen-Scan).
- Flackerreduzierte Batch-Operationen (intern), Font-Reuse-Cache zur Reduzierung von GDI-Handles.

---

<a name="installation"></a>
## 2.  Installation und Einbindung

1. Projekt/Assembly referenzieren, das `ExtendedRTFControl` enthält.
2. Visual Studio Toolbox:
   - Durch die vorhandenen Attribute wird das Control i. d. R. automatisch unter der Toolbox-Kategorie „SchlumpfSoft Controls“ geführt.
   - Alternativ per Rechtsklick in der Toolbox „Elemente auswählen…“ und die Assembly manuell hinzufügen.
3. Namespace importieren:
   - VB: `Imports ExtendedRTFControl`
   - C#: `using ExtendedRTFControl;`

---

<a name="quickstart"></a>
## 3. Schnellstart

```vbnet
Dim rtf As New ExtendedRTFControl.ExtendedRTF() With { .Dock = DockStyle.Fill } Me.Controls.Add(rtf)
rtf.Text = "Hallo Welt" 
rtf.Select(0, 5) 
rtf.ToggleBold() 
rtf.SelectionFontSize = 14.0F 
rtf.SelectionForeColor = Color.DarkBlue 
rtf.SetSelectionAlignment(HorizontalAlignment.Center)
```

WinForms-Designer:
- Control aus der Toolbox („SchlumpfSoft Controls“) auf ein Formular ziehen.
- Eigenschaften wie gewohnt über Eigenschaftenfenster oder im Code anpassen.

---

<a name="api"></a>
## 4. API-Referenz

Die folgenden öffentlichen API-Elemente sind zusätzlich zur Basis-`RichTextBox` verfügbar.

<a name="methods"></a>
### 4.1. Methoden

- `Sub ClearFormatting()`
  - Entfernt Formatierungen (Stil, Vorder-/Hintergrundfarbe, Bullet) aus aktueller Auswahl oder am Caret.
  - Optimiert: Einmalige Normalisierung der Auswahl statt per Zeichen bei fehlender Selektion.

- `Sub SetSelectionAlignment(alignment As HorizontalAlignment)`
  - Setzt die horizontale Ausrichtung für Absatz/Absätze in der Auswahl (Links, Zentriert, Rechts).

- `Sub ToggleBold()`  
  `Sub ToggleItalic()`  
  `Sub ToggleUnderline()`  
  `Sub ToggleStrikeout()`
  - Schaltet jeweilige Stil-Flags für Auswahl oder Caret um.
  - Berücksichtigen Mischzustände mittels Nullable-Logik.

- `Sub ToggleBullet()`
  - Schaltet Bullet-Aufzählung auf Absatzebene um.
  - Hinweis: Wirkt absatzweise; bei `SelectionLength = 0` auf aktuellen Absatz.

  <a name="properties"></a>
### 4.2. Eigenschaften

- `SelectionFontSize As Nullable(Of Single)`
  - Liest/Setzt die Schriftgröße der Auswahl bzw. am Caret.
  - Rückgabe `Nothing` bei Mischzustand in der Auswahl.
  - Setzen: wirft `ArgumentOutOfRangeException`, wenn `< MIN_FONT_SIZE`.

- `SelectionBold As Nullable(Of Boolean)`  
  `SelectionItalic As Nullable(Of Boolean)`  
  `SelectionUnderline As Nullable(Of Boolean)`  
  `SelectionStrikeout As Nullable(Of Boolean)`
  - Liest/Setzt den jeweiligen Stilstatus.
  - Rückgabe `Nothing` bei Mischzustand in der Auswahl.

- `SelectionForeColor As Color`
  - Liest/Setzt die Textfarbe.
  - Meldet aktuell keinen Mischzustand (liefert immer einen konkreten Wert).

- `SelectionBackColor As Color`
  - Liest/Setzt die Hintergrund-/Highlightfarbe.
  - Meldet aktuell keinen Mischzustand (liefert immer einen konkreten Wert).

- `SelectionLeftIndent As Nullable(Of Integer)`
  - Liest/Setzt den linken Absatz-Einzug (Pixel).
  - Rückgabe `Nothing` bei Mischzustand.
  - Setzen: `ArgumentOutOfRangeException` bei negativen Werten.
  - Wirkt absatzweise.


  ---

  <a name="tri-state"></a>
## 5. Mischzustände (Tri-State-Logik)

- Stil-Flags (Bold/Italic/Underline/Strikeout) und Schriftgröße:
  - Bei `SelectionLength > 0` wird per Zeichen gescannt.
  - Einheitlicher Wert -> konkreter Rückgabewert.
  - Abweichungen -> `Nothing` (d. h. Mischzustand).
- Vorder-/Hintergrundfarbe:
  - Derzeit keine Mischzustandserkennung (immer konkreter Wert).
- Absatzwerte (z. B. `SelectionLeftIndent`):
  - Scans per Zeichen (Absatz-Kontext), Mischzustände ergeben `Nothing`.

Anwendungsfall UI (Tri-State-Buttons):
- `Nullable(Of Boolean)` lässt sich direkt auf `CheckState` von `CheckBox` mit `ThreeState=True` abbilden:
  - `Nothing` -> `CheckState.Indeterminate`
  - `True` -> `CheckState.Checked`
  - `False` -> `CheckState.Unchecked`


  ---

  <a name="redraw-suppression"></a>
## 6. Redraw-Suppression und Ereignisverhalten

- Interne Batch-Blöcke unterdrücken via `WM_SETREDRAW` das Neuzeichnen, bis alle Änderungen abgeschlossen sind.
- `BeginInternalSelectionScan`/`EndInternalSelectionScan`:
  - Unterdrücken `SelectionChanged` während per-Zeichen-Scans, vermeiden so Flackern und UI-Feedback-Schleifen.
- Nach Ende der Batches wird ein sofortiges `Invalidate()` + `Update()` ausgelöst, um sichtbare Artefakte zu vermeiden.
- Diese Funktionen sind intern; als Nutzer profitieren Sie automatisch davon.

Threading:
- Wie bei allen WinForms-Controls dürfen Aufrufe nur vom UI-Thread erfolgen.


---

<a name="examples"></a>
## 7. Beispiele

<a name="example-code"></a>
### Toolbar-Buttons an Mischzustände koppeln

```vbnet
' Angenommene Toolbar-Button-Instanzen: 
' cmdBold, cmdItalic, cmdUnderline, cmdStrikeout (alle ToolStripButton)

Private Sub UpdateUIState(rtf As ExtendedRTF)
    Dim bMixed As Boolean
    ' Fett
    bMixed = (rtf.SelectionBold IsNot Nothing)
    cmdBold.Checked = (rtf.SelectionBold = True)
    cmdBold.Enabled = Not bMixed
    ' Kursiv
    bMixed = (rtf.SelectionItalic IsNot Nothing)
    cmdItalic.Checked = (rtf.SelectionItalic = True)
    cmdItalic.Enabled = Not bMixed
    ' Unterstrichen
    bMixed = (rtf.SelectionUnderline IsNot Nothing)
    cmdUnderline.Checked = (rtf.SelectionUnderline = True)
    cmdUnderline.Enabled = Not bMixed
    ' Durchgestrichen
    bMixed = (rtf.SelectionStrikeout IsNot Nothing)
    cmdStrikeout.Checked = (rtf.SelectionStrikeout = True)
    cmdStrikeout.Enabled = Not bMixed
End Sub

Private Sub cmdBold_Click(sender As Object, e As EventArgs) Handles cmdBold.Click
    rtf.ToggleBold()
    UpdateUIState(rtf)
End Sub
Private Sub cmdItalic_Click(sender As Object, e As EventArgs) Handles cmdItalic.Click
    rtf.ToggleItalic()
    UpdateUIState(rtf)
End Sub
Private Sub cmdUnderline_Click(sender As Object, e As EventArgs) Handles cmdUnderline.Click
    rtf.ToggleUnderline()
    UpdateUIState(rtf)
End Sub
Private Sub cmdStrikeout_Click(sender As Object, e As EventArgs) Handles cmdStrikeout.Click
    rtf.ToggleStrikeout()
    UpdateUIState(rtf)
End Sub

Private Sub rtf_SelectionChanged(sender As Object, e As EventArgs) Handles rtf.SelectionChanged
    UpdateUIState(rtf)
End Sub
```

<a name="example-fontsize"></a>
### Schriftgröße setzen mit Validierung

```vbnet
Private Sub SetFontSize(rtf As ExtendedRTF, size As Single)
    ' Einfache Validierung und Anwendung der Schriftgröße
    If size >= MIN_FONT_SIZE Then
        rtf.SelectionFontSize = size
    Else
        MessageBox.Show("Schriftgröße zu klein! Mindestgröße ist " & MIN_FONT_SIZE.ToString())
    End If
End Sub

' Anwendung:
SetFontSize(rtf, 12.0F)
```

<a name="example-paragraph"></a>
### Absatz-Ausrichtung und Bullets

```vbnet
' Angenommene Toolbar-Button-Instanz:
' cmdAlignLeft, cmdAlignCenter, cmdAlignRight (alle ToolStripButton)
' Angenommene Bullet-Checkbox-Instanz:
' chkBullet

Private Sub UpdateParagraphUIState(rtf As ExtendedRTF)
    Dim bMixed As Boolean
    ' Ausrichtung: Links
    bMixed = (rtf.SelectionLeftIndent Is Nothing)
    cmdAlignLeft.Checked = Not bMixed
    cmdAlignLeft.Enabled = Not bMixed
    ' Ausrichtung: Zentriert
    bMixed = (rtf.SelectionAlignment = HorizontalAlignment.Center)
    cmdAlignCenter.Checked = bMixed
    cmdAlignCenter.Enabled = Not bMixed
    ' Ausrichtung: Rechts
    bMixed = (rtf.SelectionRightIndent Is Nothing)
    cmdAlignRight.Checked = Not bMixed
    cmdAlignRight.Enabled = Not bMixed
    ' Bullets
    bMixed = (rtf.SelectionBullet IsNot Nothing)
    chkBullet.Checked = (rtf.SelectionBullet = True)
    chkBullet.Enabled = Not bMixed
End Sub

Private Sub cmdAlignLeft_Click(sender As Object, e As EventArgs) Handles cmdAlignLeft.Click
    rtf.SetSelectionAlignment(HorizontalAlignment.Left)
    UpdateParagraphUIState(rtf)
End Sub
Private Sub cmdAlignCenter_Click(sender As Object, e As EventArgs) Handles cmdAlignCenter.Click
    rtf.SetSelectionAlignment(HorizontalAlignment.Center)
    UpdateParagraphUIState(rtf)
End Sub
Private Sub cmdAlignRight_Click(sender As Object, e As EventArgs) Handles cmdAlignRight.Click
    rtf.SetSelectionAlignment(HorizontalAlignment.Right)
    UpdateParagraphUIState(rtf)
End Sub
Private Sub chkBullet_CheckedChanged(sender As Object, e As EventArgs) Handles chkBullet.CheckedChanged
    rtf.ToggleBullet()
    UpdateParagraphUIState(rtf)
End Sub

Private Sub rtf_SelectionChanged(sender As Object, e As EventArgs) Handles rtf.SelectionChanged
    UpdateParagraphUIState(rtf)
End Sub
```

<a name="example-clearformatting"></a>
### Formatierungen gezielt löschen

```vbnet
' Beispiel: Hintergrundfarbe zurücksetzen
Private Sub ClearBackgroundColor(rtf As ExtendedRTF)
    rtf.SelectionBackColor = SystemColors.Window
End Sub

' Anwendung:
ClearBackgroundColor(rtf)

' Beispiel: Alle Formatierungen außer Textfarbe zurücksetzen
Private Sub ClearAllButForeColor(rtf As ExtendedRTF)
    With rtf
        .SelectionBackColor = SystemColors.Window
        .SelectionFontSize = 0
        .SelectionBold = Nothing
        .SelectionItalic = Nothing
        .SelectionUnderline = Nothing
        .SelectionStrikeout = Nothing
    End With
End Sub

' Anwendung:
ClearAllButForeColor(rtf)
```

siehe auch Beispiel-Projekt „DemoRTFControl“ für ausführliche Verwendungsbeispiele


---

<a name="performance"></a>
## 8. Hinweise zu Leistung und Verhalten

- Per-Zeichen-Operationen: Für Mischzustandserkennung und mehrteilige Stiländerungen müssen Zeichen iteriert werden (WinForms-RTF bietet hierzu keine vollständige Multi-Range-API).
- Redraw-Suppression: Reduziert Flackern bei Massenänderungen spürbar.
- GDI-Handle-Reduktion: Interner Font-Cache bündelt identische Fonts und entsorgt temporäre Instanzen am Blockende.
- Sehr große Selektionen: Per-Zeichen-Scans können spürbar werden; vermeiden Sie unnötig häufige UI-Aktualisierung währenddessen.

---

<a name="limitations"></a>
## 9. Bekannte Einschränkungen

- `SelectionForeColor`/`SelectionBackColor` melden keinen Mischzustand.
- `MIN_FONT_SIZE` muss im Projekt definiert sein; andernfalls wirft `SelectionFontSize` beim Setzen ggf. eine Ausnahme.
- Nur Windows Forms (Windows), Aufrufe vom UI-Thread erforderlich.
- Absatzweite Features (Bullet, Einzug, Alignment) wirken auf Absatzebene; bei Teilselektionen mit mehreren Absätzen können Mischzustände auftreten.
