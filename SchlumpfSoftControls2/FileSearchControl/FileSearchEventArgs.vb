' *************************************************************************************************
' FileSearchEventArgs.vb
' Copyright (c) 2025 by Andreas Sauer
' *************************************************************************************************

Namespace FileSearchControl

    ''' <summary>
    ''' Stellt Fortschrittsinformationen für einen Datei-Suchvorgang bereit.<br/>
    ''' Instanzen werden beim <c>ProgressChanged</c>-Ereignis der Dateisuche an Handler übergeben.
    ''' </summary>
    ''' <remarks>
    ''' Die Werte werden bei jeder gefundenen Datei aktualisiert.<br/>
    ''' Es wird keine Thread-Sicherheit garantiert; Konsumenten sollten nur lesend
    ''' zugreifen.
    ''' </remarks>
    ''' <example>
    ''' Anzeige des Fortschritts im UI während der Suche. <code><![CDATA[' Handler für ProgressChanged:
    ''' Private Sub FileSearch_ProgressChanged(sender As Object, e As FileSearchControl.FileSearchEventArgs)
    '''     ProgressBar1.Value = Math.Max(0, Math.Min(100, e.Percent))
    '''     LabelStatus.Text = $"Gefunden: {e.Found} / {e.Total} Dateien"
    ''' End Sub]]></code>
    ''' </example>
    Public Class FileSearchEventArgs

        ''' <summary>
        ''' Prozentualer Fortschritt (0–100).
        ''' </summary>
        ''' <remarks>
        ''' Ist 0, wenn keine Dateien gezählt wurden oder die Berechnung noch nicht erfolgt
        ''' ist, oder wenn <see cref="Total"/> gleich 0 ist.<br/>
        ''' Werte außerhalb des Bereichs 0–100 sollten vermieden werden.
        ''' </remarks>
        ''' <value>
        ''' Wird aus <see cref="Found"/> und <see cref="Total"/> berechnet.
        ''' </value>
        ''' <example>
        ''' Fortschritt clamped auf UI-Range. <code><![CDATA[Dim percent As Integer = args.Percent
        ''' ProgressBar1.Value = Math.Max(0, Math.Min(100, percent))]]></code>
        ''' </example>
        Public Property Percent As Integer

        ''' <summary>
        ''' Anzahl der bislang gefundenen (und gemeldeten) Dateien.
        ''' </summary>
        ''' <remarks>
        ''' Der Wert kann kleiner oder gleich <see cref="Total"/> sein.<br/>
        ''' Bei unbekannter Gesamtmenge kann <see cref="Total"/> 0 bleiben, während <see
        ''' cref="Found"/> steigt.
        ''' </remarks>
        ''' <value>
        ''' Nicht-negativer Zähler, der bei jedem Treffer inkrementiert wird.
        ''' </value>
        ''' <example>
        ''' Textausgabe mit bisher gefundenen Dateien. <code><![CDATA[LabelFound.Text = $"Bisher gefunden: {args.Found}"]]></code>
        ''' </example>
        Public Property Found As Integer

        ''' <summary>
        ''' Gesamtanzahl der zu erwartenden Dateien gemäß aktueller Zählung.
        ''' </summary>
        ''' <remarks>
        ''' Dieser Wert kann sich im Verlauf der Suche ändern, wenn neue
        ''' Verzeichnisse/Filter berücksichtigt werden.<br/>
        ''' Bei <see cref="Total"/> = 0 ist eine Prozentberechnung nicht sinnvoll.
        ''' </remarks>
        ''' <value>
        ''' Kann 0 sein, wenn keine Treffer vorliegen oder die Gesamtmenge nicht ermittelt
        ''' werden konnte.
        ''' </value>
        ''' <example>
        ''' Vermeidung einer Division durch Null bei der Berechnung eigener Prozentwerte. <code><![CDATA[Dim percent As Integer
        ''' If args.Total > 0 Then
        '''     percent = CInt((args.Found / args.Total) * 100)
        ''' Else
        '''     percent = 0
        ''' End If]]></code>
        ''' </example>
        Public Property Total As Integer

    End Class

End Namespace
