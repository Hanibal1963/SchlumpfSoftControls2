' *************************************************************************************************
' VariableDefinitions.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace AniGifControl

    ''' <summary>
    ''' Beinhaltet gemeinsam genutzte Zustandsvariablen für die Anzeige und Steuerung
    ''' animierter GIFs innerhalb des AniGif-Steuerelements.
    ''' </summary>
    ''' <remarks>
    ''' <b>Sichtbarkeit:</b> Alle Member sind <see langword="Friend"/> und stehen nur
    ''' innerhalb der Assembly zur Verfügung.<br/>
    ''' <b>Thread-Sicherheit:</b> Dieser Modulzustand ist nicht threadsicher. Zugriff
    ''' immer vom UI-Thread aus vornehmen.<br/>
    ''' <b>Lebensdauer:</b> Externe Ressourcen (z. B. <see
    ''' cref="System.Drawing.Bitmap"/>) sollten bei Austausch/Entladen korrekt
    ''' freigegeben werden.
    ''' </remarks>
    Module VariableDefinitions

        ''' <summary>
        ''' Das aktuell geladene GIF-Bild.
        ''' </summary>
        ''' <remarks>
        ''' <para>Erwartet typischerweise ein animiertes GIF, kann jedoch auch ein
        ''' statisches GIF enthalten. </para>
        ''' <para>Beim Ersetzen des Bildes sollte das bisherige <see
        ''' cref="System.Drawing.Bitmap"/>-Objekt, sofern nicht mehr benötigt, ordnungsgemäß
        ''' via <see cref="System.IDisposable.Dispose"/> freigegeben werden.</para>
        ''' </remarks>
        Friend _Gif As System.Drawing.Bitmap

        ''' <summary>
        ''' Gibt an, wie das GIF im Steuerelement skaliert bzw. positioniert wird.
        ''' </summary>
        ''' <remarks>
        ''' Der konkrete Effekt hängt von der Implementierung des Steuerelements ab (z. B.
        ''' zentriert, gestreckt, proportional skaliert).
        ''' </remarks>
        Friend _GifSizeMode As SizeMode

        ''' <summary>
        ''' Bestimmt, ob eine benutzerdefinierte Anzeigegeschwindigkeit verwendet wird.
        ''' </summary>
        ''' <remarks>
        ''' <para>Ist dieser Wert <see langword="True"/>, wird die
        ''' Wiedergabe-Geschwindigkeit aus <see cref="_FramesPerSecond"/> abgeleitet.
        ''' </para>
        ''' <para>Andernfalls können frameeigene Delays bzw. Standardwerte verwendet
        ''' werden.</para>
        ''' </remarks>
        ''' <example>
        ''' <code language="vb"><![CDATA[
        ''' _CustomDisplaySpeed = True
        ''' _FramesPerSecond = 30D]]></code>
        ''' </example>
        Friend _CustomDisplaySpeed As Boolean

        ''' <summary>
        ''' Bildwiederholrate (Frames pro Sekunde) für die Animation, sofern <see
        ''' cref="_CustomDisplaySpeed"/> aktiviert ist.
        ''' </summary>
        ''' <remarks>
        ''' <para>Typischer Wertebereich: 1–60 FPS.</para>
        ''' <para>Dezimalwerte sind möglich (z. B. 12,5). </para>
        ''' <para>Negative oder 0-Werte sind ungültig.</para>
        ''' </remarks>
        Friend _FramesPerSecond As Decimal

        ''' <summary>
        ''' Die Dimension der animierten Frames im GIF.
        ''' </summary>
        ''' <remarks>
        ''' Für zeitbasierte Animationen ist normalerweise <see
        ''' cref="System.Drawing.Imaging.FrameDimension.Time"/> relevant.
        ''' </remarks>
        ''' <seealso cref="System.Drawing.Imaging.FrameDimension"/>
        Friend _Dimension As System.Drawing.Imaging.FrameDimension

        ''' <summary>
        ''' Der aktuell ausgewählte Frame-Index.
        ''' </summary>
        ''' <remarks>
        ''' Der Index ist in der Regel 0-basiert und muss im Bereich <c>0</c> bis <c>_MaxFrame - 1</c> liegen.
        ''' </remarks>
        Friend _Frame As Integer

        ''' <summary>
        ''' Die Gesamtanzahl verfügbarer Frames im geladenen GIF.
        ''' </summary>
        ''' <remarks>
        ''' <para>Dieser Wert bestimmt den gültigen Bereich für <see cref="_Frame"/>.
        ''' </para>
        ''' <para>Bei nicht animierten GIFs ist der Wert üblicherweise 1.</para>
        ''' </remarks>
        Friend _MaxFrame As Integer

        ''' <summary>
        ''' Gibt an, ob die Animation automatisch abgespielt wird.
        ''' </summary>
        ''' <remarks>
        ''' Konkreter Startzeitpunkt und -auslöser (z. B. beim Laden des GIFs oder beim
        ''' Anzeigen des Steuerelements) hängen von der Steuerelementlogik ab.
        ''' </remarks>
        Friend _Autoplay As Boolean

        ''' <summary>
        ''' Zoomfaktor für die Darstellung.
        ''' </summary>
        ''' <remarks>
        ''' <para><c>1.0</c> entspricht 100% (Originalgröße), <c>2.0</c> entspricht 200% usw. Werte &lt;= 0 sind ungültig. </para>
        ''' <para>Die Wirkung kann von <see cref="_GifSizeMode"/> abhängen.</para>
        ''' </remarks>
        Friend _ZoomFactor As Decimal

    End Module

End Namespace