' *************************************************************************************************
' EnumDefinitionsvb
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

    ''' <summary>
    ''' Auflistung der Designs
    ''' </summary>
    Public Enum NotifyFormDesign As Integer

        ''' <summary>
        ''' Helles Design
        ''' </summary>
        Bright = 0

        ''' <summary>
        ''' Farbiges Design
        ''' </summary>
        Colorful = 1

        ''' <summary>
        ''' Dunkles Design
        ''' </summary>
        Dark = 2

    End Enum

End Namespace

