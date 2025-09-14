# SevenSegmentControl

## Einführung

Ich habe für ein anderes Projekt versucht eine 7-Segmentanzeige zu programmieren.
Nach einigen Fehlversuchen und einer intensive Internetrecherche, bin ich auf GitHub fündig geworden.
([SevenSegment von Dimitry Brant](https://github.com/dbrant/SevenSegment))

Ich habe mich entschlossen den Code in VisualBasic neu zu erstellen da ich mit C# keinerlei Erfahrung habe. 

## Übersicht

Das Projekt `SevenSegmentControl` stellt zwei Windows Forms Steuerelemente bereit, die klassische 7-Segment-(LED)-Anzeige(n) simulieren:

1. `SingleDigit` – Ein einzelnes 7-Segment-Display mit optionalem Dezimalpunkt und (optionalem) Doppelpunktbereich.
2. `MultiDigit` – Ein Container für mehrere `SingleDigit`-Instanzen zur Darstellung kompletter numerischer/teilweise alphanumerischer Werte.

Der Fokus liegt auf:

- Darstellbarkeit gängiger Ziffern und ausgewählter Buchstaben/Symbole
- Frei einstellbaren Farben (aktiv/inaktiv/Hintergrund)
- Anpassbarer Segmentbreite und Schrägstellung (kursiver Effekt via Shear-Transformation)
- Optionalen Dezimalpunkten und Doppelpunkt (z. B. für Uhr- oder Zeitformat)

## Ziele / Einsatzzweck

Typische Anwendungsfälle:

- Anzeige von Messwerten (Temperatur, Spannung, etc.)
- Digitale Uhren / Timer
- Zähler, einfache Statusanzeigen
- Simulation klassischer Hardwareanzeigen im UI

## Architekturüberblick

| Bestandteil | Beschreibung |
|-------------|-------------|
| `SingleDigit` | Basiskomponente. Zeichnet ein einzelnes 7-Segment-Zeichen inkl. optionaler Punkt-/Doppelpunkt-LEDs. |
| `MultiDigit` | Verwaltet ein Array von `SingleDigit`-Instanzen; sorgt für Verteilung der Breite und Mapping eines gesamten Text-/Zahlenwertes. |
| `CharacterPattern` (Enum, intern) | Bitmasken für Segmente (Bits 0–6) zur Darstellung der Zeichen. |
| `SevenSegmentControlPackage` | VS-Package-Klasse (Infrastruktur; bei Nutzung als Erweiterung irrelevant für Laufzeit der Controls). |

## Darstellung & Bitmuster

Jedes Segment wird durch ein Bit in einem Integer repräsentiert:
- Bit 0: Oberes Segment
- Bit 1: Oben links
- Bit 2: Oben rechts
- Bit 3: Mittleres Segment
- Bit 4: Unten links
- Bit 5: Unten rechts
- Bit 6: Unteres Segment

Beispiel: Ziffer "0" (`CharacterPattern.Zero = &H77` = Binär `0b1110111`) – alle bis auf das mittlere Segment aktiv.

## Klassen im Detail

### SingleDigit

Ein einzelnes anzeigendes Control.

Wichtige Eigenschaften:

- `DigitValue As String` – Setzt ein Zeichen (Ziffer oder unterstützten Buchstaben). Überschreibt ggf. manuell gesetztes `CustomBitPattern`.
- `CustomBitPattern As Integer` – Direkte Steuerung der 7 Segmente per Bitmaske.
- `SegmentWidth As Integer` – Dicke der Segmente (Standard: 10).
- `ItalicFactor As Single` – Scherfaktor für Kursiv-Effekt (negativ = nach links geneigt). Standard: -0.1.
- `InactiveColor As Color` – Farbe inaktiver Segmente.
- `ForeColor As Color` – Farbe aktiver Segmente.
- `BackColor As Color` – Hintergrund.
- `ShowDecimalPoint / DecimalPointActive` – Sichtbarkeit und Aktivität des Dezimalpunkts.
- `ShowColon / ColonActive` – Sichtbarkeit und Aktivität des Doppelpunktes (zwei kleine LEDs rechts).
- `Padding` – Steuerung der nutzbaren Zeichenfläche relativ zur Controlgröße.

Zeichenlogik:

- Beim Setzen von `DigitValue` wird – falls Ziffer – das passende Enum-Muster gewählt.
- Buchstaben werden (soweit darstellbar) auf definierte Muster gemappt (z. B. A, b, C, d, E, F, H, L, P, U, Y, etc.).
- Sonderzeichen: `-`, `_`, `°`, `'`, `"`, `=` u. a.

### MultiDigit

Container für mehrere `SingleDigit`-Instanzen.

Wichtige Eigenschaften:

- `DigitCount As Integer` – Anzahl der Stellen (max. 100 im Code begrenzt).
- `Value As String` – Anzeige-String (rechtsbündig verteilt). Unterstützt Dezimalpunkte ('.') zur Aktivierung des Punktsegments des vorausgehenden Digits.
- `SegmentWidth`, `ItalicFactor`, `InactiveColor`, `ForeColor`, `BackColor`, `ShowDecimalPoint`, `DigitPadding` – werden auf alle enthaltenen `SingleDigit` propagiert.

Funktionsweise von `Value`:

- Iteration rückwärts (von rechts nach links) über den String.
- '.' aktiviert den Dezimalpunkt des aktuellen Digits (verbraucht keine eigene Stelle).
- Sonst wird das Zeichen einer Stelle zugeordnet.

Layout:

- Breitenzuweisung linear: Gesamtsumme / Anzahl Digits.
- Positionierung aktuell einfach proportioniert (keine Zwischenräume außer `DigitPadding` innerhalb der Digits selbst).

## Unterstützte Zeichen

(Je nach Lesbarkeit auf 7 Segmenten angepasst.)
- Ziffern: 0–9
- Buchstaben (Auswahl): A B C c d E F G H h i J L N o P Q R T U u Y
- Symbole: - _ ° ' " = ≡ ¬ [ ] { } (Mapping einzelner Varianten)
- Punkt '.' (als Dezimalpunkt)
- Doppelpunkt (separat über `ShowColon/ColonActive` beim `SingleDigit`)

Nicht eindeutig darstellbare Buchstaben werden approximiert (z. B. `S` nutzt Muster der „5“).

## Wichtige interne Aspekte

- Zeichnen erfolgt in `OnPaint` (bzw. Paint-Handler) mit Anti-Aliasing.
- Kursivdarstellung mittels `Matrix.Shear`.
- Segmentgeometrie wird bei Änderung von `SegmentWidth` neu berechnet (`CalculatePoints`).
- `DoubleBuffered` aktiv für flimmerarmes Rendern.
- `CustomBitPattern` hat Vorrang, wenn direkt gesetzt (bis `DigitValue` erneut geändert wird).

## Beispielverwendung (WinForms)

### Einzelnes Digit

```vbnet
Dim seg As New SevenSegmentControl.SingleDigit() With {
    .Parent = Me,
    .Left = 10,
    .Top = 10,
    .Width = 50,
    .Height = 90,
    .ForeColor = Color.LimeGreen,
    .InactiveColor = Color.FromArgb(40, 80, 40),
    .BackColor = Color.Black,
    .SegmentWidth = 12,
    .ItalicFactor = -0.08F,
    .ShowDecimalPoint = True,
    .DigitValue = "A"
}
seg.DecimalPointActive = True
```

### Mehrere Digits (z. B. Anzeige einer Spannung)

```vbnet
Dim multi As New SevenSegmentControl.MultiDigit() With {
    .Parent = Me,
    .Left = 10,
    .Top = 120,
    .Width = 300,
    .Height = 80,
    .DigitCount = 6,
    .ForeColor = Color.Red,
    .InactiveColor = Color.FromArgb(60, 0, 0),
    .BackColor = Color.Black,
    .SegmentWidth = 10,
    .ItalicFactor = -0.1F,
    .ShowDecimalPoint = True
}
' Wert mit Dezimalpunkt
multi.Value = "12.345"
```

### Direkte Bitsteuerung

```vbnet
' Mittleres Segment + unteres Segment einschalten
seg.CustomBitPattern = &H48 ' Beispiel (abhängig von Bitdefinitionen)
```

## Design-Time Unterstützung

- `Category`, `Description`, `Browsable` Attribute für saubere PropertyGrid-Darstellung.
- Toolbox-Bitmap via `ToolboxBitmap`-Attribut (falls Ressource vorhanden).
- Ausgeblendete Standard-Properties (`Font`, `Text`, `BackgroundImage`, etc.), um Fokus auf funktionale Anzeigeeigenschaften zu legen.

## Performancehinweise

- Rendering ist leichtgewichtig für moderate Digit-Anzahlen (< 50).
- Für sehr große `DigitCount` ggf. DoubleBuffer auf Containerform aktivieren.
- Minimieren von unnötigen `Invalidate()`-Aufrufen bei Massenupdates (kaskadierte Set-Operationen bündeln).

## Erweiterungsideen

- Unterstützung für animiertes Dimmen/Fading.
- Mehrfarbige Segmente / Gradient.
- Unterstützung für Hex-Werte (A–F direkter Modus).
- Optionale automatische Skalierung der Segmentbreite anhand Controlgröße.
- Unterstützung für zusätzliche Symbole (°C, Ω, …) via zusammengesetzte Segmente.
- Externes Mapping bereitstellen (Benutzerdefinierte Glyph-Tabelle).

## Fehlerquellen / Bekannte Einschränkungen

- Einige Buchstaben sind schwer eindeutig darstellbar (z. B. K, M, W, X, Z fehlen).
- `Value`-Parsing setzt voraus, dass Dezimalpunkte immer einem vorherigen Digit zugeordnet werden können.
- Kein automatisches Trimmen oder Overflow-Hinweis, wenn `Value` länger als `DigitCount` ist (überschüssige Zeichen werden ignoriert).
- Kein Right-To-Left Support (explizit ausgeblendet).

## Styling-Empfehlungen

| Ziel | Einstellung |
|------|-------------|
| Klassischer LED-Look (Grün) | `ForeColor = Color.Lime`, `InactiveColor = Color.FromArgb(30, 60, 30)`, `BackColor = Color.Black` |
| Rotes Retro-Display | `ForeColor = Color.Red`, `InactiveColor = Color.FromArgb(70, 0, 0)`, `BackColor = Color.Black` |
| Modern (Bernstein) | `ForeColor = Color.Orange`, `InactiveColor = Color.FromArgb(80, 40, 0)` |

## Testideen

- Einzelzeichen-Sequenz 0–9 und A–Z durchlaufen.
- `DigitCount` dynamisch ändern (Resize-Verhalten prüfen).
- Performance-Test mit 50–100 Digits.
- Randfälle: Leerstring, nur Dezimalpunkte, ungültige Zeichen.

## Wartung / Codequalität

Empfohlene nächste Schritte:

- XML-Dokumentationskommentare für alle öffentlichen Members komplettieren.
- Unit-Tests für Mapping `DigitValue -> BitPattern`.
- Refactoring: Separates Mapping-Modul (Dictionary) statt `Select Case`.
- Optional: Caching der berechneten Segment-Punkte bei Größenänderungen.
