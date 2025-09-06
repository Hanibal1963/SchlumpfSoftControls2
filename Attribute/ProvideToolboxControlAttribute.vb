' ****************************************************************************************************************
' ProvideToolboxControlAttribute.vb
' © 2024 by Andreas Sauer
' ****************************************************************************************************************
'

Imports System.Runtime.InteropServices
Imports Microsoft.VisualStudio.Shell

''' <summary>
''' Dieses Attribut fügt der Assembly einen Toolbox Controls Installer-Schlüssel hinzu, 
''' um Toolbox Controls aus der Assembly zu installieren.
''' </summary>
''' <remarks>
''' Zum Beispiel:
'''     [$(Rootkey)\ToolboxControlsInstaller\$FullAssemblyName$]
'''         "Codebase"="$path$"
'''         "WpfControls"="1"
''' </remarks>
<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False, Inherited:=True)>
<ComVisible(False)>
Public NotInheritable Class ProvideToolboxControlAttribute

    Inherits RegistrationAttribute

    Private Const ToolboxControlsInstallerPath As String = "ToolboxControlsInstaller"
    Private _isWpfControls As Boolean
    Private _name As String

    ''' <summary>
    ''' Erstellt ein neues Attribut „Provide Toolbox Control“, um die Assembly für das 
    ''' Toolbox Controls-Installationsprogramm zu registrieren.
    ''' </summary>
    ''' <param name="isWpfControls">
    ''' </param>
    Public Sub New(name As String, isWpfControls As Boolean)

        If name Is Nothing Then
            Throw New ArgumentException("name")
        End If

        Me.Name = name
        Me.IsWpfControls = isWpfControls

    End Sub

    ''' <summary>
    ''' Ruft ab, ob die Toolbox-Steuerelemente für WPF gelten.
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
    ''' Ruft den Namen für die Steuerelemente ab.
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
    ''' Wird aufgerufen, um dieses Attribut im angegebenen Kontext zu registrieren. 
    ''' Der Kontext enthält den Ort, an dem die Registrierungsinformationen 
    ''' platziert werden sollen. 
    ''' Es enthält auch andere Informationen wie den zu registrierenden Typ und Pfadinformationen.
    ''' </summary>
    ''' <param name="context">
    ''' Vorgegebener Kontext für die Registrierung.
    ''' </param>
    Public Overrides Sub Register(context As RegistrationAttribute.RegistrationContext)

        If context Is Nothing Then
            Throw New ArgumentNullException("context")
        End If

        Using key As Key = context.CreateKey(
            String.Format(
            Globalization.CultureInfo.InvariantCulture, "{0}\{1}",
            ToolboxControlsInstallerPath, context.ComponentType.Assembly.FullName))

            key.SetValue(String.Empty, Me.Name)
            key.SetValue("Codebase", context.CodeBase)
            If Me.IsWpfControls Then
                key.SetValue("WPFControls", "1")
            End If

        End Using

    End Sub

    ''' <summary>
    ''' Wird aufgerufen, um die Registrierung dieses Attributs im angegebenen Kontext aufzuheben.
    ''' </summary>
    ''' <param name="context">
    ''' Ein Registrierungskontext, der von einem externen Registrierungstool bereitgestellt wird. 
    ''' Der Kontext kann verwendet werden, um Registrierungsschlüssel zu entfernen, 
    ''' Registrierungsaktivitäten zu protokollieren und Informationen 
    ''' über die registrierte Komponente abzurufen.
    ''' </param>
    Public Overrides Sub Unregister(context As RegistrationAttribute.RegistrationContext)

        If context IsNot Nothing Then

            context.RemoveKey(
                String.Format(
                System.Globalization.CultureInfo.InvariantCulture, "{0}\{1}",
                ToolboxControlsInstallerPath, context.ComponentType.Assembly.FullName))

        End If

    End Sub

End Class