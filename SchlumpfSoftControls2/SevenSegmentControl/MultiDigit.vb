' *************************************************************************************************
' MultiDigit.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace SevenSegmentControl

    ''' <summary>
    ''' Stellt ein Control dar, das mehrere Siebensegmentanzeigen enthält.
    ''' </summary>
    ''' <example>
    ''' <code><![CDATA[' Beispiel: MultiDigit verwenden
    ''' Dim display As New SevenSegmentControl.MultiDigit()
    ''' display.DigitCount = 6
    ''' display.SegmentWidth = 12
    ''' display.InactiveColor = System.Drawing.Color.DimGray
    ''' display.ForeColor = System.Drawing.Color.LimeGreen
    ''' display.ShowDecimalPoint = True
    ''' display.Value = "12.34"
    ''' display.Dock = System.Windows.Forms.DockStyle.Top
    ''' Me.Controls.Add(display)]]></code>
    ''' </example>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("ClassDescriptionSevSegMultiDigit")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(SevenSegmentControl.MultiDigit), "MultiDigit.bmp")>
    Public Class MultiDigit : Inherits System.Windows.Forms.Control

#Region "Variablen"

        Private _Digits As SingleDigit() = Nothing ' Array der untergeordneten Einzelanzeigen (Digits), welche die Zeichen darstellen.
        Private _SegmentWidth As Integer = 10 ' Breite der LED-Segmente in Pixeln.
        Private _ItalicFactor As Single = -0.1F ' Scherkoeffizient zur Simulation von Kursivschrift (negativ = nach links geneigt).
        Private _BackgroundColor As System.Drawing.Color = System.Drawing.Color.LightGray ' Hintergrundfarbe des Controls.
        Private _InactiveColor As System.Drawing.Color = System.Drawing.Color.DarkGray ' Farbe, mit der inaktive Segmente gezeichnet werden.
        Private _ForeColor As System.Drawing.Color = System.Drawing.Color.DarkGreen ' Farbe, mit der aktive Segmente gezeichnet werden.
        Private _ShowDecimalPoint As Boolean = True ' Gibt an, ob der Dezimalpunkt pro Digit sichtbar sein kann.
        Private _DigitPadding As System.Windows.Forms.Padding ' Innenabstand (Padding), der für jedes Digit angewendet wird.
        Private _Value As String = Nothing ' Der aktuell darzustellende Textwert.

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Legt die Farbe inaktiver Segmente fest oder gibt diese zurück.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[' Inaktive Farbe setzen
        ''' display.InactiveColor = System.Drawing.Color.DarkGray]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Farbe inaktiver Segmente fest oder gibt diese zurück.")>
        Public Property InactiveColor As System.Drawing.Color
            Get
                Return Me._InactiveColor
            End Get
            Set(value As System.Drawing.Color)
                Me._InactiveColor = value
                Me.UpdateSegments()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Breite der LED-Segmente fest oder gibt diese zurück.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[' Segmentbreite anpassen
        ''' display.SegmentWidth = 14]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Breite der LED-Segmente fest oder gibt diese zurück.")>
        Public Property SegmentWidth As Integer
            Get
                Return Me._SegmentWidth
            End Get
            Set(value As Integer)
                Me._SegmentWidth = value
                Me.UpdateSegments()
            End Set
        End Property

        ''' <summary>
        ''' Scherkoeffizient für die Kursivschrift der Anzeige.
        ''' </summary>
        ''' <remarks>
        ''' Standardwert ist -0.1
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Kursivfaktor ändern (negativ neigt nach links)
        ''' display.ItalicFactor = -0.2F]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Scherkoeffizient für die Kursivschrift der Anzeige.")>
        Public Property ItalicFactor As Single
            Get
                Return Me._ItalicFactor
            End Get
            Set(value As Single)
                Me._ItalicFactor = value
                Me.UpdateSegments()
            End Set
        End Property

        ''' <summary>
        ''' Gibt an, ob die Dezimalpunkt-LED angezeigt wird.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[' Dezimalpunktanzeige aktivieren/deaktivieren
        ''' display.ShowDecimalPoint = True]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt an, ob die Dezimalpunkt-LED angezeigt wird.")>
        Public Property ShowDecimalPoint As Boolean
            Get
                Return Me._ShowDecimalPoint
            End Get
            Set(value As Boolean)
                Me._ShowDecimalPoint = value
                Me.UpdateSegments()
            End Set
        End Property

        ''' <summary>
        ''' Anzahl der Digits in diesem Control.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[' Anzahl der Stellen setzen
        ''' display.DigitCount = 8]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Anzahl der Digits in diesem Control.")>
        Public Property DigitCount As Integer
            Get
                Return Me._Digits.Length
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
        ''' <example>
        ''' <code><![CDATA[' Innenabstand der Digits festlegen
        ''' display.DigitPadding = New System.Windows.Forms.Padding(8, 4, 8, 4)]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Auffüllung, die für jedes Digit im Control gilt.")>
        Public Property DigitPadding As System.Windows.Forms.Padding
            Get
                Return Me._DigitPadding
            End Get
            Set(value As System.Windows.Forms.Padding)
                Me._DigitPadding = value
                Me.UpdateSegments()
            End Set
        End Property

        ''' <summary>
        ''' Der auf dem Control anzuzeigende Wert.
        ''' </summary>
        ''' <remarks>
        ''' Kann Zahlen, bestimmte Buchstaben und Dezimalpunkte enthalten.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Wert für die Anzeige setzen
        ''' display.Value = "HELLO"
        ''' display.Value = "3.1415"]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Der auf dem Control anzuzeigende Wert.")>
        Public Property Value As String
            Get
                Return Me._Value
            End Get
            Set(value As String)
                Me._Value = value
                For i = 0 To Me._Digits.Length - 1
                    Me._Digits(i).CustomBitPattern = 0
                    Me._Digits(i).DecimalPointActive = False
                Next
                If Not Equals(Me._Value, Nothing) Then
                    Dim segmentIndex = 0
                    For i = Me._Value.Length - 1 To 0 Step -1
                        If segmentIndex >= Me._Digits.Length Then Exit For
                        If Me._Value(i) = "."c Then
                            Me._Digits(segmentIndex).DecimalPointActive = True
                        Else
                            Me._Digits(System.Math.Min(System.Threading.Interlocked.Increment(segmentIndex), segmentIndex - 1)).DigitValue = Me._Value(i).ToString()
                        End If
                    Next
                End If
            End Set
        End Property

        ''' <summary>
        ''' Legt die Hintergrundfarbe des Controls fest oder gibt diese zurück.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[' Hintergrundfarbe setzen
        ''' display.BackColor = System.Drawing.Color.Black]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Hintergrundfarbe des Controls fest oder gibt diese zurück.")>
        Public Overrides Property BackColor As System.Drawing.Color
            Get
                Return Me._BackgroundColor
            End Get
            Set(value As System.Drawing.Color)
                Me._BackgroundColor = value
                Me.UpdateSegments()
            End Set
        End Property

        ''' <summary>
        ''' Legt die Vordergrundfarbe der Segmente des Controls fest oder gibt diese zurück.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[' Vordergrundfarbe setzen
        ''' display.ForeColor = System.Drawing.Color.Red]]></code>
        ''' </example>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt die Vordergrundfarbe der Segmente des Controls fest oder gibt diese zurück.")>
        Public Overrides Property ForeColor As System.Drawing.Color
            Get
                Return Me._ForeColor
            End Get
            Set(value As System.Drawing.Color)
                Me._ForeColor = value
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
        ''' Initialisiert eine neue Instanz von <see
        ''' cref="SchlumpfSoft.Controls.SevenSegmentControl.MultiDigit"/>.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[' Instanz erstellen und konfigurieren
        ''' Dim display As New SevenSegmentControl.MultiDigit()
        ''' display.DigitCount = 4
        ''' display.Value = "1234"
        ''' Me.Controls.Add(display)]]></code>
        ''' </example>
        Public Sub New()
            Me.SuspendLayout()
            Me.Name = "SevSegMultiDigit"
            Me.Size = New System.Drawing.Size(100, 25)
            AddHandler Resize, New System.EventHandler(AddressOf Me.SevSegMultiDigit_Resize)
            Me.ResumeLayout(False)
            Me.TabStop = False
            Me._DigitPadding = New System.Windows.Forms.Padding(10, 4, 10, 4)
            Me.CreateSegments(4)
        End Sub

#End Region

#Region "interne Methoden"

        ' Erstellt die einzelnen Segmente (Digits) basierend auf der angegebenen Anzahl.
        Private Sub CreateSegments(count As Integer)
            If Me._Digits IsNot Nothing Then
                For i = 0 To Me._Digits.Length - 1
                    Me._Digits(i).Parent = Nothing
                    Me._Digits(i).Dispose()
                Next
            End If
            If count <= 0 Then Return
            Me._Digits = New SingleDigit(count - 1) {}
            For i = 0 To count - 1
                Me._Digits(i) = New SingleDigit With {
                    .Parent = Me,
                    .Top = 0,
                    .Height = Me.Height,
                    .Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom,
                    .Visible = True
                }
            Next
            Me.ResizeSegments()
            Me.UpdateSegments()
            Me.Value = Me._Value
        End Sub

        ' Passt die Größe und Position der einzelnen Segmente basierend auf der Gesamtgröße des MultiDigit-Controls an.
        Private Sub ResizeSegments()
            Dim segWidth As Integer = CInt(Me.Width / Me._Digits.Length)
            For i = 0 To Me._Digits.Length - 1
                Me._Digits(i).Left = CInt(Me.Width * (Me._Digits.Length - 1 - i) / Me._Digits.Length)
                Me._Digits(i).Width = segWidth
            Next
        End Sub

        ' Aktualisiert die Eigenschaften aller Segmente basierend auf den aktuellen Einstellungen des MultiDigit-Controls.
        Private Sub UpdateSegments()
            For i = 0 To Me._Digits.Length - 1
                Me._Digits(i).BackColor = Me._BackgroundColor
                Me._Digits(i).InactiveColor = Me._InactiveColor
                Me._Digits(i).ForeColor = Me._ForeColor
                Me._Digits(i).SegmentWidth = Me._SegmentWidth
                Me._Digits(i).ItalicFactor = Me._ItalicFactor
                Me._Digits(i).ShowDecimalPoint = Me._ShowDecimalPoint
                Me._Digits(i).Padding = Me._DigitPadding
            Next
        End Sub

        ' Reagiert auf Größenänderungen des MultiDigit-Controls und ruft ResizeSegments() auf, um die Digits neu zu verteilen.
        Private Sub SevSegMultiDigit_Resize(sender As Object, e As System.EventArgs)
            Me.ResizeSegments()
        End Sub

        ' Zeichnet den Hintergrund des Steuerelements in der festgelegten Hintergrundfarbe.
        Protected Overrides Sub OnPaintBackground(e As System.Windows.Forms.PaintEventArgs)
            e.Graphics.Clear(Me._BackgroundColor)
        End Sub

#End Region

    End Class

End Namespace