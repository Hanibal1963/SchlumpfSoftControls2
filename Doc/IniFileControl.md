# SchlumpfSoft Controls – IniFileControl Modul

Diese Dokumentation beschreibt die bereitgestellten Klassen, Dialoge und UserControls zur Anzeige, Bearbeitung und Persistierung klassischer INI-Dateien auf Basis von .NET (WinForms / VB.NET).

## Inhaltsverzeichnis
1. Überblick
2. Komponentenübersicht
3. Architektur & Zusammenspiel
4. Klassen & Steuerelemente
   - `IniFile`
   - Editier-Steuerelemente (`ListEdit`, `EntryValueEdit`, `CommentEdit`, `ContentView`)
   - Dialoge (`AddItemDialog`, `RenameItemDialog`, `DeleteItemDialog`)
   - EventArgs-Klassen
5. Ereignismodell
6. Lebenszyklus typischer Operationen
7. Verwendung (Codebeispiele)
8. Validierung & Fehlerfälle
9. Thread-Sicherheit / Performance
10. Erweiterbarkeit / Best Practices
11. Bekannte Verbesserungsmöglichkeiten
12. Schnellreferenz (Properties & Events)

---

## 1. Überblick

Das Modul kapselt:
- Ein datenorientiertes Kernobjekt `IniFile` (nicht-visuell, verwaltet Struktur & Persistenz).
- Mehrere WinForms-UserControls zur Anzeige und Bearbeitung:
  - Listendarstellung von Sektionen oder Einträgen
  - Bearbeitung einzelner Werte
  - Bearbeitung von Kommentarblöcken
  - Anzeige des gesamten Dateiinhalts
- Dialoge für hinzufügen, löschen, umbenennen.
- Stark ereignisgetriebenes Modell: UI löst semantische Events aus, Host-Anwendung führt Änderungen aus (Trennung von Anzeige & Datenquelle).

---

## 2. Komponentenübersicht

| Kategorie | Name | Zweck |
|-----------|------|-------|
| Daten / Logik | `IniFile` | Laden, Parsen, Manipulieren und Speichern der INI-Struktur |
| UI – Listen | `ListEdit` | Anzeigen & Auslösen von Add/Rename/Delete für Sektionen oder Eintragslisten |
| UI – Wert | `EntryValueEdit` | Anzeigen & Ändern des Wertes eines konkreten Eintrags |
| UI – Kommentar | `CommentEdit` | Bearbeiten von Datei- oder Abschnittskommentaren |
| UI – Ansicht | `ContentView` | Nur Anzeige des generierten Roh-Dateiinhalts |
| Dialoge | `AddItemDialog` | Neuen Namen erfassen |
| Dialoge | `RenameItemDialog` | Vorhandenen Namen ändern |
| Dialoge | `DeleteItemDialog` | Löschbestätigung |
| EventArgs | `ListEditEventArgs` | Kontext bei Listenoperationen |
| EventArgs | `EntryValueEditEventArgs` | Kontext beim Ändern eines Eintragswertes |
| EventArgs | `CommentEditEventArgs` | Kontext beim Übernehmen eines Kommentarblocks |

---

## 3. Architektur & Zusammenspiel

````````

Architekturprinzipien:
- Keine direkte Kopplung UI ↔ Persistenz: Steuerlogik liegt im Host (z. B. Form).
- Alle UI-Steuerelemente sind zustandsarm (halten nur Kontext + Anzeigezustand).
- `IniFile` arbeitet strikt strukturorientiert (Kommentare getrennt, Abschnitte als Dictionaries).

---

## 4. Klassen & Steuerelemente

### 4.1 `IniFile`
Verwaltet:
- Kommentar am Datei-Anfang
- Abschnittskommentare
- Schlüssel-Wert-Paare je Abschnitt
- Rohinhalt als Zeilenarray

