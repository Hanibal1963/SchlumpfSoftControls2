
Namespace ShapeControl

    ''' <summary>
    ''' Legt fest welche Form gezeichnet wird
    ''' </summary>
    Public Enum ShapeModes

        ''' <summary>
        ''' Horizontale Linie
        ''' </summary>
        HorizontalLine = 0

        ''' <summary>
        ''' Vertikale Linie
        ''' </summary>
        VerticalLine = 1

        ''' <summary>
        ''' diagonale Linie
        ''' </summary>
        DiagonalLine = 2

        ''' <summary>
        ''' Rechteck
        ''' </summary>
        Rectangle = 3

        ''' <summary>
        ''' gefülltes Rechteck
        ''' </summary>
        FilledRectangle = 4

        ''' <summary>
        ''' Kreis oder Ellipse
        ''' </summary>
        Ellipse = 5

        ''' <summary>
        ''' gefüllter Kreis oder gefüllte Ellipse
        ''' </summary>
        FilledEllipse = 6

    End Enum

    ''' <summary>
    ''' Legt fest in welcher Richtung die diagonale Linie gezeichnet wird
    ''' </summary>
    Public Enum DiagonalLineModes

        ''' <summary>
        ''' von links oben nach rechts unten
        ''' </summary>
        TopLeftToBottomRight = 0

        ''' <summary>
        ''' von links unten nach rechts oben
        ''' </summary>
        BottomLeftToTopRight = 1

    End Enum

End Namespace