' *************************************************************************************************
' ConstantDefinitions.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ExtendedRTFControl

    ''' <summary>
    ''' Definition der Konstanten
    ''' </summary>
    ''' <remarks></remarks>
    Module ConstantDefinitions

        ''' <summary>
        ''' Win32-Konstante zum De-/Aktivieren des Redraws eines Fenster-Handles.
        ''' </summary>
        Friend Const WM_SETREDRAW As Integer = &HB

        ''' <summary>
        ''' Mindest-Schriftgröße.
        ''' </summary>
        ''' <remarks>
        ''' Kann bei Bedarf angepasst werden.
        ''' </remarks>
        Friend Const MIN_FONT_SIZE As Single = 8.0F

    End Module

End Namespace


