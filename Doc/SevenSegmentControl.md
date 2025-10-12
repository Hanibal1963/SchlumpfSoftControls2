# SevenSegmentControl

## Einführung

Ich habe für ein anderes Projekt versucht eine 7-Segmentanzeige zu programmieren.
Nach einigen Fehlversuchen und einer intensive Internetrecherche, bin ich auf GitHub fündig geworden.
([SevenSegment von Dimitry Brant](https://github.com/dbrant/SevenSegment))

Ich habe mich entschlossen den Code in VisualBasic neu zu erstellen da ich mit C# keinerlei Erfahrung habe. 

## Inhaltsverzeichnis

1. [Übersicht](#1-übersicht)
2. [Ziele / Einsatzzweck](#2-ziele--einsatzzweck)
3. [Architekturüberblick](#3-architekturüberblick)
4. [Darstellung & Bitmuster](#4-darstellung--bitmuster)
5. [Klassen im Detail](#5-klassen-im-detail)
   - [SingleDigit](#51-singledigit)
   - [MultiDigit](#52-multidigit)
6. [Unterstützte Zeichen](#6-unterstützte-zeichen)
7. [Wichtige interne Aspekte](#7-wichtige-interne-aspekte)
8. [Beispielverwendung (WinForms)](#8-beispielverwendung-winforms)
   - [Einzelnes Digit](#81-einzelnes-digit)
   - [Mehrere Digits (z. B. Anzeige einer Spannung)](#82-mehrere-digits-z-b-anzeige-einer-spannung)
   - [Direkte Bitsteuerung](#83-direkte-bitsteuerung)
9. [Design-Time Unterstützung](#9-design-time-unterstützung)
10. [Performancehinweise](#10-performancehinweise)
11. [Erweiterungsideen](#11-erweiterungsideen)
12. [Fehlerquellen / Bekannte Einschränkungen](#12-fehlerquellen--bekannte-einschränkungen)
13. [Styling-Empfehlungen](#13-styling-empfehlungen)
14. [Testideen](#14-testideen)
15. [Wartung / Codequalität](#15-wartung--codequalität)
16. [Weitere Literatur](#16-weitere-literatur)

---

<a name="1-übersicht"></a>
## 1. Übersicht

Das Projekt `SevenSegmentControl` stellt zwei Windows Forms Steuerelemente bereit, die klassische 7-Segment-(LED)-Anzeige(n) simulieren:

   - `SingleDigit` – Ein einzelnes 7-Segment-Display mit optionalem Dezimalpunkt und (optionalem) Doppelpunktbereich.
   - `MultiDigit` – Ein Container für mehrere `SingleDigit`-Instanzen zur Darstellung kompletter numerischer/teilweise alphanumerischer Werte.

Der Fokus liegt auf:

- Darstellbarkeit gängiger Ziffern und ausgewählter Buchstaben/Symbole
- Frei einstellbaren Farben (aktiv/inaktiv/Hintergrund)
- Anpassbarer Segmentbreite und Schrägstellung (kursiver Effekt via Shear-Transformation)
- Optionalen Dezimalpunkten und Doppelpunkt (z. B. für Uhr- oder Zeitformat)

---

<a name="2-ziele--einsatzzweck"></a>
## 2. Ziele / Einsatzzweck

Typische Anwendungsfälle:

- Anzeige von Messwerten (Temperatur, Spannung, etc.)
- Digitale Uhren / Timer
- Zähler, einfache Statusanzeigen
- Simulation klassischer Hardwareanzeigen im UI

---

<a name="3-architekturüberblick"></a>
## 3. Architekturüberblick

| Bestandteil | Beschreibung |
|-------------|-------------|
| `SingleDigit` | Basiskomponente. Zeichnet ein einzelnes 7-Segment-Zeichen inkl. optionaler Punkt-/Doppelpunkt-LEDs. |
| `MultiDigit` | Verwaltet ein Array von `SingleDigit`-Instanzen; sorgt für Verteilung der Breite und Mapping eines gesamten Text-/Zahlenwertes. |
| `CharacterPattern` (Enum, intern) | Bitmasken für Segmente (Bits 0–6) zur Darstellung der Zeichen. |
| `SevenSegmentControlPackage` | VS-Package-Klasse (Infrastruktur; bei Nutzung als Erweiterung irrelevant für Laufzeit der Controls). |

---

<a name="4-darstellung--bitmuster"></a>
## 4. Darstellung & Bitmuster

Jedes Segment wird durch ein Bit in einem Integer repräsentiert:
- Bit 0: Oberes Segment
- Bit 1: Oben links
- Bit 2: Oben rechts
- Bit 3: Mittleres Segment
- Bit 4: Unten links
- Bit 5: Unten rechts
- Bit 6: Unteres Segment

Beispiel: Ziffer "0" (`CharacterPattern.Zero = &H77` = Binär `0b1110111`) – alle bis auf das mittlere Segment aktiv.

---

<a name="5-klassen-im-detail"></a>
## 5. Klassen im Detail

<a name="51-singledigit"></a>
### 5.1. SingleDigit

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

<a name="52-multidigit"></a>
### 5.2. MultiDigit

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

---

<a name="6-unterstützte-zeichen"></a>
## 6. Unterstützte Zeichen

(Je nach Lesbarkeit auf 7 Segmenten angepasst.)
- Ziffern: 0–9
- Buchstaben (Auswahl): A B C c d E F G H h i J L N o P Q R T U u Y
- Symbole: - _ ° ' " = ≡ ¬ [ ] { } (Mapping einzelner Varianten)
- Punkt '.' (als Dezimalpunkt)
- Doppelpunkt (separat über `ShowColon/ColonActive` beim `SingleDigit`)

Nicht eindeutig darstellbare Buchstaben werden approximiert (z. B. `S` nutzt Muster der „5“).

---

<a name="7-wichtige-interne-aspekte"></a>
## 7. Wichtige interne Aspekte

- Zeichnen erfolgt in `OnPaint` (bzw. Paint-Handler) mit Anti-Aliasing.
- Kursivdarstellung mittels `Matrix.Shear`.
- Segmentgeometrie wird bei Änderung von `SegmentWidth` neu berechnet (`CalculatePoints`).
- `DoubleBuffered` aktiv für flimmerarmes Rendern.
- `CustomBitPattern` hat Vorrang, wenn direkt gesetzt (bis `DigitValue` erneut geändert wird).

---

<a name="8-beispielverwendung-winforms"></a>
## 8. Beispielverwendung (WinForms)

<a name="81-einzelnes-digit"></a>
### 8.1. Einzelnes Digit

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

<a name="82-mehrere-digits-z-b-anzeige-einer-spannung"></a>
### 8.2. Mehrere Digits (z. B. Anzeige einer Spannung)

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

<a name="83-direkte-bitsteuerung"></a>
### 8.3. Direkte Bitsteuerung

```vbnet
' Mittleres Segment + unteres Segment einschalten
seg.CustomBitPattern = &H48 ' Beispiel (abhängig von Bitdefinitionen)
```

---

<a name="9-design-time-unterstützung"></a>
## 9. Design-Time Unterstützung

- `Category`, `Description`, `Browsable` Attribute für saubere PropertyGrid-Darstellung.
- Toolbox-Bitmap via `ToolboxBitmap`-Attribut (falls Ressource vorhanden).
- Ausgeblendete Standard-Properties (`Font`, `Text`, `BackgroundImage`, etc.), um Fokus auf funktionale Anzeigeeigenschaften zu legen.

---

<a name="10-performancehinweise"></a>
## 10. Performancehinweise

- Rendering ist leichtgewichtig für moderate Digit-Anzahlen (< 50).
- Für sehr große `DigitCount` ggf. DoubleBuffer auf Containerform aktivieren.
- Minimieren von unnötigen `Invalidate()`-Aufrufen bei Massenupdates (kaskadierte Set-Operationen bündeln).

---

<a name="11-erweiterungsideen"></a>
## 11. Erweiterungsideen

- Unterstützung für animiertes Dimmen/Fading.
- Mehrfarbige Segmente / Gradient.
- Unterstützung für Hex-Werte (A–F direkter Modus).
- Optionale automatische Skalierung der Segmentbreite anhand Controlgröße.
- Unterstützung für zusätzliche Symbole (°C, Ω, …) via zusammengesetzte Segmente.
- Externes Mapping bereitstellen (Benutzerdefinierte Glyph-Tabelle).

---

<a name="12-fehlerquellen--bekannte-einschränkungen"></a>
## 12. Fehlerquellen / Bekannte Einschränkungen

- Einige Buchstaben sind schwer eindeutig darstellbar (z. B. K, M, W, X, Z fehlen).
- `Value`-Parsing setzt voraus, dass Dezimalpunkte immer einem vorherigen Digit zugeordnet werden können.
- Kein automatisches Trimmen oder Overflow-Hinweis, wenn `Value` länger als `DigitCount` ist (überschüssige Zeichen werden ignoriert).
- Kein Right-To-Left Support (explizit ausgeblendet).

---

<a name="13-styling-empfehlungen"></a>
## 13. Styling-Empfehlungen

| Ziel | Einstellung |
|------|-------------|
| Klassischer LED-Look (Grün) | `ForeColor = Color.Lime`, `InactiveColor = Color.FromArgb(30, 60, 30)`, `BackColor = Color.Black` |
| Rotes Retro-Display | `ForeColor = Color.Red`, `InactiveColor = Color.FromArgb(70, 0, 0)`, `BackColor = Color.Black` |
| Modern (Bernstein) | `ForeColor = Color.Orange`, `InactiveColor = Color.FromArgb(80, 40, 0)` |

---

<a name="14-testideen"></a>
## 14. Testideen

- Einzelzeichen-Sequenz 0–9 und A–Z durchlaufen.
- `DigitCount` dynamisch ändern (Resize-Verhalten prüfen).
- Performance-Test mit 50–100 Digits.
- Randfälle: Leerstring, nur Dezimalpunkte, ungültige Zeichen.

---

<a name="15-wartung--codequalität"></a>
## 15. Wartung / Codequalität

Empfohlene nächste Schritte:

- XML-Dokumentationskommentare für alle öffentlichen Members komplettieren.
- Unit-Tests für Mapping `DigitValue -> BitPattern`.
- Refactoring: Separates Mapping-Modul (Dictionary) statt `Select Case`.
- Optional: Caching der berechneten Segment-Punkte bei Größenänderungen.

---

<a name="16-weitere-literatur"></a>
## 16. Weitere Literatur

- [Erstellen eines Windows Forms-Toolbox-Steuerelements](https://docs.microsoft.com/de-de/visualstudio/extensibility/creating-a-windows-forms-toolbox-control?view=vs-2022)
- [ToolboxBitmapAttribute Konstruktoren](https://learn.microsoft.com/de-de/dotnet/api/system.drawing.toolboxbitmapattribute.-ctor?view=dotnet-plat-ext-7.0#system-drawing-toolboxbitmapattribute-ctor(system-type-system-string))
- [Entwickeln benutzerdefinierter Windows Forms-Steuerelemente mit .NET Framework](https://learn.microsoft.com/de-de/dotnet/desktop/winforms/controls/developing-custom-windows-forms-controls?view=netframeworkdesktop-4.8)
- [Control-Techniken: Eigenes Toolboxicon für Steuerelement](https://www.vb-paradise.de/index.php/Thread/123746-Control-Techniken-Eigenes-Toolboxicon-f%C3%BCr-Steuerelement/)

