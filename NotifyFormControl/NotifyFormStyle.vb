' *************************************************************************************************
' NotifyFormStyle.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace NotifyFormControl

    ''' <summary>
    ''' Auflistung der Styles
    ''' </summary>
    Public Enum NotifyFormStyle As Integer

        ''' <summary>
        ''' Infosymbol
        ''' </summary>
        Information = 0

        ''' <summary>
        ''' Fragesymbol
        ''' </summary>
        Question = 1

        ''' <summary>
        ''' Fehlersymbol
        ''' </summary>
        CriticalError = 2

        ''' <summary>
        ''' Warnungssymbol
        ''' </summary>
        Exclamation = 3

    End Enum

End Namespace