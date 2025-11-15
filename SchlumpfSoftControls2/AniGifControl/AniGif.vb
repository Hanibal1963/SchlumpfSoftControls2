' *************************************************************************************************
' AniGif.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace AniGifControl

    ''' <summary>
    ''' Control zum anzeigen von animierten Grafiken.
    ''' </summary>
    <ProvideToolboxControlAttribute("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Control zum Anzeigen von animierten Grafiken.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(AniGifControl.AniGif), "AniGifControl.AniGif.bmp")>
    Public Class AniGif : Inherits System.Windows.Forms.UserControl

        Implements System.IDisposable

#Region "Variablendefinitionen"

        Private WithEvents Timer As System.Windows.Forms.Timer
        Private components As System.ComponentModel.IContainer
        Private disposedValue As Boolean
        Private ReadOnly _AnimationHandler As System.EventHandler = AddressOf Me.OnNextFrame ' Gemeinsamer Handler für ImageAnimator zum Stoppen/Neu-Registrieren

#End Region

#Region "Ereignisdefinitionen"

        ''' <summary>
        ''' Wird ausgelöst wenn die Grafik nicht animiert werden kann.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Wird ausgelöst wenn die Grafik nicht animiert werden kann.")>
        Public Event NoAnimation(sender As Object, e As System.EventArgs)

        ''' <summary>
        ''' Wird ausgelöst wenn sich das Bild geändert hat.
        ''' </summary>
        Private Event GifChanged()

        ''' <summary>
        ''' Wird ausgelöst wenn sich die Anzeigegeschwindigkeit geändert hat.
        ''' </summary>
        Private Event CustomDisplaySpeedChanged()

        ''' <summary>
        ''' Wird ausgelöst wenn sich die Frames pro Sekunde geändert haben.
        ''' </summary>
        Private Event FramesPerSecondChanged()

#End Region

