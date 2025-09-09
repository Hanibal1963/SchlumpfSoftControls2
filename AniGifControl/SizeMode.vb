' *************************************************************************************************
' SizeMode.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace AniGifControl

    ''' <summary>
    ''' Auflistung der Anzeigemodi
    ''' </summary>
    Public Enum SizeMode

        ''' <summary>
        ''' Die Grafik wird in Originalgröße angezeigt (Ausrichtung oben links).
        ''' </summary>
        ''' <remarks>
        ''' Wenn die grafik größer als das Control ist, werden nicht anzeigbare Teile abgeschnitten.
        ''' </remarks>
        Normal = 0

        ''' <summary>
        ''' Die Grafik wird in Originalgröße angezeigt (zentrierte Ausrichtung).
        ''' </summary>
        ''' <remarks>
        ''' Wenn die grafik größer als das Control ist, werden nicht anzeigbare Teile abgeschnitten.
        ''' </remarks>
        CenterImage = 1

        ''' <summary>
        ''' Die Größe der Grafik kann an die Größe des Controls angepasst werden (zentrierte Ausrichtung 1-100%).
        ''' </summary>
        ''' <remarks>
        ''' Die Grafik passt immer in das Control.
        ''' </remarks>
        Zoom = 2

        ''' <summary>
        ''' Die Grafik füllt das Control immer vollständig aus (zentrierte Ausrichtung ).
        ''' </summary>
        ''' <remarks>
        ''' Kleinere Grafiken werden gezoomt und größere verkleinert.
        ''' </remarks>
        Fill = 3

    End Enum

End Namespace
