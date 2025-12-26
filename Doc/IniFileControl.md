# IniFileControl

Ein Set von Komponenten zum Verwalten von INI - Dateien.

## Einführung

Diese Komponente ist von mir ursprünglich als Hilfsklasse für ein anderes Projekt entwickelt worden.

Es entstand die Idee eine Windows-Forms-Komponente aus dieser Hilfsklasse zu machen und diese anderen zur Verfügung zu stellen.

Inzwischen habe ich weitere Komponenten hinzugefügt welche die Benutzung der Komponente IniFile weiter vereinfachen.

Es sind nur noch wenige Zeilen Code erforderlich um die volle Funktionalität zu erreichen.

---

##  Struktur einer INI-Datei

### Abschnitte (Sections)

   -	Abschnitte werden durch eckige Klammern <font color="red">[  ]</font> gekennzeichnet.
   -	Sie dienen dazu, verwandte Einstellungen zu gruppieren.
   
   Beispiel:

```ini
[Datenbank]
```

### Schlüssel-Wert-Paare (Key-Value Pairs)

   -	Innerhalb eines Abschnitts werden Einstellungen als Schlüssel-Wert-Paare definiert.
   -	Der Schlüssel und der Wert werden durch ein Gleichheitszeichen = getrennt.
   
   Beispiel:

```ini
Benutzername=admin
Passwort=geheim
```

### Kommentare

   - Kommentare beginnen mit einem Semikolon <font color="red">;</font> oder einem Hashtag <font color="red">#</font>.
   - Sie werden ignoriert und dienen nur zur Dokumentation.
   
   Beispiel:

```ini
; Dies ist ein Kommentar
# Dies ist auch ein Kommentar
```

### Beispiel einer vollständigen INI-Datei

```ini
; Dies ist eine Beispiel-INI-Datei

[Allgemein]
; Allgemeine Einstellungen
AppName=MeineApp
Version=1.0.0

[Datenbank]
; Einstellungen zur Datenbank
Server=localhost
Port=3306
Benutzername=admin
Passwort=geheim

[Logging]
LogLevel=DEBUG
LogDatei=logs/app.log
```

**Erklärung des Beispiels**

:memo: **Kommentare:** 

> &rarr; Die erste Zeile ist ein Kommentar, der ignoriert wird.

:memo: **Abschnitt "Allgemein":**

>   &rarr;	Die erste Zeile des Abschnitts ist ein Kommentar der ignoriert wird
>
>   &rarr;	**AppName=MeineApp**: Definiert den Namen der Anwendung.
>
>   &rarr;	**Version=1.0.0**: Gibt die Version der Anwendung an.

:memo: **Abschnitt "Datenbank":**

>   &rarr;	Die erste Zeile des Abschnitts ist ein Kommentar der ignoriert wird
>
>   &rarr;	**Server=localhost**: Gibt den Datenbankserver an.
>
>   &rarr;	**Port=3306**: Gibt den Port an, auf dem die Datenbank läuft.
>
>   &rarr;	**Benutzername=admin**: Der Benutzername für die Datenbank.
>
>   &rarr;	**Passwort=geheim**: Das Passwort für die Datenbank.

:memo:	**Abschnitt "Logging":**

>   &rarr;	**LogLevel=DEBUG**: Definiert das Log-Level.
>
>   &rarr;	**LogDatei=logs/app.log**: Gibt den Pfad zur Log-Datei an.

:memo: **Wichtige Hinweise:**

>   &rarr;	**Leerzeichen**: Leerzeichen um das Gleichheitszeichen werden ignoriert.
>
>   &rarr;	**Groß-/Kleinschreibung**: In der Regel sind Schlüssel und Abschnittsnamen nicht case-sensitive, aber das kann je nach Implementierung variieren.
>
>   &rarr;	**Mehrere Abschnitte**: Eine INI-Datei kann mehrere Abschnitte enthalten, und jeder Abschnitt kann mehrere Schlüssel-Wert-Paare haben.

:bulb: INI-Dateien sind aufgrund ihrer Einfachheit und Lesbarkeit weit verbreitet, insbesondere für kleinere Anwendungen und Konfigurationsdateien, die von Menschen bearbeitet werden sollen.

---

## Überblick

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

## Komponentenübersicht

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

## Architektur & Zusammenspiel

Architekturprinzipien:

- Keine direkte Kopplung UI ↔ Persistenz: Steuerlogik liegt im Host (z. B. Form).
- Alle UI-Steuerelemente sind zustandsarm (halten nur Kontext + Anzeigezustand).
- `IniFile` arbeitet strikt strukturorientiert (Kommentare getrennt, Abschnitte als Dictionaries).

---

### `IniFile`

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

### `ListEdit`

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

### `EntryValueEdit`

Bearbeitet einen Eintragswert.

- Properties: `SelectedSection`, `SelectedEntry`, `Value`, `TitelText`
- Event: `ValueChanged(EntryValueEditEventArgs)`
- Button aktiv nur, falls Text geändert wurde.

### `CommentEdit`

Bearbeitet Datei- oder Abschnittskommentare.

- Properties: `Comment` (String[]), `SectionName`, `TitelText`
- Event: `CommentChanged(CommentEditEventArgs)`
- Änderung erst nach explizitem Buttonklick.

### `ContentView`

Schreibgeschützte Anzeige eines Zeilenarrays:

- Property: `Lines`
- Dient typischerweise zur Anzeige des durch `IniFile.CreateFileContent()` generierten Zustands.

