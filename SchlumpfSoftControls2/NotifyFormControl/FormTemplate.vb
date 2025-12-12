' *************************************************************************************************
' FormTemplate.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace NotifyFormControl

    ' Formularvorlage für die Anzeige eines Benachrichtigungsfensters (Popup) mit Titel, Symbol und Nachricht.
    Friend Class FormTemplate : Inherits System.Windows.Forms.Form

#Region "Variablen"

        Public Shared BackgroundColor As System.Drawing.Color  ' Hintergrundfarbe des Formulars.
        Public Shared FontColor As System.Drawing.Color ' Schriftfarbe für Titel und Nachricht.
        Public Shared Image As System.Drawing.Image ' Anzuzeigendes Symbolbild.
        Public Shared Message As String ' Textnachricht die im Fenster dargestellt wird.
        Public Shared ShowTime As Integer ' Zeit in Millisekunden bis zum automatischen Schließen (0 = kein Autoclose).
        Public Shared TextFieldColor As System.Drawing.Color ' Hintergrundfarbe des Nachrichtentextfeldes.
        Public Shared Title As String ' Titeltext für die Titelleiste.
        Public Shared TitleBarColor As System.Drawing.Color ' Hintergrundfarbe der Titelleiste.
        Private ReadOnly LabelClose As New System.Windows.Forms.Label  ' Label-Steuerelement zum Schließen des Fensters.
        Private ReadOnly LabelTitle As New System.Windows.Forms.Label ' Label-Steuerelement zur Anzeige des Titels.
        Private ReadOnly PanelSpacer As New System.Windows.Forms.Panel ' Trenn-Panel zwischen Symbol und Text.
        Private ReadOnly PanelTitle As New System.Windows.Forms.Panel ' Panel für die Titelleiste, enthält Titel und Schließen-Label.
        Private ReadOnly PictureBoxImage As New System.Windows.Forms.PictureBox ' Bildanzeige für das Symbol.
        Private ReadOnly RichTextBoxMessage As New System.Windows.Forms.RichTextBox ' RichTextBox zur Anzeige der Nachricht.
        Private CloseThread As System.Threading.Thread ' Hintergrundthread für das automatische Schließen.

#End Region

#Region "öffentliche Methoden"

        ' Initialisiert die Form-Eigenschaften und zeigt das Formular an.
        Public Sub Initialize()
            With Me
                .MaximizeBox = False
                .MinimizeBox = False
                .BackColor = BackgroundColor
                .ForeColor = FontColor
                .Size = New System.Drawing.Size(504, 138)
                .FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
                .StartPosition = System.Windows.Forms.FormStartPosition.Manual
            End With
            Me.Show()
        End Sub

        ' Gibt verwendete Ressourcen frei und ruft die Basisimplementation auf.
        Protected Overrides Sub Dispose(disposing As Boolean)
            'Bereinigung durchführen
            With Me
                .LabelClose.Dispose()
                .LabelTitle.Dispose()
                .PanelSpacer.Dispose()
                .PanelTitle.Dispose()
                .PictureBoxImage.Dispose()
                .RichTextBoxMessage.Dispose()
            End With
            MyBase.Dispose(disposing)
        End Sub

        ' Finalizer; ruft Basis-Finalizer auf. (Keine zusätzliche Logik vorhanden.)
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

#End Region

