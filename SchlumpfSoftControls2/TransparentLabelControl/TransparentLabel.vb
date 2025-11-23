' *************************************************************************************************
' TransparentLabel.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace TransparentLabelControl

    ''' <summary>
    ''' Ein Steuerelement zum Anzeigen eines Textes mit durchscheinendem Hintergrund.
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Ein Steuerelement zum Anzeigen eines Textes mit durchscheinendem Hintergrund.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(TransparentLabelControl.TransparentLabel), "TransparentLabel.bmp")>
    Public Class TransparentLabel : Inherits System.Windows.Forms.Label

#Region "Variablendefinition"

        ''' <summary>
        ''' Container für verwaltete Komponenten dieses Steuerelements.
        ''' </summary>
        Private components As System.ComponentModel.IContainer

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
        ''' Gibt die Erstellungsparameter für das Steuerelement zurück und aktiviert die Transparenz.
        ''' </summary>
        ''' <returns>Die angepassten <see cref="CreateParams"/> mit aktiviertem WS_EX_TRANSPARENT-Stil.</returns>
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
        Public Sub New()
            'Dieser Aufruf ist für den Designer erforderlich.
            Me.InitializeComponent()
            'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            Me.InitializeStyles()
        End Sub

#End Region

#Region "interne Methoden"

        ''' <summary>
        ''' Gibt die Erstellungsparameter für das Steuerelement zurück und aktiviert die Transparenz.
        ''' </summary>
        Private Sub InitializeStyles()
            Me.SetStyle(System.Windows.Forms.ControlStyles.Opaque, True)
            Me.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, True)
            Me.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, False)
        End Sub

        ''' <summary>
        ''' Initialisiert die Komponenten des Steuerelements.
        ''' </summary>
        ''' <remarks>
        ''' <para>Die folgende Prozedur ist für den Komponenten-Designer
        ''' erforderlich.</para>
        ''' <para>Sie kann mit dem Komponenten-Designer geändert werden.</para>
        ''' <para>Das Bearbeiten mit dem Code-Editor ist nicht möglich.</para>
        ''' </remarks>
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
        End Sub

#End Region

#Region "überschriebene Methoden"

        ''' <summary>
        ''' Bereinigt die von der <see cref="TransparentLabel"/> verwendeten Ressourcen.
        ''' </summary>
        ''' <param name="disposing">Gibt an, ob verwaltete Ressourcen freigegeben werden sollen.</param>
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

    End Class

End Namespace