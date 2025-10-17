' *************************************************************************************************
' IniFileCommentEdit.vb
' Copyright (c) 2025 by Andreas Sauer 
' 
' Übersicht:
' Dieses WinForms-Steuerelement dient zum Anzeigen und Bearbeiten von Kommentarzeilen (z. B. Kopf-
' oder Abschnittskommentare) einer INI-Datei. Der Kommentar wird als String-Array verwaltet, wobei
' jede Array-Zeile einer Textzeile entspricht.
'
' Kernkonzept:
' - Die öffentliche Eigenschaft `Comment` übernimmt/liefert die Kommentarzeilen.
' - Änderungen im Textfeld aktivieren den Übernehmen-Button.
' - Ein Klick auf den Button übernimmt die Textbox-Zeilen in `Comment` und löst das Ereignis
'   `CommentChanged` aus. Dieses Ereignis übergibt den Abschnittsnamen (`SectionName`) sowie die
'   Kommentarzeilen an den Abonnenten.
' - Die Eigenschaft `TitelText` steuert den Text der umgebenden `GroupBox`. Änderungen daran lösen
'   ein internes Ereignis aus, welches den UI-Text aktualisiert.
'
' Design‑Time:
' Public Members sind mit Attributen für die Anzeige im Eigenschafteneditor versehen
' (Browsable, Category, Description, Toolbox-Einträge).
'
' Hinweis:
' - `Comment` akzeptiert/liefer ein String()-Array. `Nothing` sollte vermieden werden (der Code
'   erwartet ein Array). Falls zur Laufzeit `Nothing` möglich ist, sollte der Aufrufer ein leeres
'   Array statt `Nothing` übergeben.
' - Die Änderungserkennung bei `Comment` erfolgt über `SequenceEqual`, sodass Reihenfolge und Inhalt
'   aller Zeilen berücksichtigt werden.
' *************************************************************************************************

Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms

Namespace IniFileControl

    ''' <summary>
    ''' Steuerelement zum Anzeigen und Bearbeiten des Datei- oder Abschnittskommentars einer INI-Datei.
    ''' </summary>
    ''' <remarks>
    ''' - Setzen Sie <see cref="Comment"/> um den anzuzeigenden Kommentar zu initialisieren.
    ''' - Ändern des Textes in der Textbox aktiviert den Übernehmen-Button.
    ''' - Ein Klick auf den Button übernimmt die Änderungen in <see cref="Comment"/> und löst
    '''   <see cref="CommentChanged"/> aus.
    ''' </remarks>
    ''' <example>
    ''' ' Beispiel: Ereignis abonnieren
    ''' ' AddHandler commentEdit1.CommentChanged, Sub(sender, args)
    ''' '     Debug.WriteLine($"Sektion: {args.SectionName}, Zeilen: {args.CommentLines.Length}")
    ''' ' End Sub
    ''' </example>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Steuerelement zum Anzeigen und Bearbeiten des Datei- oder Abschnitts- Kommentars einer INI - Datei.")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(IniFileControl.CommentEdit), "CommentEdit.bmp")>
    Public Class CommentEdit

        Inherits UserControl

#Region "Definition der internen Eigenschaftsvariablen"

        ' Enthält die einzelnen Kommentarzeilen. Jede Array-Position entspricht einer Textzeile.
        Private _Lines As String() = {""}

        ' Text, der in der GroupBox als Titel angezeigt wird.
        Private _TitelText As String

        ' Name des INI-Abschnitts, zu dem der Kommentar gehört (kann leer sein für Dateikommentar).
        Private _SectionName As String

#End Region

#Region "Definition der öffentlichen Ereignisse"

        ''' <summary>
        ''' Wird ausgelöst, wenn sich der Kommentartext geändert hat und per Button übernommen wurde.
        ''' </summary>
        ''' <remarks>
        ''' Dieses Ereignis folgt dem Muster: Änderungen werden erst nach Klick auf den Übernehmen-Button
        ''' propagiert, nicht bei jeder Texteingabe. So bleiben UI-Eingaben atomar.
        ''' </remarks>
        ''' <param name="sender">Die aktuelle Instanz von <see cref="CommentEdit"/>.</param>
        ''' <param name="e">
        ''' <see cref="CommentEditEventArgs"/> mit <c>SectionName</c> (Abschnitt) und <c>CommentLines</c> (Kommentarzeilen).
        ''' </param>
        <Description("Wird ausgelöst wenn sich der Kommentartext geändert hat.")>
        Public Event CommentChanged(sender As Object, e As CommentEditEventArgs)

#End Region

#Region "Definition der internen Ereignisse"

        ' Internes Ereignis: Signalisiert, dass die Eigenschaft `Comment` (Inhalt der Kommentarzeilen)
        ' programmatisch geändert wurde. Dient dazu, die UI (Textbox) synchron zu halten.
        Private Event PropCommentChanged()

        ' Internes Ereignis: Signalisiert, dass sich der Titeltext geändert hat, damit die GroupBox
        ' ihren Text aktualisieren kann.
        Private Event TitelTextChanged()

