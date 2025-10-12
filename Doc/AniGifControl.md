# AniGifControl

Ausführliche Dokumentation für das WinForms-Steuerelement `AniGif` zur Anzeige animierter GIF-Grafiken.

## Einführung

Grundlage und Anregung für dieses Steuerelement stammen aus dem Buch 
**"Visual Basic 2015 - Grundlagen und Profiwissen"** von Walter Dobrenz und Thomas Gewinnus.

Der ursprüngliche Quelltext wurde von mir verändert und um weitere Funktionen erweitert.

Dieser Code sollte für mich als Übung dienen und ich denke das er auch für andere Anfänger 
interessant sein dürfte.

Weitere Infos unter: 

[HANSER Fachbuch](https://www.hanser-fachbuch.de/fachbuch/artikel/9783446446052) 

[Buchleser freigegeben auf onedrive](https://onedrive.live.com/?id=root&cid=D73E81A6F971DBA7&qt=people&personId=de18bb46da92110)

---

## Inhaltsverzeichnis

1. [Überblick](#überblick)
2. [Features](#features)
3. [Schnellstart](#schnellstart)
4. [Installation & Einbindung](#installation--einbindung)
5. [Eigenschaften](#eigenschaften)
   - [Interne / ausgeblendete geerbte Eigenschaften](#interne--ausgeblendete-geerbte-eigenschaften)
   - [Validierung](#validierung)
6. [Ereignisse](#ereignisse)
7. [Enumerationen](#enumerationen)
    - [`SizeMode`](#sizemode)
8. [Funktionsweise der Animation](#funktionsweise-der-animation)
9. [Zeichen-/Größenlogik (`SizeMode` + Zoom)](#zeichengrößenlogik-sizemode--zoom)
10. [Beispiele (Code)](#beispiele-code)
    - [A: Standardanzeige (statisches GIF oder animiertes ohne Autoplay)](#a-standardanzeige-statisches-gif-oder-animiertes-ohne-autoplay)
    - [B: Vollständiges Füllen des Controls](#b-vollständiges-füllen-des-controls)
    - [C: Benutzerdefinierte Geschwindigkeit (langsamer)](#c-benutzerdefinierte-geschwindigkeit-langsamer)
    - [D: Dynamischer Bildwechsel zur Laufzeit](#d-dynamischer-bildwechsel-zur-laufzeit)
    - [E: Reaktion auf nicht animierbare Bilder](#e-reaktion-auf-nicht-animierbare-bilder)
    - [F: Manuelles Starten und Stoppen der Animation](#f-manuelles-starten-und-stoppen-der-animation)
11. [Performance-Hinweise](#performance-hinweise)
12. [Ressourcen / Standardbild](#ressourcen--standardbild)
13. [Fehlerbehandlung & Sonderfälle](#fehlerbehandlung--sonderfälle)
14. [Typische Anwendungsfälle](#typische-anwendungsfälle)
15. [FAQ](#faq)
16. [Geplante Erweiterungen (Ideen)](#geplante-erweiterungen-ideen)
17. [Anhang: Interne Methoden (Kurzreferenz)](#anhang-interne-methoden-kurzreferenz)
18. [Öffentliche Steuerungsmethoden (Details)](#öffentliche-steuerungsmethoden-details)
    - [`StartAnimation()`](#startanimation)
    - [`StopAnimation()`](#stopanimation)
19. [Weitere Literatur](#weitere-literatur)
---

<a name="überblick"></a>
## 1. Überblick

`AniGif` ist ein benutzerdefiniertes WinForms-Control zur Anzeige und optionalen Steuerung animierter GIFs. Es unterstützt:
- Automatische oder benutzerdefinierte Abspielgeschwindigkeit
- Verschiedene Darstellungsmodi (Original, Zentriert, Zoom, Fill)
- Ereignis bei nicht animierbaren Bildern
- Zoomfaktor bei `SizeMode.Zoom`
- Manuelles Starten/Stoppen der Animation über `StartAnimation()` / `StopAnimation()`

Das Control ist für .NET Framework 4.7.2 (aus dem Projekt abgeleitet) konzipiert und für den Toolbox-Einsatz vorbereitet.

---

<a name="features"></a>
## 2. Features

- Einfache Integration in vorhandene WinForms-Projekte
- Saubere Kapselung der GIF-Frame-Logik
- Wahlweise: interne GIF-Timings oder eigene FPS
- Ressourcen-Unterstützung (Fallback auf Standard-GIF)
- Doppelt gepuffertes Rendering (verringerte Flackereffekte)
- Design-Time-freundlich (keine Animation im Designer)
- Öffentliche Steuerungsmethoden: `StartAnimation()` und `StopAnimation()`

---

<a name="schnellstart"></a>
## 3. Schnellstart

```vbnet
' Instanz erstellen
dim ani As New AniGifControl.AniGif() With {
    .Dock = DockStyle.None,
    .AutoPlay = True,
    .GifSizeMode = AniGifControl.SizeMode.Zoom,
    .CustomDisplaySpeed = True,
    .FramesPerSecond = 15D,
    .ZoomFactor = 75D
}
ani.Gif = New Bitmap("C:\\temp\\anim.gif")
Me.Controls.Add(ani)
```

---

<a name="installation--einbindung"></a>
## 4. Installation & Einbindung

1. Projekt `AniGifControl` kompilieren.
2. DLL (falls als Bibliothek genutzt) referenzieren oder Projekt direkt zur Solution hinzufügen.
3. Control erscheint (durch `ProvideToolboxControlAttribute`) in der Toolbox-Kategorie „SchlumpfSoft Controls“.
4. Per Drag & Drop in ein Formular einfügen oder zur Laufzeit instanziieren.

Optional: Wenn das Control in ein VSIX eingebettet wird, wird die Bereitstellung über das Package (siehe `AniGifControlPackage.vb`) begünstigt.

---

<a name="eigenschaften"></a>
## 5. Eigenschaften

| Eigenschaft | Typ | Standard | Beschreibung |
|-------------|-----|----------|--------------|
| `AutoPlay` | `Boolean` | `False` | Startet Animation automatisch (sofern animierbar) |
| `Gif` | `Bitmap` | `My.Resources.Standard` | Aktuelles animiertes oder statisches Bild |
| `GifSizeMode` | `SizeMode` | `Normal` | Darstellungsmodus (Layout/Skalierung) |
| `CustomDisplaySpeed` | `Boolean` | `False` | Schaltet auf interne Timer-Animation mit FPS um |
| `FramesPerSecond` | `Decimal` | `10` | Wird nur bei `CustomDisplaySpeed=True` genutzt (1–50) |
| `ZoomFactor` | `Decimal` | `50` | Nur wirksam bei `GifSizeMode=Zoom` (1–100 %) |

<a name="interne--geerbte-eigenschaften"></a>
### Interne / ausgeblendete geerbte Eigenschaften

Mehrere Standard-Eigenschaften (`Text`, `BackgroundImage`, `Font`, etc.) sind absichtlich über `[Browsable(False)]` versteckt, da sie für den Anwendungsfall nicht sinnvoll oder irreführend wären.

<a name="validierung"></a>
### Validierung

- `FramesPerSecond` wird intern auf 1..50 begrenzt
- `ZoomFactor` wird intern auf 1..100 begrenzt
- Null-Zuweisung bei `Gif` → Fallback auf `My.Resources.Standard`

---

<a name="ereignisse"></a>
## 6. Ereignisse

| Ereignis | Sichtbarkeit | Beschreibung |
|----------|--------------|--------------|
| `NoAnimation` | Öffentlich | Wird ausgelöst, wenn ein Bild keine Animation enthält, aber `AutoPlay=True` gesetzt ist |
| `GifChanged` | Intern | Reinitialisiert Framelogik beim Bildwechsel |
| `CustomDisplaySpeedChanged` | Intern | Start/Stop des Timers basierend auf Benutzerumschaltung |
| `FramesPerSecondChanged` | Intern | Aktualisiert Timer-Intervall |

---

<a name="enumerationen"></a>
## 7. Enumerationen

<a name="sizemode"></a>
### `SizeMode`

| Wert | Beschreibung |
|------|--------------|
| `Normal` | Originalgröße, links oben |
| `CenterImage` | Originalgröße, zentriert |
| `Zoom` | Proportionale Skalierung anhand `ZoomFactor` (1–100 %) |
| `Fill` | Füllt das Control vollständig (größere Seite passend, evtl. Beschnitt) |

---

<a name="funktionsweise-der-animation"></a>
## 8. Funktionsweise der Animation

Es existieren zwei Animationspfade:
1. GIF-interne Animation (Standard) → Verwendung von `ImageAnimator.UpdateFrames()` im `OnPaint`, sofern:
   - `AutoPlay=True`
   - `CustomDisplaySpeed=False`
   - Bild ist animierbar (`ImageAnimator.CanAnimate(Gif)=True`)
2. Benutzerdefinierte Geschwindigkeit → Interner `Timer`:
   - Aktiv bei `CustomDisplaySpeed=True`
   - Wechselt über `SelectActiveFrame` die Frames
   - FPS → `Timer.Interval = 1000 / FramesPerSecond`

Nicht animierbare Bilder erzeugen das Ereignis `NoAnimation` (und `_MaxFrame` wird 0 gesetzt).

---

<a name="zeichengrößenlogik-sizemode--zoom"></a>
## 9. Zeichen-/Größenlogik

Die Anzeigegröße wird in `GetRectStartSize` berechnet, Startpunkt in `GetRectStartPoint`.
Kriterien:

- Proportionalität bleibt bei `Zoom` und `Fill` erhalten
- `Fill` maximiert die Ausnutzung des Controls ohne Leerräume
- `Zoom` basiert auf Control-Seite mit passender Relation *und* `ZoomFactor`
- Zentrierung erfolgt bei allen Modi außer `Normal`

---

<a name="beispiele-code"></a>
## 10. Beispiele (Code)

<a name="a-standardanzeige-statisches-gif-oder-animiertes-ohne-autoplay"></a>
### A: Standardanzeige (statisches GIF oder animiertes ohne Autoplay)

```vbnet
dim ani = New AniGifControl.AniGif() With {
    .AutoPlay = False,
    .GifSizeMode = AniGifControl.SizeMode.CenterImage
}
ani.Gif = New Bitmap("logo.gif")
Me.Controls.Add(ani)
```

<a name="b-vollständiges-füllen-des-controls"></a>
### B: Vollständiges Füllen des Controls

```vbnet
dim aniFill = New AniGifControl.AniGif() With {
    .Dock = DockStyle.Fill,
    .AutoPlay = True,
    .GifSizeMode = AniGifControl.SizeMode.Fill
}
aniFill.Gif = New Bitmap("spinner.gif")
Me.Controls.Add(aniFill)
```

<a name="c-benutzerdefinierte-geschwindigkeit-langsamer"></a>
### C: Benutzerdefinierte Geschwindigkeit (langsamer)

```vbnet
dim aniSlow = New AniGifControl.AniGif() With {
    .AutoPlay = True,
    .CustomDisplaySpeed = True,
    .FramesPerSecond = 5D ' langsamer
}
aniSlow.Gif = New Bitmap("anim.gif")
Me.Controls.Add(aniSlow)
```

<a name="d-dynamischer-bildwechsel-zur-laufzeit"></a>
### D: Dynamischer Bildwechsel zur Laufzeit

```vbnet
Private Sub ButtonLoadNewGif_Click(...) Handles ButtonLoadNewGif.Click
    Using ofd As New OpenFileDialog()
        ofd.Filter = "GIF Dateien|*.gif"
        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                Dim bmp = CType(Image.FromFile(ofd.FileName), Bitmap)
                AniGif1.Gif = bmp
            Catch ex As Exception
                MessageBox.Show("Fehler beim Laden: " & ex.Message)
            End Try
        End If
    End Using
End Sub
```

<a name="e-reaktion-auf-nicht-animierbare-bilder"></a>
### E: Reaktion auf nicht animierbare Bilder

```vbnet
AddHandler AniGif1.NoAnimation, Sub(s, e)
    ToolStripStatusLabel1.Text = "Grafik ist nicht animierbar"
End Sub
```

<a name="f-manuelles-starten-und-stoppen-der-animation"></a>
### F: Manuelles Starten und Stoppen der Animation

```vbnet
' Autoplay bewusst deaktivieren
dim aniManual = New AniGifControl.AniGif() With {
    .AutoPlay = False,
    .GifSizeMode = AniGifControl.SizeMode.Zoom
}
aniManual.Gif = New Bitmap("anim.gif")
Me.Controls.Add(aniManual)

' Später z.B. über Buttons
aniManual.StartAnimation()
aniManual.StopAnimation()
```

---

<a name="performance-hinweise"></a>
## 11. Performance-Hinweise

- Große GIFs oder hohe FPS-Werte erhöhen CPU-Last
- `DoubleBuffered=True` reduziert Flackern
- Vermeiden: Sehr hohe `FramesPerSecond` (nahe 50) bei mehreren Instanzen
- Wenn Skalierung teuer ist: Vorab konvertieren / optimieren

---

<a name="ressourcen--standardbild"></a>
## 12. Ressourcen / Standardbild

Beim Initialisieren wird `_Gif = My.Resources.Standard` gesetzt. Dieses dient als Fallback:
- Wenn kein Bild geladen ist
- Wenn `Gif = Nothing` zugewiesen wird

Empfehlung: Eigenes neutrales Platzhalter-GIF ersetzen, falls Branding gewünscht.

---

<a name="fehlerbehandlung--sonderfälle"></a>
## 13. Fehlerbehandlung & Sonderfälle

| Fall | Verhalten |
|------|-----------|
| Ungültige Datei | Exception beim Laden (außerhalb des Controls) → Vor dem Setzen prüfen |
| Nicht animierbares GIF | `NoAnimation` wird ausgelöst, Frames = 0 |
| Designer-Modus | Keine Animation (Vermeidung unnötiger Verarbeitung) |
| `ZoomFactor` < 1 / > 100 | Intern korrigiert auf Bereich 1–100 |
| `FramesPerSecond` < 1 / > 50 | Intern korrigiert auf 1–50 |

---

<a name="typische-anwendungsfälle"></a>
## 14. Typische Anwendungsfälle

- Lade-/Busy-Indikatoren
- Animierte Statusanzeigen
- Tutorial-/Hinweis-Overlays (z.B. blinkende Icons)
- Platzhalter-Animation während Datenoperationen

---

<a name="faq"></a>
## 15. FAQ
**F: Warum bewegt sich das GIF im Designer nicht?**  
A: Im Designmodus wird bewusst nicht animiert, um Ressourcen zu schonen.

**F: Kann ein transparentes GIF verwendet werden?**  
A: Ja, Transparenz wird von `Bitmap`/GDI+ unterstützt.

**F: Warum ruckelt die Animation bei hoher CPU-Last?**  
A: Der UI-Thread steuert das Zeichnen. Hintergrundarbeit auslagern.

**F: Kann ich Videos (MP4) anzeigen?**  
A: Nein, nur GIF-Bilder (animiert oder statisch).

**F: Was passiert bei sehr großen GIFs (>10 MB)?**  
A: Speicher- und CPU-Verbrauch steigen. Vorverkleinern empfohlen.

---

<a name="geplante-erweiterungen-ideen"></a>
## 16. Geplante Erweiterungen (Ideen)

- Unterstützung für Schleifenbegrenzung
- Ereignis `FrameChanged`
- Asynchrones Laden aus Stream/URL
- Option: Qualitätsmodus (Interpolation / Pixelmodi)

---

<a name="anhang-interne-methoden-kurzreferenz"></a>
## 17. Anhang: Interne Methoden (Kurzreferenz)

| Methode | Zweck |
|---------|-------|
| `InitializeValues` | Setzt Startwerte |
| `SetGifImage` | Lädt/ersetzt aktuelles GIF + Ereignis |
| `SetGifSizeMode` | Anzeigeart setzen + Neuzeichnen |
| `SetCustomDisplaySpeed` | Umschalten Timersteuerung |
| `SetZoomFactor` | Zoom prüfen & anwenden |
| `CheckFramesPerSecondValue` | Bereichsprüfung FPS |
| `CheckZoomFactorValue` | Bereichsprüfung Zoom |
| `GetRectStartSize` | Zielgröße nach Modus |
| `GetRectStartPoint` | Startpunkt nach Modus |
| `StartAnimation` | Aktiviert `AutoPlay`, initialisiert und startet ggf. `ImageAnimator`/Timer |
| `StopAnimation` | Deaktiviert `AutoPlay`, stoppt Timer & `ImageAnimator` |

---

<a name="öffentliche-steuerungsmethoden-details"></a>
## 18. Öffentliche Steuerungsmethoden (Details)

<a name="startanimation"></a>
### `StartAnimation()`

Startet die Animation, falls `AutoPlay=False`. Intern wird:
- `_Autoplay` auf `True` gesetzt
- `InitLayout()` aufgerufen (registriert Animation bei animierbaren GIFs)

<a name="stopanimation"></a>
### `StopAnimation()`

Stoppt eine laufende Animation. Intern wird:
- `_Autoplay` auf `False` gesetzt
- `ImageAnimator.StopAnimate` (falls animierbar) ausgeführt
- Der interne `Timer` gestoppt (bei benutzerdefinierter Geschwindigkeit)

Einsatzszenario: Wenn man GIFs erst nach einer Benutzeraktion (Button, Hover, Sichtbarkeit) animieren möchte.

---

<a name="weitere-literatur"></a>
## 19. Weitere Literatur

-  [Erstellen eines Windows Forms-Toolbox-Steuerelements](https://docs.microsoft.com/de-de/visualstudio/extensibility/creating-a-windows-forms-toolbox-control?view=vs-2022)
-  [Infos zur ControlStyles Enumeration](https://learn.microsoft.com/de-de/dotnet/api/system.windows.forms.controlstyles?redirectedfrom=MSDN&view=netframework-4.7.2)
-  [Control-Techniken: Eigenes Toolboxicon für Steuerelement](https://www.vb-paradise.de/index.php/Thread/123746-Control-Techniken-Eigenes-Toolboxicon-f%C3%BCr-Steuerelement/)
-  [FrameDelays von animierter GIF](https://foren.activevb.de/archiv/vb-net/thread-93030/beitrag-93069/FrameDelays-von-animierter-GIF/)
