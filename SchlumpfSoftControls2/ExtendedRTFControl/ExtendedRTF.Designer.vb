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
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Erforderlich für den Windows Form-Designer
        Private components As System.ComponentModel.IContainer

        'HINWEIS: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
        'Sie kann mit dem Windows Form-Designer geändert werden.  
        'Ändern Sie sie nicht mit dem Code-Editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.SuspendLayout()
            '
            'Toolbox-Steuerelement
            '
            Me.Name = "ExtendedRTF"
            Me.ResumeLayout(False)

        End Sub

    End Class

End Namespace