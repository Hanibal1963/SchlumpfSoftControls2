' *************************************************************************************************
' ProvideToolboxControlAttribute.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
'Imports System.Collections.Generic
Imports System.Globalization
'Imports System.Linq
'Imports System.Text
Imports Microsoft.VisualStudio.Shell

''' <summary>
''' Dieses Attribut fügt für die Assembly einen ToolboxControlsInstaller-Schlüssel
''' hinzu, um Steuerelemente aus der Assembly in die Toolbox zu installieren.
''' </summary>
''' <remarks>
''' <para></para>
''' <para></para>
''' </remarks>
''' <example>
''' Beispiel:<br/>
''' <code><![CDATA[[$(Rootkey)\ToolboxControlsInstaller\$FullAssemblyName$] 
''' "Codebase"="$path$" 
''' "WPFControls"="1"]]></code>
''' <para></para>
''' </example>
<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False, Inherited:=True)>
<System.Runtime.InteropServices.ComVisibleAttribute(False)>
Public NotInheritable Class ProvideToolboxControlAttribute
    Inherits RegistrationAttribute

    Private Const ToolboxControlsInstallerPath As String = "ToolboxControlsInstaller"

    Private _isWpfControls As Boolean
    Private _name As String

    ''' <summary>
    ''' Erstellt ein neues ProvideToolboxControl-Attribut, um die Assembly für den
    ''' Toolbox Controls Installer zu registrieren.
    ''' </summary>
    ''' <param name="name">Anzeigename der Steuerelementgruppe.</param>
    ''' <param name="isWpfControls">True, wenn es sich um WPF-Steuerelemente handelt;
    ''' andernfalls False.</param>
    Public Sub New(name As String, isWpfControls As Boolean)
        If name Is Nothing Then
            Throw New ArgumentException("name")
        End If

        Me.Name = name
        Me.IsWpfControls = isWpfControls
    End Sub

    ''' <summary>
    ''' Gibt an, ob die Toolbox-Steuerelemente für WPF sind.
    ''' </summary>
    Private Property IsWpfControls As Boolean
        Get
            Return Me._isWpfControls
        End Get
        Set(value As Boolean)
            Me._isWpfControls = value
        End Set
    End Property

    ''' <summary>
    ''' Ruft den Namen für die Steuerelemente ab bzw. legt ihn fest.
    ''' </summary>
    Private Property Name As String
        Get
            Return Me._name
        End Get
        Set(value As String)
            Me._name = value
        End Set
    End Property

    ''' <summary>
    ''' <para>Registriert dieses Attribut mit dem angegebenen Kontext.</para>
    ''' <para>Der Kontext enthält den Speicherort, an dem die
    ''' Registrierungsinformationen abgelegt werden sollen.</para>
    ''' <para>Er enthält außerdem Informationen wie den zu registrierenden Typ und
    ''' Pfadinformationen.</para>
    ''' </summary>
    ''' <param name="context">Der Kontext, in dem registriert werden soll.</param>
    Public Overrides Sub Register(context As RegistrationAttribute.RegistrationContext)
        If context Is Nothing Then
            Throw New ArgumentNullException("context")
        End If
        Using key As Key = context.CreateKey(String.Format(CultureInfo.InvariantCulture, "{0}\{1}", ToolboxControlsInstallerPath, context.ComponentType.Assembly.FullName))
            key.SetValue(String.Empty, Me.Name)
            key.SetValue("Codebase", context.CodeBase)
            If Me.IsWpfControls Then
                key.SetValue("WPFControls", "1")
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Hebt die Registrierung dieses Attributs mit dem angegebenen Kontext auf.
    ''' </summary>
    ''' <param name="context"><para>Ein von einem externen Registrierungstool
    ''' bereitgestellter Registrierungskontext.</para>
    ''' <para>Der Kontext kann verwendet werden, um Registrierungsschlüssel zu
    ''' entfernen, die Registrierungsaktivität zu protokollieren und Informationen über
    ''' die zu registrierende Komponente abzurufen.</para></param>
    Public Overrides Sub Unregister(context As RegistrationAttribute.RegistrationContext)
        If context IsNot Nothing Then
            context.RemoveKey(String.Format(CultureInfo.InvariantCulture, "{0}\{1}", ToolboxControlsInstallerPath, context.ComponentType.Assembly.FullName))
        End If
    End Sub
End Class