#Region "interne Methoden"

        ' Schließt das Formular thread-sicher (Invoke bei Bedarf).
        Private Sub CloseForm()
            If Me.InvokeRequired Then
                Dim unused = Me.Invoke(New System.Windows.Forms.MethodInvoker(AddressOf Me.CloseForm))
            Else
                Me.Close()
            End If
        End Sub

        ' Fügt die benötigten Steuerelemente dem Formular hinzu.
        Private Sub AddControls()
            With Me.Controls
                .Add(Me.LabelTitle)
                .Add(Me.LabelClose)
                .Add(Me.PanelSpacer)
                .Add(Me.PanelTitle)
                .Add(Me.RichTextBoxMessage)
                .Add(Me.PictureBoxImage)
            End With
        End Sub

        ' Führt den automatischen Schließvorgang nach Ablauf der eingestellten Zeit aus.
        Private Sub AutoClose()
            'Fenster nur automatisch schließen wenn Zeit > 0 ist.
            If ShowTime > 0 Then
                System.Threading.Thread.Sleep(ShowTime)
                Me.CloseForm()
            End If
        End Sub

        ' Animierter Ausblendvorgang beim Schließen des Formulars.
        Private Sub Form_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
            System.Threading.Thread.Sleep(150)
            For iCount As Integer = 90 To 10 Step -15
                Me.Opacity = iCount / 110
                Me.Refresh()
                System.Threading.Thread.Sleep(60)
            Next
        End Sub

        ' Lädt und initialisiert die Steuerelemente, positioniert das Formular und startet den Autoclose-Thread.
        Private Sub Form_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            With Me
                .Opacity = 0.1
                .SetPropertys_Lbl_Close()
                .SetPropertys_Panel_Spacer()
                .SetPropertys_Pb_Image()
                .SetPropertys_Panel_Title()
                .SetPropertys_Lbl_Title()
                .SetPropertys_Txt_Msg()
                .AddControls()
                .ActiveControl = .Controls.Item(1)
            End With
            'Ändern Sie die Position in die untere rechte Ecke
            Dim x As Integer = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width
            Dim y As Integer = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - Me.Height - 50
            Do Until x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width
                x -= 1
                Me.Location = New System.Drawing.Point(x, y)
            Loop
            AddHandler Me.LabelClose.Click, AddressOf Me.Lbl_Close_Click
            Me.FormFadeIn()
            'Starte Thread für Autoclose-Popup.
            Me.CloseThread = New System.Threading.Thread(AddressOf Me.AutoClose) With {.IsBackground = True}
            Me.CloseThread.Start()
        End Sub

        ' Führt die Einblendanimation durch.
        Private Sub FormFadeIn()
            For iCount As Integer = 10 To 100 Step +15
                Me.Opacity = iCount / 100
                Me.Refresh()
                System.Threading.Thread.Sleep(60)
            Next
        End Sub

        ' Ereignishandler für das Schließen-Label. Schließt das Formular sofort.
        Private Sub Lbl_Close_Click(sender As Object, e As System.EventArgs)
            Me.Close()
        End Sub

        ' Setzt die Eigenschaften des Schließen-Labels.
        Private Sub SetPropertys_Lbl_Close()
            With Me.LabelClose
                .AutoSize = True
                .BackColor = TitleBarColor
                .ForeColor = FontColor
                .Location = New System.Drawing.Point(478, 9)
                .Name = "LblClose"
                .Size = New System.Drawing.Size(14, 13)
                .TabIndex = 1
                .Text = "X"
            End With
        End Sub

        ' Setzt die Eigenschaften des Titel-Labels.
        Private Sub SetPropertys_Lbl_Title()
            With Me.LabelTitle
                .AutoSize = True
                .BackColor = TitleBarColor
                .ForeColor = FontColor
                .Location = New System.Drawing.Point(11, 9)
                .Name = "LblTitle"
                .Size = New System.Drawing.Size(27, 13)
                .TabIndex = 0
                .Text = Title
            End With
        End Sub

        ' Setzt die Eigenschaften des Trenn-Panels.
        Private Sub SetPropertys_Panel_Spacer()
            With Me.PanelSpacer
                .BackColor = TitleBarColor
                .Location = New System.Drawing.Point(119, 36)
                .Name = "PanelSpacer"
                .Size = New System.Drawing.Size(1, 92)
                .TabIndex = 2
            End With
        End Sub

        ' Setzt die Eigenschaften des Titel-Panels.
        Private Sub SetPropertys_Panel_Title()
            With Me.PanelTitle
                .BackColor = TitleBarColor
                .Controls.Add(Me.LabelClose)
                .Controls.Add(Me.LabelTitle)
                .Location = New System.Drawing.Point(0, -1)
                .Name = "PanelTitle"
                .Size = New System.Drawing.Size(505, 29)
                .TabIndex = 0
            End With
        End Sub

        ' Setzt die Eigenschaften der PictureBox für das Symbol.
        Private Sub SetPropertys_Pb_Image()
            With Me.PictureBoxImage
                .BackColor = System.Drawing.Color.Transparent
                .Location = New System.Drawing.Point(12, 36)
                .Name = "PicBoxImage"
                .Size = New System.Drawing.Size(92, 92)
                .TabIndex = 1
                .TabStop = False
                .Image = Image
                .SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
                .Refresh()
            End With
        End Sub

        ' Setzt die Eigenschaften der RichTextBox für die Nachricht.
        Private Sub SetPropertys_Txt_Msg()
            With Me.RichTextBoxMessage
                .BackColor = TextFieldColor
                .ForeColor = FontColor
                .BorderStyle = System.Windows.Forms.BorderStyle.None
                .Location = New System.Drawing.Point(133, 37)
                .Multiline = True
                .Name = "RiTxtMsg"
                .Size = New System.Drawing.Size(361, 90)
                .TabIndex = 3
                .ReadOnly = True
                .Text = Message
                .SelectionProtected = True
                .Cursor = System.Windows.Forms.Cursors.Arrow
            End With
        End Sub

        ' Initialisiert Designer-generierte Komponenten (Platzhalter).
        Private Sub InitializeComponent()
            Me.SuspendLayout()
            '
            'FormTemplate
            '
            Me.ClientSize = New System.Drawing.Size(284, 261)
            Me.Name = "FormTemplate"
            Me.ResumeLayout(False)
        End Sub

#End Region

    End Class

End Namespace
