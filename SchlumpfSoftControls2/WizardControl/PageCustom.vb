' *************************************************************************************************
' PageCustom.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace WizardControl

    ''' <summary>
    ''' Definiert eine benutzerdefinierte Assistentenseite.
    ''' </summary>
    ''' <remarks>
    ''' Verwenden Sie diese Klasse, um eigene Inhalte und Verhalten innerhalb eines
    ''' Assistenten bereitzustellen.
    ''' </remarks>
    ''' <example>
    ''' <code><![CDATA[' Beispiel: Erstellen einer benutzerdefinierten Seite und Setzen des Stils
    ''' Dim page As New WizardControl.PageCustom()
    ''' page.Style = WizardControl.PageStyle.Custom
    ''' ' Seite zur Wizard-Steuerung hinzufügen (vereinfachtes Beispiel)
    ''' ' wizard.Pages.Add(page)]]></code>
    ''' </example>
    <System.ComponentModel.ToolboxItem(False)>
    Public Class PageCustom : Inherits WizardPage

        ' Interner Speicher für den Seitenstil (Standard: Custom)
        Private _Style As PageStyle = PageStyle.Custom

        ''' <summary>
        ''' Ruft den Stil der Assistentenseite ab oder legt diesen fest.
        ''' </summary>
        ''' <remarks>
        ''' Der Stil steuert das Erscheinungsbild und Verhalten der Seite innerhalb des
        ''' Assistenten.<br/>
        ''' Der Standardwert ist <see cref="WizardControl.PageStyle.Custom"/>.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Stil einer vorhandenen Seite auslesen und ändern
        ''' Dim page As New WizardControl.PageCustom()
        ''' ' Aktuellen Stil ermitteln
        ''' Dim currentStyle As WizardControl.PageStyle = page.Style
        ''' ' Stil ändern
        ''' page.Style = WizardControl.PageStyle.Custom
        ''' ' Optional: Weitere Eigenschaften basierend auf dem Stil setzen
        ''' ' If page.Style = WizardControl.PageStyle.Custom Then
        ''' '     ' Benutzerdefinierte Initialisierung
        ''' ' End If]]></code>
        ''' </example>
        <System.ComponentModel.DefaultValue(PageStyle.Custom)>
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