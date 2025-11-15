' *************************************************************************************************
' DriveRemovedEventArgs.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

'TODO: In Gemeinsame Datei "EventDefinitions.vb" verschieben

Namespace DriveWatcherControl

    ''' <summary>
    ''' Übergibt das entfernte Laufwerk.
    ''' </summary>
    Public Structure DriveRemovedEventArgs

        ''' <summary>
        ''' Ruft den Namen eines Laufwerks ab, z.B. C:\.
        ''' </summary>
        Public Property DriveName As String

    End Structure

End Namespace