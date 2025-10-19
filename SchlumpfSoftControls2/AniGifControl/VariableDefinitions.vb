' *************************************************************************************************
' VariableDefinitions.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace AniGifControl

    Module VariableDefinitions

        Friend _Gif As System.Drawing.Bitmap ' Das aktuell geladene GIF-Bild
        Friend _GifSizeMode As SizeMode ' Gibt an, wie das GIF im Steuerelement skaliert/angezeigt wird (z.B. gestreckt, zentriert)
        Friend _CustomDisplaySpeed As Boolean ' Bestimmt, ob eine benutzerdefinierte Anzeigegeschwindigkeit verwendet wird
        Friend _FramesPerSecond As Decimal ' Bildwiederholrate (Frames pro Sekunde) für die Animation
        Friend _Dimension As System.Drawing.Imaging.FrameDimension ' Die Dimension (z.B. Zeit) der animierten Frames im GIF
        Friend _Frame As Integer ' Der aktuelle Frame-Index, der angezeigt wird
        Friend _MaxFrame As Integer ' Die maximale Anzahl der Frames im GIF
        Friend _Autoplay As Boolean ' Gibt an, ob die Animation automatisch abgespielt wird
        Friend _ZoomFactor As Decimal ' Zoomfaktor für die Anzeige des GIFs

    End Module

End Namespace