
Namespace ExtendedRTFControl

    ''' <summary>
    ''' Definition der API-Funktionen
    ''' </summary>
    ''' <remarks></remarks>
    Module ApiDefinitions

        ''' <summary>
        ''' Sendet ein Windows-Message direkt an ein Fenster .
        ''' </summary>
        ''' <param name="hWnd">Das Fensterhandle des Zielfensters.</param>
        ''' <param name="msg">WM_SETREDRAW zum Ein/Aus-Schalten der Darstellung</param>
        ''' <param name="wParam"><para>Der Neuzeichnungsstatus. </para>
        ''' <para>Wenn dieser Parameter <b>TRUE</b> ist , kann der Inhalt nach einer
        ''' Änderung neu gezeichnet werden. </para>
        ''' <para>Wenn dieser Parameter <b>FALSE</b> ist , kann der Inhalt nach einer
        ''' Änderung nicht neu gezeichnet werden.</para></param>
        ''' <param name="lParam">Dieser Parameter wird nicht verwendet.</param>
        ''' <returns>
        ''' Ihre Anwendung sollte 0 zurückgeben, wenn sie diese Nachricht verarbeitet.
        ''' </returns>
        <System.Runtime.InteropServices.DllImport("user32.dll")>
        Friend Function SendMessage(hWnd As System.IntPtr, msg As Integer, wParam As Boolean, lParam As System.IntPtr) As System.IntPtr
        End Function

    End Module

End Namespace
