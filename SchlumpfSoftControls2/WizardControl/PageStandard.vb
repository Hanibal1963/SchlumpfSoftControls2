' *************************************************************************************************
' PageStandard.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System.ComponentModel

Namespace WizardControl
    ''' <summary>
    ''' Definiert eine Standardseite
    ''' </summary>
    <ToolboxItem(False)>
    Public Class PageStandard

        Inherits WizardPage

        Private _Style As PageStyle = PageStyle.Standard

        <DefaultValue(PageStyle.Standard)>
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