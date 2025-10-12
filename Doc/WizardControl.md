# WizardControl (SchlumpfSoftControls2)

## Einführung

Dieses Steuerelement wurde von mir in Anlehnung an den [Wizard](https://marketplace.visualstudio.com/items?itemName=vs-publisher-106990.RuWizard) von 
[Klaus Rutkowski](https://marketplace.visualstudio.com/publishers/vs-publisher-106990) entwickelt.

Sinn dieses Projekts ist für mich der Lerneffekt sowie eventuelle Anpassungen vornehmen zu können.

---

## Inhaltsverzeichnis

1. [Übersicht](#overview)
2. [Hauptklassen & Dateien](#Hauptklassen--Dateien)
3. [Architektur & Funktionsweise](#architecture-overview)
      - [Wizard (UserControl)](#architecture)
      - [WizardPage](#page-navigation)
      - [Spezialisierte Seiten](#pagestyles)
      - [PagesCollection](#pagescollection)
      - [Events](#events)
      - [Before / AfterSwitchPagesEventArgs](#eventargs)
 4. [Öffentliche Eigenschaften (Auszug)](#properties)
 5. [Typische Verwendung](#usage)
       - [Control platzieren](#getting-started)
       - [Seiten hinzufügen (zur Designzeit oder Laufzeit)](#adding-pages)
       - [Validierung vor Seitenwechsel](#event-handling)
       - [Abschlusslogik](#finish-logic)
       - [Dynamischer Sprung (Wizard verzweigen)](#dynamic-jump)
       - [Custom Pages](#custom-pages)
 6. [Steuerung per Code](#code-control)
 7. [Layout / Rendering Hinweise](#layout)
 8. [Erweiterbarkeit](#extensibility)
 9. [Best Practices](#best-practices)
 10. [Fehlersuche](#troubleshooting)
 11. [Ressourcen](#resources)

---

<a name="overview"></a>
## 1. Übersicht

`WizardControl` stellt ein wiederverwendbares Windows‑Forms Steuerelement bereit, mit dem mehrseitige Assistenten ("Wizards") komfortabel aufgebaut werden können. Es kapselt die Navigation (Zurück / Weiter / Abbrechen / Hilfe bzw. Beenden) sowie das Rendering unterschiedlicher Seitentypen (Welcome, Standard, Finish, Custom) inklusive Header-/Welcome‑Bildern und konfigurierbaren Schriftarten.

Das Control eignet sich für Installations‑, Konfigurations‑ oder Schritt‑für‑Schritt Dialoge, bei denen Benutzer sequenziell durch logische Abschnitte geführt werden.

---

<a name="Hauptklassen--Dateien"></a>
## 2. Hauptklassen & Dateien

| Datei | Typ | Zweck |
|-------|-----|------|
| `Wizard.vb` | `Wizard` (`UserControl`) | Zentrales Steuerelement: Navigation, Darstellung, Events |
| `WizardPage.vb` | `WizardPage` (`Panel`) | Basisklasse für alle Seiten |
| `PageWelcome.vb` | `PageWelcome` | Spezialisierte Willkommensseite |
| `PageStandard.vb` | `PageStandard` | Standardschritt mit Headerleiste |
| `PageFinish.vb` | `PageFinish` | Abschlusseite (Beenden) |
| `PageCustom.vb` | `PageCustom` | Frei gestaltbare Seite ohne vordefiniertes Layout |
| `PagesCollection.vb` | `PagesCollection` | Auflistung von `WizardPage` Instanzen |
| `PageStyle.vb` | `PageStyle` (Enum) | Definiert Seitentypen |
| `AfterSwitchPagesEventArgs.vb` | `AfterSwitchPagesEventArgs` | EventArgs nach Seitenwechsel |
| `BeforeSwitchPagesEventArgs.vb` | `BeforeSwitchPagesEventArgs` | EventArgs vor Seitenwechsel (mit `Cancel`) |

---

<a name="architecture-overview"></a>
## 3. Architektur & Funktionsweise

<a name="architecture"></a>
### 3.1. Wizard (UserControl)

Der `Wizard` hält:
- Eine `PagesCollection` (Eigenschaft `Pages`) mit den enthaltenen `WizardPage` Instanzen
- Die aktuell aktive Seite (`SelectedPage` / `SelectedIndex`)
- Buttons: Zurück, Weiter, Abbrechen/Beenden, Hilfe
- Optionale Bilder: `ImageHeader` (für Standardseiten), `ImageWelcome` (für Welcome/Finish)
- Typ-spezifische Schriftarten: `HeaderFont`, `HeaderTitleFont`, `WelcomeFont`, `WelcomeTitleFont`

Beim Seitenwechsel:
1. `BeforeSwitchPages` wird ausgelöst (Option zur Validierung – kann per `e.Cancel = True` abgebrochen werden)
2. Bei Erfolg: `ActivatePage(newIndex)` -> UI Umschalten
3. `AfterSwitchPages` wird ausgelöst

Die Beschriftung & Funktion des Abbrechen-Buttons ändert sich kontextabhängig:
- Auf `PageFinish`: Text = "Beenden", `DialogResult = OK`
- Auf letzter `Custom` Seite: Text = "Weiter" (spezieller Abschlussfall)
- Sonst: Text = "Abbruch", `DialogResult = Cancel`

<a name="page-navigation"></a>
### 3.2. WizardPage

Basisklasse für Seiten mit Eigenschaften:
- `Style` (`PageStyle`) – steuert das Rendering
- `Title` – Seitentitel
- `Description` – Beschreibungstext

Das Rendering (Override `OnPaint`) unterscheidet:
- `Standard`: Obere Headerleiste mit rechtem Bild (`ImageHeader`), darunter Titel & Beschreibung
- `Welcome` / `Finish`: Linkes Bild (`ImageWelcome`) als vertikale Fläche, rechts Titel groß & Beschreibung
- `Custom`: Kein eigenes Layout – Benutzer bestimmt Inhalt vollständig selbst (Kind‑Controls)

<a name="pagestyles"></a>
### 3.3. Spezialisierte Seiten

`PageWelcome`, `PageStandard`, `PageFinish`, `PageCustom` setzen lediglich Default `PageStyle` fest und verhindern unbeabsichtigte Änderung durch Override.

<a name="pagescollection"></a>
### 3.4. PagesCollection

Ableitung von `CollectionBase` mit Logik:
- Automatisches Setzen der aktiven Seite beim Einfügen (`OnInsertComplete`)
- Korrekte Neujustierung des `SelectedIndex` beim Entfernen (`OnRemoveComplete`)

<a name="events"></a>
### 3.5. Events

| Event | Zweck |
|-------|------|
| `BeforeSwitchPages(sender, BeforeSwitchPagesEventArgs)` | Validierung vor dem Seitenwechsel, kann abgebrochen werden |
| `AfterSwitchPages(sender, AfterSwitchPagesEventArgs)` | Initialisierung der neuen Seite nach dem Wechsel |
| `Cancel(sender, CancelEventArgs)` | Benutzer klickt auf Abbrechen; kann durch Setzen von `e.Cancel = True` unterdrückt werden |
| `Finish(sender, EventArgs)` | Assistent ist abgeschlossen (Finish oder letzter Schritt) |
| `Help(sender, EventArgs)` | Hilfeschaltfläche wurde angeklickt |

<a name="eventargs"></a>
### 3.6. Before / AfterSwitchPagesEventArgs

`BeforeSwitchPagesEventArgs` erbt von `AfterSwitchPagesEventArgs` und ergänzt:
- `Cancel` (Boolean)
- Schreibbares `NewIndex`

Damit kann während `BeforeSwitchPages` sowohl abgebrochen als auch umgeleitet werden (z.B. Sprunglogik).

---

<a name="properties"></a>
## 4. Öffentliche Eigenschaften (Auszug)

- `Pages` (Auflistung)
- `SelectedPage` (aktuelle Seite)
- `ImageHeader`, `ImageWelcome`
- `HeaderFont`, `HeaderTitleFont`, `WelcomeFont`, `WelcomeTitleFont`
- `VisibleHelp` (Sichtbarkeit der Hilfeschaltfläche)
- Laufzeitsteuerung (nicht browsable): `NextEnabled`, `BackEnabled`, `NextText`, `BackText`, `CancelText`, `HelpText`

---

<a name="usage"></a>
## 5. Typische Verwendung

<a name="getting-started"></a>
### 5.1. Control platzieren

Binden Sie `Wizard` in ein Formular ein (Dock = Fill).

<a name="adding-pages"></a>
### 5.2. Seiten hinzufügen (zur Designzeit oder Laufzeit)

Beispiel zur Laufzeit:

```vb
Dim wiz As New WizardControl.Wizard()
wiz.Dock = DockStyle.Fill
Me.Controls.Add(wiz)

Dim pWelcome As New WizardControl.PageWelcome() With {
    .Title = "Willkommen",
    .Description = "Dieser Assistent führt Sie durch die Konfiguration."}

Dim pStd As New WizardControl.PageStandard() With {
    .Title = "Allgemeine Einstellungen",
    .Description = "Wählen Sie die gewünschten Optionen."}

Dim pFinish As New WizardControl.PageFinish() With {
    .Title = "Abschluss",
    .Description = "Drücken Sie Beenden um fortzufahren."}

wiz.Pages.AddRange({pWelcome, pStd, pFinish})
```

<a name="event-handling"></a>
### 5.3. Validierung vor Seitenwechsel

```vb
AddHandler wiz.BeforeSwitchPages, Sub(s, e)
    If e.OldIndex = 1 Then
        ' Beispiel: Pflichtfeldprüfung
        If Not EingabenSindGueltig() Then
            e.Cancel = True
        End If
    End If
End Sub
```

<a name="finish-logic"></a>
### 5.4. Abschlusslogik

```vb
AddHandler wiz.Finish, Sub(s, e)
    Speichern()
    MessageBox.Show("Fertig.")
End Sub
```

<a name="dynamic-jump"></a>
### 5.6. Dynamischer Sprung (Wizard verzweigen)

```vb
AddHandler wiz.BeforeSwitchPages, Sub(s, e)
    If e.OldIndex = 0 Then
        If Not BenutzerMöchteErweitert() Then
            ' Direkt zur Finish-Seite springen
            e.NewIndex = wiz.Pages.IndexOf(pFinish)
        End If
    End If
End Sub
```

<a name="custom-pages"></a>
### 5.7. Custom Pages

Für vollständig eigene Layouts entweder `PageCustom` verwenden und Controls hineinlegen oder von `WizardPage` erben und `OnPaint` überschreiben.

---

<a name="code-control"></a>
## 6. Steuerung per Code

| Methode / Aktion | Wirkung |
|------------------|---------|
| `wizard.Next()` | Simuliert Klick auf Weiter |
| `wizard.Back()` | Simuliert Klick auf Zurück |
| Setzen `wizard.SelectedPage = page` | Springt direkt zu einer Seite |
| Setzen `wizard.SelectedIndex = n` (Friend) | Interner Indexwechsel |

---

<a name="layout"></a>
## 7. Layout / Rendering Hinweise

- Höhe der unteren Buttonleiste ist fix (48px reservierter Bereich)
- Seiteninhalt erhält Fläche: `Width x (Height - 48)`
- Fokussteuerung versucht erstes fokussierbares Control nach Seitenwechsel zu aktivieren

---

<a name="extensibility"></a>
## 8. Erweiterbarkeit

Mögliche Erweiterungen:

- Fortschrittsanzeige / Breadcrumb
- Themable Rendering (Farben, Ränder konfigurierbar machen)
- Serielle Speicherung / Wiederaufnahme eines Assistenten
- Unterstützung für asynchrone Validierung (Task-basierte Events)
- Lokalisierungs-Resource für Standardtexte (Abbruch, Weiter, Zurück, Beenden, Hilfe)
- Funktion zum verhindern des automatischen schließens des übegeordneten Fensters.

---

<a name="best-practices"></a>
## 9. Best Practices

- Validierung ausschließlich im `BeforeSwitchPages` Event durchführen
- UI-spezifische Seitenelemente als Child-Controls in jeweilige Seite legen (nicht direkt in `Wizard`)
- Lange Operationen (z.B. Abschlussarbeit) asynchron starten und UI sperren
- `Cancel` Event verwenden, um Benutzer vor Datenverlust zu warnen

---

<a name="troubleshooting"></a>
## 10. Fehlersuche

| Symptom | Ursache | Lösung |
|---------|---------|-------|
| Weiter-Button deaktiviert | Letzte Seite erreicht oder `NextEnabled = False` gesetzt | Index prüfen / aktivieren |
| Bild fehlt im Header | `ImageHeader` nicht gesetzt oder `Nothing` | Eigenschaft setzen (z.B. aus Ressourcen) |
| Seitenlayout überdeckt Buttons | Eigene Controls auf `WizardPage` platzieren, nicht auf `Wizard` | Verschieben |
| Abbrechen-Button zeigt "Beenden" | Aktuelle Seite ist `Finish` oder letzte `Custom` Seite | Erwartetes Verhalten |

---

<a name="resources"></a>
## 11. Ressourcen

Standardmäßig werden interne Ressourcen (`My.Resources.WizardHeaderImage`, `My.Resources.WizardWelcomeImage`) verwendet – anpassbar durch Setzen der entsprechenden Eigenschaften.