#Region "neue Eigenschaften"

        ''' <summary>
        ''' Steuert, ob die GIF‑Animation automatisch gestartet wird, sobald ein Bild
        ''' vorhanden ist.
        ''' </summary>
        ''' <remarks>
        ''' <b>Verhalten:</b> <list type="bullet">
        '''  <item>
        '''   <description>Beim Setzen auf <c>True</c> wird in <see cref="InitLayout"/> eine ggf. laufende Animation zuerst sicher beendet und anschließend – falls ein animierbares GIF in <see cref="Gif"/> vorliegt und kein Designmodus aktiv ist – die Animation über <see cref="System.Drawing.ImageAnimator.Animate(System.Drawing.Image,System.EventHandler)"/> neu registriert.</description>
        '''  </item>
        '''  <item>
        '''   <description>Beim Setzen auf <c>False</c> wird in <see cref="InitLayout"/> keine Neu‑Registrierung vorgenommen; bereits registrierte Animationen werden zuvor gestoppt. Für ein explizites Beenden inkl. Timer empfiehlt sich <see cref="StopAnimation"/>.</description>
        '''  </item>
        '''  <item>
        '''   <description>Im Designmodus (<c>DesignMode = True</c>) wird nie animiert.</description>
        '''  </item>
        ''' </list>
        ''' <b> Interaktion mit anderen Eigenschaften:</b> <list type="bullet">
        '''  <item>
        '''   <description><see cref="CustomDisplaySpeed"/> = <c>False</c>: Die Bildgeschwindigkeit wird aus dem GIF übernommen. Das Frame‑Fortschalten erfolgt in <see cref="OnPaint(System.Windows.Forms.PaintEventArgs)"/> via <see cref="System.Drawing.ImageAnimator.UpdateFrames()"/> nur, wenn <c>AutoPlay = True</c>.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="CustomDisplaySpeed"/> = <c>True</c>: Der interne <c>Timer</c> steuert die Frames. Läuft der Timer, bewirkt er nur dann eine sichtbare Fortschaltung (Invalidate) im <see cref="Timer_Tick(System.Object,System.EventArgs)"/>, wenn <c>AutoPlay = True</c>. Ist <c>AutoPlay = False</c>, bleibt der Timer wirkungslos, kann aber noch aktiv sein. Nutzen Sie bei Bedarf <see cref="StopAnimation"/> um den Timer zu stoppen.</description>
        '''  </item>
        ''' </list>
        ''' <b> Lebenszyklus:</b> <list type="bullet">
        '''  <item>
        '''   <description><see cref="StartAnimation"/> setzt diese Eigenschaft auf <c>True</c> und ruft <see cref="InitLayout"/> auf.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="StopAnimation"/> setzt auf <c>False</c>, stoppt den <c>Timer</c> und beendet die Registrierung beim <see cref="System.Drawing.ImageAnimator"/>.</description>
        '''  </item>
        ''' </list>
        ''' <b> Hinweise:</b> <list type="bullet">
        '''  <item>
        '''   <description>Standardwert ist <c>False</c> (siehe <see cref="InitializeValues"/>).</description>
        '''  </item>
        '''  <item>
        '''   <description>Das Setzen dieser Eigenschaft löst kein eigenes Ereignis
        ''' aus.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        ''' <value>
        ''' <c>True</c>, um die Animation automatisch zu starten; andernfalls <c>False</c>. Standard: <c>False</c>.
        ''' </value>
        ''' <example>
        ''' Beispiel: <code language="vb"><![CDATA[
        ''' ' Automatisch abspielen (Geschwindigkeit aus GIF)
        ''' AniGif1.Gif = My.Resources.Standard
        ''' AniGif1.CustomDisplaySpeed = False
        ''' AniGif1.AutoPlay = True
        '''  
        ''' ' Benutzerdefinierte Geschwindigkeit (z. B. 15 FPS) und starten
        ''' AniGif1.CustomDisplaySpeed = True
        ''' AniGif1.FramesPerSecond = 15D
        ''' AniGif1.AutoPlay = True
        '''  
        ''' ' Animation explizit stoppen (Timer + Animator)
        ''' AniGif1.StopAnimation()]]></code>
        ''' </example>
        ''' <seealso cref="Gif"/>
        ''' <seealso cref="CustomDisplaySpeed"/>
        ''' <seealso cref="FramesPerSecond"/>
        ''' <seealso cref="StartAnimation"/>
        ''' <seealso cref="StopAnimation"/>
        ''' <seealso cref="InitLayout"/>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Legt fest ob die Animation sofort nach dem laden gestartet wird.")>
        Public Property AutoPlay() As Boolean
            Get
                Return _Autoplay
            End Get
            Set(value As Boolean)
                _Autoplay = value
                Me.InitLayout()
            End Set
        End Property

        ''' <summary>
        ''' Gibt die animierte GIF‑Grafik zurück oder legt diese fest.
        ''' </summary>
        ''' <remarks>
        ''' <b>Verhalten beim Setzen:</b> <list type="bullet">
        '''  <item>
        '''   <description>Wird <c>Nothing</c> übergeben, wird das Standard‑GIF aus <c>My.Resources.Standard</c> verwendet.</description>
        '''  </item>
        '''  <item>
        '''   <description>Vor dem Wechsel wird eine ggf. laufende Animation für das bisherige Bild über <see cref="System.Drawing.ImageAnimator.StopAnimate(System.Drawing.Image,System.EventHandler)"/> beendet und das alte <see cref="System.Drawing.Bitmap"/> entsorgt (<c>Dispose()</c>).</description>
        '''  </item>
        '''  <item>
        '''   <description>Nach dem Setzen wird intern das Ereignis <c>GifChanged</c> ausgelöst. Dadurch werden Frame‑Dimension, Frame‑Anzahl und (falls <see cref="CustomDisplaySpeed"/> = <c>True</c>) der Timer initialisiert; anschließend erfolgt <see cref="System.Windows.Forms.Control.Invalidate"/> und ein Aufruf von <see cref="InitLayout"/>, der die Animation bei <see cref="AutoPlay"/> = <c>True</c> neu registriert.</description>
        '''  </item>
        '''  <item>
        '''   <description>Ist das Bild nicht animierbar und <see cref="AutoPlay"/> = <c>True</c>, wird das Ereignis <see cref="NoAnimation"/> ausgelöst.</description>
        '''  </item>
        ''' </list>
        '''  <b>Lebenszyklus &amp; Besitzrechte (Ownership):</b> <list type="bullet">
        '''  <item>
        '''   <description>Das Control übernimmt die Eigentümerschaft des gesetzten <see cref="System.Drawing.Bitmap"/> und ruft dessen <c>Dispose()</c> beim nächsten Setzen oder beim Entsorgen des Controls auf. Das übergebene Objekt darf außerhalb des Controls nicht weiterverwendet oder entsorgt werden. Verwenden Sie bei Bedarf eine Kopie (<c>bitmap.Clone()</c>).</description>
        '''  </item>
        ''' </list>
        '''  <b>Interaktion mit anderen Eigenschaften:</b> <list type="bullet">
        '''  <item>
        '''   <description><see cref="AutoPlay"/>: Steuert, ob die Animation nach dem Setzen
        ''' automatisch startet. Im Designmodus wird nie animiert.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="CustomDisplaySpeed"/> / <see cref="FramesPerSecond"/>: Bei aktivierter benutzerdefinierter Geschwindigkeit steuert der interne <c>Timer</c> den Frame‑Fortschritt. Andernfalls wird die im GIF hinterlegte Bildfolge über <see cref="System.Drawing.ImageAnimator.UpdateFrames()"/> in <see cref="OnPaint(System.Windows.Forms.PaintEventArgs)"/> verwendet.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="GifSizeMode"/> / <see cref="ZoomFactor"/>:
        ''' Beeinflussen ausschließlich die Darstellung, nicht die Bilddaten.</description>
        '''  </item>
        ''' </list>
        '''  <b>Threading:</b> Das Setzen sollte auf dem UI‑Thread erfolgen.
        ''' </remarks>
        ''' <value>
        ''' Das anzuzeigende <see cref="System.Drawing.Bitmap"/>. Bei <c>Nothing</c> wird ein Standard‑GIF aus den Ressourcen verwendet.
        ''' </value>
        ''' <example>
        ''' Beispiel: <code language="vb"><![CDATA[
        ''' ' 1) GIF aus Ressourcen, automatische Geschwindigkeit aus der Datei
        ''' AniGif1.Gif = My.Resources.Standard
        ''' AniGif1.CustomDisplaySpeed = False
        ''' AniGif1.AutoPlay = True
        '''  
        ''' ' 2) GIF aus Datei, benutzerdefinierte Geschwindigkeit (15 FPS)
        ''' Dim bmp As New System.Drawing.Bitmap("C:\Bilder\loader.gif")
        ''' AniGif1.Gif = bmp              ' Kontrolle übernimmt Ownership von bmp
        ''' AniGif1.CustomDisplaySpeed = True
        ''' AniGif1.FramesPerSecond = 15D
        ''' AniGif1.AutoPlay = True
        '''  
        ''' ' 3) Auf Standard zurücksetzen
        ''' AniGif1.Gif = Nothing]]></code>
        ''' </example>
        ''' <seealso cref="AutoPlay"/>
        ''' <seealso cref="CustomDisplaySpeed"/>
        ''' <seealso cref="FramesPerSecond"/>
        ''' <seealso cref="GifSizeMode"/>
        ''' <seealso cref="ZoomFactor"/>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt die animierte Gif-Grafik zurück oder legt diese fest.")>
        Public Property Gif() As System.Drawing.Bitmap
            Get
                Return _Gif
            End Get
            Set(value As System.Drawing.Bitmap)
                Me.SetGifImage(value)
            End Set
        End Property

        ''' <summary>
        ''' Gibt den Anzeigemodus (Skalierung/Ausrichtung) der GIF-Grafik zurück oder legt
        ''' ihn fest.
        ''' </summary>
        ''' <remarks>
        ''' <b>Verhalten je Modus:</b> <para>- <see cref="SizeMode.Normal"/>: Das Bild wird
        ''' unverändert an Position (0,0) gezeichnet. Ist das Bild größer als das Control,
        ''' wird es abgeschnitten.</para>
        ''' <para>- <see cref="SizeMode.CenterImage"/>: Das Bild wird unverändert zentriert
        ''' gezeichnet. Es kann abgeschnitten werden, wenn es größer als das Control
        ''' ist.</para>
        ''' <para>- <see cref="SizeMode.Zoom"/>: Das Bild wird proportional skaliert und
        ''' zentriert. Die Skalierung richtet sich nach <see cref="ZoomFactor"/> (1–100%).
        ''' Die Berechnung orientiert sich an der Controlgröße, das Seitenverhältnis bleibt
        ''' erhalten.</para>
        ''' <para>- <see cref="SizeMode.Fill"/>: Das Bild wird proportional so skaliert,
        ''' dass das Control vollständig gefüllt wird. Dadurch kann das Bild an einer Seite
        ''' zugeschnitten werden; es wird zentriert gezeichnet.</para>
        ''' <b> Rendering:</b> <para>Für <see cref="SizeMode.Zoom"/> und <see
        ''' cref="SizeMode.Fill"/> werden hochwertige Interpolations- und
        ''' Glättungseinstellungen (HighQualityBicubic, HighQuality) verwendet, um die
        ''' Bildqualität bei der Skalierung zu verbessern.</para>
        '''  Ablauf/Seiteneffekte: <para>Beim Setzen dieser Eigenschaft wird das Control
        ''' über <see cref="System.Windows.Forms.Control.Invalidate"/> neu gezeichnet. Die
        ''' Bilddaten werden nicht verändert; nur die Darstellung ändert sich. Es wird kein
        ''' eigener Ereignis-Callback ausgelöst.</para>
        ''' <b> Hinweise:</b> <para>- Der Standardwert wird in <c>InitializeValues</c> gesetzt.</para>
        ''' <para>- <see cref="ZoomFactor"/> wirkt ausschließlich, wenn der Modus <see
        ''' cref="SizeMode.Zoom"/> aktiv ist.</para>
        ''' <para>- Die Eigenschaft ist im Designer sichtbar (Kategorie "Behavior").</para>
        ''' </remarks>
        ''' <value>
        ''' Einer der <see cref="SizeMode"/>-Werte: <list type="bullet">
        '''  <item>
        '''   <description><see cref="SizeMode.Normal"/>: Bild in Originalgröße, oben
        ''' links.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="SizeMode.CenterImage"/>: Bild in Originalgröße,
        ''' zentriert.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="SizeMode.Zoom"/>: Proportionale Skalierung nach <see
        ''' cref="ZoomFactor"/>, zentriert.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="SizeMode.Fill"/>: Proportionale Skalierung, Control
        ''' wird vollständig gefüllt (ggf. Zuschnitt), zentriert.</description>
        '''  </item>
        ''' </list>
        '''  Standardwert ist <see cref="SizeMode.Normal"/>.
        ''' </value>
        ''' <example>
        ''' Beispiel: <code language="vb"><![CDATA[
        '''  ' Bild proportional zoomen und auf 75% skalieren
        '''  AniGif1.GifSizeMode = SizeMode.Zoom
        '''  AniGif1.ZoomFactor = 75D
        '''  
        '''  ' Bild zentriert in Originalgröße anzeigen
        '''  AniGif1.GifSizeMode = SizeMode.CenterImage
        '''  
        '''  ' Bild das Control ausfüllen lassen (möglicher Zuschnitt)
        '''  AniGif1.GifSizeMode = SizeMode.Fill
        '''  ]]></code>
        ''' </example>
        ''' <seealso cref="ZoomFactor"/>
        ''' <seealso cref="Gif"/>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Gibt die Art wie die Grafik angezeigt wird zurück oder legt diese fest.")>
        Public Property GifSizeMode() As SizeMode
            Get
                Return _GifSizeMode
            End Get
            Set(value As SizeMode)
                Me.SetGifSizeMode(value)
            End Set
        End Property

        ''' <summary>
        ''' Legt fest, ob die benutzerdefinierte Anzeigegeschwindigkeit (Timer/FPS) oder
        ''' <br/>
        ''' die im GIF hinterlegte Bildfolge (ImageAnimator) verwendet wird.
        ''' </summary>
        ''' <remarks>
        ''' <b>Verhalten beim Setzen:</b> <list type="bullet">
        '''  <item>
        '''   <description><c>True</c>: Die Animation wird über den internen <see cref="System.Windows.Forms.Timer"/> mit der in <see cref="FramesPerSecond"/> konfigurierten Frequenz gesteuert. Pro Tick wird das nächste Frame per <see cref="System.Drawing.Bitmap.SelectActiveFrame(System.Drawing.Imaging.FrameDimension,Integer)"/> ausgewählt und das Control über <see cref="System.Windows.Forms.Control.Invalidate"/> neu gezeichnet.</description>
        '''  </item>
        '''  <item>
        '''   <description><c>False</c>: Die im GIF gespeicherten Verzögerungen werden verwendet. Das Umschalten der Frames erfolgt in <see cref="OnPaint(System.Windows.Forms.PaintEventArgs)"/> über <see cref="System.Drawing.ImageAnimator.UpdateFrames()"/> (nur bei <see cref="AutoPlay"/> = <c>True</c>).</description>
        '''  </item>
        '''  <item>
        '''   <description>Beim Aktivieren (<c>True</c>) wird das Timer-Intervall sofort auf <c>1000 / FramesPerSecond</c> gesetzt und der Timer gestartet; beim Deaktivieren (<c>False</c>) wird der Timer gestoppt.</description>
        '''  </item>
        ''' </list>
        '''  <b>Interaktion mit anderen Eigenschaften/Ereignissen:</b> <list type="bullet">
        '''  <item>
        '''   <description><see cref="AutoPlay"/>: Eine sichtbare Animation erfolgt nur bei <c>AutoPlay = True</c>. Ist <c>AutoPlay = False</c>, kann der Timer zwar laufen, hat aber keine sichtbare Wirkung (keine Frame-Fortschaltung). Nutzen Sie bei Bedarf <see cref="StopAnimation"/> um Timer und Animator zu beenden.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="FramesPerSecond"/>: Wirkt nur, wenn <c>CustomDisplaySpeed = True</c>. Der Wert wird intern auf den Bereich 1–50 begrenzt. Änderungen aktualisieren das Timer-Intervall unmittelbar.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="Gif"/>: Beim Bildwechsel werden Frame-Dimension und -Anzahl neu ermittelt. Ist das Bild nicht animierbar und <c>AutoPlay = True</c>, wird <see cref="NoAnimation"/> ausgelöst und der Timer gestoppt.</description>
        '''  </item>
        ''' </list>
        '''  <b>Lebenszyklus/Design:</b> <list type="bullet">
        '''  <item>
        '''   <description>Standardwert ist <c>False</c> (siehe <see cref="InitializeValues"/>).</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="StartAnimation"/> ändert diese Eigenschaft nicht; sie steuert lediglich <see cref="AutoPlay"/>. Die Auswahl der Geschwindigkeitsquelle erfolgt ausschließlich über <c>CustomDisplaySpeed</c>.</description>
        '''  </item>
        '''  <item>
        '''   <description>Im Designmodus wird nie animiert.</description>
        '''  </item>
        ''' </list>
        '''  <b>Hinweise zur internen Umsetzung:</b> <list type="bullet">
        '''  <item>
        '''   <description>Bei <c>CustomDisplaySpeed = True</c> übernimmt der Timer die Frame-Steuerung. Vom <see cref="System.Drawing.ImageAnimator"/> ausgelöste Ereignisse können zusätzliche <c>Invalidate</c>-Aufrufe bewirken, die Bildfolge bestimmt jedoch der Timer.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        ''' <value>
        ''' <c>True</c>, um die Anzeigegeschwindigkeit über <see cref="FramesPerSecond"/> zu steuern; andernfalls <c>False</c>, um die im GIF hinterlegte Geschwindigkeit zu verwenden. Standard: <c>False</c>.
        ''' </value>
        ''' <example>
        ''' Beispiel: <code language="vb"><![CDATA[
        ''' ' 1) Geschwindigkeit aus GIF-Datei verwenden
        ''' AniGif1.CustomDisplaySpeed = False
        ''' AniGif1.AutoPlay = True
        '''  
        ''' ' 2) Benutzerdefinierte Geschwindigkeit (24 FPS) verwenden
        ''' AniGif1.CustomDisplaySpeed = True
        ''' AniGif1.FramesPerSecond = 24D
        ''' AniGif1.AutoPlay = True
        '''  
        ''' ' 3) Zur GIF-Geschwindigkeit zurückkehren
        ''' AniGif1.CustomDisplaySpeed = False
        '''  
        ''' ' 4) Animation vollständig stoppen (Timer + Animator)
        ''' AniGif1.StopAnimation()]]></code>
        ''' </example>
        ''' <seealso cref="AutoPlay"/>
        ''' <seealso cref="FramesPerSecond"/>
        ''' <seealso cref="Gif"/>
        ''' <seealso cref="StartAnimation"/>
        ''' <seealso cref="StopAnimation"/>
        ''' <seealso cref="InitLayout"/>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Legt fest ob die benutzerdefinierte Anzeigegeschwindigkeit oder die in der Datei festgelegte Geschwindigkeit benutzt wird.")>
        Public Property CustomDisplaySpeed As Boolean
            Get
                Return _CustomDisplaySpeed
            End Get
            Set(value As Boolean)
                Me.SetCustomDisplaySpeed(value)
            End Set
        End Property

        ''' <summary>
        ''' Legt die benutzerdefinierte Anzeigegeschwindigkeit in Bildern pro Sekunde (FPS)
        ''' fest.
        ''' </summary>
        ''' <remarks>
        ''' <b>Geltungsbereich:</b> <list type="bullet">
        '''  <item>
        '''   <description>Wirksam nur, wenn <see cref="CustomDisplaySpeed"/> = <c>True</c> ist. Andernfalls wird die im GIF hinterlegte Bildfolge über <see cref="System.Drawing.ImageAnimator.UpdateFrames()"/> verwendet.</description>
        '''  </item>
        ''' </list>
        '''  <b>Wertebereich und Validierung:</b> <list type="bullet">
        '''  <item>
        '''   <description>Zulässiger Bereich ist <c>1</c> bis <c>50</c> FPS. Werte außerhalb dieses Bereichs werden in <see cref="CheckFramesPerSecondValue(Decimal)"/> automatisch begrenzt.</description>
        '''  </item>
        '''  <item>
        '''   <description>Dezimalwerte sind erlaubt. Das resultierende Timer-Intervall wird als <c>CInt(1000 / FramesPerSecond)</c> in Millisekunden berechnet und auf ganze Millisekunden gerundet.</description>
        '''  </item>
        ''' </list>
        '''  <b>Laufzeitverhalten:</b> <list type="bullet">
        '''  <item>
        '''   <description>Beim Setzen wird <see cref="FramesPerSecondChanged"/> ausgelöst.
        ''' Der zugehörige Handler aktualisiert das Intervall des internen <see
        ''' cref="System.Windows.Forms.Timer"/> sofort: Läuft der Timer, wird er kurz
        ''' gestoppt, neu konfiguriert und wieder gestartet; andernfalls wird nur das
        ''' Intervall gesetzt.</description>
        '''  </item>
        '''  <item>
        '''   <description>Eine sichtbare Animation erfolgt nur, wenn <see cref="AutoPlay"/> = <c>True</c> ist und ggf. ein animierbares GIF vorliegt (<see cref="System.Drawing.ImageAnimator.CanAnimate(System.Drawing.Image)"/>).</description>
        '''  </item>
        ''' </list>
        '''  <b>Interaktion mit anderen Eigenschaften/Ereignissen:</b> <list type="bullet">
        '''  <item>
        '''   <description><see cref="CustomDisplaySpeed"/>: Muss <c>True</c> sein, damit der Timer die Frames steuert.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="AutoPlay"/>: Steuert, ob die Fortschaltung sichtbar ist. Bei <c>False</c> kann der Timer laufen, es erfolgt jedoch keine Fortschaltung.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="GifChanged"/>: Initialisiert Frame-Dimension/-Anzahl. Ist kein animierbares GIF vorhanden und <see cref="AutoPlay"/> = <c>True</c>, wird <see cref="NoAnimation"/> ausgelöst und der Timer gestoppt.</description>
        '''  </item>
        ''' </list>
        '''  <b>Design/Standardwerte:</b> <list type="bullet">
        '''  <item>
        '''   <description>Standardwert ist <c>10</c> FPS (siehe <see cref="InitializeValues"/>).</description>
        '''  </item>
        '''  <item>
        '''   <description>Im Designmodus (<c>DesignMode = True</c>) wird nicht animiert.</description>
        '''  </item>
        ''' </list>
        '''  <b>Threading:</b> Auf dem UI-Thread setzen.
        ''' </remarks>
        ''' <value>
        ''' Zielwert in Bildern pro Sekunde (1–50). Werte außerhalb des Bereichs werden automatisch korrigiert. Standard: <c>10</c>.
        ''' </value>
        ''' <example>
        ''' Beispiel: <code language="vb"><![CDATA[
        '''  ' Benutzerdefinierte Geschwindigkeit aktivieren und während der Laufzeit ändern
        '''  AniGif1.CustomDisplaySpeed = True
        '''  AniGif1.FramesPerSecond = 12.5D   ' Dezimalwerte möglich, Intervall wird gerundet
        '''  AniGif1.AutoPlay = True           ' Sichtbare Animation aktivieren
        '''  
        '''  ' Später schneller machen
        '''  AniGif1.FramesPerSecond = 24D
        '''  
        '''  ' Zur GIF-internen Geschwindigkeit zurückkehren
        '''  AniGif1.CustomDisplaySpeed = False
        '''  ]]></code>
        ''' </example>
        ''' <seealso cref="CustomDisplaySpeed"/>
        ''' <seealso cref="AutoPlay"/>
        ''' <seealso cref="StartAnimation"/>
        ''' <seealso cref="StopAnimation"/>
        ''' <seealso
        ''' cref="CheckFramesPerSecondValue(Decimal)">CheckFramesPerSecondValue</seealso>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Legt die benutzerdefinierte Anzeigegeschwindigkeit in Bildern/Sekunde fest wenn CustomDisplaySpeed auf True festgelegt ist.")>
        Public Property FramesPerSecond As Decimal
            Get
                Return _FramesPerSecond
            End Get
            Set(value As Decimal)
                _FramesPerSecond = Me.CheckFramesPerSecondValue(value)
                RaiseEvent FramesPerSecondChanged()
            End Set
        End Property

        ''' <summary>
        ''' Legt den Zoomfaktor in Prozent fest, mit dem das GIF skaliert wird.
        ''' </summary>
        ''' <remarks>
        ''' <b>Geltungsbereich:</b>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Wirksam nur, wenn <see cref="GifSizeMode"/> auf <see cref="SizeMode.Zoom"/> steht. In den Modi <see cref="SizeMode.Normal"/>, <see cref="SizeMode.CenterImage"/> und <see cref="SizeMode.Fill"/> hat der Wert keine Auswirkung auf die Darstellung.</description>
        '''  </item>
        ''' </list>
        ''' <b>Wertebereich und Validierung:</b>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Zulässiger Bereich: <c>1</c> bis <c>100</c> (%). Abweichungen werden in <see cref="CheckZoomFactorValue(Decimal)"/> automatisch begrenzt.</description>
        '''  </item>
        '''  <item>
        '''   <description>Dezimalwerte sind erlaubt (z. B. <c>62.5</c>). Intern wird in <see cref="OnPaint(System.Windows.Forms.PaintEventArgs)"/> der Faktor als Dezimalwert zwischen <c>0</c> und <c>1</c> verwendet (<c>_ZoomFactor / 100</c>) und an <see cref="GetRectStartSize(SizeMode, AniGif, System.Drawing.Bitmap, Decimal)"/> übergeben.</description>
        '''  </item>
        ''' </list>
        ''' <b>Laufzeitverhalten:</b>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Beim Setzen wird intern <see cref="SetZoomFactor(Decimal)"/> aufgerufen, der den Wert prüft und <see cref="System.Windows.Forms.Control.Invalidate"/> auslöst. Dadurch wird das Control neu gezeichnet; ein eigenes Ereignis wird nicht ausgelöst.</description>
        '''  </item>
        '''  <item>
        '''   <description>Die Änderung beeinflusst ausschließlich die Darstellung (Größe/Position), nicht die Bilddaten oder die Animation selbst.</description>
        '''  </item>
        ''' </list>
        ''' <b>Rendering-Qualität:</b>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Im Zoom- bzw. Fill-Modus werden hochwertige Einstellungen gesetzt (<see cref="System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic"/>, <see cref="System.Drawing.Drawing2D.SmoothingMode.HighQuality"/>, <see cref="System.Drawing.Drawing2D.PixelOffsetMode.HighQuality"/>), um die Skalierungsqualität zu verbessern.</description>
        '''  </item>
        ''' </list>
        ''' <b>Interaktion mit anderen Eigenschaften:</b>
        ''' <list type="bullet">
        '''  <item>
        '''   <description><see cref="GifSizeMode"/>: Muss auf <see cref="SizeMode.Zoom"/> stehen, damit der Wert sichtbar wird.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="AutoPlay"/> / <see cref="CustomDisplaySpeed"/> / <see cref="FramesPerSecond"/>: Beeinflussen nur die Animation, nicht die Skalierung. <c>ZoomFactor</c> wirkt unabhängig davon.</description>
        '''  </item>
        ''' </list>
        ''' <b>Design/Standardwerte:</b>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Standardwert ist <c>50</c> (%), gesetzt in <see cref="InitializeValues"/>.</description>
        '''  </item>
        '''  <item>
        '''   <description>Im Designmodus wird nicht animiert; die Skalierung im Zoom-Modus bleibt jedoch sichtbar.</description>
        '''  </item>
        ''' </list>
        ''' <b>Threading:</b> Auf dem UI-Thread setzen.
        ''' </remarks>
        ''' <value>
        ''' Zoomfaktor in Prozent im Bereich <c>1</c>–<c>100</c>. Werte außerhalb werden automatisch korrigiert. Standard: <c>50</c>.
        ''' </value>
        ''' <example>
        ''' Beispiel:
        ''' <code language="vb"><![CDATA[
        ''' ' Bild proportional auf 75% skalieren (Zoom-Modus erforderlich)
        ''' AniGif1.GifSizeMode = SizeMode.Zoom
        ''' AniGif1.ZoomFactor = 75D
        ''' 
        ''' ' Feinstufiger Zoom mit Dezimalwerten
        ''' AniGif1.ZoomFactor = 62.5D
        ''' 
        ''' ' Wechsel in einen Modus, in dem ZoomFactor keine Wirkung hat
        ''' AniGif1.GifSizeMode = SizeMode.CenterImage
        ''' ' ... Darstellung bleibt jetzt unabhängig vom ZoomFactor unverändert
        ''' ]]></code>
        ''' </example>
        ''' <seealso cref="GifSizeMode"/>
        ''' <seealso cref="SizeMode.Zoom"/>
        ''' <seealso cref="CheckZoomFactorValue(Decimal)"/>
        ''' <seealso cref="OnPaint(System.Windows.Forms.PaintEventArgs)"/>
        ''' <seealso cref="GetRectStartSize(SizeMode, AniGif, System.Drawing.Bitmap, Decimal)"/>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Legt den Zoomfaktor fest wenn GifSizeMode auf Zoom festgelegt ist.")>
        Public Property ZoomFactor As Decimal
            Get
                Return _ZoomFactor
            End Get
            Set(value As Decimal)
                Me.SetZoomFactor(value)
            End Set
        End Property

