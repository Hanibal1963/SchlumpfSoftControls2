# AniGif Control

Ein Steuerelement welches zum Anzeigen animierter Grafiken dient.

---

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

## Eigenschaften Anzeigemodi und Ereignisse

<details>
<summary>Eigenschaften</summary>

-  **Gif** - Gibt die animierte Gif-Grafik zurück oder legt diese fest.
-  **AutoPlay** - Legt fest ob die Animation sofort nach dem laden gestartet wird.
-  **GifSizeMode** - Gibt die Art wie die Grafik angezeigt wird zurück oder legt diese fest.
-  **CustomDisplaySpeed** - Legt fest ob die im Bild gespeicherte Anzeigegeschwindigkeit oder die benutzerdefinierte verwendet werden soll.
-  **FramesPerSecond** - Legt die Anzahl der Bilder pro Sekunde fest (1-50) die angezeigt werden, wenn die Benutzerdefinierte Geschwindigkeit aktiv ist.
-  **ZoomFaktor** - Legt den Zoomfaktor für GifSizeMode "Zoom" in % (1-100) fest.

</details>

<details>
<summary> Anzeigemodi </summary>

Die Eigenschaft **"GifSizeMode"** kann folgende Werte annehmen:

-  **Normal** - Die Grafik wird in Originalgröße angezeigt (Ausrichtung oben links)
-  **CenterImage** - Die Grafik wird in Originalgröße angezeigt (zentrierte Ausrichtung)
-  **Zoom** - Die Grafik wird an die Größe des Steuerelementes angepasst (Die größere Ausdehnung der Grafik wird als Anpassung verwendet, die Ausrichtung erfolgt zentriert und das Seitenverhältnis bleibt erhalten)
-  **Fill** - Die Grafik wird in das Control eingepasst (unabhängig von ihrer Größe).

</details>

<details>
<summary> Ereignisse </summary>

-  **NoAnimation** - wird ausgelöst, wenn das Bild nicht animiert werden kann.

</details>

## Weitere Literatur

-  [Erstellen eines Windows Forms-Toolbox-Steuerelements](https://docs.microsoft.com/de-de/visualstudio/extensibility/creating-a-windows-forms-toolbox-control?view=vs-2022)
-  [Infos zur ControlStyles Enumeration](https://learn.microsoft.com/de-de/dotnet/api/system.windows.forms.controlstyles?redirectedfrom=MSDN&view=netframework-4.7.2)
-  [Control-Techniken: Eigenes Toolboxicon für Steuerelement](https://www.vb-paradise.de/index.php/Thread/123746-Control-Techniken-Eigenes-Toolboxicon-f%C3%BCr-Steuerelement/)
-  [FrameDelays von animierter GIF](https://foren.activevb.de/archiv/vb-net/thread-93030/beitrag-93069/FrameDelays-von-animierter-GIF/)

