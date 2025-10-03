' *************************************************************************************************
' ListEdit.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
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
    '''' Steuerelement zum Anzeigen und Bearbeiten der Abschnitts- oder Eintrags- Liste einer INI - Datei.
    '''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Steuerelement zum Anzeigen und Bearbeiten der Abschnitts- oder Eintrags- Liste einer INI - Datei.")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(IniFileControl.ListEdit), "ListEdit.bmp")>
    Public Class ListEdit

        Inherits UserControl

#Region "Definition der internen Eigenschaftsvariablen"

        Private _SelectedItem As String = String.Empty
        Private _SelectedSection As String = String.Empty
        Private _Items As String() = {""}
        Private _TitelText As String

#End Region

#Region "Definition der öffentlichen Ereignisse"

        ''' <summary>
        ''' Wird ausgelöst wenn ein Eintrag hinzugefügt werden soll.
        ''' </summary>
        <Description("Wird ausgelöst wenn ein Eintrag hinzugefügt werden soll.")>
        <Category("ListEdit")>
        Public Event ItemAdd(sender As Object, e As ListEditEventArgs)

        ''' <summary>
        ''' Wird ausgelöst wenn ein Eintrag umbenannt werden soll.
        ''' </summary>
        <Description("Wird ausgelöst wenn ein Eintrag umbenannt werden soll.")>
        <Category("ListEdit")>
        Public Event ItemRename(sender As Object, e As ListEditEventArgs)

        ''' <summary>
        ''' Wird ausgelöst wenn ein Eintrag gelöscht werden soll.
        ''' </summary>
        <Description("Wird ausgelöst wenn ein Eintrag gelöscht werden soll.")>
        <Category("ListEdit")>
        Public Event ItemRemove(sender As Object, e As ListEditEventArgs)

        ''' <summary>
        ''' Wird ausgelöst wenn sich der gewählte Eintrag geändert hat.
        ''' </summary>
        <Description("Wird ausgelöst wenn sich der gewählte Eintrag geändert hat.")>
        <Category("ListEdit")>
        Public Event SelectedItemChanged(sender As Object, e As ListEditEventArgs)

#End Region

#Region "Definition der internen Ereignisse"

        ''' <summary>
        ''' wird ausgelöst wenn sich der Titeltext geändert hat.
        ''' </summary>
        Private Event TitelTextChanged()

        ''' <summary>
        ''' Wird ausgelöst wenn sich die Liste der Einträge geändert hat.
        ''' </summary>
        Private Event ListItemsChanged()

#End Region

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
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den Text der Titelzeile zurück oder legt diesen fest.")>
        Public Property TitelText As String
            Set(value As String)
                If value <> Me._TitelText Then
                    Me._TitelText = value
                    RaiseEvent TitelTextChanged()
                End If
            End Set
            Get
                Return Me._TitelText
            End Get
        End Property

        ''' <summary>
        ''' Setzt die Elemente der Listbox oder gibt diese zurück.
        ''' </summary>
        <Browsable(True)>
        <Category("Data")>
        <Description("Setzt die Elemente der Listbox oder gibt diese zurück.")>
        Public Property ListItems() As String()
            Set
                If Me._Items IsNot Value Then
                    Me._Items = Value
                    RaiseEvent ListItemsChanged()
                End If
            End Set
            Get
                Return Me._Items
            End Get
        End Property

        ''' <summary>
        ''' Gibt den aktuell ausgewählten Abschnitt zurück oder legt diesen fest.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den aktuell ausgewählten Abschnitt zurück oder legt diesen fest.")>
        Public Property SelectedSection As String
            Get
                Return Me._SelectedSection
            End Get
            Set
                Me._SelectedSection = Value
            End Set
        End Property

#End Region

#Region "Ereignisse der internen ListBox"

        ''' <summary>
        ''' Setzt die Eigenschaft und schaltet die Buttons.
        ''' </summary>
        Private Sub ListBox_SelectedIndex_Changed(sender As Object, e As EventArgs) Handles ListBox.SelectedIndexChanged
            ' Wenn kein Eintrag ausgewählt -> Eigenschaft auf leer ansonten auf Wert setzen
            If Me.ListBox.SelectedIndex = -1 Then
                Me.ClearPropertySelectedItem()
            Else
                Me.SetPropertySelectedItem()
            End If
            'Event auslösen
            RaiseEvent SelectedItemChanged(Me, New ListEditEventArgs(Me._SelectedSection, Me._SelectedItem, String.Empty))
        End Sub

#End Region

