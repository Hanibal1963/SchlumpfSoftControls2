# WizardControl (SchlumpfSoftControls2)

## Einführung

Dieses Steuerelement wurde von mir in Anlehnung an den [Wizard](https://marketplace.visualstudio.com/items?itemName=vs-publisher-106990.RuWizard) von 
[Klaus Rutkowski](https://marketplace.visualstudio.com/publishers/vs-publisher-106990) entwickelt.

Sinn dieses Projekts ist für mich der Lerneffekt sowie eventuelle Anpassungen vornehmen zu können.

---

## Übersicht

`WizardControl` stellt ein wiederverwendbares Windows‑Forms Steuerelement bereit, mit dem mehrseitige Assistenten ("Wizards") komfortabel aufgebaut werden können. Es kapselt die Navigation (Zurück / Weiter / Abbrechen / Hilfe bzw. Beenden) sowie das Rendering unterschiedlicher Seitentypen (Welcome, Standard, Finish, Custom) inklusive Header-/Welcome‑Bildern und konfigurierbaren Schriftarten.

Das Control eignet sich für Installations‑, Konfigurations‑ oder Schritt‑für‑Schritt Dialoge, bei denen Benutzer sequenziell durch logische Abschnitte geführt werden.

---

## Custom Pages

Für vollständig eigene Layouts entweder `PageCustom` verwenden und Controls hineinlegen oder von `WizardPage` erben und `OnPaint` überschreiben.

---

## Steuerung per Code

| Methode / Aktion | Wirkung |
|------------------|---------|
| `wizard.Next()` | Simuliert Klick auf Weiter |
| `wizard.Back()` | Simuliert Klick auf Zurück |
| Setzen `wizard.SelectedPage = page` | Springt direkt zu einer Seite |
| Setzen `wizard.SelectedIndex = n` (Friend) | Interner Indexwechsel |

---

## Layout / Rendering Hinweise

- Höhe der unteren Buttonleiste ist fix (48px reservierter Bereich)
- Seiteninhalt erhält Fläche: `Width x (Height - 48)`
- Fokussteuerung versucht erstes fokussierbares Control nach Seitenwechsel zu aktivieren

---

## Erweiterbarkeit

Mögliche Erweiterungen:

- Fortschrittsanzeige / Breadcrumb
- Themable Rendering (Farben, Ränder konfigurierbar machen)
- Serielle Speicherung / Wiederaufnahme eines Assistenten
- Unterstützung für asynchrone Validierung (Task-basierte Events)
- Lokalisierungs-Resource für Standardtexte (Abbruch, Weiter, Zurück, Beenden, Hilfe)
- Funktion zum verhindern des automatischen schließens des übegeordneten Fensters.

---

## Best Practices

- Validierung ausschließlich im `BeforeSwitchPages` Event durchführen
- UI-spezifische Seitenelemente als Child-Controls in jeweilige Seite legen (nicht direkt in `Wizard`)
- Lange Operationen (z.B. Abschlussarbeit) asynchron starten und UI sperren
- `Cancel` Event verwenden, um Benutzer vor Datenverlust zu warnen

---

## Fehlersuche

| Symptom | Ursache | Lösung |
|---------|---------|-------|
| Weiter-Button deaktiviert | Letzte Seite erreicht oder `NextEnabled = False` gesetzt | Index prüfen / aktivieren |
| Bild fehlt im Header | `ImageHeader` nicht gesetzt oder `Nothing` | Eigenschaft setzen (z.B. aus Ressourcen) |
| Seitenlayout überdeckt Buttons | Eigene Controls auf `WizardPage` platzieren, nicht auf `Wizard` | Verschieben |
| Abbrechen-Button zeigt "Beenden" | Aktuelle Seite ist `Finish` oder letzte `Custom` Seite | Erwartetes Verhalten |

---

## Ressourcen

Standardmäßig werden interne Ressourcen (`My.Resources.WizardHeaderImage`, `My.Resources.WizardWelcomeImage`) verwendet – anpassbar durch Setzen der entsprechenden Eigenschaften.
