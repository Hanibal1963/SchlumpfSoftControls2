' *************************************************************************************************
' PageDesigner.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace WizardControl

    Friend Class PageDesigner : Inherits System.Windows.Forms.Design.ParentControlDesigner

        Public Overrides ReadOnly Property SelectionRules As System.Windows.Forms.Design.SelectionRules
            Get
                Return System.Windows.Forms.Design.SelectionRules.Locked Or System.Windows.Forms.Design.SelectionRules.Visible
            End Get
        End Property

    End Class

End Namespace