' *************************************************************************************************
' ColorProgressBar.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ColorProgressBarControl

    ''' <summary>
    ''' Control zum Anzeigen eines farbigen Fortschrittbalkens.
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Control zum Anzeigen eines farbigen Fortschrittbalkens.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(ColorProgressBarControl.ColorProgressBar), "ColorProgressBar.bmp")>
    Public Class ColorProgressBar

        Inherits System.Windows.Forms.UserControl

#Region "Ereignisdefinitionen für öffentliche Ereignisse"

        Public Shadows Event Click(sender As Object, e As System.EventArgs)

#End Region

#Region "neue Eigenschaften"

        ''' <summary>
        ''' Gibt den Gesamtfortschritt des Fortschrittsbalkens zurück oder legt diesen fest.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Gibt den Gesamtfortschritt des Fortschrittsbalkens zurück oder legt diesen fest.")>
        <System.ComponentModel.DefaultValue(1)>
        Public Property Value() As Integer
            Get
                Return _ProgressValue
            End Get
            Set(value As Integer)
                'Nicht mehr als den Maximalwert erlauben
                _ProgressValue = If(value <= _MaxValue, value, _MaxValue)
                Dim unused = Me.UpdateProgress()
            End Set
        End Property

        ''' <summary>
        ''' Gibt den Maximalwert des Fortschrittsbalkens zurück oder legt diesen fest.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Gibt den Maximalwert des Fortschrittsbalkens zurück oder legt diesen fest.")>
        <System.ComponentModel.DefaultValue(10)>
        Public Property ProgressMaximumValue() As Integer
            Get
                Return _MaxValue
            End Get
            Set(value As Integer)
                _MaxValue = If(value > Width, Width, value)
                Dim unused = UpdateProgress()
            End Set
        End Property

        ''' <summary>
        ''' Gibt die Farbe des Fortschrittsbalkens zurück oder legt diese fest.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt die Farbe des Fortschrittsbalkens zurück oder legt diese fest.")>
        Public Property BarColor As System.Drawing.Color
            Get
                Return _BarColor
            End Get
            Set(value As System.Drawing.Color)
                _BarColor = value
                Me.ProgressFull.BackColor = _BarColor
            End Set
        End Property

        ''' <summary>
        ''' Gibt die Farbe des leeren Fortschrittsbalkens zurück oder legt diese fest.
        ''' </summary>
        ''' <returns></returns>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt die Farbe des leeren Fortschrittsbalkens zurück oder legt diese fest.")>
        Public Property EmptyColor As System.Drawing.Color
            Get
                Return _EmptyColor
            End Get
            Set(value As System.Drawing.Color)
                _EmptyColor = value
                ProgressEmpty.BackColor = _EmptyColor
            End Set
        End Property

        ''' <summary>
        ''' Gibt die Farbe des Rahmens zurück oder legt diese fest.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt die Farbe des Rahmens zurück oder legt diese fest.")>
        Public Property BorderColor As System.Drawing.Color
            Get
                Return _BorderColor
            End Get
            Set(value As System.Drawing.Color)
                _BorderColor = value
                BackColor = _BorderColor
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob der Rahmen auf der Fortschrittsanzeige aktiviert ist.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt an, ob der Rahmen auf der Fortschrittsanzeige aktiviert ist.")>
        <System.ComponentModel.DefaultValue(True)>
        Public Property ShowBorder As Boolean
            Get
                Return _ShowBorder
            End Get
            Set(value As Boolean)
                _ShowBorder = value
                Dim unused = UpdateProgress()
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob der Glanz auf der Fortschrittsleiste angezeigt wird.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt an, ob der Glanz auf der Fortschrittsleiste angezeigt wird.")>
        <System.ComponentModel.DefaultValue(True)>
        Public Property IsGlossy As Boolean
            Get
                Return _IsGlossy
            End Get
            Set(value As Boolean)
                _IsGlossy = value
                Dim unused = UpdateProgress()
            End Set
        End Property

#End Region

#Region "ausgeblendete Eigenschaften"

        ''' <summary>
        ''' Ausgeblendet da nicht relevant.
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
        ''' Ausgeblendet da nicht relevant.
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
        ''' Ausgeblendet da nicht relevant.
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
        ''' Ausgeblendet da nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Shadows Property BorderStyle As System.Windows.Forms.BorderStyle
            Get
                Return MyBase.BorderStyle
            End Get
            Set(value As System.Windows.Forms.BorderStyle)
                MyBase.BorderStyle = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property ForeColor As System.Drawing.Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As System.Drawing.Color)
                MyBase.ForeColor = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overloads Property Padding As System.Windows.Forms.Padding
            Get
                Return MyBase.Padding
            End Get
            Set(value As System.Windows.Forms.Padding)
                MyBase.Padding = value
            End Set
        End Property

#End Region

        Public Sub New()
            'Dieser Aufruf ist für den Designer erforderlich.
            Me.InitializeComponent()
            'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            ' Standardwerte setzen
            GlossLeft.BackColor = System.Drawing.Color.FromArgb(100, 255, 255, 255)
            GlossRight.BackColor = System.Drawing.Color.FromArgb(100, 255, 255, 255)
            BackColor = _BorderColor
            ProgressEmpty.BackColor = _EmptyColor
            ProgressFull.BackColor = _BarColor
        End Sub

#Region "interne Funktionen"

        Private Function UpdateGloss() As Boolean
            Try
                'Jedes Glanzfeld ausblenden
                GlossLeft.Height = CInt(Height / 3)
                GlossRight.Height = GlossLeft.Height
            Catch MyException As System.Exception
                'Einen Fehler zurückgeben und beenden
                Return False
                Exit Function
            End Try
            Return True
        End Function

        Private Function UpdateProgress() As Boolean
            Try
                'Globale Werte neu berechnen
                _ProgressUnit = CInt(Me.Width / _MaxValue)
                ProgressFull.Width = _ProgressValue * _ProgressUnit
                'Glanzfelder gemäß Globals ausblenden oder anzeigen
                If _IsGlossy Then
                    GlossLeft.Visible = True
                    GlossRight.Visible = True
                Else
                    GlossLeft.Visible = False
                    GlossRight.Visible = False
                End If
                'Bei Maximalwert den Fortschrittsbalken ausfüllen
                If _ProgressValue = _MaxValue Then
                    ProgressFull.Width = If(_ShowBorder, Width - 2, Width)
                End If
                'Ränder je nach Globalwerten ausblenden oder anzeigen
                Padding = If(_ShowBorder, New System.Windows.Forms.Padding(1), New System.Windows.Forms.Padding(0))
            Catch MyException As System.Exception
                'Einen Fehler zurückgeben und beenden
                Return False
                Exit Function
            End Try
            Return True
        End Function

#End Region

#Region "Interne Ereignisbehandlungen"

        Private Sub ColorProgressBar_PaddingChanged(sender As Object, e As System.EventArgs) Handles Me.PaddingChanged
            Padding = If(_ShowBorder, New System.Windows.Forms.Padding(1), New System.Windows.Forms.Padding(0))
        End Sub

        Private Sub ColorProgressBar_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
            If Value <= _MaxValue Then
                Dim unused = UpdateProgress()
                Dim unused1 = UpdateGloss()
            End If
        End Sub

        Private Sub Panel_Click(sender As Object, e As System.EventArgs) Handles GlossLeft.Click, ProgressFull.Click, ProgressEmpty.Click, GlossRight.Click
            RaiseEvent Click(Me, e)
        End Sub

#End Region

    End Class

End Namespace