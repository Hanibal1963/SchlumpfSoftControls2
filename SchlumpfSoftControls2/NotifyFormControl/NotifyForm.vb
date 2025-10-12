' *************************************************************************************************
' NotifyForm.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms
'Imports SchlumpfSoft.Controls.Attribute

Namespace NotifyFormControl

    ''' <summary>
    ''' Control zum anzeigen von Benachrichtigungsfenstern.
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <Description("Control zum anzeigen von Benachrichtigungsfenstern.")>
    <ToolboxItem(True)>
    <ToolboxBitmap(GetType(NotifyFormControl.NotifyForm), "NotifyForm.bmp")>
    Public Class NotifyForm

        Inherits Component

#Region "öffentliche Eigenschaften"

        ''' <summary>
        ''' Legt das Aussehen des Benachrichtigungsfensters fest.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Legt das Aussehen des Benachrichtigungsfensters fest.")>
        Public Property Design As NotifyFormDesign

        ''' <summary>
        ''' Legt den Benachrichtigungstext fest der angezeigt werden soll.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Legt den Benachrichtigungstext fest der angezeigt werden soll.")>
        Public Property Message As String

        ''' <summary>
        ''' Legt die Anzeigedauer des Benachrichtigungsfensters in ms fest.
        ''' </summary>
        ''' <remarks>
        ''' Der Wert 0 bewirkt das kein automatisches schließen des Fensters erfolgt.
        ''' </remarks>
        <Browsable(True)>
        <Category("Behavior")>
        <Description("Legt die Anzeigedauer des Benachrichtigungsfensters in ms fest.")>
        Public Property ShowTime As Integer

        ''' <summary>
        ''' Legt das anzuzeigende Symbol im Benachrichtigungsfensters fest.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Legt das anzuzeigende Symbol im Benachrichtigungsfensters fest.")>
        Public Property Style As NotifyFormStyle

        ''' <summary>
        ''' Legt den Text der Titelzeile des Benachrichtigungsfensters fest.
        ''' </summary>
        <Browsable(True)>
        <Category("Appearance")>
        <Description("Legt den Text der Titelzeile des Benachrichtigungsfensters fest.")>
        Public Property Title As String

#End Region

        Public Sub New()
            Title = $"Titel"
            Message = $"Mitteilung"
            Design = NotifyFormDesign.Bright
            Style = NotifyFormStyle.Information
            ShowTime = 5000
        End Sub

        ''' <summary>
        ''' Zeigt das Meldungsfenster an.
        ''' </summary>
        <Description("Zeigt das Meldungsfenster an.")>
        Public Sub Show()
            FormTemplate.Image = SetFormImage()
            FormTemplate.Title = Title
            FormTemplate.Message = Message
            FormTemplate.ShowTime = ShowTime
            SetFormDesign()
        End Sub

#Region "Interne Hilfsfunktionen"

        ' Setzt das Design des Fensters
        Private Sub SetFormDesign()
            Select Case Design
                Case NotifyFormDesign.Bright
                    SetFormDesignBright()
                Case NotifyFormDesign.Colorful
                    SetFormDesignColorful()
                Case NotifyFormDesign.Dark
                    SetFormDesignDark()
            End Select
        End Sub

        ' Setzt das helle Design
        Private Shared Sub SetFormDesignBright()
            FormTemplate.BackgroundColor = Color.White
            FormTemplate.TextFieldColor = Color.White
            FormTemplate.TitleBarColor = Color.Gray
            FormTemplate.FontColor = Color.Black
            Dim ini As New FormTemplate
            ini.Initialize()
        End Sub

        ' Setz das farbige Design
        Private Shared Sub SetFormDesignColorful()
            FormTemplate.BackgroundColor = Color.LightBlue
            FormTemplate.TextFieldColor = Color.LightBlue
            FormTemplate.TitleBarColor = Color.LightSeaGreen
            FormTemplate.FontColor = Color.White
            Dim ini As New FormTemplate
            ini.Initialize()
        End Sub

        ' Setzt das dunkle Design
        Private Shared Sub SetFormDesignDark()
            FormTemplate.BackgroundColor = Color.FromArgb(83, 79, 75)
            FormTemplate.TextFieldColor = Color.FromArgb(83, 79, 75)
            FormTemplate.TitleBarColor = Color.FromArgb(60, 57, 54)
            FormTemplate.FontColor = Color.White
            Dim ini As New FormTemplate
            ini.Initialize()
        End Sub

        ' Setzt das Symbol des Fensters
        Private Function SetFormImage() As Image
            Dim result As Bitmap = Nothing
            Select Case Style
                Case NotifyFormStyle.CriticalError
                    result = My.Resources.CriticalError
                Case NotifyFormStyle.Exclamation
                    result = My.Resources.Warning
                Case NotifyFormStyle.Information
                    result = My.Resources.Information
                Case NotifyFormStyle.Question
                    result = My.Resources.Question
            End Select
            Return result
        End Function

        Private Sub InitializeComponent()
        End Sub

