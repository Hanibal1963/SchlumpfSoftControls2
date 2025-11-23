' *************************************************************************************************
' EnumDefinitions.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

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

        ''' <summary>
        ''' Kein Segment aktiv (alles aus).
        ''' </summary>
        None = &H0

        ''' <summary>
        ''' Darstellung der Ziffer 0.
        ''' </summary>
        Zero = &H77

        ''' <summary>
        ''' Darstellung der Ziffer 1.
        ''' </summary>
        One = &H24

        ''' <summary>
        ''' Darstellung der Ziffer 2.
        ''' </summary>
        Two = &H5D

        ''' <summary>
        ''' Darstellung der Ziffer 3.
        ''' </summary>
        Three = &H6D

        ''' <summary>
        ''' Darstellung der Ziffer 4.
        ''' </summary>
        Four = &H2E

        ''' <summary>
        ''' Darstellung der Ziffer 5.
        ''' </summary>
        Five = &H6B

        ''' <summary>
        ''' Darstellung der Ziffer 6.
        ''' </summary>
        Six = &H7B

        ''' <summary>
        ''' Darstellung der Ziffer 7.
        ''' </summary>
        Seven = &H25

        ''' <summary>
        ''' Darstellung der Ziffer 8 (alle Segmente an).
        ''' </summary>
        Eight = &H7F

        ''' <summary>
        ''' Darstellung der Ziffer 9.
        ''' </summary>
        Nine = &H6F

        ''' <summary>
        ''' Großbuchstabe A.
        ''' </summary>
        A = &H3F

        ''' <summary>
        ''' Großbuchstabe B.
        ''' </summary>
        B = &H7A

        ''' <summary>
        ''' Großbuchstabe C.
        ''' </summary>
        C = &H53

        ''' <summary>
        ''' Kleinbuchstabe c (abgekürzte Form / Feldbezeichnung).
        ''' </summary>
        cField = &H58

        ''' <summary>
        ''' Großbuchstabe D.
        ''' </summary>
        D = &H7C

        ''' <summary>
        ''' Großbuchstabe E.
        ''' </summary>
        E = &H5B

        ''' <summary>
        ''' Großbuchstabe F.
        ''' </summary>
        F = &H1B

        ''' <summary>
        ''' Großbuchstabe G.
        ''' </summary>
        G = &H73

        ''' <summary>
        ''' Großbuchstabe H.
        ''' </summary>
        H = &H3E

        ''' <summary>
        ''' Kleinbuchstabe h (abgekürzte Form / Feldbezeichnung).
        ''' </summary>
        hField = &H3A

        ''' <summary>
        ''' Kleinbuchstabe i.
        ''' </summary>
        i = &H20

        ''' <summary>
        ''' Großbuchstabe J.
        ''' </summary>
        J = &H74

        ''' <summary>
        ''' Großbuchstabe L.
        ''' </summary>
        L = &H52

        ''' <summary>
        ''' Großbuchstabe N.
        ''' </summary>
        N = &H38

        ''' <summary>
        ''' Kleinbuchstabe o.
        ''' </summary>
        o = &H78

        ''' <summary>
        ''' Großbuchstabe P.
        ''' </summary>
        P = &H1F

        ''' <summary>
        ''' Großbuchstabe Q.
        ''' </summary>
        Q = &H2F

        ''' <summary>
        ''' Großbuchstabe R.
        ''' </summary>
        R = &H18

        ''' <summary>
        ''' Großbuchstabe T.
        ''' </summary>
        T = &H5A

        ''' <summary>
        ''' Großbuchstabe U.
        ''' </summary>
        U = &H76

        ''' <summary>
        ''' Kleinbuchstabe u (abgekürzte Form / Feldbezeichnung).
        ''' </summary>
        uField = &H70

        ''' <summary>
        ''' Großbuchstabe Y.
        ''' </summary>
        Y = &H6E

        ''' <summary>
        ''' Bindestrich / Minuszeichen.
        ''' </summary>
        Dash = &H8

        ''' <summary>
        ''' Gleichheitszeichen (=).
        ''' </summary>
        Equals = &H48

        ''' <summary>
        ''' Gradzeichen (°).
        ''' </summary>
        Degrees = &HF

        ''' <summary>
        ''' Apostroph (').
        ''' </summary>
        Apostrophe = &H2

        ''' <summary>
        ''' Anführungszeichen (").
        ''' </summary>
        Quote = &H6

        ''' <summary>
        ''' Rechte Klammer (]).
        ''' </summary>
        RBracket = &H65

        ''' <summary>
        ''' Unterstrich (_).
        ''' </summary>
        Underscore = &H40

        ''' <summary>
        ''' Identisch-Zeichen (≡).
        ''' </summary>
        Identical = &H49

        ''' <summary>
        ''' Logisches NOT-Zeichen (¬).
        ''' </summary>
        [Not] = &H28

    End Enum

End Namespace