Zentrale Methoden:
- Erzeugen: `CreateNewFile([commentPrefix])`
- Laden / Speichern: `LoadFile()`, `SaveFile()`
- Abfragen: `GetSectionNames()`, `GetEntryNames(section)`, `GetEntryValue(section, key)`
- Mutationen: `AddSection()`, `RenameSection()`, `DeleteSection()`, `AddEntry()`, `RenameEntry()`, `DeleteEntry()`, `SetEntryValue()`, `SetSectionComment()`, `SetFileComment()`

Parsing:
- `ParseFileContent()` rekonstruiert interne Strukturen.
- Kommentare werden prefixfrei intern gespeichert (Wiederzusammenbau via `CreateFileContent()`).

Ereignisse:
- `FileContentChanged`
- `SectionNameExist`
- `EntryNameExist`

### 4.2 `ListEdit`
Darstellung einer Liste (Sektionen oder Einträge):
- Buttons: Hinzufügen, Umbenennen, Löschen
- Events mit `ListEditEventArgs`:
  - `ItemAdd` (NewItemName)
  - `ItemRename` (SelectedItem + NewItemName)
  - `ItemRemove` (SelectedItem)
  - `SelectedItemChanged`
- Host setzt:
  - `ListItems` (String-Array)
  - `SelectedSection`
- Intern: keine direkte Manipulation der INI-Datei.

### 4.3 `EntryValueEdit`
Bearbeitet einen Eintragswert.
- Properties: `SelectedSection`, `SelectedEntry`, `Value`, `TitelText`
- Event: `ValueChanged(EntryValueEditEventArgs)`
- Button aktiv nur, falls Text geändert wurde.

### 4.4 `CommentEdit`
Bearbeitet Datei- oder Abschnittskommentare.
- Properties: `Comment` (String[]), `SectionName`, `TitelText`
- Event: `CommentChanged(CommentEditEventArgs)`
- Änderung erst nach explizitem Buttonklick.

### 4.5 `ContentView`
Schreibgeschützte Anzeige eines Zeilenarrays:
- Property: `Lines`
- Dient typischerweise zur Anzeige des durch `IniFile.CreateFileContent()` generierten Zustands.

### 4.6 Dialoge
| Dialog | Zweck | Besonderheiten |
|--------|-------|----------------|
| `AddItemDialog` | Neuen Namen erfassen | OK erst bei nicht-leerer Eingabe |
| `RenameItemDialog` | Alten → neuen Namen | Platzhalter `{0}` im Label für alten Namen |
| `DeleteItemDialog` | Löschbestätigung | Platzhalter `{0}` für anzuzeigendes Element |

### 4.7 EventArgs-Klassen
| Klasse | Felder / Properties | Kontext |
|--------|---------------------|---------|
| `ListEditEventArgs` | SelectedSection, SelectedItem, NewItemName | Add/Rename/Delete/Selection |
| `EntryValueEditEventArgs` | SelectedSection, SelectedEntry, NewValue | Bestätigung eines Wert-Edits |
| `CommentEditEventArgs` | Section, Comment() | Übernahme Kommentarblock |
| (intern verwendet: `CommentEditEventArgs` in `CommentEdit`) | – | – |

---

## 5. Ereignismodell

| Auslöser | Event | Reaktion (Host) |
|----------|-------|-----------------|
| Sektion/Eintrag hinzufügen | `ItemAdd` | `IniFile.AddSection()` oder `IniFile.AddEntry()` |
| Umbenennen | `ItemRename` | `IniFile.RenameSection()` oder `IniFile.RenameEntry()` |
| Löschen | `ItemRemove` | `IniFile.DeleteSection()` / `IniFile.DeleteEntry()` |
| Auswahl geändert | `SelectedItemChanged` | Kontext synchronisieren (z. B. passenden Value laden) |
| Wert bestätigt | `ValueChanged` | Persistieren: `IniFile.SetEntryValue()` |
| Kommentar übernommen | `CommentChanged` | Datei-/Abschnittskommentar setzen |
| Datei geändert | `FileContentChanged` | UI refresh: Listen, Inhalt, Rohansicht |
| Abschnitt existiert | `SectionNameExist` | Hinweis anzeigen |
| Eintrag existiert | `EntryNameExist` | Hinweis anzeigen |

