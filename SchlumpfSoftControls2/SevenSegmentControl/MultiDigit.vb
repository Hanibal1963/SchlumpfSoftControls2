' *************************************************************************************************
' MultiDigit.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

' TODO: Code noch überarbeiten

Namespace SevenSegmentControl

    ''' <summary>
    ''' Stellt ein Control dar, das mehrere Siebensegmentanzeigen enthält.
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("ClassDescriptionSevSegMultiDigit")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(SevenSegmentControl.MultiDigit), "MultiDigit.bmp")>
    Public Class MultiDigit : Inherits System.Windows.Forms.Control

#Region "Variablendefinition"

        Private _digits As SingleDigit() = Nothing
        Private _segmentWidth As Integer = 10
        Private _italicFactor As Single = -0.1F
        Private _backgroundColor As System.Drawing.Color = System.Drawing.Color.LightGray
        Private _inactiveColor As System.Drawing.Color = System.Drawing.Color.DarkGray
        Private _foreColor As System.Drawing.Color = System.Drawing.Color.DarkGreen
        Private _showDecimalPoint As Boolean = True
        Private _digitPadding As System.Windows.Forms.Padding
        Private _value As String = Nothing

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
                Me.UpdateSegments()
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
                Me.UpdateSegments()
            End Set
        End Property

        ''' <summary>
        ''' Scherkoeffizient für die Kursivschrift der Anzeige.
        ''' </summary>
        ''' <remarks>
        ''' Standardwert ist -0.1
        ''' </remarks>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Scherkoeffizient für die Kursivschrift der Anzeige.")>
        Public Property ItalicFactor As Single
            Get
                Return Me._italicFactor
            End Get
            Set(value As Single)
                Me._italicFactor = value
                Me.UpdateSegments()
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
                Me.UpdateSegments()
            End Set
        End Property

        ''' <summary>
        ''' Anzahl der Digits in diesem Control.
        ''' </summary>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Anzahl der Digits in diesem Control.")>
        Public Property DigitCount As Integer
            Get
                Return Me._digits.Length
            End Get
            Set(value As Integer)
                If value > 0 AndAlso value <= 100 Then Me.CreateSegments(value)
            End Set
        End Property

        ''' <summary>
        ''' Auffüllung, die für jedes Digit im Control gilt.
        ''' </summary>
        ''' <remarks>
        ''' Passen Sie diese Zahlen an, um das perfekte Erscheinungsbild für das Control
        ''' Ihrer Größe zu erhalten.
        ''' </remarks>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Auffüllung, die für jedes Digit im Control gilt.")>
        Public Property DigitPadding As System.Windows.Forms.Padding
            Get
                Return Me._digitPadding
            End Get
            Set(value As System.Windows.Forms.Padding)
                Me._digitPadding = value
                Me.UpdateSegments()
            End Set
        End Property

        ''' <summary>
        ''' Der auf dem Control anzuzeigende Wert. 
        ''' </summary>
        ''' <remarks>
        ''' Kann Zahlen, bestimmte Buchstaben und Dezimalpunkte enthalten.
        ''' </remarks>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Der auf dem Control anzuzeigende Wert.")>
        Public Property Value As String
            Get
                Return Me._value
            End Get
            Set(value As String)
                Me._value = value
                For i = 0 To Me._digits.Length - 1
                    Me._digits(i).CustomBitPattern = 0
                    Me._digits(i).DecimalPointActive = False
                Next
                If Not Equals(Me._value, Nothing) Then
                    Dim segmentIndex = 0
                    For i = Me._value.Length - 1 To 0 Step -1
                        If segmentIndex >= Me._digits.Length Then Exit For
                        If Me._value(i) = "."c Then
                            Me._digits(segmentIndex).DecimalPointActive = True
                        Else
                            Me._digits(System.Math.Min(System.Threading.Interlocked.Increment(segmentIndex), segmentIndex - 1)).DigitValue = Me._value(i).ToString()
                        End If
                    Next
                End If
            End Set
        End Property

