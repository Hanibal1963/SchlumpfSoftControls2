' *************************************************************************************************
' PageFinish.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace WizardControl

    ''' <summary>
    ''' Definiert die Abschlußseite
    ''' </summary>
    <System.ComponentModel.ToolboxItem(False)>
    Public Class PageFinish : Inherits WizardPage

        Private _Style As PageStyle = PageStyle.Finish

        <System.ComponentModel.DefaultValue(PageStyle.Finish)>
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