' *************************************************************************************************
' AniGif.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace AniGifControl

    ''' <summary>
    ''' Control zum anzeigen von animierten Grafiken.
    ''' </summary>
    ''' <example>
    ''' Beispiel: Verwendung des AniGif-Controls in einer Windows-Forms-Anwendung. <code><![CDATA[Imports SchlumpfSoftControls2.AniGifControl
    '''  
    ''' Public Class MainForm
    '''     Inherits System.Windows.Forms.Form
    '''  
    '''     Private ReadOnly _gifControl As AniGif = New AniGif() With {
    '''         .Name = "AniGif1",
    '''         .Size = New System.Drawing.Size(200, 200),
    '''         .Location = New System.Drawing.Point(10, 10),
    '''         .GifSizeMode = AniGif.ImageSizeMode.Zoom,
    '''         .ZoomFactor = 75D,
    '''         .CustomDisplaySpeed = True,
    '''         .FramesPerSecond = 12.5D,
    '''         .AutoPlay = True
    '''     }
    '''  
    '''     Public Sub New()
    '''         Me.Text = "AniGif Demo"
    '''         Me.ClientSize = New System.Drawing.Size(320, 240)
    '''  
    '''         ' Ein GIF aus Datei laden und setzen (Ownership liegt beim Control).
    '''         ' Hinweis: Verwenden Sie bei Bedarf .Clone(), wenn die Bitmap außerhalb weitergenutzt wird.
    '''         Dim bmp As System.Drawing.Bitmap = CType(System.Drawing.Image.FromFile("C:\Pfad\zu\animiert.gif"), System.Drawing.Bitmap)
    '''         _gifControl.Gif = bmp
    '''  
    '''         Me.Controls.Add(_gifControl)
    '''     End Sub
    '''  
    '''     ' Animation zur Laufzeit explizit starten/stoppen:
    '''     Private Sub StartButton_Click(sender As Object, e As System.EventArgs) Handles MyBase.Click
    '''         _gifControl.StartAnimation() ' setzt AutoPlay = True und initialisiert Animation
    '''     End Sub
    '''  
    '''     Private Sub StopButton_Click(sender As Object, e As System.EventArgs)
    '''         _gifControl.StopAnimation()  ' setzt AutoPlay = False, stoppt Timer und Animator
    '''     End Sub
    ''' End Class]]></code>
    ''' </example>
    <ProvideToolboxControlAttribute("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Control zum Anzeigen von animierten Grafiken.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(AniGifControl.AniGif), "AniGifControl.AniGif.bmp")>
    Public Class AniGif : Inherits System.Windows.Forms.UserControl

        Implements System.IDisposable

#Region "Variablen"

        Private _FramesPerSecond As Decimal = 10
        Private _Dimension As System.Drawing.Imaging.FrameDimension
        Private _Frame As Integer
        Private _MaxFrame As Integer
        Private _Autoplay As Boolean = False
        Private _ZoomFactor As Decimal = 50
        Private _CustomDisplaySpeed As Boolean = False
        Private _GifSizeMode As ImageSizeMode = ImageSizeMode.Normal
        Private _Gif As System.Drawing.Bitmap = My.Resources.Standard
        Private WithEvents Timer As System.Windows.Forms.Timer
        Private components As System.ComponentModel.IContainer
        Private disposedValue As Boolean
        Private ReadOnly _AnimationHandler As System.EventHandler = AddressOf Me.OnNextFrame ' Gemeinsamer Handler für ImageAnimator zum Stoppen/Neu-Registrieren

#End Region

#Region "Aufzählungen"

        ''' <summary>
        ''' Legt fest, wie eine Grafik innerhalb der verfügbaren Client-Fläche eines
        ''' Controls angezeigt wird.
        ''' </summary>
        ''' <remarks>
        ''' Die Anzeigemodi bestimmen Ausrichtung und Skalierung der Grafik in Bezug auf die
        ''' Größe des Controls. <list type="bullet">
        '''  <item>
        '''   <description> <see cref="ImageSizeMode.Normal"/><b>:</b> Keine Skalierung;
        ''' Ausrichtung oben links. </description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="ImageSizeMode.CenterImage"/><b>:</b> Keine Skalierung;
        ''' zentriert. </description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="ImageSizeMode.Zoom"/><b>:</b> Einheitliche Skalierung,
        ''' sodass die Grafik vollständig in den verfügbaren Bereich passt
        ''' (letterboxing/pillarboxing möglich). </description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="ImageSizeMode.Fill"/><b>:</b> Skalierung, sodass der
        ''' verfügbare Bereich vollständig gefüllt wird (Zuschnitt oder Verzerrung abhängig
        ''' von der Implementierung möglich). </description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        ''' <seealso cref="System.Drawing.Image"/>
        ''' <seealso cref="System.Windows.Forms.Control"/>
        Public Enum ImageSizeMode

            ''' <summary>
            ''' Die Grafik wird in Originalgröße angezeigt; Ausrichtung erfolgt oben links.
            ''' </summary>
            ''' <remarks>
            ''' <list type="bullet">
            '''  <item>
            '''   <description>Es findet keine Skalierung statt. </description>
            '''  </item>
            '''  <item>
            '''   <description>Ist die Grafik größer als das Control, werden nicht sichtbare
            ''' Bereiche an den Rändern abgeschnitten. </description>
            '''  </item>
            '''  <item>
            '''   <description>Ist die Grafik kleiner, bleibt der restliche Bereich des Controls
            ''' leer.</description>
            '''  </item>
            ''' </list>
            ''' </remarks>
            Normal = 0

            ''' <summary>
            ''' Die Grafik wird in Originalgröße zentriert angezeigt.
            ''' </summary>
            ''' <remarks>
            ''' <list type="bullet">
            '''  <item>
            '''   <description>Es findet keine Skalierung statt. </description>
            '''  </item>
            '''  <item>
            '''   <description>Ist die Grafik größer als das Control, werden an den Rändern
            ''' nicht anzeigbare Teile abgeschnitten. </description>
            '''  </item>
            '''  <item>
            '''   <description>Ist die Grafik kleiner, entstehen umlaufende Ränder (leere
            ''' Flächen).</description>
            '''  </item>
            ''' </list>
            ''' </remarks>
            CenterImage = 1

            ''' <summary>
            ''' Die Größe der Grafik wird einheitlich skaliert, sodass sie in den verfügbaren
            ''' Bereich des Controls passt; zentrierte Ausrichtung (1–100%).
            ''' </summary>
            ''' <remarks>
            ''' <list type="bullet">
            '''  <item>
            '''   <description>Das Seitenverhältnis bleibt erhalten (keine Verzerrung).
            ''' </description>
            '''  </item>
            '''  <item>
            '''   <description>Die Grafik passt immer vollständig in das Control; ggf. mit
            ''' Rändern oben/unten oder links/rechts. </description>
            '''  </item>
            '''  <item>
            '''   <description>Eine Vergrößerung über 100% kann je nach Implementierung
            ''' unterbunden werden.</description>
            '''  </item>
            ''' </list>
            ''' </remarks>
            Zoom = 2

            ''' <summary>
            ''' Die Grafik wird so skaliert, dass der verfügbare Bereich vollständig gefüllt
            ''' wird; zentrierte Ausrichtung.
            ''' </summary>
            ''' <remarks>
            ''' <list type="bullet">
            '''  <item>
            '''   <description>Kleinere Grafiken werden vergrößert, größere verkleinert.
            ''' </description>
            '''  </item>
            '''  <item>
            '''   <description>Abhängig von der Implementierung kann entweder das
            ''' Seitenverhältnis beibehalten werden (dann erfolgt Zuschnitt an
            ''' gegenüberliegenden Kanten), oder die Grafik wird an das Seitenverhältnis des
            ''' Controls angepasst (mögliche Verzerrung).</description>
            '''  </item>
            ''' </list>
            ''' </remarks>
            Fill = 3

        End Enum

#End Region

#Region "Ereignisse"

        ''' <summary>
        ''' Wird ausgelöst wenn die Grafik nicht animiert werden kann.
        ''' </summary>
        ''' <example>
        ''' Beispiel: Ereignis abonnieren und ein Fallback setzen, falls das Bild nicht animierbar ist.
        ''' <code><![CDATA[
        ''' Imports SchlumpfSoftControls2.AniGifControl
        ''' 
        ''' Public Class MainForm
        '''     Inherits System.Windows.Forms.Form
        ''' 
        '''     Private ReadOnly _gifControl As New AniGif() With {
        '''         .GifSizeMode = AniGif.ImageSizeMode.Zoom,
        '''         .ZoomFactor = 60D,
        '''         .CustomDisplaySpeed = False,
        '''         .AutoPlay = True
        '''     }
        ''' 
        '''     Public Sub New()
        '''         Me.Text = "AniGif NoAnimation-Beispiel"
        '''         Me.ClientSize = New System.Drawing.Size(300, 220)
        '''         Me.Controls.Add(_gifControl)
        ''' 
        '''         ' Ein GIF laden (ggf. ist dieses nicht animierbar)
        '''         Dim bmp As System.Drawing.Bitmap =
        '''             CType(System.Drawing.Image.FromFile("C:\Pfad\zu\möglicherweise_nicht_animiert.gif"), System.Drawing.Bitmap)
        '''         _gifControl.Gif = bmp
        ''' 
        '''         ' Ereignis abonnieren
        '''         AddHandler _gifControl.NoAnimation, AddressOf OnNoAnimation
        '''     End Sub
        ''' 
        '''     Private Sub OnNoAnimation(sender As Object, e As System.EventArgs)
        '''         ' Benutzer informieren
        '''         System.Windows.Forms.MessageBox.Show("Das Bild ist nicht animierbar.", "Hinweis",
        '''             System.Windows.Forms.MessageBoxButtons.OK,
        '''             System.Windows.Forms.MessageBoxIcon.Information)
        ''' 
        '''         ' Optional: auf ein bekannt animiertes Standard-GIF wechseln
        '''         Dim fallback As System.Drawing.Bitmap =
        '''             CType(System.Drawing.Image.FromFile("C:\Pfad\zu\fallback_animiert.gif"), System.Drawing.Bitmap)
        '''         _gifControl.Gif = fallback
        '''     End Sub
        ''' End Class
        ''' ]]></code>
        ''' </example>        
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Wird ausgelöst wenn die Grafik nicht animiert werden kann.")>
        Public Event NoAnimation(sender As Object, e As System.EventArgs)

        Private Event GifChanged()
        Private Event CustomDisplaySpeedChanged()
        Private Event FramesPerSecondChanged()

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Steuert, ob die GIF‑Animation automatisch gestartet wird, sobald ein Bild
        ''' vorhanden ist.
        ''' </summary>
        ''' <remarks>
        ''' <b>Verhalten:</b> <list type="bullet">
        '''  <item>
        '''   <description>Beim Setzen auf <c>True</c> wird in <see cref="InitLayout"/> eine ggf. laufende Animation zuerst sicher beendet und anschließend (falls ein animierbares GIF in <see cref="Gif"/> vorliegt und kein Designmodus aktiv ist) die Animation über <see cref="System.Drawing.ImageAnimator.Animate(System.Drawing.Image,System.EventHandler)"/> neu registriert.</description>
        '''  </item>
        '''  <item>
        '''   <description>Beim Setzen auf <c>False</c> wird in <see cref="InitLayout"/> keine Neu‑Registrierung vorgenommen; bereits registrierte Animationen werden vorher gestoppt. <br/>
        ''' Für ein explizites Beenden inkl. Timer empfiehlt sich <see
        ''' cref="StopAnimation"/>.</description>
        '''  </item>
        '''  <item>
        '''   <description>Im Designmodus (<c>DesignMode = True</c>) wird nie animiert.</description>
        '''  </item>
        ''' </list>
        '''  <b>Interaktion:</b> <list type="bullet">
        '''  <item>
        '''   <description><see cref="CustomDisplaySpeed"/> = <c>False</c>: <br/>
        ''' Fortschalten via <see cref="System.Drawing.ImageAnimator.UpdateFrames()"/> in <see cref="OnPaint(System.Windows.Forms.PaintEventArgs)"/> nur, wenn <c>AutoPlay = True</c>.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="CustomDisplaySpeed"/> = <c>True</c>: <br/>
        ''' Der interne <c>Timer</c> steuert die Frames; sichtbar nur, wenn <c>AutoPlay = True</c>.</description>
        '''  </item>
        ''' </list>
        '''  <b>Lebenszyklus:</b> <list type="bullet">
        '''  <item>
        '''   <description><see cref="StartAnimation"/> setzt auf <c>True</c> und ruft <see cref="InitLayout"/> auf.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="StopAnimation"/> setzt auf <c>False</c>, stoppt Timer und beendet die Animator‑Registrierung.</description>
        '''  </item>
        ''' </list>
        ''' <para> <b>Hinweise:</b> </para>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Standardwert ist <c>False</c>. <br/>
        ''' Das Setzen löst kein eigenes Ereignis aus.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        ''' <value>
        ''' <c>True</c>, um die Animation automatisch zu starten; andernfalls <c>False</c>.<br/>
        ''' Standard: <c>False</c>.
        ''' </value>
        ''' <example>
        ''' <code><![CDATA[' AutoPlay per Code aktivieren
        ''' Dim control As New AniGif() With {.AutoPlay = True}
        ''' control.Gif = CType(System.Drawing.Image.FromFile("C:\gif\anim.gif"), System.Drawing.Bitmap)
        ''' ' Alternativ manuell starten/stoppen:
        ''' control.StartAnimation()
        ''' control.StopAnimation()]]></code>
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
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0049:Namen vereinfachen", Justification:="<Ausstehend>")>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Unnötige Unterdrückung entfernen", Justification:="<Ausstehend>")>
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
        '''   <description>Bei <c>Nothing</c> wird <c>My.Resources.Standard</c> verwendet.</description>
        '''  </item>
        '''  <item>
        '''   <description>Vor dem Wechsel:<br/>
        ''' Animation des alten Bildes via <see cref="System.Drawing.ImageAnimator.StopAnimate(System.Drawing.Image,System.EventHandler)"/> beenden und die alte <see cref="System.Drawing.Bitmap"/> entsorgen (<c>Dispose()</c>).</description>
        '''  </item>
        '''  <item>
        '''   <description>Nach dem Setzen:<br/>
        ''' <c>GifChanged</c> auslösen, Frame‑Infos initialisieren, ggf. Timer konfigurieren, <see cref="System.Windows.Forms.Control.Invalidate"/> und <see cref="InitLayout"/> zur Neu‑Registrierung bei <see cref="AutoPlay"/> = <c>True</c>.</description>
        '''  </item>
        '''  <item>
        '''   <description>Ist das Bild nicht animierbar und <see cref="AutoPlay"/> = <c>True</c>, wird <see cref="NoAnimation"/> ausgelöst.</description>
        '''  </item>
        ''' </list>
        ''' <para> <b>Ownership:</b></para>
        ''' <para>Das Control übernimmt die Eigentümerschaft des gesetzten <see
        ''' cref="System.Drawing.Bitmap"/> und entsorgt es bei einem erneuten Setzen oder
        ''' beim Dispose.<br/>
        ''' Nutzen Sie bei Bedarf <c>bitmap.Clone()</c>.</para>
        ''' </remarks>
        ''' <value>
        ''' Das anzuzeigende <see cref="System.Drawing.Bitmap"/>.<br/>
        ''' Bei <c>Nothing</c> wird ein Standard‑GIF aus den Ressourcen verwendet.
        ''' </value>
        ''' <example>
        ''' <code><![CDATA[' GIF setzen (Ownership liegt beim Control)
        ''' Dim bmp As System.Drawing.Bitmap =
        '''     CType(System.Drawing.Image.FromFile("C:\gif\anim.gif"), System.Drawing.Bitmap)
        ''' AniGif1.Gif = bmp
        ''' ' Sicher: außerhalb weiterverwenden -> Clone()
        ''' Dim copy As System.Drawing.Bitmap = CType(bmp.Clone(), System.Drawing.Bitmap)]]></code>
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
        ''' Gibt den Anzeigemodus (Skalierung/Ausrichtung) der GIF‑Grafik zurück oder legt
        ''' ihn fest.
        ''' </summary>
        ''' <remarks>
        ''' <b>Modi:</b> <list type="bullet">
        '''  <item>
        '''   <description><see cref="ImageSizeMode.Normal"/>: Originalgröße, oben links,
        ''' ggf. Zuschnitt.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="ImageSizeMode.CenterImage"/>: Originalgröße,
        ''' zentriert, ggf. Zuschnitt.</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="ImageSizeMode.Zoom"/>: Proportionale Skalierung und
        ''' Zentrierung, beeinflusst durch <see cref="ZoomFactor"/> (1–100%).</description>
        '''  </item>
        '''  <item>
        '''   <description><see cref="ImageSizeMode.Fill"/>: Proportionale Skalierung,
        ''' Control wird vollständig gefüllt, zentriert, ggf. Zuschnitt.</description>
        '''  </item>
        ''' </list>
        ''' <para> <b>Rendering:</b></para>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>In Zoom/Fill werden hochwertige Interpolations‑ und
        ''' Glättungseinstellungen verwendet. </description>
        '''  </item>
        ''' </list>
        ''' <para><b>Seiteneffekt:</b></para>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Beim Setzen wird <see
        ''' cref="System.Windows.Forms.Control.Invalidate"/> ausgelöst; Bilddaten bleiben
        ''' unverändert.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        ''' <value>
        ''' Einer der <see cref="ImageSizeMode"/>‑Werte.<br/>
        ''' Standard: <see cref="ImageSizeMode.Normal"/>.
        ''' </value>
        ''' <example>
        ''' <code><![CDATA[' Darstellung anpassen
        ''' AniGif1.GifSizeMode = AniGif.ImageSizeMode.Zoom
        ''' AniGif1.ZoomFactor = 80D]]></code>
        ''' </example>
        ''' <seealso cref="ZoomFactor"/>
        ''' <seealso cref="Gif"/>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Gibt die Art wie die Grafik angezeigt wird zurück oder legt diese fest.")>
        Public Property GifSizeMode() As SchlumpfSoft.Controls.AniGifControl.AniGif.ImageSizeMode
            Get
                Return _GifSizeMode
            End Get
            Set(value As ImageSizeMode)
                Me.SetGifSizeMode(value)
            End Set
        End Property

        ''' <summary>
        ''' Legt fest, ob die benutzerdefinierte Anzeigegeschwindigkeit (Timer/FPS) oder die
        ''' im GIF hinterlegte Bildfolge (ImageAnimator) verwendet wird.
        ''' </summary>
        ''' <remarks>
        ''' <para><b>True:</b></para>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Steuerung über internen <see cref="System.Windows.Forms.Timer"/>
        ''' mit <see cref="FramesPerSecond"/>; pro Tick Auswahl des nächsten Frames via <see
        ''' cref="System.Drawing.Bitmap.SelectActiveFrame(System.Drawing.Imaging.FrameDimension,
        ''' Integer)"/> und <see
        ''' cref="System.Windows.Forms.Control.Invalidate"/>.</description>
        '''  </item>
        ''' </list>
        ''' <para><b>False:</b> </para>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Nutzung der im GIF gespeicherten Verzögerungen; Umschalten der Frames über <see cref="System.Drawing.ImageAnimator.UpdateFrames()"/> in <see cref="OnPaint(System.Windows.Forms.PaintEventArgs)"/> (nur bei <see cref="AutoPlay"/> = <c>True</c>).</description>
        '''  </item>
        ''' </list>
        ''' <para><b>Umschalten:</b></para>
        ''' <list type="bullet">
        '''  <item>
        '''   <description>Beim Aktivieren wird das Timer‑Intervall sofort gesetzt und der
        ''' Timer gestartet; beim Deaktivieren wird der Timer gestoppt.</description>
        '''  </item>
        ''' </list>
        ''' </remarks>
        ''' <value>
        ''' <c>True</c>, um die Anzeigegeschwindigkeit über <see cref="FramesPerSecond"/> zu steuern; andernfalls <c>False</c>.<br/>
        ''' Standard: <c>False</c>.
        ''' </value>
        ''' <example>
        ''' <code><![CDATA[' Benutzerdefinierte Geschwindigkeit aktivieren
        ''' AniGif1.CustomDisplaySpeed = True
        ''' AniGif1.FramesPerSecond = 24D
        ''' ' Zur GIF-eigenen Abspielgeschwindigkeit wechseln
        ''' AniGif1.CustomDisplaySpeed = False]]></code>
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
        ''' Gilt nur, wenn <see cref="CustomDisplaySpeed"/> = <c>True</c>.<br/>
        ''' Bereich: <c>1</c>–<c>50</c> FPS, Validierung über <see cref="CheckFramesPerSecondValue(Decimal)"/>.<br/>
        ''' Das Timer‑Intervall wird als <c>CInt(1000/FramesPerSecond)</c> in Millisekunden berechnet.<br/>
        ''' Beim Setzen wird <c>FramesPerSecondChanged</c> ausgelöst; das Intervall wird sofort übernommen.
        ''' </remarks>
        ''' <value>
        ''' Zielwert in Bildern pro Sekunde (1–50).<br/>
        ''' Außerhalb des Bereichs automatische Korrektur. Standard: <c>10</c>.
        ''' </value>
        ''' <example>
        ''' <code><![CDATA[' 12,5 FPS im benutzerdefinierten Modus
        ''' AniGif1.CustomDisplaySpeed = True
        ''' AniGif1.FramesPerSecond = 12.5D]]></code>
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
        ''' Wirksam nur bei <see cref="GifSizeMode"/> = <see
        ''' cref="ImageSizeMode.Zoom"/>.<br/>
        ''' Bereich: <c>1</c>–<c>100</c>% mit Validierung über <see cref="CheckZoomFactorValue(Decimal)"/>.<br/>
        ''' Dezimalwerte sind erlaubt (z. B. <c>62.5</c>).<br/>
        ''' Intern wird mit einem Faktor von <c>_ZoomFactor/100</c> gearbeitet.<br/>
        ''' Beim Setzen wird <see cref="SetZoomFactor(Decimal)"/> aufgerufen und <see
        ''' cref="System.Windows.Forms.Control.Invalidate"/> ausgelöst.
        ''' </remarks>
        ''' <value>
        ''' Zoomfaktor in Prozent im Bereich <c>1</c>–<c>100</c>.<br/>
        ''' Standard: <c>50</c>.
        ''' </value>
        ''' <example>
        ''' <code><![CDATA[AniGif1.GifSizeMode = AniGif.ImageSizeMode.Zoom
        ''' AniGif1.ZoomFactor = 75D ' 75% skaliert, zentriert]]></code>
        ''' </example>
        ''' <seealso cref="GifSizeMode"/>
        ''' <seealso cref="ImageSizeMode.Zoom"/>
        ''' <seealso cref="CheckZoomFactorValue(Decimal)">CheckZoomFactorValue</seealso>
        ''' <seealso cref="OnPaint(System.Windows.Forms.PaintEventArgs)">OnPaint</seealso>
        ''' <seealso
        ''' cref="GetRectStartSize(ImageSizeMode,AniGif,System.Drawing.Bitmap,Decimal)">GetRectStartSize</seealso>
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

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
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

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
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

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
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

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
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

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
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

        ''' <summary>
        ''' Ausgeblendet da für dieses Control nicht relevant.
        ''' </summary>
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
        ''' <remarks>
        ''' Führt die Designer-Initialisierung aus und setzt interne Standardwerte (z. B. <c>AutoPlay = False</c>, <c>ZoomFactor = 50</c>, Standardressourcenbild).
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Control programmatisch erzeugen und zur Form hinzufügen
        ''' Dim ctrl As New SchlumpfSoft.Controls.AniGifControl.AniGif()
        ''' ctrl.GifSizeMode = SchlumpfSoft.Controls.AniGifControl.AniGif.ImageSizeMode.Zoom
        ''' ctrl.ZoomFactor = 75D
        ''' ctrl.AutoPlay = True
        ''' Me.Controls.Add(ctrl)]]></code>
        ''' </example>
        Public Sub New()
            Me.InitializeComponent() 'Designer-Initialisierung
        End Sub

        ''' <summary>
        ''' Startet die Animation (falls noch nicht aktiv).
        ''' </summary>
        ''' <remarks>
        ''' Setzt <see cref="AutoPlay"/> auf <c>True</c> und ruft <see cref="InitLayout"/> auf, wodurch eine ggf. vorherige Animator-Registrierung sicher beendet und bei animierbaren GIFs neu registriert wird.<br/>
        ''' Im Designmodus wird nicht animiert.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Animation explizit starten
        ''' AniGif1.StartAnimation()
        '''  
        ''' ' Alternative: AutoPlay aktivieren, Animation startet bei nächster Initialisierung
        ''' AniGif1.AutoPlay = True]]></code>
        ''' </example>
        ''' <seealso cref="StopAnimation"/>
        ''' <seealso cref="AutoPlay"/>
        ''' <seealso cref="InitLayout"/>
        Public Sub StartAnimation()
            If Not _Autoplay Then
                _Autoplay = True
                Me.InitLayout()
            End If
        End Sub

        ''' <summary>
        ''' Stoppt die Animation und beendet Timer sowie ImageAnimator.
        ''' </summary>
        ''' <remarks>
        ''' Setzt <see cref="AutoPlay"/> auf <c>False</c>, ruft <see cref="System.Drawing.ImageAnimator.StopAnimate(System.Drawing.Image,System.EventHandler)"/> für das aktuelle GIF auf und stoppt den internen <see cref="System.Windows.Forms.Timer"/>.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Animation anhalten (Timer und Animator werden beendet)
        ''' AniGif1.StopAnimation()
        '''  
        ''' ' Danach kann z. B. ein anderes Bild gesetzt werden:
        ''' AniGif1.Gif = CType(System.Drawing.Image.FromFile("C:\gif\anderes.gif"), System.Drawing.Bitmap)]]></code>
        ''' </example>
        ''' <seealso cref="StartAnimation"/>
        ''' <seealso cref="AutoPlay"/>
        Public Sub StopAnimation()
            If _Autoplay Then
                _Autoplay = False
                If _Gif IsNot Nothing Then System.Drawing.ImageAnimator.StopAnimate(_Gif, Me._AnimationHandler)
                Me.Timer.Stop()
            End If
        End Sub

        Public Overloads Sub Dispose() Implements System.IDisposable.Dispose
            Me.Dispose(True)
            System.GC.SuppressFinalize(Me)
        End Sub

