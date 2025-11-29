' *************************************************************************************************
' IniFileCommentEdit.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System.Linq

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
    <System.ComponentModel.Description("Steuerelement zum Anzeigen und Bearbeiten des Datei- oder Abschnitts- Kommentars einer INI - Datei.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(IniFileControl.CommentEdit), "CommentEdit.bmp")>
    Public Class CommentEdit : Inherits System.Windows.Forms.UserControl

#Region "Variablendefinition"

        ''' <summary>
        ''' Vom Windows Forms Designer verwaltete Komponentenliste.
        ''' </summary>
        Private ReadOnly components As System.ComponentModel.IContainer

        ''' <summary>
        ''' Umschließende Gruppe zur Darstellung des Titels und der enthaltenen Steuerelemente.
        ''' </summary>
        Private WithEvents GroupBox As System.Windows.Forms.GroupBox

        ''' <summary>
        ''' Schaltfläche zum Übernehmen der in der Textbox vorgenommenen Änderungen.
        ''' </summary>
        Private WithEvents Button As System.Windows.Forms.Button

        ''' <summary>
        ''' Mehrzeiliges Texteingabefeld zur Bearbeitung der Kommentarzeilen.
        ''' </summary>
        Private WithEvents TextBox As System.Windows.Forms.TextBox

        ''' <summary>
        ''' Layoutcontainer zur Anordnung von Textbox und Schaltfläche.
        ''' </summary>
        Private WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel

        ''' <summary>
        ''' Enthält die einzelnen Kommentarzeilen. Jede Array-Position entspricht einer Textzeile.
        ''' </summary>
        Private _Lines As String() = {""}

        ''' <summary>
        ''' Text, der in der GroupBox als Titel angezeigt wird.
        ''' </summary>
        Private _TitelText As String

#End Region

#Region "öffentlichen Ereignisse"

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
        <System.ComponentModel.Description("Wird ausgelöst wenn sich der Kommentartext geändert hat.")>
        Public Event CommentChanged(sender As Object, e As CommentEditEventArgs)

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see cref="CommentEdit"/> und setzt die Ausgangswerte.
        ''' </summary>
        ''' <remarks>
        ''' Ruft <see cref="InitializeComponent"/> auf, übernimmt den aktuellen GroupBox-Titel in <see cref="TitelText"/>
        ''' und deaktiviert den Übernehmen-Button, bis eine Änderung erfolgt.
        ''' </remarks>
        Public Sub New()
            Me.InitializeComponent() ' Dieser Aufruf ist für den Designer erforderlich.
            Me._TitelText = Me.GroupBox.Text ' Den aktuellen GroupBox-Titel als Ausgangswert für `TitelText` übernehmen.
            Me.Button.Enabled = False ' Den Übernehmen-Button deaktivieren, bis eine Änderung erfolgt.
        End Sub

#End Region

#Region "Definition der neuen Eigenschaften"

        ''' <summary>
        ''' Gibt den Text der Titelzeile (GroupBox) zurück oder legt diesen fest.
        ''' </summary>
        ''' <remarks>
        ''' Das Setzen löst intern <see cref="TextChanged"/> aus, wodurch der UI-Text aktualisiert wird.
        ''' </remarks>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt den Text der Titelzeile zurück oder legt diesen fest.")>
        Public Property TitelText As String
            Set(value As String)
                ' Nur reagieren, wenn sich der Wert tatsächlich ändert
                If Me._TitelText <> value Then
                    Me._TitelText = value
                    Me.GroupBox.Text = Me._TitelText
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
        ''' - Das Setzen löst intern <see cref="CommentChanged"/> aus, wodurch die Textbox synchronisiert wird.
        ''' </remarks>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt den Kommentartext zurück oder legt diesen fest.")>
        Public Property Comment As String()
            Get
                Return Me._Lines
            End Get
            Set
                ' SequenceEqual stellt sicher, dass sowohl Anzahl als auch Inhalt der Zeilen übereinstimmen
                If Not Me._Lines.SequenceEqual(Value) Then
                    Me._Lines = Value
                    Me.TextBox.Lines = Me._Lines ' Kommentarzeilen in die Textbox eintragen (ersetzt den gesamten Inhalt)
                    Me.Button.Enabled = False ' Änderungen wurden übernommen -> Button deaktivieren
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gibt den Namen des INI-Abschnitts zurück oder legt diesen fest, für den der Kommentar angezeigt wird.
        ''' </summary>
        ''' <remarks>
        ''' Dieser Name wird zusammen mit den Kommentarzeilen im Ereignis <see cref="CommentChanged"/> übermittelt.
        ''' </remarks>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt den Name des Abschnitts zurück oder legt diesen fest für den der Kommentar angezeigt werden soll.")>
        Public Property SectionName As String

#End Region

#Region "interne Methoden"

        ''' <summary>
        ''' Klick auf den Übernehmen-Button: übernimmt die aktuellen Textbox-Zeilen und meldet die Änderung.
        ''' </summary>
        ''' <param name="sender">Button</param>
        ''' <param name="e">Nicht verwendet</param>
        Private Sub Button_Click(sender As Object, e As System.EventArgs) Handles Button.Click
            Me._Lines = Me.TextBox.Lines ' Geänderten Kommentar aus der Textbox in das interne Array übernehmen
            Me.Button.Enabled = False ' Button deaktivieren, da die Änderungen jetzt übernommen sind
            RaiseEvent CommentChanged(Me, New CommentEditEventArgs(Me.SectionName, Me._Lines)) ' Änderung nach außen signalisieren (inkl. Abschnittsname)
        End Sub

        ''' <summary>
        ''' Text wurde in der Textbox geändert: aktiviert den Übernehmen-Button.
        ''' </summary>
        ''' <param name="sender">Textbox</param>
        ''' <param name="e">Nicht verwendet</param>
        Private Sub TextBox_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox.TextChanged
            Me.Button.Enabled = True ' Aktiviert den Button, um die Änderungen explizit übernehmen zu können
        End Sub

        ''' <summary>
        ''' Initialisiert und konfiguriert alle vom Designer verwalteten Steuerelemente des Controls.
        ''' </summary>
        ''' <remarks>
        ''' Diese Methode wird automatisch vom Konstruktor aufgerufen und sollte nicht manuell geändert werden.
        ''' </remarks>
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.GroupBox = New System.Windows.Forms.GroupBox()
            Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
            Me.TextBox = New System.Windows.Forms.TextBox()
            Me.Button = New System.Windows.Forms.Button()
            Me.GroupBox.SuspendLayout()
            Me.TableLayoutPanel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox
            '
            Me.GroupBox.Controls.Add(Me.TableLayoutPanel1)
            Me.GroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GroupBox.Location = New System.Drawing.Point(0, 0)
            Me.GroupBox.Name = "GroupBox"
            Me.GroupBox.Size = New System.Drawing.Size(165, 97)
            Me.GroupBox.TabIndex = 0
            Me.GroupBox.TabStop = False
            Me.GroupBox.Text = "CommentEdit"
            '
            'TableLayoutPanel1
            '
            Me.TableLayoutPanel1.ColumnCount = 1
            Dim unused = Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel1.Controls.Add(Me.TextBox, 0, 0)
            Me.TableLayoutPanel1.Controls.Add(Me.Button, 0, 1)
            Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 16)
            Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
            Me.TableLayoutPanel1.RowCount = 2
            Dim unused1 = Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Dim unused2 = Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel1.Size = New System.Drawing.Size(159, 78)
            Me.TableLayoutPanel1.TabIndex = 2
            '
            'TextBox
            '
            Me.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.TextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TextBox.Location = New System.Drawing.Point(3, 3)
            Me.TextBox.Multiline = True
            Me.TextBox.Name = "TextBox"
            Me.TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.TextBox.Size = New System.Drawing.Size(153, 41)
            Me.TextBox.TabIndex = 0
            Me.TextBox.WordWrap = False
            '
            'Button
            '
            Me.Button.Anchor = CType(System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
            Me.Button.Location = New System.Drawing.Point(52, 50)
            Me.Button.Name = "Button"
            Me.Button.Size = New System.Drawing.Size(104, 25)
            Me.Button.TabIndex = 1
            Me.Button.Text = "übernehmen"
            Me.Button.UseVisualStyleBackColor = True
            '
            'CommentEdit
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.GroupBox)
            Me.Name = "CommentEdit"
            Me.Size = New System.Drawing.Size(165, 97)
            Me.GroupBox.ResumeLayout(False)
            Me.TableLayoutPanel1.ResumeLayout(False)
            Me.TableLayoutPanel1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

#End Region

#Region "überschriebene Methoden"

        ''' <summary>
        ''' Gibt verwaltete Ressourcen frei und bereinigt die Komponentenliste, sofern vorhanden.
        ''' </summary>
        ''' <param name="disposing">True, wenn verwaltete Ressourcen freigegeben werden sollen; andernfalls False.</param>
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

#End Region

    End Class

End Namespace