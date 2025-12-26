# SevenSegmentControl

## Einführung

Ich habe für ein anderes Projekt versucht eine 7-Segmentanzeige zu programmieren.
Nach einigen Fehlversuchen und einer intensive Internetrecherche, bin ich auf GitHub fündig geworden.
([SevenSegment von Dimitry Brant](https://github.com/dbrant/SevenSegment))

Ich habe mich entschlossen den Code in VisualBasic neu zu erstellen da ich mit C# keinerlei Erfahrung habe. 

  ---

## Übersicht

Das Projekt `SevenSegmentControl` stellt zwei Windows Forms Steuerelemente bereit, die klassische 7-Segment-(LED)-Anzeige(n) simulieren:

   - `SingleDigit` – Ein einzelnes 7-Segment-Display mit optionalem Dezimalpunkt und (optionalem) Doppelpunktbereich.
   - `MultiDigit` – Ein Container für mehrere `SingleDigit`-Instanzen zur Darstellung kompletter numerischer/teilweise alphanumerischer Werte.

Der Fokus liegt auf:

- Darstellbarkeit gängiger Ziffern und ausgewählter Buchstaben/Symbole
- Frei einstellbaren Farben (aktiv/inaktiv/Hintergrund)
- Anpassbarer Segmentbreite und Schrägstellung (kursiver Effekt via Shear-Transformation)
- Optionalen Dezimalpunkten und Doppelpunkt (z. B. für Uhr- oder Zeitformat)

---

##  Ziele / Einsatzzweck

Typische Anwendungsfälle:

- Anzeige von Messwerten (Temperatur, Spannung, etc.)
- Digitale Uhren / Timer
- Zähler, einfache Statusanzeigen
- Simulation klassischer Hardwareanzeigen im UI

---

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

---

## Wichtige interne Aspekte

- Zeichnen erfolgt in `OnPaint` (bzw. Paint-Handler) mit Anti-Aliasing.
- Kursivdarstellung mittels `Matrix.Shear`.
- Segmentgeometrie wird bei Änderung von `SegmentWidth` neu berechnet (`CalculatePoints`).
- `DoubleBuffered` aktiv für flimmerarmes Rendern.
- `CustomBitPattern` hat Vorrang, wenn direkt gesetzt (bis `DigitValue` erneut geändert wird).

---

## Performancehinweise

- Rendering ist leichtgewichtig für moderate Digit-Anzahlen (< 50).
- Für sehr große `DigitCount` ggf. DoubleBuffer auf Containerform aktivieren.
- Minimieren von unnötigen `Invalidate()`-Aufrufen bei Massenupdates (kaskadierte Set-Operationen bündeln).

---

## Erweiterungsideen

- Unterstützung für animiertes Dimmen/Fading.
- Mehrfarbige Segmente / Gradient.
- Unterstützung für Hex-Werte (A–F direkter Modus).
- Optionale automatische Skalierung der Segmentbreite anhand Controlgröße.
- Unterstützung für zusätzliche Symbole (°C, Ω, …) via zusammengesetzte Segmente.
- Externes Mapping bereitstellen (Benutzerdefinierte Glyph-Tabelle).

---

## Fehlerquellen / Bekannte Einschränkungen

- Einige Buchstaben sind schwer eindeutig darstellbar (z. B. K, M, W, X, Z fehlen).
- `Value`-Parsing setzt voraus, dass Dezimalpunkte immer einem vorherigen Digit zugeordnet werden können.
- Kein automatisches Trimmen oder Overflow-Hinweis, wenn `Value` länger als `DigitCount` ist (überschüssige Zeichen werden ignoriert).
- Kein Right-To-Left Support (explizit ausgeblendet).

---

## Weitere Literatur

- [Erstellen eines Windows Forms-Toolbox-Steuerelements](https://docs.microsoft.com/de-de/visualstudio/extensibility/creating-a-windows-forms-toolbox-control?view=vs-2022)
- [ToolboxBitmapAttribute Konstruktoren](https://learn.microsoft.com/de-de/dotnet/api/system.drawing.toolboxbitmapattribute.-ctor?view=dotnet-plat-ext-7.0#system-drawing-toolboxbitmapattribute-ctor(system-type-system-string))
- [Entwickeln benutzerdefinierter Windows Forms-Steuerelemente mit .NET Framework](https://learn.microsoft.com/de-de/dotnet/desktop/winforms/controls/developing-custom-windows-forms-controls?view=netframeworkdesktop-4.8)
- [Control-Techniken: Eigenes Toolboxicon für Steuerelement](https://www.vb-paradise.de/index.php/Thread/123746-Control-Techniken-Eigenes-Toolboxicon-f%C3%BCr-Steuerelement/)

