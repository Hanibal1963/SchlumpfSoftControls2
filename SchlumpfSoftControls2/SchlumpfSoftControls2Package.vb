' *************************************************************************************************
' SchlumpfSoftControls2Package.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.Runtime.InteropServices
Imports System.Threading
'Imports Microsoft.VisualBasic
Imports Microsoft.VisualStudio.Shell
Imports Task = System.Threading.Tasks.Task


''' <summary>
''' Dies ist die Klasse, die das von dieser Assembly bereitgestellte Paket
''' implementiert.
''' </summary>
''' <remarks>
''' <para> Die Mindestanforderung, damit eine Klasse als gültiges Paket für Visual
''' Studio gilt, besteht darin, das IVsPackage-Interface zu implementieren und sich
''' bei der Shell zu registrieren. </para>
''' <para>Dieses Paket verwendet die in der Managed Package Framework (MPF)
''' definierten Hilfsklassen, um dies zu erreichen: </para>
''' <para>Es leitet sich von der Klasse Package ab, die die Implementierung der
''' IVsPackage-Schnittstelle bereitstellt, und verwendet die im Framework
''' definierten Registrierungsattribute, um sich selbst und seine Komponenten bei
''' der Shell zu registrieren. </para>
''' <para>Diese Attribute teilen dem Pkgdef-Erstellungstool mit, welche Daten in die
''' .pkgdef-Datei geschrieben werden sollen. </para>
''' <para> Damit das Paket in VS geladen wird, muss in der .vsixmanifest-Datei mit
''' &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; darauf verwiesen
''' werden. </para>
''' </remarks>
<PackageRegistration(UseManagedResourcesOnly:=True, AllowsBackgroundLoading:=True)>
<Guid(SchlumpfSoftControls2Package.PackageGuidString)>
Public NotInheritable Class SchlumpfSoftControls2Package
    Inherits AsyncPackage

    ''' <summary>
    ''' Paket-GUID
    ''' </summary>
    Public Const PackageGuidString As String = "c5a0e70e-9d56-456c-8627-ae3e1624057a"

#Region "Paketmitglieder"

    ''' <summary>
    ''' <para>Initialisierung des Pakets; diese Methode wird unmittelbar nach dem
    ''' Einbinden (Siting) des Pakets aufgerufen. </para>
    ''' <para>Hier gehört jeglicher Initialisierungscode hin, der von durch Visual
    ''' Studio bereitgestellten Diensten abhängt.</para>
    ''' </summary>
    ''' <param name="cancellationToken">Ein Abbruchtoken zur Überwachung eines Abbruchs
    ''' der Initialisierung, z. B. beim Herunterfahren von VS.</param>
    ''' <param name="progress">Ein Anbieter für Fortschrittsaktualisierungen.</param>
    ''' <returns>
    ''' <para>Eine Aufgabe, die die asynchrone Arbeit der Paketinitialisierung
    ''' darstellt, oder eine bereits abgeschlossene Aufgabe, falls keine Arbeit anfällt.
    ''' </para>
    ''' <para>Geben Sie aus dieser Methode nicht null zurück.</para>
    ''' </returns>
    Protected Overrides Async Function InitializeAsync(cancellationToken As CancellationToken, progress As IProgress(Of ServiceProgressData)) As Task
        ' Bei asynchroner Initialisierung kann der aktuelle Thread zu diesem Zeitpunkt ein Hintergrundthread sein.
        ' Führen Sie Initialisierungen, die den UI-Thread erfordern, erst nach dem Wechsel auf den UI-Thread aus.
        Await Me.JoinableTaskFactory.SwitchToMainThreadAsync()
    End Function

#End Region

End Class