#End Region

#Region "ausgeblendete Eigenschaften"

        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property MaximumSize As System.Drawing.Size
            Get
                Return MyBase.MaximumSize
            End Get
            Set(value As System.Drawing.Size)
                MyBase.MaximumSize = value
            End Set
        End Property

        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property MinimumSize As System.Drawing.Size
            Get
                Return MyBase.MinimumSize
            End Get
            Set(value As System.Drawing.Size)
                MyBase.MinimumSize = value
            End Set
        End Property

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

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property RightToLeft() As System.Windows.Forms.RightToLeft
            Get
                Return MyBase.RightToLeft
            End Get
            Set(value As System.Windows.Forms.RightToLeft)
                MyBase.RightToLeft = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property Text() As String
            Get
                Return MyBase.Text
            End Get
            Set(value As String)
                MyBase.Text = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property AllowDrop() As Boolean
            Get
                Return MyBase.AllowDrop
            End Get
            Set(value As Boolean)
                MyBase.AllowDrop = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property AutoScrollOffset As System.Drawing.Point
            Get
                Return MyBase.AutoScrollOffset
            End Get
            Set(value As System.Drawing.Point)
                MyBase.AutoScrollOffset = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property AutoSize As Boolean
            Get
                Return MyBase.AutoSize
            End Get
            Set(value As Boolean)
                MyBase.AutoSize = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImage() As System.Drawing.Image
            Get
                Return MyBase.BackgroundImage
            End Get
            Set(value As System.Drawing.Image)
                MyBase.BackgroundImage = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property BackgroundImageLayout() As System.Windows.Forms.ImageLayout
            Get
                Return MyBase.BackgroundImageLayout
            End Get
            Set(value As System.Windows.Forms.ImageLayout)
                MyBase.BackgroundImageLayout = value
            End Set
        End Property

        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property ContextMenuStrip() As System.Windows.Forms.ContextMenuStrip
            Get
                Return MyBase.ContextMenuStrip
            End Get
            Set(value As System.Windows.Forms.ContextMenuStrip)
                MyBase.ContextMenuStrip = value
            End Set
        End Property

        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property Dock() As System.Windows.Forms.DockStyle
            Get
                Return MyBase.Dock
            End Get
            Set(value As System.Windows.Forms.DockStyle)
                MyBase.Dock = value
            End Set
        End Property

        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property Font() As System.Drawing.Font
            Get
                Return MyBase.Font
            End Get
            Set(value As System.Drawing.Font)
                MyBase.Font = value
            End Set
        End Property

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Overrides Property ForeColor() As System.Drawing.Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As System.Drawing.Color)
                MyBase.ForeColor = value
            End Set
        End Property

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see
        ''' cref="SchlumpfSoft.Controls.AniGifControl.AniGif"/>.
        ''' </summary>
        Public Sub New()
            Me.InitializeComponent()
            Me.InitializeValues() 'Standardwerte laden
        End Sub

        ''' <summary>
        ''' Startet die Animation (falls noch nicht aktiv).
        ''' </summary>
        Public Sub StartAnimation()
            If Not _Autoplay Then
                _Autoplay = True
                Me.InitLayout()
            End If
        End Sub

        ''' <summary>
        ''' Stoppt die Animation und beendet Timer sowie ImageAnimator.
        ''' </summary>
        Public Sub StopAnimation()
            If _Autoplay Then
                _Autoplay = False
                If _Gif IsNot Nothing Then System.Drawing.ImageAnimator.StopAnimate(_Gif, Me._AnimationHandler)
                Me.Timer.Stop()
            End If
        End Sub

