# FileSearchControl

Führt eine optionale rekursive, asynchrone Dateisuche anhand eines Suchmusters aus und meldet Ergebnisse sowie Fortschritt über Ereignisse. Die eigentliche Arbeit läuft im ThreadPool, Ereignisse werden typischerweise auf den ursprünglichen Synchronisierungskontext (z. B. UI-Thread) gemarshallt.

---

## Inhalt
1. [Schnellüberblick](#schnellüberblick)
2. [Öffentliche API](#öffentliche-api)
    * 2.1. [Eigenschaften](#eigenschaften)
    * 2.2. [Methoden](#methoden)
    * 2.3. [Ereignisse](#ereignisse)
3. [Ablauf und Threading](#abluft-und-threading)
4. [Fehler- und Abbruchverhalten](#fehler--und-abbruchverhalten)
5. [Leistungsaspekte](#leistungsaspekte)
6. [Design-Time/Toolbox](#design-time-toolbox)
7. [Verhalten bei erneutem Start](#verhalten-bei-erneutem-start)
8. [Randbedingungen und Annahmen](#randbedingungen-und-annahmen)
9. [Beispiele](#beispiele)

---

<a name="schnellüberblick"></a>
## 1. Schnellüberblick

- Asynchrone Suche per `StartSearchAsync()` (awaitable)
- Optional rekursiv (`SearchInSubfolders`)
- Ereignisse:
  - `FileFound`: jede gefundene Datei
  - `ProgressChanged`: kumulierter Fortschritt (Found/Total/Percent)
  - `ErrorOccurred`: Fehler während Enumeration/IO
  - `SearchCompleted`: Abschluss oder Abbruch
- Kooperativer Abbruch via `StopSearch()` oder erneutem Start
- Fortschritt und Events typischerweise auf dem UI-Thread (abhängig von `SimpleProgress(Of T)`)

---

<a name="öffentliche-api"></a>
## 2. Öffentliche API

<a name="eigenschaften"></a>
### 2.1. Eigenschaften

- `StartPath As String`
  - Startverzeichnis der Suche. Standard: `String.Empty`
- `SearchPattern As String`
  - Suchmuster, z. B. `*.txt`. Standard: `"*.*"`
- `SearchInSubfolders As Boolean`
  - Rekursive Suche in Unterordnern. Standard: `False`

  <a name="methoden"></a>
### 2.2. Methoden

- `StartSearchAsync() As System.Threading.Tasks.Task`
  - Startet die Suche asynchron im ThreadPool.
  - Ein laufender Durchlauf wird vorher kooperativ abgebrochen (es wird nicht auf dessen Ende gewartet).
  - Ereignisse werden während der Suche ausgelöst.
  - Kann und sollte bei Bedarf `Await`-ed werden.
- `StopSearch() As Void`
  - Bricht eine laufende Suche kooperativ ab. Kurzzeitig können noch Events der abgebrochenen Suche eintreffen.

  <a name="ereignisse"></a>
### 2.3. Ereignisse

- `FileFound(sender As Object, file As String)`
  - Für jede gefundene Datei (vollqualifizierter Pfad).
- `ProgressChanged(sender As Object, e As FileSearchEventArgs)`
  - Kumulierte Fortschrittswerte nach jeder gefundenen Datei.
  - Erwartete Felder in `FileSearchEventArgs`:
    - `Found As Integer`
    - `Total As Integer`
    - `Percent As Integer` (0–100; bei `Total=0` ist `Percent=0`)
- `ErrorOccurred(sender As Object, error As System.Exception)`
  - Gemeldete Fehler (z. B. fehlende Rechte, ungültiger Pfad). Beendet die Suche.
- `SearchCompleted(sender As Object, cancel As Boolean)`
  - Wird am Ende ausgelöst. `cancel=True`, wenn der Durchlauf abgebrochen wurde.

> :warning:**Hinweis:** 
>
>`FileSearchEventArgs` und `SimpleProgress(Of T)` sind Teil der Bibliothek, jedoch nicht in dieser Datei definiert. 
>
>`SimpleProgress` sorgt für das Marshaling der Event-Aufrufe auf den zum Startzeitpunkt aktiven Synchronisierungskontext (z. B. UI-Thread).

---

<a name="ablauf-und-threading"></a>
## 3. Ablauf und Threading

- Die Datei-Enumeration läuft innerhalb von `Task.Run(...)` auf einem ThreadPool-Thread.
- Zwei `SimpleProgress`-Instanzen melden:
  - pro Datei (`FileFound`)
  - aggregiert (`ProgressChanged`)
- Synchronisierung: Sofern beim Start ein Synchronisierungskontext vorhanden ist (WinForms/WPF), werden die Ereignisse dorthin gepostet. Andernfalls auf ThreadPool-Threads.

---

<a name="fehler--und-abbruchverhalten"></a>
## 4. Fehler- und Abbruchverhalten

- Abbruch: `CancellationToken` wird geprüft; `StopSearch()` oder ein erneuter `StartSearchAsync()`-Aufruf löst Abbruch aus.
- Fehlerfälle, die abgefangen und über `ErrorOccurred` gemeldet werden:
  - `UnauthorizedAccessException`
  - `DirectoryNotFoundException`
  - andere `Exception`
- `OperationCanceledException` führt zu `SearchCompleted(..., cancel:=True)`.

Wichtig: Pro-Datei-Fehler werden nicht individuell behandelt. Tritt während der Enumeration ein IO-Fehler auf, wird die gesamte Suche beendet.

---

<a name="leistungsaspekte"></a>
## 5. Leistungsaspekte

- Es wird bewusst doppelt enumeriert:
  1) `Count()` ermittelt die Gesamtanzahl
  2) Zweite Enumeration verarbeitet die Dateien
- Bei sehr großen Verzeichnisbäumen kann das Performance kosten.
- Alternativen (nicht implementiert, mögliche Erweiterungen):
  - Einmaliges Materialisieren: `ToList()` (Speicher vs. Zeit)
  - Fortschritt ohne Total (z. B. `Total = -1`)
  - Schätzung des Totals während der Laufzeit

---

<a name="design-time-toolbox"></a>
## 6. Design-Time/Toolbox

- Die Komponente erscheint in der Toolbox-Gruppe „SchlumpfSoft Controls“ mit Icon `FileSearch.bmp`.
- Eigenschaften sind in der Eigenschaften-Fenster-Kategorie „Behavior“ verfügbar.
- Ereignisse können im Designer verdrahtet werden.

---

<a name="verhalten-bei-erneutem-start"></a>
## 7. Verhalten bei erneutem Start

- Ein erneuter Aufruf von `StartSearchAsync()` bricht eine laufende Suche kooperativ ab und startet sofort neu.
- Kurzzeitig können Events aus der vorherigen Suche noch eintreffen (race-artig). UI-seitig ggf. mit Laufzeit-Tokens/Instanz-IDs entkoppeln.

---

<a name="randbedingungen-und-annahmen"></a>
## 8. Randbedingungen und Annahmen

- `StartPath` muss existieren und zugreifbar sein.
- `SearchPattern` nutzt die Semantik von `System.IO.Directory.EnumerateFiles`.
- `Imports System.Linq` wird benötigt (für `Count()` auf `IEnumerable(Of String)`).

---

<a name="beispiele"></a>
## 9. Beispiele

### WinForms: Button startet Suche, UI-Thread bleibt responsiv

```vbnet
Imports SchlumpfSoftControls2.FileSearchControl

Public Class MainForm Private WithEvents _search As New FileSearch()

Private Async Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
    _search.StartPath = "C:\Temp"
    _search.SearchPattern = "*.txt"
    _search.SearchInSubfolders = True

    Try
        Await _search.StartSearchAsync()
    Catch ex As Exception
        ' StartSearchAsync fängt intern ab; hier nur für Vollständigkeit
    End Try
End Sub

Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
    _search.StopSearch()
End Sub

Private Sub _search_FileFound(sender As Object, file As String) Handles _search.FileFound
    lstFiles.Items.Add(file)
End Sub

Private Sub _search_ProgressChanged(sender As Object, e As FileSearchEventArgs) Handles _search.ProgressChanged
    progressBar.Value = e.Percent
    lblStatus.Text = $"{e.Found}/{e.Total} ({e.Percent}%)"
End Sub

Private Sub _search_ErrorOccurred(sender As Object, [error] As Exception) Handles _search.ErrorOccurred
    MessageBox.Show([error].Message, "Suche: Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
End Sub

Private Sub _search_SearchCompleted(sender As Object, cancel As Boolean) Handles _search.SearchCompleted
    lblStatus.Text = If(cancel, "Abgebrochen", "Fertig")
End Sub

End Class
