# ShapeControl

Steuerelement zum Darstellen einfacher Formen (Linien, Rechtecke, Ellipsen) für WinForms (.NET Framework 4.7.2).

## Überblick

`Shape` ist ein leichtgewichtiges WinForms-Control zur Darstellung einfacher Vektorformen ohne Abhängigkeit zu GDI+ High-Level Wrappern oder externen Bibliotheken. Es eignet sich für UI-Trennlinien, einfache Markierungen, Status-Indikatoren oder visuelle Gruppierungen.

---

## Funktionsumfang

- Horizontale, vertikale und diagonale Linien (2 Richtungen)
- Rechtecke und Ellipsen (leer und gefüllt)
- Konfigurierbare Linienbreite und -farbe
- Separate Füllfarbe für gefüllte Formen
- Toolbox-Integration (Kategorie: "SchlumpfSoft Controls")
- Designer-Unterstützung (Properties erscheinen in Kategorie "Appearance")

---

## Darstellungsdetails

- Die tatsächliche Zeichenlogik befindet sich in `OnPaint`.
- Es wird direkt ein `Graphics`-Objekt über `CreateGraphics()` erzeugt (Hinweis: Für künftige Erweiterungen wäre die Nutzung des bereitgestellten `e.Graphics` in `OnPaint` vorzuziehen, um Flickern zu reduzieren und Repaints konsistenter zu halten).
- Linien- und Rahmenbreiten werden so versetzt gezeichnet, dass Konturen vollständig innerhalb des Controls liegen (`_LineWidth / 2` Offset).

---

## Transparenz / Zeichenlogik

Das Control setzt im überschriebenen `CreateParams` das Extended Window Style Flag `WS_EX_TRANSPARENT` (`&H20`). Dadurch:
- Zeichnet Windows zuerst die darunterliegenden Controls.
- Das Shape wird anschließend darüber gerendert.
- Effekt: pseudo-transparenter Hintergrund (keine eigene Füllung). 

Hinweis: Echt-Transparenz im Sinne von Alphakomposition bietet dieses Vorgehen nicht, aber für einfache Überlagerungen genügt es meist.

---

## Performance-Hinweise
- `ControlStyles.OptimizedDoubleBuffer` ist deaktiviert (`False`). Bei starkem Redraw (Resize/Animation) kann Flickern auftreten.
- Für großflächige oder häufig animierte Szenarien kann eine Anpassung sinnvoll sein:
  ```vbnet
  SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
  SetStyle(ControlStyles.AllPaintingInWmPaint, True)
  SetStyle(ControlStyles.UserPaint, True)
  ```
- Mehrfaches Erzeugen von `Pen` / `Brush` Objekten in `OnPaint` könnte bei sehr vielen Instanzen optimiert werden (Objekt-Caching). Für normale UI-Nutzung unkritisch.

---

## Bekannte Einschränkungen

| Bereich | Beschreibung |
|--------|--------------|
| DPI | Keine spezielle DPI-Anpassung implementiert |
| Animation | Kein integrierter Animationssupport |
| Barrierefreiheit | Keine eigene Accessibility-Implementierung |
| Fokus | Kein Fokusverhalten / keine Interaktion vorgesehen |
| Text | Keine Textdarstellung (absichtlich ausgeblendet) |

---

## Erweiterbarkeit

Mögliche Erweiterungen:
- Anti-Aliasing aktivierbar (`g.SmoothingMode = SmoothingMode.AntiAlias`).
- Weitere Formen (Dreieck, Polygon, Pfeile).
- Unterstützung für gestrichelte Linien (`Pen.DashStyle`).
- Ereignisse wie `ShapeClicked` (Hit-Test für Linien vs. Füllung separat).
- Eigener Renderer / Strategy Pattern für Formen.
- Caching von `Pen`/`Brush` über PropertyChanged-Ereignisse.

---

## Fehlersuche / Troubleshooting

| Problem | Ursache | Lösung |
|---------|---------|--------|
| Linie wirkt unscharf | Ungerade Breiten auf unpassenden Koordinaten | Ganzzahlige Größe / Position prüfen oder Anti-Aliasing aktivieren |
| Form flackert | Kein DoubleBuffering | Optionale Styles aktivieren (s. Performance) |
| Transparenz funktioniert nicht wie erwartet | Erwartung echter Alpha-Transparenz | GDI+ Repaint oder Parent-Repaint-Trigger nötig / anderes Konzept (z.B. Layered Window) |

---

## weitere Literatur

- [Erstellen eines Windows Forms-Toolbox-Steuerelements](https://docs.microsoft.com/de-de/visualstudio/extensibility/creating-a-windows-forms-toolbox-control?view=vs-2022)
- [Infos zur ControlStyles Enumeration](https://learn.microsoft.com/de-de/dotnet/api/system.windows.forms.controlstyles?redirectedfrom=MSDN&view=netframework-4.7.2)
- [Control-Techniken: Eigenes Toolboxicon für Steuerelement](https://www.vb-paradise.de/index.php/Thread/123746-Control-Techniken-Eigenes-Toolboxicon-f%C3%BCr-Steuerelement/)
- [Transparent control backgrounds on a VB.NET gradient filled form?](https://stackoverflow.com/questions/511320/transparent-control-backgrounds-on-a-vb-net-gradient-filled-form)
- [Extended Window Styles](https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles)
