' *************************************************************************************************
' PageWelcome.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace WizardControl

    ''' <summary>
    ''' Definiert die Willkommenseite für den Assistenten.
    ''' </summary>
    ''' <remarks>
    ''' Diese Seite dient als Einstiegsseite und verwendet standardmäßig den Stil <see
    ''' cref="PageStyle.Welcome"/>.
    ''' </remarks>
    ''' <example>
    ''' <code><![CDATA[' Beispiel: Willkommenseite erstellen und dem Assistenten hinzufügen
    ''' Dim welcomePage As New WizardControl.PageWelcome()
    ''' welcomePage.Style = WizardControl.PageStyle.Welcome
    '''  
    ''' ' Annahme: wizard ist eine Instanz eines Assistenten, der WizardPage-Seiten verwaltet
    ''' wizard.Pages.Add(welcomePage)]]></code>
    ''' </example>
    <System.ComponentModel.ToolboxItem(False)>
    Public Class PageWelcome : Inherits WizardPage

        ' Privates Feld zur Speicherung des Seitenstils.
        Private _Style As PageStyle = PageStyle.Welcome

        ''' <summary>
        ''' Ruft den Stil der Assistentenseite ab oder legt diesen fest.
        ''' </summary>
        ''' <remarks>
        ''' Standardwert ist <see cref="PageStyle.Welcome"/>. Die Änderung des Stils kann
        ''' das Layout und die Darstellung der Seite beeinflussen.
        ''' </remarks>
        ''' <value>
        ''' Der aktuell konfigurierte Seitenstil des Assistenten.
        ''' </value>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Stil der Willkommenseite ändern
        ''' Dim page As New WizardControl.PageWelcome()
        '''  
        ''' ' Standard ist Welcome
        ''' Dim aktuellerStil As WizardControl.PageStyle = page.Style
        '''  
        ''' ' Stil setzen (sofern unterstützt)
        ''' page.Style = WizardControl.PageStyle.Welcome]]></code>
        ''' </example>
        <System.ComponentModel.DefaultValue(PageStyle.Welcome)>
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