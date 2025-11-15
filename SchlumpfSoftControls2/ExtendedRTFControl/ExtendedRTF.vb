' *************************************************************************************************
' ExtendedRTF.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ExtendedRTFControl

    ''' <summary>
    ''' Erweiterte <see cref="System.Windows.Forms.RichTextBox"/> mit bequemen Formatierungs- und Abfrage-Hilfen
    ''' (Schriftgröße, Stil-Flags, Farben, Einzüge, Ausrichtung) sowie Batch-Update (Redraw-Suppression).
    ''' </summary>
    ''' <remarks>
    ''' <para>Redraw-Suppression (verringerte Flackereffekte) über verschachteltes <see cref="System.Windows.Forms.RichTextBox.BeginUpdate"/> /
    ''' <see cref="System.Windows.Forms.RichTextBox.EndUpdate"/> mittels <c>WM_SETREDRAW</c>.</para>
    ''' <para>Mischzustände (uneinheitliche Formatierung in einer Auswahl) werden als <c>Nothing</c> (Nullable)
    ''' dargestellt – soweit implementiert (z.B. Stil-Flags, Schriftgröße, Einzug).</para>
    ''' <para>Vorder-/Hintergrundfarbe melden aktuell keinen Mischzustand (immer konkreter Wert).</para>
    ''' </remarks>
    <ProvideToolboxControlAttribute("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Control zum Anzeigen von animierten Grafiken.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(ExtendedRTFControl.ExtendedRTF), "ExtendedRTF.bmp")>
    Public Class ExtendedRTF : Inherits System.Windows.Forms.RichTextBox

#Region "Variablendefinitionen"

        ''' <summary>
        ''' Zähler für geschachtelte Update-Blöcke.
        ''' </summary>
        ''' <remarks>
        ''' Redraw-Unterdrückung
        ''' </remarks>
        Private _updateNesting As Integer = 0

        ''' <summary>
        ''' Flag zur Unterdrückung von "OnSelectionChanged", wenn intern temporär
        ''' per-Zeichen-Selektionen durchgeführt werden.
        ''' </summary>
        ''' <remarks>
        ''' Mischzustandsanalyse
        ''' </remarks>
        Private _suppressSelectionEvents As Boolean = False

        'Required by the Windows Form Designer
        Private ReadOnly components As System.ComponentModel.IContainer

#End Region