#End Region

#Region "überladene und überschriebene Methoden"

        ''' <summary>
        ''' Führt Layout-Initialisierung durch und startet ggf. die GIF-Animation.
        ''' </summary>
        ''' <remarks>
        ''' Stoppt vorher eine bestehende Animation um Mehrfach-Registrierungen zu vermeiden.
        ''' </remarks>
        Protected Overloads Overrides Sub InitLayout()
            MyBase.InitLayout()
            ' Alte Animation stoppen um Mehrfach-Registrierungen zu vermeiden
            If _Gif IsNot Nothing Then
                System.Drawing.ImageAnimator.StopAnimate(_Gif, Me._AnimationHandler)
            End If
            ' Nur animieren wenn AutoPlay aktiv ist
            If Not Me.DesignMode AndAlso _Autoplay AndAlso _Gif IsNot Nothing AndAlso System.Drawing.ImageAnimator.CanAnimate(_Gif) Then
                System.Drawing.ImageAnimator.Animate(_Gif, Me._AnimationHandler)
            End If
        End Sub

        ''' <summary>
        ''' Zeichnet das aktuelle Frame des GIFs unter Berücksichtigung der Skalierung.
        ''' </summary>
        ''' <param name="e">Zeicheninformationen.</param>
        ''' <remarks>
        ''' Aktualisiert die Animation bei automatischer Geschwindigkeitssteuerung.
        ''' </remarks>
        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            If _Gif Is Nothing Then Return ' Null-Schutz

            Dim g As System.Drawing.Graphics = e.Graphics ' Variable für Zeichenfläche
            Dim rectstartsize As System.Drawing.Size = Me.GetRectStartSize(_GifSizeMode, Me, _Gif, _ZoomFactor / 100) ' Größe der Zeichenfläche berechnen
            Dim rectstartpoint As System.Drawing.Point = Me.GetRectStartPoint(_GifSizeMode, Me, _Gif, rectstartsize) 'Startpunkt der Zeichenfläche berechnen

            ' Qualitätsverbesserung nur bei Skalierung
            If _GifSizeMode = SizeMode.Zoom OrElse _GifSizeMode = SizeMode.Fill Then
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            End If

            g.DrawImage(_Gif, New System.Drawing.Rectangle(rectstartpoint, rectstartsize)) ' Zeichenfläche festlegen und Bild zeichnen
            If Not Me.DesignMode And _Autoplay And Not _CustomDisplaySpeed Then ' Bild animieren wenn AutoPlay aktiv und Benutzerdefinierte Geschwindigkeit deaktiviert
                System.Drawing.ImageAnimator.UpdateFrames() ' im Bild gespeicherte Geschwindigkeit verwenden
            End If
        End Sub

        ''' <summary>
        ''' Gibt Ressourcen frei und stoppt ggf. laufende Animationen.
        ''' </summary>
        ''' <param name="disposing">True um verwaltete Ressourcen freizugeben.</param>
        Protected Overrides Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' Animation stoppen bevor Bild entsorgt wird
                    If _Gif IsNot Nothing Then
                        System.Drawing.ImageAnimator.StopAnimate(_Gif, Me._AnimationHandler)
                    End If
                    Me.components?.Dispose()
                    Me.Timer?.Dispose()
                    _Gif?.Dispose()
                End If
                Me.disposedValue = True
            End If
            MyBase.Dispose(disposing)
        End Sub

        ''' <summary>
        ''' IDisposable Support
        ''' </summary>
        Public Overloads Sub Dispose() Implements System.IDisposable.Dispose
            Me.Dispose(True)
            System.GC.SuppressFinalize(Me)
        End Sub

