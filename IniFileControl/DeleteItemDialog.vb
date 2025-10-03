' *************************************************************************************************
' DeleteItemDialog.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.Windows.Forms

Namespace IniFileControl

    Public Class DeleteItemDialog

        Private _itemvalue As String = $""

        Private Event PropertyItemValueChanged()

        ''' <summary>
        ''' Gibt den Wert des Elementes zurück oder legt ihn fest.
        ''' </summary>
        Public Property ItemValue As String
            Get
                Return Me._itemvalue
            End Get
            Set
                Me._itemvalue = Value
                RaiseEvent PropertyItemValueChanged()
            End Set
        End Property

        Private Sub Button_Click(sender As Object, e As EventArgs) Handles ButtonYes.Click, ButtonNo.Click
            If sender Is Me.ButtonYes Then ' Welcher Button wurde gedrückt?
                Me.DialogResult = DialogResult.OK ' Ergebnis setzen
            ElseIf sender Is Me.ButtonNo Then
                Me.DialogResult = DialogResult.Cancel ' Ergebnis setzen
            End If
            Me.Close() ' Dialog schließen
        End Sub

        Private Sub IniFileDeleteItemDialog_PropertyItemValueChanged() Handles Me.PropertyItemValueChanged
            Me.Label.Text = Me.Label.Text.Replace("{0}", Me.ItemValue)
        End Sub

    End Class

End Namespace