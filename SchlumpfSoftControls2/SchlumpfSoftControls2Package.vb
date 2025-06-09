
Imports System
Imports System.Runtime.InteropServices
Imports System.Threading
Imports Microsoft.VisualBasic
Imports Microsoft.VisualStudio.Shell
Imports Task = System.Threading.Tasks.Task

''' <summary>
''' Dies ist die Klasse, die das von dieser Baugruppe freigelegte Paket implementiert.
''' </summary>
''' <remarks>
''' <para>
''' Die Mindestanforderung, dass eine Klasse als gültiges Paket für Visual Studio angesehen wird
''' Soll die IVS -Paketschnittstelle implementieren und sich bei der Shell registrieren.
''' Dieses Paket verwendet die im verwalteten Paket Framework (MPF) definierten Helferklassen (MPF)
''' Um es zu tun: Es stammt aus der Paketklasse, die die Implementierung der 
''' IVS -Paket -Schnittstelle und verwendet die im Framework definierten Registrierungsattribute zu 
''' registrieren sich und seine Komponenten mit der Shell. Diese Attribute zeigen die PKGDEF -Erstellung
''' "Dienstprogramm", welche Daten in .pkgdef -Datei einfügen sollen.
''' </para>
''' <para>
''' Um in VS geladen zu werden, muss das Paket von & lt; asset type = "microsoft.visual Studio.vs Paket" ... & gt; In .VSIXMANIFEST -Datei.
''' </para>
''' </remarks>
<PackageRegistration(UseManagedResourcesOnly:=True, AllowsBackgroundLoading:=True)>
<Guid(SchlumpfSoftControls2Package.PackageGuidString)>
Public NotInheritable Class SchlumpfSoftControls2Package

    Inherits AsyncPackage

    ''' <summary>
    ''' Paketguid
    ''' </summary>
    Public Const PackageGuidString As String = "a2451894-758b-47f0-bb72-651a271386fe"

#Region "Packungsmitglieder"

    ''' <summary>
    ''' Initialisierung des Pakets; Diese Methode wird direkt nach dem Paket aufgerufen. 
    ''' Dies ist also der Ort wo Sie den gesamten Initialisierungscode einfügen können, 
    ''' der sich auf Dienste stützt, die von Visual Studio bereitgestellt werden.    
    ''' </summary>
    ''' <param name="cancellationToken">
    ''' Ein Stornierungs -Token zur Überwachung der Initialisierungsstornierung, 
    ''' die auftreten kann, wenn VS heruntergefahren wird.
    ''' </param>
    ''' <param name="progress">
    ''' Ein Anbieter für Fortschrittsaktualisierungen.
    ''' </param>
    ''' <returns>
    ''' Eine Aufgabe, die die asynchronisierte Arbeit der Paketinitialisierung darstellt, 
    ''' oder eine bereits abgeschlossene Aufgabe, wenn keine vorhanden ist. 
    ''' NICHT NULL aus dieser Methode zurückgeben.
    ''' </returns>
    Protected Overrides Async Function InitializeAsync(cancellationToken As CancellationToken, progress As IProgress(Of ServiceProgressData)) As Task
        'Wenn asynchron initialisiert wurde, kann der aktuelle Thread an diesem Punkt ein Hintergrund -Thread sein.
        'Führen Sie eine Initialisierung durch, die den UI -Thread nach dem Umschalten zum UI -Thread benötigt. 
        Await Me.JoinableTaskFactory.SwitchToMainThreadAsync()
    End Function

#End Region

End Class
