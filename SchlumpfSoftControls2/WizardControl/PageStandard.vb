' *************************************************************************************************
' PageStandard.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace WizardControl

    ''' <summary>
    ''' Definiert eine Standardseite.
    ''' </summary>
    ''' <remarks>
    ''' Diese Seite verwendet standardmäßig den Stil <see cref="PageStyle.Standard"/>.
    ''' </remarks>
    ''' <example>
    ''' <code><![CDATA['Beispiel: Verwendung von PageStandard und Zugriff auf die Style-Eigenschaft
    '''     Imports WizardControl
    '''  
    '''     Module ExampleModule
    '''         Sub Main()
    '''             Dim page As New PageStandard()
    '''  
    '''             Stil abfragen(Standard)
    '''             Dim aktuellerStil As PageStyle = page.Style
    '''  
    '''             Stil setzen
    '''             page.Style = PageStyle.Standard
    '''  
    '''             Prüfen des gesetzten Stils
    '''             If page.Style = PageStyle.Standard Then
    '''                 Console.WriteLine("Standardstil ist gesetzt.")
    '''             End If
    '''         End Sub
    '''     End Module
    '''     ]]></code>
    ''' </example>
    <System.ComponentModel.ToolboxItem(False)>
    Public Class PageStandard : Inherits WizardPage

        ' Privates Feld zur Speicherung des Seitenstils.
        Private _Style As PageStyle = PageStyle.Standard

        ''' <summary>
        ''' Ruft den Stil der Assistentenseite ab oder legt diesen fest.
        ''' </summary>
        ''' <remarks>
        ''' Der Standardwert ist <see cref="PageStyle.Standard"/>.
        ''' </remarks>
        ''' <value>
        ''' Der aktuell konfigurierte Stil der Seite als <see cref="PageStyle"/>.
        ''' </value>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Stil lesen und ändern
        ''' Dim page As New WizardControl.PageStandard()
        '''  
        ''' ' Lesen
        ''' Dim stil As WizardControl.PageStyle = page.Style
        '''  
        ''' ' Ändern
        ''' page.Style = WizardControl.PageStyle.Standard]]></code>
        ''' </example>
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