' *************************************************************************************************
' NotifyForm.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace NotifyFormControl

    ''' <summary>
    ''' Control zum anzeigen von Benachrichtigungsfenstern.
    ''' </summary>
    ''' <example>
    ''' <code><![CDATA[Dim nf As New NotifyFormControl.NotifyForm()
    ''' nf.Title = "Hinweis"
    ''' nf.Message = "Speichern erfolgreich."
    ''' nf.Style = NotifyFormControl.NotifyForm.NotifyFormStyle.Information
    ''' nf.Design = NotifyFormControl.NotifyForm.NotifyFormDesign.Bright
    ''' nf.ShowTime = 3000
    ''' nf.Show()]]></code>
    ''' </example>
    <ProvideToolboxControl("SchlumpfSoft Controls", False)>
    <System.ComponentModel.Description("Control zum anzeigen von Benachrichtigungsfenstern.")>
    <System.ComponentModel.ToolboxItem(True)>
    <System.Drawing.ToolboxBitmap(GetType(NotifyFormControl.NotifyForm), "NotifyForm.bmp")>
    Public Class NotifyForm : Inherits System.ComponentModel.Component

#Region "Aufzählungen"

        ''' <summary>
        ''' Auflistung der Styles
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[Dim nf As New NotifyFormControl.NotifyForm()
        ''' nf.Style = NotifyFormControl.NotifyForm.NotifyFormStyle.Exclamation
        ''' nf.Message = "Bitte Eingaben prüfen."
        ''' nf.Show()]]></code>
        ''' </example>
        Public Enum NotifyFormStyle As Integer

            Information = 0 ' Infosymbol
            Question = 1 ' Fragesymbol
            CriticalError = 2 ' Fehlersymbol
            Exclamation = 3 ' Warnungssymbol

        End Enum

        ''' <summary>
        ''' Auflistung der Designs
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[Dim nf As New NotifyFormControl.NotifyForm()
        ''' nf.Design = NotifyFormControl.NotifyForm.NotifyFormDesign.Dark
        ''' nf.Style = NotifyFormControl.NotifyForm.NotifyFormStyle.Information
        ''' nf.Title = "Status"
        ''' nf.Message = "Nachtmodus aktiv."
        ''' nf.Show()]]></code>
        ''' </example>
        Public Enum NotifyFormDesign As Integer

            Bright = 0  ' Helles Design
            Colorful = 1 ' Farbiges Design
            Dark = 2 ' Dunkles Design

        End Enum

#End Region

#Region "Eigenschaften"

        ''' <summary>
        ''' Legt das Aussehen des Benachrichtigungsfensters fest.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[Dim nf As New NotifyFormControl.NotifyForm()
        ''' nf.Design = NotifyFormControl.NotifyForm.NotifyFormDesign.Colorful
        ''' nf.Show()]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt das Aussehen des Benachrichtigungsfensters fest.")>
        Public Property Design As NotifyFormDesign

        ''' <summary>
        ''' Legt den Benachrichtigungstext fest der angezeigt werden soll.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[Dim nf As New NotifyFormControl.NotifyForm()
        ''' nf.Message = "Update abgeschlossen"
        ''' nf.Show()]]></code>
        ''' </example>
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
        ''' <example>
        ''' <code><![CDATA[Dim nf As New NotifyFormControl.NotifyForm()
        ''' nf.ShowTime = 0 ' Manuelles Schließen erforderlich
        ''' nf.Show()]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Behavior")>
        <System.ComponentModel.Description("Legt die Anzeigedauer des Benachrichtigungsfensters in ms fest.")>
        Public Property ShowTime As Integer

        ''' <summary>
        ''' Legt das anzuzeigende Symbol im Benachrichtigungsfensters fest.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[Dim nf As New NotifyFormControl.NotifyForm()
        ''' nf.Style = NotifyFormControl.NotifyForm.NotifyFormStyle.CriticalError
        ''' nf.Message = "Unerwarteter Fehler aufgetreten"
        ''' nf.Show()]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt das anzuzeigende Symbol im Benachrichtigungsfensters fest.")>
        Public Property Style As NotifyFormStyle

        ''' <summary>
        ''' Legt den Text der Titelzeile des Benachrichtigungsfensters fest.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[Dim nf As New NotifyFormControl.NotifyForm()
        ''' nf.Title = "Ergebnis"
        ''' nf.Message = "Vorgang erfolgreich"
        ''' nf.Show()]]></code>
        ''' </example>
        <System.ComponentModel.Browsable(True)>
        <System.ComponentModel.Category("Appearance")>
        <System.ComponentModel.Description("Legt den Text der Titelzeile des Benachrichtigungsfensters fest.")>
        Public Property Title As String

#End Region

#Region "öffentliche Methoden"

        ''' <summary>
        ''' Initialisiert eine neue Instanz der <see cref="NotifyForm"/> Klasse mit
        ''' Standardwerten.
        ''' </summary>
        ''' <example>
        ''' <code><![CDATA[Dim nf As New NotifyFormControl.NotifyForm()
        ''' ' Standardwerte: Title="Titel", Message="Mitteilung", Design=Bright, Style=Information, ShowTime=5000
        ''' nf.Show()]]></code>
        ''' </example>
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
        ''' <example>
        ''' <code><![CDATA[Dim nf As New NotifyFormControl.NotifyForm()
        ''' nf.Title = "Frage"
        ''' nf.Message = "Möchten Sie fortfahren?"
        ''' nf.Style = NotifyFormControl.NotifyForm.NotifyFormStyle.Question
        ''' nf.ShowTime = 7000
        ''' nf.Show()]]></code>
        ''' </example>
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

        ' Erstellt und initialisiert das zugrunde liegende Formular.
        Private Sub ShowForm()
            Dim frm As New FormTemplate
            frm.Initialize()
        End Sub

        ' Wählt anhand der aktuellen Einstellung das Design aus und setzt es.
        Private Sub SetFormDesign()
            Select Case Me.Design
                Case NotifyFormDesign.Bright : SetFormDesignBright()
                Case NotifyFormDesign.Colorful : SetFormDesignColorful()
                Case NotifyFormDesign.Dark : SetFormDesignDark()
            End Select
        End Sub

        ' Setzt das helle Design.
        Private Shared Sub SetFormDesignBright()
            FormTemplate.BackgroundColor = System.Drawing.Color.White
            FormTemplate.TextFieldColor = System.Drawing.Color.White
            FormTemplate.TitleBarColor = System.Drawing.Color.Gray
            FormTemplate.FontColor = System.Drawing.Color.Black
        End Sub

        ' Setzt das farbige Design.
        Private Shared Sub SetFormDesignColorful()
            FormTemplate.BackgroundColor = System.Drawing.Color.LightBlue
            FormTemplate.TextFieldColor = System.Drawing.Color.LightBlue
            FormTemplate.TitleBarColor = System.Drawing.Color.LightSeaGreen
            FormTemplate.FontColor = System.Drawing.Color.White
        End Sub

        ' Setzt das dunkle Design.
        Private Shared Sub SetFormDesignDark()
            FormTemplate.BackgroundColor = System.Drawing.Color.FromArgb(83, 79, 75)
            FormTemplate.TextFieldColor = System.Drawing.Color.FromArgb(83, 79, 75)
            FormTemplate.TitleBarColor = System.Drawing.Color.FromArgb(60, 57, 54)
            FormTemplate.FontColor = System.Drawing.Color.White
        End Sub

        ' Ermittelt das anzuzeigende Symbol entsprechend dem eingestellten Stil.
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

        ' Initialisiert die durch den Designer generierten Komponenten (Platzhalter).
        Private Sub InitializeComponent()
        End Sub

#End Region

    End Class

End Namespace