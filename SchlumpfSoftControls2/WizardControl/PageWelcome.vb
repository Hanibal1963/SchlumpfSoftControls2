' *************************************************************************************************
' PageWelcome.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

' TODO: Code noch überarbeiten

Imports System.ComponentModel

Namespace WizardControl

    ''' <summary>
    ''' Definiert die Willkommenseite
    ''' </summary>
    <ToolboxItem(False)>
    Public Class PageWelcome

        Inherits WizardPage

        Private _Style As PageStyle = PageStyle.Welcome

        <DefaultValue(PageStyle.Welcome)>
        <Category("Design")>
        <Description("Ruft den Stil der Assistentenseite ab oder legt diesen fest.")>
        Public Overrides Property Style As PageStyle
            Get
                Return Me._Style
            End Get
            Set(value As PageStyle)
                Me._Style = value
            End Set
        End Property

    End Class

End Namespace