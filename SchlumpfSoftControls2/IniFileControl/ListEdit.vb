' *************************************************************************************************
' ListEdit.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace IniFileControl

    ''' <summary>
    ''' Steuerelement zum Anzeigen und Bearbeiten der Abschnitts- oder Eintrags-Liste
    ''' einer INI-Datei.
    ''' </summary>
    ''' <remarks>
    ''' <para><b>Darstellung:</b> </para>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Eine `GroupBox` mit Titel (Eigenschaft `TitelText`).
    ''' </description>
    '''  </item>
    '''  <item>
    '''   <description>Eine `ListBox` mit Einträgen (Eigenschaft `ListItems`).
    ''' </description>
    '''  </item>
    '''  <item>
    '''   <description>Drei Buttons: Hinzufügen, Umbenennen, Löschen. Interaktion:
    ''' </description>
    '''  </item>
    '''  <item>
    '''   <description>Auswahländerung in der ListBox löst `SelectedItemChanged` aus.
    ''' </description>
    '''  </item>
    '''  <item>
    '''   <description>Button-Klicks lösen semantische Ereignisse aus (`ItemAdd`,
    ''' `ItemRename`, `ItemRemove`), die vom Host verarbeitet werden. </description>
    '''  </item>
    ''' </list>
    ''' <para><b>Zustandsführung:</b> </para>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>`_SelectedSection`: Der aktuell betroffene INI-Abschnitt (vom
    ''' Host gesetzt). </description>
    '''  </item>
    '''  <item>
    '''   <description>`_SelectedItem`: Der aktuell gewählte Eintrag der `ListBox`.
    ''' </description>
    '''  </item>
    '''  <item>
    '''   <description>`_Items`: Datenquelle für die `ListBox`.</description>
    '''  </item>
    ''' </list>
    ''' <para><b>Design-Time:</b> </para>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Mit Toolbox-Attributen versehen, damit es in der VS-Toolbox
    ''' erscheint.</description>
    '''  </item>
    ''' </list>
    ''' </remarks>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Steuerelement zum Anzeigen und Bearbeiten der Abschnitts- oder Eintrags- Liste einer INI - Datei.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(IniFileControl.ListEdit), "ListEdit.bmp")>
    Public Class ListEdit : Inherits System.Windows.Forms.UserControl

#Region "Variablendefinition"

        ''' <summary>
        ''' Schaltfläche zum Löschen des aktuell ausgewählten Eintrags.
        ''' </summary>
        Private WithEvents ButtonDelete As System.Windows.Forms.Button

        ''' <summary>
        ''' Schaltfläche zum Umbenennen des aktuell ausgewählten Eintrags.
        ''' </summary>
        Private WithEvents ButtonRename As System.Windows.Forms.Button

        ''' <summary>
        ''' Schaltfläche zum Hinzufügen eines neuen Eintrags zur Liste.
        ''' </summary>
        Private WithEvents ButtonAdd As System.Windows.Forms.Button

        ''' <summary>
        ''' ListBox zur Anzeige und Auswahl der Eintragsliste (<see cref="ListItems"/>).
        ''' </summary>
        Private WithEvents ListBox As System.Windows.Forms.ListBox

        ''' <summary>
        ''' Umschließende GroupBox zur Darstellung des Titels (<see cref="TitelText"/>) und der enthaltenen Controls.
        ''' </summary>
        Private WithEvents GroupBox As System.Windows.Forms.GroupBox

        ''' <summary>
        ''' Äußeres Layout-Panel zur vertikalen Anordnung von <see cref="ListBox"/> und unterem Button-Panel.
        ''' </summary>
        Private WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel

        ''' <summary>
        ''' Inneres Layout-Panel für die drei Aktions-Schaltflächen (Hinzufügen, Umbenennen, Löschen).
        ''' </summary>
        Private WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel

        ''' <summary>
        ''' Vom Windows Forms Designer verwaltete Komponentenliste.
        ''' </summary>
        Private ReadOnly components As System.ComponentModel.IContainer

        ''' <summary>
        ''' Intern gespeicherter Wert des aktuell ausgewählten Listeneintrags (Text des Items).
        ''' </summary>
        Private _SelectedItem As String = String.Empty

        ''' <summary>
        ''' Intern gespeicherter Abschnittsname, zu dem die Liste der Einträge gehört (vom Host gesetzt).
        ''' </summary>
        Private _SelectedSection As String = String.Empty

        ''' <summary>
        ''' Aktueller Datenbestand aller Einträge, die in der <see cref="ListBox"/> angezeigt werden.
        ''' </summary>
        Private _Items As String() = {""}

        ''' <summary>
        ''' Intern gespeicherter Titeltext (wird in <see cref="GroupBox"/> angezeigt und über <see cref="TitelText"/> exponiert).
        ''' </summary>
        Private _TitelText As String

#End Region

#Region "Ereignisdefinition"

        ''' <summary>
        ''' Wird ausgelöst, wenn über den UI-Button ein neuer Eintrag hinzugefügt werden soll.
        ''' Der Host soll im Handler den Eintrag tatsächlich anlegen und die ListItems ggf. neu setzen.
        ''' </summary>
        <System.ComponentModel.Description("Wird ausgelöst wenn ein Eintrag hinzugefügt werden soll.")>
        <System.ComponentModel.Category("ListEdit")>
        Public Event ItemAdd(sender As Object, e As ListEditEventArgs)

        ''' <summary>
        ''' Wird ausgelöst, wenn ein Eintrag umbenannt werden soll.
        ''' Alte und neue Werte sind in den EventArgs enthalten.
        ''' </summary>
        <System.ComponentModel.Description("Wird ausgelöst wenn ein Eintrag umbenannt werden soll.")>
        <System.ComponentModel.Category("ListEdit")>
        Public Event ItemRename(sender As Object, e As ListEditEventArgs)

        ''' <summary>
        ''' Wird ausgelöst, wenn ein Eintrag gelöscht werden soll.
        ''' </summary>
        <System.ComponentModel.Description("Wird ausgelöst wenn ein Eintrag gelöscht werden soll.")>
        <System.ComponentModel.Category("ListEdit")>
        Public Event ItemRemove(sender As Object, e As ListEditEventArgs)

        ''' <summary>
        ''' Wird ausgelöst, wenn sich der gewählte Eintrag geändert hat (Auswahl in der ListBox).
        ''' </summary>
        <System.ComponentModel.Description("Wird ausgelöst wenn sich der gewählte Eintrag geändert hat.")>
        <System.ComponentModel.Category("ListEdit")>
        Public Event SelectedItemChanged(sender As Object, e As ListEditEventArgs)

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

#Region "neue Eigenschaften"

        ''' <summary>
        ''' Gibt den Text der Titelzeile zurück oder legt diesen fest.
        ''' Beim Setzen wird das interne Ereignis `PropertyTitelTextChanged` ausgelöst, welches das UI aktualisiert.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt den Text der Titelzeile zurück oder legt diesen fest.")>
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
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Data")>
        <System.ComponentModel.Description("Setzt die Elemente der Listbox oder gibt diese zurück.")>
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
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt den aktuell ausgewählten Abschnitt zurück oder legt diesen fest.")>
        Public Property SelectedSection As String
            Get
                Return Me._SelectedSection
            End Get
            Set
                Me._SelectedSection = Value
            End Set
        End Property

#End Region

#Region "öffenliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz des Steuerelements <see cref="ListEdit"/> und übernimmt den initialen Titel.
        ''' </summary>
        ''' <remarks>
        ''' Ruft <see cref="InitializeComponent"/> auf und spiegelt anschließend den in der GroupBox gesetzten Text
        ''' in die interne Variable für <see cref="TitelText"/>.
        ''' </remarks>
        Public Sub New()
            Me.InitializeComponent() ' Dieser Aufruf ist für den Designer erforderlich.
            Me._TitelText = Me.GroupBox.Text
        End Sub

#End Region

#Region "interne Methoden"

        ''' <summary>
        ''' Reagiert auf eine Auswahländerung in der ListBox, aktualisiert internen Zustand und löst <see cref="SelectedItemChanged"/> aus.
        ''' </summary>
        ''' <param name="sender">Die ListBox deren Auswahl sich geändert hat.</param>
        ''' <param name="e">Ereignisargumente (nicht verwendet).</param>
        Private Sub ListBox_SelectedIndex_Changed(sender As Object, e As System.EventArgs) Handles ListBox.SelectedIndexChanged
            If Me.ListBox.SelectedIndex = -1 Then
                Me.ClearPropertySelectedItem()
            Else
                Me.SetPropertySelectedItem()
            End If
            RaiseEvent SelectedItemChanged(Me, New ListEditEventArgs(Me._SelectedSection, Me._SelectedItem, String.Empty))
        End Sub

        ''' <summary>
        ''' Verteilt Button-Klicks auf die jeweilige Aktion (Hinzufügen, Umbenennen, Löschen).
        ''' </summary>
        ''' <param name="sender">Der geklickte Button.</param>
        ''' <param name="e">Ereignisargumente (nicht verwendet).</param>
        Private Sub Button_Click(sender As Object, e As System.EventArgs) Handles ButtonAdd.Click, ButtonRename.Click, ButtonDelete.Click
            Select Case True
                Case sender Is Me.ButtonAdd
                    Me.AddNewItem()
                Case sender Is Me.ButtonRename
                    Me.RenameItem()
                Case sender Is Me.ButtonDelete
                    Me.DeleteItem()
            End Select
        End Sub

        ''' <summary>
        ''' Handler für <c>ListItemsChanged</c>: befüllt die ListBox neu gemäß <see cref="_Items"/>.
        ''' </summary>
        Private Sub IniFileListEdit_ListItemsChanged() Handles Me.ListItemsChanged
            Me.FillListbox()
        End Sub

        ''' <summary>
        ''' Handler für <c>TitelTextChanged</c>: aktualisiert den GroupBox-Titel.
        ''' </summary>
        Private Sub IniFileListEdit_TitelTextChanged() Handles Me.TitelTextChanged
            Me.GroupBox.Text = Me._TitelText
        End Sub

        ''' <summary>
        ''' Setzt den internen Wert für den selektierten Eintrag und aktiviert passende Buttons.
        ''' </summary>
        Private Sub SetPropertySelectedItem()
            Me._SelectedItem = CStr(Me.ListBox.SelectedItem)
            Me.ButtonDelete.Enabled = True
            Me.ButtonRename.Enabled = True
        End Sub

        ''' <summary>
        ''' Leert den internen Wert für den selektierten Eintrag und deaktiviert nicht anwendbare Buttons.
        ''' </summary>
        Private Sub ClearPropertySelectedItem()
            Me._SelectedItem = String.Empty
            Me.ButtonDelete.Enabled = False
            Me.ButtonRename.Enabled = False
        End Sub

        ''' <summary>
        ''' Befüllt die ListBox aus <see cref="_Items"/> und setzt Auswahl sowie Buttonzustände zurück.
        ''' </summary>
        Private Sub FillListbox()
            Me.ListBox.Items.Clear()
            If Me._Items IsNot Nothing Then
                Me.ListBox.Items.AddRange(Me._Items)
            End If
            Me.ListBox.SelectedIndex = -1
            Me._SelectedItem = ""
            Me.ButtonAdd.Enabled = True
            Me.ButtonDelete.Enabled = False
            Me.ButtonRename.Enabled = False
            RaiseEvent SelectedItemChanged(Me, New ListEditEventArgs(String.Empty, Me._SelectedItem, String.Empty))
        End Sub

        ''' <summary>
        ''' Öffnet den Lösch-Dialog und löst bei Bestätigung <see cref="ItemRemove"/> aus.
        ''' </summary>
        Private Sub DeleteItem()
            Dim deldlg As New DeleteItemDialog With {.ItemValue = Me._SelectedItem}
            Dim result As System.Windows.Forms.DialogResult = deldlg.ShowDialog(Me)
            If result = System.Windows.Forms.DialogResult.OK Then
                RaiseEvent ItemRemove(Me, New ListEditEventArgs(Me._SelectedSection, Me._SelectedItem, String.Empty))
            End If
        End Sub

        ''' <summary>
        ''' Öffnet den Umbenennen-Dialog und löst bei Bestätigung <see cref="ItemRename"/> aus.
        ''' </summary>
        Private Sub RenameItem()
            Dim renamedlg As New RenameItemDialog With {.OldItemValue = Me._SelectedItem}
            Dim result As System.Windows.Forms.DialogResult = renamedlg.ShowDialog(Me)
            If result = System.Windows.Forms.DialogResult.Yes Then
                RaiseEvent ItemRename(Me, New ListEditEventArgs(Me._SelectedSection, Me._SelectedItem, renamedlg.NewItemValue))
            End If
        End Sub

        ''' <summary>
        ''' Öffnet den Hinzufügen-Dialog und löst bei Bestätigung <see cref="ItemAdd"/> aus.
        ''' </summary>
        Private Sub AddNewItem()
            Dim newitemdlg As New AddItemDialog
            Dim result As System.Windows.Forms.DialogResult = newitemdlg.ShowDialog(Me)
            If result = System.Windows.Forms.DialogResult.OK Then
                RaiseEvent ItemAdd(Me, New ListEditEventArgs(Me._SelectedSection, Me._SelectedItem, newitemdlg.NewItemValue))
            End If
        End Sub

        ''' <summary>
        ''' Initialisiert alle vom Windows Forms Designer erzeugten Steuerelemente.
        ''' </summary>
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.GroupBox = New System.Windows.Forms.GroupBox()
            Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
            Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
            Me.ButtonAdd = New System.Windows.Forms.Button()
            Me.ButtonRename = New System.Windows.Forms.Button()
            Me.ButtonDelete = New System.Windows.Forms.Button()
            Me.ListBox = New System.Windows.Forms.ListBox()
            Me.GroupBox.SuspendLayout()
            Me.TableLayoutPanel1.SuspendLayout()
            Me.TableLayoutPanel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'GroupBox
            '
            Me.GroupBox.Controls.Add(Me.TableLayoutPanel1)
            Me.GroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GroupBox.Location = New System.Drawing.Point(0, 0)
            Me.GroupBox.Name = "GroupBox"
            Me.GroupBox.Size = New System.Drawing.Size(327, 94)
            Me.GroupBox.TabIndex = 0
            Me.GroupBox.TabStop = False
            Me.GroupBox.Text = "ListEdit"
            '
            'TableLayoutPanel1
            '
            Me.TableLayoutPanel1.ColumnCount = 1
            Dim unused = Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 1)
            Me.TableLayoutPanel1.Controls.Add(Me.ListBox, 0, 0)
            Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 16)
            Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
            Me.TableLayoutPanel1.RowCount = 2
            Dim unused6 = Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Dim unused5 = Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel1.Size = New System.Drawing.Size(321, 75)
            Me.TableLayoutPanel1.TabIndex = 4
            '
            'TableLayoutPanel2
            '
            Me.TableLayoutPanel2.Anchor = CType(System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
            Me.TableLayoutPanel2.AutoSize = True
            Me.TableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.TableLayoutPanel2.ColumnCount = 3
            Dim unused4 = Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Dim unused3 = Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Dim unused2 = Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.TableLayoutPanel2.Controls.Add(Me.ButtonAdd, 0, 0)
            Me.TableLayoutPanel2.Controls.Add(Me.ButtonRename, 1, 0)
            Me.TableLayoutPanel2.Controls.Add(Me.ButtonDelete, 2, 0)
            Me.TableLayoutPanel2.Location = New System.Drawing.Point(6, 39)
            Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
            Me.TableLayoutPanel2.RowCount = 1
            Dim unused1 = Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel2.Size = New System.Drawing.Size(312, 33)
            Me.TableLayoutPanel2.TabIndex = 5
            '
            'ButtonAdd
            '
            Me.ButtonAdd.Location = New System.Drawing.Point(3, 3)
            Me.ButtonAdd.Name = "ButtonAdd"
            Me.ButtonAdd.Size = New System.Drawing.Size(98, 27)
            Me.ButtonAdd.TabIndex = 1
            Me.ButtonAdd.Text = "hinzufügen"
            Me.ButtonAdd.UseVisualStyleBackColor = True
            '
            'ButtonRename
            '
            Me.ButtonRename.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.ButtonRename.Location = New System.Drawing.Point(107, 3)
            Me.ButtonRename.Name = "ButtonRename"
            Me.ButtonRename.Size = New System.Drawing.Size(98, 27)
            Me.ButtonRename.TabIndex = 2
            Me.ButtonRename.Text = "umbenennen"
            Me.ButtonRename.UseVisualStyleBackColor = True
            '
            'ButtonDelete
            '
            Me.ButtonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.ButtonDelete.Location = New System.Drawing.Point(211, 3)
            Me.ButtonDelete.Name = "ButtonDelete"
            Me.ButtonDelete.Size = New System.Drawing.Size(98, 27)
            Me.ButtonDelete.TabIndex = 3
            Me.ButtonDelete.Text = "löschen"
            Me.ButtonDelete.UseVisualStyleBackColor = True
            '
            'ListBox
            '
            Me.ListBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ListBox.FormattingEnabled = True
            Me.ListBox.Location = New System.Drawing.Point(3, 3)
            Me.ListBox.Name = "ListBox"
            Me.ListBox.Size = New System.Drawing.Size(315, 30)
            Me.ListBox.TabIndex = 0
            '
            'ListEdit
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.GroupBox)
            Me.Name = "ListEdit"
            Me.Size = New System.Drawing.Size(327, 94)
            Me.GroupBox.ResumeLayout(False)
            Me.TableLayoutPanel1.ResumeLayout(False)
            Me.TableLayoutPanel1.PerformLayout()
            Me.TableLayoutPanel2.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

#End Region

#Region "überschriebene Methoden"

        ''' <summary>
        ''' Gibt verwaltete Ressourcen frei und bereinigt die Komponentenliste, sofern vorhanden.
        ''' </summary>
        ''' <param name="disposing">True, wenn verwaltete Ressourcen freigegeben werden sollen; andernfalls False.</param>
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

#End Region

    End Class

End Namespace