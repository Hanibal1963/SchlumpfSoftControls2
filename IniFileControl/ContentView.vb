' *************************************************************************************************
' ContentView.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports SchlumpfSoft.Controls.Attribute

Namespace IniFileControl

    ' weitere Infos:
    ' <Browsable> - https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.browsableattribute?view=netframework-4.7.2
    ' <Category> - https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.categoryattribute?view=netframework-4.7.2
    ' <Description> - https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.descriptionattribute?view=netframework-4.7.2

    '''' <summary>
    '''' Steuerelement zum Anzeigen des Dateiinhaltes.
    '''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Steuerelement zum Anzeigen des Dateiinhaltes.")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(IniFileControl.ContentView), "ContentView.bmp")>
    Public Class ContentView

        Inherits UserControl

        Private _Lines As String()
        Private _TitelText As String
        Private Event PropLinesChanged()
        Private Event TitelTextChanged()

        Public Sub New()
            ' Dieser Aufruf ist für den Designer erforderlich.
            Me.InitializeComponent()
            ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            Me._TitelText = Me.GroupBox.Text
        End Sub

#Region "Definition der neuen Eigenschaften"

        ''' <summary>
        ''' Gibt den Text der Titelzeile zurück oder legt diesen fest.
        ''' </summary>
        ''' <returns></returns>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den Text der Titelzeile zurück oder legt diesen fest.")>
        Public Property TitelText As String
            Set(value As String)
                If Me._TitelText <> value Then
                    Me._TitelText = value
                    RaiseEvent TitelTextChanged()
                End If
            End Set
            Get
                Return Me._TitelText
            End Get
        End Property

        ''' <summary>
        ''' Gibt den Dateiinhalt zurück oder legt diesen fest.
        ''' </summary>
        ''' <returns></returns>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den Dateiinhalt zurück oder legt diesen fest.")>
        Public Property Lines As String()
            Get
                Return Me._Lines
            End Get
            Set
                If Me._Lines IsNot Value Then
                    Me._Lines = Value
                    RaiseEvent PropLinesChanged()
                End If
            End Set
        End Property

#End Region

        Private Sub IniFileContentView_LinesChanged() Handles Me.PropLinesChanged
            Me.TextBox.Lines = Me._Lines
        End Sub

        Private Sub IniFileCommentEdit_TitelTextChanged() Handles Me.TitelTextChanged
            Me.GroupBox.Text = Me._TitelText
        End Sub

    End Class

End Namespace