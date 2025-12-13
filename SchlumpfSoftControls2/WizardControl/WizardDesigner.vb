' *************************************************************************************************
' WizardDesigner.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace WizardControl

    ''' <summary>
    ''' Designer für dieses Control
    ''' </summary>
    Friend Class WizardDesigner : Inherits System.Windows.Forms.Design.ParentControlDesigner

        Protected Overrides Property DrawGrid As Boolean
            Get
                Return MyBase.DrawGrid
            End Get
            Set(value As Boolean)
                MyBase.DrawGrid = value
            End Set
        End Property

        Protected Overrides Sub WndProc(ByRef msg As System.Windows.Forms.Message)
            If msg.Msg = 513 OrElse msg.Msg = 515 Then
                Dim selectionService = CType(MyBase.GetService(GetType(System.ComponentModel.Design.ISelectionService)), System.ComponentModel.Design.ISelectionService)
                Dim wizard As Wizard = TryCast(selectionService.PrimarySelection, Wizard)
                If wizard IsNot Nothing Then
                    Dim x As Integer = CShort(CInt(msg.LParam) And &HFFFF)
                    Dim y As Integer = CShort(CUInt(CInt(msg.LParam) And -65536) >> 16)
                    Dim pt As New System.Drawing.Point(x, y)
                    If msg.HWnd = wizard.ButtonNext.Handle Then
                        If wizard.ButtonNext.Enabled AndAlso wizard.ButtonNext.ClientRectangle.Contains(pt) Then
                            wizard.Next()
                        End If
                    ElseIf msg.HWnd = wizard.ButtonBack.Handle AndAlso wizard.ButtonBack.Enabled AndAlso wizard.ButtonBack.ClientRectangle.Contains(pt) Then
                        wizard.Back()
                    End If
                    Return
                End If
            End If
            MyBase.WndProc(msg)
        End Sub

    End Class

End Namespace