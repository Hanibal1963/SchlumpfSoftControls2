# DriveWatcherControl

Ausführliche Dokumentation für die Komponente `DriveWatcher` des Projekts **DriveWatcherControl**.

## Einführung

Grundlage und Anregung für dieses Control stammen aus dem Internet.

[ActiveVB - VB.NET Tipp 0055:Hinzufügen und Entfernen von USB-Wechselmedien erkennen](http://www.activevb.de/tipps/vbnettipps/tipp0055.html)

---

## Inhaltsverzeichnis

1. [Überblick](#1-überblick)
2. [Funktionsumfang](#2-funktionsumfang)
3. [Architektur & interne Arbeitsweise](#3-architektur--interne-arbeitsweise)
4. [Öffentliche API](#4-öffentliche-api)
   - [Ereignisse](#ereignisse)
   - [EventArgs-Strukturen](#eventargs-strukturen)
        - [DriveAddedEventArgs](#driveaddedeventargs)
        - [DriveRemovedEventArgs](#driveremovedeventargs)
5. [Lebenszyklus / Ablauf beim Erkennen von Laufwerksänderungen](#5-lebenszyklus--ablauf-beim-erkennen-von-laufwerksänderungen)
6. [Einsatzszenarien](#6-einsatzszenarien)
7. [Verwendung (Beispiele in VB.NET)](#7-verwendung-vbnet-beispiele)
    - [Einbindung (Designer)](#einbindung-designer)
    - [Programmatische Instanziierung](#programmatische-instanziierung)
    - [Check auf `IsReady`](#check-auf-isready)
    - [Filtern nur bestimmter Laufwerkstypen](#filtern-nur-bestimmter-laufwerkstypen)
8. [Entwurfsentscheidungen](#8-entwurfsentscheidungen)
9. [Fehler- und Sonderfälle](#9-fehler--und-sonderfälle)
10. [Performance-Hinweise](#10-performance-hinweise)
11. [Sicherheit / Berechtigungen](#11-sicherheit--berechtigungen)
12. [Test- & Diagnosehinweise](#12-test--diagnosehinweise)
13. [Logging-Tipp](#13-logging-tipp)
14. [Erweiterungsideen](#14-erweiterungsideen)
15. [Kurzübersicht (Cheat Sheet)](#15-kurzübersicht-cheat-sheet)
16. [FAQ](#16-faq)
17. [weitere Literatur](#17-weitere-literatur)

---

<a name="1-überblick"></a>
## 1. Überblick

`DriveWatcher` ist eine nicht-visuelle Komponente (erbt von `System.ComponentModel.Component`) zur Überwachung von Änderungen an logischen Laufwerken (Volumes) unter Windows. Sie meldet:
- Hinzufügen (Einhängen) eines Laufwerks (z. B. USB-Stick, Netzlaufwerk, virtuelle Laufwerke)
- Entfernen (Aushängen) eines Laufwerks

Die Komponente ist für WinForms-Projekte ausgelegt und verwendet ein internes unsichtbares Fenster (`NativeWindow`), um auf Systemnachrichten (Windows Messages) zu reagieren (`WM_DEVICECHANGE`).

---

<a name="2-funktionsumfang"></a>
## 2. Funktionsumfang

| Funktion | Beschreibung |
|----------|--------------|
| Überwachung von Laufwerksänderungen | Erkennt dynamisch hinzugefügte und entfernte Volumes. |
| Übergabe detaillierter Laufwerksinformationen | Beim Hinzufügen: Name, Label, Größen-/Speicherinformationen, Dateisystemtyp, Laufwerkstyp, Bereitschaft. |
| Einfache Integration | Drag & Drop ins Komponentenfenster oder programmatische Instanziierung. |
| Ressourcenbereinigung | Implementiert korrektes `Dispose`-Pattern. |

---

<a name="3-architektur--interne-arbeitsweise"></a>
## 3. Architektur & interne Arbeitsweise

Die Komponente kapselt die direkte Arbeit mit Windows-spezifischen Broadcast-Nachrichten.

Wesentliche Bausteine:

- `DriveWatcher` (öffentliche API & Ereignisse)
- Innere Klasse `NativeForm` (erbt `NativeWindow`):
  - Erstellt eigenes Fensterhandle
  - Überschreibt `WndProc`
  - Filtert `WM_DEVICECHANGE`
  - Interpretiert Strukturen wie `DEV_BROADCAST_VOLUME`

Interner Ablauf bei Systemmeldung:

1. Windows sendet `WM_DEVICECHANGE`
2. `NativeForm.WndProc` leitet an `HandleHeader`
3. Erkennung des Gerätetyps (hier: Volume)
4. Interpretation von `dbcv_unitmask` ⇒ Laufwerksbuchstabe
5. Erstellung `DriveInfo` und Auslösen des passenden Ereignisses (`DriveAdded` / `DriveRemoved`)
6. Beim Hinzufügen werden – falls `DriveInfo.IsReady` – weitere Eigenschaften gelesen (ansonsten Platzhalterwerte)

---

<a name="4-öffentliche-api"></a>
## 4. Öffentliche API

<a name="ereignisse"></a>
### 4.1 Ereignisse

| Ereignis | Signatur | Ausgelöst bei |
|----------|----------|---------------|
| `DriveAdded` | `Event DriveAdded(sender As Object, e As DriveAddedEventArgs)` | Neues Laufwerk verfügbar |
| `DriveRemoved` | `Event DriveRemoved(sender As Object, e As DriveRemovedEventArgs)` | Laufwerk entfernt |

<a name="eventargs-strukturen"></a>
### 4.2 EventArgs-Strukturen

<a name="driveaddedeventargs"></a>
#### `DriveAddedEventArgs`

| Property | Typ | Beschreibung |
|----------|-----|--------------|
| `DriveName` | `String` | Name inkl. Backslash (z. B. `C:\`) |
| `VolumeLabel` | `String` | Volumebezeichnung (falls bereit) |
| `AvailableFreeSpace` | `Long` | Freier Speicher (für Benutzer) in Bytes |
| `TotalFreeSpace` | `Long` | Gesamter freier Speicher in Bytes |
| `TotalSize` | `Long` | Gesamtkapazität des Laufwerks in Bytes |
| `DriveFormat` | `String` | Dateisystem (z. B. NTFS, FAT32) |
| `DriveType` | `System.IO.DriveType` | Typ (Removable, Fixed, Network, CDROM ...) |
| `IsReady` | `Boolean` | Gibt an, ob Medium bereit ist |

<a name="driveremovedeventargs"></a>
#### `DriveRemovedEventArgs`

| Property | Typ | Beschreibung |
|----------|-----|--------------|
| `DriveName` | `String` | Name des entfallenen Laufwerks |

---

<a name="5-lebenszyklus--ablauf-beim-erkennen-von-laufwerksänderungen"></a>
## 5. Lebenszyklus / Ablauf beim Erkennen von Laufwerksänderungen

```
+------------------+
|  Windows System  |
+---------+--------+
          |
          | WM_DEVICECHANGE
          v
+------------------+
|  NativeForm      |  (WndProc)
+---------+--------+
          | prüft Header / Gerätetyp
          v
+------------------+
| HandleVolume     |
|  - dbcv_unitmask |
|  - DriveInfo(...)|
+---------+--------+
   | DBT_DEVICEARRIVAL        | DBT_DEVICEREMOVECOMPLETE
   v                          v
DriveWatcher.DriveAdded   DriveWatcher.DriveRemoved
```

---

<a name="6-einsatzszenarien"></a>
## 6. Einsatzszenarien

- Automatisches Erkennen von USB-Sticks zur Datensicherung
- Monitoring von Netzlaufwerken (Verfügbarkeit)
- Triggern von Importprozessen beim Einstecken externer Medien
- Anzeige dynamischer Laufwerkslisten in UI-Elementen

---

<a name="7-verwendung-vbnet-beispiele"></a>
## 7. Verwendung (VB.NET Beispiele)

<a name="einbindung-designer"></a>
### 7.1 Einbindung (Designer)

1. Projekt referenziert die Assembly `DriveWatcherControl`
2. Komponente in der Toolbox unter Kategorie "Schlumpfsoft Controls"
3. Auf ein Formular ziehen ⇒ Ereignisse verdrahten

<a name="programmatische-instanziierung"></a>
### 7.2 Programmatische Instanziierung

```vbnet
Imports DriveWatcherControl

Public Class Form1
    Private WithEvents _watcher As New DriveWatcher()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Keine zusätzliche Initialisierung nötig
    End Sub

    Private Sub _watcher_DriveAdded(sender As Object, e As DriveAddedEventArgs) Handles _watcher.DriveAdded
        Dim msg = $"Laufwerk hinzugefügt: {e.DriveName} (Label={e.VolumeLabel}, Typ={e.DriveType}, Größe={FormatBytes(e.TotalSize)})"
        ListBox1.Items.Add(msg)
    End Sub

    Private Sub _watcher_DriveRemoved(sender As Object, e As DriveRemovedEventArgs) Handles _watcher.DriveRemoved
        ListBox1.Items.Add($"Laufwerk entfernt: {e.DriveName}")
    End Sub

    Private Function FormatBytes(value As Long) As String
        Dim sizes = {"B","KB","MB","GB","TB"}
        Dim len = CDbl(value)
        Dim order = 0
        While len >= 1024 AndAlso order < sizes.Length - 1
            order += 1
            len /= 1024
        End While
        Return $"{len:0.##} {sizes(order)}"
    End Function
End Class
```

<a name="check-auf-isready"></a>
### 7.3 Check auf `IsReady`

```vbnet
Private Sub _watcher_DriveAdded(sender As Object, e As DriveAddedEventArgs) Handles _watcher.DriveAdded
    If e.IsReady Then
        Debug.WriteLine($"Bereit: {e.DriveName} - {e.VolumeLabel}")
    Else
        Debug.WriteLine($"Noch nicht bereit: {e.DriveName}")
    End If
End Sub
```

<a name="filtern-nur-bestimmter-laufwerkstypen"></a>
### 7.4 Filtern nur bestimmter Laufwerkstypen

```vbnet
If e.DriveType = IO.DriveType.Removable Then
    ' Nur Wechseldatenträger verarbeiten
End If
```

---

<a name="8-entwurfsentscheidungen"></a>
## 8. Entwurfsentscheidungen

| Entscheidung | Begründung |
|--------------|-----------|
| Nutzung eines versteckten `NativeWindow` | Direkter Zugriff auf `WM_DEVICECHANGE` ohne zusätzliches UI |
| Strukturierte EventArgs statt `DriveInfo` direkt | Entkoppelung & Serialisierbarkeit / Stabilität bei entferntem Medium |
| Prüfung `IsReady` | Verhindert Zugriffe auf nicht initialisierte Medien (z. B. langsame USB-Sticks) |
| Keine Polling-Logik | Effizienter, reagiert nur auf Systemereignisse |

---

<a name="9-fehler--und-sonderfälle"></a>
## 9. Fehler- und Sonderfälle

| Fall | Verhalten |
|------|----------|
| Laufwerk nicht bereit (`IsReady=False`) | Größen-/Formatwerte = 0 / leer |
| Sehr schneller Ein-/Aussteck-Zyklus | Windows liefert u. U. nur ein Teil der erwarteten Meldungen |
| Netzwerk-Laufwerke trennen | Ereignis kann verzögert kommen |
| Virtuelle Laufwerke (ISO-Mount) | Werden i. d. R. als Volume erkannt |
| Zugriff verweigert | Größen/Labels evtl. nicht lesbar |

> **Hinweis**
>
> Bei den Tests, das ISO-datei beim mounten nicht immer als laufwerk erkannt wird. 
>
> Dies scheint von der Windows-Version und dem verwendeten Tool abzuhängen.

---

<a name="10-performance-hinweise"></a>
## 10. Performance-Hinweise

- Ereignisse treten nur bei Änderungen auf → geringer Overhead
- Kein Timer/Polling → CPU-schonend
- Zugriff auf `DriveInfo` ist relativ leichtgewichtig; kostspielige I/O (z. B. Verzeichnis-Scans) im Eventhandler asynchron auslagern.

---

<a name="11-sicherheit--berechtigungen"></a>
## 11. Sicherheit / Berechtigungen

- Erfordert normale Benutzerrechte
- Adminrechte nur nötig, falls nachträgliche Operationen auf geschützten Laufwerken erfolgen sollen
- Kein direkter Schreibzugriff durch Komponente selbst

---

<a name="12-test--diagnosehinweise"></a>
## 12. Test- & Diagnosehinweise

| Test | Vorgehen |
|------|----------|
| USB-Stick hinzufügen | Erwartet: `DriveAdded` mit korrekter Größe |
| USB-Stick entfernen | Erwartet: `DriveRemoved` |
| Netzlaufwerk verbinden/trennen | Prüfen auf Ereignisauslösung |
| Nicht bereites Medium (z. B. Kartenleser ohne Karte) | `IsReady=False` |
| Mehrfach schnell einstecken | Prüfen auf Stabilität |

---

<a name="13-logging-tipp"></a>
### 13. Logging-Tipp

```vbnet
Private Sub _watcher_DriveAdded(sender As Object, e As DriveAddedEventArgs) Handles _watcher.DriveAdded
    Debug.WriteLine($"[{Now:HH:mm:ss}] ADD {e.DriveName} Ready={e.IsReady}")
End Sub
```

---

<a name="14-erweiterungsideen"></a>
## 14. Erweiterungsideen

| Idee | Nutzen |
|------|-------|
| Ereignis für Änderung freier Speicherplatz | Laufzeit-Monitoring (z. B. Warnungen) |
| Black-/Whitelist von Laufwerken | Reduktion von Eventrauschen |
| Unterstützung mehrerer Plattformen | (Derzeit Windows-spezifisch) |
| Async Callback / Task-basierte API | Modernere Nutzung in Async-Umgebungen |
| Ereignis für Medienwechsel (CD/DVD) | Vollständigere Abdeckung optischer Medien |

---

<a name="15-kurzübersicht-cheat-sheet"></a>
## 15. Kurzübersicht (Cheat Sheet)

| Element | Kurzbeschreibung |
|---------|------------------|
| `DriveWatcher` | Komponente zur Laufwerksüberwachung |
| `DriveAdded` | Event beim Hinzufügen eines Laufwerks |
| `DriveRemoved` | Event beim Entfernen eines Laufwerks |
| `DriveAddedEventArgs` | Enthält vollständige Laufwerksinfo (falls bereit) |
| `DriveRemovedEventArgs` | Enthält nur den Namen des entfernten Laufwerks |
| `IsReady=False` | Medium (noch) nicht zugreifbar |

---

<a name="16-faq"></a>
## 16. FAQ

**F: Warum manchmal leere/0-Werte?**  
A: Das Laufwerk war beim Eventempfang noch nicht bereit (`IsReady=False`). Später erneut prüfen.

**F: Funktioniert das unter .NET Core / .NET 6+?**  
A: Prinzipiell ja, sofern `System.Windows.Forms` unter Windows verwendet wird. Mögliche Anpassungen bei Interop nötig.

**F: Werden auch Ordner-Mountpoints erkannt?**  
A: Nur, wenn Windows sie als Volume-Broadcast meldet.

---

<a name="17-weitere-literatur"></a>
## 17. weitere Literatur

- [Erstellen eines Windows Forms-Toolbox-Steuerelements](https://docs.microsoft.com/de-de/visualstudio/extensibility/creating-a-windows-forms-toolbox-control?view=vs-2022)
- [Infos zur ControlStyles Enumeration](https://learn.microsoft.com/de-de/dotnet/api/system.windows.forms.controlstyles?redirectedfrom=MSDN&view=netframework-4.7.2)
- [Control-Techniken: Eigenes Toolboxicon für Steuerelement](https://www.vb-paradise.de/index.php/Thread/123746-Control-Techniken-Eigenes-Toolboxicon-f%C3%BCr-Steuerelement/)
- [Erkennen, wenn Wechseldatenträger angeschlossen/entfernt werden](https://www.vbarchiv.net/tipps/tipp_1928-erkennen-wenn-wechseldatentraeger-angeschlossen-entfernt-werden.html)
