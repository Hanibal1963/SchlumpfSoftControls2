' *************************************************************************************************
' SingleDigit.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
'Imports SchlumpfSoft.Controls.Attribute

Namespace SevenSegmentControl

    ''' <summary>
    ''' <para>Dieses Steuerelement stellt ein einzelnes Siebensegment-LED-Display dar,
    ''' </para>
    ''' <para>das eine Ziffer oder einen Buchstaben anzeigt.</para>
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Dieses Steuerelement stellt ein einzelnes Siebensegment-LED-Display dar, das eine Ziffer oder einen Buchstaben anzeigt.")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(SevenSegmentControl.SingleDigit), "SingleDigit.bmp")>
    Public Class SingleDigit : Inherits Control

#Region "interne Eigenschaftsvariablen"

        Private ReadOnly _segmentPoints As Point()()
        Private ReadOnly _digitHeight As Integer = 80
        Private ReadOnly _digitWidth As Integer = 48
        Private _segmentWidth As Integer = 10
        Private _italicFactor As Single = -0.1F
        Private _backgroundColor As Color = Color.LightGray
        Private _inactiveColor As Color = Color.DarkGray
        Private _foreColor As Color = Color.DarkGreen
        Private _digitValue As String = Nothing
        Private _showDecimalPoint As Boolean = True
        Private _decimalPointActive As Boolean = False
        Private _showColon As Boolean = False
        Private _colonActive As Boolean = False
        Private _customBitPattern As Integer = 0

