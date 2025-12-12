' *************************************************************************************************
' IniFileCommentEdit.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System.Linq

Namespace IniFileControl

    ''' <summary>
    ''' Steuerelement zum Anzeigen und Bearbeiten des Datei- oder Abschnittskommentars
    ''' einer INI-Datei.
    ''' </summary>
    ''' <remarks>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Setzen Sie <see cref="Comment"/> um den anzuzeigenden Kommentar
    ''' zu initialisieren.</description>
    '''  </item>
    '''  <item>
    '''   <description>Änderungen in der TextBox aktivieren den
    ''' Übernehmen-Button.</description>
    '''  </item>
    '''  <item>
    '''   <description>Ein Klick auf den Button übernimmt die Änderungen in <see
    ''' cref="Comment"/> und löst <see cref="CommentChanged"/> aus.</description>
    '''  </item>
    ''' </list>
    ''' </remarks>
    ''' <example>
    ''' <code><![CDATA[' Beispiel: Verwendung im Formular
    ''' Dim editor As New IniFileControl.CommentEdit() With {
    '''     .SectionName = "General",
    '''     .TitelText = "Dateikommentar",
    '''     .Comment = New String() {"Zeile 1", "Zeile 2"}
    ''' }
    ''' AddHandler editor.CommentChanged,
    '''     Sub(sender, args)
    '''         ' Änderungen verarbeiten
    '''         Debug.WriteLine($"Sektion: {args.SectionName}")
    '''         For Each line In args.CommentLines
    '''             Debug.WriteLine(line)
    '''         Next
    '''     End Sub
    ''' ' Editor dem UI hinzufügen:
    ''' Me.Controls.Add(editor)]]></code>
    ''' </example>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Steuerelement zum Anzeigen und Bearbeiten des Datei- oder Abschnitts- Kommentars einer INI - Datei.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(IniFileControl.CommentEdit), "CommentEdit.bmp")>
    Public Class CommentEdit : Inherits System.Windows.Forms.UserControl

#Region "Variablen"

        Private ReadOnly components As System.ComponentModel.IContainer
        Private WithEvents GroupBox As System.Windows.Forms.GroupBox
        Private WithEvents Button As System.Windows.Forms.Button
        Private WithEvents TextBox As System.Windows.Forms.TextBox
        Private WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
        Private _Lines As String() = {""}
        Private _TitelText As String

#End Region

#Region "Ereignisse"

        ''' <summary>
        ''' Wird ausgelöst, wenn sich der Kommentartext geändert hat und per Button
        ''' übernommen wurde.
        ''' </summary>
        ''' <remarks>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Änderungen werden erst nach Klick auf den Übernehmen-Button
        ''' propagiert, nicht bei jeder Texteingabe.</description>
        '''  </item>
        '''  <item>
        '''   <description>Dies ermöglicht eine explizite Bestätigung der Eingaben durch die
        ''' Nutzenden.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Auf Änderungen reagieren
        ''' Dim editor As New IniFileControl.CommentEdit() With {
        '''     .SectionName = "General",
        '''     .TitelText = "Abschnittskommentar",
        '''     .Comment = New String() {"Startwert Zeile 1", "Startwert Zeile 2"}
        ''' }
        ''' AddHandler editor.CommentChanged,
        '''     Sub(s, args)
        '''         Debug.WriteLine("Kommentar übernommen für Sektion: " & args.SectionName)
        '''         For Each line In args.CommentLines
        '''             Debug.WriteLine("-> " & line)
        '''         Next
        '''     End Sub
        ''' Me.Controls.Add(editor)]]></code>
        ''' </example>
        <System.ComponentModel.Description("Wird ausgelöst wenn sich der Kommentartext geändert hat.")>
        Public Event CommentChanged(sender As Object, e As CommentEditEventArgs)

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Gibt den Text der Titelzeile (GroupBox) zurück oder legt diesen fest.
        ''' </summary>
        ''' <remarks>
        ''' Beim Setzen wird der UI-Text der GroupBox aktualisiert.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Titel ändern
        ''' Dim editor As New IniFileControl.CommentEdit()
        ''' editor.TitelText = "Dateikommentar"
        ''' Me.Controls.Add(editor)]]></code>
        ''' </example>
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
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Jede Array-Position entspricht einer Zeile in der
        ''' TextBox.</description>
        '''  </item>
        '''  <item>
        '''   <description>Änderungserkennung erfolgt per <see
        ''' cref="Enumerable.SequenceEqual(Of TSource)"/>. </description>
        '''  </item>
        '''  <item>
        '''   <description>Beim Setzen werden die TextBox-Zeilen synchronisiert und der
        ''' Übernehmen-Button deaktiviert.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Kommentarzeilen setzen
        ''' Dim editor As New IniFileControl.CommentEdit()
        ''' editor.Comment = New String() {
        '''     "Dies ist Zeile 1",
        '''     "Dies ist Zeile 2",
        '''     "Dies ist Zeile 3"
        ''' }
        ''' Me.Controls.Add(editor)]]></code>
        ''' </example>
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
        ''' Gibt den Namen des INI-Abschnitts zurück oder legt diesen fest, für den der
        ''' Kommentar angezeigt wird.
        ''' </summary>
        ''' <remarks>
        ''' Dieser Name wird zusammen mit den Kommentarzeilen im Ereignis <see
        ''' cref="CommentChanged"/> übermittelt.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Abschnittsname setzen
        ''' Dim editor As New IniFileControl.CommentEdit()
        ''' editor.SectionName = "Network"
        ''' editor.Comment = New String() {"Timeout=30", "Retries=3"}
        ''' Me.Controls.Add(editor)]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt den Name des Abschnitts zurück oder legt diesen fest für den der Kommentar angezeigt werden soll.")>
        Public Property SectionName As String

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see cref="CommentEdit"/> und setzt die
        ''' Ausgangswerte.
        ''' </summary>
        ''' <remarks>
        ''' Ruft <see cref="InitializeComponent"/> auf, übernimmt den aktuellen
        ''' GroupBox-Titel in <see cref="TitelText"/> und deaktiviert den Übernehmen-Button,
        ''' bis eine Änderung erfolgt.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Instanz erzeugen und anzeigen
        ''' Dim editor As New IniFileControl.CommentEdit()
        ''' editor.TitelText = "Dateikommentar"
        ''' editor.SectionName = "General"
        ''' editor.Comment = New String() {"Initiale Zeile"}
        ''' Me.Controls.Add(editor)]]></code>
        ''' </example>
        Public Sub New()
            Me.InitializeComponent() ' Dieser Aufruf ist für den Designer erforderlich.
            Me._TitelText = Me.GroupBox.Text ' Den aktuellen GroupBox-Titel als Ausgangswert für `TitelText` übernehmen.
            Me.Button.Enabled = False ' Den Übernehmen-Button deaktivieren, bis eine Änderung erfolgt.
        End Sub

        ''' <summary>
        ''' Gibt verwaltete Ressourcen frei und bereinigt die Komponentenliste, sofern
        ''' vorhanden.
        ''' </summary>
        ''' <remarks>
        ''' Bei <c>disposing = True</c> werden verwaltete Komponenten über <c>components.Dispose()</c> bereinigt.
        ''' </remarks>
        ''' <param name="disposing">True, wenn verwaltete Ressourcen freigegeben werden
        ''' sollen; andernfalls False.</param>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Ordnungsgemäßes Freigeben
        ''' Using editor As New IniFileControl.CommentEdit()
        '''     editor.SectionName = "General"
        '''     editor.Comment = New String() {"Temp"}
        '''     Me.Controls.Add(editor)
        ''' End Using ' Dispose wird automatisch aufgerufen]]></code>
        ''' </example>
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

#Region "interne Methoden"

        Private Sub Button_Click(sender As Object, e As System.EventArgs) Handles Button.Click
            Me._Lines = Me.TextBox.Lines ' Geänderten Kommentar aus der Textbox in das interne Array übernehmen
            Me.Button.Enabled = False ' Button deaktivieren, da die Änderungen jetzt übernommen sind
            RaiseEvent CommentChanged(Me, New CommentEditEventArgs(Me.SectionName, Me._Lines)) ' Änderung nach außen signalisieren (inkl. Abschnittsname)
        End Sub

        Private Sub TextBox_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox.TextChanged
            Me.Button.Enabled = True ' Aktiviert den Button, um die Änderungen explizit übernehmen zu können
        End Sub

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

    End Class

End Namespace