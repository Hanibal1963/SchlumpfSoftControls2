' *************************************************************************************************
' EntryValueEdit.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace IniFileControl

    ''' <summary>
    ''' Steuerelement zum Anzeigen und Bearbeiten der Einträge eines Abschnitts einer INI - Datei.
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Steuerelement zum Anzeigen und Bearbeiten der Einträge eines Abschnitts einer INI - Datei.")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(IniFileControl.EntryValueEdit), "EntryValueEdit.bmp")> ' Hinweis: Das Bitmap "EntryValueEdit.bmp" muss als eingebettete Ressource vorliegen (BuildAction: Embedded Resource).
    Public Class EntryValueEdit : Inherits System.Windows.Forms.UserControl

        ''' <summary>
        ''' Anzeige-/Titeltext der GroupBox.
        ''' </summary>
        Private _TitelText As String

        ''' <summary>
        ''' Aktueller Eintragswert (interne Quelle der Wahrheit, wird mit der TextBox synchronisiert).
        ''' </summary>
        Private _Value As String = String.Empty

        ''' <summary>
        ''' Der aktuell ausgewählte Abschnitt (INI-Sektion)
        ''' </summary>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0032:Automatisch generierte Eigenschaft verwenden", Justification:="<Ausstehend>")>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Unnötige Unterdrückung entfernen", Justification:="<Ausstehend>")>
        Private _SelectedSection As String = String.Empty

        ''' <summary>
        ''' Der aktuell ausgewählte Eintrag innerhalb der Sektion
        ''' </summary>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0032:Automatisch generierte Eigenschaft verwenden", Justification:="<Ausstehend>")>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Unnötige Unterdrückung entfernen", Justification:="<Ausstehend>")>
        Private _SelectedEntry As String

        ''' <summary>
        ''' Erforderliche Designervariable.
        ''' </summary>
        Private ReadOnly components As System.ComponentModel.IContainer

        ''' <summary>
        ''' Übernehmen-/Bestätigen-Schaltfläche.
        ''' </summary>
        Private WithEvents Button As System.Windows.Forms.Button

        ''' <summary>
        ''' Eingabefeld zur Bearbeitung des Eintragswerts.
        ''' </summary>
        Private WithEvents TextBox As System.Windows.Forms.TextBox

        ''' <summary>
        ''' Rahmen/Container-Element (GroupBox) für die enthaltenen Steuerelemente.
        ''' </summary>
        Private WithEvents GroupBox As System.Windows.Forms.GroupBox

        ''' <summary>
        ''' Layoutcontainer zur Anordnung von TextBox und Button.
        ''' </summary>
        Private WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel

#End Region

#Region "Ereignisdefinition"

        ''' <summary>
        ''' Wird ausgelöst, wenn sich der Wert geändert hat und der Benutzer die Änderung bestätigt hat.
        ''' Typisches Einsatzszenario: Persistieren in die zugrunde liegende INI-Datei außerhalb des Controls.
        ''' </summary>
        <System.ComponentModel.Description("Wird ausgelöst wenn sich der Wert geändert hat.")>
        Public Event ValueChanged(sender As Object, e As EntryValueEditEventArgs)

        ''' <summary>
        ''' Internes Ereignis: Wird nach Änderung der Eigenschaft <see cref="TitelText"/> ausgelöst,
        ''' um die UI (GroupBox.Text) zu aktualisieren.
        ''' </summary>
        Private Event TitelTextChanged()

        ''' <summary>
        ''' Internes Ereignis: Wird ausgelöst, wenn die Eigenschaft <see cref="Value"/> programmatisch gesetzt wurde.
        ''' Synchronisiert die TextBox mit dem internen Wert und deaktiviert den Button.
        ''' </summary>
        Private Event PropertyValueChanged()

#End Region

#Region "neue Eigenschaften"

        ''' <summary>
        ''' Gibt den Text der Titelzeile zurück oder legt diesen fest.
        ''' Das Setzen dieser Eigenschaft aktualisiert die UI (GroupBox.Text) über das interne Ereignis.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt den Text der Titelzeile zurück oder legt diesen fest.")>
        Public Property TitelText As String
            Set(value As String)
                If Me._TitelText <> value Then
                    Me._TitelText = value
                    RaiseEvent TitelTextChanged() ' UI-Update entkoppelt auslösen, damit Logik und Darstellung getrennt bleiben.
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
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt den aktuell ausgewählten Abschnitt zurück oder legt diesen fest.")>
        Public Property SelectedSection As String
            Get
                Return Me._SelectedSection
            End Get
            Set
                Me._SelectedSection = Value ' Keine Nebenwirkungen auf die UI notwendig; die Information wird nur transportiert.
            End Set
        End Property

        ''' <summary>
        ''' Gibt den aktuell ausgewählten Eintrag innerhalb der Sektion zurück oder legt diesen fest.
        ''' Diese Information wird gemeinsam mit dem Abschnitt im ValueChanged-Ereignis übergeben.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Gibt den aktuell ausgewählten Eintrag zurück oder legt diesen fest.")>
        Public Property SelectedEntry As String
            Get
                Return Me._SelectedEntry
            End Get
            Set
                Me._SelectedEntry = Value ' Analog zu SelectedSection: reine Kontextinformation ohne direkte UI-Auswirkung.
            End Set
        End Property

        ''' <summary>
        ''' Gibt den Eintragswert zurück oder legt diesen fest.
        ''' Beim Setzen wird die TextBox über das interne Ereignis synchronisiert und der Übernehmen-Button deaktiviert.
        ''' Hinweis: Das ändert NICHT automatisch die INI-Datei; Persistenz erfolgt über das ValueChanged-Ereignis.
        ''' </summary>
        <System.ComponentModel.Description("Gibt den Eintragswert zurück oder legt diesen fest.")>
        Public Property Value As String
            Get
                Return Me._Value
            End Get
            Set
                If Me._Value <> Value Then
                    Me._Value = Value
                    RaiseEvent PropertyValueChanged() ' Programmgesteuerte Änderung -> UI synchronisieren und Button deaktivieren.
                End If
            End Set
        End Property

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz von <see cref="EntryValueEdit"/> und übernimmt den initialen Titeltext.
        ''' </summary>
        Public Sub New()
            Me.InitializeComponent() ' Dieser Aufruf ist für den Designer erforderlich (legt die im Designer definierten Controls an).
            Me._TitelText = Me.GroupBox.Text ' Initialer Titeltext aus der GroupBox übernehmen, damit Eigenschaft und UI konsistent sind.
        End Sub

#End Region

#Region "interne Methoden"

        ''' <summary>
        ''' Bestätigt eine geänderte Eingabe und löst das <see cref="ValueChanged"/>-Ereignis mit Kontextinformationen aus.
        ''' </summary>
        ''' <param name="sender">Auslösende Schaltfläche.</param>
        ''' <param name="e">Ereignisargumente.</param>
        Private Sub Button_Click(sender As Object, e As System.EventArgs) Handles Button.Click
            ' Ereignis auslösen und Kontextinformationen (Sektion, Eintrag, Wert) mitgeben.
            RaiseEvent ValueChanged(Me, New EntryValueEditEventArgs(Me.SelectedSection, Me.SelectedEntry, Me._Value))
            ' Nach dem Bestätigen wieder deaktivieren, bis sich der Text erneut ändert.
            Me.Button.Enabled = False
        End Sub

        ''' <summary>
        ''' Reagiert auf Benutzereingaben in der TextBox, synchronisiert den internen Wert und steuert den Button-Zustand.
        ''' </summary>
        ''' <param name="sender">Die TextBox.</param>
        ''' <param name="e">Ereignisargumente.</param>
        Private Sub TextBox_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox.TextChanged
            If Me._Value <> Me.TextBox.Text Then ' Prüfung, ob sich der Wert geändert hat (Two-Way-Sync: interner Wert <-> TextBox.Text)
                Me.Button.Enabled = True ' Es liegt eine nicht bestätigte Änderung vor -> Button aktivieren.
                Me._Value = Me.TextBox.Text ' Internen Wert direkt mitführen, damit der Button-Klick den aktuellen Text erhält.
            Else
                Me.Button.Enabled = False ' TextBox wurde programmatisch auf den internen Wert gesetzt -> keine Änderung offen.
            End If
        End Sub

        ''' <summary>
        ''' Aktualisiert den GroupBox-Titel, nachdem die Eigenschaft <see cref="TitelText"/> geändert wurde.
        ''' </summary>
        Private Sub IniFileCommentEdit_TitelTextChanged() Handles Me.TitelTextChanged
            Me.GroupBox.Text = Me._TitelText ' Titeltext in der GroupBox aktualisieren.
        End Sub

        ''' <summary>
        ''' Synchronisiert die TextBox mit dem internen Wert und deaktiviert den Button,
        ''' wenn die Eigenschaft <see cref="Value"/> programmatisch geändert wurde.
        ''' </summary>
        Private Sub IniFileEntryValueEdit_PropertyValueChanged() Handles Me.PropertyValueChanged
            Me.TextBox.Text = Me._Value ' TextBox an den internen Wert angleichen (dies löst TextChanged aus, das den Button korrekt deaktiviert).
            Me.Button.Enabled = False ' Sicherheitshalber explizit deaktivieren.
        End Sub

        ''' <summary>
        ''' Bereinigt verwendete Ressourcen.
        ''' </summary>
        ''' <param name="disposing">True, um verwaltete Ressourcen zu bereinigen; andernfalls False.</param>
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

        ''' <summary>
        ''' Initialisiert die im Designer angelegten Steuerelemente und deren Layout.
        ''' </summary>
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.Button = New System.Windows.Forms.Button()
            Me.TextBox = New System.Windows.Forms.TextBox()
            Me.GroupBox = New System.Windows.Forms.GroupBox()
            Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
            Me.GroupBox.SuspendLayout()
            Me.TableLayoutPanel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Button
            '
            Me.Button.Anchor = CType(System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
            Me.Button.Location = New System.Drawing.Point(48, 29)
            Me.Button.Name = "Button"
            Me.Button.Size = New System.Drawing.Size(98, 25)
            Me.Button.TabIndex = 0
            Me.Button.Text = "übernehmen"
            Me.Button.UseVisualStyleBackColor = True
            '
            'TextBox
            '
            Me.TextBox.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left _
            Or System.Windows.Forms.AnchorStyles.Right
            Me.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.TextBox.Location = New System.Drawing.Point(3, 3)
            Me.TextBox.Name = "TextBox"
            Me.TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.TextBox.Size = New System.Drawing.Size(143, 20)
            Me.TextBox.TabIndex = 1
            Me.TextBox.WordWrap = False
            '
            'GroupBox
            '
            Me.GroupBox.Controls.Add(Me.TableLayoutPanel1)
            Me.GroupBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GroupBox.Location = New System.Drawing.Point(0, 0)
            Me.GroupBox.Name = "GroupBox"
            Me.GroupBox.Size = New System.Drawing.Size(155, 73)
            Me.GroupBox.TabIndex = 2
            Me.GroupBox.TabStop = False
            Me.GroupBox.Text = "EntryValueEdit"
            '
            'TableLayoutPanel1
            '
            Me.TableLayoutPanel1.ColumnCount = 1
            Dim unused2 = Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel1.Controls.Add(Me.TextBox, 0, 0)
            Me.TableLayoutPanel1.Controls.Add(Me.Button, 0, 1)
            Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 16)
            Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
            Me.TableLayoutPanel1.RowCount = 2
            Dim unused1 = Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Dim unused = Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel1.Size = New System.Drawing.Size(149, 54)
            Me.TableLayoutPanel1.TabIndex = 2
            '
            'EntryValueEdit
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.GroupBox)
            Me.Name = "EntryValueEdit"
            Me.Size = New System.Drawing.Size(155, 73)
            Me.GroupBox.ResumeLayout(False)
            Me.TableLayoutPanel1.ResumeLayout(False)
            Me.TableLayoutPanel1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

    End Class

End Namespace