#Region "Öffentliche Methoden"

        ''' <summary>
        ''' Erzeugt eine neue Instanz der erweiterten RichTextBox.
        ''' </summary>
        Public Sub New()
            MyBase.New()
            Me.InitializeComponent()
        End Sub


        ''' <summary>
        ''' Entfernt Formatierungen (Schriftstil, Vorder-/Hintergrundfarbe, Bullet-Aufzählung)
        ''' vollständig aus aktueller Auswahl oder – ohne Auswahl – ab der Caret-Position.
        ''' </summary>
        ''' <remarks>
        ''' Optimiert: Wendet die Normalisierung einmal auf die gesamte Auswahl an (statt per Zeichen).
        ''' </remarks>
        Public Sub ClearFormatting()
            If Me.SelectionLength = 0 Then
                ' Kein Bereich markiert -> Format am Caret zurücksetzen.
                Dim baseFont = Me.SelectionFont
                If baseFont Is Nothing Then baseFont = Me.Font
                ' Neuer Font auf Regular (alle Stil-Flags weg)
                Using resetFont As New System.Drawing.Font(baseFont.FontFamily, baseFont.Size, System.Drawing.FontStyle.Regular, baseFont.Unit, baseFont.GdiCharSet, baseFont.GdiVerticalFont)
                    Me.SelectionFont = resetFont
                End Using
                Me.SelectionColor = Me.ForeColor
                Me.SelectionBackColor = Me.BackColor
                Me.SelectionBullet = False
                Return
            End If

            Me.BeginUpdate()
            Try
                Dim baseFont = Me.SelectionFont
                If baseFont Is Nothing Then baseFont = Me.Font
                Using resetFont As New System.Drawing.Font(baseFont.FontFamily, baseFont.Size, System.Drawing.FontStyle.Regular, baseFont.Unit, baseFont.GdiCharSet, baseFont.GdiVerticalFont)
                    Me.SelectionFont = resetFont
                End Using
                Me.SelectionColor = Me.ForeColor
                Me.SelectionBackColor = Me.BackColor
                Me.SelectionBullet = False
            Finally
                Me.EndUpdate()
            End Try
        End Sub

        ''' <summary>
        ''' Setzt die horizontale Ausrichtung der aktuellen Absatz-/Absatzauswahl.
        ''' </summary>
        Public Sub SetSelectionAlignment(alignment As System.Windows.Forms.HorizontalAlignment)
            Me.SelectionAlignment = alignment
        End Sub

        ''' <summary>
        ''' Schaltet Fettdruck für aktuelle Auswahl bzw. Caret um.
        ''' </summary>
        Public Sub ToggleBold()
            Me.SelectionBold = Not Me.SelectionBold.GetValueOrDefault(False)
        End Sub

        ''' <summary>
        ''' Schaltet Kursiv für aktuelle Auswahl bzw. Caret um.
        ''' </summary>
        Public Sub ToggleItalic()
            Me.SelectionItalic = Not Me.SelectionItalic.GetValueOrDefault(False)
        End Sub

        ''' <summary>
        ''' Schaltet Unterstreichung für aktuelle Auswahl bzw. Caret um.
        ''' </summary>
        Public Sub ToggleUnderline()
            Me.SelectionUnderline = Not Me.SelectionUnderline.GetValueOrDefault(False)
        End Sub

        ''' <summary>
        ''' Schaltet Durchstreichung für aktuelle Auswahl bzw. Caret um.
        ''' </summary>
        Public Sub ToggleStrikeout()
            Me.SelectionStrikeout = Not Me.SelectionStrikeout.GetValueOrDefault(False)
        End Sub

        ''' <summary>
        ''' Schaltet Bullet-Aufzählung für aktuelle Absatz-/Absatzauswahl um.
        ''' </summary>
        ''' <remarks>
        ''' Funktioniert nur auf Absatzebene (SelectionLength=0 -> aktueller Absatz).
        ''' </remarks>
        Public Sub ToggleBullet()
            Me.SelectionBullet = Not Me.SelectionBullet
        End Sub

#End Region

#Region "neue Eigenschaften"

        ''' <summary>
        ''' Setzt die Schriftgröße der Auswahl oder Größe am Caret oder gibt diese
        ''' zurück.
        ''' </summary>
        ''' <remarks>
        ''' Ein Mischzustand ergibt Nothing.
        ''' </remarks>
        <System.ComponentModel.Browsable(False)>
        Public Property SelectionFontSize As System.Nullable(Of Single)
            Get
                If Me.SelectionLength = 0 Then
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Return f.Size
                End If
                Return Me.GetUniformFontValue(Function(f) f.Size)
            End Get
            Set(value As System.Nullable(Of Single))
                If Not value.HasValue Then Exit Property
                If value.Value < MIN_FONT_SIZE Then
                    Throw New System.ArgumentOutOfRangeException(NameOf(value),
                        $"Schriftgröße muss mindestens {MIN_FONT_SIZE} sein.")
                End If
                Me.SetSelectionFontSize(value.Value)
            End Set
        End Property

        ''' <summary>
        ''' Setzt den Bold-Zustand der Auswahl oder am Caret oder gibt diesen zurück.
        ''' </summary>
        ''' <remarks>
        ''' Ein Mischzustand ergibt Nothing.
        ''' </remarks>
        <System.ComponentModel.Browsable(False)>
        Public Property SelectionBold As System.Nullable(Of Boolean)
            Get
                If Me.SelectionLength = 0 Then
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Return f.Bold
                End If
                Return Me.GetUniformFontFlag(Function(f) f.Bold)
            End Get
            Set(value As System.Nullable(Of Boolean))
                If Not value.HasValue Then Exit Property
                Me.ApplyStyleFlag(System.Drawing.FontStyle.Bold, value.Value)
            End Set
        End Property

        ''' <summary>
        ''' Setzt den Kursiv-Zustand der Auswahl oder am Caret  oder gibt diesen zurück.
        ''' </summary>
        ''' <remarks>
        ''' Ein Mischzustand ergibt Nothing.
        ''' </remarks>
        <System.ComponentModel.Browsable(False)>
        Public Property SelectionItalic As System.Nullable(Of Boolean)
            Get
                If Me.SelectionLength = 0 Then
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Return f.Italic
                End If
                Return Me.GetUniformFontFlag(Function(f) f.Italic)
            End Get
            Set(value As System.Nullable(Of Boolean))
                If Not value.HasValue Then Exit Property
                Me.ApplyStyleFlag(System.Drawing.FontStyle.Italic, value.Value)
            End Set
        End Property

        ''' <summary>
        ''' Setzt die Unterstreichung der Auswahl oder am Caret oder gibt diesen zurück.
        ''' </summary>
        ''' <remarks>
        ''' Ein Mischzustand ergibt Nothing.
        ''' </remarks>
        <System.ComponentModel.Browsable(False)>
        Public Property SelectionUnderline As System.Nullable(Of Boolean)
            Get
                If Me.SelectionLength = 0 Then
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Return f.Underline
                End If
                Return Me.GetUniformFontFlag(Function(f) f.Underline)
            End Get
            Set(value As System.Nullable(Of Boolean))
                If Not value.HasValue Then Exit Property
                Me.ApplyStyleFlag(System.Drawing.FontStyle.Underline, value.Value)
            End Set
        End Property

        ''' <summary>
        ''' Setzt die Durchstreichung der Auswahl oder am Caret oder gibt diesen zurück.
        ''' </summary>
        ''' <remarks>
        ''' Ein Mischzustand ergibt Nothing.
        ''' </remarks>
        <System.ComponentModel.Browsable(False)>
        Public Property SelectionStrikeout As System.Nullable(Of Boolean)
            Get
                If Me.SelectionLength = 0 Then
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Return f.Strikeout
                End If
                Return Me.GetUniformFontFlag(Function(f) f.Strikeout)
            End Get
            Set(value As System.Nullable(Of Boolean))
                If Not value.HasValue Then Exit Property
                Me.ApplyStyleFlag(System.Drawing.FontStyle.Strikeout, value.Value)
            End Set
        End Property

        ''' <summary>
        ''' Setzt die aktuelle Vordergrundfarbe (Textfarbe) der Auswahl oder am Caret oder gibt diese zurück.
        ''' </summary>
        ''' <remarks>
        ''' <para>Meldet keinen Mischzustand (immer konkreter Wert). </para>
        ''' <para>Für echte Mischzustandserkennung wäre eine per-Zeichen-Prüfung analog zu
        ''' den Stil-Flags nötig.</para>
        ''' </remarks>
        <System.ComponentModel.Browsable(False)>
        Public Property SelectionForeColor As System.Drawing.Color
            Get
                Return MyBase.SelectionColor
            End Get
            Set(value As System.Drawing.Color)
                MyBase.SelectionColor = value
            End Set
        End Property

        ''' <summary>
        ''' Setzt die aktuelle Hintergrund-/Highlightfarbe der Auswahl oder am Caret oder gibt diese zurück.
        ''' </summary>
        ''' <remarks>
        ''' <para>Meldet keinen Mischzustand (immer konkreter Wert).</para>
        ''' <para>Für echte Mischzustandserkennung wäre eine per-Zeichen-Prüfung analog zu
        ''' den Stil-Flags nötig.</para>
        ''' </remarks>
        <System.ComponentModel.Browsable(False)>
        Public Overloads Property SelectionBackColor As System.Drawing.Color
            Get
                Return MyBase.SelectionBackColor
            End Get
            Set(value As System.Drawing.Color)
                MyBase.SelectionBackColor = value
            End Set
        End Property

        ''' <summary>
        ''' Setzt den Absatz-Einzug (Pixel) der Auswahl oder am Caret oder gibt diesen zurück.
        ''' </summary>
        ''' <remarks>
        ''' <para>Ein Mischzustand ergibt Nothing.</para>
        ''' <para>Der Einzug wird immer für den gesamten Absatz gesetzt (SelectionLength wird
        ''' intern ignoriert).</para>
        ''' </remarks>
        <System.ComponentModel.Browsable(False)>
        Public Property SelectionLeftIndent As System.Nullable(Of Integer)
            Get
                Return If(Me.SelectionLength = 0, MyBase.SelectionIndent, Me.GetUniformParagraphValue(Function() MyBase.SelectionIndent))
            End Get
            Set(value As System.Nullable(Of Integer))
                If Not value.HasValue Then Exit Property
                If value.Value < 0 Then Throw New System.ArgumentOutOfRangeException(NameOf(value), "Einzug darf nicht negativ sein.")
                MyBase.SelectionIndent = value.Value
            End Set
        End Property

#End Region

#Region "Interne Methoden"

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.SuspendLayout()
            '
            'ToolboxControl
            '
            Me.Name = "ExtendedRTF"
            Me.ResumeLayout(False)

        End Sub

        ''' <summary>
        ''' Liefert ein einheitliches Bool-Stil-Flag (oder Nothing bei Mischzustand).
        ''' </summary>
        Private Function GetUniformFontFlag(selector As System.Func(Of System.Drawing.Font, Boolean)) As System.Nullable(Of Boolean)
            Dim len = Me.SelectionLength
            If len <= 0 Then Return Nothing
            Dim start = Me.SelectionStart
            Dim result As System.Nullable(Of Boolean) = Nothing
            Me.BeginInternalSelectionScan()
            Try
                For i = 0 To len - 1
                    Me.[Select](start + i, 1)
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Dim v = selector(f)
                    If Not result.HasValue Then
                        result = v
                    ElseIf result.Value <> v Then
                        result = Nothing
                        Exit For
                    End If
                Next
            Finally
                Me.[Select](start, len)
                Me.EndInternalSelectionScan()
            End Try
            Return result
        End Function

        ''' <summary>
        ''' Liefert einen einheitlichen Single-Wert (Schriftgröße) oder Nothing (Mischzustand).
        ''' </summary>
        Private Function GetUniformFontValue(selector As System.Func(Of System.Drawing.Font, Single)) As System.Nullable(Of Single)
            Dim len = Me.SelectionLength
            If len <= 0 Then Return Nothing
            Dim start = Me.SelectionStart
            Dim value As System.Nullable(Of Single) = Nothing
            Me.BeginInternalSelectionScan()
            Try
                For i = 0 To len - 1
                    Me.[Select](start + i, 1)
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Dim s = selector(f)
                    If Not value.HasValue Then
                        value = s
                    ElseIf System.Math.Abs(value.Value - s) > 0.01F Then
                        value = Nothing
                        Exit For
                    End If
                Next
            Finally
                Me.[Select](start, len)
                Me.EndInternalSelectionScan()
            End Try
            Return value
        End Function

        ''' <summary>
        ''' Liefert einen einheitlichen Absatzwert (Integer) oder Nothing bei Mischzustand.
        ''' </summary>
        Private Function GetUniformParagraphValue(selector As System.Func(Of Integer)) As System.Nullable(Of Integer)
            Dim len = Me.SelectionLength
            If len <= 0 Then Return Nothing
            Dim start = Me.SelectionStart
            Dim v As System.Nullable(Of Integer) = Nothing
            Me.BeginInternalSelectionScan()
            Try
                For i = 0 To len - 1
                    Me.[Select](start + i, 1)
                    Dim cur = selector()
                    If Not v.HasValue Then
                        v = cur
                    ElseIf v.Value <> cur Then
                        v = Nothing
                        Exit For
                    End If
                Next
            Finally
                Me.[Select](start, len)
                Me.EndInternalSelectionScan()
            End Try
            Return v
        End Function

        ''' <summary>
        ''' Setzt die Schriftgröße (alle anderen Attribute bleiben erhalten).
        ''' </summary>
        Private Sub SetSelectionFontSize(newSize As Single)
            If newSize <= 0 Then Throw New System.ArgumentOutOfRangeException(NameOf(newSize))
            Me.ApplyFontTransformation(
                Function(f)
                    Return New System.Drawing.Font(f.FontFamily, newSize, f.Style, f.Unit, f.GdiCharSet, f.GdiVerticalFont)
                End Function)
        End Sub

        ''' <summary>
        ''' Wendet / entfernt ein einzelnes FontStyle-Flag auf Auswahl/Caret an.
        ''' </summary>
        Private Sub ApplyStyleFlag(flag As System.Drawing.FontStyle, enabled As Boolean)
            Me.ApplyFontTransformation(
                Function(f)
                    Dim targetStyle = If(enabled, f.Style Or flag, f.Style And Not flag)
                    If targetStyle = f.Style Then
                        ' Keine Änderung nötig -> selben Font zurückgeben (wird nicht ersetzt)
                        Return f
                    End If
                    Return New System.Drawing.Font(f, targetStyle)
                End Function)
        End Sub

        ''' <summary>
        ''' Kernroutine zur Font-Transformation (Stil-/Größenänderungen) für Caret oder Auswahl.
        ''' </summary>
        ''' <param name="transform">Funktion, die auf Basis des vorhandenen Fonts einen neuen zurückgibt.
        ''' Gibt sie exakt denselben Font zurück, erfolgt keine Zuweisung.</param>
        Private Sub ApplyFontTransformation(transform As System.Func(Of System.Drawing.Font, System.Drawing.Font))
            If Me.SelectionLength = 0 Then
                ' Nur Caret: Einfach einmal transformieren
                Dim f = Me.SelectionFont
                If f Is Nothing Then f = Me.Font
                Dim nf = transform(f)
                If nf Is Nothing Then Exit Sub
                If nf Is f Then Exit Sub ' keine Änderung
                Try
                    Me.SelectionFont = nf
                Finally
                    If nf IsNot f Then nf.Dispose()
                End Try
                Return
            End If

            ' Auswahl: pro Zeichen anwenden (RichTextBox hat kein native Multi-Teil-Transform API auf .NET-Level)
            Dim start = Me.SelectionStart
            Dim len = Me.SelectionLength
            Me.BeginUpdate()
            Try
                ' Cache zur Wiederverwendung identischer Fonts -> reduziert GDI Handles
                Dim cache As New System.Collections.Generic.Dictionary(Of String, System.Drawing.Font)(System.StringComparer.Ordinal)
                For i = 0 To len - 1
                    Me.[Select](start + i, 1)
                    Dim f = Me.SelectionFont
                    If f Is Nothing Then f = Me.Font
                    Dim nf = transform(f)
                    If nf Is Nothing OrElse nf Is f Then Continue For

                    Dim key = FontCacheKey(nf)
                    Dim apply As System.Drawing.Font
#Disable Warning BC42030 ' Die Variable wurde als Verweis übergeben, bevor ihr ein Wert zugewiesen wurde.
                    If cache.TryGetValue(key, apply) Then
#Enable Warning BC42030 ' Die Variable wurde als Verweis übergeben, bevor ihr ein Wert zugewiesen wurde.
                        ' Haben bereits ein identisches Font-Objekt -> erstellen entsorgtes verwerfen
                        nf.Dispose()
                    Else
                        cache(key) = nf
                        apply = nf
                    End If
                    Me.SelectionFont = apply
                Next
                ' Ursprüngliche Auswahl wiederherstellen
                Me.[Select](start, len)
                ' Fonts aus Cache entsorgen (RichTextBox kopiert Formatdaten intern)
                For Each kv In cache
                    kv.Value.Dispose()
                Next
            Finally
                Me.EndUpdate()
            End Try
        End Sub

        ''' <summary>
        ''' Erzeugt einen konsistenten Cache-Schlüssel für einen Font (Familie+Größe+Stil+Einheit+Charset+Vertikal).
        ''' </summary>
        Private Shared Function FontCacheKey(f As System.Drawing.Font) As String
            Return $"{f.FontFamily.Name}|{f.Size}|{CInt(f.Style)}|{CInt(f.Unit)}|{f.GdiCharSet}|{f.GdiVerticalFont}"
        End Function

        ''' <summary>
        ''' Start eines verschachtelbaren Batch-Blocks. Unterdrückt Neuzeichnen via WM_SETREDRAW.
        ''' </summary>
        Private Sub BeginUpdate()
            If Not Me.IsHandleCreated Then Return
            If Me._updateNesting = 0 Then
                Dim unused = SendMessage(Me.Handle, WM_SETREDRAW, False, System.IntPtr.Zero)
            End If
            Me._updateNesting += 1
        End Sub

        ''' <summary>
        ''' Ende eines Batch-Blocks. Bei Erreichen von 0 wird Redraw wieder aktiviert und das Control neu gezeichnet.
        ''' </summary>
        Private Sub EndUpdate()
            If Not Me.IsHandleCreated Then Return
            Me._updateNesting -= 1
            If Me._updateNesting <= 0 Then
                Me._updateNesting = 0
                Dim unused = SendMessage(Me.Handle, WM_SETREDRAW, True, System.IntPtr.Zero)
                Me.Invalidate() ' Neuzeichnen anfordern
                Me.Update()     ' Sofortige Ausführung (reduziert wahrnehmbares Flackern)
            End If
        End Sub

        ''' <summary>
        ''' Signalisiert Beginn eines internen Auswahl-Scans (Mischzustandserkennung): unterdrückt SelectionChanged.
        ''' </summary>
        Private Sub BeginInternalSelectionScan()
            Me._suppressSelectionEvents = True
            Me.BeginUpdate()
        End Sub

        ''' <summary>
        ''' Beendet internen Auswahl-Scan und reaktiviert Events/Redraw (verschachtelt sicher).
        ''' </summary>
        Private Sub EndInternalSelectionScan()
            Me._suppressSelectionEvents = False
            Me.EndUpdate()
        End Sub

        ''' <inheritdoc/>
        Protected Overrides Sub OnSelectionChanged(e As System.EventArgs)
            If Me._suppressSelectionEvents Then
                ' Intern ausgelöste per-Zeichen-Select-Operation -> nicht an UI weiterreichen.
                Return
            End If
            MyBase.OnSelectionChanged(e)
        End Sub

#End Region

#Region "überschriebene Methoden"

        'UserControl überschreibt Dispose, um die Komponentenliste zu bereinigen.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso Me.components IsNot Nothing Then
                    Me.components.Dispose()
                End If
                ' Falls Entwickler vergessen hat EndUpdate mehrfach aufzurufen.
                While Me._updateNesting > 0
                    Me._updateNesting = 1
                    Me.EndUpdate()
                End While
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

#End Region

    End Class

End Namespace