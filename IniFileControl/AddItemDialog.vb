' *************************************************************************************************
' AddItemDialog.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace IniFileControl

    Public Class AddItemDialog

        Private _NewItemValue As String = $""

        ''' <summary>
        ''' Gibt den neuen Wert zurück oder legt ihn fest.
        ''' </summary>
        ''' <returns></returns>
        <Browsable(True)>
        Public Property NewItemValue As String
            Get
                Return Me._NewItemValue
            End Get
            Set
                Me._NewItemValue = Value
            End Set
        End Property

        Public Sub New()
            ' Dieser Aufruf ist für den Designer erforderlich.
            Me.InitializeComponent()
            ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            ' Button deaktivieren
            Me.ButtonOK.Enabled = False
        End Sub

        Private Sub Button_Click(sender As Object, e As System.EventArgs) Handles ButtonOK.Click, ButtonCancel.Click
            If sender Is Me.ButtonOK Then ' Welcher Button wurde geklickt
                Me.SetNewItemValue() ' Neuen Wert setzen
                Me.DialogResult = DialogResult.OK ' Ergebnis setzen
            ElseIf sender Is Me.ButtonCancel Then
                Me.DialogResult = DialogResult.Cancel ' Ergebnis setzen
            End If
            Me.Close() ' Dialog schließen
        End Sub

        ''' <summary>
        ''' Übernimmt den neuen Wert.
        ''' </summary>
        Private Sub SetNewItemValue()
            Me._NewItemValue = Me.TextBox.Text
        End Sub

        ''' <summary>
        ''' Wird aufgerufen wenn sich der Text im Textfeld ändert.
        ''' </summary>
        Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox.TextChanged
            If String.IsNullOrWhiteSpace(CType(sender, TextBox).Text) Then ' Textbox leer oder nur Leerzeichen?
                Me.ButtonOK.Enabled = False ' Button deaktivieren wenn Textbox leer oder nur Leerzeichen enthält
            Else
                Me.ButtonOK.Enabled = True ' ansonsten Button aktivieren
            End If
        End Sub

    End Class

End Namespace