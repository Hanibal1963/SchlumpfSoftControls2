' *************************************************************************************************
' TransparentLabel.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace TransparentLabelControl

    ''' <summary>
    ''' Ein Steuerelement zum Anzeigen eines Textes mit durchscheinendem Hintergrund.
    ''' </summary>
    ''' <remarks>
    ''' Dieses Label nutzt den erweiterten Fensterstil <c>WS_EX_TRANSPARENT</c>, um den Hintergrund transparent erscheinen zu lassen.
    ''' </remarks>
    ''' <example>
    ''' <code><![CDATA[' Verwendung von TransparentLabel in einem Formular
    ''' Public Class MainForm
    '''     Inherits Form
    '''  
    '''     Private ReadOnly _lbl As New TransparentLabelControl.TransparentLabel With {
    '''         .Text = "Durchscheinender Text",
    '''         .AutoSize = True,
    '''         .ForeColor = Color.DarkBlue,
    '''         .Location = New Point(20, 20)
    '''     }
    '''  
    '''     Public Sub New()
    '''         Me.Text = "Demo TransparentLabel"
    '''         Me.ClientSize = New Size(400, 200)
    '''  
    '''         ' Hintergrund eines übergeordneten Controls sichtbar machen
    '''         Dim panel As New Panel With {
    '''             .Dock = DockStyle.Fill,
    '''             .BackgroundImage = Image.FromFile("background.jpg"),
    '''             .BackgroundImageLayout = ImageLayout.Stretch
    '''         }
    '''  
    '''         panel.Controls.Add(_lbl)
    '''         Me.Controls.Add(panel)
    '''     End Sub
    ''' End Class]]></code>
    ''' </example>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Ein Steuerelement zum Anzeigen eines Textes mit durchscheinendem Hintergrund.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(TransparentLabelControl.TransparentLabel), "TransparentLabel.bmp")>
    Public Class TransparentLabel : Inherits System.Windows.Forms.Label

#Region "Variablendefinition"

        Private components As System.ComponentModel.IContainer ' Container für verwaltete Komponenten dieses Steuerelements.

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property BackColor As System.Drawing.Color
            Get
                Return MyBase.BackColor
            End Get
            Set(value As System.Drawing.Color)
                MyBase.BackColor = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImage As System.Drawing.Image
            Get
                Return MyBase.BackgroundImage
            End Get
            Set(value As System.Drawing.Image)
                MyBase.BackgroundImage = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImageLayout As System.Windows.Forms.ImageLayout
            Get
                Return MyBase.BackgroundImageLayout
            End Get
            Set(value As System.Windows.Forms.ImageLayout)
                MyBase.BackgroundImageLayout = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overloads Property FlatStyle As System.Windows.Forms.FlatStyle
            Get
                Return MyBase.FlatStyle
            End Get
            Set(value As System.Windows.Forms.FlatStyle)
                MyBase.FlatStyle = value
            End Set
        End Property

        ''' <summary>
        ''' Gibt die Erstellungsparameter für das Steuerelement zurück und aktiviert die
        ''' Transparenz.
        ''' </summary>
        ''' <remarks>
        ''' <para>Das Setzen von <c>WS_EX_TRANSPARENT</c> sorgt dafür, dass der Hintergrund des Eltern-Steuerelements durchscheint.</para>
        ''' <para><b>Weitere Infos unter:</b><br/>
        ''' <see
        ''' href="https://stackoverflow.com/questions/511320/transparent-control-backgrounds-on-a-vb-net-gradient-filled-form"/><br/>
        ''' und<br/>
        ''' <see
        ''' href="https://learn.microsoft.com/de-de/windows/win32/winmsg/extended-window-styles"/></para>
        ''' </remarks>
        ''' <value>
        ''' Die angepassten <see cref="CreateParams"/> mit aktiviertem
        ''' WS_EX_TRANSPARENT-Stil.
        ''' </value>
        ''' <example>
        ''' <code><![CDATA[' CreateParams ist schreibgeschützt; Beispiel zeigt, wie die Transparenz wirkt:
        ''' Dim form As New Form() With {.ClientSize = New Size(300, 150)}
        ''' Dim panel As New Panel() With {
        '''     .Dock = DockStyle.Fill,
        '''     .BackgroundImage = Image.FromFile("background.jpg"),
        '''     .BackgroundImageLayout = ImageLayout.Stretch
        ''' }
        '''  
        ''' Dim lbl As New TransparentLabelControl.TransparentLabel() With {
        '''     .Text = "Transparent",
        '''     .AutoSize = True,
        '''     .ForeColor = Color.White,
        '''     .Location = New Point(10, 10)
        ''' }
        '''  
        ''' panel.Controls.Add(lbl)
        ''' form.Controls.Add(panel)
        ''' form.Show()]]></code>
        ''' </example>
        Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
            Get
                Dim cp As System.Windows.Forms.CreateParams = MyBase.CreateParams
                ' WS EX TRANSPARENT aktivieren (https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles)
                cp.ExStyle = cp.ExStyle Or &H20
                Return cp
            End Get
        End Property

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz der <see cref="TransparentLabel"/>-Klasse.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[' Instanziierung und Grundkonfiguration
        ''' Dim lbl As New TransparentLabelControl.TransparentLabel() With {
        '''     .Text = "Hallo Welt",
        '''     .AutoSize = True,
        '''     .ForeColor = Color.Black
        ''' }
        '''  
        ''' ' In ein Formular oder Panel einfügen
        ''' Me.Controls.Add(lbl)]]></code>
        ''' </example>
        Public Sub New()
            'Dieser Aufruf ist für den Designer erforderlich.
            Me.InitializeComponent()
            'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            Me.InitializeStyles()
        End Sub

        ''' <summary>
        ''' Bereinigt die von der <see cref="TransparentLabel"/> verwendeten Ressourcen.
        ''' </summary>
        ''' <remarks>
        ''' Bei <c>disposing = True</c> werden verwaltete Ressourcen freigegeben. Nicht verwaltete Ressourcen werden immer freigegeben.
        ''' </remarks>
        ''' <param name="disposing">Gibt an, ob verwaltete Ressourcen freigegeben werden
        ''' sollen.</param>
        ''' <example>
        ''' <code><![CDATA[' Ordnungsgemäße Entsorgung (IDisposable-Pattern beachten)
        ''' Using lbl As New TransparentLabelControl.TransparentLabel()
        '''     lbl.Text = "Temporäres Label"
        '''     ' ... Verwendung ...
        ''' End Using ' Dispose wird automatisch aufgerufen]]></code>
        ''' </example>
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso Me.components IsNot Nothing Then
                    Me.components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

#End Region

#Region "interne Methoden"

        ' Gibt die Erstellungsparameter für das Steuerelement zurück und aktiviert die Transparenz.
        Private Sub InitializeStyles()
            Me.SetStyle(System.Windows.Forms.ControlStyles.Opaque, True)
            Me.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, True)
            Me.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, False)
        End Sub

        ' Initialisiert die Komponenten des Steuerelements.
        ' Die folgende Prozedur ist für den Komponenten-Designer erforderlich.
        ' Sie kann mit dem Komponenten-Designer geändert werden.
        ' Das Bearbeiten mit dem Code-Editor ist nicht möglich.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
        End Sub

#End Region

    End Class

End Namespace