#End Region

#Region "überschriebene Methoden"

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

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            If _Gif Is Nothing Then Return ' Null-Schutz

            Dim g As System.Drawing.Graphics = e.Graphics ' Variable für Zeichenfläche
            Dim rectstartsize As System.Drawing.Size = Me.GetRectStartSize(_GifSizeMode, Me, _Gif, _ZoomFactor / 100) ' Größe der Zeichenfläche berechnen
            Dim rectstartpoint As System.Drawing.Point = Me.GetRectStartPoint(_GifSizeMode, Me, _Gif, rectstartsize) 'Startpunkt der Zeichenfläche berechnen

            ' Qualitätsverbesserung nur bei Skalierung
            If _GifSizeMode = ImageSizeMode.Zoom OrElse _GifSizeMode = ImageSizeMode.Fill Then
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            End If

            g.DrawImage(_Gif, New System.Drawing.Rectangle(rectstartpoint, rectstartsize)) ' Zeichenfläche festlegen und Bild zeichnen
            If Not Me.DesignMode And _Autoplay And Not _CustomDisplaySpeed Then ' Bild animieren wenn AutoPlay aktiv und Benutzerdefinierte Geschwindigkeit deaktiviert
                System.Drawing.ImageAnimator.UpdateFrames() ' im Bild gespeicherte Geschwindigkeit verwenden
            End If
        End Sub

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

