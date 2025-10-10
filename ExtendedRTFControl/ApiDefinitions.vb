
Namespace ExtendedRTFControl

    Module ApiDefinitions

        ' Sendet ein Windows-Message direkt an ein Fenster (hier: WM_SETREDRAW zum Ein/Aus-Schalten der Darstellung).
        <System.Runtime.InteropServices.DllImport("user32.dll")>
        Friend Function SendMessage(hWnd As System.IntPtr, msg As Integer, wParam As Boolean, lParam As System.IntPtr) As System.IntPtr
        End Function

    End Module

End Namespace
