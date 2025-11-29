' *************************************************************************************************
' SimpleProgress.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace FileSearchControl

    ''' <summary>
    ''' <para>Stellt einen einfachen, generischen Fortschritts-/Rückruf-Helfer bereit,
    ''' der – falls vorhanden – den zum Erstellungszeitpunkt aktuellen <see
    ''' cref="System.Threading.SynchronizationContext"/> erfasst </para>
    ''' <para>und Rückrufe (`Report`) automatisch auf diesen Kontext marshalt (z. B.
    ''' UI-Thread in WinForms/WPF). </para>
    ''' <para>Falls kein Synchronisierungskontext vorhanden ist (Nothing), wird der
    ''' Rückruf direkt synchron ausgeführt.</para>
    ''' </summary>
    ''' <remarks>
    ''' <para>Diese Klasse ist eine schlanke Alternative zu "IProgress(Of T)" bzw. "Progress(Of T)". </para>
    ''' <para>Sie führt keine zusätzliche Thread-Sicherheitslogik ein; Mehrfachaufrufe
    ''' von <see cref="Report"/> können parallel erfolgen.</para>
    ''' </remarks>
    ''' <typeparam name="T">Der Datentyp des zu meldenden
    ''' Fortschritts-/Ergebniswertes.</typeparam>
    Friend Class SimpleProgress(Of T)

        ''' <summary>
        ''' Der beim Erzeugen übergebene Rückruf, der den Fortschrittswert konsumiert.
        ''' </summary>
        Private ReadOnly _callback As System.Action(Of T)

        ''' <summary>
        ''' Gespeicherter Synchronisierungskontext, um Aufrufe zurück auf den ursprünglichen (UI-)Thread zu marshallen.
        ''' Kann <c>Nothing</c> sein, wenn beim Erzeugen keiner gesetzt war.
        ''' </summary>
        Private ReadOnly _context As System.Threading.SynchronizationContext

        ''' <summary>
        ''' Erstellt eine neue Instanz von <see cref="SimpleProgress(Of T)"/> und erfasst
        ''' den aktuellen <see cref="System.Threading.SynchronizationContext"/>.
        ''' </summary>
        ''' <param name="callback">Delegat, der bei jedem Aufruf von <see cref="Report"/>
        ''' mit dem gemeldeten Wert ausgeführt wird.</param>
        ''' <exception cref="system.ArgumentNullException">Wird ausgelöst, wenn <paramref name="callback"/> <c>Nothing</c> ist.</exception>
        Public Sub New(callback As System.Action(Of T))
            If callback Is Nothing Then Throw New System.ArgumentNullException(NameOf(callback))
            _callback = callback
            _context = System.Threading.SynchronizationContext.Current
        End Sub

        ''' <summary>
        ''' <para>Meldet einen neuen Wert. </para>
        ''' <para>Falls ein Synchronisierungskontext erfasst wurde, wird der Rückruf
        ''' asynchron mittels <see cref="System.Threading.SynchronizationContext.Post(System.Threading.SendOrPostCallback,
        ''' Object)"/> dorthin gesendet. </para>
        ''' <para>Andernfalls erfolgt der Aufruf unmittelbar (synchron) im aufrufenden
        ''' Thread.</para>
        ''' </summary>
        ''' <remarks>
        ''' Bei Verwendung in UI-Anwendungen sorgt dies dafür, dass UI-Elemente gefahrlos
        ''' aktualisiert werden können, ohne selbst Invoke/BeginInvoke o. Ä. aufzurufen.
        ''' 
        ''' WARUM VSTHRD001 AUFTRITT:
        ''' - Der Roslyn-Analyzer "Microsoft.VisualStudio.Threading.Analyzers" warnt vor direkter Verwendung von SynchronizationContext.Post/Send,
        '''   weil VS-Extensions stattdessen JoinableTaskFactory.SwitchToMainThreadAsync nutzen sollen (deadlock-sicher, prioritätsbewusst).
        ''' 
        ''' WIE VERMEIDEN:
        ''' - Für VS-Extensions: JoinableTaskFactory verwenden (#If VS_THREADING Then ...).
        ''' - Für normale WinForms/WPF/Console-Bibliotheken: Weiterhin SynchronizationContext.Post nutzen und die Analyzer-Warnung lokal unterdrücken.
        ''' - Zusätzlich: Falls bereits auf dem gewünschten Kontext, direkt synchron aufrufen (spart Post und vermeidet unnötiges Marshalling).
        ''' </remarks>
        ''' <param name="value">Der zu meldende Wert.</param>
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

        ''' <summary>
        ''' Interner Adapter, der den generischen Wert aus dem Post-Aufruf entpackt und den eigentlichen Callback aufruft.
        ''' </summary>
        ''' <param name="state">Der übergebene Zustand (muss vom Typ <typeparamref name="T"/> sein).</param>
        Private Sub InvokeCallback(state As Object)
            _callback(DirectCast(state, T))
        End Sub

    End Class

End Namespace