#End Region

#Region "überschriebene Eigenschaften"

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
                Me.UpdateSegments()
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
                Me.UpdateSegments()
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
        ''' Initialisiert eine neue Instanz von <see cref="SchlumpfSoft.Controls.SevenSegmentControl.MultiDigit"/>.
        ''' </summary>
        Public Sub New()
            Me.SuspendLayout()
            Me.Name = "SevSegMultiDigit"
            Me.Size = New System.Drawing.Size(100, 25)
            AddHandler Resize, New System.EventHandler(AddressOf Me.SevSegMultiDigit_Resize)
            Me.ResumeLayout(False)
            Me.TabStop = False
            Me._digitPadding = New System.Windows.Forms.Padding(10, 4, 10, 4)
            Me.CreateSegments(4)
        End Sub

#End Region

#Region "interne Methoden"

        ''' <summary>
        ''' <para>Ändert die Anzahl der Elemente im LED-Array. </para>
        ''' <para>Dadurch werden die vorherigen Elemente zerstört und an ihrer Stelle neue
        ''' erstellt, </para>
        ''' <para>wobei alle aktuellen Optionen auf die neuen angewendet werden.</para>
        ''' </summary>
        ''' <param name="count">Anzahl der zu erstellenden Elemente.</param>
        Private Sub CreateSegments(count As Integer)
            If Me._digits IsNot Nothing Then
                For i = 0 To Me._digits.Length - 1
                    Me._digits(i).Parent = Nothing
                    Me._digits(i).Dispose()
                Next
            End If
            If count <= 0 Then Return
            Me._digits = New SingleDigit(count - 1) {}
            For i = 0 To count - 1
                Me._digits(i) = New SingleDigit With {
                    .Parent = Me,
                    .Top = 0,
                    .Height = Me.Height,
                    .Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom,
                    .Visible = True
                }
            Next
            Me.ResizeSegments()
            Me.UpdateSegments()
            Me.Value = Me._value
        End Sub

        ''' <summary>
        ''' Richtet die Elemente des Arrays so aus, dass sie genau in die Breite des
        ''' übergeordneten Steuerelements passen.
        ''' </summary>
        Private Sub ResizeSegments()
            Dim segWidth As Integer = CInt(Me.Width / Me._digits.Length)
            For i = 0 To Me._digits.Length - 1
                Me._digits(i).Left = CInt(Me.Width * (Me._digits.Length - 1 - i) / Me._digits.Length)
                Me._digits(i).Width = segWidth
            Next
        End Sub

        ''' <summary>
        ''' Aktualisiert die Eigenschaften jedes Elements mit den Eigenschaften.
        ''' </summary>
        Private Sub UpdateSegments()
            For i = 0 To Me._digits.Length - 1
                Me._digits(i).BackColor = Me._backgroundColor
                Me._digits(i).InactiveColor = Me._inactiveColor
                Me._digits(i).ForeColor = Me._foreColor
                Me._digits(i).SegmentWidth = Me._segmentWidth
                Me._digits(i).ItalicFactor = Me._italicFactor
                Me._digits(i).ShowDecimalPoint = Me._showDecimalPoint
                Me._digits(i).Padding = Me._digitPadding
            Next
        End Sub

        ''' <summary>
        ''' Wird ausgeführt wenn die Größe des Controls geändert wird
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub SevSegMultiDigit_Resize(sender As Object, e As System.EventArgs)
            Me.ResizeSegments()
        End Sub

        Private Sub InitializeComponent()
            Me.SuspendLayout()
            Me.ResumeLayout(False)
        End Sub

#End Region

#Region "überschriebene Methoden"

        Protected Overrides Sub OnPaintBackground(e As System.Windows.Forms.PaintEventArgs)
            e.Graphics.Clear(Me._backgroundColor)
        End Sub

#End Region

    End Class

End Namespace