# FileSearchControl

Führt eine optionale rekursive, asynchrone Dateisuche anhand eines Suchmusters aus und meldet Ergebnisse sowie Fortschritt über Ereignisse. Die eigentliche Arbeit läuft im ThreadPool, Ereignisse werden typischerweise auf den ursprünglichen Synchronisierungskontext (z. B. UI-Thread) gemarshallt.

---

## Schnellüberblick

- Asynchrone Suche per `StartSearchAsync()` (awaitable)
- Optional rekursiv (`SearchInSubfolders`)
- Ereignisse:
- `FileFound`: jede gefundene Datei
- `ProgressChanged`: kumulierter Fortschritt (Found/Total/Percent)
- `ErrorOccurred`: Fehler während Enumeration/IO
- `SearchCompleted`: Abschluss oder Abbruch
- Kooperativer Abbruch via `StopSearch()` oder erneutem Start
- Fortschritt und Events typischerweise auf dem UI-Thread (abhängig von `SimpleProgress(Of T)`)


## Fehler- und Abbruchverhalten

- Abbruch: `CancellationToken` wird geprüft; `StopSearch()` oder ein erneuter `StartSearchAsync()`-Aufruf löst Abbruch aus.
- Fehlerfälle, die abgefangen und über `ErrorOccurred` gemeldet werden:
- `UnauthorizedAccessException`
- `DirectoryNotFoundException`
- andere `Exception`
- `OperationCanceledException` führt zu `SearchCompleted(..., cancel:=True)`.

Wichtig: Pro-Datei-Fehler werden nicht individuell behandelt. Tritt während der Enumeration ein IO-Fehler auf, wird die gesamte Suche beendet.

---

## Leistungsaspekte

- Es wird bewusst doppelt enumeriert:
-  `Count()` ermittelt die Gesamtanzahl
-  Zweite Enumeration verarbeitet die Dateien
- Bei sehr großen Verzeichnisbäumen kann das Performance kosten.
- Alternativen (nicht implementiert, mögliche Erweiterungen):
- Einmaliges Materialisieren: `ToList()` (Speicher vs. Zeit)
- Fortschritt ohne Total (z. B. `Total = -1`)
- Schätzung des Totals während der Laufzeit

---

## Design-Time/Toolbox

- Die Komponente erscheint in der Toolbox-Gruppe „SchlumpfSoft Controls“ mit Icon `FileSearch.bmp`.
- Eigenschaften sind in der Eigenschaften-Fenster-Kategorie „Behavior“ verfügbar.
- Ereignisse können im Designer verdrahtet werden.

---

## Verhalten bei erneutem Start

- Ein erneuter Aufruf von `StartSearchAsync()` bricht eine laufende Suche kooperativ ab und startet sofort neu.
- Kurzzeitig können Events aus der vorherigen Suche noch eintreffen (race-artig). UI-seitig ggf. mit Laufzeit-Tokens/Instanz-IDs entkoppeln.

---

## Randbedingungen und Annahmen

- `StartPath` muss existieren und zugreifbar sein.
- `SearchPattern` nutzt die Semantik von `System.IO.Directory.EnumerateFiles`.
- `Imports System.Linq` wird benötigt (für `Count()` auf `IEnumerable(Of String)`).

---
