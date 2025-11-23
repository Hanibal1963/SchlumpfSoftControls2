' *************************************************************************************************
' DiagonalLineModes.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ShapeControl

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