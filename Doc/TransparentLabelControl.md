# TransparentLabelControl

Ein .NET Windows Forms Steuerelement (`TransparentLabel`) zum Anzeigen von Text mit durchscheinendem (quasi transparentem) Hintergrund über anderen Steuerelementen oder Grafiken.

---

## Inhaltsverzeichnis

1. [Übersicht](#1-übersicht)
2. [Einordnung & Motivation](#2-einordnung--motivation)
3. [Funktionsprinzip von Transparenz unter WinForms](#3-funktionsprinzip-von-transparenz-unter-winforms)
4. [Hauptmerkmale](#4-hauptmerkmale)
5. [Öffentliche / Verwendbare Eigenschaften](#5-öffentliche--verwendbare-eigenschaften)
6. [Ausgeblendete Eigenschaften (Design-Entscheidung)](#6-ausgeblendete-eigenschaften-design-entscheidung)
7. [Verwendung](#7-verwendung)
    - [Im Designer](#71-im-designer)
    - [Dynamische Erzeugung zur Laufzeit](#72-dynamische-erzeugung-zur-laufzeit)
    - [Über anderen Controls / Hintergrundgrafiken](#73-über-anderen-controls--hintergrundgrafiken)
8. [Leistungs- & Zeichenverhalten](#8-leistungs--zeichenverhalten)
9. [Unterschiede zu Standard `Label`](#9-unterschiede-zu-standard-label)
10. [Grenzen / Bekannte Einschränkungen](#10-grenzen--bekannte-einschränkungen)
11. [Troubleshooting](#11-troubleshooting)
12. [Erweiterung / Anpassung](#12-erweiterung--anpassung)
13. [Beispielcode](#13-beispielcode)
    - [Override für individuellen Text-Renderstil](#131-beispiel-override-für-individuellen-text-renderstil)
    - [Formular mit Hintergrundgrafik und TransparentLabel](#132-formular-mit-hintergrundgrafik-und-transparentlabel)

---

<a name="1-übersicht"></a>
 ## 1. Übersicht

`TransparentLabel` ist ein benutzerdefiniertes Label-Control, das es ermöglicht, Text mit transparentem Hintergrund darzustellen. 

Es eignet sich besonders für Oberflächen, bei denen der Hintergrund durchscheinen soll, z. B. bei überlagerten Texten auf Bildern oder farbigen Flächen.

Die Idee hinter diesem Projekt ist, z.Bsp. einen Text teilweise über ein Bild zu legen ohne sich großartig Gedanken über Grafikroutinen zu machen.

Mit diesem Control ist das in wenigen Zeilen Code erledigt bzw. im Designer zusammengeklickt.

---

<a name="2-einordnung--motivation"></a>
## 2. Einordnung & Motivation

Das Standard-`Label`-Control von WinForms unterstützt keine echte Alphatransparenz beim Überlagern auf anderen Steuerelementen (z.B. `PictureBox`, benutzerdefinierte Zeichenflächen). `BackColor = Color.Transparent` führt häufig nur zur Transparent-Behandlung relativ zum Elterncontainer – nicht jedoch zu einer echten Überlagerung mehrerer Controls mit durchscheinendem Hintergrund.

`TransparentLabel` erzwingt transparentes Zeichnen durch Setzen des erweiterten Fensterstils (`WS_EX_TRANSPARENT`) und geeigneter ControlStyles. Dadurch kann Text "schwebend" über einer darunterliegenden Oberfläche erscheinen.

---

<a name="3-funktionsprinzip-von-transparenz-unter-winforms"></a>
## 3. Funktionsprinzip von Transparenz unter WinForms

Windows Forms ist GDI basierend und kennt keine generische per-Pixel-Alpha-Durchzeichnung zwischen Controls. Der Ansatz dieses Controls:
- Setzen des Extended Window Styles `WS_EX_TRANSPARENT` (Hex: `0x20`).
- Verhindern unnötiger Doppel-Pufferung (bewusste Kontrolle über `OptimizedDoubleBuffer`).
- Erzwingen von Repaint-Reihenfolgen: Das transparente Control wird später gezeichnet / erlaubt darunterliegendem Parent die Hintergrunddarstellung.

Das Ergebnis ist *praktische* Transparenz für typische Szenarien: Text wirkt freigestellt.

---

<a name="4-hauptmerkmale"></a>
## 4. Hauptmerkmale

- Transparent wirkender Hintergrund (Parent-Inhalt scheint durch).
- Verwendet Standard-Label-Funktionalitäten für Textausgabe, AutoSize, Font, ForeColor etc.
- Reduzierte Design-Oberfläche: irrelevante Eigenschaften werden im Designer ausgeblendet (vereinfachte Bedienung).
- Toolbox-Integration mittels `ProvideToolboxControl` & `ToolboxBitmap`.
- Ressourcen-schonende Implementierung (keine komplexe Overhead-Paint-Pipeline).

---

<a name="5-öffentliche--verwendbare-eigenschaften"></a>
## 5. Öffentliche / Verwendbare Eigenschaften

Alle nicht überschriebenen, vom Basistyp `Label` geerbten, weiterhin sichtbaren Eigenschaften können verwendet werden, u.a.:
- `Text`
- `Font`
- `ForeColor`
- `AutoSize`
- `TextAlign`
- `Dock` / `Anchor`
- `UseMnemonic`
- `Enabled`, `Visible`
- Ereignisse wie `Click`, `DoubleClick`, `MouseEnter`, etc.

---

<a name="6-ausgeblendete-eigenschaften-design-entscheidung"></a>
## 6. Ausgeblendete Eigenschaften (Design-Entscheidung)

Folgende Properties sind für das Konzept (bewusst transparenter Hintergrund) nicht sinnvoll und wurden deshalb mit `[Browsable(False)]` & `[EditorBrowsable(Never)]` versteckt:
- `BackColor`
- `BackgroundImage`
- `BackgroundImageLayout`
- `FlatStyle`

Begründung: Diese Eigenschaften erzeugen Erwartungshaltungen (Farbfüllung, Layout), die das Transparenzkonzept konterkarieren würden.

---

<a name="7-verwendung"></a>
## 7. Verwendung

<a name="71-im-designer"></a>
### 7.1 Im Designer

1. Projekt referenzieren (falls das Control in anderem Projekt genutzt wird).
2. Build ausführen, damit `TransparentLabel` in der Toolbox unter "SchlumpfSoft Controls" erscheint.
3. Control auf ein Formular ziehen.
4. `Text`, `Font`, `ForeColor`, `AutoSize` konfigurieren.
5. Position über anderen Steuerelementen (z.B. `PictureBox`) platzieren.

<a name="72-dynamische-erzeugung-zur-laufzeit"></a>
### 7.2 Dynamische Erzeugung zur Laufzeit

```vb
Dim lbl As New TransparentLabelControl.TransparentLabel() With {
    .Text = "Überlagerter Text",
    .ForeColor = Color.Yellow,
    .Font = New Font("Segoe UI", 14, FontStyle.Bold),
    .AutoSize = True,
    .Location = New Point(32, 32)
}
Me.Controls.Add(lbl)
```

<a name="73-über-anderen-controls--hintergrundgrafiken"></a>
### 7.3 Über anderen Controls / Hintergrundgrafiken

- Am besten liegt das `TransparentLabel` direkt auf demselben Parent wie das darunterliegende Element.
- Reihenfolge kontrollieren: `BringToFront()` verwenden.
- Bei flackernden Neu-Zeichnungen (häufig bei sich ändernden Hintergrundbildern) ggf. manuell `Parent.Invalidate()` oder gezieltes Redraw steuern.

---

<a name="8-leistungs--zeichenverhalten"></a>
## 8. Leistungs- & Zeichenverhalten

- Durch `WS_EX_TRANSPARENT` kann Windows das Control beim Neuzeichnen mehrfach anstoßen (Overdraw). Für statischen Text ist das unkritisch.
- `OptimizedDoubleBuffer = False`: Hintergrund wird nicht fälschlich weggedoppelt (vermeidet, dass Transparenzwirkung zerstört wird). Bei stark flackernder Umgebung könnte ein eigener Offscreen-Puffer in einer erweiterten Version implementiert werden.
- Für Masseneinsatz (viele Dutzend Labels) prüfen, ob Redraw-Frequenzen akzeptabel bleiben.

---

<a name="9-unterschiede-zu-standard-label"></a>
## 9. Unterschiede zu Standard `Label`

| Aspekt | Standard Label | TransparentLabel |
|-------|----------------|------------------|
| Transparenter Hintergrund | Nur Parent-Hintergrund | Parent-Inhalt scheint durch (overdraw) |
| Designer-Properties | Vollständig | Reduzierte, kuratierte Menge |
| Hintergrund-Bitmap | Unterstützt | Deaktiviert/ausgeblendet |
| Einsatz über bewegten Grafiken | Eingeschränkt | Besser geeignet (aber kein echtes Alpha-Blend) |

---

<a name="10-grenzen--bekannte-einschränkungen"></a>
## 10. Grenzen / Bekannte Einschränkungen

- Keine echte Per-Pixel-Alpha-Komposition; es wird neu über darunter liegende Inhalte gezeichnet.
- Flackern möglich bei sehr häufigem Repaint anderer Controls.
- Nicht ideal für Szenarien mit Videos / hochfrequent animierten Hintergründen.
- Hintergrundinteraktion (Mausereignisse) unter dem Label erfolgt nicht, solange das Label `Enabled` ist (normaler Windows-Message-HitTest).

---

<a name="11-troubleshooting"></a>
## 11. Troubleshooting

| Problem | Mögliche Ursache | Lösung |
|---------|------------------|--------|
| Text flackert | Häufige Parent-Neuzeichnung | DoubleBuffering für Parent aktivieren / Redraw reduzieren |
| Scheint nicht transparent | Falscher Parent oder Reihenfolge | Sicherstellen: `Parent` korrekt, `BringToFront()` ausführen |
| Nicht in Toolbox sichtbar | Projekt nicht gebaut | Lösung: Build ausführen |
| Hintergrund wirkt grau | OS Theme / falsches Zeichenereignis | Prüfen, ob Parent korrekt rendert (kein benutzerdefiniertes Überschreiben ohne BaseCall) |

---

<a name="12-erweiterung--anpassung"></a>
## 12. Erweiterung / Anpassung

Ideen für zukünftige Versionen:
- Optionale Alphafarb-Hinterlegung (teiltransparente Box hinter Text via Custom Paint).
- Optionaler Rand / Umriss (Outline) zur besseren Lesbarkeit.
- Schattenwurf (z.B. über `TextRenderer.DrawText` zweimal mit Versatz zeichnen).
- Performance-Optimierung durch eigenes Caching des Hintergrundausschnitts.

 ---

 <a name="13-beispielcode"></a>
## 13. Beispielcode

<a name="131-beispiel-override-für-individuellen-text-renderstil"></a>
### 13.1. Beispiel: Override für individuellen Text-Renderstil

```vb
Protected Overrides Sub OnPaint(e As PaintEventArgs)
    MyBase.OnPaint(e)
    ' Erweiterungspunkt: zusätzlicher Effekt
    ' e.Graphics.DrawRectangle(Pens.Yellow, 0, 0, Me.Width - 1, Me.Height - 1)
End Sub
```

<a name="132-formular-mit-hintergrundgrafik-und-transparentlabel"></a>
### 13.2. Formular mit Hintergrundgrafik und TransparentLabel

```vb
Public Class Form1
    Public Sub New()
        InitializeComponent()
        Dim bg As New PictureBox() With {
            .Image = Image.FromFile("hintergrund.jpg"),
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Dock = DockStyle.Fill
        }
        Me.Controls.Add(bg)

        Dim overlay As New TransparentLabelControl.TransparentLabel() With {
            .Text = "Overlay Text",
            .Font = New Font("Segoe UI", 24, FontStyle.Bold),
            .ForeColor = Color.White,
            .AutoSize = True,
            .BackColor = Color.Transparent, ' (wird intern ignoriert / nicht genutzt)
            .Location = New Point(30, 30)
        }
        Me.Controls.Add(overlay)
        overlay.BringToFront()
    End Sub
End Class
```