#End Region

        ''' <summary>
        ''' Wird ausgeführt wenn eine neue Instanz dieses Controls erstellt wird.
        ''' </summary>
        Public Sub New()
            SuspendLayout()
            Name = "SevSegSingleDigit"
            Size = New Size(32, 64)
            ResumeLayout(False)
            TabStop = False
            Padding = New Padding(10, 4, 10, 4)
            MyBase.DoubleBuffered = True
            _segmentPoints = New Point(6)() {}
            For i = 0 To 6
                _segmentPoints(i) = New Point(5) {}
            Next
            CalculatePoints(_segmentPoints, _digitHeight, _digitWidth, _segmentWidth)
        End Sub

#Region "neue Eigenschaften"

        ''' <summary>
        ''' Legt die Farbe inaktiver Segmente fest oder gibt diese zurück.
        ''' </summary>
        <Category("Appearance")>
        <Description("Legt die Farbe inaktiver Segmente fest oder gibt diese zurück.")>
        Public Property InactiveColor As Color
            Get
                Return _inactiveColor
            End Get
            Set(value As Color)
                _inactiveColor = value
                Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Breite der LED-Segmente fest oder gibt diese zurück.
        ''' </summary>
        <Category("Appearance")>
        <Description("Legt die Breite der LED-Segmente fest oder gibt diese zurück.")>
        Public Property SegmentWidth As Integer
            Get
                Return _segmentWidth
            End Get
            Set(value As Integer)
                _segmentWidth = value
                CalculatePoints(_segmentPoints, _digitHeight, _digitWidth, _segmentWidth)
                Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Scherkoeffizient für die Kursivschrift der Anzeige.
        ''' </summary>
        ''' <remarks>
        ''' Standarwert ist -0,1.
        ''' </remarks>
        <Category("Appearance")>
        <Description("Scherkoeffizient für die Kursivschrift der Anzeige.")>
        Public Property ItalicFactor As Single
            Get
                Return _italicFactor
            End Get
            Set(value As Single)
                _italicFactor = value
                Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Legt das anzuzeigende Zeichen fest oder gibt dieses zurück.
        ''' </summary>
        ''' <remarks>
        ''' Unterstützte Zeichen sind Ziffern und die meisten Buchstaben.
        ''' </remarks>
        <Category("Appearance")>
        <Description("Legt das anzuzeigende Zeichen fest oder gibt dieses zurück.")>
        Public Property DigitValue As String
            Get
                Return _digitValue
            End Get
            Set(value As String)
                _customBitPattern = 0
                _digitValue = value
                Invalidate()
                If Equals(value, Nothing) OrElse value.Length = 0 Then
                    Return
                End If
                Dim tempValue As Integer
                If Integer.TryParse(value, tempValue) Then
                    If tempValue > 9 Then tempValue = 9
                    If tempValue < 0 Then tempValue = 0
                    'ist es eine ganze Zahl?
                    Select Case tempValue
                        Case 0 : _customBitPattern = CharacterPattern.Zero
                        Case 1 : _customBitPattern = CharacterPattern.One
                        Case 2 : _customBitPattern = CharacterPattern.Two
                        Case 3 : _customBitPattern = CharacterPattern.Three
                        Case 4 : _customBitPattern = CharacterPattern.Four
                        Case 5 : _customBitPattern = CharacterPattern.Five
                        Case 6 : _customBitPattern = CharacterPattern.Six
                        Case 7 : _customBitPattern = CharacterPattern.Seven
                        Case 8 : _customBitPattern = CharacterPattern.Eight
                        Case 9 : _customBitPattern = CharacterPattern.Nine
                        Case 8 : _customBitPattern = CharacterPattern.Eight
                        Case 9 : _customBitPattern = CharacterPattern.Nine
                    End Select
                Else
                    'ist es ein Buchstabe?
                    Select Case value(0)
                        Case "A"c, "a"c : _customBitPattern = CharacterPattern.A
                        Case "B"c, "b"c : _customBitPattern = CharacterPattern.B
                        Case "C"c : _customBitPattern = CharacterPattern.C
                        Case "c"c : _customBitPattern = CharacterPattern.cField
                        Case "D"c, "d"c : _customBitPattern = CharacterPattern.D
                        Case "E"c, "e"c : _customBitPattern = CharacterPattern.E
                        Case "F"c, "f"c : _customBitPattern = CharacterPattern.F
                        Case "G"c, "g"c : _customBitPattern = CharacterPattern.G
                        Case "H"c : _customBitPattern = CharacterPattern.H
                        Case "h"c : _customBitPattern = CharacterPattern.hField
                        Case "I"c : _customBitPattern = CharacterPattern.One
                        Case "i"c : _customBitPattern = CharacterPattern.i
                        Case "J"c, "j"c : _customBitPattern = CharacterPattern.J
                        Case "L"c, "l"c : _customBitPattern = CharacterPattern.L
                        Case "N"c, "n"c : _customBitPattern = CharacterPattern.N
                        Case "O"c : _customBitPattern = CharacterPattern.Zero
                        Case "o"c : _customBitPattern = CharacterPattern.o
                        Case "P"c, "p"c : _customBitPattern = CharacterPattern.P
                        Case "Q"c, "q"c : _customBitPattern = CharacterPattern.Q
                        Case "R"c, "r"c : _customBitPattern = CharacterPattern.R
                        Case "S"c, "s"c : _customBitPattern = CharacterPattern.Five
                        Case "T"c, "t"c : _customBitPattern = CharacterPattern.T
                        Case "U"c : _customBitPattern = CharacterPattern.U
                        Case "u"c, "µ"c, "μ"c : _customBitPattern = CharacterPattern.uField
                        Case "Y"c, "y"c : _customBitPattern = CharacterPattern.Y
                        Case "-"c : _customBitPattern = CharacterPattern.Dash
                        Case "="c : _customBitPattern = CharacterPattern.Equals
                        Case "°"c : _customBitPattern = CharacterPattern.Degrees
                        Case "'"c : _customBitPattern = CharacterPattern.Apostrophe
                        Case """"c : _customBitPattern = CharacterPattern.Quote
                        Case "["c, "{"c : _customBitPattern = CharacterPattern.C
                        Case "]"c, "}"c : _customBitPattern = CharacterPattern.RBracket
                        Case "_"c : _customBitPattern = CharacterPattern.Underscore
                        Case "≡"c : _customBitPattern = CharacterPattern.Identical
                        Case "¬"c : _customBitPattern = CharacterPattern.Not
                    End Select
                End If
            End Set
        End Property

        ''' <summary>
        ''' <para>Legt ein benutzerdefiniertes Bitmuster fest, das in den sieben Segmenten
        ''' angezeigt werden soll.</para>
        ''' <para>Dies ist ein ganzzahliger Wert, bei dem die Bits 0 bis 6 den jeweiligen
        ''' LED-Segmenten entsprechen.</para>
        ''' </summary>
        <Category("Appearance")>
        <Description("Legt ein benutzerdefiniertes Bitmuster fest, das in den sieben Segmenten angezeigt werden soll.")>
        Public Property CustomBitPattern As Integer
            Get
                Return _customBitPattern
            End Get
            Set(value As Integer)
                _customBitPattern = value
                Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob die Dezimalpunkt-LED angezeigt wird.
        ''' </summary>
        <Category("Appearance")>
        <Description("Gibt an, ob die Dezimalpunkt-LED angezeigt wird.")>
        Public Property ShowDecimalPoint As Boolean
            Get
                Return _showDecimalPoint
            End Get
            Set(value As Boolean)
                _showDecimalPoint = value
                Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob die Dezimalpunkt-LED aktiv ist.
        ''' </summary>
        <Category("Appearance")>
        <Description("Gibt an, ob die Dezimalpunkt-LED aktiv ist.")>
        Public Property DecimalPointActive As Boolean
            Get
                Return _decimalPointActive
            End Get
            Set(value As Boolean)
                _decimalPointActive = value
                Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob die Doppelpunkt-LEDs angezeigt werden.
        ''' </summary>
        <Category("Appearance")>
        <Description("Gibt an, ob die Doppelpunkt-LEDs angezeigt werden.")>
        Public Property ShowColon As Boolean
            Get
                Return _showColon
            End Get
            Set(value As Boolean)
                _showColon = value
                Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob die Doppelpunkt-LEDs aktiv sind.
        ''' </summary>
        <Category("Appearance")>
        <Description("Gibt an, ob die Doppelpunkt-LEDs aktiv sind.")>
        Public Property ColonActive As Boolean
            Get
                Return _colonActive
            End Get
            Set(value As Boolean)
                _colonActive = value
                Invalidate()
            End Set
        End Property

#End Region

#Region "geänderte Eigenschaften"

        ''' <summary>
        ''' Legt die Hintergrundfarbe des Controls fest oder gibt diese zurück.
        ''' </summary>
        ''' <returns></returns>
        <Category("Appearance")>
        <Description("Legt die Hintergrundfarbe des Controls fest oder gibt diese zurück.")>
        Public Overrides Property BackColor As Color
            Get
                Return _backgroundColor
            End Get
            Set(value As Color)
                _backgroundColor = value
                Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Vordergrundfarbe der Segmente des Controls fest oder gibt diese zurück.
        ''' </summary>
        ''' <returns></returns>
        <Category("Appearance")>
        <Description("Legt die Vordergrundfarbe der Segmente des Controls fest oder gibt diese zurück.")>
        Public Overrides Property ForeColor As Color
            Get
                Return _foreColor
            End Get
            Set(value As Color)
                _foreColor = value
                Invalidate()
            End Set
        End Property

#End Region

#Region "Ausgeblendete Eigenschaften"

        ''' <summary>
        ''' ausgeblendet da nicht relevant.
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
        ''' ausgeblendet da nicht relevant.
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
        ''' ausgeblendet da nicht relevant.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property Font As Font
            Get
                Return MyBase.Font
            End Get
            Set(value As Font)
                MyBase.Font = value
            End Set
        End Property

        ''' <summary>
        ''' ausgeblendet da nicht relevant.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property Text As String
            Get
                Return MyBase.Text
            End Get
            Set(value As String)
                MyBase.Text = value
            End Set
        End Property

        ''' <summary>
        ''' ausgeblendet da nicht relevant.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property RightToLeft As RightToLeft
            Get
                Return MyBase.RightToLeft
            End Get
            Set(value As RightToLeft)
                MyBase.RightToLeft = value
            End Set
        End Property

#End Region

#Region "interne Ereignisbehandlung"

        ''' <summary>
        ''' Tritt ein, wenn das Steuerelement neu gezeichnet wird.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub SevSegsingleDigit_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
            Dim useValue = _customBitPattern
            Dim brushLight As Brush = New SolidBrush(_foreColor)
            Dim brushDark As Brush = New SolidBrush(_inactiveColor)
            'Definiert die Transformation für den Container ...
            Dim srcRect As RectangleF
            Dim colonWidth As Integer = CInt(_digitWidth / 4)
            srcRect = If(_showColon,
                New RectangleF(0.0F, 0.0F, _digitWidth + colonWidth, _digitHeight),
                New RectangleF(0.0F, 0.0F, _digitWidth, _digitHeight))
            Dim destRect As New RectangleF(Padding.Left, Padding.Top, Width - Padding.Left - Padding.Right, Height - Padding.Top - Padding.Bottom)
            'Grafikcontainer, der die Koordinaten neu zuordnet
            Dim containerState = e.Graphics.BeginContainer(destRect, srcRect, GraphicsUnit.Pixel)
            Dim trans As New Matrix()
            trans.Shear(_italicFactor, 0.0F)
            e.Graphics.Transform = trans
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Default
            'Segmente zeichnen
            PaintSegments(e, useValue, brushLight, brushDark, _segmentPoints)
            If _showDecimalPoint Then
                e.Graphics.FillEllipse(If(_decimalPointActive, brushLight, brushDark), _digitWidth - 1, _digitHeight - _segmentWidth + 1, _segmentWidth, _segmentWidth)
            End If
            If _showColon Then
                e.Graphics.FillEllipse(If(_colonActive, brushLight, brushDark), _digitWidth + colonWidth - 4, CInt((_digitHeight / 4) - _segmentWidth + 8), _segmentWidth, _segmentWidth)
                e.Graphics.FillEllipse(If(_colonActive, brushLight, brushDark), _digitWidth + colonWidth - 4, CInt((_digitHeight * 3 / 4) - _segmentWidth + 4), _segmentWidth, _segmentWidth)
            End If
            e.Graphics.EndContainer(containerState)
        End Sub


        ''' <summary>
        ''' Zeichnet die Segmente basierend darauf, ob das entsprechende Bit hoch ist
        ''' </summary>
        ''' <param name="e"></param>
        ''' <param name="BitPattern"></param>
        ''' <param name="BrushLight"></param>
        ''' <param name="BrushDark"></param>
        ''' <param name="SegmentPoints"></param>
        Private Sub PaintSegments(e As PaintEventArgs, BitPattern As Integer, BrushLight As Brush, BrushDark As Brush, ByRef SegmentPoints As Point()())
            e.Graphics.FillPolygon(If((BitPattern And &H1) = &H1, BrushLight, BrushDark), SegmentPoints(0))
            e.Graphics.FillPolygon(If((BitPattern And &H2) = &H2, BrushLight, BrushDark), SegmentPoints(1))
            e.Graphics.FillPolygon(If((BitPattern And &H4) = &H4, BrushLight, BrushDark), SegmentPoints(2))
            e.Graphics.FillPolygon(If((BitPattern And &H8) = &H8, BrushLight, BrushDark), SegmentPoints(3))
            e.Graphics.FillPolygon(If((BitPattern And &H10) = &H10, BrushLight, BrushDark), SegmentPoints(4))
            e.Graphics.FillPolygon(If((BitPattern And &H20) = &H20, BrushLight, BrushDark), SegmentPoints(5))
            e.Graphics.FillPolygon(If((BitPattern And &H40) = &H40, BrushLight, BrushDark), SegmentPoints(6))
        End Sub


        ''' <summary>
        ''' Tritt beim Ändern der Größe des Steuerelements ein.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub SevSegSingleDigit_Resize(sender As Object, e As EventArgs) Handles Me.Resize
            Invalidate()
        End Sub

#End Region

#Region "geänderte Methoden"


        ''' <summary>
        ''' Löst das PaddingChanged-Ereignis aus.
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overrides Sub OnPaddingChanged(e As EventArgs)
            MyBase.OnPaddingChanged(e)
            Invalidate()
        End Sub


        ''' <summary>
        ''' Zeichnet den Hintergrund des Steuerelements.
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            'MyBase.OnPaintBackground(e)
            e.Graphics.Clear(_backgroundColor)
        End Sub


        ''' <summary>
        ''' <para>Gibt nicht verwaltete Ressourcen frei und führt weitere
        ''' Bereinigungsvorgänge durch, </para>
        ''' <para>bevor <see cref="SchlumpfSoft.Controls.SevenSegmentControl.SingleDigit"/>
        ''' durch die Garbage Collection zurückgefordert wird.</para>
        ''' </summary>
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Private Sub InitializeComponent()
            SuspendLayout()
            ResumeLayout(False)
        End Sub

#End Region

#Region "Interne Hilfsfunktionen"


        ''' <summary>
        ''' <para>Berechnet die Punkte, die die Polygone der sieben Segmente darstellen,
        ''' </para>
        ''' <para>unabhängig davon, ob initialisiert oder die Segmentbreite geändert
        ''' wird.</para>
        ''' </summary>
        ''' <param name="SegmentCornerPoints"></param>
        ''' <param name="DigitHeight"></param>
        ''' <param name="DigitWidth"></param>
        ''' <param name="SegmentWidth"></param>
        Private Sub CalculatePoints(ByRef SegmentCornerPoints As Point()(), DigitHeight As Integer, DigitWidth As Integer, SegmentWidth As Integer)
            Dim halfHeight As Integer = CInt(DigitHeight / 2)
            Dim halfWidth As Integer = CInt(SegmentWidth / 2)
            Dim p = 0
            SegmentCornerPoints(p)(0).X = SegmentWidth + 1
            SegmentCornerPoints(p)(0).Y = 0
            SegmentCornerPoints(p)(1).X = DigitWidth - SegmentWidth - 1
            SegmentCornerPoints(p)(1).Y = 0
            SegmentCornerPoints(p)(2).X = DigitWidth - halfWidth - 1
            SegmentCornerPoints(p)(2).Y = halfWidth
            SegmentCornerPoints(p)(3).X = DigitWidth - SegmentWidth - 1
            SegmentCornerPoints(p)(3).Y = SegmentWidth
            SegmentCornerPoints(p)(4).X = SegmentWidth + 1
            SegmentCornerPoints(p)(4).Y = SegmentWidth
            SegmentCornerPoints(p)(5).X = halfWidth + 1
            SegmentCornerPoints(p)(5).Y = halfWidth
            p += 1
            SegmentCornerPoints(p)(0).X = 0
            SegmentCornerPoints(p)(0).Y = SegmentWidth + 1
            SegmentCornerPoints(p)(1).X = halfWidth
            SegmentCornerPoints(p)(1).Y = halfWidth + 1
            SegmentCornerPoints(p)(2).X = SegmentWidth
            SegmentCornerPoints(p)(2).Y = SegmentWidth + 1
            SegmentCornerPoints(p)(3).X = SegmentWidth
            SegmentCornerPoints(p)(3).Y = halfHeight - halfWidth - 1
            SegmentCornerPoints(p)(4).X = 4
            SegmentCornerPoints(p)(4).Y = halfHeight - 1
            SegmentCornerPoints(p)(5).X = 0
            SegmentCornerPoints(p)(5).Y = halfHeight - 1
            p += 1
            SegmentCornerPoints(p)(0).X = DigitWidth - SegmentWidth
            SegmentCornerPoints(p)(0).Y = SegmentWidth + 1
            SegmentCornerPoints(p)(1).X = DigitWidth - halfWidth
            SegmentCornerPoints(p)(1).Y = halfWidth + 1
            SegmentCornerPoints(p)(2).X = DigitWidth
            SegmentCornerPoints(p)(2).Y = SegmentWidth + 1
            SegmentCornerPoints(p)(3).X = DigitWidth
            SegmentCornerPoints(p)(3).Y = halfHeight - 1
            SegmentCornerPoints(p)(4).X = DigitWidth - 4
            SegmentCornerPoints(p)(4).Y = halfHeight - 1
            SegmentCornerPoints(p)(5).X = DigitWidth - SegmentWidth
            SegmentCornerPoints(p)(5).Y = halfHeight - halfWidth - 1
            p += 1
            SegmentCornerPoints(p)(0).X = SegmentWidth + 1
            SegmentCornerPoints(p)(0).Y = halfHeight - halfWidth
            SegmentCornerPoints(p)(1).X = DigitWidth - SegmentWidth - 1
            SegmentCornerPoints(p)(1).Y = halfHeight - halfWidth
            SegmentCornerPoints(p)(2).X = DigitWidth - 5
            SegmentCornerPoints(p)(2).Y = halfHeight
            SegmentCornerPoints(p)(3).X = DigitWidth - SegmentWidth - 1
            SegmentCornerPoints(p)(3).Y = halfHeight + halfWidth
            SegmentCornerPoints(p)(4).X = SegmentWidth + 1
            SegmentCornerPoints(p)(4).Y = halfHeight + halfWidth
            SegmentCornerPoints(p)(5).X = 5
            SegmentCornerPoints(p)(5).Y = halfHeight
            p += 1
            SegmentCornerPoints(p)(0).X = 0
            SegmentCornerPoints(p)(0).Y = halfHeight + 1
            SegmentCornerPoints(p)(1).X = 4
            SegmentCornerPoints(p)(1).Y = halfHeight + 1
            SegmentCornerPoints(p)(2).X = SegmentWidth
            SegmentCornerPoints(p)(2).Y = halfHeight + halfWidth + 1
            SegmentCornerPoints(p)(3).X = SegmentWidth
            SegmentCornerPoints(p)(3).Y = DigitHeight - SegmentWidth - 1
            SegmentCornerPoints(p)(4).X = halfWidth
            SegmentCornerPoints(p)(4).Y = DigitHeight - halfWidth - 1
            SegmentCornerPoints(p)(5).X = 0
            SegmentCornerPoints(p)(5).Y = DigitHeight - SegmentWidth - 1
            p += 1
            SegmentCornerPoints(p)(0).X = DigitWidth - SegmentWidth
            SegmentCornerPoints(p)(0).Y = halfHeight + halfWidth + 1
            SegmentCornerPoints(p)(1).X = DigitWidth - 4
            SegmentCornerPoints(p)(1).Y = halfHeight + 1
            SegmentCornerPoints(p)(2).X = DigitWidth
            SegmentCornerPoints(p)(2).Y = halfHeight + 1
            SegmentCornerPoints(p)(3).X = DigitWidth
            SegmentCornerPoints(p)(3).Y = DigitHeight - SegmentWidth - 1
            SegmentCornerPoints(p)(4).X = DigitWidth - halfWidth
            SegmentCornerPoints(p)(4).Y = DigitHeight - halfWidth - 1
            SegmentCornerPoints(p)(5).X = DigitWidth - SegmentWidth
            SegmentCornerPoints(p)(5).Y = DigitHeight - SegmentWidth - 1
            p += 1
            SegmentCornerPoints(p)(0).X = SegmentWidth + 1
            SegmentCornerPoints(p)(0).Y = DigitHeight - SegmentWidth
            SegmentCornerPoints(p)(1).X = DigitWidth - SegmentWidth - 1
            SegmentCornerPoints(p)(1).Y = DigitHeight - SegmentWidth
            SegmentCornerPoints(p)(2).X = DigitWidth - halfWidth - 1
            SegmentCornerPoints(p)(2).Y = DigitHeight - halfWidth
            SegmentCornerPoints(p)(3).X = DigitWidth - SegmentWidth - 1
            SegmentCornerPoints(p)(3).Y = DigitHeight
            SegmentCornerPoints(p)(4).X = SegmentWidth + 1
            SegmentCornerPoints(p)(4).Y = DigitHeight
            SegmentCornerPoints(p)(5).X = halfWidth + 1
            SegmentCornerPoints(p)(5).Y = DigitHeight - halfWidth
        End Sub

#End Region


        ''' <summary>
        ''' <para>Dies sind die verschiedenen Bitmuster, die die Zeichen darstellen, </para>
        ''' <para>die in den sieben Segmenten angezeigt werden können.<br/>
        ''' </para>
        ''' </summary>
        ''' <remarks>
        ''' Die Bits 0 bis 6 entsprechen den einzelnen LEDs, von oben nach unten!
        ''' </remarks>
        Private Enum CharacterPattern
            None = &H0
            Zero = &H77
            One = &H24
            Two = &H5D
            Three = &H6D
            Four = &H2E
            Five = &H6B
            Six = &H7B
            Seven = &H25
            Eight = &H7F
            Nine = &H6F
            A = &H3F
            B = &H7A
            C = &H53
            cField = &H58
            D = &H7C
            E = &H5B
            F = &H1B
            G = &H73
            H = &H3E
            hField = &H3A
            i = &H20
            J = &H74
            L = &H52
            N = &H38
            o = &H78
            P = &H1F
            Q = &H2F
            R = &H18
            T = &H5A
            U = &H76
            uField = &H70
            Y = &H6E
            Dash = &H8
            Equals = &H48
            Degrees = &HF
            Apostrophe = &H2
            Quote = &H6
            RBracket = &H65
            Underscore = &H40
            Identical = &H49
            [Not] = &H28
        End Enum

    End Class

End Namespace