#End Region

        ' Vorlage für das Benachrichtigungsfenster
        Private Class FormTemplate

            Inherits Form

            Private CloseThread As Thread

            'Eigenschaften
            Public Shared BackgroundColor As Color
            Public Shared FontColor As Color
            Public Shared Image As Image
            Public Shared Message As String
            Public Shared ShowTime As Integer
            Public Shared TextFieldColor As Color
            Public Shared Title As String
            Public Shared TitleBarColor As Color

            'Controls
            Private ReadOnly LabelClose As New Label
            Private ReadOnly LabelTitle As New Label
            Private ReadOnly PanelSpacer As New Panel
            Private ReadOnly PanelTitle As New Panel
            Private ReadOnly PictureBoxImage As New PictureBox
            Private ReadOnly RichTextBoxMessage As New RichTextBox

            Private Sub CloseForm()
                If InvokeRequired Then
                    Dim unused = Invoke(New MethodInvoker(AddressOf CloseForm))
                Else
                    Close()
                End If
            End Sub

            Public Sub Initialize()
                With Me
                    .MaximizeBox = False
                    .MinimizeBox = False
                    .BackColor = BackgroundColor
                    .ForeColor = FontColor
                    .Size = New Size(504, 138)
                    .FormBorderStyle = FormBorderStyle.None
                    .StartPosition = FormStartPosition.Manual
                End With
                Show()
            End Sub

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

            Private Sub AddControls()
                With Controls
                    .Add(LabelTitle)
                    .Add(LabelClose)
                    .Add(PanelSpacer)
                    .Add(PanelTitle)
                    .Add(RichTextBoxMessage)
                    .Add(PictureBoxImage)
                End With
            End Sub

            Private Sub AutoClose()
                'Fenster nur automatisch schließen wenn Zeit > 0 ist.
                If ShowTime > 0 Then
                    Thread.Sleep(ShowTime)
                    CloseForm()
                End If
            End Sub

            Private Sub Form_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
                Thread.Sleep(150)
                For iCount As Integer = 90 To 10 Step -15
                    Opacity = iCount / 110
                    Refresh()
                    Thread.Sleep(60)
                Next
            End Sub

            Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
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
                Dim x As Integer = Screen.PrimaryScreen.WorkingArea.Width
                Dim y As Integer = Screen.PrimaryScreen.WorkingArea.Height - Height - 50
                Do Until x = Screen.PrimaryScreen.WorkingArea.Width - Width
                    x -= 1
                    Location = New Point(x, y)
                Loop
                AddHandler LabelClose.Click, AddressOf Lbl_Close_Click
                FormFadeIn()
                'Starte Thread für Autoclose-Popup.
                CloseThread = New Thread(AddressOf AutoClose) With {.IsBackground = True}
                CloseThread.Start()
            End Sub

            Private Sub FormFadeIn()
                For iCount As Integer = 10 To 100 Step +15
                    Opacity = iCount / 100
                    Refresh()
                    Thread.Sleep(60)
                Next
            End Sub

            Private Sub Lbl_Close_Click(sender As Object, e As EventArgs)
                Close()
            End Sub

            Private Sub SetPropertys_Lbl_Close()
                With LabelClose
                    .AutoSize = True
                    .BackColor = TitleBarColor
                    .ForeColor = FontColor
                    .Location = New Point(478, 9)
                    .Name = "LblClose"
                    .Size = New Size(14, 13)
                    .TabIndex = 1
                    .Text = "X"
                End With
            End Sub

            Private Sub SetPropertys_Lbl_Title()
                With LabelTitle
                    .AutoSize = True
                    .BackColor = TitleBarColor
                    .ForeColor = FontColor
                    .Location = New Point(11, 9)
                    .Name = "LblTitle"
                    .Size = New Size(27, 13)
                    .TabIndex = 0
                    .Text = Title
                End With
            End Sub

            Private Sub SetPropertys_Panel_Spacer()
                With PanelSpacer
                    .BackColor = TitleBarColor
                    .Location = New Point(119, 36)
                    .Name = "PanelSpacer"
                    .Size = New Size(1, 92)
                    .TabIndex = 2
                End With
            End Sub

            Private Sub SetPropertys_Panel_Title()
                With PanelTitle
                    .BackColor = TitleBarColor
                    .Controls.Add(LabelClose)
                    .Controls.Add(LabelTitle)
                    .Location = New Point(0, -1)
                    .Name = "PanelTitle"
                    .Size = New Size(505, 29)
                    .TabIndex = 0
                End With
            End Sub

            Private Sub SetPropertys_Pb_Image()
                With PictureBoxImage
                    .BackColor = Color.Transparent
                    .Location = New Point(12, 36)
                    .Name = "PicBoxImage"
                    .Size = New Size(92, 92)
                    .TabIndex = 1
                    .TabStop = False
                    .Image = Image
                    .SizeMode = PictureBoxSizeMode.StretchImage
                    .Refresh()
                End With
            End Sub

            Private Sub SetPropertys_Txt_Msg()
                With RichTextBoxMessage
                    .BackColor = TextFieldColor
                    .ForeColor = FontColor
                    .BorderStyle = BorderStyle.None
                    .Location = New Point(133, 37)
                    .Multiline = True
                    .Name = "RiTxtMsg"
                    .Size = New Size(361, 90)
                    .TabIndex = 3
                    .ReadOnly = True
                    .Text = Message
                    .SelectionProtected = True
                    .Cursor = Cursors.Arrow
                End With
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub

            Private Sub InitializeComponent()
                SuspendLayout()
                '
                'FormTemplate
                '
                ClientSize = New System.Drawing.Size(284, 261)
                Name = "FormTemplate"
                ResumeLayout(False)
            End Sub

        End Class

    End Class

End Namespace