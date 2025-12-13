' *************************************************************************************************
' PageStandard.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace WizardControl

    ''' <summary>
    ''' Definiert eine Standardseite
    ''' </summary>
    <System.ComponentModel.ToolboxItem(False)>
    Public Class PageStandard : Inherits WizardPage

        Private _Style As PageStyle = PageStyle.Standard

        <System.ComponentModel.DefaultValue(PageStyle.Standard)>
        <System.ComponentModel.Category("Design")>
        <System.ComponentModel.Description("Ruft den Stil der Assistentenseite ab oder legt diesen fest.")>
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