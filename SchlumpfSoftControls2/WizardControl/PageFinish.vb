' *************************************************************************************************
' PageFinish.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace WizardControl

    ''' <summary>
    ''' Definiert die Abschlußseite des Assistenten.
    ''' </summary>
    ''' <remarks>
    ''' Diese Seite wird typischerweise am Ende des Assistenten angezeigt, um Ergebnisse
    ''' zusammenzufassen oder eine Bestätigung bereitzustellen.
    ''' </remarks>
    ''' <example>
    ''' <code><![CDATA[' Beispiel: Verwendung der Abschlussseite im Assistenten
    ''' Dim finishPage As New WizardControl.PageFinish()
    '''  
    ''' ' Stil ist standardmäßig PageStyle.Finish, kann aber explizit gesetzt werden:
    ''' finishPage.Style = PageStyle.Finish
    '''  
    ''' ' Seite zu einem hypothetischen Wizard hinzufügen:
    ''' Dim wizard As New WizardControl.Wizard()
    ''' wizard.Pages.Add(finishPage)
    '''  
    ''' ' Wizard anzeigen/starten
    ''' wizard.Start()]]></code>
    ''' </example>
    <System.ComponentModel.ToolboxItem(False)>
    Public Class PageFinish : Inherits WizardPage

        ' Privates Feld zur Speicherung des Seitenstils.
        Private _Style As PageStyle = PageStyle.Finish

        ''' <summary>
        ''' Ruft den Stil der Assistentenseite ab oder legt diesen fest.
        ''' </summary>
        ''' <remarks>
        ''' Der Standardwert ist <see cref="PageStyle.Finish"/>.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Stil der Abschlussseite lesen und setzen
        ''' Dim p As New WizardControl.PageFinish()
        '''  
        ''' ' Stil auslesen
        ''' Dim aktuellerStil As PageStyle = p.Style
        '''  
        ''' ' Stil setzen (hier bleibt es bei Finish)
        ''' p.Style = PageStyle.Finish]]></code>
        ''' </example>
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