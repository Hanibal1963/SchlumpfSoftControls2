' *************************************************************************************************
' IniFileCommentEdit.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports SchlumpfSoft.Controls.Attribute

Namespace IniFileControl

    ' weitere Infos:
    ' <Browsable> - https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.browsableattribute?view=netframework-4.7.2
    ' <Category> - https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.categoryattribute?view=netframework-4.7.2
    ' <Description> - https://learn.microsoft.com/de-de/dotnet/api/system.componentmodel.descriptionattribute?view=netframework-4.7.2

    '''' <summary>
    '''' Steuerelement zum Anzeigen und Bearbeiten des Datei- oder Abschnitts- Kommentars einer INI - Datei.
    '''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Steuerelement zum Anzeigen und Bearbeiten des Datei- oder Abschnitts- Kommentars einer INI - Datei.")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(IniFileControl.CommentEdit), "CommentEdit.bmp")>
    Public Class CommentEdit

        Inherits UserControl

#Region "Definition der internen Eigenschaftsvariablen"

        Private _Lines As String() = {""}
        Private _TitelText As String
        Private _SectionName As String

#End Region

#Region "Definition der öffentlichen Ereignisse"

        ''' <summary>
        ''' Wird ausgelöst wenn sich der Kommentartext geändert hat.
        ''' </summary>
        <Description("Wird ausgelöst wenn sich der Kommentartext geändert hat.")>
        Public Event CommentChanged(sender As Object, e As CommentEditEventArgs)

#End Region

#Region "Definition der internen Ereignisse"

        ''' <summary>
        ''' Wir ausgelöst wenn sich die Eigenschaft Comment geändert hat.
        ''' </summary>
        Private Event PropCommentChanged()

        ''' <summary>
        ''' Wird ausgelöst wenn sich die Eigenschaft TitelText geändert hat.
        ''' </summary>
        Private Event TitelTextChanged()

#End Region

        Public Sub New()
            ' Dieser Aufruf ist für den Designer erforderlich.
            Me.InitializeComponent()
            ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            Me._TitelText = Me.GroupBox.Text
            Me.Button.Enabled = False
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
                If Me._TitelText <> value Then ' hat sich der Wert geändert?
                    Me._TitelText = value ' Wert setzen
                    RaiseEvent TitelTextChanged() ' Ereignis auslösen
                End If
            End Set
            Get
                Return Me._TitelText ' Wert zurückgeben
            End Get
        End Property

        ''' <summary>
        ''' Gibt den Kommentartext zurück oder legt diesen fest.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den Kommentartext zurück oder legt diesen fest.")>
        Public Property Comment As String()
            Get
                Return Me._Lines ' Wert zurückgeben
            End Get
            Set
                If Not Me._Lines.SequenceEqual(Value) Then ' hat sich der Wert geändert?
                    Me._Lines = Value ' Wert setzen
                    RaiseEvent PropCommentChanged() ' Ereignis auslösen
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gibt den Name des Abschnitts zurück oder legt diesen fest 
        ''' für den der Kommentar angezeigt werden soll.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den Name des Abschnitts zurück oder legt diesen fest für den der Kommentar angezeigt werden soll.")>
        Public Property SectionName As String
            Get
                Return Me._SectionName
            End Get
            Set
                Me._SectionName = Value
            End Set
        End Property

#End Region

#Region "Definition der internen Ereignisbehandlungen"

        ''' <summary>
        ''' Wird ausgelöst wenn der Button geklickt wird.
        ''' </summary>
        Private Sub Button_Click(sender As Object, e As EventArgs) Handles Button.Click
            Me._Lines = Me.TextBox.Lines ' geänderten Kommentar übernehmen
            Me.Button.Enabled = False ' Button deaktivieren
            RaiseEvent CommentChanged(Me, New CommentEditEventArgs(Me._SectionName, Me._Lines)) ' Ereignis auslösen
        End Sub

        ''' <summary>
        ''' Wird ausgelöst wenn sich der Text in der Textbox geändert hat.
        ''' </summary>
        Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox.TextChanged
            Me.Button.Enabled = True  ' Button zum übernehmen der Dateikommentaränderungen aktivieren
        End Sub

        ''' <summary>
        ''' Wird ausgelöst wenn sich die Eigenschaft Comment geändert hat.
        ''' </summary>
        Private Sub IniFileCommentEdit_PropCommentChanged() Handles Me.PropCommentChanged
            Me.TextBox.Lines = Me._Lines ' Kommentarzeilen in die Textbox eintragen
            Me.Button.Enabled = False ' Button deaktivieren
        End Sub

        ''' <summary>
        '''  Wird ausgelöst wenn sich die Eigenschaft TitelText geändert hat.
        ''' </summary>
        Private Sub IniFileCommentEdit_TitelTextChanged() Handles Me.TitelTextChanged
            Me.GroupBox.Text = Me._TitelText ' Titeltext setzen
        End Sub

#End Region

    End Class

End Namespace