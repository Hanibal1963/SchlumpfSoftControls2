' *************************************************************************************************
' ExtendedRTF.Designer.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ExtendedRTFControl

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class ExtendedRTF
        Inherits System.Windows.Forms.RichTextBox

        'UserControl überschreibt Dispose, um die Komponentenliste zu bereinigen.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
                ' Falls Entwickler vergessen hat EndUpdate mehrfach aufzurufen.
                While _updateNesting > 0
                    _updateNesting = 1
                    EndUpdate()
                End While
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.SuspendLayout()
            '
            'ToolboxControl
            '
            Me.Name = "ExtendedRTF"
            Me.ResumeLayout(False)

        End Sub

    End Class

End Namespace