' *************************************************************************************************
' SingleDigit.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace SevenSegmentControl

    ''' <summary>
    ''' <para>Dieses Steuerelement stellt ein einzelnes Siebensegment-LED-Display dar,
    ''' </para>
    ''' <para>das eine Ziffer oder einen Buchstaben anzeigt.</para>
    ''' </summary>
    ''' <example>
    ''' <code><![CDATA[
    ''' ' Beispiel: Verwendung von SingleDigit in einem Formular
    ''' Dim digit As New SevenSegmentControl.SingleDigit()
    ''' digit.Dock = DockStyle.None
    ''' digit.Size = New Size(48, 96)
    ''' digit.ForeColor = Color.Red
    ''' digit.InactiveColor = Color.Gray
    ''' digit.BackColor = Color.Black
    ''' digit.SegmentWidth = 8
    ''' digit.ItalicFactor = -0.1F
    ''' digit.ShowDecimalPoint = True
    ''' digit.DecimalPointActive = True
    ''' digit.ShowColon = False
    ''' digit.DigitValue = "A"
    ''' Me.Controls.Add(digit)
    ''' ]]></code>
    ''' </example>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Dieses Steuerelement stellt ein einzelnes Siebensegment-LED-Display dar, das eine Ziffer oder einen Buchstaben anzeigt.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(SevenSegmentControl.SingleDigit), "SingleDigit.bmp")>
    Public Class SingleDigit : Inherits System.Windows.Forms.Control

#Region "Variablen"

        Private ReadOnly _SegmentPoints As System.Drawing.Point()()  ' Sammlung der Eckpunkte für jedes der 7 Segmente (jedes Segment als Polygon mit 6 Punkten).
        Private ReadOnly _DigitHeight As Integer = 80 ' Interne, fixe Höhe (virtuell) der Ziffer für die Berechnung der Segmentkoordinaten.
        Private ReadOnly _DigitWidth As Integer = 48 ' Interne, fixe Breite (virtuell) der Ziffer für die Berechnung der Segmentkoordinaten.
        Private _SegmentWidth As Integer = 10 ' Aktuelle Segmentbreite (Dicke der LED-Balken) in Pixeln.
        Private _ItalicFactor As Single = -0.1F ' Scherfaktor zur Erzeugung einer kursiven Darstellung (negativ neigt nach links).
        Private _BackgroundColor As System.Drawing.Color = System.Drawing.Color.LightGray ' Zwischengespeicherte Hintergrundfarbe des Steuerelements.
        Private _InactiveColor As System.Drawing.Color = System.Drawing.Color.DarkGray ' Farbe für inaktive (nicht leuchtende) Segmente.
        Private _ForeColor As System.Drawing.Color = System.Drawing.Color.DarkGreen  ' Vordergrundfarbe für aktive (leuchtende) Segmente.
        Private _DigitValue As String = Nothing ' Zu darstellender Zeichenwert (Ziffer/Buchstabe/Sonderzeichen) als String.
        Private _ShowDecimalPoint As Boolean = True ' Steuert, ob der Dezimalpunkt gezeichnet wird.
        Private _DecimalPointActive As Boolean = False ' Status des Dezimalpunktes (aktiv = leuchtend).
        Private _ShowColon As Boolean = False ' Steuert, ob der Doppelpunkt (zwei Punkte) gezeichnet wird.
        Private _ColonActive As Boolean = False  ' Status des Doppelpunkts (aktiv = beide Punkte leuchten).
        Private _CustomBitPattern As Integer = 0 ' Bitmaske für die 7 Segmente (Bit0..Bit6); ermöglicht benutzerdefinierte Muster.

#End Region

#Region "Aufzählungen"

        ' Dies sind die verschiedenen Bitmuster, die die Zeichen darstellen, die in den sieben Segmenten angezeigt werden können.
        ' Die Bits 0 bis 6 entsprechen den einzelnen LEDs, von oben nach unten!
        Friend Enum CharacterPattern

            None = &H0 ' Kein Segment aktiv (alles aus).
            Zero = &H77 ' Darstellung der Ziffer 0.
            One = &H24 ' Darstellung der Ziffer 1.
            Two = &H5D  ' Darstellung der Ziffer 2.
            Three = &H6D ' Darstellung der Ziffer 3.
            Four = &H2E ' Darstellung der Ziffer 4.
            Five = &H6B ' Darstellung der Ziffer 5.
            Six = &H7B ' Darstellung der Ziffer 6.
            Seven = &H25  ' Darstellung der Ziffer 7.
            Eight = &H7F  ' Darstellung der Ziffer 8 (alle Segmente an).
            Nine = &H6F  ' Darstellung der Ziffer 9.
            A = &H3F  ' Großbuchstabe A.
            B = &H7A ' Großbuchstabe B.
            C = &H53
            cField = &H58 ' Kleinbuchstabe c (abgekürzte Form / Feldbezeichnung).
            D = &H7C ' Großbuchstabe D.
            E = &H5B ' Großbuchstabe E.
            F = &H1B ' Großbuchstabe F.
            G = &H73 ' Großbuchstabe G.
            H = &H3E ' Großbuchstabe H.
            hField = &H3A ' Kleinbuchstabe h (abgekürzte Form / Feldbezeichnung).
            i = &H20 ' Kleinbuchstabe i.
            J = &H74 ' Großbuchstabe J.
            L = &H52 ' Großbuchstabe L.
            N = &H38 ' Großbuchstabe N.
            o = &H78 ' Kleinbuchstabe o.
            P = &H1F ' Großbuchstabe P.
            Q = &H2F ' Großbuchstabe Q.
            R = &H18 ' Großbuchstabe R.
            T = &H5A ' Großbuchstabe T.
            U = &H76 ' Großbuchstabe U.
            uField = &H70 ' Kleinbuchstabe u (abgekürzte Form / Feldbezeichnung).
            Y = &H6E ' Großbuchstabe Y.
            Dash = &H8 ' Bindestrich / Minuszeichen.
            Equals = &H48 ' Gleichheitszeichen (=).
            Degrees = &HF ' Gradzeichen (°).
            Apostrophe = &H2 ' Apostroph (').
            Quote = &H6 ' Anführungszeichen (").
            RBracket = &H65 ' Rechte Klammer (]).
            Underscore = &H40 ' Unterstrich (_).
            Identical = &H49 ' Identisch-Zeichen (≡).
            [Not] = &H28 ' Logisches NOT-Zeichen (¬).

        End Enum

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Legt die Farbe inaktiver Segmente fest oder gibt diese zurück.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[
        ''' ' Inaktive Segmente dunkelgrau anzeigen
        ''' digit.InactiveColor = Color.DarkGray
        ''' ]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Farbe inaktiver Segmente fest oder gibt diese zurück.")>
        Public Property InactiveColor As System.Drawing.Color
            Get
                Return Me._InactiveColor
            End Get
            Set(value As System.Drawing.Color)
                Me._InactiveColor = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Breite der LED-Segmente fest oder gibt diese zurück.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[
        ''' ' Segmentbreite auf 12 Pixel setzen
        ''' digit.SegmentWidth = 12
        ''' ]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Breite der LED-Segmente fest oder gibt diese zurück.")>
        Public Property SegmentWidth As Integer
            Get
                Return Me._SegmentWidth
            End Get
            Set(value As Integer)
                Me._SegmentWidth = value
                Me.CalculatePoints(Me._SegmentPoints, Me._DigitHeight, Me._DigitWidth, Me._SegmentWidth)
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Scherkoeffizient für die Kursivschrift der Anzeige.
        ''' </summary>
        ''' <remarks>
        ''' Standardwert ist -0,1.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[
        ''' ' Anzeige leicht nach links geneigt darstellen
        ''' digit.ItalicFactor = -0.15F
        ''' ]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Scherkoeffizient für die Kursivschrift der Anzeige.")>
        Public Property ItalicFactor As Single
            Get
                Return Me._ItalicFactor
            End Get
            Set(value As Single)
                Me._ItalicFactor = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Legt das anzuzeigende Zeichen fest oder gibt dieses zurück.
        ''' </summary>
        ''' <remarks>
        ''' Unterstützte Zeichen sind Ziffern und die meisten Buchstaben.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[
        ''' ' Ziffer 7 anzeigen
        ''' digit.DigitValue = "7"
        ''' ' Buchstabe A anzeigen
        ''' digit.DigitValue = "A"
        ''' ]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt das anzuzeigende Zeichen fest oder gibt dieses zurück.")>
        Public Property DigitValue As String
            Get
                Return Me._DigitValue
            End Get
            Set(value As String)
                Me._CustomBitPattern = 0
                Me._DigitValue = value
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
                        Case 0 : Me._CustomBitPattern = CharacterPattern.Zero
                        Case 1 : Me._CustomBitPattern = CharacterPattern.One
                        Case 2 : Me._CustomBitPattern = CharacterPattern.Two
                        Case 3 : Me._CustomBitPattern = CharacterPattern.Three
                        Case 4 : Me._CustomBitPattern = CharacterPattern.Four
                        Case 5 : Me._CustomBitPattern = CharacterPattern.Five
                        Case 6 : Me._CustomBitPattern = CharacterPattern.Six
                        Case 7 : Me._CustomBitPattern = CharacterPattern.Seven
                        Case 8 : Me._CustomBitPattern = CharacterPattern.Eight
                        Case 9 : Me._CustomBitPattern = CharacterPattern.Nine
                        Case 8 : Me._CustomBitPattern = CharacterPattern.Eight
                        Case 9 : Me._CustomBitPattern = CharacterPattern.Nine
                    End Select
                Else
                    'ist es ein Buchstabe?
                    Select Case value(0)
                        Case "A"c, "a"c : Me._CustomBitPattern = CharacterPattern.A
                        Case "B"c, "b"c : Me._CustomBitPattern = CharacterPattern.B
                        Case "C"c : Me._CustomBitPattern = CharacterPattern.C
                        Case "c"c : Me._CustomBitPattern = CharacterPattern.cField
                        Case "D"c, "d"c : Me._CustomBitPattern = CharacterPattern.D
                        Case "E"c, "e"c : Me._CustomBitPattern = CharacterPattern.E
                        Case "F"c, "f"c : Me._CustomBitPattern = CharacterPattern.F
                        Case "G"c, "g"c : Me._CustomBitPattern = CharacterPattern.G
                        Case "H"c : Me._CustomBitPattern = CharacterPattern.H
                        Case "h"c : Me._CustomBitPattern = CharacterPattern.hField
                        Case "I"c : Me._CustomBitPattern = CharacterPattern.One
                        Case "i"c : Me._CustomBitPattern = CharacterPattern.i
                        Case "J"c, "j"c : Me._CustomBitPattern = CharacterPattern.J
                        Case "L"c, "l"c : Me._CustomBitPattern = CharacterPattern.L
                        Case "N"c, "n"c : Me._CustomBitPattern = CharacterPattern.N
                        Case "O"c : Me._CustomBitPattern = CharacterPattern.Zero
                        Case "o"c : Me._CustomBitPattern = CharacterPattern.o
                        Case "P"c, "p"c : Me._CustomBitPattern = CharacterPattern.P
                        Case "Q"c, "q"c : Me._CustomBitPattern = CharacterPattern.Q
                        Case "R"c, "r"c : Me._CustomBitPattern = CharacterPattern.R
                        Case "S"c, "s"c : Me._CustomBitPattern = CharacterPattern.Five
                        Case "T"c, "t"c : Me._CustomBitPattern = CharacterPattern.T
                        Case "U"c : Me._CustomBitPattern = CharacterPattern.U
                        Case "u"c, "µ"c, "μ"c : Me._CustomBitPattern = CharacterPattern.uField
                        Case "Y"c, "y"c : Me._CustomBitPattern = CharacterPattern.Y
                        Case "-"c : Me._CustomBitPattern = CharacterPattern.Dash
                        Case "="c : Me._CustomBitPattern = CharacterPattern.Equals
                        Case "°"c : Me._CustomBitPattern = CharacterPattern.Degrees
                        Case "'"c : Me._CustomBitPattern = CharacterPattern.Apostrophe
                        Case """"c : Me._CustomBitPattern = CharacterPattern.Quote
                        Case "["c, "{"c : Me._CustomBitPattern = CharacterPattern.C
                        Case "]"c, "}"c : Me._CustomBitPattern = CharacterPattern.RBracket
                        Case "_"c : Me._CustomBitPattern = CharacterPattern.Underscore
                        Case "≡"c : Me._CustomBitPattern = CharacterPattern.Identical
                        Case "¬"c : Me._CustomBitPattern = CharacterPattern.Not
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
        ''' <example>
        ''' <code><![CDATA[
        ''' ' Benutzerdefiniertes Muster setzen (alle Segmente an)
        ''' digit.CustomBitPattern = &H7F
        ''' ]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt ein benutzerdefiniertes Bitmuster fest, das in den sieben Segmenten angezeigt werden soll.")>
        Public Property CustomBitPattern As Integer
            Get
                Return Me._CustomBitPattern
            End Get
            Set(value As Integer)
                Me._CustomBitPattern = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob die Dezimalpunkt-LED angezeigt wird.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[
        ''' ' Dezimalpunkt sichtbar machen
        ''' digit.ShowDecimalPoint = True
        ''' ]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt an, ob die Dezimalpunkt-LED angezeigt wird.")>
        Public Property ShowDecimalPoint As Boolean
            Get
                Return Me._ShowDecimalPoint
            End Get
            Set(value As Boolean)
                Me._ShowDecimalPoint = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob die Dezimalpunkt-LED aktiv ist.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[
        ''' ' Dezimalpunkt einschalten
        ''' digit.DecimalPointActive = True
        ''' ]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt an, ob die Dezimalpunkt-LED aktiv ist.")>
        Public Property DecimalPointActive As Boolean
            Get
                Return Me._DecimalPointActive
            End Get
            Set(value As Boolean)
                Me._DecimalPointActive = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob die Doppelpunkt-LEDs angezeigt werden.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[
        ''' ' Doppelpunkt anzeigen
        ''' digit.ShowColon = True
        ''' ]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt an, ob die Doppelpunkt-LEDs angezeigt werden.")>
        Public Property ShowColon As Boolean
            Get
                Return Me._ShowColon
            End Get
            Set(value As Boolean)
                Me._ShowColon = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob die Doppelpunkt-LEDs aktiv sind.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[
        ''' ' Doppelpunkt einschalten
        ''' digit.ColonActive = True
        ''' ]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt an, ob die Doppelpunkt-LEDs aktiv sind.")>
        Public Property ColonActive As Boolean
            Get
                Return Me._ColonActive
            End Get
            Set(value As Boolean)
                Me._ColonActive = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Hintergrundfarbe des Controls fest oder gibt diese zurück.
        ''' </summary>
        ''' <returns>Aktuelle Hintergrundfarbe.</returns>
        ''' <example>
        ''' <code><![CDATA[
        ''' ' Hintergrundfarbe auf Schwarz setzen
        ''' digit.BackColor = Color.Black
        ''' ]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Hintergrundfarbe des Controls fest oder gibt diese zurück.")>
        Public Overrides Property BackColor As System.Drawing.Color
            Get
                Return Me._BackgroundColor
            End Get
            Set(value As System.Drawing.Color)
                Me._BackgroundColor = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Vordergrundfarbe der Segmente des Controls fest oder gibt diese zurück.
        ''' </summary>
        ''' <returns>Aktuelle Segment-Vordergrundfarbe.</returns>
        ''' <example>
        ''' <code><![CDATA[
        ''' ' Aktive Segmentfarbe auf Grün setzen
        ''' digit.ForeColor = Color.Green
        ''' ]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Vordergrundfarbe der Segmente des Controls fest oder gibt diese zurück.")>
        Public Overrides Property ForeColor As System.Drawing.Color
            Get
                Return Me._ForeColor
            End Get
            Set(value As System.Drawing.Color)
                Me._ForeColor = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da nicht relevant für die Funktion der Anzeige.
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
        ''' Ausgeblendet da nicht relevant für die Funktion der Anzeige.
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
        ''' Ausgeblendet da nicht relevant für die Funktion der Anzeige.
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
        ''' Ausgeblendet da nicht relevant für die Funktion der Anzeige.
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
        ''' Ausgeblendet da nicht relevant für die Funktion der Anzeige.
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
        ''' Initialisiert eine neue Instanz der <see cref="SingleDigit"/>-Klasse.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[
        ''' ' Neues SingleDigit-Control erzeugen und konfigurieren
        ''' Dim digit As New SevenSegmentControl.SingleDigit()
        ''' digit.DigitValue = "9"
        ''' digit.ShowDecimalPoint = True
        ''' digit.DecimalPointActive = False
        ''' digit.ShowColon = True
        ''' digit.ColonActive = True
        ''' ]]></code>
        ''' </example>
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

        ''' <summary>
        ''' <para>Gibt nicht verwaltete Ressourcen frei und führt weitere
        ''' Bereinigungsvorgänge durch, </para>
        ''' <para>bevor <see cref="SchlumpfSoft.Controls.SevenSegmentControl.SingleDigit"/>
        ''' durch die Garbage Collection zurückgefordert wird.</para>
        ''' </summary>
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

#End Region

#Region "interne Methoden"

        ' Tritt ein, wenn das Steuerelement neu gezeichnet wird.
        Private Sub SevSegsingleDigit_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
            Dim useValue = Me._CustomBitPattern
            Dim brushLight As System.Drawing.Brush = New System.Drawing.SolidBrush(Me._ForeColor)
            Dim brushDark As System.Drawing.Brush = New System.Drawing.SolidBrush(Me._InactiveColor)
            'Definiert die Transformation für den Container ...
            Dim srcRect As System.Drawing.RectangleF
            Dim colonWidth As Integer = CInt(Me._DigitWidth / 4)
            srcRect = If(Me._ShowColon,
                New System.Drawing.RectangleF(0.0F, 0.0F, Me._DigitWidth + colonWidth, Me._DigitHeight),
                New System.Drawing.RectangleF(0.0F, 0.0F, Me._DigitWidth, Me._DigitHeight))
            Dim destRect As New System.Drawing.RectangleF(Me.Padding.Left, Me.Padding.Top, Me.Width - Me.Padding.Left - Me.Padding.Right, Me.Height - Me.Padding.Top - Me.Padding.Bottom)
            'Grafikcontainer, der die Koordinaten neu zuordnet
            Dim containerState = e.Graphics.BeginContainer(destRect, srcRect, System.Drawing.GraphicsUnit.Pixel)
            Dim trans As New System.Drawing.Drawing2D.Matrix()
            trans.Shear(Me._ItalicFactor, 0.0F)
            e.Graphics.Transform = trans
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default
            'Segmente zeichnen
            Me.PaintSegments(e, useValue, brushLight, brushDark, Me._SegmentPoints)
            If Me._ShowDecimalPoint Then
                e.Graphics.FillEllipse(If(Me._DecimalPointActive, brushLight, brushDark), Me._DigitWidth - 1, Me._DigitHeight - Me._SegmentWidth + 1, Me._SegmentWidth, Me._SegmentWidth)
            End If
            If Me._ShowColon Then
                e.Graphics.FillEllipse(If(Me._ColonActive, brushLight, brushDark), Me._DigitWidth + colonWidth - 4, CInt((Me._DigitHeight / 4) - Me._SegmentWidth + 8), Me._SegmentWidth, Me._SegmentWidth)
                e.Graphics.FillEllipse(If(Me._ColonActive, brushLight, brushDark), Me._DigitWidth + colonWidth - 4, CInt((Me._DigitHeight * 3 / 4) - Me._SegmentWidth + 4), Me._SegmentWidth, Me._SegmentWidth)
            End If
            e.Graphics.EndContainer(containerState)
        End Sub

        ' Zeichnet die Segmente basierend auf den gesetzten Bits im Bitmuster.
        ' e: Paint-Ereignisargumente mit Grafik.
        ' BitPattern: Bitmaske (Bit0..Bit6) für die 7 Segmente.
        ' BrushLight: Pinsel für aktive Segmente.
        ' BrushDark: Pinsel für inaktive Segmente.
        ' SegmentPoints: Polygonpunkte je Segment.
        Private Sub PaintSegments(e As System.Windows.Forms.PaintEventArgs, BitPattern As Integer, BrushLight As System.Drawing.Brush, BrushDark As System.Drawing.Brush, ByRef SegmentPoints As System.Drawing.Point()())
            e.Graphics.FillPolygon(If((BitPattern And &H1) = &H1, BrushLight, BrushDark), SegmentPoints(0))
            e.Graphics.FillPolygon(If((BitPattern And &H2) = &H2, BrushLight, BrushDark), SegmentPoints(1))
            e.Graphics.FillPolygon(If((BitPattern And &H4) = &H4, BrushLight, BrushDark), SegmentPoints(2))
            e.Graphics.FillPolygon(If((BitPattern And &H8) = &H8, BrushLight, BrushDark), SegmentPoints(3))
            e.Graphics.FillPolygon(If((BitPattern And &H10) = &H10, BrushLight, BrushDark), SegmentPoints(4))
            e.Graphics.FillPolygon(If((BitPattern And &H20) = &H20, BrushLight, BrushDark), SegmentPoints(5))
            e.Graphics.FillPolygon(If((BitPattern And &H40) = &H40, BrushLight, BrushDark), SegmentPoints(6))
        End Sub

        ' Berechnet die Polygonpunkte für alle sieben Segmente bei Initialisierung oder Segmentbreitenänderung.
        ' SegmentCornerPoints: Zielarray mit Punktdaten für die Segmente.
        ' DigitHeight: Virtuelle Höhe der Ziffer.
        ' DigitWidth: Virtuelle Breite der Ziffer.
        ' SegmentWidth: Breite (Dicke) eines Segments.
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

        ' Tritt beim Ändern der Größe des Steuerelements ein und invalidiert die Anzeige.
        Private Sub SevSegSingleDigit_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
            Me.Invalidate()
        End Sub

        ' Löst das PaddingChanged-Ereignis aus und invalidiert die Anzeige.
        Protected Overrides Sub OnPaddingChanged(e As System.EventArgs)
            MyBase.OnPaddingChanged(e)
            Me.Invalidate()
        End Sub

        ' Zeichnet den Hintergrund des Steuerelements (überschreibt Standardverhalten).
        Protected Overrides Sub OnPaintBackground(e As System.Windows.Forms.PaintEventArgs)
            'MyBase.OnPaintBackground(e)
            e.Graphics.Clear(Me._BackgroundColor)
        End Sub

        ' Initialisiert Komponenten des Steuerelements (Designer-Unterstützung).
        Private Sub InitializeComponent()
            Me.SuspendLayout()
            Me.ResumeLayout(False)
        End Sub

#End Region

    End Class

End Namespace