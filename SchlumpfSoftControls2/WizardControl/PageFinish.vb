' *************************************************************************************************
' PageFinish.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System.ComponentModel

Namespace WizardControl

    ''' <summary>
    ''' Definiert die Abschlußseite
    ''' </summary>
    <ToolboxItem(False)>
    Public Class PageFinish

        Inherits WizardPage

        Private _Style As PageStyle = PageStyle.Finish

        <DefaultValue(PageStyle.Finish)>
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