### Dialoge

| Dialog | Zweck | Besonderheiten |
|--------|-------|----------------|
| `AddItemDialog` | Neuen Namen erfassen | OK erst bei nicht-leerer Eingabe |
| `RenameItemDialog` | Alten → neuen Namen | Platzhalter `{0}` im Label für alten Namen |
| `DeleteItemDialog` | Löschbestätigung | Platzhalter `{0}` für anzuzeigendes Element |

 ---

### Eintrag umbenennen

1. Benutzer wählt Item in `ListEdit`
2. Klick "Umbenennen" → Dialog → bestätigt
3. `ListEdit` feuert `ItemRename`
4. Host ruft `IniFile.RenameEntry()`
5. `IniFile` erzeugt neuen Rohinhalt → `FileContentChanged`
6. Host lädt neue `EntryNames` → setzt `ListEdit.ListItems`

### Kommentar bearbeiten

1. Host setzt `CommentEdit.Comment = ini.GetSectionComment("Logging")`
2. Benutzer editiert → klickt Übernehmen
3. `CommentChanged`-Event
4. Host ruft `SetSectionComment()`
5. `FileContentChanged` → Host aktualisiert `ContentView.Lines`

---

## Validierung & Fehlerfälle

**Allgemeine Prinzipien:**

  - Fehler dürfen nicht stillschweigend passieren.
  - Kein partielles Speichern von Änderungen.

**Eingangsvalidierung:**

  - Alle Eingaben (z. B. von Dialogen) sind vor Verarbeitung zu überprüfen.

**Spezifische Validierungen:**

`IniFile`:

   - Verbieten leerer oder nur aus Leerzeichen bestehenden Namen für Sektionen und Einträge.
   - Verhindern von Duplikaten: Keine zwei Sektionen oder Einträge mit demselben Namen.

**Steuerelemente:**

   - `EntryValueEdit`: Nur gültige, nicht-leere Werte erlauben.
   - `CommentEdit`: Umgang mit mehrzeiligen Kommentaren prüfen (z. B. durch `Join()` beim Setzen).

**Fehlerberichte:**

  - Verwendung eigener Ausnahmen (z. B. `IniException`) zur präzisen Fehlerkommunikation.
  - Zwingend: Fehlerursache und behobene/falsche Werte im Fehlerbericht.

---

## Thread-Sicherheit / Performance

**Thread-Sicherheit:**

  - Prinzipiell nicht thread-sicher. Entwickler muss für Sicherheit sorgen.
  - Vorschlag: Synchronisierungsmechanismen wie `lock` bei Zugriff auf `IniFile`-Instanzen.

**Performance-Überlegungen:**

  - Vermeiden unnötiger `Load`/`Save`-Operationen.
  - Beispiel:
  
    ```vbnet
    lock (iniFile)
    iniFile
    iniFile.LoadFile("konfiguration.ini")
    ' Änderungen vornehmen
    iniFile.SaveFile   
    ```

  - Aufwand für Synchronisierung meist vernachlässigbar gegen I/O-Operationen.

---

## Erweiterbarkeit / Best Practices

**Erweiterbarkeit:**

  - Gestaltung auf Einfachheit der Erweiterung bedacht.
  - Z. B. Hinzufügen neuer Steuerelemente oder Anpassungen in `IniFile`-Logik.

**Best Practices:**

  - Bei Subklassenbildung: Basisimplementierungen stets berücksichtigen.
  - Bei Erweiterung von Dialogen: Immer auch an Validierung denken.

---

## Bekannte Verbesserungsmöglichkeiten

**Allgemein:**

   - Einheitlichere Ereignisnamen (z. B. `ItemAdded` statt `ItemAdd`).
   - Konsistentere Methode zur Fehlerberichterstattung (z. B. überall Ausnahmen verwenden).

**`IniFile`-Klasse:**

   - Verbessertes Fehlerhandling beim Laden/Speichern (z. B. Detailierte Ausnahmen).
   - Möglichkeit, Dateiüberschreibungen zu erzwingen oder abzulehnen.

**UI-Komponenten:**

   - Verbesserung der Benutzerführung, z. B. durch Tooltips oder Eingabehilfen.
   - Erweiterte Dialogoptionen, z. B. Mehrfachlöschung oder -umbenennung.

**Dokumentation:**

   - Vollständige Beispiele für typische Anwendungsfälle.
   - Detailliertere Fehlerbeschreibungen und Lösungsvorschläge.

---

## Qualitätshinweise

| Thema | Empfehlung |
|-------|------------|
| Fehlerkommunikation | Events ergänzen durch aussagekräftige Argumente (z. B. Konfliktname) |
| Logging | Optionales Interface (ILogger) integrieren |
| Unit Tests | Fokus: Parser, Rename-Operationen, Kommentar-Roundtrip |
| API Konsistenz | Einheitliche Benamung (Entry vs. Item) prüfen |

---

## Fazit

Das Modul stellt eine klare Trennung von Datenmodell (`IniFile`) und Benutzerinteraktion (UserControls + Dialoge) bereit. Durch konsequente Eventorientierung bleibt die Host-Anwendung flexibel und kann eigene Logik (Validierung, Persistenzstrategien, Undo/Redo) ergänzen.

Für größere Evolutionsschritte bieten sich an:
- Zentralisiertes Änderungs- und Konfliktmanagement
- Verbesserte Nutzungs- und Fehlermeldungen
- Erweiterte Formatierungsregeln (z. B. Quoting)