#End Region

#Region "Interne Methoden"

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

        Private Sub AniGif_CustomDisplaySpeedChanged() Handles Me.CustomDisplaySpeedChanged
            ' Intervall direkt setzen und Timer entsprechend Zustand starten/stoppen
            If _CustomDisplaySpeed Then
                Me.Timer.Interval = CInt(1000 / _FramesPerSecond)
                Me.Timer.Start()
            Else
                Me.Timer.Stop()
            End If
        End Sub

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

        Private Sub OnNextFrame(o As Object, e As System.EventArgs)
            If Me.AutoPlay AndAlso Not Me.DesignMode Then
                Me.Invalidate() 'neu zeichnen
            End If
        End Sub

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

        Private Sub SetGifImage(value As System.Drawing.Bitmap)
            ' Vorherige Animation stoppen bevor Bild entsorgt wird
            If _Gif IsNot Nothing Then
                System.Drawing.ImageAnimator.StopAnimate(_Gif, Me._AnimationHandler)
                _Gif.Dispose() ' Vorhandenes Bild freigeben
            End If
            _Gif = If(value, My.Resources.Standard) 'Standardanimation verwenden wenn keine Auswahl erfolgte
            RaiseEvent GifChanged()
        End Sub

        Private Sub SetGifSizeMode(value As ImageSizeMode)
            _GifSizeMode = value
            Me.Invalidate()
        End Sub

        Private Sub SetCustomDisplaySpeed(value As Boolean)
            _CustomDisplaySpeed = value
            RaiseEvent CustomDisplaySpeedChanged()
        End Sub

        Private Sub SetZoomFactor(value As Decimal)
            _ZoomFactor = Me.CheckZoomFactorValue(value)
            Me.Invalidate() 'neu zeichnen
        End Sub

        Private Function CheckFramesPerSecondValue(Frames As Decimal) As Decimal
            ' Überprüft, ob der FPS-Wert im zulässigen Bereich liegt (1 bis 50)
            Select Case Frames
                Case Is < 1 : Return 1' Wenn der Wert kleiner als 1 ist, auf Mindestwert 1 setzen
                Case Is > 50 : Return 50 ' Wenn der Wert größer als 50 ist, auf Höchstwert 50 setzen
                Case Else : Return Frames ' Ansonsten den übergebenen Wert zurückgeben
            End Select
        End Function

        Private Function CheckZoomFactorValue(ZoomFactor As Decimal) As Decimal
            Select Case ZoomFactor
                Case Is < 1 : Return 1 ' Wenn der Zoomfaktor kleiner als 1 ist, auf Mindestwert 1 setzen
                Case Is > 100 : Return 100  ' Wenn der Zoomfaktor größer als 100 ist, auf Höchstwert 100 setzen
                Case Else : Return ZoomFactor  ' Ansonsten den übergebenen Wert zurückgeben
            End Select
        End Function

        Private Function GetRectStartSize(Mode As ImageSizeMode, Control As AniGif, Gif As System.Drawing.Bitmap, Zoom As Decimal) As System.Drawing.Size
            If Gif Is Nothing Then Return System.Drawing.Size.Empty ' Null-Schutz
            Select Case Mode
                Case ImageSizeMode.Normal
                    ' Bild wird in Originalgröße angezeigt (keine Skalierung)
                    Return New System.Drawing.Size(Gif.Size.Width, Gif.Size.Height)
                Case ImageSizeMode.CenterImage
                    ' Bild wird ebenfalls in Originalgröße angezeigt (zentriert, aber Größe bleibt gleich)
                    Return New System.Drawing.Size(Gif.Size.Width, Gif.Size.Height)
                Case ImageSizeMode.Zoom
                    ' Bild wird proportional zum Zoomfaktor skaliert
                    If Gif.Size.Width < Gif.Size.Height Then
                        Return New System.Drawing.Size(CInt(Control.Height / CDec(Gif.Size.Height / Gif.Size.Width) * Zoom), CInt(Control.Height * Zoom))
                    Else
                        ' Bild ist breiter als hoch
                        ' Breite des Controls als Basis, Höhe proportional berechnen und mit Zoom multiplizieren
                        Return New System.Drawing.Size(CInt(Control.Width * Zoom), CInt(Control.Width * CDec(Gif.Size.Height / Gif.Size.Width) * Zoom))
                    End If
                Case ImageSizeMode.Fill
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

        Private Function GetRectStartPoint(Mode As ImageSizeMode, Control As AniGif, Gif As System.Drawing.Bitmap, RectStartSize As System.Drawing.Size) As System.Drawing.Point
            ' Bestimmt den Startpunkt (linke obere Ecke) für das Zeichnen des Bildes
            Select Case Mode
                Case ImageSizeMode.Normal
                    ' Bild wird in Originalgröße oben links gezeichnet
                    Return New System.Drawing.Point(0, 0)
                Case ImageSizeMode.CenterImage
                    ' Bild wird in Originalgröße zentriert gezeichnet
                    ' X-Position: Hälfte der Differenz zwischen Control-Breite und Bild-Breite
                    ' Y-Position: Hälfte der Differenz zwischen Control-Höhe und Bild-Höhe
                    Return New System.Drawing.Point(CInt((Control.Width - Gif.Size.Width) / 2), CInt((Control.Height - Gif.Size.Height) / 2))
                Case ImageSizeMode.Zoom
                    ' Bild wird skaliert (gezoomt) und zentriert gezeichnet
                    ' X-Position: Hälfte der Differenz zwischen Control-Breite und skalierter Bild-Breite
                    ' Y-Position: Hälfte der Differenz zwischen Control-Höhe und skalierter Bild-Höhe
                    Return New System.Drawing.Point(CInt((Control.Width - RectStartSize.Width) / 2), CInt((Control.Height - RectStartSize.Height) / 2))
                Case ImageSizeMode.Fill
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
