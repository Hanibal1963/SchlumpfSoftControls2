' ContentView.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace IniFileControl

    ''' <summary>
    ''' Steuerelement zur read-only Anzeige eines Dateiinhalts (z. B. INI-Datei, Log,
    ''' Text) in einer mehrzeiligen <see cref="System.Windows.Forms.TextBox"/> innerhalb
    ''' einer <see cref="System.Windows.Forms.GroupBox"/>.
    ''' </summary>
    ''' <remarks>
    ''' <para><b>Funktion:</b></para>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Stellt die Eigenschaften <see cref="TitelText"/> (Titel der
    ''' GroupBox) und <see cref="Lines"/> (Zeileninhalt) bereit.</description>
    '''  </item>
    '''  <item>
    '''   <description>Änderungen an diesen Eigenschaften lösen interne Ereignisse aus,
    ''' die die UI synchron aktualisieren.</description>
    '''  </item>
    '''  <item>
    '''   <description>Layout und untergeordnete Steuerelemente (<c>GroupBox</c>, <c>TextBox</c>) werden im Designer erstellt und in <c>InitializeComponent()</c> initialisiert. </description>
    '''  </item>
    ''' </list>
    ''' <para><b>Einschränkungen:</b></para>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Die TextBox ist schreibgeschützt (<c>ReadOnly = True</c>) und dient ausschließlich der Anzeige.</description>
    '''  </item>
    '''  <item>
    '''   <description>Zeilenumbrüche werden aus <see cref="Lines"/> übernommen; <c>WordWrap</c> ist deaktiviert, Scrollbars sind aktiv.</description>
    '''  </item>
    ''' </list>
    ''' </remarks>
    ''' <example>
    ''' <code language="vb"><![CDATA[' Beispiel: Einbindung in ein WinForms-Formular und Anzeige von Textzeilen
    ''' Public Class MainForm
    '''     Inherits Form
    '''  
    '''     Private ReadOnly _view As New IniFileControl.ContentView()
    '''  
    '''     Public Sub New()
    '''         Me.Text = "Demo"
    '''         Me.ClientSize = New Drawing.Size(400, 300)
    '''         _view.Dock = DockStyle.Fill
    '''         _view.TitelText = "Dateiinhalt"
    '''         _view.Lines = New String() {
    '''             "; Kommentarzeile",
    '''             "[Section]",
    '''             "Key=Value"
    '''         }
    '''         Me.Controls.Add(_view)
    '''     End Sub
    ''' End Class]]></code>
    ''' </example>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)> ' Sorgt dafür, dass das Control in einer angegebenen Toolbox-Kategorie erscheint.
    <System.ComponentModel.Description("Steuerelement zum Anzeigen des Dateiinhaltes.")> ' Beschreibt das Control im Designer (Eigenschaftenfenster/Toolbox).
    <System.ComponentModel.ToolboxItem(True)> ' Markiert die Klasse als Toolbox-Element.
    <System.Drawing.ToolboxBitmap(GetType(IniFileControl.ContentView), "ContentView.bmp")> ' Legt das Symbol in der Toolbox fest.
    Public Class ContentView : Inherits System.Windows.Forms.UserControl

#Region "Variablen"

        Private WithEvents GroupBox As System.Windows.Forms.GroupBox
        Private WithEvents TextBox As System.Windows.Forms.TextBox
        Private ReadOnly components As System.ComponentModel.IContainer
        Private _Lines As String()
        Private _TitelText As String

#End Region

#Region "Ereignisse"

        Private Event PropertyLinesChanged()
        Private Event PropertyTitelTextChanged()

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Titeltext der <see cref="System.Windows.Forms.GroupBox"/>.
        ''' </summary>
        ''' <remarks>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Beim Setzen wird <c>PropertyTitelTextChanged</c> ausgelöst und der UI-Titel aktualisiert.</description>
        '''  </item>
        '''  <item>
        '''   <description>Die Property ist im Designer sichtbar und der Kategorie
        ''' "Appearance" zugeordnet.</description>
        '''  </item>
        '''  <item>
        '''   <description>Standardwert wird im Konstruktor aus <c>GroupBox.Text</c> übernommen.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        ''' <value>
        ''' Text, der in der GroupBox als Überschrift angezeigt wird.
        ''' </value>
        ''' <example>
        ''' <code language="vb"><![CDATA[Dim view As New IniFileControl.ContentView()
        ''' view.TitelText = "Konfiguration"
        ''' ' Der Text der GroupBox wird sofort aktualisiert.]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt den Text der Titelzeile zurück oder legt diesen fest.")>
        Public Property TitelText As String
            Set(value As String)
                If Me._TitelText <> value Then
                    Me._TitelText = value
                    RaiseEvent PropertyTitelTextChanged()
                End If
            End Set
            Get
                Return Me._TitelText
            End Get
        End Property

        ''' <summary>
        ''' Anzeigeninhalt als Array von Zeilen für die read-only <see
        ''' cref="System.Windows.Forms.TextBox"/>.
        ''' </summary>
        ''' <remarks>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Beim Setzen wird <c>PropertyLinesChanged</c> ausgelöst und der TextBox-Inhalt mit <c>TextBox.Lines</c> aktualisiert.</description>
        '''  </item>
        '''  <item>
        '''   <description>Der Vergleich <c>IsNot</c> prüft Referenzungleichheit des Arrays.</description>
        '''  </item>
        '''  <item>
        '''   <description>Wird dieselbe Arrayinstanz intern verändert, ohne das Arrayobjekt
        ''' zu ersetzen, erfolgt keine Aktualisierung.</description>
        '''  </item>
        '''  <item>
        '''   <description>Für große Inhalte empfiehlt sich die Zuweisung eines neuen
        ''' Arrays, um das Aktualisierungsereignis sicher auszulösen.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        ''' <value>
        ''' Ein String-Array, dessen Elemente als einzelne Zeilen dargestellt werden; <c>Nothing</c> zeigt keinen Inhalt.
        ''' </value>
        ''' <example>
        ''' <code language="vb"><![CDATA[
        ''' Dim view As New IniFileControl.ContentView()
        ''' view.Lines = IO.File.ReadAllLines("C:\Temp\settings.ini")
        ''' ' Die TextBox zeigt die gelesenen Zeilen an; Scrollbars sind aktiv, Umbruch ist aus.]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt den Dateiinhalt zurück oder legt diesen fest.")>
        Public Property Lines As String()
            Get
                Return Me._Lines
            End Get
            Set
                If Me._Lines IsNot Value Then
                    Me._Lines = Value
                    RaiseEvent PropertyLinesChanged()
                End If
            End Set
        End Property

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz des Controls, erstellt die Designer-Komponenten
        ''' und übernimmt den anfänglichen Titel der <see
        ''' cref="System.Windows.Forms.GroupBox"/>.
        ''' </summary>
        ''' <remarks>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Ruft <see cref="InitializeComponent"/> auf, um die vom Designer
        ''' verwalteten Untersteuerelemente zu erzeugen.</description>
        '''  </item>
        '''  <item>
        '''   <description>Setzt den internen Standardwert für <see cref="TitelText"/> anhand von <c>GroupBox.Text</c>.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        ''' <example>
        ''' <code language="vb"><![CDATA[' Beispiel: Erzeugen und Konfigurieren des Controls zur Anzeige von INI-Zeilen
        ''' Dim view As New IniFileControl.ContentView()
        ''' view.Dock = DockStyle.Fill
        ''' view.TitelText = "INI-Datei"
        ''' view.Lines = New String() {
        '''     "; Kommentar",
        '''     "[Allgemein]",
        '''     "Sprache=de-DE"
        ''' }
        '''  
        ''' ' In ein Formular einfügen:
        ''' Dim form As New Form() With { .Text = "Demo", .ClientSize = New Drawing.Size(400, 300) }
        ''' form.Controls.Add(view)
        ''' form.Show()]]></code>
        ''' </example>
        Public Sub New()
            Me.InitializeComponent() ' Wird vom Designer benötigt.
            Me._TitelText = Me.GroupBox.Text ' Initialer Titeltext sichern.
        End Sub

        ''' <summary>
        ''' Gibt verwaltete Ressourcen frei und bereinigt die Komponentenliste, sofern
        ''' vorhanden.
        ''' </summary>
        ''' <remarks>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Bei <c>disposing = True</c> wird die vom Designer erzeugte <c>components</c>-Containerinstanz disponiert.</description>
        '''  </item>
        '''  <item>
        '''   <description>Ruft stets die Basisklasse (<see
        ''' cref="System.Windows.Forms.UserControl.Dispose(Boolean)"/>) auf.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        ''' <param name="disposing">True, wenn verwaltete (managed) Ressourcen freigegeben
        ''' werden sollen; andernfalls False.</param>
        ''' <example>
        ''' <code language="vb"><![CDATA[' Beispiel: Explizites Freigeben eines ContentView-Controls
        ''' Dim view As New IniFileControl.ContentView()
        ''' view.TitelText = "Logdatei"
        ''' view.Lines = IO.File.ReadAllLines("C:\Temp\app.log")
        '''  
        ''' ' Nutzung ...
        '''  
        ''' ' Danach explizit freigeben:
        ''' view.Dispose() ' Ruft Dispose(True) und gibt Komponenten frei.
        '''  
        ''' ' Alternative: Lebensdauer über Formular verwalten:
        ''' Using form As New Form()
        '''     form.Controls.Add(view)
        '''     form.Show()
        '''     ' Beim Dispose des Formulars werden enthaltene Controls ebenfalls disponiert.
        ''' End Using]]></code>
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

        Private Sub ContentView_LinesChanged() Handles Me.PropertyLinesChanged
            Me.TextBox.Lines = Me._Lines
        End Sub

        Private Sub ContentView_TitelTextChanged() Handles Me.PropertyTitelTextChanged
            Me.GroupBox.Text = Me._TitelText
        End Sub

        ' Initialisiert und konfiguriert alle vom Designer verwalteten Steuerelemente des Controls.
        ' Diese Methode wird automatisch vom Konstruktor aufgerufen und sollte nicht manuell geändert werden.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.GroupBox = New System.Windows.Forms.GroupBox()
            Me.TextBox = New System.Windows.Forms.TextBox()
            Me.GroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox
            '
            Me.GroupBox.Controls.Add(Me.TextBox)
            Me.GroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GroupBox.Location = New System.Drawing.Point(0, 0)
            Me.GroupBox.Name = "GroupBox"
            Me.GroupBox.Size = New System.Drawing.Size(164, 95)
            Me.GroupBox.TabIndex = 0
            Me.GroupBox.TabStop = False
            Me.GroupBox.Text = "ContentView"
            '
            'TextBox
            '
            Me.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.TextBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TextBox.Location = New System.Drawing.Point(3, 16)
            Me.TextBox.Multiline = True
            Me.TextBox.Name = "TextBox"
            Me.TextBox.ReadOnly = True
            Me.TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.TextBox.Size = New System.Drawing.Size(158, 76)
            Me.TextBox.TabIndex = 0
            Me.TextBox.WordWrap = False
            '
            'ContentView
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.GroupBox)
            Me.Name = "ContentView"
            Me.Size = New System.Drawing.Size(164, 95)
            Me.GroupBox.ResumeLayout(False)
            Me.GroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

#End Region

    End Class

End Namespace