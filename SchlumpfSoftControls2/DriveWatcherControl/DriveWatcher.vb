' *************************************************************************************************
' DriveWatcher.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace DriveWatcherControl

    ''' <summary>
    ''' Steuerelement zum Überwachen von logischen Laufwerken (Volumes).<br/>
    ''' Erkennt das Hinzufügen und Entfernen von Laufwerken.
    ''' </summary>
    ''' <remarks>
    ''' Dieses Komponentenobjekt erzeugt intern ein natives Fenster, um
    ''' Windows-Nachrichten für Geräteänderungen (WM_DEVICECHANGE) zu empfangen.<br/>
    ''' Bei relevanten Ereignissen werden die .NET-Events <see cref="DriveAdded"/> und
    ''' <see cref="DriveRemoved"/> ausgelöst.
    ''' </remarks>
    ''' <example>
    ''' <para></para>
    ''' <code><![CDATA['Verwendung in einer WinForms-Form zur Protokollierung von Laufwerksänderungen.
    '''  Private WithEvents _watcher As New DriveWatcherControl.DriveWatcher()
    '''  
    ''' Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '''     ' Optional: Ausgabe in eine ListBox namens "lstLog"
    '''     Me.lstLog.Items.Add("DriveWatcher gestartet ...")
    ''' End Sub
    '''  
    ''' Private Sub _watcher_DriveAdded(sender As Object, e As DriveAddedEventArgs) _
    '''     Handles _watcher.DriveAdded
    '''     Dim info As String
    '''     If e.IsReady Then
    '''         info = $"Hinzugefügt: {e.DriveName} ({e.VolumeLabel}), Format={e.DriveFormat}, Typ={e.DriveType}, Größe={e.TotalSize} Bytes "
    '''     Else
    '''         info = $"Hinzugefügt: {e.DriveName} (Volume noch nicht bereit)"
    '''     End If
    '''     Me.lstLog.Items.Add(info)
    ''' End Sub
    '''  
    ''' Private Sub _watcher_DriveRemoved(sender As Object, e As DriveRemovedEventArgs) _
    '''     Handles _watcher.DriveRemoved
    '''     Me.lstLog.Items.Add($"Entfernt: {e.DriveName}")
    ''' End Sub]]></code>
    ''' </example>
    <ProvideToolboxControl("Schlumpfsoft Controls", False)>
    <System.ComponentModel.ToolboxItem(True)>
    <System.ComponentModel.Description("Steuerelement um die Laufwerke zu überwachen.")>
    <System.Drawing.ToolboxBitmap(GetType(DriveWatcherControl.DriveWatcher), "DriveWatcher.bmp")>
    Public Class DriveWatcher : Inherits System.ComponentModel.Component

#Region "Variablen"

        Private ReadOnly components As System.ComponentModel.IContainer ' Wird vom Komponenten-Designer benötigt.
        Private WithEvents NatForm As New NativeForm ' Internes Formular welches die Meldungen empfängt.

#End Region

