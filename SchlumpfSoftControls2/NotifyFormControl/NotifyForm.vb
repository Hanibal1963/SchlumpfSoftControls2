' *************************************************************************************************
' NotifyForm.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace NotifyFormControl

    ''' <summary>
    ''' Control zum anzeigen von Benachrichtigungsfenstern.
    ''' </summary>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Control zum anzeigen von Benachrichtigungsfenstern.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(NotifyFormControl.NotifyForm), "NotifyForm.bmp")>
    Public Class NotifyForm : Inherits System.ComponentModel.Component

#Region "Aufzählungen"

        ''' <summary>
        ''' Auflistung der Styles
        ''' </summary>
        Public Enum NotifyFormStyle As Integer

            ''' <summary>
            ''' Infosymbol
            ''' </summary>
            Information = 0

            ''' <summary>
            ''' Fragesymbol
            ''' </summary>
            Question = 1

            ''' <summary>
            ''' Fehlersymbol
            ''' </summary>
            CriticalError = 2

            ''' <summary>
            ''' Warnungssymbol
            ''' </summary>
            Exclamation = 3

        End Enum

        ''' <summary>
        ''' Auflistung der Designs
        ''' </summary>
        Public Enum NotifyFormDesign As Integer

            ''' <summary>
            ''' Helles Design
            ''' </summary>
            Bright = 0

            ''' <summary>
            ''' Farbiges Design
            ''' </summary>
            Colorful = 1

            ''' <summary>
            ''' Dunkles Design
            ''' </summary>
            Dark = 2

        End Enum

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Legt das Aussehen des Benachrichtigungsfensters fest.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt das Aussehen des Benachrichtigungsfensters fest.")>
        Public Property Design As NotifyFormDesign

        ''' <summary>
        ''' Legt den Benachrichtigungstext fest der angezeigt werden soll.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt den Benachrichtigungstext fest der angezeigt werden soll.")>
        Public Property Message As String

        ''' <summary>
        ''' Legt die Anzeigedauer des Benachrichtigungsfensters in ms fest.
        ''' </summary>
        ''' <remarks>
        ''' Der Wert 0 bewirkt das kein automatisches schließen des Fensters erfolgt.
        ''' </remarks>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Legt die Anzeigedauer des Benachrichtigungsfensters in ms fest.")>
        Public Property ShowTime As Integer

        ''' <summary>
        ''' Legt das anzuzeigende Symbol im Benachrichtigungsfensters fest.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt das anzuzeigende Symbol im Benachrichtigungsfensters fest.")>
        Public Property Style As NotifyFormStyle

        ''' <summary>
        ''' Legt den Text der Titelzeile des Benachrichtigungsfensters fest.
        ''' </summary>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt den Text der Titelzeile des Benachrichtigungsfensters fest.")>
        Public Property Title As String

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz der <see cref="NotifyForm"/> Klasse mit Standardwerten.
        ''' </summary>
        Public Sub New()
            Me.Title = $"Titel"
            Me.Message = $"Mitteilung"
            Me.Design = NotifyFormDesign.Bright
            Me.Style = NotifyFormStyle.Information
            Me.ShowTime = 5000
        End Sub

        ''' <summary>
        ''' Zeigt das Meldungsfenster an.
        ''' </summary>
        <System.ComponentModel.Description("Zeigt das Meldungsfenster an.")>
        Public Sub Show()
            FormTemplate.Image = Me.SetFormImage()
            FormTemplate.Title = Me.Title
            FormTemplate.Message = Me.Message
            FormTemplate.ShowTime = Me.ShowTime
            Me.SetFormDesign()
            Me.ShowForm()
        End Sub

#End Region

#Region "Interne Methoden"

        ''' <summary>
        ''' Erstellt und initialisiert das zugrunde liegende Formular.
        ''' </summary>
        Private Sub ShowForm()
            Dim frm As New FormTemplate
            frm.Initialize()
        End Sub

        ''' <summary>
        ''' Wählt anhand der aktuellen Einstellung das Design aus und setzt es.
        ''' </summary>
        Private Sub SetFormDesign()
            Select Case Me.Design
                Case NotifyFormDesign.Bright : SetFormDesignBright()
                Case NotifyFormDesign.Colorful : SetFormDesignColorful()
                Case NotifyFormDesign.Dark : SetFormDesignDark()
            End Select
        End Sub

        ''' <summary>
        ''' Setzt das helle Design.
        ''' </summary>
        Private Shared Sub SetFormDesignBright()
            FormTemplate.BackgroundColor = System.Drawing.Color.White
            FormTemplate.TextFieldColor = System.Drawing.Color.White
            FormTemplate.TitleBarColor = System.Drawing.Color.Gray
            FormTemplate.FontColor = System.Drawing.Color.Black
        End Sub

        ''' <summary>
        ''' Setzt das farbige Design.
        ''' </summary>
        Private Shared Sub SetFormDesignColorful()
            FormTemplate.BackgroundColor = System.Drawing.Color.LightBlue
            FormTemplate.TextFieldColor = System.Drawing.Color.LightBlue
            FormTemplate.TitleBarColor = System.Drawing.Color.LightSeaGreen
            FormTemplate.FontColor = System.Drawing.Color.White
        End Sub

        ''' <summary>
        ''' Setzt das dunkle Design.
        ''' </summary>
        Private Shared Sub SetFormDesignDark()
            FormTemplate.BackgroundColor = System.Drawing.Color.FromArgb(83, 79, 75)
            FormTemplate.TextFieldColor = System.Drawing.Color.FromArgb(83, 79, 75)
            FormTemplate.TitleBarColor = System.Drawing.Color.FromArgb(60, 57, 54)
            FormTemplate.FontColor = System.Drawing.Color.White
        End Sub

        ''' <summary>
        ''' Ermittelt das anzuzeigende Symbol entsprechend dem eingestellten Stil.
        ''' </summary>
        ''' <returns>Das anzuzeigende <see cref="System.Drawing.Image"/> Objekt oder Nothing.</returns>
        Private Function SetFormImage() As System.Drawing.Image
            Dim result As System.Drawing.Bitmap = Nothing
            Select Case Me.Style
                Case NotifyFormStyle.CriticalError : result = My.Resources.CriticalError
                Case NotifyFormStyle.Exclamation : result = My.Resources.Warning
                Case NotifyFormStyle.Information : result = My.Resources.Information
                Case NotifyFormStyle.Question : result = My.Resources.Question
            End Select
            Return result
        End Function

        ''' <summary>
        ''' Initialisiert die durch den Designer generierten Komponenten (Platzhalter).
        ''' </summary>
        Private Sub InitializeComponent()
        End Sub

#End Region

    End Class

End Namespace