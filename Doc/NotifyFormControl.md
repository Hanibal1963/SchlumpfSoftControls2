# NotifyFormControl

## Einf�hrung

Der urspr�ngliche Code zu diesem Projekt stammt aus den Tiefen des Internets.

Leider ist die Quelle offensichtlich nicht mehr verf�gbar.

Falls jemand die urspr�ngliche Quelle kennt oder finden sollte, dann bitte eine Nachricht an mich damit ich diese hier benennen kann.

---

## Inhaltsverzeichnis

1. [�bersicht](#1-�bersicht)
2. [Anwendungsf�lle](#2-anwendungsf�lle)
3. [Architektur](#3-architektur)
4. [Lebenszyklus eines Popups](#4-lebenszyklus-eines-popups)
5. [�ffentliche API (`NotifyForm`)](#5-�ffentliche-api-notifyform)
6. [Enums](#6-enums)
   - [NotifyFormDesign](#61-notifyformdesign)
   - [NotifyFormStyle](#62-notifyformstyle)
 7. [Innerer Aufbau des Fensters (`FormTemplate`)](#7-innerer-aufbau-des-fensters-formtemplate)
 8. [Ablauf von `Show()` (vereinfacht)](#8--ablauf-von-show-vereinfacht)
 9. [Threading / Auto-Close](#9-threading--auto-close)
 10. [Beispielverwendung](#10-beispielverwendung)
    - [Einfaches Informationsfenster](#101-einfaches-informationsfenster)
    - [Warnung ohne automatisches Schlie�en](#102-warnung-ohne-automatisches-schlie�en)
    - [Farbiges Fehlerfenster](#103-farbiges-fehlerfenster)
 11. [Anpassungsm�glichkeiten / Erweiterungsideen](#11-anpassungsm�glichkeiten--erweiterungsideen)
 12. [Technische Hinweise / Codeanalyse](#12-technische-hinweise--codeanalyse)
 13. [Barrierefreiheit](#13-barrierefreiheit)
 14. [Testempfehlungen](#14-testempfehlungen)
 15. [Bekannte Einschr�nkungen](#15-bekannte-einschr�nkungen)
 16. [Sicherheitsaspekte](#16-sicherheitsaspekte)
 17. [Wartung / Refactoring-Vorschl�ge](#17-wartung--refactoring-vorschl�ge)
 18. [Weitere Literatur](#18-weitere-literatur)

---

<a name="1-�bersicht"></a>
## 1. �bersicht

Das Projekt `NotifyFormControl` stellt eine wiederverwendbare Komponente (`NotifyForm`) zur Anzeige von Benachrichtigungs-Popups (�hnlich "Toast" bzw. Hinweisfenstern) bereit. Diese Benachrichtigungsfenster erscheinen ohne Rahmen am unteren rechten Bildschirmrand, blenden weich ein und (optional) nach Ablauf einer definierten Zeit wieder aus.

Hauptmerkmale:

- Einfache API �ber die Komponente `NotifyForm` (Properties setzen, `Show()` aufrufen)
- Verschiedene vordefinierte Designs (hell, farbig, dunkel)
- Verschiedene Symbolarten (Information, Frage, Fehler, Warnung)
- Animiertes Ein-/Ausblenden und seitliches Einschieben
- Automatisches Schlie�en nach definierter Millisekunden-Dauer (optional)

---

<a name="2-anwendungsf�lle"></a>
## 2. Anwendungsf�lle

Typische Einsatzszenarien:

- Hinweis auf erfolgreiche oder fehlgeschlagene Operationen
- Warnungen oder kritische Fehlerhinweise
- Informations-Popups (z. B. nach Speichern, Export, Verbindung hergestellt)
- Nachfrage-Situationen (Frage-Symbol; allerdings kein Eingabe-/Best�tigungsdialog � rein passiv)

---

<a name="3-architektur"></a>
## 3. Architektur

| Bestandteil | Beschreibung |
|-------------|-------------|
| `NotifyForm` | �ffentliche Komponente (Inherits `Component`), stellt Properties + `Show()` bereit. |
| `NotifyFormEnums.vb` | Definition der Enums `NotifyFormDesign` und `NotifyFormStyle`. |
| `FormTemplate` (innere Klasse) | Interne Fensterklasse (`Form`) zur tats�chlichen Darstellung des Popups. |
| Ressourcen (`My.Resources.*`) | Enthalten Bitmaps f�r Symbole (Information, Warning, Question, CriticalError). |
| `NotifyFormControlPackage` | VS Package (Infrastruktur; f�r Design-Time / Erweiterungsintegration). |

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
4. Optionaler Hintergrund-Thread wartet (`ShowTime` > 0) und initiiert Schlie�en.
5. Fade-Out Animation l�uft im `Form_Closing`.

---

<a name="5-�ffentliche-api-notifyform"></a>
## 5. �ffentliche API (`NotifyForm`)

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

- `Bright` � Wei� / Grau, neutral
- `Colorful` � Helle Blau-/T�rkist�ne
- `Dark` � Dunkle Grau-/Braunt�ne

<a name="62-notifyformstyle"></a>
### 6.2. NotifyFormStyle

- `Information` � Infosymbol
- `Question` � Fragezeichen
- `CriticalError` � Fehlersymbol (rot)
- `Exclamation` � Warnsymbol / Ausrufezeichen

---

<a name="7-innerer-aufbau-des-fensters-formtemplate"></a>
## 7. Innerer Aufbau des Fensters (`FormTemplate`)

Element | Funktion
--------|---------
`LabelTitle` | Titelzeile
`LabelClose` | Schlie�en-Schaltfl�che ("X")
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
    SetFormDesign()          ' W�hlt Farbschema & initialisiert Form
End Sub
```

`SetFormDesign()` ruft abh�ngig vom Enum eine der Methoden auf:
- `SetFormDesignBright()`
- `SetFormDesignColorful()`
- `SetFormDesignDark()`

Jede Methode setzt statische Felder (`BackgroundColor`, `FontColor`, etc.) und erzeugt danach eine neue Instanz `FormTemplate`, ruft `Initialize()`, wodurch das Fenster erstellt und direkt angezeigt wird.

---

<a name="9-threading--auto-close"></a>
## 9. Threading / Auto-Close

- Ein Hintergrund-Thread (`CloseThread`) wird beim `Load`-Event des Fensters gestartet.
- Dieser wartet blockierend (`Thread.Sleep(ShowTime)`) und ruft danach per Invoke `Close()`.
- Fade-Out-Prozess l�uft dann regul�r im `Closing`.

> **Hinweis:**
>
>Der Einsatz eines eigenen Threads ist funktional, k�nnte jedoch moderner durch `Task.Run` + `Await Task.Delay` ersetzt werden (kein harter Threadblock, bessere Cancellation-M�glichkeiten).

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
 <a name="102-warnung-ohne-automatisches-schlie�en"></a>
### 10.2. Warnung ohne automatisches Schlie�en

```vbnet
Dim warn As New NotifyFormControl.NotifyForm() With {
    .Title = "Achtung",
    .Message = "Konfiguration unvollst�ndig.",
    .Design = NotifyFormControl.NotifyFormDesign.Dark,
    .Style = NotifyFormControl.NotifyFormStyle.Exclamation,
    .ShowTime = 0 ' Manuell schlie�en
}
warn.Show()
```

<a name="103-farbiges-fehlerfenster"></a>
### 10.3. Farbiges Fehlerfenster

```vbnet
Dim err As New NotifyFormControl.NotifyForm() With {
    .Title = "Fehler",
    .Message = "�bertragung fehlgeschlagen!",
    .Design = NotifyFormControl.NotifyFormDesign.Colorful,
    .Style = NotifyFormControl.NotifyFormStyle.CriticalError,
    .ShowTime = 6000
}
err.Show()
```

---

<a name="11-anpassungsm�glichkeiten--erweiterungsideen"></a>
## 11. Anpassungsm�glichkeiten / Erweiterungsideen

1. Weitere Designs (z. B. Transparent / Acrylic / System Theme).
2. Mehr Interaktivit�t: Buttons (OK / Abbrechen) oder Hyperlinks.
3. Unterst�tzung mehrerer gleichzeitiger Popups (Stacking / Queueing oben �bereinander).
4. Verwendung von `Task.Delay` statt `Thread.Sleep` f�r bessere Responsiveness.
5. Konfigurierbare Animationsdauer (Fade-In/Out Geschwindigkeit) als Property.
6. Unterst�tzung f�r mehrere Monitore (Fenster auf aktivem Monitor positionieren).
7. DPI-Awareness / Skalierung f�r High-DPI Displays pr�fen.
8. Option: Fortschrittsanzeige integrieren.
9. Logging-Hook (Callback-Event bei Anzeigen/Schlie�en).
10. Wiederverwendung statt statischer Felder (aktuell verhindern statische Felder parallele unabh�ngige Fensterinstanzen mit unterschiedlichen Designs).

---

<a name="12-technische-hinweise--codeanalyse"></a>
## 12. Technische Hinweise / Codeanalyse

- Nutzung statischer (Shared) Felder in `FormTemplate`: Kollisionspotenzial bei mehreren schnell aufeinanderfolgenden `Show()`-Aufrufen mit unterschiedlichen Designs/Symbolen.
- `Thread.Sleep` im UI-nahen Kontext (Animation) halbwegs unkritisch, aber modernisierbar.
- Keine Fehlerbehandlung bei Ressourcenladefehlern (`My.Resources.*`).
- `RichTextBox` wird als reines Anzeigeelement verwendet � evtl. durch `Label` + automatisches Wrapping ersetzbar (geringerer Overhead).
- Animation erfolgt blockierend in Schleifen � k�nnte durch `Timer` oder asynchrone Routinen entlastet werden.
- Close-Button nutzt `Label`; ein Button mit AccessibleName w�re barrierefreier.

---

<a name="13-barrierefreiheit"></a>
## 13. Barrierefreiheit

Verbesserbare Punkte:

- Fokussteuerung (aktueller Fokus nach �ffnen unklar). 
- Tastaturbedienung (ESC zum Schlie�en implementieren).
- Screenreader-Kompatibilit�t (Role + AccessibleName/Description setzen).

---

<a name="14-testempfehlungen"></a>
## 14. Testempfehlungen

Art | Beschreibung
----|-------------
Unit | Pr�fen: `ShowTime=0` schlie�t nicht automatisch.
UI | Mehrere Popups nacheinander � Designwechsel korrekt?
UI | Very long `Message` -> Layout/Scroll pr�fen.
UI | Hochaufl�sender Monitor / Skalierung >125%.
Perf | 20 Popups hintereinander (Ressourcenfreigabe / GDI Handles).
Threading | Race: `Show()` schnell mehrfach nacheinander.

---

<a name="15-bekannte-einschr�nkungen"></a>
## 15. Bekannte Einschr�nkungen

- Nur eine konsistent instanzierte Design-/Symbol-Konfiguration gleichzeitig sinnvoll (Shared Felder!).
- Keine Warteschlange / kein Overlapping-Management.
- Kein Multi-Monitor Bewusstsein.
- Keine Cancellation des Auto-Close Threads.

---

<a name="16-sicherheitsaspekte"></a>
## 16. Sicherheitsaspekte

- Kein externes Laden unsicherer Ressourcen.
- Keine Benutzereingaben (rein ausgebendes UI) � geringes Risiko.

---

<a name="17-wartung--refactoring-vorschl�ge"></a>
## 17. Wartung / Refactoring-Vorschl�ge

Priorit�t | Vorschlag
---------|----------
Hoch | Shared Felder in Instanzfelder umwandeln, damit mehrere Fenster parallel konsistent angezeigt werden k�nnen.
Mittel | Umstellung auf asynchrones Muster (Tasks + Await) f�r Auto-Close / Animation.
Mittel | Extraktion einer `INotifyFormTheme` Schnittstelle zur einfachen Theme-Erweiterung.
Niedrig | Ersetzen `RichTextBox` durch `Label` + Textumbruch.
Niedrig | Accessibility-Verbesserungen.

---

<a name="18-weitere-literatur"></a>
## 18. Weitere Literatur

- [Erstellen eines Windows Forms-Toolbox-Steuerelements](https://docs.microsoft.com/de-de/visualstudio/extensibility/creating-a-windows-forms-toolbox-control?view=vs-2022)
- [Infos zur ControlStyles Enumeration](https://learn.microsoft.com/de-de/dotnet/api/system.windows.forms.controlstyles?redirectedfrom=MSDN&view=netframework-4.7.2)
- [Control-Techniken: Eigenes Toolboxicon f�r Steuerelement](https://www.vb-paradise.de/index.php/Thread/123746-Control-Techniken-Eigenes-Toolboxicon-f%C3%BCr-Steuerelement/)