---

## 6. Lebenszyklus typischer Operationen

### 6.1 Eintrag umbenennen
1. Benutzer wählt Item in `ListEdit`
2. Klick "Umbenennen" → Dialog → bestätigt
3. `ListEdit` feuert `ItemRename`
4. Host ruft `IniFile.RenameEntry()`
5. `IniFile` erzeugt neuen Rohinhalt → `FileContentChanged`
6. Host lädt neue `EntryNames` → setzt `ListEdit.ListItems`

### 6.2 Kommentar bearbeiten
1. Host setzt `CommentEdit.Comment = ini.GetSectionComment("Logging")`
2. Benutzer editiert → klickt Übernehmen
3. `CommentChanged`-Event
4. Host ruft `SetSectionComment()`
5. `FileContentChanged` → Host aktualisiert `ContentView.Lines`

---

## 7. Verwendung (vereinfachtes Beispiel)

```csharp
// IniFile laden
iniFile = new IniFile();
iniFile.LoadFile("konfiguration.ini");

// Sektionen auslesen
string[] sektionen = iniFile.GetSectionNames();

// Wert eines Eintrags auslesen
string wert = iniFile.GetEntryValue("Datenbank", "Benutzername");

// Kommentar einer Sektion setzen
iniFile.SetSectionComment("Logging", "Einstellungen für das Logging");

// Datei speichern
iniFile.SaveFile();
```

---

## 8. Validierung & Fehlerfälle

- **Allgemeine Prinzipien:**
  - Fehler dürfen nicht stillschweigend passieren.
  - Kein partielles Speichern von Änderungen.

- **Eingangsvalidierung:**
  - Alle Eingaben (z. B. von Dialogen) sind vor Verarbeitung zu überprüfen.

- **Spezifische Validierungen:**
  - `IniFile`:
    - Verbieten leerer oder nur aus Leerzeichen bestehenden Namen für Sektionen und Einträge.
    - Verhindern von Duplikaten: Keine zwei Sektionen oder Einträge mit demselben Namen.

  - Steuerelemente:
    - `EntryValueEdit`: Nur gültige, nicht-leere Werte erlauben.
    - `CommentEdit`: Umgang mit mehrzeiligen Kommentaren prüfen (z. B. durch `Join()` beim Setzen).

- **Fehlerberichte:**
  - Verwendung eigener Ausnahmen (z. B. `IniException`) zur präzisen Fehlerkommunikation.
  - Zwingend: Fehlerursache und behobene/falsche Werte im Fehlerbericht.

---

## 9. Thread-Sicherheit / Performance

- **Thread-Sicherheit:**
  - Prinzipiell nicht thread-sicher. Entwickler muss für Sicherheit sorgen.
  - Vorschlag: Synchronisierungsmechanismen wie `lock` bei Zugriff auf `IniFile`-Instanzen.

- **Performance-Überlegungen:**
  - Vermeiden unnötiger `Load`/`Save`-Operationen.
  - Beispiel:
    ```csharp
    lock (iniFile)
    {
        iniFile.LoadFile("konfiguration.ini");
        // Änderungen vornehmen
        iniFile.SaveFile();
    }
    ```
  - Aufwand für Synchronisierung meist vernachlässigbar gegen I/O-Operationen.

---

## 10. Erweiterbarkeit / Best Practices

- **Erweiterbarkeit:**
  - Gestaltung auf Einfachheit der Erweiterung bedacht.
  - Z. B. Hinzufügen neuer Steuerelemente oder Anpassungen in `IniFile`-Logik.

