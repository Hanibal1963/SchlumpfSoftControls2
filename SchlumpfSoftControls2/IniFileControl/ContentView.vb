' ContentView.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace IniFileControl

    ''' <summary>
    ''' Steuerelement zum Anzeigen des Dateiinhalts (z. B. einer INI-Datei) in einer TextBox innerhalb einer GroupBox.
    ''' </summary>
    ''' <remarks>
    ''' - Das Control stellt zwei öffentliche Eigenschaften bereit:
    '''   - <see cref="TitelText"/> für den Text der GroupBox (Titelzeile).
    '''   - <see cref="Lines"/> für den darzustellenden Textinhalt als String-Array.
    ''' - Änderungen an diesen Eigenschaften lösen interne Events aus, die über Handler die UI aktualisieren.
    ''' - Das konkrete Layout (z. B. Instanzen von <c>GroupBox</c> und <c>TextBox</c>) wird im Designer erzeugt
    '''   und in <c>InitializeComponent()</c> initialisiert.
    ''' </remarks>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)> ' Sorgt dafür, dass das Control in einer angegebenen Toolbox-Kategorie erscheint.
    <System.ComponentModel.Description("Steuerelement zum Anzeigen des Dateiinhaltes.")> ' Beschreibt das Control im Designer (Eigenschaftenfenster/Toolbox).
    <System.ComponentModel.ToolboxItem(True)> ' Markiert die Klasse als Toolbox-Element.
    <System.Drawing.ToolboxBitmap(GetType(IniFileControl.ContentView), "ContentView.bmp")> ' Legt das Symbol in der Toolbox fest.
    Public Class ContentView : Inherits System.Windows.Forms.UserControl

#Region "Variablendefinition"

        ''' <summary>
        ''' Umschließende GroupBox, in der die TextBox angezeigt wird und deren Titelzeile über <see cref="TitelText"/> gesteuert wird.
        ''' </summary>
        Private WithEvents GroupBox As System.Windows.Forms.GroupBox

        ''' <summary>
        ''' ReadOnly-TextBox, in der die Zeilen von <see cref="Lines"/> dargestellt werden.
        ''' </summary>
        Private WithEvents TextBox As System.Windows.Forms.TextBox

        ''' <summary>
        ''' Vom Windows Forms Designer verwaltete Komponentenliste.
        ''' </summary>
        Private ReadOnly components As System.ComponentModel.IContainer

        ''' <summary>
        ''' Hält die aktuell gesetzten Textzeilen, die in der TextBox angezeigt werden.
        ''' Jede Array-Position entspricht einer Zeile.
        ''' </summary>
        Private _Lines As String()

        ''' <summary>
        ''' Hält den anzuzeigenden Titeltext der GroupBox.
        ''' </summary>
        Private _TitelText As String

#End Region

#Region "Eventdefinition"

        ''' <summary>
        ''' Internes Ereignis das ausgelöst wird, wenn sich <see cref="Lines"/> verändert hat und der Inhalt der TextBox aktualisiert werden soll.
        ''' </summary>
        Private Event PropertyLinesChanged()

        ''' <summary>
        ''' Internes Ereignis das ausgelöst wird, wenn sich <see cref="TitelText"/> verändert hat und die GroupBox ihren Titel aktualisieren soll.
        ''' </summary>
        Private Event PropertyTitelTextChanged()

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz des Controls und stellt Designer-Komponenten bereit.
        ''' </summary>
        ''' <remarks>
        ''' Ruft <see cref="InitializeComponent"/> auf und übernimmt den initialen Text der GroupBox in <see cref="TitelText"/>.
        ''' </remarks>
        Public Sub New()
            Me.InitializeComponent() ' Wird vom Designer benötigt.
            Me._TitelText = Me.GroupBox.Text ' Initialer Titeltext sichern.
        End Sub

#End Region

#Region "Definition der neuen Eigenschaften"

        ''' <summary>
        ''' Gibt den Text der Titelzeile (GroupBox) zurück oder legt diesen fest.
        ''' </summary>
        ''' <value>Text, der in der GroupBox als Titel angezeigt wird.</value>
        ''' <remarks>
        ''' - Das Setzen löst das interne Event <c>PropertyTitelTextChanged</c> aus, welches die UI aktualisiert.
        ''' - Die Property ist im Designer sichtbar und unter "Appearance" kategorisiert.
        ''' </remarks>
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
        ''' Gibt den Dateiinhalt als Array von Zeilen zurück oder legt diesen fest.
        ''' </summary>
        ''' <value>Ein String-Array, dessen Elemente als Zeilen in der TextBox angezeigt werden.</value>
        ''' <remarks>
        ''' - Das Setzen löst das interne Event <c>PropertyLinesChanged</c> aus, welches die UI aktualisiert.
        ''' - Der Vergleich <c>IsNot</c> prüft hier Referenzungleichheit des Arrays.
        '''   Wird derselbe Array-Instanzinhalt verändert, ohne das Array-Objekt zu tauschen, wird kein Event ausgelöst.
        ''' </remarks>
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

#Region "interne Methoden"

        ''' <summary>
        ''' Reagiert auf Änderungen der <see cref="Lines"/>-Eigenschaft und überträgt die Zeilen in die TextBox.
        ''' </summary>
        ''' <remarks>
        ''' Erfolgt durch Zuweisung an <c>TextBox.Lines</c>. Null wird unverändert durchgereicht.
        ''' </remarks>
        Private Sub ContentView_LinesChanged() Handles Me.PropertyLinesChanged
            Me.TextBox.Lines = Me._Lines
        End Sub

        ''' <summary>
        ''' Reagiert auf Änderungen der <see cref="TitelText"/>-Eigenschaft und aktualisiert den GroupBox-Titel.
        ''' </summary>
        ''' <remarks>
        ''' Setzt <c>GroupBox.Text</c> auf den neuen Titel.
        ''' </remarks>
        Private Sub ContentView_TitelTextChanged() Handles Me.PropertyTitelTextChanged
            Me.GroupBox.Text = Me._TitelText
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