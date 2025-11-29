' *************************************************************************************************
' SelectedPathChangedEventArgs.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Stellt Daten für das Ereignis bereit, das ausgelöst wird, wenn sich der
    ''' ausgewählte Pfad ändert.
    ''' </summary>
    Public Class SelectedPathChangedEventArgs : Inherits System.EventArgs

        ''' <summary>
        ''' Ruft den aktuell ausgewählten Pfad ab.
        ''' </summary>
        ''' <value>
        ''' Der ausgewählte Pfad als Zeichenfolge.
        ''' </value>
        Public ReadOnly Property SelectedPath As String

        ''' <summary>
        ''' Initialisiert eine neue Instanz der <see
        ''' cref="SelectedPathChangedEventArgs"/>-Klasse.
        ''' </summary>
        ''' <param name="Path">Der neue ausgewählte Pfad.</param>
        Public Sub New(Path As String)
            Me.SelectedPath = Path
        End Sub

    End Class

End Namespace
