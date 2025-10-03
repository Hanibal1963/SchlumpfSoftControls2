' *************************************************************************************************
' RenameItemDialog.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace IniFileControl

    Friend Class RenameItemDialog

        Private _OldItemValue As String = $""
        Private _NewItemValue As String = $""

        Private Event PropertyoldItemValueChanged()

        ''' <summary>
        ''' Gibt den alten Wert zurück oder legt ihn fest.
        ''' </summary>
        Public Sub New()
            ' Dieser Aufruf ist für den Designer erforderlich.
            Me.InitializeComponent()
            ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            Me.ButtonYes.Enabled = False ' Button deaktivieren
        End Sub

        ''' <summary>
        ''' Gibt den alten Wert zurück oder legt ihn fest.
        ''' </summary>
        <Browsable(True)>
        Public Property OldItemValue As String
            Get
                Return Me._OldItemValue
            End Get
            Set
                Me._OldItemValue = Value
                RaiseEvent PropertyoldItemValueChanged()
            End Set
        End Property

        ''' <summary>
        ''' Gibt den neuen Wert zurück oder legt ihn fest.
        ''' </summary>
        <Browsable(True)>
        Public Property NewItemValue As String
            Get
                Return Me._NewItemValue
            End Get
            Set
                Me._NewItemValue = Value
            End Set
        End Property

        Private Sub Button_Click(sender As Object, e As System.EventArgs) Handles ButtonYes.Click, ButtonNo.Click
            If sender Is Me.ButtonYes Then  ' welcher Button wurde geklickt?
                Me.SetNewItemValue() ' neuen Wert setzen
                Me.DialogResult = DialogResult.Yes  ' Ergebnis setzen
            ElseIf sender Is Me.ButtonNo Then
                Me.DialogResult = DialogResult.No ' Ergebnis setzen
            End If
            Me.Close()  ' Dialog schließen
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
                Me.ButtonYes.Enabled = False ' Button deaktivieren wenn Textbox leer oder nur Leerzeichen enthält
            Else
                Me.ButtonYes.Enabled = True ' ansonsten Button aktivieren
            End If
        End Sub

        Private Sub IniFileRenameItemDialog_PropertyoldItemValueChanged() Handles Me.PropertyoldItemValueChanged
            Me.Label.Text = Me.Label.Text.Replace("{0}", Me._OldItemValue)
        End Sub

    End Class


End Namespace