' *************************************************************************************************
' SimpleProgress.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace FileSearchControl

    ' Stellt einen einfachen, generischen Fortschritts-/Rückruf-Helfer bereit, der – falls vorhanden – den zum Erstellungszeitpunkt aktuellen 
    ' "System.Threading.SynchronizationContext" erfasst
    ' und Rückrufe (`Report`) automatisch auf diesen Kontext marshalt (z. B. UI-Thread in WinForms/WPF).
    ' Falls kein Synchronisierungskontext vorhanden ist (Nothing), wird der Rückruf direkt synchron ausgeführt.
    ' Diese Klasse ist eine schlanke Alternative zu "IProgress(Of T)" bzw. "Progress(Of T)". 
    ' Sie führt keine zusätzliche Thread-Sicherheitslogik ein; Mehrfachaufrufe von "Report" können parallel erfolgen.
    Friend Class SimpleProgress(Of T)

        ' Der beim Erzeugen übergebene Rückruf, der den Fortschrittswert konsumiert.
        Private ReadOnly _callback As System.Action(Of T)

        ' Gespeicherter Synchronisierungskontext, um Aufrufe zurück auf den ursprünglichen (UI-)Thread zu marshallen.
        ' Kann Nothing sein, wenn beim Erzeugen keiner gesetzt war.
        Private ReadOnly _context As System.Threading.SynchronizationContext

        ' Erstellt eine neue Instanz von "SimpleProgress(Of T)" und erfasst den aktuellen "System.Threading.SynchronizationContext".
        Public Sub New(callback As System.Action(Of T))
            If callback Is Nothing Then Throw New System.ArgumentNullException(NameOf(callback))
            _callback = callback
            _context = System.Threading.SynchronizationContext.Current
        End Sub

        ' Meldet einen neuen Wert. 
        ' Falls ein Synchronisierungskontext erfasst wurde, wird der Rückruf
        ' asynchron mittels System.Threading.SynchronizationContext.Post(System.Threading.SendOrPostCallback,Object)" dorthin gesendet.
        ' Andernfalls erfolgt der Aufruf unmittelbar (synchron) im aufrufenden Thread.
        ' Bei Verwendung in UI-Anwendungen sorgt dies dafür, dass UI-Elemente gefahrlos
        ' aktualisiert werden können, ohne selbst Invoke/BeginInvoke o. Ä. aufzurufen.
        ' 
        ' WARUM VSTHRD001 AUFTRITT:
        ' - Der Roslyn-Analyzer "Microsoft.VisualStudio.Threading.Analyzers" warnt vor direkter Verwendung von SynchronizationContext.Post/Send,
        '   weil VS-Extensions stattdessen JoinableTaskFactory.SwitchToMainThreadAsync nutzen sollen (deadlock-sicher, prioritätsbewusst).
        ' 
        ' WIE VERMEIDEN:
        ' - Für VS-Extensions: JoinableTaskFactory verwenden (#If VS_THREADING Then ...).
        ' - Für normale WinForms/WPF/Console-Bibliotheken: Weiterhin SynchronizationContext.Post nutzen und die Analyzer-Warnung lokal unterdrücken.
        ' - Zusätzlich: Falls bereits auf dem gewünschten Kontext, direkt synchron aufrufen (spart Post und vermeidet unnötiges Marshalling).
        Public Sub Report(value As T)
            Dim ctx = _context
            ' Wenn kein Kontext erfasst wurde oder wir bereits auf dem Zielkontext sind, direkt ausführen.
            If ctx Is Nothing OrElse ctx Is System.Threading.SynchronizationContext.Current Then
                _callback(value)
                Return
            End If

#If VS_THREADING Then
            ' Pfad für VS-Extensions: JTF verwenden, um sicher auf den UI-Thread zu wechseln.
            Dim jtf = Microsoft.VisualStudio.Shell.ThreadHelper.JoinableTaskFactory
            Dim _ = jtf.RunAsync(
                Async Function()
                    Await jtf.SwitchToMainThreadAsync()
                    _callback(value)
                End Function)
#Else
#Disable Warning IDE0079 ' Unnötige Unterdrückung entfernen
            ' Pfad für normale Apps/Bibliotheken: SynchronizationContext.Post ist korrekt und nicht blockierend.
#Disable Warning VSTHRD001 ' In Nicht-VS-Extension-Szenarien ist Post die geeignete, deadlock-freie Methode.
            ctx.Post(AddressOf InvokeCallback, value)
#Enable Warning VSTHRD001
#Enable Warning IDE0079 ' Unnötige Unterdrückung entfernen
#End If
        End Sub

        ' Interner Adapter, der den generischen Wert aus dem Post-Aufruf entpackt und den eigentlichen Callback aufruft.
        Private Sub InvokeCallback(state As Object)
            _callback(DirectCast(state, T))
        End Sub

    End Class

End Namespace
