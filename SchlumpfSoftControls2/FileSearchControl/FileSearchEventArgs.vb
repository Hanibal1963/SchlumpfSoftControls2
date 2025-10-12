' *************************************************************************************************
' FileSearchEventArgs.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace FileSearchControl

    ''' <summary>
    ''' Stellt Fortschrittsinformationen für den Datei-Suchvorgang bereit und wird beim <c>ProgressChanged</c>-Ereignis der Dateisuche übergeben.
    ''' </summary>
    ''' <remarks>
    ''' Die Werte werden bei jeder gefundenen Datei aktualisiert. Es wird kein
    ''' Thread-Sicherheits- garantiert; Konsumenten sollten nur lesend zugreifen.
    ''' </remarks>
    Public Class FileSearchEventArgs

        ''' <summary>
        ''' <para>Prozentualer Fortschritt (0–100). </para>
        ''' <para>Wird aus <see cref="Found"/> und <see cref="Total"/> berechnet. </para>
        ''' <para>Ist 0, wenn keine Dateien gezählt wurden oder die Berechnung noch nicht
        ''' erfolgt ist.</para>
        ''' </summary>
        Public Property Percent As Integer

        ''' <summary>
        ''' Anzahl der bislang gefundenen (und gemeldeten) Dateien.
        ''' </summary>
        Public Property Found As Integer

        ''' <summary>
        ''' <para>Gesamtanzahl der zu erwartenden Dateien gemäß aktueller Zählung. </para>
        ''' <para>Kann 0 sein, wenn kein Treffer vorliegt oder die Gesamtmenge nicht
        ''' ermittelt werden konnte.</para>
        ''' </summary>
        Public Property Total As Integer

    End Class

End Namespace
