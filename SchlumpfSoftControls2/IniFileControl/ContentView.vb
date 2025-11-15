' ContentView.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System.ComponentModel
Imports System.Drawing

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
    <Description("Steuerelement zum Anzeigen des Dateiinhaltes.")> ' Beschreibt das Control im Designer (Eigenschaftenfenster/Toolbox).
    <ToolboxItem(True)> ' Markiert die Klasse als Toolbox-Element.
    <ToolboxBitmap(GetType(IniFileControl.ContentView), "ContentView.bmp")> ' Legt das Symbol in der Toolbox fest.
    Public Class ContentView : Inherits System.Windows.Forms.UserControl

        ' Hält die aktuell gesetzten Textzeilen, die in der TextBox angezeigt werden.
        Private _Lines As String()

        ' Hält den anzuzeigenden Titeltext der GroupBox.
        Private _TitelText As String

        ' Internes Event, das ausgelöst wird, wenn sich die Lines-Eigenschaft ändert.
        ' Die zugehörige Handler-Methode aktualisiert die TextBox.
        Private Event PropLinesChanged()

        ' Internes Event, das ausgelöst wird, wenn sich der Titeltext ändert.
        ' Die zugehörige Handler-Methode aktualisiert die GroupBox.
        Private Event TitelTextChanged()

        ''' <summary>
        ''' Initialisiert eine neue Instanz des Controls und stellt Designer-Komponenten bereit.
        ''' </summary>
        Public Sub New()
            ' Dieser Aufruf initialisiert alle über den Designer erzeugten Steuerelemente (z. B. GroupBox, TextBox).
            Me.InitializeComponent()

            ' Initialer Titeltext wird aus der im Designer gesetzten GroupBox übernommen,
            ' sodass die Eigenschaft <see cref="TitelText"/> konsistent ist.
            Me._TitelText = Me.GroupBox.Text
        End Sub

#Region "Definition der neuen Eigenschaften"

        ''' <summary>
        ''' Gibt den Text der Titelzeile (GroupBox) zurück oder legt diesen fest.
        ''' </summary>
        ''' <value>Text, der in der GroupBox als Titel angezeigt wird.</value>
        ''' <remarks>
        ''' - Das Setzen löst das interne Event <c>TitelTextChanged</c> aus, welches die UI aktualisiert.
        ''' - Die Property ist im Designer sichtbar und unter "Appearance" kategorisiert.
        ''' </remarks>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den Text der Titelzeile zurück oder legt diesen fest.")>
        Public Property TitelText As String
            Set(value As String)
                ' Nur reagieren, wenn sich der Wert tatsächlich geändert hat.
                If Me._TitelText <> value Then
                    Me._TitelText = value
                    ' UI-Aktualisierung über den zugehörigen Handler anstoßen.
                    RaiseEvent TitelTextChanged()
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
        ''' - Das Setzen löst das interne Event <c>PropLinesChanged</c> aus, welches die UI aktualisiert.
        ''' - Der Vergleich <c>IsNot</c> prüft hier Referenzungleichheit des Arrays.
        '''   Wird derselbe Array-Instanzinhalt verändert, ohne das Array-Objekt zu tauschen, wird kein Event ausgelöst.
        ''' </remarks>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den Dateiinhalt zurück oder legt diesen fest.")>
        Public Property Lines As String()
            Get
                Return Me._Lines
            End Get
            Set
                ' Nur reagieren, wenn eine andere Array-Instanz gesetzt wird.
                If Me._Lines IsNot Value Then
                    Me._Lines = Value
                    ' UI-Aktualisierung über den zugehörigen Handler anstoßen.
                    RaiseEvent PropLinesChanged()
                End If
            End Set
        End Property

#End Region

        ''' <summary>
        ''' Reagiert auf Änderungen der <see cref="Lines"/>-Eigenschaft und überträgt die Zeilen in die TextBox.
        ''' </summary>
        ''' <remarks>
        ''' - Die Verkabelung erfolgt über <c>Handles Me.PropLinesChanged</c>.
        ''' - Erwartet, dass <c>TextBox</c> eine im Designer erstellte Instanz ist.
        ''' - Wenn <c>_Lines</c> <c>Nothing</c> ist, wird der TextBox typischerweise ein leeres Array zugewiesen
        '''   oder sie bleibt unverändert (abhängig vom WinForms-Verhalten der .NET-Version).
        ''' </remarks>
        Private Sub IniFileContentView_LinesChanged() Handles Me.PropLinesChanged
            Me.TextBox.Lines = Me._Lines
        End Sub

        ''' <summary>
        ''' Reagiert auf Änderungen der <see cref="TitelText"/>-Eigenschaft und aktualisiert den GroupBox-Titel.
        ''' </summary>
        ''' <remarks>
        ''' - Die Verkabelung erfolgt über <c>Handles Me.TitelTextChanged</c>.
        ''' - Erwartet, dass <c>GroupBox</c> eine im Designer erstellte Instanz ist.
        ''' </remarks>
        Private Sub IniFileCommentEdit_TitelTextChanged() Handles Me.TitelTextChanged
            Me.GroupBox.Text = Me._TitelText
        End Sub


        'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

        'Wird vom Windows Form-Designer benötigt.
        Private components As System.ComponentModel.IContainer

        'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
        'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
        'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
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

        Private WithEvents GroupBox As System.Windows.Forms.GroupBox
        Private WithEvents TextBox As System.Windows.Forms.TextBox




    End Class

End Namespace