' *************************************************************************************************
' EnumDefinitions.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

' TODO: Code noch überarbeiten

Namespace SevenSegmentControl

    ''' <summary>
    ''' <para>Dies sind die verschiedenen Bitmuster, die die Zeichen darstellen, </para>
    ''' <para>die in den sieben Segmenten angezeigt werden können.<br/>
    ''' </para>
    ''' </summary>
    ''' <remarks>
    ''' Die Bits 0 bis 6 entsprechen den einzelnen LEDs, von oben nach unten!
    ''' </remarks>
    Friend Enum CharacterPattern
        None = &H0
        Zero = &H77
        One = &H24
        Two = &H5D
        Three = &H6D
        Four = &H2E
        Five = &H6B
        Six = &H7B
        Seven = &H25
        Eight = &H7F
        Nine = &H6F
        A = &H3F
        B = &H7A
        C = &H53
        cField = &H58
        D = &H7C
        E = &H5B
        F = &H1B
        G = &H73
        H = &H3E
        hField = &H3A
        i = &H20
        J = &H74
        L = &H52
        N = &H38
        o = &H78
        P = &H1F
        Q = &H2F
        R = &H18
        T = &H5A
        U = &H76
        uField = &H70
        Y = &H6E
        Dash = &H8
        Equals = &H48
        Degrees = &HF
        Apostrophe = &H2
        Quote = &H6
        RBracket = &H65
        Underscore = &H40
        Identical = &H49
        [Not] = &H28
    End Enum

End Namespace
