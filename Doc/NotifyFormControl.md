# NotifyFormControl

## Einführung

Der ursprüngliche Code zu diesem Projekt stammt aus den Tiefen des Internets.

Leider ist die Quelle offensichtlich nicht mehr verfügbar.

Falls jemand die ursprüngliche Quelle kennt oder finden sollte, dann bitte eine Nachricht an mich damit ich diese hier benennen kann.

---

## Übersicht

Das Projekt `NotifyFormControl` stellt eine wiederverwendbare Komponente (`NotifyForm`) zur Anzeige von Benachrichtigungs-Popups (ähnlich "Toast" bzw. Hinweisfenstern) bereit. Diese Benachrichtigungsfenster erscheinen ohne Rahmen am unteren rechten Bildschirmrand, blenden weich ein und (optional) nach Ablauf einer definierten Zeit wieder aus.

Hauptmerkmale:

- Einfache API über die Komponente `NotifyForm` (Properties setzen, `Show()` aufrufen)
- Verschiedene vordefinierte Designs (hell, farbig, dunkel)
- Verschiedene Symbolarten (Information, Frage, Fehler, Warnung)
- Animiertes Ein-/Ausblenden und seitliches Einschieben
- Automatisches Schließen nach definierter Millisekunden-Dauer (optional)

---

## Anwendungsfälle

Typische Einsatzszenarien:

- Hinweis auf erfolgreiche oder fehlgeschlagene Operationen
- Warnungen oder kritische Fehlerhinweise
- Informations-Popups (z. B. nach Speichern, Export, Verbindung hergestellt)
- Nachfrage-Situationen (Frage-Symbol; allerdings kein Eingabe-/Bestätigungsdialog – rein passiv)

---

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

## Threading / Auto-Close

- Ein Hintergrund-Thread (`CloseThread`) wird beim `Load`-Event des Fensters gestartet.
- Dieser wartet blockierend (`Thread.Sleep(ShowTime)`) und ruft danach per Invoke `Close()`.
- Fade-Out-Prozess läuft dann regulär im `Closing`.

> **Hinweis:**
>
>Der Einsatz eines eigenen Threads ist funktional, könnte jedoch moderner durch `Task.Run` + `Await Task.Delay` ersetzt werden (kein harter Threadblock, bessere Cancellation-Möglichkeiten).

---

## Anpassungsmöglichkeiten / Erweiterungsideen

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

## Technische Hinweise / Codeanalyse

- Nutzung statischer (Shared) Felder in `FormTemplate`: Kollisionspotenzial bei mehreren schnell aufeinanderfolgenden `Show()`-Aufrufen mit unterschiedlichen Designs/Symbolen.
- `Thread.Sleep` im UI-nahen Kontext (Animation) halbwegs unkritisch, aber modernisierbar.
- Keine Fehlerbehandlung bei Ressourcenladefehlern (`My.Resources.*`).
- `RichTextBox` wird als reines Anzeigeelement verwendet – evtl. durch `Label` + automatisches Wrapping ersetzbar (geringerer Overhead).
- Animation erfolgt blockierend in Schleifen – könnte durch `Timer` oder asynchrone Routinen entlastet werden.
- Close-Button nutzt `Label`; ein Button mit AccessibleName wäre barrierefreier.

---

## Barrierefreiheit

Verbesserbare Punkte:

- Fokussteuerung (aktueller Fokus nach Öffnen unklar).
- Tastaturbedienung (ESC zum Schließen implementieren).
- Screenreader-Kompatibilität (Role + AccessibleName/Description setzen).

---

## Weitere Literatur

- [Erstellen eines Windows Forms-Toolbox-Steuerelements](https://docs.microsoft.com/de-de/visualstudio/extensibility/creating-a-windows-forms-toolbox-control?view=vs-2022)
- [Infos zur ControlStyles Enumeration](https://learn.microsoft.com/de-de/dotnet/api/system.windows.forms.controlstyles?redirectedfrom=MSDN&view=netframework-4.7.2)
- [Control-Techniken: Eigenes Toolboxicon für Steuerelement](https://www.vb-paradise.de/index.php/Thread/123746-Control-Techniken-Eigenes-Toolboxicon-f%C3%BCr-Steuerelement/)
