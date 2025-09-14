# WizardControl

Ein Steuerelement zum Erstellen eines benutzerdefinierten Assistenten (Wizard) für Windows Forms.

---

## Einführung

Dieses Steuerelement wurde von mir in Anlehnung an den [Wizard](https://marketplace.visualstudio.com/items?itemName=vs-publisher-106990.RuWizard) von 
[Klaus Rutkowski](https://marketplace.visualstudio.com/publishers/vs-publisher-106990) entwickelt.

Sinn dieses Projekts ist für mich der Lerneffekt sowie eventuelle Anpassungen
vornehmen zu können.

---

## Funktionsübersicht

Das `WizardControl`-Projekt besteht aus mehreren zentralen Klassen, insbesondere `Wizard` und `PagesCollection`. Im Folgenden werden alle wichtigen Funktionen, Eigenschaften und Events ausführlich beschrieben und mit Beispielen versehen.

---

### Klasse: Wizard

#### Eigenschaften

- `VisibleHelp`
  Steuert die Sichtbarkeit der Hilfeschaltfläche.  
  *Beispiel:*

```vbnet
' Hilfeschaltfläche ausblenden
 Wizard1.VisibleHelp = False
```

- `Pages`
  Gibt die Sammlung der Wizard-Seiten zurück.  
  *Beispiel:*

```vbnet
  Dim seiten = Wizard1.Pages
```

- `ImageHeader ` 
  Bild für die Kopfzeile der Standardseiten.  
  *Beispiel:*

```vbnet
  ' Setzt ein benutzerdefiniertes Bild für die Kopfzeile
Wizard1.ImageHeader = My.Resources.MyHeaderImage
```

- `ImageWelcome`
  Bild für Begrüßungs- und Abschlussseiten.  
  *Beispiel:*

```vbnet
  ' Setzt ein benutzerdefiniertes Bild für die Willkommensseite
 Wizard1.ImageWelcome = My.Resources.MyWelcomeImage
```

- `Dock`
  Dockt das Steuerelement an eine Seite des Containers an.  
  *Beispiel:*

```vbnet
  ' Dockt den Wizard an alle Seiten des Containers an
Wizard1.Dock = DockStyle.Fill
```

- `SelectedPage`
  Gibt die aktuell ausgewählte Seite zurück oder legt sie fest.  
  *Beispiel:*

```vbnet
  ' Wechselt zur zweiten Seite des Wizards
Wizard1.SelectedPage = Wizard1.Pages(1)
```

- `HeaderFont`/ `HeaderTitleFont` / `WelcomeFont` / `WelcomeTitleFont`
  Schriftarten für verschiedene Bereiche des Wizards.  
  *Beispiel:*

```vbnet
  Wizard1.HeaderFont = New Font("Segoe UI", 10) Wizard1.WelcomeTitleFont = New Font("Segoe UI", 16, FontStyle.Bold)
```

- `NextEnabled` / `BackEnabled `
  Aktiviert oder deaktiviert die Weiter-/Zurück-Schaltflächen.  
  *Beispiel:*

```vbnet
  Wizard1.NextEnabled = False
```

- `SelectedIndex` 
  Gibt den Index der aktuellen Seite zurück oder legt ihn fest.  
  *Beispiel:*

```vbnet
  Wizard1.SelectedIndex = 0
```

- `NextText` / `BackText `/ `CancelText` / `HelpText`  
  Legt die Beschriftung der Schaltflächen fest.  
  *Beispiel:*

```vbnet
  Wizard1.NextText = "Fortfahren"
```

#### Methoden

- `Next() ` 
  Entspricht einem Klick auf die Schaltfläche "Weiter".  
  *Beispiel:*

```vbnet
 Wizard1.Next()
```

- `Back()` 
  Entspricht einem Klick auf die Schaltfläche "Zurück".  
  *Beispiel:*

```vbnet
 Wizard1.Back()
```
 
#### Events

- `BeforeSwitchPages`
  Wird ausgelöst, bevor die Seite gewechselt wird (z.B. zur Validierung).  
  *Beispiel:*

```vbnet
  AddHandler Wizard1.BeforeSwitchPages, Sub(sender, e) If Not ValidiereAktuelleSeite() Then e.Cancel = True End If End Sub
```
  
- `AfterSwitchPages`  
  Wird nach dem Seitenwechsel ausgelöst.  
  *Beispiel:*

```vbnet
  AddHandler Wizard1.AfterSwitchPages, Sub(sender, e) ' Neue Seite initialisieren End Sub
```
 
- `Cancel` 
  Wird ausgelöst, wenn der Benutzer auf "Abbrechen" klickt.  
  *Beispiel:*

```vbnet
 AddHandler Wizard1.Cancel, Sub(sender, e) If Not DarfAbbrechen() Then e.Cancel = True End If End Sub
```
 
- `Finish`  
  Wird ausgelöst, wenn der Assistent abgeschlossen wird.  
  *Beispiel:*

```vbnet
  AddHandler Wizard1.Finish, Sub(sender, e) MessageBox.Show("Assistent abgeschlossen!") End Sub
```

- `Help` 
  Wird ausgelöst, wenn der Benutzer auf die Hilfeschaltfläche klickt.  
  *Beispiel:*

```vbnet
 AddHandler Wizard1.Help, Sub(sender, e) MessageBox.Show("Hier finden Sie Hilfe.") End Sub
```

---

### Klasse: PagesCollection

Verwaltet die Sammlung der Seiten (`WizardPage`) des Wizards.

#### Eigenschaften & Methoden

- `Item(Index As Integer) As WizardPage `
  Indexbasierter Zugriff auf die Seiten.  
  *Beispiel:*

```vbnet
Dim page = Wizard1.Pages(0)
```
  
- `Add(Page As WizardPage) As Integer`  
  Fügt eine Seite hinzu.  
  *Beispiel:*

```vbnet
  Wizard1.Pages.Add(New WizardPage())
```

- `AddRange(Pages As WizardPage())`  
  Fügt mehrere Seiten hinzu.  
  *Beispiel:*

```vbnet
 Wizard1.Pages.AddRange({New WizardPage(),New WizardPage()})
```

- `IndexOf(Page As WizardPage) As Integer` 
  Gibt den Index einer Seite zurück.  
  *Beispiel:*

```vbnet
Dim idx = Wizard1.Pages.IndexOf(page)
```

- `Insert(Index As Integer, Page As WizardPage)` 
  Fügt eine Seite an einer bestimmten Position ein.  
  *Beispiel:*

```vbnet
Wizard1.Pages.Insert(1, New WizardPage())
```
 
- `Remove(Page As WizardPage)` 
  Entfernt eine Seite.  
  *Beispiel:*

```vbnet
Wizard1.Pages.Remove(page)
```

- `Contains(Page As WizardPage) As Boolean`  
  Prüft, ob eine Seite enthalten ist.  
  *Beispiel:*

```vbnet
  If Wizard1.Pages.Contains(page) Then ...
```
  
---

## Hinweise

- Die Steuerung der Navigation erfolgt über die Methoden `Next()` und `Back()` oder durch Setzen von `SelectedIndex`.
- Die Events erlauben Validierung, Initialisierung und Abschlussaktionen.
- Die Seitenverwaltung erfolgt über die `PagesCollection`.

---

## Beispiel für die Verwendung

```vbnet
' Wizard initialisieren 
Dim wizard As New WizardControl.Wizard() 
wizard.Pages.Add(New WizardControl.WizardPage() With {.Text = "Seite 1"}) 
wizard.Pages.Add(New WizardControl.WizardPage() With {.Text = "Seite 2"})

' Event-Handler hinzufügen 
AddHandler wizard.Finish, Sub(sender, e) MessageBox.Show("Fertig!") End Sub

' Wizard anzeigen (z.B. in einem Form) 
Me.Controls.Add(wizard)
```
---

## Lizenz 

Dieses Projekt ist unter der MIT-Lizenz lizenziert. Siehe die [LICENSE](../LICENSE.txt) Datei für Details.
