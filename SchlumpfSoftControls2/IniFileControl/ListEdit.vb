' *************************************************************************************************
' ListEdit.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
'Imports SchlumpfSoft.Controls.Attribute

Namespace IniFileControl

    '''' <summary>
    '''' Steuerelement zum Anzeigen und Bearbeiten der Abschnitts- oder Eintrags-Liste einer INI-Datei.
    '''' </summary>
    ''' <remarks>
    '''' Darstellung:
    '''' - Eine `GroupBox` mit Titel (Eigenschaft `TitelText`).
    '''' - Eine `ListBox` mit Einträgen (Eigenschaft `ListItems`).
    '''' - Drei Buttons: Hinzufügen, Umbenennen, Löschen.
    ''''
    '''' Interaktion:
    '''' - Auswahländerung in der ListBox löst `SelectedItemChanged` aus.
    '''' - Button-Klicks lösen semantische Ereignisse aus (`ItemAdd`, `ItemRename`, `ItemRemove`), die vom Host verarbeitet werden.
    ''''
    '''' Zustandsführung:
    '''' - `_SelectedSection`: Der aktuell betroffene INI-Abschnitt (vom Host gesetzt).
    '''' - `_SelectedItem`: Der aktuell gewählte Eintrag der `ListBox`.
    '''' - `_Items`: Datenquelle für die `ListBox`.
    ''''
    '''' Design-Time:
    '''' - Mit Toolbox-Attributen versehen, damit es in der VS-Toolbox erscheint.
    '''' </remarks>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Steuerelement zum Anzeigen und Bearbeiten der Abschnitts- oder Eintrags- Liste einer INI - Datei.")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(IniFileControl.ListEdit), "ListEdit.bmp")>
    Public Class ListEdit

        Inherits UserControl

#Region "Definition der internen Eigenschaftsvariablen"

        ' Hält den aktuell in der ListBox gewählten Item-Text.
        Private _SelectedItem As String = String.Empty
        ' Hält den aktuell ausgewählten Abschnittsnamen (durch übergeordneten Kontext gesetzt).
        Private _SelectedSection As String = String.Empty
        ' Interne Datenquelle für die ListBox. Wird als Ganzes gesetzt/ersetzt.
        Private _Items As String() = {""}
        ' Zwischenspeicher für den Titel (GroupBox.Text).
        Private _TitelText As String

#End Region

        Public Sub New()
            ' Dieser Aufruf ist für den Designer erforderlich.
            Me.InitializeComponent()
            ' Nach dem InitializeComponent-Aufruf: den anfänglichen GroupBox-Titel in die Eigenschaft spiegeln.
            ' Dadurch bleibt der Property-Zustand kohärent zum UI-Initialzustand.
            Me._TitelText = Me.GroupBox.Text
        End Sub

#Region "Definition der öffentlichen Ereignisse"

        ''' <summary>
        ''' Wird ausgelöst, wenn über den UI-Button ein neuer Eintrag hinzugefügt werden soll.
        ''' Der Host soll im Handler den Eintrag tatsächlich anlegen und die ListItems ggf. neu setzen.
        ''' </summary>
        <Description("Wird ausgelöst wenn ein Eintrag hinzugefügt werden soll.")>
        <Category("ListEdit")>
        Public Event ItemAdd(sender As Object, e As ListEditEventArgs)

        ''' <summary>
        ''' Wird ausgelöst, wenn ein Eintrag umbenannt werden soll.
        ''' Alte und neue Werte sind in den EventArgs enthalten.
        ''' </summary>
        <Description("Wird ausgelöst wenn ein Eintrag umbenannt werden soll.")>
        <Category("ListEdit")>
        Public Event ItemRename(sender As Object, e As ListEditEventArgs)

        ''' <summary>
        ''' Wird ausgelöst, wenn ein Eintrag gelöscht werden soll.
        ''' </summary>
        <Description("Wird ausgelöst wenn ein Eintrag gelöscht werden soll.")>
        <Category("ListEdit")>
        Public Event ItemRemove(sender As Object, e As ListEditEventArgs)

        ''' <summary>
        ''' Wird ausgelöst, wenn sich der gewählte Eintrag geändert hat (Auswahl in der ListBox).
        ''' </summary>
        <Description("Wird ausgelöst wenn sich der gewählte Eintrag geändert hat.")>
        <Category("ListEdit")>
        Public Event SelectedItemChanged(sender As Object, e As ListEditEventArgs)

#End Region

#Region "Definition der internen Ereignisse"

        ''' <summary>
        ''' Internes Ereignis: wird ausgelöst, wenn sich der Titeltext geändert hat.
        ''' Dient zur Entkopplung der Public-Property vom UI-Update.
        ''' </summary>
        Private Event TitelTextChanged()

        ''' <summary>
        ''' Internes Ereignis: wird ausgelöst, wenn sich die Liste der Einträge geändert hat.
        ''' Führt zum Neuaufbau der ListBox.
        ''' </summary>
        Private Event ListItemsChanged()

#End Region

#Region "Definition der neuen Eigenschaften"

        ''' <summary>
        ''' Gibt den Text der Titelzeile zurück oder legt diesen fest.
        ''' Beim Setzen wird das interne Ereignis `TitelTextChanged` ausgelöst, welches das UI aktualisiert.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den Text der Titelzeile zurück oder legt diesen fest.")>
        Public Property TitelText As String
            Set(value As String)
                ' Nur ändern, wenn sich der Wert wirklich unterscheidet, um unnötige UI-Updates zu vermeiden.
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
        ''' Setzt die Elemente der ListBox oder gibt diese zurück.
        ''' Hinweis: Der Vergleich nutzt Referenzungleichheit (IsNot). Das Array wird als Ganzes ersetzt.
        ''' Das interne Ereignis `ListItemsChanged` triggert anschließend das Befüllen der ListBox.
        ''' </summary>
        <Browsable(True)>
        <Category("Data")>
        <Description("Setzt die Elemente der Listbox oder gibt diese zurück.")>
        Public Property ListItems() As String()
            Set
                ' Nur aktualisieren, wenn eine andere Array-Instanz zugewiesen wird.
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
        ''' Wird typischerweise vom Host beim Wechsel der INI-Sektion gesetzt, damit Events den Kontext kennen.
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
        ''' Reagiert auf eine Auswahländerung in der ListBox.
        ''' - Aktualisiert den internen Zustand (`_SelectedItem`).
        ''' - Schaltet die Buttons abhängig von der Auswahl.
        ''' - Löst `SelectedItemChanged` mit aktuellem Kontext aus.
        ''' </summary>
        Private Sub ListBox_SelectedIndex_Changed(sender As Object, e As EventArgs) Handles ListBox.SelectedIndexChanged
            ' Wenn kein Eintrag ausgewählt -> Eigenschaft auf leer, ansonsten auf den gewählten Wert setzen.
            If Me.ListBox.SelectedIndex = -1 Then
                Me.ClearPropertySelectedItem()
            Else
                Me.SetPropertySelectedItem()
            End If
            ' Ausgewählten Kontext propagieren.
            RaiseEvent SelectedItemChanged(Me, New ListEditEventArgs(Me._SelectedSection, Me._SelectedItem, String.Empty))
        End Sub