#End Region

#Region "interne Ereignisbehandlungen"

        ''' <summary>
        ''' Reagiert auf den Wechsel des GIF-Bildes und initialisiert Animationsparameter.
        ''' </summary>
        Private Sub AniGif_GifChange() Handles Me.GifChanged
            If System.Drawing.ImageAnimator.CanAnimate(_Gif) = False And _Autoplay = True Then 'überprüfen ob das Bild animiert werden kann wenn Autoplay auf True gesetzt ist
                Me.Timer.Stop() 'Timer stoppen und Anzahl der Frames auf 0 setzen (für nicht animiertes bild)
                _MaxFrame = 0
                RaiseEvent NoAnimation(Me, System.EventArgs.Empty) ' Ereignis auslösen
            Else 'Werte für Benutzerdefinierte Geschwindigkeit speichern
                _Dimension = New System.Drawing.Imaging.FrameDimension(_Gif.FrameDimensionsList(0))
                _MaxFrame = _Gif.GetFrameCount(_Dimension) - 1
                _Frame = 0
                If _CustomDisplaySpeed Then
                    Me.Timer.Interval = CInt(1000 / _FramesPerSecond) ' Intervall sofort setzen
                    Me.Timer.Start() ' Timer starten
                End If
            End If
            Me.Invalidate() ' neu zeichnen
            Me.InitLayout() ' Animation starten
        End Sub

        ''' <summary>
        ''' Aktiviert oder deaktiviert die benutzerdefinierte Animationsgeschwindigkeit.
        ''' </summary>
        Private Sub AniGif_CustomDisplaySpeedChanged() Handles Me.CustomDisplaySpeedChanged
            ' Intervall direkt setzen und Timer entsprechend Zustand starten/stoppen
            If _CustomDisplaySpeed Then
                Me.Timer.Interval = CInt(1000 / _FramesPerSecond)
                Me.Timer.Start()
            Else
                Me.Timer.Stop()
            End If
        End Sub

        ''' <summary>
        ''' Reagiert auf Änderung der Frames-pro-Sekunde Einstellung.
        ''' </summary>
        Private Sub AniGif_FramesPerSecondChanged() Handles Me.FramesPerSecondChanged
            If _FramesPerSecond < 1D Then _FramesPerSecond = 1D ' Sicherheitsprüfung
            If Me.Timer.Enabled Then
                Me.Timer.Stop() ' Timer stoppen um die Intervalle zu aktualisieren
                Me.Timer.Interval = CInt(1000 / _FramesPerSecond)
                Me.Timer.Start() ' Timer neu starten
            Else
                Me.Timer.Interval = CInt(1000 / _FramesPerSecond) ' Nur das Intervall aktualisieren wenn der Timer nicht läuft
            End If
        End Sub

        ''' <summary>
        ''' Wird vom ImageAnimator bei jedem anstehenden Frame aufgerufen.
        ''' </summary>
        ''' <param name="o">Auslösendes Objekt.</param>
        ''' <param name="e">Ereignisdaten.</param>
        Private Sub OnNextFrame(o As Object, e As System.EventArgs)
            If Me.AutoPlay AndAlso Not Me.DesignMode Then
                Me.Invalidate() 'neu zeichnen
            End If
        End Sub

        ''' <summary>
        ''' Timer-Tick zur manuellen Frame-Steuerung bei benutzerdefinierter Geschwindigkeit.
        ''' </summary>
        ''' <param name="sender">Timer.</param>
        ''' <param name="e">Ereignisdaten.</param>
        Private Sub Timer_Tick(sender As Object, e As System.EventArgs) Handles Timer.Tick
            'Bild animieren wenn AutoPlay und Benutzerdefinierte Geschwindigkeit aktiv
            If Not Me.DesignMode AndAlso Me.AutoPlay Then
                If _MaxFrame = 0 Then Exit Sub ' wenn Frames = 0 ist das Bild nicht animiert -> Ende
                If _Frame > _MaxFrame Then _Frame = 0 ' Bildzähler zurücksetzen wenn maximale Anzahl überschritten
                Dim unused = _Gif.SelectActiveFrame(_Dimension, _Frame) ' nächstes Bild auswählen
                _Frame += 1 ' Bildzähler weiterschalten
                Me.Invalidate() ' neu zeichnen
            End If
        End Sub

#End Region

#Region "Interne Methoden"

        ''' <summary>
        ''' <para>Initialisiert die Komponenten des Steuerelements.</para>
        ''' <para><b>Achtung!</b></para>
        ''' <para>Designer-generierter Code. Dieser Code kann nicht editiert werden da dies
        ''' zu Fehlern führen kann.</para>
        ''' <para>Eine Bearbeitung is im Designer möglich.</para>
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.Timer = New System.Windows.Forms.Timer(Me.components)
            Me.SuspendLayout()
            '
            'Timer
            '
            Me.Timer.Interval = 200
            '
            'AniGif
            '
            Me.DoubleBuffered = True
            Me.Name = "AniGif"
            Me.ResumeLayout(False)
        End Sub

        ''' <summary>
        ''' Setzt die Standardwerte für die wichtigsten Variablen der AniGif Control.
        ''' </summary>
        Private Sub InitializeValues()
            _Gif = My.Resources.Standard ' Standard-GIF aus den Ressourcen laden
            _Autoplay = False                   ' Animation startet nicht automatisch
            _GifSizeMode = SizeMode.Normal      ' GIF wird in Originalgröße angezeigt
            _CustomDisplaySpeed = False         ' Keine benutzerdefinierte Geschwindigkeit
            _FramesPerSecond = 10               ' Standard: 10 Bilder pro Sekunde
            _ZoomFactor = 50                    ' Standard-Zoomfaktor: 50%
        End Sub

        ''' <summary>
        ''' Setzt eine neue Animation.
        ''' </summary>
        ''' <param name="value">Neues GIF-Bitmap oder Nothing für Standard.</param>
        Private Sub SetGifImage(value As System.Drawing.Bitmap)
            ' Vorherige Animation stoppen bevor Bild entsorgt wird
            If _Gif IsNot Nothing Then
                System.Drawing.ImageAnimator.StopAnimate(_Gif, Me._AnimationHandler)
                _Gif.Dispose() ' Vorhandenes Bild freigeben
            End If
            _Gif = If(value, My.Resources.Standard) 'Standardanimation verwenden wenn keine Auswahl erfolgte
            RaiseEvent GifChanged()
        End Sub

        ''' <summary>
        ''' Setzt die Anzeigeart der Animation.
        ''' </summary>
        ''' <param name="value">Neuer SizeMode.</param>
        Private Sub SetGifSizeMode(value As SizeMode)
            _GifSizeMode = value
            Me.Invalidate()
        End Sub

        ''' <summary>
        ''' Setzt die benutzerdefinierte Anzeigegeschwindigkeit (aktiv/inaktiv).
        ''' </summary>
        ''' <param name="value">True zur Aktivierung.</param>
        Private Sub SetCustomDisplaySpeed(value As Boolean)
            _CustomDisplaySpeed = value
            RaiseEvent CustomDisplaySpeedChanged()
        End Sub

        ''' <summary>
        ''' Setzt den Zoomfaktor (wird geprüft und begrenzt).
        ''' </summary>
        ''' <param name="value">Neuer Zoomfaktor (1-100).</param>
        Private Sub SetZoomFactor(value As Decimal)
            _ZoomFactor = Me.CheckZoomFactorValue(value)
            Me.Invalidate() 'neu zeichnen
        End Sub

        ''' <summary>
        ''' Prüft und korrigiert den FPS-Wert in den Bereich 1..50.
        ''' </summary>
        ''' <param name="Frames">Angeforderte Frames pro Sekunde.</param>
        ''' <returns>Korrigierter Wert.</returns>
        Private Function CheckFramesPerSecondValue(Frames As Decimal) As Decimal
            ' Überprüft, ob der FPS-Wert im zulässigen Bereich liegt (1 bis 50)
            Select Case Frames
                Case Is < 1 : Return 1' Wenn der Wert kleiner als 1 ist, auf Mindestwert 1 setzen
                Case Is > 50 : Return 50 ' Wenn der Wert größer als 50 ist, auf Höchstwert 50 setzen
                Case Else : Return Frames ' Ansonsten den übergebenen Wert zurückgeben
            End Select
        End Function

        ''' <summary>
        ''' Prüft, ob der übergebene Zoomfaktor im gültigen Bereich (1-100) liegt.
        ''' </summary>
        ''' <param name="ZoomFactor">Angeforderter Zoomfaktor.</param>
        ''' <returns>Gibt den korrigierten Zoomfaktor im Bereich 1 bis 100 zurück.</returns>
        Private Function CheckZoomFactorValue(ZoomFactor As Decimal) As Decimal
            Select Case ZoomFactor
                Case Is < 1 : Return 1 ' Wenn der Zoomfaktor kleiner als 1 ist, auf Mindestwert 1 setzen
                Case Is > 100 : Return 100  ' Wenn der Zoomfaktor größer als 100 ist, auf Höchstwert 100 setzen
                Case Else : Return ZoomFactor  ' Ansonsten den übergebenen Wert zurückgeben
            End Select
        End Function

        ''' <summary>
        ''' Berechnet die Zielgröße des zu zeichnenden Bildes abhängig vom Modus.
        ''' </summary>
        ''' <param name="Mode">Aktueller SizeMode.</param>
        ''' <param name="Control">Referenz auf das Control.</param>
        ''' <param name="Gif">Aktuelles GIF.</param>
        ''' <param name="Zoom">Zoomfaktor (0-1) bei Zoom-Modus.</param>
        ''' <returns>Berechnete Größe.</returns>
        Private Function GetRectStartSize(Mode As SizeMode, Control As AniGif, Gif As System.Drawing.Bitmap, Zoom As Decimal) As System.Drawing.Size
            If Gif Is Nothing Then Return System.Drawing.Size.Empty ' Null-Schutz
            Select Case Mode
                Case SizeMode.Normal
                    ' Bild wird in Originalgröße angezeigt (keine Skalierung)
                    Return New System.Drawing.Size(Gif.Size.Width, Gif.Size.Height)
                Case SizeMode.CenterImage
                    ' Bild wird ebenfalls in Originalgröße angezeigt (zentriert, aber Größe bleibt gleich)
                    Return New System.Drawing.Size(Gif.Size.Width, Gif.Size.Height)
                Case SizeMode.Zoom
                    ' Bild wird proportional zum Zoomfaktor skaliert
                    If Gif.Size.Width < Gif.Size.Height Then
                        Return New System.Drawing.Size(CInt(Control.Height / CDec(Gif.Size.Height / Gif.Size.Width) * Zoom), CInt(Control.Height * Zoom))
                    Else
                        ' Bild ist breiter als hoch
                        ' Breite des Controls als Basis, Höhe proportional berechnen und mit Zoom multiplizieren
                        Return New System.Drawing.Size(CInt(Control.Width * Zoom), CInt(Control.Width * CDec(Gif.Size.Height / Gif.Size.Width) * Zoom))
                    End If
                Case SizeMode.Fill
                    ' Bild wird so skaliert, dass es das Control vollständig ausfüllt (Seitenverhältnis bleibt erhalten)
                    If Gif.Size.Width < Gif.Size.Height Then
                        ' Bild ist höher als breit
                        ' Höhe des Controls als Basis, Breite proportional berechnen
                        Return New System.Drawing.Size(CInt(Control.Height / CDec(Gif.Size.Height / Gif.Size.Width)), Control.Height)
                    Else
                        ' Bild ist breiter als hoch
                        ' Breite des Controls als Basis, Höhe proportional berechnen
                        Return New System.Drawing.Size(Control.Width, CInt(Control.Width * CDec(Gif.Size.Height / Gif.Size.Width)))
                    End If
                Case Else
                    Return New System.Drawing.Size(Gif.Size.Width, Gif.Size.Height)
            End Select
        End Function

        ''' <summary>
        ''' Berechnet den Startpunkt (linke obere Ecke) für das Zeichnen des Bildes.
        ''' </summary>
        ''' <param name="Mode">Aktueller SizeMode.</param>
        ''' <param name="Control">Referenz auf das Control.</param>
        ''' <param name="Gif">Aktuelles GIF.</param>
        ''' <param name="RectStartSize">Berechnete Zielgröße.</param>
        ''' <returns>Zeichenursprung.</returns>
        Private Function GetRectStartPoint(Mode As SizeMode, Control As AniGif, Gif As System.Drawing.Bitmap, RectStartSize As System.Drawing.Size) As System.Drawing.Point
            ' Bestimmt den Startpunkt (linke obere Ecke) für das Zeichnen des Bildes
            Select Case Mode
                Case SizeMode.Normal
                    ' Bild wird in Originalgröße oben links gezeichnet
                    Return New System.Drawing.Point(0, 0)
                Case SizeMode.CenterImage
                    ' Bild wird in Originalgröße zentriert gezeichnet
                    ' X-Position: Hälfte der Differenz zwischen Control-Breite und Bild-Breite
                    ' Y-Position: Hälfte der Differenz zwischen Control-Höhe und Bild-Höhe
                    Return New System.Drawing.Point(CInt((Control.Width - Gif.Size.Width) / 2), CInt((Control.Height - Gif.Size.Height) / 2))
                Case SizeMode.Zoom
                    ' Bild wird skaliert (gezoomt) und zentriert gezeichnet
                    ' X-Position: Hälfte der Differenz zwischen Control-Breite und skalierter Bild-Breite
                    ' Y-Position: Hälfte der Differenz zwischen Control-Höhe und skalierter Bild-Höhe
                    Return New System.Drawing.Point(CInt((Control.Width - RectStartSize.Width) / 2), CInt((Control.Height - RectStartSize.Height) / 2))
                Case SizeMode.Fill
                    ' Bild wird so skaliert, dass es das Control ausfüllt und zentriert gezeichnet
                    ' X- und Y-Position wie bei Zoom
                    Return New System.Drawing.Point(CInt((Control.Width - RectStartSize.Width) / 2), CInt((Control.Height - RectStartSize.Height) / 2))
                Case Else
                    Return New System.Drawing.Point(0, 0) ' Fallback
            End Select
        End Function

#End Region

    End Class

End Namespace
