Imports System
Imports System.Runtime.InteropServices
Imports System.Threading
Imports Microsoft.VisualBasic
Imports Microsoft.VisualStudio.Shell
Imports Task = System.Threading.Tasks.Task


''' <summary>
''' Diese Klasse implementiert das von diesem Assembly bereitgestellte Package.
''' </summary>
''' <remarks>
''' <para> Die Mindestanforderung für eine Klasse, um als gültiges Package für
''' Visual Studio zu gelten, ist die Implementierung des IVsPackage-Interfaces und
''' die Registrierung bei der Shell. </para>
''' <para>Dieses Package verwendet die Hilfsklassen aus dem Managed Package
''' Framework (MPF), um dies zu tun: </para>
''' <para>Es erbt von der Package-Klasse, die die Implementierung des
''' IVsPackage-Interfaces bereitstellt, und verwendet die im Framework definierten
''' Registrierungsattribute, </para>
''' <para>um sich selbst und seine Komponenten bei der Shell zu registrieren.
''' </para>
''' <para>Diese Attribute geben dem pkgdef-Erstellungstool an, welche Daten in die
''' .pkgdef-Datei geschrieben werden sollen. </para>
''' <para> Damit das Package in VS geladen wird, muss es im .vsixmanifest mit
''' &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; referenziert werden.
''' </para>
''' </remarks>
<PackageRegistration(UseManagedResourcesOnly:=True, AllowsBackgroundLoading:=True)>
<Guid(TransparentLabelControlPackage.PackageGuidString)>
Public NotInheritable Class TransparentLabelControlPackage
    Inherits AsyncPackage

    ''' <summary>
    ''' Package-GUID
    ''' </summary>
    Public Const PackageGuidString As String = "41fe6750-9b7b-4ebc-a241-c68d131e0306"

#Region "Package Members"

    ''' <summary>
    ''' <para>Initialisierung des Packages; diese Methode wird direkt nach dem Einbinden
    ''' des Packages aufgerufen. </para>
    ''' <para>Hier kann sämtlicher Initialisierungscode platziert werden, der auf von
    ''' Visual Studio bereitgestellte Dienste angewiesen ist.</para>
    ''' </summary>
    ''' <param name="cancellationToken">Ein CancellationToken zur Überwachung einer
    ''' möglichen Abbruchanforderung während der Initialisierung, z.B. beim
    ''' Herunterfahren von VS.</param>
    ''' <param name="progress">Ein Provider für Fortschrittsmeldungen.</param>
    ''' <returns>
    ''' <para>Ein Task, der die asynchrone Initialisierung des Packages repräsentiert,
    ''' oder ein bereits abgeschlossener Task, falls keine Initialisierung erforderlich
    ''' ist. </para>
    ''' <para>Es darf kein null zurückgegeben werden.</para>
    ''' </returns>
    Protected Overrides Async Function InitializeAsync(cancellationToken As CancellationToken, progress As IProgress(Of ServiceProgressData)) As Task
        ' Bei asynchroner Initialisierung kann der aktuelle Thread zu diesem Zeitpunkt ein Hintergrundthread sein.
        ' Jegliche Initialisierung, die den UI-Thread benötigt, sollte nach dem Umschalten auf den UI-Thread erfolgen.
        Await Me.JoinableTaskFactory.SwitchToMainThreadAsync()
    End Function

#End Region

End Class