#Region "Ereignisse der internen Buttons"

        ''' <summary>
        ''' Wird aufgerufen wenn der Button "Hinzufügen" geklickt wurde
        ''' </summary>
        Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
            Me.AddNewItem()  ' Eintrag hinzufügen
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub ButtonRename_Click(sender As Object, e As EventArgs) Handles ButtonRename.Click
            Me.RenameItem() ' Eintrag umbenennen
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles TableLayoutPanel2.Click
            Me.DeleteItem() ' Eintrag löschen
        End Sub

#End Region

#Region "interne Ereignisbehandlungen"

        ''' <summary>
        ''' wird ausgelöst wenn sich die Liste der Einträge geändert hat.
        ''' </summary>
        Private Sub IniFileListEdit_ListItemsChanged() Handles Me.ListItemsChanged
            Me.FillListbox() ' Listbox neu befüllen
        End Sub

        ''' <summary>
        ''' Wird ausgelöst wenn sich der Titeltext geändert hat.
        ''' </summary>
        Private Sub IniFileListEdit_TitelTextChanged() Handles Me.TitelTextChanged
            Me.GroupBox.Text = Me._TitelText  ' Titeltext setzen
        End Sub

#End Region

#Region "Definition der internen Funktionen"

        ''' <summary>
        ''' Setzt die Eigenschaft <see cref="_SelectedItem"/> auf den Gewählten Eintrag
        ''' </summary>
        Private Sub SetPropertySelectedItem()
            Me._SelectedItem = CStr(Me.ListBox.SelectedItem) 'Eigenschaft setzen
            'Buttons schalten
            Me.ButtonDelete.Enabled = True
            Me.ButtonRename.Enabled = True
        End Sub

        ''' <summary>
        ''' Setzt die Eigenschaft <see cref="_SelectedItem"/> auf leer.
        ''' </summary>
        Private Sub ClearPropertySelectedItem()
            Me._SelectedItem = String.Empty ' Eigenschaft leeren
            'Buttons schalten
            Me.ButtonDelete.Enabled = False
            Me.ButtonRename.Enabled = False
        End Sub

        ''' <summary>
        ''' Befüllt die Listbox
        ''' </summary>
        Private Sub FillListbox()
            Me.ListBox.Items.Clear()  ' Listbox leeren
            If Me._Items IsNot Nothing Then Me.ListBox.Items.AddRange(Me._Items)  ' Listbox neu befüllen
            Me.ListBox.SelectedIndex = -1 ' kein Eintrag ausgewählt
            Me._SelectedItem = $""  ' Eigenschaft setzen
            ' Buttons schalten
            Me.ButtonAdd.Enabled = True
            Me.ButtonDelete.Enabled = False
            Me.ButtonRename.Enabled = False
            ' Event auslösen
            RaiseEvent SelectedItemChanged(Me, New ListEditEventArgs(String.Empty, Me._SelectedItem, String.Empty))
        End Sub

        ''' <summary>
        ''' Zeigt den Dialog zum Löschen an und wertet das Ergebnis aus.
        ''' </summary>
        Private Sub DeleteItem()
            Dim deldlg As New DeleteItemDialog With {.ItemValue = Me._SelectedItem} ' Dialog initialisieren
            Dim result As DialogResult = deldlg.ShowDialog(Me) ' Dialog anzeigen und Ergebnis abfragen
            If result = DialogResult.OK Then ' Ergebnis auswerten
                ' wenn Antwort Ja -> Event auslösen
                RaiseEvent ItemRemove(Me, New ListEditEventArgs(Me._SelectedSection, Me._SelectedItem, String.Empty))
            End If
        End Sub

        ''' <summary>
        ''' Zeigt den Dialog zum Umbenennen an und wertet das Ergebnis aus.
        ''' </summary>
        Private Sub RenameItem()
            Dim renamedlg As New RenameItemDialog With {.OldItemValue = Me._SelectedItem}  ' Dialog initialisieren
            Dim result As DialogResult = renamedlg.ShowDialog(Me) ' Dialog anzeigen und Ergebnis abfragen
            If result = DialogResult.Yes Then ' Ergebnis auswerten
                ' wenn Antwort Ja -> Event auslösen
                RaiseEvent ItemRename(Me, New ListEditEventArgs(Me._SelectedSection, Me._SelectedItem, renamedlg.NewItemValue))
            End If
        End Sub

        ''' <summary>
        ''' Zeigt den Dialog zum Hinzufügen eines neuen Elementes an und wertet das Ergebnis aus.
        ''' </summary>
        Private Sub AddNewItem()
            Dim newitemdlg As New AddItemDialog ' Dialog initialisieren
            Dim result As DialogResult = newitemdlg.ShowDialog(Me) ' Dialog anzeigen und Ergebnis abfragen
            If result = DialogResult.OK Then ' Ergebnis auswerten
                ' wenn Antwort OK -> Event auslösen
                RaiseEvent ItemAdd(Me, New ListEditEventArgs(Me._SelectedSection, Me._SelectedItem, newitemdlg.NewItemValue))
            End If
        End Sub

#End Region

    End Class

End Namespace