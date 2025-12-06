' *************************************************************************************************
' SelectedPathChangedEventArgs.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ExplorerTreeViewControl

    ''' <summary>
    ''' Stellt Daten für das Ereignis bereit, das ausgelöst wird, wenn sich der
    ''' ausgewählte Pfad ändert.
    ''' </summary>
    ''' <remarks>
    ''' Dieser Typ wird typischerweise mit einem Ereignis vom Typ <see
    ''' cref="System.EventHandler(Of TEventArgs)"/> verwendet, um den neuen Pfad an
    ''' Abonnenten zu übermitteln.
    ''' </remarks>
    ''' <example>
    ''' Das folgende Beispiel zeigt einen Publisher, der das Ereignis mit <see cref="SelectedPathChangedEventArgs"/> auslöst, und einen Subscriber, der den neuen Pfad entgegennimmt. <code language="vb"><![CDATA[ Imports System
    '''  Imports ExplorerTreeViewControl
    '''  
    '''  Public Class PfadPublisher
    '''      Public Event SelectedPathChanged As EventHandler(Of SelectedPathChangedEventArgs)
    '''  
    '''      Public Sub UpdatePath(newPath As String)
    '''          ' Ereignis mit neuem Pfad auslösen
    '''          RaiseEvent SelectedPathChanged(Me, New SelectedPathChangedEventArgs(newPath))
    '''      End Sub
    '''  End Class
    '''  
    '''  Public Class PfadSubscriber
    '''      Public Sub New(publisher As PfadPublisher)
    '''          AddHandler publisher.SelectedPathChanged, AddressOf OnSelectedPathChanged
    '''      End Sub
    '''  
    '''      Private Sub OnSelectedPathChanged(sender As Object, e As SelectedPathChangedEventArgs)
    '''          Console.WriteLine($"Neuer Pfad: {e.SelectedPath}")
    '''      End Sub
    '''  End Class
    '''  
    '''  ' Beispielverwendung:
    '''  ' Dim pub = New PfadPublisher()
    '''  ' Dim subr = New PfadSubscriber(pub)
    '''  ' pub.UpdatePath("C:\Daten\Projekte")
    '''  ]]></code>
    ''' </example>
    Public Class SelectedPathChangedEventArgs : Inherits System.EventArgs

        ''' <summary>
        ''' Ruft den aktuell ausgewählten Pfad ab.
        ''' </summary>
        ''' <remarks>
        ''' Der Wert repräsentiert typischerweise einen Dateisystempfad (z. B.
        ''' "C:\Ordner\Datei.txt").
        ''' </remarks>
        ''' <value>
        ''' Der ausgewählte Pfad als Zeichenfolge.
        ''' </value>
        ''' <example>
        ''' Das folgende Beispiel zeigt den Zugriff auf die Eigenschaft innerhalb eines Ereignishandlers. <code language="vb"><![CDATA[Private Sub OnSelectedPathChanged(sender As Object, e As ExplorerTreeViewControl.SelectedPathChangedEventArgs)
        '''     Dim pfad As String = e.SelectedPath
        '''     ' Weiterverarbeiten des Pfads:
        '''     Console.WriteLine($"Ausgewählter Pfad: {pfad}")
        ''' End Sub]]></code>
        ''' </example>
        Public ReadOnly Property SelectedPath As String

        ''' <summary>
        ''' Initialisiert eine neue Instanz der <see
        ''' cref="SelectedPathChangedEventArgs"/>-Klasse.
        ''' </summary>
        ''' <remarks>
        ''' Der übergebene Wert wird der schreibgeschützten Eigenschaft <see
        ''' cref="SelectedPath"/> zugewiesen.
        ''' </remarks>
        ''' <param name="Path">Der neue ausgewählte Pfad.</param>
        ''' <example>
        ''' Das folgende Beispiel zeigt das manuelle Erzeugen einer Instanz und den Zugriff auf den Pfad. <code language="vb"><![CDATA[ Imports ExplorerTreeViewControl
        '''  
        '''  Dim args = New SelectedPathChangedEventArgs("D:\Dokumente")
        '''  Console.WriteLine(args.SelectedPath)
        '''  ]]></code>
        ''' </example>
        Public Sub New(Path As String)
            Me.SelectedPath = Path
        End Sub

    End Class

End Namespace
