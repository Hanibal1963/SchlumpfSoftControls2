' *************************************************************************************************
' AfterSwitchPagesEventArgs.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System

Namespace WizardControl

    ''' <summary>
    ''' Enthält die Indexwerte der Seiten nachdem die Seiten gewechselt wurden.
    ''' </summary>
    Public Class AfterSwitchPagesEventArgs

        Inherits EventArgs

        Protected _NewIndex As Integer

        ''' <summary>
        ''' Index der alten Seite
        ''' </summary>
        Public ReadOnly Property OldIndex As Integer

        ''' <summary>
        ''' Index der neuen Seite
        ''' </summary>
        Public ReadOnly Property NewIndex As Integer
            Get
                Return Me._NewIndex
            End Get
        End Property

        Friend Sub New(OldIndex As Integer, NewIndex As Integer)

            Me.OldIndex = OldIndex
            Me._NewIndex = NewIndex

        End Sub

    End Class

End Namespace