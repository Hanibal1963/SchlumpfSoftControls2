# ShapeControl

Steuerelement zum Darstellen einfacher Formen (Linien, Rechtecke, Ellipsen) für WinForms (.NET Framework 4.7.2).

## Inhalt

- [Überblick](#überblick)
- [Funktionsumfang](#funktionsumfang)
- [Unterstützte Formen](#unterstützte-formen)
- [Enums](#enums)
    - [ShapeModes](#shapemodes)
    - [DiagonalLineModes](#diagonallinemodes)
- [Eigenschaften](#eigenschaften)
- [Schnellstart](#schnellstart)
  - [Verwendung im Designer](#verwendung-im-designer)
  - [Verwendung im Code](#verwendung-im-code)
- [Beispiele](#beispiele)
    - [Dünne Trennlinie](#dünne-trennlinie)
    - [Diagonale Linie als dekoratives Element](#diagonale-linie-als-dekoratives-element)
    - [Rechteck als Rahmen](#rechteck-als-rahmen)
    - [Gefüllte Ellipse als Statusindikator](#gefüllte-ellipse-als-statusindikator)
- [Darstellungsdetails](#darstellungsdetails)
- [Transparenz / Zeichenlogik](#transparenz--zeichenlogik)
- [Performance-Hinweise](#performance-hinweise)
- [Bekannte Einschränkungen](#bekannte-einschränkungen)
- [Erweiterbarkeit](#erweiterbarkeit)
- [Fehlersuche / Troubleshooting](#fehlersuche--troubleshooting)
- [weitere Literatur](#weitere-literatur)

---

<a name="überblick"></a>
## 1. Überblick

`Shape` ist ein leichtgewichtiges WinForms-Control zur Darstellung einfacher Vektorformen ohne Abhängigkeit zu GDI+ High-Level Wrappern oder externen Bibliotheken. Es eignet sich für UI-Trennlinien, einfache Markierungen, Status-Indikatoren oder visuelle Gruppierungen.

---

<a name="funktionsumfang"></a>
## 2. Funktionsumfang

- Horizontale, vertikale und diagonale Linien (2 Richtungen)
- Rechtecke und Ellipsen (leer und gefüllt)
- Konfigurierbare Linienbreite und -farbe
- Separate Füllfarbe für gefüllte Formen
- Toolbox-Integration (Kategorie: "SchlumpfSoft Controls")
- Designer-Unterstützung (Properties erscheinen in Kategorie "Appearance")

---

<a name="unterstützte-formen"></a>
## 3. Unterstützte Formen

Der Modus wird über `ShapeModus` gesteuert:

| Modus | Beschreibung |
|-------|--------------|
| `HorizontalLine` | Horizontale Linie mittig im Control |
| `VerticalLine` | Vertikale Linie mittig im Control |
| `DiagonalLine` | Diagonale Linie (Richtung über `DiagonalLineModus`) |
| `Rectangle` | Rechteck (nur Rahmen) |
| `FilledRectangle` | Gefülltes Rechteck |
| `Ellipse` | Ellipse / Kreis (nur Rahmen) |
| `FilledEllipse` | Gefüllte Ellipse / gefüllter Kreis |

---

<a name="enums"></a>
## 4. Enums


<a name="shapemodes"></a>
### 4.1. ShapeModes

Steuert die Grundform (siehe Tabelle oben).

<a name="diagonallinemodes"></a>
### 4.2. DiagonalLineModes

| Wert | Richtung |
|------|----------|
| `TopLeftToBottomRight` | Von links oben nach rechts unten |
| `BottomLeftToTopRight` | Von links unten nach rechts oben |

---

<a name="eigenschaften"></a>
## 5. Eigenschaften

| Eigenschaft | Typ | Beschreibung | Standard |
|-------------|-----|--------------|----------|
| `ShapeModus` | `ShapeModes` | Zu zeichnende Form | `HorizontalLine` |
| `LineWidth` | `Single` | Linien- bzw. Rahmenstärke (Pixel) | `2` |
| `LineColor` | `Color` | Linien-/Rahmenfarbe | `Black` |
| `FillColor` | `Color` | Füllfarbe (nur gefüllte Formen) | `Gray` |
| `DiagonalLineModus` | `DiagonalLineModes` | Richtung für diagonale Linien | `TopLeftToBottomRight` |

---

<a name="ausgeblendete-geerbte-eigenschaften"></a>
### 6. Ausgeblendete geerbte Eigenschaften

Zur Vereinfachung der Verwendung und weil sie für die Darstellung nicht benötigt werden, sind u.a. folgende geerbte Eigenschaften im Designer ausgeblendet: `BackColor`, `BackgroundImage`, `BackgroundImageLayout`, `Font`, `ForeColor`, `RightToLeft`, `Text`.

---

<a name="schnellstart"></a>
## 7. Schnellstart

<a name="verwendung-im-designer"></a>
### 7.1. Verwendung im Designer

1. Projekt bauen, damit das Control in der Toolbox erscheint (Kategorie: "SchlumpfSoft Controls").
2. `Shape` auf ein Formular ziehen.
3. In den Eigenschaften z.B. `ShapeModus = HorizontalLine` und `LineColor = DarkGray` setzen.

<a name="verwendung-im-code"></a>
### 7.2. Verwendung im Code

```vbnet
Dim s As New ShapeControl.Shape()
With s
    .ShapeModus = ShapeControl.ShapeModes.FilledEllipse
    .LineColor = Color.DarkBlue
    .FillColor = Color.LightSkyBlue
    .LineWidth = 3
    .Size = New Size(80, 80)
    .Location = New Point(20, 20)
End With
Me.Controls.Add(s)
```

---

<a name="beispiele"></a>
## 8. Beispiele

<a name="beispiele"></a>
### 8.1. Dünne Trennlinie

```vbnet
Dim separator As New ShapeControl.Shape() With {
    .ShapeModus = ShapeControl.ShapeModes.HorizontalLine,
    .LineWidth = 1,
    .LineColor = Color.Silver,
    .Dock = DockStyle.Top,
    .Height = 6 ' etwas höher für mehr "Luft" um die Linie
}
Me.Controls.Add(separator)
```

<a name="beispiele"></a>
### 8.2. Diagonale Linie als dekoratives Element

```vbnet
Dim diag As New ShapeControl.Shape() With {
    .ShapeModus = ShapeControl.ShapeModes.DiagonalLine,
    .DiagonalLineModus = ShapeControl.DiagonalLineModes.BottomLeftToTopRight,
    .LineWidth = 4,
    .LineColor = Color.OrangeRed,
    .Size = New Size(120, 120)
}
Me.Controls.Add(diag)
```

<a name="beispiele"></a>
### 8.3. Rechteck als Rahmen

```vbnet
Dim frame As New ShapeControl.Shape() With {
    .ShapeModus = ShapeControl.ShapeModes.Rectangle,
    .LineWidth = 2,
    .LineColor = Color.DarkGreen,
    .Size = New Size(200, 100)
}
Me.Controls.Add(frame)
```

<a name="beispiele"></a>
### 8.4. Gefüllte Ellipse als Statusindikator

```vbnet
Dim indicator As New ShapeControl.Shape() With {
    .ShapeModus = ShapeControl.ShapeModes.FilledEllipse,
    .LineWidth = 2,
    .LineColor = Color.DarkGray,
    .FillColor = Color.LimeGreen,
    .Size = New Size(24, 24)
}
Me.Controls.Add(indicator)
```

---

<a name="darstellungsdetails"></a>
## 9. Darstellungsdetails

- Die tatsächliche Zeichenlogik befindet sich in `OnPaint`.
- Es wird direkt ein `Graphics`-Objekt über `CreateGraphics()` erzeugt (Hinweis: Für künftige Erweiterungen wäre die Nutzung des bereitgestellten `e.Graphics` in `OnPaint` vorzuziehen, um Flickern zu reduzieren und Repaints konsistenter zu halten).
- Linien- und Rahmenbreiten werden so versetzt gezeichnet, dass Konturen vollständig innerhalb des Controls liegen (`_LineWidth / 2` Offset).

---

<a name="transparenz--zeichenlogik"></a>
## 10. Transparenz / Zeichenlogik

Das Control setzt im überschriebenen `CreateParams` das Extended Window Style Flag `WS_EX_TRANSPARENT` (`&H20`). Dadurch:
- Zeichnet Windows zuerst die darunterliegenden Controls.
- Das Shape wird anschließend darüber gerendert.
- Effekt: pseudo-transparenter Hintergrund (keine eigene Füllung). 

Hinweis: Echt-Transparenz im Sinne von Alphakomposition bietet dieses Vorgehen nicht, aber für einfache Überlagerungen genügt es meist.

---

<a name="performance-hinweise"></a>
## 11. Performance-Hinweise
- `ControlStyles.OptimizedDoubleBuffer` ist deaktiviert (`False`). Bei starkem Redraw (Resize/Animation) kann Flickern auftreten.
- Für großflächige oder häufig animierte Szenarien kann eine Anpassung sinnvoll sein:
  ```vbnet
  SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
  SetStyle(ControlStyles.AllPaintingInWmPaint, True)
  SetStyle(ControlStyles.UserPaint, True)
  ```
- Mehrfaches Erzeugen von `Pen` / `Brush` Objekten in `OnPaint` könnte bei sehr vielen Instanzen optimiert werden (Objekt-Caching). Für normale UI-Nutzung unkritisch.

---

<a name="bekannte-einschränkungen"></a>
## 12. Bekannte Einschränkungen

| Bereich | Beschreibung |
|--------|--------------|
| DPI | Keine spezielle DPI-Anpassung implementiert |
| Animation | Kein integrierter Animationssupport |
| Barrierefreiheit | Keine eigene Accessibility-Implementierung |
| Fokus | Kein Fokusverhalten / keine Interaktion vorgesehen |
| Text | Keine Textdarstellung (absichtlich ausgeblendet) |

---

<a name="erweiterbarkeit"></a>
## 13. Erweiterbarkeit

Mögliche Erweiterungen:
- Anti-Aliasing aktivierbar (`g.SmoothingMode = SmoothingMode.AntiAlias`).
- Weitere Formen (Dreieck, Polygon, Pfeile).
- Unterstützung für gestrichelte Linien (`Pen.DashStyle`).
- Ereignisse wie `ShapeClicked` (Hit-Test für Linien vs. Füllung separat).
- Eigener Renderer / Strategy Pattern für Formen.
- Caching von `Pen`/`Brush` über PropertyChanged-Ereignisse.

---

<a name="fehlersuche--troubleshooting"></a>
## 14. Fehlersuche / Troubleshooting

| Problem | Ursache | Lösung |
|---------|---------|--------|
| Linie wirkt unscharf | Ungerade Breiten auf unpassenden Koordinaten | Ganzzahlige Größe / Position prüfen oder Anti-Aliasing aktivieren |
| Form flackert | Kein DoubleBuffering | Optionale Styles aktivieren (s. Performance) |
| Transparenz funktioniert nicht wie erwartet | Erwartung echter Alpha-Transparenz | GDI+ Repaint oder Parent-Repaint-Trigger nötig / anderes Konzept (z.B. Layered Window) |

---

<a name="weitere-literatur"></a>
## 15. weitere Literatur

- [Erstellen eines Windows Forms-Toolbox-Steuerelements](https://docs.microsoft.com/de-de/visualstudio/extensibility/creating-a-windows-forms-toolbox-control?view=vs-2022)
- [Infos zur ControlStyles Enumeration](https://learn.microsoft.com/de-de/dotnet/api/system.windows.forms.controlstyles?redirectedfrom=MSDN&view=netframework-4.7.2)
- [Control-Techniken: Eigenes Toolboxicon für Steuerelement](https://www.vb-paradise.de/index.php/Thread/123746-Control-Techniken-Eigenes-Toolboxicon-f%C3%BCr-Steuerelement/)
- [Transparent control backgrounds on a VB.NET gradient filled form?](https://stackoverflow.com/questions/511320/transparent-control-backgrounds-on-a-vb-net-gradient-filled-form)
- [Extended Window Styles](https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles)
