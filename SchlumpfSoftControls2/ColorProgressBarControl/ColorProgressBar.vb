' *************************************************************************************************
' ColorProgressBar.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
'Imports SchlumpfSoft.Controls.Attribute

Namespace ColorProgressBarControl

    ''' <summary>
    ''' Control zum Anzeigen eines farbigen Fortschrittbalkens.
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Control zum Anzeigen eines farbigen Fortschrittbalkens.")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(ColorProgressBarControl.ColorProgressBar), "ColorProgressBar.bmp")>
    Public Class ColorProgressBar

        Inherits UserControl

#Region "Interne Eigenschaftsvariablen"

        Private _ProgressUnit As Integer = 20 ' Der Betrag in Pixeln, um den der Fortschrittsbalken erhöht wird.
        Private _ProgressValue As Integer = 1 ' Die Menge des ausgefüllten Maximalwerts.
        Private _MaxValue As Integer = 10 ' Der Maximalwert des Fortschrittsbalkens.
        Private _ShowBorder As Boolean = True ' Legt fest, ob der Rahmen auf der Fortschrittsanzeige aktiviert ist
        Private _IsGlossy As Boolean = True ' Legt fest, ob der Glanz auf der Fortschrittsleiste angezeigt wird.
        Private _BarColor As Color = Color.Blue ' Die Farbe des Fortschrittsbalkens
        Private _EmptyColor As Color = Color.LightGray ' Die Farbe des leeren Fortschrittsbalkens.
        Private _BorderColor As Color = Color.Black ' Die Farbe des Rahmens.

#End Region

#Region "Ereignisdefinitionen für öffentliche Ereignisse"

        Public Shadows Event Click(sender As Object, e As EventArgs)

#End Region

#Region "neue Eigenschaften"

        ''' <summary>
        ''' Gibt den Gesamtfortschritt des Fortschrittsbalkens zurück oder legt diesen fest.
        ''' </summary>
        <Browsable(True)>
        <Category("Behavior")>
        <Description("Gibt den Gesamtfortschritt des Fortschrittsbalkens zurück oder legt diesen fest.")>
        <DefaultValue(1)>
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
        <Browsable(True)>
        <Category("Behavior")>
        <Description("Gibt den Maximalwert des Fortschrittsbalkens zurück oder legt diesen fest.")>
        <DefaultValue(10)>
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
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt die Farbe des Fortschrittsbalkens zurück oder legt diese fest.")>
        Public Property BarColor As Color
            Get
                Return _BarColor
            End Get
            Set(value As Color)
                _BarColor = value
                Me.ProgressFull.BackColor = _BarColor
            End Set
        End Property

        ''' <summary>
        ''' Gibt die Farbe des leeren Fortschrittsbalkens zurück oder legt diese fest.
        ''' </summary>
        ''' <returns></returns>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt die Farbe des leeren Fortschrittsbalkens zurück oder legt diese fest.")>
        Public Property EmptyColor As Color
            Get
                Return _EmptyColor
            End Get
            Set(value As Color)
                _EmptyColor = value
                ProgressEmpty.BackColor = _EmptyColor
            End Set
        End Property

        ''' <summary>
        ''' Gibt die Farbe des Rahmens zurück oder legt diese fest.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt die Farbe des Rahmens zurück oder legt diese fest.")>
        Public Property BorderColor As Color
            Get
                Return _BorderColor
            End Get
            Set(value As Color)
                _BorderColor = value
                BackColor = _BorderColor
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob der Rahmen auf der Fortschrittsanzeige aktiviert ist.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt an, ob der Rahmen auf der Fortschrittsanzeige aktiviert ist.")>
        <DefaultValue(True)>
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
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt an, ob der Glanz auf der Fortschrittsleiste angezeigt wird.")>
        <DefaultValue(True)>
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
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property BackColor As Color
            Get
                Return MyBase.BackColor
            End Get
            Set(value As Color)
                MyBase.BackColor = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da nicht relevant.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImage As Image
            Get
                Return MyBase.BackgroundImage
            End Get
            Set(value As Image)
                MyBase.BackgroundImage = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da nicht relevant.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImageLayout As ImageLayout
            Get
                Return MyBase.BackgroundImageLayout
            End Get
            Set(value As ImageLayout)
                MyBase.BackgroundImageLayout = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da nicht relevant.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Property BorderStyle As BorderStyle
            Get
                Return MyBase.BorderStyle
            End Get
            Set(value As BorderStyle)
                MyBase.BorderStyle = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da nicht relevant.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property ForeColor As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As Color)
                MyBase.ForeColor = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da nicht relevant.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overloads Property Padding As Padding
            Get
                Return MyBase.Padding
            End Get
            Set(value As Padding)
                MyBase.Padding = value
            End Set
        End Property

#End Region

        Public Sub New()
            'Dieser Aufruf ist für den Designer erforderlich.
            Me.InitializeComponent()
            'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            ' Standardwerte setzen
            GlossLeft.BackColor = Color.FromArgb(100, 255, 255, 255)
            GlossRight.BackColor = Color.FromArgb(100, 255, 255, 255)
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
            Catch MyException As Exception
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
                Padding = If(_ShowBorder, New Padding(1), New Padding(0))
            Catch MyException As Exception
                'Einen Fehler zurückgeben und beenden
                Return False
                Exit Function
            End Try
            Return True
        End Function

#End Region

#Region "Interne Ereignisbehandlungen"

        Private Sub ColorProgressBar_PaddingChanged(sender As Object, e As EventArgs) Handles Me.PaddingChanged
            Padding = If(_ShowBorder, New Padding(1), New Padding(0))
        End Sub

        Private Sub ColorProgressBar_Resize(sender As Object, e As EventArgs) Handles Me.Resize
            If Value <= _MaxValue Then
                Dim unused = UpdateProgress()
                Dim unused1 = UpdateGloss()
            End If
        End Sub

        Private Sub Panel_Click(sender As Object, e As EventArgs) Handles GlossLeft.Click, ProgressFull.Click, ProgressEmpty.Click, GlossRight.Click
            RaiseEvent Click(Me, e)
        End Sub

#End Region

    End Class

End Namespace