' *************************************************************************************************
' ApiDefinitions.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ExtendedRTFControl

    Module ApiDefinitions

        ' Win32-Konstante zum De-/Aktivieren des Redraws eines Fenster-Handles.
        Public Const WM_SETREDRAW As Integer = &HB

        ' Mindest-Schriftgröße.
        ' Kann bei Bedarf angepasst werden.
        Public Const MIN_FONT_SIZE As Single = 8.0F

        ' Sendet ein Windows-Message direkt an ein Fenster .
        ' Die Anwendung sollte 0 zurückgeben, wenn sie diese Nachricht verarbeitet.
        <System.Runtime.InteropServices.DllImport("user32.dll")>
        Public Function SendMessage(hWnd As System.IntPtr, msg As Integer, wParam As Boolean, lParam As System.IntPtr) As System.IntPtr
        End Function

    End Module

End Namespace