#Region "Ereignisse"

        ''' <summary>
        ''' Wird ausgelöst, wenn ein logisches Laufwerk (Volume) hinzugefügt wurde.
        ''' </summary>
        ''' <param name="sender">Die auslösende Instanz des <see cref="DriveWatcher"/>.</param>
        ''' <param name="e">Die Laufwerksinformationen im <see cref="DriveAddedEventArgs"/>.</param>
        ''' <remarks>
        ''' Das Ereignis wird typischerweise bei Anschluss eines Wechselmediums (z. B. USB-Stick) ausgelöst.
        ''' Wenn das Volume noch nicht bereit ist (<see cref="DriveAddedEventArgs.IsReady"/> = False), sind Größen-/Formatfelder neutral befüllt.
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Subskription und Protokollierung in einer WinForms-Form.
        ''' Private WithEvents _watcher As New DriveWatcherControl.DriveWatcher()
        ''' 
        ''' Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '''     ' Startinformation
        '''     Me.lstLog.Items.Add("DriveWatcher aktiv.")
        ''' End Sub
        ''' 
        ''' Private Sub _watcher_DriveAdded(sender As Object, e As DriveAddedEventArgs) Handles _watcher.DriveAdded
        '''     Dim info As String
        '''     If e.IsReady Then
        '''         info = $"Hinzugefügt: {e.DriveName} ({e.VolumeLabel}), Format={e.DriveFormat}, Typ={e.DriveType}, Größe={e.TotalSize} Bytes"
        '''     Else
        '''         info = $"Hinzugefügt: {e.DriveName} (Volume noch nicht bereit)"
        '''     End If
        '''     Me.lstLog.Items.Add(info)
        ''' End Sub]]></code>
        ''' </example>
        <System.ComponentModel.Description("Wird ausgelöst wenn ein Laufwerk hinzugefügt wurde.")>
        Public Event DriveAdded(sender As Object, e As DriveAddedEventArgs)

        ''' <summary>
        ''' Wird ausgelöst, wenn ein logisches Laufwerk (Volume) entfernt wurde.
        ''' </summary>
        ''' <param name="sender">Die auslösende Instanz des <see cref="DriveWatcher"/>.</param>
        ''' <param name="e">Informationen zum entfernten Laufwerk im <see cref="DriveRemovedEventArgs"/>.</param>
        ''' <remarks>
        ''' Das Ereignis enthält mindestens den Laufwerksnamen (z. B. "E:\"). Nach dem Entfernen sind keine Größen-/Formatinformationen verfügbar.
        ''' </remarks>
        '''         ''' <example>
        ''' <code><![CDATA[' Subskription und Protokollierung in einer WinForms-Form.
        ''' Private WithEvents _watcher As New DriveWatcherControl.DriveWatcher()
        ''' 
        ''' Private Sub _watcher_DriveRemoved(sender As Object, e As DriveRemovedEventArgs) Handles _watcher.DriveRemoved
        '''     Me.lstLog.Items.Add($"Entfernt: {e.DriveName}")
        ''' End Sub]]></code>
        ''' </example>
        <System.ComponentModel.Description("Wird ausgelöst wenn ein Laufwerk entfernt wurde.")>
        Public Event DriveRemoved(sender As Object, e As DriveRemovedEventArgs)

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz der <see cref="DriveWatcher"/>-Komponente.
        ''' </summary>
        ''' <remarks>
        ''' Erstellt ein internes natives Fenster zum Empfang von Geräteänderungsnachrichten
        ''' (WM_DEVICECHANGE).
        ''' </remarks>
        ''' <example>
        ''' <code><![CDATA[' Einfache Verwendung in einer WinForms-Form.
        ''' Private WithEvents _watcher As New DriveWatcherControl.DriveWatcher()
        '''  
        ''' Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '''     Me.lstLog.Items.Add("DriveWatcher bereit.")
        ''' End Sub
        '''  
        ''' Private Sub _watcher_DriveAdded(sender As Object, e As DriveAddedEventArgs) Handles _watcher.DriveAdded
        '''     Me.lstLog.Items.Add($"Hinzugefügt: {e.DriveName}")
        ''' End Sub
        '''  
        ''' Private Sub _watcher_DriveRemoved(sender As Object, e As DriveRemovedEventArgs) Handles _watcher.DriveRemoved
        '''     Me.lstLog.Items.Add($"Entfernt: {e.DriveName}")
        ''' End Sub]]></code>
        ''' </example>
        <System.Diagnostics.DebuggerNonUserCode()>
        Public Sub New()
            MyBase.New()
            'Dieser Aufruf ist für den Komponenten-Designer erforderlich.
            Me.InitializeComponent()
        End Sub

        ''' <summary>
        ''' Initialisiert eine neue Instanz der <see cref="DriveWatcher"/>-Komponente und
        ''' fügt sie einem Container hinzu.
        ''' </summary>
        ''' <remarks>
        ''' Geeignet für Designer-/Kompositionsszenarien.<br/>
        ''' Bei Übergabe eines Containers wird die Instanz registriert und gemeinsam
        ''' verwaltet.
        ''' </remarks>
        ''' <param name="container">Ein optionaler <see
        ''' cref="System.ComponentModel.IContainer"/>, dem die Komponente hinzugefügt
        ''' wird.</param>
        ''' <example>
        ''' <code><![CDATA[' Verwendung mit Komponenten-Container (z. B. in einer Form).
        ''' Private components As New System.ComponentModel.Container()
        ''' Private WithEvents _watcher As DriveWatcherControl.DriveWatcher
        '''  
        ''' Public Sub New()
        '''     InitializeComponent()
        '''     _watcher = New DriveWatcherControl.DriveWatcher(components)
        ''' End Sub
        '''  
        ''' Private Sub _watcher_DriveAdded(sender As Object, e As DriveAddedEventArgs) Handles _watcher.DriveAdded
        '''     Debug.WriteLine($"Drive hinzugefügt: {e.DriveName}")
        ''' End Sub
        '''  
        ''' Private Sub _watcher_DriveRemoved(sender As Object, e As DriveRemovedEventArgs) Handles _watcher.DriveRemoved
        '''     Debug.WriteLine($"Drive entfernt: {e.DriveName}")
        ''' End Sub]]></code>
        ''' </example>
        <System.Diagnostics.DebuggerNonUserCode()>
        Public Sub New(container As System.ComponentModel.IContainer)
            MyClass.New()
            'Erforderlich für die Unterstützung des Windows.Forms-Klassenkompositions-Designers
            If container IsNot Nothing Then container.Add(Me)
        End Sub

        ''' <summary>
        ''' Gibt verwaltete Ressourcen frei und zerstört das interne native Fensterhandle.
        ''' </summary>
        ''' <remarks>
        ''' Nach dem Aufruf sind keine Ereignisse mehr verfügbar. Verwenden Sie <see
        ''' cref="System.IDisposable.Dispose"/> bzw. `Using`-Semantik, um Ressourcen zeitnah
        ''' freizugeben.
        ''' </remarks>
        ''' <param name="disposing">True, um verwaltete Ressourcen freizugeben; False, um
        ''' nur nicht verwaltete Ressourcen freizugeben.</param>
        ''' <example>
        ''' <code><![CDATA[' Korrekte Freigabe in einer Form.
        ''' Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        '''     MyBase.OnFormClosed(e)
        '''     ' Explizit freigeben, wenn nicht im Komponenten-Container verwaltet:
        '''     If _watcher IsNot Nothing Then
        '''         _watcher.Dispose()
        '''         _watcher = Nothing
        '''     End If
        ''' End Sub
        '''  
        ''' ' Alternative mit Using-Semantik außerhalb einer Form:
        ''' Using watcher As New DriveWatcherControl.DriveWatcher()
        '''     ' verwenden ...
        ''' End Using ' Dispose wird automatisch aufgerufen]]></code>
        ''' </example>
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(disposing As Boolean)
            Try
                If disposing Then
                    If Me.components IsNot Nothing Then Me.components.Dispose()
                    If Me.NatForm IsNot Nothing Then Me.NatForm.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

#End Region

#Region "interne Methoden"

        ' Wird ausgelöst wenn ein Laufwerk hinzugefügt wurde
        Private Sub NatForm_DriveAdded(sender As Object, e As System.IO.DriveInfo) Handles NatForm.DriveAdded
            Dim arg As New DriveAddedEventArgs
            If e.IsReady Then
                With arg
                    .DriveName = e.Name
                    .VolumeLabel = e.VolumeLabel
                    .AvailableFreeSpace = e.AvailableFreeSpace
                    .TotalFreeSpace = e.TotalFreeSpace
                    .TotalSize = e.TotalSize
                    .DriveFormat = e.DriveFormat
                    .DriveType = e.DriveType
                    .IsReady = e.IsReady
                End With
            Else
                With arg
                    .DriveName = e.Name
                    .VolumeLabel = $""
                    .AvailableFreeSpace = 0
                    .TotalFreeSpace = 0
                    .TotalSize = 0
                    .DriveFormat = $""
                    .DriveType = e.DriveType
                    .IsReady = e.IsReady
                End With
            End If
            RaiseEvent DriveAdded(Me, arg)
        End Sub

        ' Wird ausgelöst wenn ein Laufwerk entfern wurde
        Private Sub NatForm_DriveRemoved(sender As Object, e As System.IO.DriveInfo) Handles NatForm.DriveRemoved
            Dim arg As New DriveRemovedEventArgs With {.DriveName = e.Name}
            RaiseEvent DriveRemoved(Me, arg)
        End Sub

        ' Hinweis: Die folgende Prozedur ist für den Komponenten-Designer erforderlich.
        ' Das Bearbeiten ist mit dem Komponenten-Designer möglich.<br/>
        ' Das Bearbeiten mit dem Code-Editor ist nicht möglich.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
        End Sub

#End Region

#Region "interne Klassen"

        ' Definiert das Fenster welches die WindowsMessages empfängt.
        Private Class NativeForm : Inherits System.Windows.Forms.NativeWindow

            Implements System.IDisposable

            'Das sind die Ereignisse aus WParam.
            'Uns interessiert nur, ob ein Laufwerk hinzugekommen ist oder entfernt wurde.
            Public Event DriveAdded(sender As Object, e As System.IO.DriveInfo)
            Public Event DriveRemoved(sender As Object, e As System.IO.DriveInfo)

            'Windowmessage DeviceChange
            Private Const WM_DEVICECHANGE As Integer = &H219

            'Die beiden Ereignisse, die für uns von Bedeutung sind.
            Private Const DBT_DEVICEARRIVAL As Integer = &H8000
            Private Const DBT_DEVICEREMOVECOMPLETE As Integer = &H8004

            Private disposedValue As Boolean

            ' Das sind die Konstanten der Gerätetypen
            ' Vorsicht die Gerätetypen Variablen in den Strukturen sind vom Typ Integer.<br/>
            ' IntelliSense kann das nicht auflösen.
            Private Enum DBT_DEVTYP
                OEM = 0 ' OEM- oder IHV-definiert
                DEVNODE = 1 ' Devnode-Nummer
                VOLUME = 2 ' Logisches Volumen
                PORT = 3 ' Port (seriell oder parallel)
                NET = 4 ' Netzwerkressource
                DEVICEINTERFACE = 5 ' Geräteschnittstellenklasse
                HANDLE = 6 ' Dateisystem-Handle
            End Enum

            ' Die Struktur für den Header.
            ' https://learn.microsoft.com/de-de/windows/win32/api/dbt/ns-dbt-dev_broadcast_hdr
            Private Structure DEV_BROADCAST_HDR
                Public dbch_size As Integer
                Public dbch_devicetype As Integer
                Public dbch_reserved As Integer
            End Structure

            ' Die Struktur für OEM.
            ' https://learn.microsoft.com/de-de/windows/win32/api/dbt/ns-dbt-dev_broadcast_oem
            Private Structure DEV_BROADCAST_OEM
                Public dbco_size As Integer
                Public dbco_devicetype As Integer
                Public dbco_reserved As Integer
                Public dbco_identifier As Integer
                Public dbco_suppfunc As Integer
            End Structure

            ' Die Struktur für Volumes.
            ' https://learn.microsoft.com/de-de/windows/win32/api/dbt/ns-dbt-dev_broadcast_volume
            Private Structure DEV_BROADCAST_VOLUME
                Public dbch_size As Integer
                Public dbch_devicetype As Integer
                Public dbch_reserved As Integer
                Public dbcv_unitmask As Integer
                Public dbcv_flags As Short
            End Structure

            'Dies ist der Dreh- und Angelpunkt der Klasse. - Hier bekommen wir die Messages mit.
            'In unserm Fall interessiert uns nur die WM_DeviceChange-Nachricht
            Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
                If m.Msg = WM_DEVICECHANGE Then Me.HandleHeader(m)
                MyBase.WndProc(m)
            End Sub

            'Hier schauen wir erst mal in den Header und verzweigen dementsprechend
            Private Sub HandleHeader(ByRef m As System.Windows.Forms.Message)
                Dim header As DEV_BROADCAST_HDR
                Dim objHeader As Object = m.GetLParam(header.GetType)
                If Not Microsoft.VisualBasic.IsNothing(objHeader) Then
                    Select Case header.dbch_devicetype
                        Case DBT_DEVTYP.OEM
                            Me.HandleOEM(m)
                        Case DBT_DEVTYP.DEVNODE
                        Case DBT_DEVTYP.VOLUME
                            Me.HandleVolume(m)
                        Case DBT_DEVTYP.PORT
                        Case DBT_DEVTYP.NET
                        Case DBT_DEVTYP.DEVICEINTERFACE
                        Case DBT_DEVTYP.HANDLE
                    End Select
                End If
            End Sub

            'Das Ereignis betrifft ein Volume
            Private Sub HandleVolume(ByRef m As System.Windows.Forms.Message)
                Dim volume As DEV_BROADCAST_VOLUME
                Dim objVolume As Object = m.GetLParam(volume.GetType)
                If Not Microsoft.VisualBasic.IsNothing(objVolume) Then
                    volume = DirectCast(objVolume, DEV_BROADCAST_VOLUME)
                    Dim di As New System.IO.DriveInfo(Me.DriveFromMask(volume.dbcv_unitmask))
                    Select Case CInt(m.WParam)
                        Case DBT_DEVICEARRIVAL
                            RaiseEvent DriveAdded(Me, di)
                        Case DBT_DEVICEREMOVECOMPLETE
                            RaiseEvent DriveRemoved(Me, di)
                    End Select
                End If
            End Sub

            'OEM, und was genau?
            'Uns interesieren nur Volumes
            Private Sub HandleOEM(ByRef m As System.Windows.Forms.Message)
                Dim oem As DEV_BROADCAST_OEM
                Dim objOem As Object = m.GetLParam(oem.GetType)
                If Not Microsoft.VisualBasic.IsNothing(objOem) Then
                    oem = DirectCast(objOem, DEV_BROADCAST_OEM)
                    If oem.dbco_devicetype = DBT_DEVTYP.VOLUME Then Me.HandleVolume(m)
                End If
            End Sub

            'Liefert den Laufwerksbuchstaben zurück
            Private Function DriveFromMask(mask As Integer) As Char
                Dim result As Char = CChar(String.Empty)
                For b As Integer = 0 To 25
                    If (mask And CInt(2 ^ b)) <> 0 Then
                        result = Microsoft.VisualBasic.Chr(65 + b)
                        Exit For
                    End If
                Next b
                Return result
            End Function

            Public Sub New()
                Me.CreateHandle(New System.Windows.Forms.CreateParams) 'eigenes Handle erstellen
            End Sub

            Protected Overridable Sub Dispose(disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        ' Verwalteten Zustand (verwaltete Objekte) bereinigen
                        Me.DestroyHandle() 'eigenes Handle zerstören
                    End If
                    ' Nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer überschreiben
                    ' Große Felder auf NULL setzen
                    Me.disposedValue = True
                End If
            End Sub

            ' Finalizer nur überschreiben, wenn "Dispose(disposing As Boolean)"
            ' Code für die Freigabe nicht verwalteter Ressourcen enthält
            Protected Overrides Sub Finalize()
                ' Ändern Sie diesen Code nicht.
                ' Fügen Sie Bereinigungscode in der Methode "Dispose(disposing As Boolean)" ein.
                Me.Dispose(disposing:=False)
                MyBase.Finalize()
            End Sub

            Public Sub Dispose() Implements System.IDisposable.Dispose
                ' Ändern Sie diesen Code nicht.
                ' Fügen Sie Bereinigungscode in der Methode "Dispose(disposing As Boolean)" ein.
                Me.Dispose(disposing:=True)
                System.GC.SuppressFinalize(Me)
            End Sub

        End Class

#End Region

    End Class

End Namespace