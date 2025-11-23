' *************************************************************************************************
' SingleDigit.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

' TODO: Code noch überarbeiten

Namespace SevenSegmentControl

    ''' <summary>
    ''' <para>Dieses Steuerelement stellt ein einzelnes Siebensegment-LED-Display dar,
    ''' </para>
    ''' <para>das eine Ziffer oder einen Buchstaben anzeigt.</para>
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Dieses Steuerelement stellt ein einzelnes Siebensegment-LED-Display dar, das eine Ziffer oder einen Buchstaben anzeigt.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(SevenSegmentControl.SingleDigit), "SingleDigit.bmp")>
    Public Class SingleDigit : Inherits System.Windows.Forms.Control

#Region "Variablendefinition"

        Private ReadOnly _SegmentPoints As System.Drawing.Point()()
        Private ReadOnly _DigitHeight As Integer = 80
        Private ReadOnly _DigitWidth As Integer = 48
        Private _SegmentWidth As Integer = 10
        Private _ItalicFactor As Single = -0.1F
        Private _BackgroundColor As System.Drawing.Color = System.Drawing.Color.LightGray
        Private _InactiveColor As System.Drawing.Color = System.Drawing.Color.DarkGray
        Private _ForeColor As System.Drawing.Color = System.Drawing.Color.DarkGreen
        Private _DigitValue As String = Nothing
        Private _ShowDecimalPoint As Boolean = True
        Private _DecimalPointActive As Boolean = False
        Private _ShowColon As Boolean = False
        Private _ColonActive As Boolean = False
        Private _CustomBitPattern As Integer = 0

#End Region

