' *************************************************************************************************
' EntryValueEdit.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
'Imports SchlumpfSoft.Controls.Attribute

Namespace IniFileControl

    '''' <summary>
    '''' Steuerelement zum Anzeigen und Bearbeiten der Einträge eines Abschnitts einer INI - Datei.
    '''' </summary>
    '''<remarks>
    ''' Hinweise zum Aufbau:
    ''' - Dieses UserControl setzt voraus, dass im Designer-Teil (Partial-Klasse) mindestens folgende Steuerelemente
    ''' vorhanden sind: GroupBox (als Titelrahmen), TextBox (zur Eingabe des Wertes) und Button (zum Bestätigen/Übernehmen).
    ''' - Das Steuerelement kapselt den aktuellen Kontext (Abschnitt + Eintrag) sowie den zugehörigen Wert.
    ''' - Änderungen am TextBox-Inhalt aktivieren den Button. Ein Klick auf den Button bestätigt die Änderung und löst ein Ereignis aus.
    '''</remarks>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Steuerelement zum Anzeigen und Bearbeiten der Einträge eines Abschnitts einer INI - Datei.")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(IniFileControl.EntryValueEdit), "EntryValueEdit.bmp")> ' Hinweis: Das Bitmap "EntryValueEdit.bmp" muss als eingebettete Ressource vorliegen (BuildAction: Embedded Resource).
    Public Class EntryValueEdit

        Inherits UserControl

        ' ---------------------------
        ' Private Felder (Zustand)
        ' ---------------------------
        ' Anzeige-/Titeltext der GroupBox
        Private _TitelText As String
        ' Aktueller Eintragswert (als interne Quelle der Wahrheit, wird mit der TextBox synchronisiert)
        Private _Value As String = String.Empty
        ' Der aktuell ausgewählte Abschnitt (INI-Sektion)
        Private _SelectedSection As String = String.Empty
        ' Der aktuell ausgewählte Eintrag innerhalb der Sektion
        Private _SelectedEntry As String

#Region "Definition der Ereignisse"

        ''' <summary>
        ''' Wird ausgelöst, wenn sich der Wert geändert hat und der Benutzer die Änderung bestätigt hat.
        ''' Typisches Einsatzszenario: Persistieren in die zugrunde liegende INI-Datei außerhalb des Controls.
        ''' </summary>
        <Description("Wird ausgelöst wenn sich der Wert geändert hat.")>
        Public Event ValueChanged(sender As Object, e As EntryValueEditEventArgs)

        ' Internes Ereignis: Titeltext wurde über die Eigenschaft geändert.
        ' Dient dazu, die UI (GroupBox.Text) zu aktualisieren, ohne externe Abhängigkeiten.
        Private Event TitelTextChanged()
        ' Internes Ereignis: Der Wert wurde programmatisch (über die Eigenschaft Value) verändert.
        ' Dient dazu, die TextBox an den internen Wert anzupassen und den Button zu deaktivieren.
        Private Event PropertyValueChanged()

#End Region

        Public Sub New()
            ' Dieser Aufruf ist für den Designer erforderlich (legt die im Designer definierten Controls an).
            Me.InitializeComponent()
            ' Initialer Titeltext aus der GroupBox übernehmen, damit Eigenschaft und UI konsistent sind.
            Me._TitelText = Me.GroupBox.Text
        End Sub

