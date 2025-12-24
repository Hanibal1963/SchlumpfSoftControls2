# DriveWatcherControl

## Einführung

Grundlage und Anregung für dieses Control stammen aus dem Internet.

[ActiveVB - VB.NET Tipp 0055:Hinzufügen und Entfernen von USB-Wechselmedien erkennen](http://www.activevb.de/tipps/vbnettipps/tipp0055.html)

---

## Überblick

`DriveWatcher` ist eine nicht-visuelle Komponente (erbt von `System.ComponentModel.Component`) zur Überwachung von Änderungen an logischen Laufwerken (Volumes) unter Windows. Sie meldet:
- Hinzufügen (Einhängen) eines Laufwerks (z. B. USB-Stick, Netzlaufwerk, virtuelle Laufwerke)
- Entfernen (Aushängen) eines Laufwerks

Die Komponente ist für WinForms-Projekte ausgelegt und verwendet ein internes unsichtbares Fenster (`NativeWindow`), um auf Systemnachrichten (Windows Messages) zu reagieren (`WM_DEVICECHANGE`).

---

## Funktionsumfang

| Funktion | Beschreibung |
|----------|--------------|
| Überwachung von Laufwerksänderungen | Erkennt dynamisch hinzugefügte und entfernte Volumes. |
| Übergabe detaillierter Laufwerksinformationen | Beim Hinzufügen: Name, Label, Größen-/Speicherinformationen, Dateisystemtyp, Laufwerkstyp, Bereitschaft. |
| Einfache Integration | Drag & Drop ins Komponentenfenster oder programmatische Instanziierung. |
| Ressourcenbereinigung | Implementiert korrektes `Dispose`-Pattern. |

---

## Architektur & interne Arbeitsweise

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

## Einsatzszenarien

- Automatisches Erkennen von USB-Sticks zur Datensicherung
- Monitoring von Netzlaufwerken (Verfügbarkeit)
- Triggern von Importprozessen beim Einstecken externer Medien
- Anzeige dynamischer Laufwerkslisten in UI-Elementen

---

## Entwurfsentscheidungen

| Entscheidung | Begründung |
|--------------|-----------|
| Nutzung eines versteckten `NativeWindow` | Direkter Zugriff auf `WM_DEVICECHANGE` ohne zusätzliches UI |
| Strukturierte EventArgs statt `DriveInfo` direkt | Entkoppelung & Serialisierbarkeit / Stabilität bei entferntem Medium |
| Prüfung `IsReady` | Verhindert Zugriffe auf nicht initialisierte Medien (z. B. langsame USB-Sticks) |
| Keine Polling-Logik | Effizienter, reagiert nur auf Systemereignisse |

---

## Fehler- und Sonderfälle

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

## Performance-Hinweise

- Ereignisse treten nur bei Änderungen auf → geringer Overhead
- Kein Timer/Polling → CPU-schonend
- Zugriff auf `DriveInfo` ist relativ leichtgewichtig; kostspielige I/O (z. B. Verzeichnis-Scans) im Eventhandler asynchron auslagern.

---

## Sicherheit / Berechtigungen

- Erfordert normale Benutzerrechte
- Adminrechte nur nötig, falls nachträgliche Operationen auf geschützten Laufwerken erfolgen sollen
- Kein direkter Schreibzugriff durch Komponente selbst

---

## FAQ

**F: Warum manchmal leere/0-Werte?**  
A: Das Laufwerk war beim Eventempfang noch nicht bereit (`IsReady=False`). Später erneut prüfen.

**F: Funktioniert das unter .NET Core / .NET 6+?**  
A: Prinzipiell ja, sofern `System.Windows.Forms` unter Windows verwendet wird. Mögliche Anpassungen bei Interop nötig.

**F: Werden auch Ordner-Mountpoints erkannt?**  
A: Nur, wenn Windows sie als Volume-Broadcast meldet.

---

## weitere Literatur

- [Erstellen eines Windows Forms-Toolbox-Steuerelements](https://docs.microsoft.com/de-de/visualstudio/extensibility/creating-a-windows-forms-toolbox-control?view=vs-2022)
- [Infos zur ControlStyles Enumeration](https://learn.microsoft.com/de-de/dotnet/api/system.windows.forms.controlstyles?redirectedfrom=MSDN&view=netframework-4.7.2)
- [Control-Techniken: Eigenes Toolboxicon für Steuerelement](https://www.vb-paradise.de/index.php/Thread/123746-Control-Techniken-Eigenes-Toolboxicon-f%C3%BCr-Steuerelement/)
- [Erkennen, wenn Wechseldatenträger angeschlossen/entfernt werden](https://www.vbarchiv.net/tipps/tipp_1928-erkennen-wenn-wechseldatentraeger-angeschlossen-entfernt-werden.html)
