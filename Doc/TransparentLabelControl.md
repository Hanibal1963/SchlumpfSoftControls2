# TransparenLabelControl

Ein .NET Windows Forms-Steuerelement zum Anzeigen eines Textes mit durchscheinendem Hintergrund.

---

## Übersicht

`TransparentLabel` ist ein benutzerdefiniertes Label-Control, das es ermöglicht, Text mit transparentem Hintergrund darzustellen. 

Es eignet sich besonders für Oberflächen, bei denen der Hintergrund durchscheinen soll, z. B. bei überlagerten Texten auf Bildern oder farbigen Flächen.

Die Idee hinter diesem Projekt ist, z.Bsp. einen Text teilweise über ein Bild 
zu legen ohne sich großartig Gedanken über 
Grafikroutinen zu machen.

Mit diesem Control ist das in wenigen Zeilen Code erledigt bzw. im 
Designer zusammengeklickt.

---

## Eigenschaften

### Ausgeblendete Eigenschaften

Die folgenden Eigenschaften sind im Designer und zur Laufzeit ausgeblendet, da sie für dieses Control nicht relevant sind:

- `BackColor`
- `BackgroundImage`
- `BackgroundImageLayout`
- `FlatStyle`

Diese Eigenschaften sind zwar technisch vorhanden, werden aber nicht angezeigt oder verwendet.

---

## Konstruktor

Initialisiert eine neue Instanz des `TransparentLabel`. Setzt die erforderlichen Styles für Transparenz.

---

## Methoden und Funktionen

### Dispose

Bereinigt die von `TransparentLabel` verwendeten Ressourcen.

**Parameter:**
- `disposing` – Gibt an, ob verwaltete Ressourcen freigegeben werden sollen.

---

### CreateParams

Gibt die Erstellungsparameter für das Steuerelement zurück und aktiviert die Transparenz durch Setzen des `WS_EX_TRANSPARENT`-Stils.

---

### InitializeStyles

Setzt die erforderlichen ControlStyles, um Transparenz zu ermöglichen.

---

### InitializeComponent

Initialisiert die Komponenten des Steuerelements (wird vom Designer verwendet).

---

## Beispiel

Einbinden des Controls in ein Formular:

```vbnet
Imports TransparentLabelControl

Public Class MainForm Inherits Form

    Private transparentLabel As TransparentLabel

    Public Sub New()

        Me.transparentLabel = New TransparentLabel()
        Me.transparentLabel.Text = "Transparenter Text"
        Me.transparentLabel.Font = New Font("Arial", 24, FontStyle.Bold)
        Me.transparentLabel.ForeColor = Color.White
        Me.transparentLabel.Location = New Point(50, 50)
        Me.transparentLabel.AutoSize = True

        ' Hintergrundbild für das Formular setzen
        Me.BackgroundImage = Image.FromFile("background.jpg")
        Me.BackgroundImageLayout = ImageLayout.Stretch

        ' TransparentLabel zum Formular hinzufügen
        Me.Controls.Add(Me.transparentLabel)

        ' Formulareigenschaften setzen
        Me.Text = "TransparentLabel Beispiel"
        Me.Size = New Size(800, 600)

    End Sub

End Class
```

---

## Hinweise

- Das Control ist für die Verwendung im Designer geeignet.
- Die Transparenz wird durch den Windows-Stil `WS_EX_TRANSPARENT` und entsprechende ControlStyles realisiert.
- Die Hintergrundfarbe und Hintergrundbilder werden ignoriert.

---

## Lizenz

Copyright (c) 2025 Andreas Sauer

---

## Weiterführende Links

- [Browsable-Attribut](https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.browsableattribute?view=netframework-4.7.2)
- [Category-Attribut](https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.categoryattribute?view=netframework-4.7.2)
- [Description-Attribut](https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.descriptionattribute?view=netframework-4.7.2)

