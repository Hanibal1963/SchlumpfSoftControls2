' *************************************************************************************************
' ColorProgressBar.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ColorProgressBarControl

    ''' <summary>
    ''' Ein benutzerdefiniertes Windows Forms-Steuerelement zur Anzeige eines farbigen
    ''' Fortschrittsbalkens mit optionalem Rahmen und Glanzeffekt.
    ''' </summary>
    ''' <remarks>
    ''' Das Steuerelement besteht aus zwei <see
    ''' cref="System.Windows.Forms.Panel"/>-Elementen: <list type="bullet">
    '''  <item>
    '''   <description><c>ProgressFull</c>:<br/>
    ''' Visualisiert den aktuellen Fortschritt.</description>
    '''  </item>
    '''  <item>
    '''   <description><c>ProgressEmpty</c>:<br/>
    ''' Visualisiert den verbleibenden Fortschritt bis zum Maximum.</description>
    '''  </item>
    ''' </list>
    '''  Die Glanzeffekte (<c>GlossLeft</c> und <c>GlossRight</c>) können über <see cref="IsGlossy"/> ein- bzw. ausgeschaltet werden. Der Rahmen wird über <see cref="ShowBorder"/> gesteuert.
    ''' </remarks>
    ''' <example>
    ''' <code language="vb"><![CDATA[' Beispiel: Verwendung in einem Formular
    ''' Dim bar As New ColorProgressBarControl.ColorProgressBar() With {
    '''     .Dock = DockStyle.Top,
    '''     .ProgressMaximumValue = 100,
    '''     .Value = 25,
    '''     .BarColor = System.Drawing.Color.SeaGreen,
    '''     .EmptyColor = System.Drawing.Color.Gainsboro,
    '''     .BorderColor = System.Drawing.Color.Black,
    '''     .ShowBorder = True,
    '''     .IsGlossy = True
    ''' }
    ''' Me.Controls.Add(bar)
    '''  
    ''' ' Fortschritt aktualisieren
    ''' bar.Value = Math.Min(bar.ProgressMaximumValue, bar.Value + 10)]]></code>
    ''' </example>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Control zum Anzeigen eines farbigen Fortschrittbalkens.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(ColorProgressBarControl.ColorProgressBar), "ColorProgressBar.bmp")>
    Public Class ColorProgressBar : Inherits System.Windows.Forms.UserControl

#Region "Variablen"

        Private _ProgressUnit As Integer = 20
        Private _ProgressValue As Integer = 1
        Private _MaxValue As Integer = 10
        Private _ShowBorder As Boolean = True
        Private _IsGlossy As Boolean = True
        Private _BarColor As System.Drawing.Color = System.Drawing.Color.Blue
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0032:Automatisch generierte Eigenschaft verwenden", Justification:="<Ausstehend>")>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Unnötige Unterdrückung entfernen", Justification:="<Ausstehend>")>
        Private _EmptyColor As System.Drawing.Color = System.Drawing.Color.LightGray
        Private _BorderColor As System.Drawing.Color = System.Drawing.Color.Black
        Private WithEvents ProgressFull As System.Windows.Forms.Panel
        Private WithEvents ProgressEmpty As System.Windows.Forms.Panel
        Private WithEvents GlossLeft As System.Windows.Forms.Panel
        Private WithEvents GlossRight As System.Windows.Forms.Panel
        Private ReadOnly components As System.ComponentModel.IContainer

#End Region

#Region "Ereignisse"

        ''' <summary>
        ''' Tritt auf, wenn auf das Steuerelement oder einen seiner sichtbaren Bereiche
        ''' geklickt wird.
        ''' </summary>
        ''' <remarks>
        ''' Dieses Ereignis wird ausgelöst, wenn einer der internen Panels (<c>GlossLeft</c>, <c>GlossRight</c>, <c>ProgressFull</c>, <c>ProgressEmpty</c>) angeklickt wird.<br/>
        ''' Verwenden Sie <see
        ''' href="https://learn.microsoft.com/de-de/dotnet/visual-basic/language-reference/statements/addhandler-statement">AddHandler</see>,
        ''' um eine Klickbehandlung zu registrieren.
        ''' </remarks>
        ''' <example>
        ''' <code language="vb"><![CDATA[AddHandler myProgressBar.Click,
        '''     Sub(s, args)
        '''         System.Windows.Forms.MessageBox.Show("Fortschrittsbalken geklickt.")
        '''     End Sub]]></code>
        ''' </example>
        Public Shadows Event Click(sender As Object, e As System.EventArgs)

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Gibt den aktuellen Fortschrittswert zurück oder legt diesen fest.
        ''' </summary>
        ''' <remarks>
        ''' Beim Setzen wird der Wert automatisch auf <see cref="ProgressMaximumValue"/>
        ''' begrenzt.<br/>
        ''' Das Layout wird anschließend über <see cref="UpdateProgress"/> aktualisiert.
        ''' </remarks>
        ''' <value>
        ''' Ein ganzzahliger Wert im Bereich [0..ProgressMaximumValue].
        ''' </value>
        ''' <example>
        ''' <code language="vb"><![CDATA[progressBar.ProgressMaximumValue = 100
        ''' progressBar.Value = 75 ' Zeigt 75% Fortschritt]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Gibt den Gesamtfortschritt des Fortschrittsbalkens zurück oder legt diesen fest.")>
        <System.ComponentModel.DefaultValue(1)>
        Public Property Value() As Integer
            Get
                Return Me._ProgressValue
            End Get
            Set(value As Integer)
                Me._ProgressValue = If(value <= Me._MaxValue, value, Me._MaxValue) ' Nicht mehr als den Maximalwert erlauben
                Dim unused = Me.UpdateProgress()
            End Set
        End Property

        ''' <summary>
        ''' Gibt den Maximalwert des Fortschrittsbalkens zurück oder legt diesen fest.
        ''' </summary>
        ''' <remarks>
        ''' Der visuelle Fortschritt berechnet sich aus <c>Width/ProgressMaximumValue</c>.<br/>
        ''' Beim Setzen wird der Maximalwert auf die aktuelle Breite des Steuerelements
        ''' begrenzt, um eine sinnvolle Skalierung sicherzustellen.
        ''' </remarks>
        ''' <value>
        ''' Ein ganzzahliger positiver Wert.<br/>
        ''' Der interne Layoutwert wird durch die Steuerelementbreite begrenzt.
        ''' </value>
        ''' <example>
        ''' <code language="vb"><![CDATA[progressBar.ProgressMaximumValue = 50
        ''' For i = 0 To 50
        '''     progressBar.Value = i
        ''' Next]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Gibt den Maximalwert des Fortschrittsbalkens zurück oder legt diesen fest.")>
        <System.ComponentModel.DefaultValue(10)>
        Public Property ProgressMaximumValue() As Integer
            Get
                Return Me._MaxValue
            End Get
            Set(value As Integer)
                Me._MaxValue = If(value > Me.Width, Me.Width, value)
                Dim unused = Me.UpdateProgress()
            End Set
        End Property

        ''' <summary>
        ''' Gibt die Farbe des gefüllten Fortschrittsbereichs zurück oder legt diese fest.
        ''' </summary>
        ''' <remarks>
        ''' Beim Setzen wird die Hintergrundfarbe von <c>ProgressFull</c> entsprechend aktualisiert.
        ''' </remarks>
        ''' <value>
        ''' Eine <see cref="System.Drawing.Color"/> für den gefüllten Bereich.
        ''' </value>
        ''' <example>
        ''' <code language="vb"><![CDATA[progressBar.BarColor = System.Drawing.Color.DarkOrange]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt die Farbe des Fortschrittsbalkens zurück oder legt diese fest.")>
        Public Property BarColor As System.Drawing.Color
            Get
                Return Me._BarColor
            End Get
            Set(value As System.Drawing.Color)
                Me._BarColor = value
                Me.ProgressFull.BackColor = Me._BarColor
            End Set
        End Property

        ''' <summary>
        ''' Gibt die Farbe des leeren Fortschrittsbereichs zurück oder legt diese fest.
        ''' </summary>
        ''' <remarks>
        ''' Beim Setzen wird die Hintergrundfarbe von <c>ProgressEmpty</c> entsprechend aktualisiert.
        ''' </remarks>
        ''' <value>
        ''' Die aktuell eingestellte Farbe für den leeren Bereich.
        ''' </value>
        ''' <example>
        ''' <code language="vb"><![CDATA[progressBar.EmptyColor = System.Drawing.Color.LightSteelBlue]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt die Farbe des leeren Fortschrittsbalkens zurück oder legt diese fest.")>
        Public Property EmptyColor As System.Drawing.Color
            Get
                Return Me._EmptyColor
            End Get
            Set(value As System.Drawing.Color)
                Me._EmptyColor = value
                Me.ProgressEmpty.BackColor = Me._EmptyColor
            End Set
        End Property

        ''' <summary>
        ''' Gibt die Farbe des Rahmens zurück oder legt diese fest.
        ''' </summary>
        ''' <remarks>
        ''' Beim Setzen wird <see cref="System.Windows.Forms.Control.BackColor"/> des
        ''' Steuerelements angepasst.
        ''' </remarks>
        ''' <value>
        ''' Eine <see cref="System.Drawing.Color"/> für den Rahmen des Steuerelements.
        ''' </value>
        ''' <example>
        ''' <code language="vb"><![CDATA[progressBar.BorderColor = System.Drawing.Color.Gray
        ''' progressBar.ShowBorder = True]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt die Farbe des Rahmens zurück oder legt diese fest.")>
        Public Property BorderColor As System.Drawing.Color
            Get
                Return Me._BorderColor
            End Get
            Set(value As System.Drawing.Color)
                Me._BorderColor = value
                Me.BackColor = Me._BorderColor
            End Set
        End Property

        ''' <summary>
        ''' Legt fest, ob ein Rahmen um die Fortschrittsanzeige angezeigt wird.
        ''' </summary>
        ''' <remarks>
        ''' Beim Umschalten wird das <see cref="Padding"/> intern auf 1 (mit Rahmen) bzw. 0
        ''' (ohne Rahmen) gesetzt.
        ''' </remarks>
        ''' <value>
        ''' <c>True</c>, wenn ein Rahmen angezeigt wird; andernfalls <c>False</c>.
        ''' </value>
        ''' <example>
        ''' <code language="vb"><![CDATA[progressBar.ShowBorder = False ' Rand ausblenden]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt an, ob der Rahmen auf der Fortschrittsanzeige aktiviert ist.")>
        <System.ComponentModel.DefaultValue(True)>
        Public Property ShowBorder As Boolean
            Get
                Return Me._ShowBorder
            End Get
            Set(value As Boolean)
                Me._ShowBorder = value
                Dim unused = Me.UpdateProgress()
            End Set
        End Property

        ''' <summary>
        ''' Legt fest, ob ein Glanzeffekt auf der Fortschrittsleiste angezeigt wird.
        ''' </summary>
        ''' <remarks>
        ''' Beim Umschalten werden die Panels <c>GlossLeft</c> und <c>GlossRight</c> sichtbar bzw. unsichtbar.
        ''' </remarks>
        ''' <value>
        ''' <c>True</c>, wenn der Glanzeffekt sichtbar ist; andernfalls <c>False</c>.
        ''' </value>
        ''' <example>
        ''' <code language="vb"><![CDATA[progressBar.IsGlossy = True ' Glanzeffekt aktivieren]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt an, ob der Glanz auf der Fortschrittsleiste angezeigt wird.")>
        <System.ComponentModel.DefaultValue(True)>
        Public Property IsGlossy As Boolean
            Get
                Return Me._IsGlossy
            End Get
            Set(value As Boolean)
                Me._IsGlossy = value
                Dim unused = Me.UpdateProgress()
            End Set
        End Property

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

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz der <see cref="ColorProgressBar"/>-Klasse und
        ''' setzt Standardwerte.
        ''' </summary>
        ''' <remarks>
        ''' <see cref="InitializeComponent"/> erstellt die internen Steuerelemente.<br/>
        ''' <see cref="InitializeDefaults"/> setzt Standardfarben und -zustände.
        ''' </remarks>
        ''' <example>
        ''' <code language="vb"><![CDATA[Dim bar As New ColorProgressBarControl.ColorProgressBar()
        ''' bar.ProgressMaximumValue = 100
        ''' bar.Value = 10]]></code>
        ''' </example>
        Public Sub New()
            Me.InitializeComponent()  ' Dieser Aufruf ist für den Designer erforderlich.
            Me.InitializeDefaults() ' Standardwerte setzen
        End Sub

#End Region

#Region "interne Methoden"

        ''' <summary>
        ''' Vom Designer generierte Methode zur Initialisierung der
        ''' Steuerelement-Komponenten.
        ''' </summary>
        ''' <remarks>
        ''' <b>Achtung!</b><br/>
        ''' Änderungen sollten ausschließlich über den Designer erfolgen.<br/>
        ''' Eine Änderung mit dem Editor kann zu unerwarteten Fehlern führen.
        ''' </remarks>
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.ProgressFull = New System.Windows.Forms.Panel()
            Me.GlossLeft = New System.Windows.Forms.Panel()
            Me.ProgressEmpty = New System.Windows.Forms.Panel()
            Me.GlossRight = New System.Windows.Forms.Panel()
            Me.ProgressFull.SuspendLayout()
            Me.ProgressEmpty.SuspendLayout()
            Me.SuspendLayout()
            '
            'ProgressFull
            '
            Me.ProgressFull.BackColor = System.Drawing.SystemColors.HotTrack
            Me.ProgressFull.Controls.Add(Me.GlossLeft)
            Me.ProgressFull.Dock = System.Windows.Forms.DockStyle.Left
            Me.ProgressFull.Location = New System.Drawing.Point(1, 1)
            Me.ProgressFull.Name = "ProgressFull"
            Me.ProgressFull.Size = New System.Drawing.Size(20, 22)
            Me.ProgressFull.TabIndex = 0
            '
            'GlossLeft
            '
            Me.GlossLeft.Dock = System.Windows.Forms.DockStyle.Top
            Me.GlossLeft.Location = New System.Drawing.Point(0, 0)
            Me.GlossLeft.Name = "GlossLeft"
            Me.GlossLeft.Size = New System.Drawing.Size(20, 10)
            Me.GlossLeft.TabIndex = 0
            '
            'ProgressEmpty
            '
            Me.ProgressEmpty.Controls.Add(Me.GlossRight)
            Me.ProgressEmpty.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ProgressEmpty.Location = New System.Drawing.Point(21, 1)
            Me.ProgressEmpty.Name = "ProgressEmpty"
            Me.ProgressEmpty.Size = New System.Drawing.Size(197, 22)
            Me.ProgressEmpty.TabIndex = 1
            '
            'GlossRight
            '
            Me.GlossRight.Dock = System.Windows.Forms.DockStyle.Top
            Me.GlossRight.Location = New System.Drawing.Point(0, 0)
            Me.GlossRight.Name = "GlossRight"
            Me.GlossRight.Size = New System.Drawing.Size(197, 10)
            Me.GlossRight.TabIndex = 0
            '
            'ColorProgressBar
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.ProgressEmpty)
            Me.Controls.Add(Me.ProgressFull)
            Me.Name = "ColorProgressBar"
            Me.Padding = New System.Windows.Forms.Padding(1)
            Me.Size = New System.Drawing.Size(219, 24)
            Me.ProgressFull.ResumeLayout(False)
            Me.ProgressEmpty.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Private Sub InitializeDefaults()
            Me.GlossLeft.BackColor = System.Drawing.Color.FromArgb(100, 255, 255, 255)
            Me.GlossRight.BackColor = System.Drawing.Color.FromArgb(100, 255, 255, 255)
            Me.BackColor = Me._BorderColor
            Me.ProgressEmpty.BackColor = Me._EmptyColor
            Me.ProgressFull.BackColor = Me._BarColor
        End Sub

        Private Function UpdateGloss() As Boolean
            Try
                ' Jedes Glanzfeld ausblenden
                Me.GlossLeft.Height = CInt(Me.Height / 3)
                Me.GlossRight.Height = Me.GlossLeft.Height
            Catch MyException As System.Exception
                Return False ' Einen Fehler zurückgeben und beenden
                Exit Function
            End Try
            Return True
        End Function

        Private Function UpdateProgress() As Boolean
            Try
                'Globale Werte neu berechnen
                Me._ProgressUnit = CInt(Me.Width / Me._MaxValue)
                Me.ProgressFull.Width = Me._ProgressValue * Me._ProgressUnit
                ' Glanzfelder gemäß Globals ausblenden oder anzeigen
                If Me._IsGlossy Then
                    Me.GlossLeft.Visible = True
                    Me.GlossRight.Visible = True
                Else
                    Me.GlossLeft.Visible = False
                    Me.GlossRight.Visible = False
                End If
                ' Bei Maximalwert den Fortschrittsbalken ausfüllen
                If Me._ProgressValue = Me._MaxValue Then
                    Me.ProgressFull.Width = If(Me._ShowBorder, Me.Width - 2, Me.Width)
                End If
                'Ränder je nach Globalwerten ausblenden oder anzeigen
                Me.Padding = If(Me._ShowBorder, New System.Windows.Forms.Padding(1), New System.Windows.Forms.Padding(0))
            Catch MyException As System.Exception
                Return False ' Einen Fehler zurückgeben und beenden
                Exit Function
            End Try
            Return True
        End Function

        Private Sub ColorProgressBar_PaddingChanged(sender As Object, e As System.EventArgs) Handles Me.PaddingChanged
            Me.Padding = If(Me._ShowBorder, New System.Windows.Forms.Padding(1), New System.Windows.Forms.Padding(0))
        End Sub

        Private Sub ColorProgressBar_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
            If Me.Value <= Me._MaxValue Then
                Dim unused = Me.UpdateProgress()
                Dim unused1 = Me.UpdateGloss()
            End If
        End Sub

        Private Sub Panel_Click(sender As Object, e As System.EventArgs) Handles GlossLeft.Click, ProgressFull.Click, ProgressEmpty.Click, GlossRight.Click
            RaiseEvent Click(Me, e)
        End Sub

        'UserControl1 überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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