#Region "Definition der neuen Eigenschaften"

        ''' <summary>
        ''' Gibt den Text der Titelzeile zurück oder legt diesen fest.
        ''' Das Setzen dieser Eigenschaft aktualisiert die UI (GroupBox.Text) über das interne Ereignis.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den Text der Titelzeile zurück oder legt diesen fest.")>
        Public Property TitelText As String
            Set(value As String)
                If Me._TitelText <> value Then
                    Me._TitelText = value
                    ' UI-Update entkoppelt auslösen, damit Logik und Darstellung getrennt bleiben.
                    RaiseEvent TitelTextChanged()
                End If
            End Set
            Get
                Return Me._TitelText
            End Get
        End Property

        ''' <summary>
        ''' Gibt den aktuell ausgewählten Abschnitt (INI-Sektion) zurück oder legt diesen fest.
        ''' Diese Information wird zusammen mit dem Eintragsnamen im ValueChanged-Ereignis mitgegeben.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den aktuell ausgewählten Abschnitt zurück oder legt diesen fest.")>
        Public Property SelectedSection As String
            Get
                Return Me._SelectedSection
            End Get
            Set
                ' Keine Nebenwirkungen auf die UI notwendig; die Information wird nur transportiert.
                Me._SelectedSection = Value
            End Set
        End Property

        ''' <summary>
        ''' Gibt den aktuell ausgewählten Eintrag innerhalb der Sektion zurück oder legt diesen fest.
        ''' Diese Information wird gemeinsam mit dem Abschnitt im ValueChanged-Ereignis übergeben.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Gibt den aktuell ausgewählten Eintrag zurück oder legt diesen fest.")>
        Public Property SelectedEntry As String
            Get
                Return Me._SelectedEntry
            End Get
            Set
                ' Analog zu SelectedSection: reine Kontextinformation ohne direkte UI-Auswirkung.
                Me._SelectedEntry = Value
            End Set
        End Property

        ''' <summary>
        ''' Gibt den Eintragswert zurück oder legt diesen fest.
        ''' Beim Setzen wird die TextBox über das interne Ereignis synchronisiert und der Übernehmen-Button deaktiviert.
        ''' Hinweis: Das ändert NICHT automatisch die INI-Datei; Persistenz erfolgt über das ValueChanged-Ereignis.
        ''' </summary>
        <Description("Gibt den Eintragswert zurück oder legt diesen fest.")>
        Public Property Value As String
            Get
                Return Me._Value
            End Get
            Set
                If Me._Value <> Value Then
                    Me._Value = Value
                    ' Programmgesteuerte Änderung -> UI synchronisieren und Button deaktivieren.
                    RaiseEvent PropertyValueChanged()
                End If
            End Set
        End Property

#End Region

#Region "Definition der internen Ereignisbehandlungen"

        ' Klick auf den Bestätigungs-/Übernehmen-Button.
        ' Semantik: Die aktuelle Änderung wird "veröffentlicht" – d.h. es wird ValueChanged ausgelöst,
        ' damit ein externer Listener (z.B. Form/Presenter) den neuen Wert persistieren kann.
        Private Sub Button_Click(sender As Object, e As System.EventArgs) Handles Button.Click
            ' Ereignis auslösen und Kontextinformationen (Sektion, Eintrag, Wert) mitgeben.
            RaiseEvent ValueChanged(Me, New EntryValueEditEventArgs(Me._SelectedSection, Me._SelectedEntry, Me._Value))
            ' Nach dem Bestätigen wieder deaktivieren, bis sich der Text erneut ändert.
            Me.Button.Enabled = False
        End Sub

        ' Reaktion auf Benutzereingaben in der TextBox.
        ' Aktiviert den Button nur, wenn sich der Text vom internen Wert unterscheidet.
        ' Dadurch wird unnötiges Auslösen von ValueChanged verhindert.
        Private Sub TextBox_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox.TextChanged
            ' Prüfung, ob sich der Wert geändert hat (Two-Way-Sync: interner Wert <-> TextBox.Text)
            If Me._Value <> Me.TextBox.Text Then
                ' Es liegt eine nicht bestätigte Änderung vor -> Button aktivieren.
                Me.Button.Enabled = True
                ' Internen Wert direkt mitführen, damit der Button-Klick den aktuellen Text erhält.
                Me._Value = Me.TextBox.Text
            Else
                ' TextBox wurde programmatisch auf den internen Wert gesetzt -> keine Änderung offen.
                Me.Button.Enabled = False
            End If
        End Sub

        ' UI-Update für den Titel, wenn die Eigenschaft TitelText geändert wurde.
        Private Sub IniFileCommentEdit_TitelTextChanged() Handles Me.TitelTextChanged
            ' Titeltext in der GroupBox aktualisieren.
            Me.GroupBox.Text = Me._TitelText
        End Sub

        ' UI-Update für den Wert, wenn die Eigenschaft Value programmatisch gesetzt wurde.
        Private Sub IniFileEntryValueEdit_PropertyValueChanged() Handles Me.PropertyValueChanged
            ' TextBox an den internen Wert angleichen (dies löst TextChanged aus, das den Button korrekt deaktiviert).
            Me.TextBox.Text = Me._Value
            ' Sicherheitshalber explizit deaktivieren.
            Me.Button.Enabled = False
        End Sub

#End Region

        ' Anmerkungen zur Verwendung:
        ' - Threading: Alle Methoden setzen UI-Elemente und müssen daher im UI-Thread aufgerufen werden.
        ' - Ereignismodell: Benutzer tippt -> Button wird aktiv -> Klick -> ValueChanged (mit Kontext) -> externer Listener speichert Wert.
        ' - Programmgesteuertes Setzen von Value führt nie direkt zu ValueChanged, sondern nur zur UI-Synchronisierung.
        ' - SelectedSection/SelectedEntry liefern den Kontext für ValueChanged und können außerhalb dieses Controls verwaltet werden.

    End Class

End Namespace