# NotifyFormControl

## Einführung

Der ursprüngliche Code zu diesem Projekt stammt aus den Tiefen des Internets.

Leider ist die Quelle offensichtlich nicht mehr verfügbar.

Falls jemand die ursprüngliche Quelle kennt oder finden sollte, dann bitte eine Nachricht an mich damit ich diese hier benennen kann.

---

## Inhaltsverzeichnis

1. [Übersicht](#1-übersicht)
2. [Anwendungsfälle](#2-anwendungsfälle)
3. [Architektur](#3-architektur)
4. [Lebenszyklus eines Popups](#4-lebenszyklus-eines-popups)
5. [Öffentliche API (`NotifyForm`)](#5-öffentliche-api-notifyform)
6. [Enums](#6-enums)
   - [NotifyFormDesign](#61-notifyformdesign)
   - [NotifyFormStyle](#62-notifyformstyle)
 7. [Innerer Aufbau des Fensters (`FormTemplate`)](#7-innerer-aufbau-des-fensters-formtemplate)
 8. [Ablauf von `Show()` (vereinfacht)](#8--ablauf-von-show-vereinfacht)
 9. [Threading / Auto-Close](#9-threading--auto-close)
 10. [Beispielverwendung](#10-beispielverwendung)
    - [Einfaches Informationsfenster](#101-einfaches-informationsfenster)
    - [Warnung ohne automatisches Schließen](#102-warnung-ohne-automatisches-schließen)
    - [Farbiges Fehlerfenster](#103-farbiges-fehlerfenster)
 11. [Anpassungsmöglichkeiten / Erweiterungsideen](#11-anpassungsmöglichkeiten--erweiterungsideen)
 12. [Technische Hinweise / Codeanalyse](#12-technische-hinweise--codeanalyse)
 13. [Barrierefreiheit](#13-barrierefreiheit)
 14. [Testempfehlungen](#14-testempfehlungen)
 15. [Bekannte Einschränkungen](#15-bekannte-einschränkungen)
 16. [Sicherheitsaspekte](#16-sicherheitsaspekte)
 17. [Wartung / Refactoring-Vorschläge](#17-wartung--refactoring-vorschläge)
 18. [Weitere Literatur](#18-weitere-literatur)

---

<a name="1-übersicht"></a>
## 1. Übersicht

Das Projekt `NotifyFormControl` stellt eine wiederverwendbare Komponente (`NotifyForm`) zur Anzeige von Benachrichtigungs-Popups (ähnlich "Toast" bzw. Hinweisfenstern) bereit. Diese Benachrichtigungsfenster erscheinen ohne Rahmen am unteren rechten Bildschirmrand, blenden weich ein und (optional) nach Ablauf einer definierten Zeit wieder aus.

Hauptmerkmale:

- Einfache API über die Komponente `NotifyForm` (Properties setzen, `Show()` aufrufen)
- Verschiedene vordefinierte Designs (hell, farbig, dunkel)
- Verschiedene Symbolarten (Information, Frage, Fehler, Warnung)
- Animiertes Ein-/Ausblenden und seitliches Einschieben
- Automatisches Schließen nach definierter Millisekunden-Dauer (optional)

---

<a name="2-anwendungsfälle"></a>
## 2. Anwendungsfälle

Typische Einsatzszenarien:

- Hinweis auf erfolgreiche oder fehlgeschlagene Operationen
- Warnungen oder kritische Fehlerhinweise
- Informations-Popups (z. B. nach Speichern, Export, Verbindung hergestellt)
- Nachfrage-Situationen (Frage-Symbol; allerdings kein Eingabe-/Bestätigungsdialog – rein passiv)

---

<a name="3-architektur"></a>
## 3. Architektur

| Bestandteil | Beschreibung |
|-------------|-------------|
| `NotifyForm` | Öffentliche Komponente (Inherits `Component`), stellt Properties + `Show()` bereit. |
| `NotifyFormEnums.vb` | Definition der Enums `NotifyFormDesign` und `NotifyFormStyle`. |
| `FormTemplate` (innere Klasse) | Interne Fensterklasse (`Form`) zur tatsächlichen Darstellung des Popups. |
| Ressourcen (`My.Resources.*`) | Enthalten Bitmaps für Symbole (Information, Warning, Question, CriticalError). |
| `NotifyFormControlPackage` | VS Package (Infrastruktur; für Design-Time / Erweiterungsintegration). |

---

<a name="4-lebenszyklus-eines-popups"></a>
## 4. Lebenszyklus eines Popups

1. Konfiguration der Komponente (`Title`, `Message`, `Design`, `Style`, `ShowTime`).
2. Aufruf von `Show()`:
   - Symbol wird anhand `Style` zugewiesen.
   - Text und Titel werden gesetzt.
   - Designfarben werden angewendet.
   - Instanz der inneren `FormTemplate` wird initialisiert + angezeigt.
3. Fenster blendet mit Fade-In + seitlichem Einschieben ein.
4. Optionaler Hintergrund-Thread wartet (`ShowTime` > 0) und initiiert Schließen.
5. Fade-Out Animation läuft im `Form_Closing`.

---

<a name="5-öffentliche-api-notifyform"></a>
## 5. Öffentliche API (`NotifyForm`)

Eigenschaft / Methode | Typ | Beschreibung
----------------------|-----|-------------
`Title` | `String` | Titelzeile des Fensters (Standard: "Titel").
`Message` | `String` | Anzeigetext (Standard: "Mitteilung").
`Design` | `NotifyFormDesign` | Helligkeit/Farbstimmung (`Bright`, `Colorful`, `Dark`).
`Style` | `NotifyFormStyle` | Symbolauswahl (`Information`, `Question`, `CriticalError`, `Exclamation`).
`ShowTime` | `Integer` (ms) | Dauer bis Auto-Close. `0` = bleibt offen bis manuell geschlossen.
`Show()` | `Sub` | Erzeugt und zeigt das Popup mit aktuellen Einstellungen.

---

<a name="6-enums"></a>
## 6. Enums

<a name="61-notifyformdesign"></a>
### 6.1. NotifyFormDesign

- `Bright` – Weiß / Grau, neutral
- `Colorful` – Helle Blau-/Türkistöne
- `Dark` – Dunkle Grau-/Brauntöne

<a name="62-notifyformstyle"></a>
### 6.2. NotifyFormStyle

- `Information` – Infosymbol
- `Question` – Fragezeichen
- `CriticalError` – Fehlersymbol (rot)
- `Exclamation` – Warnsymbol / Ausrufezeichen

---

<a name="7-innerer-aufbau-des-fensters-formtemplate"></a>
## 7. Innerer Aufbau des Fensters (`FormTemplate`)

Element | Funktion
--------|---------
`LabelTitle` | Titelzeile
`LabelClose` | Schließen-Schaltfläche ("X")
`PictureBoxImage` | Symbolanzeige
`RichTextBoxMessage` | Mehrzeiliger Hinweistext (ReadOnly)
`PanelTitle` | Oberer Balken (Titelbereich + Close)
`PanelSpacer` | Vertikaler Trenner zwischen Symbol und Text

Besondere Punkte:

- Kein Fensterrahmen (`FormBorderStyle.None`)
- Positionierung rechts unten relativ zur `Screen.PrimaryScreen.WorkingArea`
- Animation: seitliches Hereingleiten + Fade-In (Opacity von 0.1 -> 1.0)
- Fade-Out im `Closing`-Handler (Opacity in Stufen reduziert)
- Ressourcenbereinigung in `Dispose` implementiert

<a name="8--ablauf-von-show-vereinfacht"></a>
## 8.  Ablauf von `Show()` (vereinfacht)

```vbnet
Public Sub Show()
    FormTemplate.Image = SetFormImage()
    FormTemplate.Title = Title
    FormTemplate.Message = Message
    FormTemplate.ShowTime = ShowTime
    SetFormDesign()          ' Wählt Farbschema & initialisiert Form
End Sub
```

`SetFormDesign()` ruft abhängig vom Enum eine der Methoden auf:
- `SetFormDesignBright()`
- `SetFormDesignColorful()`
- `SetFormDesignDark()`

Jede Methode setzt statische Felder (`BackgroundColor`, `FontColor`, etc.) und erzeugt danach eine neue Instanz `FormTemplate`, ruft `Initialize()`, wodurch das Fenster erstellt und direkt angezeigt wird.

---

<a name="9-threading--auto-close"></a>
## 9. Threading / Auto-Close

- Ein Hintergrund-Thread (`CloseThread`) wird beim `Load`-Event des Fensters gestartet.
- Dieser wartet blockierend (`Thread.Sleep(ShowTime)`) und ruft danach per Invoke `Close()`.
- Fade-Out-Prozess läuft dann regulär im `Closing`.

> **Hinweis:**
>
>Der Einsatz eines eigenen Threads ist funktional, könnte jedoch moderner durch `Task.Run` + `Await Task.Delay` ersetzt werden (kein harter Threadblock, bessere Cancellation-Möglichkeiten).

<a name="10-beispielverwendung"></a>
## 10. Beispielverwendung

<a name="101-einfaches-informationsfenster"></a>
### 10.1. Einfaches Informationsfenster

```vbnet
Dim nf As New NotifyFormControl.NotifyForm() With {
    .Title = "Information",
    .Message = "Vorgang erfolgreich abgeschlossen.",
    .Design = NotifyFormControl.NotifyFormDesign.Bright,
    .Style = NotifyFormControl.NotifyFormStyle.Information,
    .ShowTime = 4000
}
nf.Show()
```
 <a name="102-warnung-ohne-automatisches-schließen"></a>
### 10.2. Warnung ohne automatisches Schließen

```vbnet
Dim warn As New NotifyFormControl.NotifyForm() With {
    .Title = "Achtung",
    .Message = "Konfiguration unvollständig.",
    .Design = NotifyFormControl.NotifyFormDesign.Dark,
    .Style = NotifyFormControl.NotifyFormStyle.Exclamation,
    .ShowTime = 0 ' Manuell schließen
}
warn.Show()
```

<a name="103-farbiges-fehlerfenster"></a>
### 10.3. Farbiges Fehlerfenster

```vbnet
Dim err As New NotifyFormControl.NotifyForm() With {
    .Title = "Fehler",
    .Message = "Übertragung fehlgeschlagen!",
    .Design = NotifyFormControl.NotifyFormDesign.Colorful,
    .Style = NotifyFormControl.NotifyFormStyle.CriticalError,
    .ShowTime = 6000
}
err.Show()
```

---

<a name="11-anpassungsmöglichkeiten--erweiterungsideen"></a>
## 11. Anpassungsmöglichkeiten / Erweiterungsideen

1. Weitere Designs (z. B. Transparent / Acrylic / System Theme).
2. Mehr Interaktivität: Buttons (OK / Abbrechen) oder Hyperlinks.
3. Unterstützung mehrerer gleichzeitiger Popups (Stacking / Queueing oben übereinander).
4. Verwendung von `Task.Delay` statt `Thread.Sleep` für bessere Responsiveness.
5. Konfigurierbare Animationsdauer (Fade-In/Out Geschwindigkeit) als Property.
6. Unterstützung für mehrere Monitore (Fenster auf aktivem Monitor positionieren).
7. DPI-Awareness / Skalierung für High-DPI Displays prüfen.
8. Option: Fortschrittsanzeige integrieren.
9. Logging-Hook (Callback-Event bei Anzeigen/Schließen).
10. Wiederverwendung statt statischer Felder (aktuell verhindern statische Felder parallele unabhängige Fensterinstanzen mit unterschiedlichen Designs).

---

<a name="12-technische-hinweise--codeanalyse"></a>
## 12. Technische Hinweise / Codeanalyse

- Nutzung statischer (Shared) Felder in `FormTemplate`: Kollisionspotenzial bei mehreren schnell aufeinanderfolgenden `Show()`-Aufrufen mit unterschiedlichen Designs/Symbolen.
- `Thread.Sleep` im UI-nahen Kontext (Animation) halbwegs unkritisch, aber modernisierbar.
- Keine Fehlerbehandlung bei Ressourcenladefehlern (`My.Resources.*`).
- `RichTextBox` wird als reines Anzeigeelement verwendet – evtl. durch `Label` + automatisches Wrapping ersetzbar (geringerer Overhead).
- Animation erfolgt blockierend in Schleifen – könnte durch `Timer` oder asynchrone Routinen entlastet werden.
- Close-Button nutzt `Label`; ein Button mit AccessibleName wäre barrierefreier.

---

<a name="13-barrierefreiheit"></a>
## 13. Barrierefreiheit

Verbesserbare Punkte:

- Fokussteuerung (aktueller Fokus nach Öffnen unklar). 
- Tastaturbedienung (ESC zum Schließen implementieren).
- Screenreader-Kompatibilität (Role + AccessibleName/Description setzen).

---

<a name="14-testempfehlungen"></a>
## 14. Testempfehlungen

Art | Beschreibung
----|-------------
Unit | Prüfen: `ShowTime=0` schließt nicht automatisch.
UI | Mehrere Popups nacheinander – Designwechsel korrekt?
UI | Very long `Message` -> Layout/Scroll prüfen.
UI | Hochauflösender Monitor / Skalierung >125%.
Perf | 20 Popups hintereinander (Ressourcenfreigabe / GDI Handles).
Threading | Race: `Show()` schnell mehrfach nacheinander.

---

<a name="15-bekannte-einschränkungen"></a>
## 15. Bekannte Einschränkungen

- Nur eine konsistent instanzierte Design-/Symbol-Konfiguration gleichzeitig sinnvoll (Shared Felder!).
- Keine Warteschlange / kein Overlapping-Management.
- Kein Multi-Monitor Bewusstsein.
- Keine Cancellation des Auto-Close Threads.

---

<a name="16-sicherheitsaspekte"></a>
## 16. Sicherheitsaspekte

- Kein externes Laden unsicherer Ressourcen.
- Keine Benutzereingaben (rein ausgebendes UI) – geringes Risiko.

---

<a name="17-wartung--refactoring-vorschläge"></a>
## 17. Wartung / Refactoring-Vorschläge

Priorität | Vorschlag
---------|----------
Hoch | Shared Felder in Instanzfelder umwandeln, damit mehrere Fenster parallel konsistent angezeigt werden können.
Mittel | Umstellung auf asynchrones Muster (Tasks + Await) für Auto-Close / Animation.
Mittel | Extraktion einer `INotifyFormTheme` Schnittstelle zur einfachen Theme-Erweiterung.
Niedrig | Ersetzen `RichTextBox` durch `Label` + Textumbruch.
Niedrig | Accessibility-Verbesserungen.

---

<a name="18-weitere-literatur"></a>
## 18. Weitere Literatur

- [Erstellen eines Windows Forms-Toolbox-Steuerelements](https://docs.microsoft.com/de-de/visualstudio/extensibility/creating-a-windows-forms-toolbox-control?view=vs-2022)
- [Infos zur ControlStyles Enumeration](https://learn.microsoft.com/de-de/dotnet/api/system.windows.forms.controlstyles?redirectedfrom=MSDN&view=netframework-4.7.2)
- [Control-Techniken: Eigenes Toolboxicon für Steuerelement](https://www.vb-paradise.de/index.php/Thread/123746-Control-Techniken-Eigenes-Toolboxicon-f%C3%BCr-Steuerelement/)