#End Region

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see cref="CommentEdit"/>.
        ''' </summary>
        Public Sub New()
            ' Dieser Aufruf ist für den Designer erforderlich.
            Me.InitializeComponent()

            ' Initialwerte nach der Designer-Initialisierung:
            ' - Den aktuellen GroupBox-Titel als Ausgangswert für `TitelText` übernehmen.
            ' - Den Übernehmen-Button deaktivieren, bis eine Änderung erfolgt.
            Me._TitelText = Me.GroupBox.Text
            Me.Button.Enabled = False
        End Sub

#Region "Definition der neuen Eigenschaften"

        ''' <summary>
        ''' Gibt den Text der Titelzeile (GroupBox) zurück oder legt diesen fest.
        ''' </summary>
        ''' <remarks>
        ''' Das Setzen löst intern <see cref="TitelTextChanged"/> aus, wodurch der UI-Text aktualisiert wird.
        ''' </remarks>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den Text der Titelzeile zurück oder legt diesen fest.")>
        Public Property TitelText As String
            Set(value As String)
                ' Nur reagieren, wenn sich der Wert tatsächlich ändert
                If Me._TitelText <> value Then
                    Me._TitelText = value
                    ' UI-Aktualisierung entkoppelt über internes Ereignis
                    RaiseEvent TitelTextChanged()
                End If
            End Set
            Get
                Return Me._TitelText
            End Get
        End Property

        ''' <summary>
        ''' Gibt die Kommentarzeilen zurück oder legt diese fest.
        ''' </summary>
        ''' <remarks>
        ''' - Jede Array-Position entspricht einer Zeile in der Textbox.
        ''' - Änderungserkennung erfolgt per <see cref="Enumerable.SequenceEqual(Of TSource)"/>.
        ''' - Das Setzen löst intern <see cref="PropCommentChanged"/> aus, wodurch die Textbox synchronisiert wird.
        ''' </remarks>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den Kommentartext zurück oder legt diesen fest.")>
        Public Property Comment As String()
            Get
                Return Me._Lines
            End Get
            Set
                ' SequenceEqual stellt sicher, dass sowohl Anzahl als auch Inhalt der Zeilen übereinstimmen
                If Not Me._Lines.SequenceEqual(Value) Then
                    Me._Lines = Value
                    ' UI aktualisieren (Textbox füllen, Button-Zustand setzen)
                    RaiseEvent PropCommentChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gibt den Namen des INI-Abschnitts zurück oder legt diesen fest, für den der Kommentar angezeigt wird.
        ''' </summary>
        ''' <remarks>
        ''' Dieser Name wird zusammen mit den Kommentarzeilen im Ereignis <see cref="CommentChanged"/> übermittelt.
        ''' </remarks>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den Name des Abschnitts zurück oder legt diesen fest für den der Kommentar angezeigt werden soll.")>
        Public Property SectionName As String
            Get
                Return Me._SectionName
            End Get
            Set
                Me._SectionName = Value
            End Set
        End Property

#End Region

#Region "Definition der internen Ereignisbehandlungen"

        ''' <summary>
        ''' Klick auf den Übernehmen-Button: übernimmt die aktuellen Textbox-Zeilen und meldet die Änderung.
        ''' </summary>
        ''' <param name="sender">Button</param>
        ''' <param name="e">Nicht verwendet</param>
        Private Sub Button_Click(sender As Object, e As EventArgs) Handles Button.Click
            ' Geänderten Kommentar aus der Textbox in das interne Array übernehmen
            Me._Lines = Me.TextBox.Lines

            ' Button deaktivieren, da die Änderungen jetzt übernommen sind
            Me.Button.Enabled = False

            ' Änderung nach außen signalisieren (inkl. Abschnittsname)
            RaiseEvent CommentChanged(Me, New CommentEditEventArgs(Me._SectionName, Me._Lines))
        End Sub

        ''' <summary>
        ''' Text wurde in der Textbox geändert: aktiviert den Übernehmen-Button.
        ''' </summary>
        ''' <param name="sender">Textbox</param>
        ''' <param name="e">Nicht verwendet</param>
        Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox.TextChanged
            ' Aktiviert den Button, um die Änderungen explizit übernehmen zu können
            Me.Button.Enabled = True
        End Sub

        ''' <summary>
        ''' Reaktion auf programmatische Änderung von <see cref="Comment"/>:
        ''' synchronisiert die Textbox mit den aktuellen Kommentarzeilen.
        ''' </summary>
        Private Sub IniFileCommentEdit_PropCommentChanged() Handles Me.PropCommentChanged
            ' Kommentarzeilen in die Textbox eintragen (ersetzt den gesamten Inhalt)
            Me.TextBox.Lines = Me._Lines

            ' Änderungen wurden übernommen -> Button deaktivieren
            Me.Button.Enabled = False
        End Sub

        ''' <summary>
        ''' Reaktion auf Änderung von <see cref="TitelText"/>:
        ''' setzt den Text der GroupBox.
        ''' </summary>
        Private Sub IniFileCommentEdit_TitelTextChanged() Handles Me.TitelTextChanged
            Me.GroupBox.Text = Me._TitelText
        End Sub

#End Region

    End Class

End Namespace