' ****************************************************************************************************************
' ProvideToolboxControlAttribute.vb
' © 2024 - 2025 by Andreas Sauer
' ****************************************************************************************************************
'

Imports System
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
Public NotInheritable Class ProvideToolboxControlAttribute : Inherits RegistrationAttribute

    ''' <summary>
    ''' Der Registrierungspfad für Toolbox Controls Installer in der Registry.
    ''' </summary>
    Private Const ToolboxControlsInstallerPath As String = "ToolboxControlsInstaller"

    ''' <summary>
    ''' Ruft einen Wert ab, der angibt, ob die Toolbox-Steuerelemente für WPF gelten.
    ''' </summary>
    ''' <value>
    ''' <c>true</c>, wenn die Steuerelemente WPF-Steuerelemente sind; andernfalls <c>false</c>.
    ''' </value>
    Private Property IsWpfControls As Boolean

    ''' <summary>
    ''' Ruft den Namen für die Steuerelemente ab oder legt ihn fest.
    ''' </summary>
    ''' <value>
    ''' Der Name, der für die Registrierung der Toolbox-Steuerelemente verwendet wird.
    ''' </value>
    Private Property Name As String

    ''' <summary>
    ''' Erstellt ein neues Attribut „Provide Toolbox Control", um die Assembly für das 
    ''' Toolbox Controls-Installationsprogramm zu registrieren.
    ''' </summary>
    ''' <param name="name">Der Name für die Toolbox-Steuerelemente. Darf nicht <c>null</c> sein.</param>
    ''' <param name="isWpfControls">
    ''' <c>true</c>, wenn es sich um WPF-Steuerelemente handelt; andernfalls <c>false</c> für WinForms-Steuerelemente.
    ''' </param>
    ''' <exception cref="ArgumentException">
    ''' Wird ausgelöst, wenn <paramref name="name"/> <c>null</c> ist.
    ''' </exception>
    Public Sub New(name As String, isWpfControls As Boolean)

        ' Eingabevalidierung: Überprüfung des name-Parameters auf Null-Wert
        ' Ein leerer Name würde zu Problemen bei der Registry-Registrierung führen
        ' Visual Studio benötigt einen gültigen Namen, um die Controls in der Toolbox anzuzeigen
        If name Is Nothing Then

            ' Auslösen einer ArgumentException bei ungültigem Parameter
            ' Dies ist eine frühe Fehlererkennung zur Laufzeit
            ' Entwickler werden sofort über den Fehler informiert
            Throw New ArgumentException("name")

        End If

        ' Zuweisung des validierten Namens an die private Property
        ' Dieser Name wird später in der Register()-Methode als Anzeigename
        ' in der Registry gespeichert und von Visual Studio verwendet
        Me.Name = name

        ' Speicherung der Control-Art (WPF oder WinForms)
        ' Diese Information bestimmt, wie Visual Studio die Controls behandelt:
        ' - WPF Controls: Werden in WPF-Projekten in der Toolbox angezeigt
        ' - WinForms Controls: Werden in WinForms-Projekten in der Toolbox angezeigt
        Me.IsWpfControls = isWpfControls

        ' Hinweis: Keine weitere Initialisierung erforderlich
        ' Die Basisklasse RegistrationAttribute wird automatisch initialisiert

    End Sub

    ''' <summary>
    ''' Wird aufgerufen, um dieses Attribut im angegebenen Kontext zu registrieren. 
    ''' Der Kontext enthält den Ort, an dem die Registrierungsinformationen 
    ''' platziert werden sollen. 
    ''' Es enthält auch andere Informationen wie den zu registrierenden Typ und Pfadinformationen.
    ''' </summary>
    ''' <param name="context">
    ''' Vorgegebener Kontext für die Registrierung. Darf nicht <c>null</c> sein.
    ''' </param>
    ''' <exception cref="ArgumentNullException">
    ''' Wird ausgelöst, wenn <paramref name="context"/> <c>null</c> ist.
    ''' </exception>
    ''' <remarks>
    ''' Diese Methode erstellt einen Registrierungsschlüssel unter dem Pfad
    ''' "ToolboxControlsInstaller\{AssemblyFullName}" und setzt die entsprechenden Werte
    ''' für Codebase und WPF-Steuerelemente (falls zutreffend).
    ''' </remarks>
    Public Overrides Sub Register(context As RegistrationAttribute.RegistrationContext)

        ' Validierung: Sicherstellen, dass ein gültiger Kontext übergeben wurde
        ' Der Kontext ist erforderlich für alle Registrierungsoperationen
        If context Is Nothing Then
            Throw New ArgumentNullException("context")
        End If

        ' Using-Statement für automatische Ressourcenverwaltung
        ' Das Key-Objekt wird automatisch disposed, auch wenn eine Exception auftritt
        Using key As Key = context.CreateKey(
            String.Format(
            Globalization.CultureInfo.InvariantCulture, "{0}\{1}",  ' InvariantCulture stellt sicher, dass die Formatierung unabhängig von den Systemeinstellungen konsistent ist
            ToolboxControlsInstallerPath,  ' "ToolboxControlsInstaller" - Hauptpfad
            context.ComponentType.Assembly.FullName)) ' Assembly-Name als Subkey 
            ' Beispiel-Pfad: "ToolboxControlsInstaller\MyAssembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"

            ' Setzen des Standard-Wertes (leerer Name) auf den Control-Namen
            ' Dies ist der Anzeigename, der in Visual Studio verwendet wird
            key.SetValue(String.Empty, Me.Name)

            ' Setzen der Codebase - der physische Pfad zur Assembly-Datei
            ' Visual Studio benötigt diese Information, um die Assembly zu laden
            key.SetValue("Codebase", context.CodeBase)

            ' Bedingte Registrierung als WPF-Controls
            ' Nur wenn IsWpfControls = True wird dieser Wert gesetzt
            If IsWpfControls Then

                ' Markierung als WPF-Controls durch Setzen des Wertes auf "1"
                ' Dies teilt Visual Studio mit, dass es sich um WPF-Controls handelt
                key.SetValue("WPFControls", "1")

            End If
            ' Wenn IsWpfControls = False, wird kein "WPFControls"-Wert gesetzt
            ' Visual Studio behandelt dies dann als WinForms-Controls

        End Using
        ' Am Ende des Using-Blocks wird key.Dispose() automatisch aufgerufen
        ' Dies stellt sicher, dass alle Registry-Handles ordnungsgemäß geschlossen werden

    End Sub

    ''' <summary>
    ''' Wird aufgerufen, um die Registrierung dieses Attributs im angegebenen Kontext aufzuheben.
    ''' </summary>
    ''' <param name="context">
    ''' Ein Registrierungskontext, der von einem externen Registrierungstool bereitgestellt wird. 
    ''' Der Kontext kann verwendet werden, um Registrierungsschlüssel zu entfernen, 
    ''' Registrierungsaktivitäten zu protokollieren und Informationen 
    ''' über die registrierte Komponente abzurufen. Kann <c>null</c> sein.
    ''' </param>
    ''' <remarks>
    ''' Diese Methode entfernt den Registrierungsschlüssel unter dem Pfad
    ''' "ToolboxControlsInstaller\{AssemblyFullName}", der zuvor durch die
    ''' <see cref="Register"/> Methode erstellt wurde.
    ''' Wenn der Kontext <c>null</c> ist, wird keine Aktion durchgeführt.
    ''' </remarks>
    Public Overrides Sub Unregister(context As RegistrationAttribute.RegistrationContext)

        ' Überprüfung, ob ein gültiger Kontext vorhanden ist
        ' Dies verhindert NullReferenceException und stellt sicher,
        ' dass nur dann eine Deregistrierung erfolgt, wenn sie möglich ist
        If context IsNot Nothing Then

            ' Entfernung des Registry-Schlüssels für diese Assembly
            ' Der Schlüssel wurde ursprünglich in der Register()-Methode erstellt
            context.RemoveKey(
                String.Format(
                System.Globalization.CultureInfo.InvariantCulture, "{0}\{1}", ' InvariantCulture stellt sicher, dass die Formatierung unabhängig von den Systemeinstellungen konsistent ist
                ToolboxControlsInstallerPath, ' Konstante: "ToolboxControlsInstaller"
                context.ComponentType.Assembly.FullName)) ' Vollständiger Assembly-Name als Unterschlüssel

            ' Resultierender Pfad: "ToolboxControlsInstaller\AssemblyName, Version=..., Culture=..., PublicKeyToken=..."

        End If
        ' Wenn context Nothing ist, wird stillschweigend nichts unternommen
        ' Dies ist ein defensives Programmierverhalten für robuste Deregistrierung

    End Sub

End Class