#Region "neue Eigenschaften"

        ''' <summary>
        ''' Legt die Farbe inaktiver Segmente fest oder gibt diese zurück.
        ''' </summary>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Farbe inaktiver Segmente fest oder gibt diese zurück.")>
        Public Property InactiveColor As System.Drawing.Color
            Get
                Return Me._inactiveColor
            End Get
            Set(value As System.Drawing.Color)
                Me._inactiveColor = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Breite der LED-Segmente fest oder gibt diese zurück.
        ''' </summary>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Breite der LED-Segmente fest oder gibt diese zurück.")>
        Public Property SegmentWidth As Integer
            Get
                Return Me._segmentWidth
            End Get
            Set(value As Integer)
                Me._segmentWidth = value
                Me.CalculatePoints(Me._segmentPoints, Me._digitHeight, Me._digitWidth, Me._segmentWidth)
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Scherkoeffizient für die Kursivschrift der Anzeige.
        ''' </summary>
        ''' <remarks>
        ''' Standarwert ist -0,1.
        ''' </remarks>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Scherkoeffizient für die Kursivschrift der Anzeige.")>
        Public Property ItalicFactor As Single
            Get
                Return Me._italicFactor
            End Get
            Set(value As Single)
                Me._italicFactor = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Legt das anzuzeigende Zeichen fest oder gibt dieses zurück.
        ''' </summary>
        ''' <remarks>
        ''' Unterstützte Zeichen sind Ziffern und die meisten Buchstaben.
        ''' </remarks>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt das anzuzeigende Zeichen fest oder gibt dieses zurück.")>
        Public Property DigitValue As String
            Get
                Return Me._digitValue
            End Get
            Set(value As String)
                Me._customBitPattern = 0
                Me._digitValue = value
                Me.Invalidate()
                If Equals(value, Nothing) OrElse value.Length = 0 Then
                    Return
                End If
                Dim tempValue As Integer
                If Integer.TryParse(value, tempValue) Then
                    If tempValue > 9 Then tempValue = 9
                    If tempValue < 0 Then tempValue = 0
                    'ist es eine ganze Zahl?
                    Select Case tempValue
                        Case 0 : Me._customBitPattern = CharacterPattern.Zero
                        Case 1 : Me._customBitPattern = CharacterPattern.One
                        Case 2 : Me._customBitPattern = CharacterPattern.Two
                        Case 3 : Me._customBitPattern = CharacterPattern.Three
                        Case 4 : Me._customBitPattern = CharacterPattern.Four
                        Case 5 : Me._customBitPattern = CharacterPattern.Five
                        Case 6 : Me._customBitPattern = CharacterPattern.Six
                        Case 7 : Me._customBitPattern = CharacterPattern.Seven
                        Case 8 : Me._customBitPattern = CharacterPattern.Eight
                        Case 9 : Me._customBitPattern = CharacterPattern.Nine
                        Case 8 : Me._customBitPattern = CharacterPattern.Eight
                        Case 9 : Me._customBitPattern = CharacterPattern.Nine
                    End Select
                Else
                    'ist es ein Buchstabe?
                    Select Case value(0)
                        Case "A"c, "a"c : Me._customBitPattern = CharacterPattern.A
                        Case "B"c, "b"c : Me._customBitPattern = CharacterPattern.B
                        Case "C"c : Me._customBitPattern = CharacterPattern.C
                        Case "c"c : Me._customBitPattern = CharacterPattern.cField
                        Case "D"c, "d"c : Me._customBitPattern = CharacterPattern.D
                        Case "E"c, "e"c : Me._customBitPattern = CharacterPattern.E
                        Case "F"c, "f"c : Me._customBitPattern = CharacterPattern.F
                        Case "G"c, "g"c : Me._customBitPattern = CharacterPattern.G
                        Case "H"c : Me._customBitPattern = CharacterPattern.H
                        Case "h"c : Me._customBitPattern = CharacterPattern.hField
                        Case "I"c : Me._customBitPattern = CharacterPattern.One
                        Case "i"c : Me._customBitPattern = CharacterPattern.i
                        Case "J"c, "j"c : Me._customBitPattern = CharacterPattern.J
                        Case "L"c, "l"c : Me._customBitPattern = CharacterPattern.L
                        Case "N"c, "n"c : Me._customBitPattern = CharacterPattern.N
                        Case "O"c : Me._customBitPattern = CharacterPattern.Zero
                        Case "o"c : Me._customBitPattern = CharacterPattern.o
                        Case "P"c, "p"c : Me._customBitPattern = CharacterPattern.P
                        Case "Q"c, "q"c : Me._customBitPattern = CharacterPattern.Q
                        Case "R"c, "r"c : Me._customBitPattern = CharacterPattern.R
                        Case "S"c, "s"c : Me._customBitPattern = CharacterPattern.Five
                        Case "T"c, "t"c : Me._customBitPattern = CharacterPattern.T
                        Case "U"c : Me._customBitPattern = CharacterPattern.U
                        Case "u"c, "µ"c, "μ"c : Me._customBitPattern = CharacterPattern.uField
                        Case "Y"c, "y"c : Me._customBitPattern = CharacterPattern.Y
                        Case "-"c : Me._customBitPattern = CharacterPattern.Dash
                        Case "="c : Me._customBitPattern = CharacterPattern.Equals
                        Case "°"c : Me._customBitPattern = CharacterPattern.Degrees
                        Case "'"c : Me._customBitPattern = CharacterPattern.Apostrophe
                        Case """"c : Me._customBitPattern = CharacterPattern.Quote
                        Case "["c, "{"c : Me._customBitPattern = CharacterPattern.C
                        Case "]"c, "}"c : Me._customBitPattern = CharacterPattern.RBracket
                        Case "_"c : Me._customBitPattern = CharacterPattern.Underscore
                        Case "≡"c : Me._customBitPattern = CharacterPattern.Identical
                        Case "¬"c : Me._customBitPattern = CharacterPattern.Not
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
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt ein benutzerdefiniertes Bitmuster fest, das in den sieben Segmenten angezeigt werden soll.")>
        Public Property CustomBitPattern As Integer
            Get
                Return Me._customBitPattern
            End Get
            Set(value As Integer)
                Me._customBitPattern = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob die Dezimalpunkt-LED angezeigt wird.
        ''' </summary>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt an, ob die Dezimalpunkt-LED angezeigt wird.")>
        Public Property ShowDecimalPoint As Boolean
            Get
                Return Me._showDecimalPoint
            End Get
            Set(value As Boolean)
                Me._showDecimalPoint = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob die Dezimalpunkt-LED aktiv ist.
        ''' </summary>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt an, ob die Dezimalpunkt-LED aktiv ist.")>
        Public Property DecimalPointActive As Boolean
            Get
                Return Me._decimalPointActive
            End Get
            Set(value As Boolean)
                Me._decimalPointActive = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob die Doppelpunkt-LEDs angezeigt werden.
        ''' </summary>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt an, ob die Doppelpunkt-LEDs angezeigt werden.")>
        Public Property ShowColon As Boolean
            Get
                Return Me._showColon
            End Get
            Set(value As Boolean)
                Me._showColon = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob die Doppelpunkt-LEDs aktiv sind.
        ''' </summary>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt an, ob die Doppelpunkt-LEDs aktiv sind.")>
        Public Property ColonActive As Boolean
            Get
                Return Me._colonActive
            End Get
            Set(value As Boolean)
                Me._colonActive = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Hintergrundfarbe des Controls fest oder gibt diese zurück.
        ''' </summary>
        ''' <returns></returns>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Hintergrundfarbe des Controls fest oder gibt diese zurück.")>
        Public Overrides Property BackColor As System.Drawing.Color
            Get
                Return Me._backgroundColor
            End Get
            Set(value As System.Drawing.Color)
                Me._backgroundColor = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Vordergrundfarbe der Segmente des Controls fest oder gibt diese zurück.
        ''' </summary>
        ''' <returns></returns>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Vordergrundfarbe der Segmente des Controls fest oder gibt diese zurück.")>
        Public Overrides Property ForeColor As System.Drawing.Color
            Get
                Return Me._foreColor
            End Get
            Set(value As System.Drawing.Color)
                Me._foreColor = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' ausgeblendet da nicht relevant.
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
        ''' ausgeblendet da nicht relevant.
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
        ''' ausgeblendet da nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property Font As System.Drawing.Font
            Get
                Return MyBase.Font
            End Get
            Set(value As System.Drawing.Font)
                MyBase.Font = value
            End Set
        End Property

        ''' <summary>
        ''' ausgeblendet da nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
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
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property RightToLeft As System.Windows.Forms.RightToLeft
            Get
                Return MyBase.RightToLeft
            End Get
            Set(value As System.Windows.Forms.RightToLeft)
                MyBase.RightToLeft = value
            End Set
        End Property

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Wird ausgeführt wenn eine neue Instanz dieses Controls erstellt wird.
        ''' </summary>
        Public Sub New()
            Me.SuspendLayout()
            Me.Name = "SevSegSingleDigit"
            Me.Size = New System.Drawing.Size(32, 64)
            Me.ResumeLayout(False)
            Me.TabStop = False
            Me.Padding = New System.Windows.Forms.Padding(10, 4, 10, 4)
            MyBase.DoubleBuffered = True
            Me._SegmentPoints = New System.Drawing.Point(6)() {}
            For i = 0 To 6
                Me._SegmentPoints(i) = New System.Drawing.Point(5) {}
            Next
            Me.CalculatePoints(Me._SegmentPoints, Me._DigitHeight, Me._DigitWidth, Me._SegmentWidth)
        End Sub

#End Region

#Region "interne Methoden"

        ''' <summary>
        ''' Tritt ein, wenn das Steuerelement neu gezeichnet wird.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub SevSegsingleDigit_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
            Dim useValue = Me._customBitPattern
            Dim brushLight As System.Drawing.Brush = New System.Drawing.SolidBrush(Me._foreColor)
            Dim brushDark As System.Drawing.Brush = New System.Drawing.SolidBrush(Me._inactiveColor)
            'Definiert die Transformation für den Container ...
            Dim srcRect As System.Drawing.RectangleF
            Dim colonWidth As Integer = CInt(Me._digitWidth / 4)
            srcRect = If(Me._showColon,
                New System.Drawing.RectangleF(0.0F, 0.0F, Me._digitWidth + colonWidth, Me._digitHeight),
                New System.Drawing.RectangleF(0.0F, 0.0F, Me._digitWidth, Me._digitHeight))
            Dim destRect As New System.Drawing.RectangleF(Me.Padding.Left, Me.Padding.Top, Me.Width - Me.Padding.Left - Me.Padding.Right, Me.Height - Me.Padding.Top - Me.Padding.Bottom)
            'Grafikcontainer, der die Koordinaten neu zuordnet
            Dim containerState = e.Graphics.BeginContainer(destRect, srcRect, System.Drawing.GraphicsUnit.Pixel)
            Dim trans As New System.Drawing.Drawing2D.Matrix()
            trans.Shear(Me._italicFactor, 0.0F)
            e.Graphics.Transform = trans
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default
            'Segmente zeichnen
            Me.PaintSegments(e, useValue, brushLight, brushDark, Me._segmentPoints)
            If Me._showDecimalPoint Then
                e.Graphics.FillEllipse(If(Me._decimalPointActive, brushLight, brushDark), Me._digitWidth - 1, Me._digitHeight - Me._segmentWidth + 1, Me._segmentWidth, Me._segmentWidth)
            End If
            If Me._showColon Then
                e.Graphics.FillEllipse(If(Me._colonActive, brushLight, brushDark), Me._digitWidth + colonWidth - 4, CInt((Me._digitHeight / 4) - Me._segmentWidth + 8), Me._segmentWidth, Me._segmentWidth)
                e.Graphics.FillEllipse(If(Me._colonActive, brushLight, brushDark), Me._digitWidth + colonWidth - 4, CInt((Me._digitHeight * 3 / 4) - Me._segmentWidth + 4), Me._segmentWidth, Me._segmentWidth)
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
        Private Sub PaintSegments(e As System.Windows.Forms.PaintEventArgs, BitPattern As Integer, BrushLight As System.Drawing.Brush, BrushDark As System.Drawing.Brush, ByRef SegmentPoints As System.Drawing.Point()())
            e.Graphics.FillPolygon(If((BitPattern And &H1) = &H1, BrushLight, BrushDark), SegmentPoints(0))
            e.Graphics.FillPolygon(If((BitPattern And &H2) = &H2, BrushLight, BrushDark), SegmentPoints(1))
            e.Graphics.FillPolygon(If((BitPattern And &H4) = &H4, BrushLight, BrushDark), SegmentPoints(2))
            e.Graphics.FillPolygon(If((BitPattern And &H8) = &H8, BrushLight, BrushDark), SegmentPoints(3))
            e.Graphics.FillPolygon(If((BitPattern And &H10) = &H10, BrushLight, BrushDark), SegmentPoints(4))
            e.Graphics.FillPolygon(If((BitPattern And &H20) = &H20, BrushLight, BrushDark), SegmentPoints(5))
            e.Graphics.FillPolygon(If((BitPattern And &H40) = &H40, BrushLight, BrushDark), SegmentPoints(6))
        End Sub


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
        Private Sub CalculatePoints(ByRef SegmentCornerPoints As System.Drawing.Point()(), DigitHeight As Integer, DigitWidth As Integer, SegmentWidth As Integer)
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

        ''' <summary>
        ''' Tritt beim Ändern der Größe des Steuerelements ein.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub SevSegSingleDigit_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
            Me.Invalidate()
        End Sub

#End Region

#Region "überschriebene Methoden"


        ''' <summary>
        ''' Löst das PaddingChanged-Ereignis aus.
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overrides Sub OnPaddingChanged(e As System.EventArgs)
            MyBase.OnPaddingChanged(e)
            Me.Invalidate()
        End Sub


        ''' <summary>
        ''' Zeichnet den Hintergrund des Steuerelements.
        ''' </summary>
        ''' <param name="e"></param>
        Protected Overrides Sub OnPaintBackground(e As System.Windows.Forms.PaintEventArgs)
            'MyBase.OnPaintBackground(e)
            e.Graphics.Clear(Me._backgroundColor)
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
            Me.SuspendLayout()
            Me.ResumeLayout(False)
        End Sub

#End Region

    End Class

End Namespace