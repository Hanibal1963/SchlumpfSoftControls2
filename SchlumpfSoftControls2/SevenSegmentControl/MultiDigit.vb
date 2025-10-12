' *************************************************************************************************
' MultiDigit.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
'Imports SchlumpfSoft.Controls.Attribute

Namespace SevenSegmentControl

    ''' <summary>
    ''' Stellt ein Control dar, das mehrere Siebensegmentanzeigen enthält.
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("ClassDescriptionSevSegMultiDigit")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(SevenSegmentControl.MultiDigit), "MultiDigit.bmp")>
    Public Class MultiDigit : Inherits Control

#Region "Eigenschaftsvariablen"

        Private _digits As SingleDigit() = Nothing
        Private _segmentWidth As Integer = 10
        Private _italicFactor As Single = -0.1F
        Private _backgroundColor As Color = Color.LightGray
        Private _inactiveColor As Color = Color.DarkGray
        Private _foreColor As Color = Color.DarkGreen
        Private _showDecimalPoint As Boolean = True
        Private _digitPadding As Padding
        Private _value As String = Nothing

#End Region

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see cref="SchlumpfSoft.Controls.SevenSegmentControl.MultiDigit"/>.
        ''' </summary>
        Public Sub New()
            SuspendLayout()
            Name = "SevSegMultiDigit"
            Size = New Size(100, 25)
            AddHandler Resize, New EventHandler(AddressOf SevSegMultiDigit_Resize)
            ResumeLayout(False)
            TabStop = False
            _digitPadding = New Padding(10, 4, 10, 4)
            CreateSegments(4)
        End Sub

#Region "interne Methoden"

        ''' <summary>
        ''' <para>Ändert die Anzahl der Elemente im LED-Array. </para>
        ''' <para>Dadurch werden die vorherigen Elemente zerstört und an ihrer Stelle neue
        ''' erstellt, </para>
        ''' <para>wobei alle aktuellen Optionen auf die neuen angewendet werden.</para>
        ''' </summary>
        ''' <param name="count">Anzahl der zu erstellenden Elemente.</param>
        Private Sub CreateSegments(count As Integer)
            If _digits IsNot Nothing Then
                For i = 0 To _digits.Length - 1
                    _digits(i).Parent = Nothing
                    _digits(i).Dispose()
                Next
            End If
            If count <= 0 Then Return
            _digits = New SingleDigit(count - 1) {}
            For i = 0 To count - 1
                _digits(i) = New SingleDigit With {
                    .Parent = Me,
                    .Top = 0,
                    .Height = Height,
                    .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom,
                    .Visible = True
                }
            Next
            ResizeSegments()
            UpdateSegments()
            Value = _value
        End Sub

        ''' <summary>
        ''' Richtet die Elemente des Arrays so aus, dass sie genau in die Breite des
        ''' übergeordneten Steuerelements passen.
        ''' </summary>
        Private Sub ResizeSegments()
            Dim segWidth As Integer = CInt(Width / _digits.Length)
            For i = 0 To _digits.Length - 1
                _digits(i).Left = CInt(Width * (_digits.Length - 1 - i) / _digits.Length)
                _digits(i).Width = segWidth
            Next
        End Sub

        ''' <summary>
        ''' Aktualisiert die Eigenschaften jedes Elements mit den Eigenschaften.
        ''' </summary>
        Private Sub UpdateSegments()
            For i = 0 To _digits.Length - 1
                _digits(i).BackColor = _backgroundColor
                _digits(i).InactiveColor = _inactiveColor
                _digits(i).ForeColor = _foreColor
                _digits(i).SegmentWidth = _segmentWidth
                _digits(i).ItalicFactor = _italicFactor
                _digits(i).ShowDecimalPoint = _showDecimalPoint
                _digits(i).Padding = _digitPadding
            Next
        End Sub

        ''' <summary>
        ''' Wird ausgeführt wenn die Größe des Controls geändert wird
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub SevSegMultiDigit_Resize(sender As Object, e As EventArgs)
            ResizeSegments()
        End Sub

#End Region

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
                UpdateSegments()
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
                UpdateSegments()
            End Set
        End Property

        ''' <summary>
        ''' Scherkoeffizient für die Kursivschrift der Anzeige.
        ''' </summary>
        ''' <remarks>
        ''' Standardwert ist -0.1
        ''' </remarks>
        <Category("Appearance")>
        <Description("Scherkoeffizient für die Kursivschrift der Anzeige.")>
        Public Property ItalicFactor As Single
            Get
                Return _italicFactor
            End Get
            Set(value As Single)
                _italicFactor = value
                UpdateSegments()
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
                UpdateSegments()
            End Set
        End Property

        ''' <summary>
        ''' Anzahl der Digits in diesem Control.
        ''' </summary>
        <Category("Appearance")>
        <Description("Anzahl der Digits in diesem Control.")>
        Public Property DigitCount As Integer
            Get
                Return _digits.Length
            End Get
            Set(value As Integer)
                If value > 0 AndAlso value <= 100 Then CreateSegments(value)
            End Set
        End Property

        ''' <summary>
        ''' Auffüllung, die für jedes Digit im Control gilt.
        ''' </summary>
        ''' <remarks>
        ''' Passen Sie diese Zahlen an, um das perfekte Erscheinungsbild für das Control
        ''' Ihrer Größe zu erhalten.
        ''' </remarks>
        <Category("Appearance")>
        <Description("Auffüllung, die für jedes Digit im Control gilt.")>
        Public Property DigitPadding As Padding
            Get
                Return _digitPadding
            End Get
            Set(value As Padding)
                _digitPadding = value
                UpdateSegments()
            End Set
        End Property

        ''' <summary>
        ''' Der auf dem Control anzuzeigende Wert. 
        ''' </summary>
        ''' <remarks>
        ''' Kann Zahlen, bestimmte Buchstaben und Dezimalpunkte enthalten.
        ''' </remarks>
        <Category("Appearance")>
        <Description("Der auf dem Control anzuzeigende Wert.")>
        Public Property Value As String
            Get
                Return _value
            End Get
            Set(value As String)
                _value = value
                For i = 0 To _digits.Length - 1
                    _digits(i).CustomBitPattern = 0
                    _digits(i).DecimalPointActive = False
                Next
                If Not Equals(_value, Nothing) Then
                    Dim segmentIndex = 0
                    For i = _value.Length - 1 To 0 Step -1
                        If segmentIndex >= _digits.Length Then Exit For
                        If _value(i) = "."c Then
                            _digits(segmentIndex).DecimalPointActive = True
                        Else
                            _digits(Math.Min(Threading.Interlocked.Increment(segmentIndex), segmentIndex - 1)).DigitValue = _value(i).ToString()
                        End If
                    Next
                End If
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
                UpdateSegments()
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
                UpdateSegments()
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

#Region "geänderte Methoden"

        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            e.Graphics.Clear(_backgroundColor)
        End Sub

        Private Sub InitializeComponent()
            Me.SuspendLayout()
            Me.ResumeLayout(False)
        End Sub

#End Region

    End Class

End Namespace