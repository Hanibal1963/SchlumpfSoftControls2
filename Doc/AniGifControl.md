# AniGifControl

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

## Überblick

`AniGif` ist ein benutzerdefiniertes WinForms-Control zur Anzeige und optionalen Steuerung animierter GIFs. Es unterstützt:
- Automatische oder benutzerdefinierte Abspielgeschwindigkeit
- Verschiedene Darstellungsmodi (Original, Zentriert, Zoom, Fill)
- Ereignis bei nicht animierbaren Bildern
- Zoomfaktor bei `SizeMode.Zoom`
- Manuelles Starten/Stoppen der Animation über `StartAnimation()` / `StopAnimation()`

Das Control ist für .NET Framework 4.7.2 (aus dem Projekt abgeleitet) konzipiert und für den Toolbox-Einsatz vorbereitet.

---

## Features

- Einfache Integration in vorhandene WinForms-Projekte
- Saubere Kapselung der GIF-Frame-Logik
- Wahlweise: interne GIF-Timings oder eigene FPS
- Ressourcen-Unterstützung (Fallback auf Standard-GIF)
- Doppelt gepuffertes Rendering (verringerte Flackereffekte)
- Design-Time-freundlich (keine Animation im Designer)
- Öffentliche Steuerungsmethoden: `StartAnimation()` und `StopAnimation()`

---

## Funktionsweise der Animation

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

## Zeichen-/Größenlogik

Die Anzeigegröße wird in `GetRectStartSize` berechnet, Startpunkt in `GetRectStartPoint`.
Kriterien:

- Proportionalität bleibt bei `Zoom` und `Fill` erhalten
- `Fill` maximiert die Ausnutzung des Controls ohne Leerräume
- `Zoom` basiert auf Control-Seite mit passender Relation *und* `ZoomFactor`
- Zentrierung erfolgt bei allen Modi außer `Normal`

---

## Performance-Hinweise

- Große GIFs oder hohe FPS-Werte erhöhen CPU-Last
- `DoubleBuffered=True` reduziert Flackern
- Vermeiden: Sehr hohe `FramesPerSecond` (nahe 50) bei mehreren Instanzen
- Wenn Skalierung teuer ist: Vorab konvertieren / optimieren

---

## Ressourcen / Standardbild

Beim Initialisieren wird `_Gif = My.Resources.Standard` gesetzt. Dieses dient als Fallback:
- Wenn kein Bild geladen ist
- Wenn `Gif = Nothing` zugewiesen wird

Empfehlung: Eigenes neutrales Platzhalter-GIF ersetzen, falls Branding gewünscht.

---

## Typische Anwendungsfälle

- Lade-/Busy-Indikatoren
- Animierte Statusanzeigen
- Tutorial-/Hinweis-Overlays (z.B. blinkende Icons)
- Platzhalter-Animation während Datenoperationen

---

## FAQ
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

## Weitere Literatur

-  [Erstellen eines Windows Forms-Toolbox-Steuerelements](https://docs.microsoft.com/de-de/visualstudio/extensibility/creating-a-windows-forms-toolbox-control?view=vs-2022)
-  [Infos zur ControlStyles Enumeration](https://learn.microsoft.com/de-de/dotnet/api/system.windows.forms.controlstyles?redirectedfrom=MSDN&view=netframework-4.7.2)
-  [Control-Techniken: Eigenes Toolboxicon für Steuerelement](https://www.vb-paradise.de/index.php/Thread/123746-Control-Techniken-Eigenes-Toolboxicon-f%C3%BCr-Steuerelement/)
-  [FrameDelays von animierter GIF](https://foren.activevb.de/archiv/vb-net/thread-93030/beitrag-93069/FrameDelays-von-animierter-GIF/)