#End Region

#Region "Ereignisse der internen Buttons"

        ''' <summary>
        ''' Reagiert auf Klicks der drei Buttons.
        ''' Leitet die Aktion an die entsprechenden internen Methoden weiter.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub Button_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click, ButtonRename.Click, ButtonDelete.Click
            Select Case True
                Case sender Is Me.ButtonAdd
                    Me.AddNewItem()  ' Klick auf "Hinzufügen": zeigt den Dialog und löst bei Bestätigung `ItemAdd` aus.
                Case sender Is Me.ButtonRename
                    Me.RenameItem() ' Klick auf "Umbenennen": zeigt den Dialog und löst bei Bestätigung `ItemRename` aus.
                Case sender Is Me.ButtonDelete
                    Me.DeleteItem() ' Klick auf "Löschen": zeigt den Dialog und löst bei Bestätigung `ItemRemove` aus.
            End Select
        End Sub

#End Region

#Region "interne Ereignisbehandlungen"

        ''' <summary>
        ''' Reaktion auf geänderte ListItems:
        ''' - ListBox wird neu befüllt.
        ''' - Auswahl und Buttonzustände werden zurückgesetzt.
        ''' - `SelectedItemChanged` wird mit leerer Auswahl gemeldet.
        ''' </summary>
        Private Sub IniFileListEdit_ListItemsChanged() Handles Me.ListItemsChanged
            Me.FillListbox() ' Listbox neu befüllen
        End Sub

        ''' <summary>
        ''' Reaktion auf geänderten Titeltext:
        ''' - Der GroupBox-Titel wird aktualisiert.
        ''' </summary>
        Private Sub IniFileListEdit_TitelTextChanged() Handles Me.TitelTextChanged
            Me.GroupBox.Text = Me._TitelText  ' Titeltext setzen
        End Sub

#End Region

#Region "Definition der internen Funktionen"

        ''' <summary>
        ''' Setzt die Eigenschaft <see cref="_SelectedItem"/> auf den aktuell gewählten Eintrag.
        ''' Schaltet die Aktionen "Löschen" und "Umbenennen" aktiv.
        ''' </summary>
        Private Sub SetPropertySelectedItem()
            Me._SelectedItem = CStr(Me.ListBox.SelectedItem) ' Eigenschaft setzen
            ' Buttons schalten: Aktionen für selektiertes Item erlauben.
            Me.ButtonDelete.Enabled = True
            Me.ButtonRename.Enabled = True
        End Sub

        ''' <summary>
        ''' Setzt die Eigenschaft <see cref="_SelectedItem"/> auf leer.
        ''' Deaktiviert Aktionen, die eine Auswahl erfordern.
        ''' </summary>
        Private Sub ClearPropertySelectedItem()
            Me._SelectedItem = String.Empty ' Eigenschaft leeren
            ' Buttons schalten: ohne Auswahl keine Lösch-/Umbenenn-Aktion.
            Me.ButtonDelete.Enabled = False
            Me.ButtonRename.Enabled = False
        End Sub

        ''' <summary>
        ''' Befüllt die ListBox basierend auf <see cref="_Items"/>.
        ''' - Löscht vorhandene Einträge.
        ''' - Fügt neue Einträge hinzu (falls vorhanden).
        ''' - Setzt Auswahl und Buttonzustände zurück.
        ''' - Meldet nach außen, dass aktuell kein Item selektiert ist.
        ''' </summary>
        Private Sub FillListbox()
            Me.ListBox.Items.Clear()  ' Listbox leeren
            If Me._Items IsNot Nothing Then
                Me.ListBox.Items.AddRange(Me._Items)  ' Listbox neu befüllen
            End If
            Me.ListBox.SelectedIndex = -1 ' kein Eintrag ausgewählt
            Me._SelectedItem = $""  ' Eigenschaft setzen (entspricht String.Empty)

            ' Buttons schalten: Hinzufügen ist immer möglich, andere erfordern Auswahl.
            Me.ButtonAdd.Enabled = True
            Me.ButtonDelete.Enabled = False
            Me.ButtonRename.Enabled = False

            ' Nach außen signalisieren, dass es aktuell keinen selektierten Eintrag gibt.
            RaiseEvent SelectedItemChanged(Me, New ListEditEventArgs(String.Empty, Me._SelectedItem, String.Empty))
        End Sub

        ''' <summary>
        ''' Zeigt den Dialog zum Löschen an und wertet das Ergebnis aus.
        ''' Bei Bestätigung (OK) wird `ItemRemove` mit aktuellem Abschnitt und Item ausgelöst.
        ''' Die tatsächliche Lösch-Operation erfolgt im Host (Event-Handler).
        ''' </summary>
        Private Sub DeleteItem()
            Dim deldlg As New DeleteItemDialog With {.ItemValue = Me._SelectedItem} ' Dialog initialisieren
            Dim result As DialogResult = deldlg.ShowDialog(Me) ' Dialog anzeigen und Ergebnis abfragen
            If result = DialogResult.OK Then ' Ergebnis auswerten
                ' Wenn Antwort OK -> Event auslösen (Host löscht Eintrag und aktualisiert ListItems).
                RaiseEvent ItemRemove(Me, New ListEditEventArgs(Me._SelectedSection, Me._SelectedItem, String.Empty))
            End If
        End Sub

        ''' <summary>
        ''' Zeigt den Dialog zum Umbenennen an und wertet das Ergebnis aus.
        ''' Bei Bestätigung (Yes) wird `ItemRename` mit altem und neuem Wert ausgelöst.
        ''' Hinweis: Der Dialog verwendet hier `DialogResult.Yes` als Bestätigungswert (nicht OK).
        ''' </summary>
        Private Sub RenameItem()
            Dim renamedlg As New RenameItemDialog With {.OldItemValue = Me._SelectedItem}  ' Dialog initialisieren
            Dim result As DialogResult = renamedlg.ShowDialog(Me) ' Dialog anzeigen und Ergebnis abfragen
            If result = DialogResult.Yes Then ' Ergebnis auswerten
                ' Wenn Antwort Ja -> Event auslösen (Host führt Umbenennen aus und aktualisiert ListItems).
                RaiseEvent ItemRename(Me, New ListEditEventArgs(Me._SelectedSection, Me._SelectedItem, renamedlg.NewItemValue))
            End If
        End Sub

        ''' <summary>
        ''' Zeigt den Dialog zum Hinzufügen eines neuen Eintrags an und wertet das Ergebnis aus.
        ''' Bei Bestätigung (OK) wird `ItemAdd` mit dem neuen Wert ausgelöst.
        ''' Die tatsächliche Hinzufügungs-Operation erfolgt im Host (Event-Handler).
        ''' </summary>
        Private Sub AddNewItem()
            Dim newitemdlg As New AddItemDialog ' Dialog initialisieren
            Dim result As DialogResult = newitemdlg.ShowDialog(Me) ' Dialog anzeigen und Ergebnis abfragen
            If result = DialogResult.OK Then ' Ergebnis auswerten
                ' Wenn Antwort OK -> Event auslösen (Host fügt Eintrag hinzu und aktualisiert ListItems).
                RaiseEvent ItemAdd(Me, New ListEditEventArgs(Me._SelectedSection, Me._SelectedItem, newitemdlg.NewItemValue))
            End If
        End Sub

#End Region

    End Class

End Namespace