- **Best Practices:**
  - Bei Subklassenbildung: Basisimplementierungen stets berücksichtigen.
  - Bei Erweiterung von Dialogen: Immer auch an Validierung denken.

---

## 11. Bekannte Verbesserungsmöglichkeiten

1. **Allgemein:
   - Einheitlichere Ereignisnamen (z. B. `ItemAdded` statt `ItemAdd`).
   - Konsistentere Methode zur Fehlerberichterstattung (z. B. überall Ausnahmen verwenden).

2. **`IniFile`-Klasse:**
   - Verbessertes Fehlerhandling beim Laden/Speichern (z. B. Detailierte Ausnahmen).
   - Möglichkeit, Dateiüberschreibungen zu erzwingen oder abzulehnen.

3. **UI-Komponenten:**
   - Verbesserung der Benutzerführung, z. B. durch Tooltips oder Eingabehilfen.
   - Erweiterte Dialogoptionen, z. B. Mehrfachlöschung oder -umbenennung.

4. **Dokumentation:**
   - Vollständige Beispiele für typische Anwendungsfälle.
   - Detailliertere Fehlerbeschreibungen und Lösungsvorschläge.

---

## 12. Schnellreferenz (Properties & Events)

### 12.1 `IniFile`-Ereignisse
- `FileContentChanged`: Wird ausgelöst, wenn der Inhalt der Datei geändert wurde.
- `SectionNameExist`: Tritt ein, wenn der Name einer Sektion bereits existiert.
- `EntryNameExist`: Tritt ein, wenn der Name eines Eintrags bereits existiert.

### 12.2 `ListEdit`-Events
- `ItemAdd`: Wenn ein neues Item hinzugefügt wird.
- `ItemRename`: Beim Umbenennen eines Items.
- `ItemRemove`: Wenn ein Item entfernt wird.
- `SelectedItemChanged`: Wenn sich das ausgewählte Item ändert.

### 12.3 `EntryValueEdit`-Event
- `ValueChanged`: Wenn sich der Wert eines Eintrags ändert.

### 12.4 `CommentEdit`-Event
- `CommentChanged`: Wenn ein Kommentar geändert wurde.

---

## 13. Beispiel: Zusammenspiel mehrerer Controls (Ausschnitt)

```vb.net
' UI initial binden
ListSections.ListItems = _ini.GetSectionNames()
ContentView1.Lines = _ini.GetFileContent()

' Events
AddHandler _ini.FileContentChanged,
    Sub()
        ContentView1.Lines = _ini.GetFileContent()
    End Sub
End Sub
```

---

## 14. Qualitätshinweise

| Thema | Empfehlung |
|-------|------------|
| Fehlerkommunikation | Events ergänzen durch aussagekräftige Argumente (z. B. Konfliktname) |
| Logging | Optionales Interface (ILogger) integrieren |
| Unit Tests | Fokus: Parser, Rename-Operationen, Kommentar-Roundtrip |
| API Konsistenz | Einheitliche Benamung (Entry vs. Item) prüfen |

---

## 15. Changelog (konzeptionell)

| Version | Änderungsidee |
|---------|---------------|
| 1.0 | Basisfunktionalität (aktuell) |
| 1.1 | Reihenfolge beibehalten, Undo-Stack |
| 1.2 | Encoding & BOM-Unterstützung |
| 1.3 | Fehlerrobuster Parser (tolerant) |

---

## 16. Fazit

Das Modul stellt eine klare Trennung von Datenmodell (`IniFile`) und Benutzerinteraktion (UserControls + Dialoge) bereit. Durch konsequente Eventorientierung bleibt die Host-Anwendung flexibel und kann eigene Logik (Validierung, Persistenzstrategien, Undo/Redo) ergänzen.

Für größere Evolutionsschritte bieten sich an:
- Zentralisiertes Änderungs- und Konfliktmanagement
- Verbesserte Nutzungs- und Fehlermeldungen
- Erweiterte Formatierungsregeln (z. B. Quoting)

