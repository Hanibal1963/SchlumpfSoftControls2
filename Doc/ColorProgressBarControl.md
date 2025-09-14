# ColorProgressBarControl

Ein Control zum Anzeigen eines farbigen Fortschrittsbalkens.

---

## Einführung

Die Idee hinter dem `ColorProgressBarControl` ist es, einen Fortschrittsbalken zu erstellen, der in optisch anpassbar ist.

Der Standard-Fortschrittsbalken in Windows ist ein einfacher Balken, der den Fortschritt in Form einer Füllung anzeigt. 
Der `ColorProgressBarControl` hingegen kann in verschiedenen Farben und Stilen angezeigt werden.

Als Anregung diente der Artikel [A Better ProgressBar - Using Panels!](https://www.codeproject.com/Articles/31903/A-Better-ProgressBar-Using-Panels) von Saul Johnson.

Da die Donwnloads auf der Seite nicht mehr zu funktionieren scheinen und die Beschreibung nur Ausschnitte aus dem Original C# Code enthält und ich wenig Ahnung von C# habe, habe ich das Control in VB NET umgesetzt.

---

## Eigenschaften und Ereignisse

<details>
<summary>Eigenschaften</summary>

- **Value** - Gibt den Gesamtfortschritt des Fortschrittsbalkens zurück oder legt diesen fest.
- **ProgressMaximumValue** - Gibt den Maximalwert des Fortschrittsbalkens zurück oder legt diesen fest.
- **BarColor** - Gibt die Farbe des Fortschrittsbalkens zurück oder legt diese fest.
- **EmptyColor** - Gibt die Farbe des leeren Fortschrittsbalkens zurück oder legt diese fest.
- **BorderColor** - Gibt die Farbe des Rahmens zurück oder legt diese fest.
- **ShowBorder** - Gibt an, ob der Rahmen auf der Fortschrittsanzeige aktiviert ist.
- **IsGlossy** - Gibt an, ob der Glanz auf der Fortschrittsleiste angezeigt wird.

</details>
