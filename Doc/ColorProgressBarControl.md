# ColorProgressBarControl

Ausführliche Dokumentation für das benutzerdefinierte WinForms-Steuerelement `ColorProgressBar`.

## Einführung

Die Idee hinter dem `ColorProgressBarControl` ist es, einen Fortschrittsbalken zu erstellen, der in optisch anpassbar ist.

Der Standard-Fortschrittsbalken in Windows ist ein einfacher Balken, der den Fortschritt in Form einer Füllung anzeigt. 
Der `ColorProgressBarControl` hingegen kann in verschiedenen Farben und Stilen angezeigt werden.

Als Anregung diente der Artikel [A Better ProgressBar - Using Panels!](https://www.codeproject.com/Articles/31903/A-Better-ProgressBar-Using-Panels) von Saul Johnson.

Da die Donwnloads auf der Seite nicht mehr zu funktionieren scheinen und die Beschreibung nur Ausschnitte aus dem Original C# Code enthält und ich wenig Ahnung von C# habe, habe ich das Control in VB NET umgesetzt.

---

## Überblick

`ColorProgressBar` ist ein leichtgewichtiges Windows Forms UserControl zur Visualisierung eines fortschreitenden Wertes mittels farbigem Balken mit optionalem Rahmen und Glanzeffekt. Es eignet sich für Szenarien, in denen ein einfacher, visuell anpassbarer Fortschrittsindikator benötigt wird und der Standard-`ProgressBar` nicht ausreichend flexibel ist.

Das Control wird über das Attribut `[ProvideToolboxControl]` einer Toolbox-Kategorie ("SchlumpfSoft Controls") zugeordnet und kann damit direkt im Designer verwendet werden.

---

## Features

- Einstellbarer Maximal- und Ist-Wert (Ganzzahlen)
- Dynamische Breitenberechnung basierend auf Control-Breite
- Konfigurierbarer Rahmen (an/aus, Farbe)
- Zwei Zustandsfarben: gefüllter Bereich (`BarColor`), leerer Bereich (`EmptyColor`)
- Optionaler Glanzeffekt (zweiteilige Gloss-Overlays)
- Klick-Ereignisweiterleitung (vereinheitlicht über alle Teilflächen)
- Design-Time Unterstützung (Eigenschaften gruppiert per `Category`)
- Automatische Korrektur des Wertes beim Überschreiten des Maximums

---

## Architektur & Aufbau

Das Control erbt von `UserControl` und besteht intern (laut Code) aus mehreren Panel-Elementen:

- `ProgressEmpty`: Grundfläche (leerer Bereich)
- `ProgressFull`: gefüllter Fortschrittsbalken (Breite dynamisch)
- `GlossLeft` / `GlossRight`: halbtransparente Overlays für den Glanzeffekt

Die Berechnungslogik ist in zwei interne Methoden gekapselt:
- `UpdateProgress()`: Berechnet Pixelbreite pro Einheit und setzt Balkenbreite, Rahmen-Padding und Sichtbarkeit.
- `UpdateGloss()`: Passt Gloss-Höhe proportional zur Control-Höhe an.

---

## Darstellung & Render-Logik

- Breitenberechnung: `ProgressFull.Width = Value * (Width / MaxValue)`
- Bei erreichtem Maximum wird der Balken (abhängig vom Rahmen) auf volle Breite minus Rand gesetzt.
- Der Rahmen wird nicht gezeichnet, sondern durch internes `Padding = 1` simuliert (äußere `BackColor` = Rahmenfarbe).
- Gloss: Zwei Panels (`GlossLeft`, `GlossRight`) mit halbtransparenter weißer Farbe (`ARGB(100,255,255,255)`), Höhe = `Height / 3`.

---

## Verhalten bei Größenänderung

Das `Resize`-Ereignis aktualisiert automatisch:

- Einheitliche Pixelbreite pro Fortschrittseinheit (`_ProgressUnit`)
- Gloss-Höhe
- Balkenbreite

Empfehlung: Bei starker Dynamik (Layout Panels, Anchoring) nicht manuell `UpdateProgress()` aufrufen – dies wird intern gehandhabt.

---

## Glossy-Effekt

Der Glanzeffekt simuliert ein reflektierendes Highlight durch zwei übereinander liegende halbtransparente Panels. Abschaltbar über `IsGlossy = False`.

Visuelle Anpassungsideen (derzeit nicht implementiert):

- Gradient statt fester Transparenz
- Animierter Sweep
- Dynamische Opazität in Abhängigkeit vom Fortschritt

---

## Fehler- und Grenzfälle

| Szenario | Verhalten |
|----------|-----------|
| `Value` > `ProgressMaximumValue` | Automatisch gedeckelt (Clamp). |
| `ProgressMaximumValue` > `Width` | Auf `Width` begrenzt. |
| `ProgressMaximumValue` <= 0 | Kein Hard-Check – sollte vor Nutzung sichergestellt werden. |
| Schnelles Resizing | Neuberechnung erfolgt pro Resize-Ereignis. |
| `IsGlossy = False` | Gloss-Panels werden ausgeblendet. |

Empfehlung: Vor Setzen des Wertes zuerst Maximum definieren.

---

## Performance-Hinweise

- Keine GDI+ benutzerdefinierte OnPaint-Routinen – reine Panel-Manipulation → sehr leichtgewichtig.
- Für hochfrequente Aktualisierung (z. B. Timer < 15 ms) könnte Flickern auftreten; in solchen Fällen DoubleBuffering erwägen (aktuell nicht explizit gesetzt).
- Große Containerbreiten → lineare Breitenberechnung bleibt konstant performant.

Mögliche Optimierung:

- Setzen von `DoubleBuffered = True` (protected Property via Reflection oder abgeleitetem Custom-Wrapper).

---

## Erweiterungsideen / Roadmap

| Idee | Beschreibung |
|------|--------------|
| Prozentanzeige | Optionaler Text ("XX %") mittig/überlagert. |
| Fortschrittsänderungs-Ereignis | Event `ValueChanged` zur Reaktion auf Änderungen. |
| Unterstützung für `Minimum` | Analog zu Standard-ProgressBar (`Minimum`, `Maximum`). |
| Animationsmodus | Smooth-Interpolationen beim Füllen. |
| Vertikale Darstellung | Optionale Orientierung (Horizontal/Vertikal). |
| Farbgradient | Farbverlauf statt Vollton (`LinearGradientBrush`). |
| Theming | Vordefinierte Farbpaletten (Success, Warning, Error). |
| Hintergrundtextur | Optionaler Image-Hintergrund. |
| Bar Corner Radius | Abgerundete Ecken (Custom Paint). |
| Accessibility | Support für UIA / Screenreader (`IAccessible`). |
| Wert > Int32 | Unterstützung für `Long` oder Prozent-Float. |

---

## Migration & Versionierung

Aktuell frühe Basisversion ohne Breaking Changes. Bei zukünftiger Einführung zusätzlicher Eigenschaften sollte darauf geachtet werden:
- Standardwerte rückwärtskompatibel halten.
- Zusätzliche Ereignisse nicht blockierend gestalten.
- Rendering auf OnPaint umstellen → Breaking Change klar dokumentieren.
