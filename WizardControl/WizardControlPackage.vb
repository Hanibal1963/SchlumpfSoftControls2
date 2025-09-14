Imports System
Imports System.Runtime.InteropServices
Imports System.Threading
Imports Microsoft.VisualBasic
Imports Microsoft.VisualStudio.Shell
Imports Task = System.Threading.Tasks.Task


''' <summary>
''' Dies ist die Klasse, die das von dieser Assembly bereitgestellte Package
''' implementiert.
''' </summary>
''' <remarks>
''' <para> Die Mindestanforderung, damit eine Klasse als gültiges Package für Visual
''' Studio gilt, ist die Implementierung des IVsPackage-Interfaces und die
''' Registrierung bei der Shell. </para>
''' <para>Dieses Package verwendet die Hilfsklassen aus dem Managed Package
''' Framework (MPF), um dies zu tun: </para>
''' <para>Es erbt von der Package-Klasse, die die Implementierung des
''' IVsPackage-Interfaces bereitstellt, und verwendet die im Framework definierten
''' Registrierungsattribute, </para>
''' <para>um sich selbst und seine Komponenten bei der Shell zu registrieren.
''' </para>
''' <para>Diese Attribute geben dem pkgdef-Erstellungsprogramm an, welche Daten in
''' die .pkgdef-Datei geschrieben werden sollen. </para>
''' <para> Damit das Package in VS geladen wird, muss es im .vsixmanifest mit
''' &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; referenziert werden.
''' </para>
''' </remarks>
<PackageRegistration(UseManagedResourcesOnly:=True, AllowsBackgroundLoading:=True)>
<Guid(WizardControlPackage.PackageGuidString)>
Public NotInheritable Class WizardControlPackage
    Inherits AsyncPackage

    ''' <summary>
    ''' Package-GUID
    ''' </summary>
    Public Const PackageGuidString As String = "b9f2da36-3836-4cfe-a7b7-d79d55c3c261"

#Region "Package Members"

    ''' <summary>
    ''' <para>Initialisierung des Packages; diese Methode wird direkt nach dem Einbinden
    ''' des Packages aufgerufen. </para>
    ''' <para>Hier können Sie jeglichen Initialisierungscode einfügen, der auf von
    ''' Visual Studio bereitgestellte Dienste angewiesen ist.</para>
    ''' </summary>
    ''' <param name="cancellationToken">Ein CancellationToken, um die
    ''' Initialisierungsabbruch zu überwachen, z. B. wenn VS heruntergefahren
    ''' wird.</param>
    ''' <param name="progress">Ein Provider für Fortschrittsmeldungen.</param>
    ''' <returns>
    ''' Ein Task, der die asynchrone Initialisierung des Packages repräsentiert, oder
    ''' ein bereits abgeschlossener Task, falls keine Initialisierung erforderlich ist.
    ''' Geben Sie niemals null zurück.
    ''' </returns>
    Protected Overrides Async Function InitializeAsync(cancellationToken As CancellationToken, progress As IProgress(Of ServiceProgressData)) As Task
        ' Wenn asynchron initialisiert wird, kann der aktuelle Thread zu diesem Zeitpunkt ein Hintergrundthread sein.
        ' Führen Sie jegliche Initialisierung, die den UI-Thread benötigt, erst nach dem Wechsel auf den UI-Thread aus.
        Await Me.JoinableTaskFactory.SwitchToMainThreadAsync()
    End Function

#End Region

End Class
