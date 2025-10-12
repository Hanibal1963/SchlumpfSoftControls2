# ColorProgressBarControl

Ausführliche Dokumentation für das benutzerdefinierte WinForms-Steuerelement `ColorProgressBar`.

## Einführung

Die Idee hinter dem `ColorProgressBarControl` ist es, einen Fortschrittsbalken zu erstellen, der in optisch anpassbar ist.

Der Standard-Fortschrittsbalken in Windows ist ein einfacher Balken, der den Fortschritt in Form einer Füllung anzeigt. 
Der `ColorProgressBarControl` hingegen kann in verschiedenen Farben und Stilen angezeigt werden.

Als Anregung diente der Artikel [A Better ProgressBar - Using Panels!](https://www.codeproject.com/Articles/31903/A-Better-ProgressBar-Using-Panels) von Saul Johnson.

Da die Donwnloads auf der Seite nicht mehr zu funktionieren scheinen und die Beschreibung nur Ausschnitte aus dem Original C# Code enthält und ich wenig Ahnung von C# habe, habe ich das Control in VB NET umgesetzt.

---

## Inhaltsverzeichnis

1. [Überblick](#ueberblick)
2. [Features](#features)
3. [Architektur & Aufbau](#architektur_aufbau)
4. [Öffentliche API](#oeffentliche_api)
   - [Eigenschaften](#eigenschaften)
   - [Ereignisse](#ereignisse)
5. [Versteckte / unterdrückte Eigenschaften](#versteckte_eigenschaften)
6. [Darstellung & Render-Logik](#darstellung_render_logik)
7. [Verwendung](#verwendung)
   - [Schnelles Beispiel](#schnelles_beispiel)
   - [Dynamische Aktualisierung](#dynamische_aktualisierung)
   - [Designer-Nutzung](#designer_nutzung)
8. [Verhalten bei Größenänderung](#verhalten_bei_groessenanderung)
9. [Glossy-Effekt](#glossy_effekt)
10. [Fehler- und Grenzfälle](#fehler_und_grenzfaelle)
11. [Performance-Hinweise](#performance_hinweise)
12. [Erweiterungsideen / Roadmap](#erweiterungsideen_roadmap)
13. [Migration & Versionierung](#migration_versionierung)
14. [Kurzübersicht](#kurzuebersicht)

---

<a id="ueberblick"></a>
## 1. Überblick

`ColorProgressBar` ist ein leichtgewichtiges Windows Forms UserControl zur Visualisierung eines fortschreitenden Wertes mittels farbigem Balken mit optionalem Rahmen und Glanzeffekt. Es eignet sich für Szenarien, in denen ein einfacher, visuell anpassbarer Fortschrittsindikator benötigt wird und der Standard-`ProgressBar` nicht ausreichend flexibel ist.

Das Control wird über das Attribut `[ProvideToolboxControl]` einer Toolbox-Kategorie ("SchlumpfSoft Controls") zugeordnet und kann damit direkt im Designer verwendet werden.

---

<a id="features"></a>
## 2. Features

- Einstellbarer Maximal- und Ist-Wert (Ganzzahlen)
- Dynamische Breitenberechnung basierend auf Control-Breite
- Konfigurierbarer Rahmen (an/aus, Farbe)
- Zwei Zustandsfarben: gefüllter Bereich (`BarColor`), leerer Bereich (`EmptyColor`)
- Optionaler Glanzeffekt (zweiteilige Gloss-Overlays)
- Klick-Ereignisweiterleitung (vereinheitlicht über alle Teilflächen)
- Design-Time Unterstützung (Eigenschaften gruppiert per `Category`)
- Automatische Korrektur des Wertes beim Überschreiten des Maximums

---

<a id="architektur_aufbau"></a>
## 3. Architektur & Aufbau

Das Control erbt von `UserControl` und besteht intern (laut Code) aus mehreren Panel-Elementen:

- `ProgressEmpty`: Grundfläche (leerer Bereich)
- `ProgressFull`: gefüllter Fortschrittsbalken (Breite dynamisch)
- `GlossLeft` / `GlossRight`: halbtransparente Overlays für den Glanzeffekt

Die Berechnungslogik ist in zwei interne Methoden gekapselt:
- `UpdateProgress()`: Berechnet Pixelbreite pro Einheit und setzt Balkenbreite, Rahmen-Padding und Sichtbarkeit.
- `UpdateGloss()`: Passt Gloss-Höhe proportional zur Control-Höhe an.

---

<a id="oeffentliche_api"></a>
## 4. Öffentliche API

<a id="eigenschaften"></a>
### 4.1 Eigenschaften

| Eigenschaft | Typ | Standard | Kategorie | Beschreibung |
|-------------|-----|----------|-----------|--------------|
| `Value` | `Integer` | 1 | Behavior | Aktueller Fortschrittswert. Wird auf `ProgressMaximumValue` gedeckelt. |
| `ProgressMaximumValue` | `Integer` | 10 | Behavior | Maximaler Fortschrittswert. Wird auf `Width` begrenzt. |
| `BarColor` | `Color` | `Blue` | Appearance | Farbe des gefüllten Fortschrittsbereichs. |
| `EmptyColor` | `Color` | `LightGray` | Appearance | Farbe des leeren Bereichs. |
| `BorderColor` | `Color` | `Black` | Appearance | Rahmen-/Hintergrundfarbe des äußeren Containers. |
| `ShowBorder` | `Boolean` | `True` | Appearance | Steuert Rahmenanzeige (intern via `Padding = 1`). |
| `IsGlossy` | `Boolean` | `True` | Appearance | Aktiviert/Deaktiviert Glanzoverlays. |

<a id="ereignisse"></a>
### 4.2 Ereignisse

| Ereignis | Beschreibung |
|----------|--------------|
| `Click` | Shadows-Ereignis; wird auf Klick in eine interne Teilfläche ausgelöst (vereinheitlichte Weiterleitung). |

---

<a id="versteckte_eigenschaften"></a>
## 5. Versteckte / unterdrückte Eigenschaften

Folgende geerbte Eigenschaften wurden mittels `Browsable(False)` und `EditorBrowsable(EditorBrowsableState.Never)` ausgeblendet, um die API zu verschlanken:
- `BackColor` (wird intern für Rahmen genutzt)
- `BackgroundImage`
- `BackgroundImageLayout`
- `BorderStyle`
- `ForeColor`
- `Padding`

Hinweis: Änderungen über Code sind weiterhin möglich, werden aber ggf. von der internen Logik (z. B. `Padding`) wieder überschrieben.

---

<a id="darstellung_render_logik"></a>
## 6. Darstellung & Render-Logik

- Breitenberechnung: `ProgressFull.Width = Value * (Width / MaxValue)`
- Bei erreichtem Maximum wird der Balken (abhängig vom Rahmen) auf volle Breite minus Rand gesetzt.
- Der Rahmen wird nicht gezeichnet, sondern durch internes `Padding = 1` simuliert (äußere `BackColor` = Rahmenfarbe).
- Gloss: Zwei Panels (`GlossLeft`, `GlossRight`) mit halbtransparenter weißer Farbe (`ARGB(100,255,255,255)`), Höhe = `Height / 3`.

---

<a id="verwendung"></a>
## 7. Verwendung

<a id="schnelles_beispiel"></a>
### 7.1 Schnelles Beispiel (Code Behind)

```vbnet
Imports ColorProgressBarControl

' Erstellung
Dim cpb As New ColorProgressBar() With {
    .ProgressMaximumValue = 50,
    .Value = 10,
    .BarColor = Color.LimeGreen,
    .EmptyColor = Color.Gainsboro,
    .BorderColor = Color.Black,
    .ShowBorder = True,
    .IsGlossy = True,
    .Dock = DockStyle.Top,
    .Height = 24,
    .Width = 300
}
AddHandler cpb.Click, Sub(sender, e) Console.WriteLine($"Progress: {cpb.Value}/{cpb.ProgressMaximumValue}")
Me.Controls.Add(cpb)
```

<a id="dynamische_aktualisierung"></a>
### 7.2 Dynamische Aktualisierung

```vbnet
For i = 1 To cpb.ProgressMaximumValue
    cpb.Value = i
    Application.DoEvents() ' Nur bei Demo – in Produktion besser async / Timer
    Threading.Thread.Sleep(50)
Next
```

<a id="designer_nutzung"></a>
### 7.3 Nutzung im Designer

1. Projekt bauen – das Control erscheint in der Toolbox-Kategorie „SchlumpfSoft Controls“.
2. Control auf ein Formular ziehen.
3. Eigenschaften im Eigenschaftenfenster anpassen.
4. Optional: `Click` Ereignis doppelt anklicken für Handler.

---

<a id="verhalten_bei_groessenanderung"></a>
## 8. Verhalten bei Größenänderung

Das `Resize`-Ereignis aktualisiert automatisch:

- Einheitliche Pixelbreite pro Fortschrittseinheit (`_ProgressUnit`)
- Gloss-Höhe
- Balkenbreite

Empfehlung: Bei starker Dynamik (Layout Panels, Anchoring) nicht manuell `UpdateProgress()` aufrufen – dies wird intern gehandhabt.

---

<a id="glossy_effekt"></a>
## 9. Glossy-Effekt

Der Glanzeffekt simuliert ein reflektierendes Highlight durch zwei übereinander liegende halbtransparente Panels. Abschaltbar über `IsGlossy = False`.

Visuelle Anpassungsideen (derzeit nicht implementiert):

- Gradient statt fester Transparenz
- Animierter Sweep
- Dynamische Opazität in Abhängigkeit vom Fortschritt

---

<a id="fehler_und_grenzfaelle"></a>
## 10. Fehler- und Grenzfälle

| Szenario | Verhalten |
|----------|-----------|
| `Value` > `ProgressMaximumValue` | Automatisch gedeckelt (Clamp). |
| `ProgressMaximumValue` > `Width` | Auf `Width` begrenzt. |
| `ProgressMaximumValue` <= 0 | Kein Hard-Check – sollte vor Nutzung sichergestellt werden. |
| Schnelles Resizing | Neuberechnung erfolgt pro Resize-Ereignis. |
| `IsGlossy = False` | Gloss-Panels werden ausgeblendet. |

Empfehlung: Vor Setzen des Wertes zuerst Maximum definieren.

---

<a id="performance_hinweise"></a>
## 11. Performance-Hinweise

- Keine GDI+ benutzerdefinierte OnPaint-Routinen – reine Panel-Manipulation → sehr leichtgewichtig.
- Für hochfrequente Aktualisierung (z. B. Timer < 15 ms) könnte Flickern auftreten; in solchen Fällen DoubleBuffering erwägen (aktuell nicht explizit gesetzt).
- Große Containerbreiten → lineare Breitenberechnung bleibt konstant performant.

Mögliche Optimierung:

- Setzen von `DoubleBuffered = True` (protected Property via Reflection oder abgeleitetem Custom-Wrapper).

---

<a id="erweiterungsideen_roadmap"></a>
## 12. Erweiterungsideen / Roadmap

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

<a id="migration_versionierung"></a>
## 13. Migration & Versionierung

Aktuell frühe Basisversion ohne Breaking Changes. Bei zukünftiger Einführung zusätzlicher Eigenschaften sollte darauf geachtet werden:
- Standardwerte rückwärtskompatibel halten.
- Zusätzliche Ereignisse nicht blockierend gestalten.
- Rendering auf OnPaint umstellen → Breaking Change klar dokumentieren.

---

<a id="kurzuebersicht"></a>
## 14. Kurzübersicht (Cheat Sheet)

```text
Max setzen:   ProgressMaximumValue = 100
Wert setzen:  Value = 42
Farben:       BarColor / EmptyColor / BorderColor
Optik:        ShowBorder, IsGlossy
Klick:        Click-Ereignis
```
