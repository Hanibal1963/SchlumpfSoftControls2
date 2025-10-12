' *************************************************************************************************
' CommentEditEventArgs.vb
' Copyright (c) 2025 by Andreas Sauer
'
' Beschreibung:
'   Stellt Argumentdaten für Ereignisse bereit, die beim Bearbeiten von Kommentarblöcken
'   in INI-Dateien ausgelöst werden. Enthält den Namen des Abschnitts sowie den neuen
'   Kommentartext als Zeilenarray.
'
' Hinweise:
'   - Diese Klasse ist ein einfacher Datencontainer (DTO) und erbt von EventArgs,
'     damit sie in .NET-Ereignissignaturen verwendet werden kann.
'   - Der Kommentar wird als String()-Array behandelt, um mehrzeilige Kommentare
'     (z. B. Kopfzeilenkommentare vor einem Abschnitt) exakt abzubilden.
'   - Der Name des Abschnitts kann je nach Anwendungsfall leer oder Nothing sein,
'     z. B. wenn es sich um einen globalen Kommentar außerhalb eines konkreten Abschnitts handelt.
'
' Beispiel (VB):
'   ' Ereignis auslösen:
'   Dim args = New CommentEditEventArgs("General", New String() {"Zeile 1", "Zeile 2"})
'   RaiseEvent CommentEdited(Me, args)
'
'   ' Ereignis abonnieren:
'   AddHandler editor.CommentEdited, Sub(sender, e)
'       ' e.Section enthält den Abschnittsnamen
'       ' e.Comment enthält die Kommentarzeilen
'   End Sub
' *************************************************************************************************

Imports System

Namespace IniFileControl

    ''' <summary>
    ''' Repräsentiert die Ereignisdaten für das Bearbeiten von Kommentartexten in einer INI-Datei.
    ''' </summary>
    ''' <remarks>
    ''' <para>
    ''' Der Kommentar wird als Array von Zeilen (<see cref="String"/>) gespeichert. Dies erlaubt
    ''' eine genaue Trennung und spätere Ausgabe, ohne Zeilenumbrüche neu zusammensetzen zu müssen.
    ''' </para>
    ''' <para>
    ''' Beachten Sie, dass das übergebene Array als Referenz gespeichert wird. Wenn Unveränderlichkeit
    ''' gewünscht ist, übergeben Sie eine Kopie (z. B. mittels <c>Comment.ToArray()</c>) oder erstellen
    ''' Sie innerhalb des Aufrufers eine neue Arrayinstanz.
    ''' </para>
    ''' </remarks>
    Public Class CommentEditEventArgs
        Inherits EventArgs

        ' Hält die Zeilen des Kommentarblocks. Referenz auf das übergebene Array.
        Private _comment() As String

        ' Hält den Namen des betroffenen Abschnitts (z. B. "General" für [General]).
        ' Kann Nothing/Leer sein, wenn sich der Kommentar nicht auf einen bestimmten Abschnitt bezieht.
        Private _section As String

        ''' <summary>
        ''' Speichert den neuen Kommentartext als Array von Zeilen.
        ''' </summary>
        ''' <value>
        ''' Ein Array von <see cref="String"/>-Zeilen, das den Kommentartext in seiner ursprünglichen
        ''' Zeilenstruktur enthält. Kann leer sein, aber sollte nicht Nothing sein.
        ''' </value>
        ''' <remarks>
        ''' Diese Eigenschaft hält eine Referenz auf das Array. Externe Änderungen am Array werden
        ''' somit in dieser Instanz sichtbar. Wenn das unerwünscht ist, verwenden Sie eine Kopie.
        ''' </remarks>
        Public Property Comment As String()
            Get
                Return Me._comment
            End Get
            Set
                Me._comment = Value
            End Set
        End Property

        ''' <summary>
        ''' Speichert den Namen des aktuellen Abschnitts.
        ''' </summary>
        ''' <value>
        ''' Der Abschnittsname ohne eckige Klammern, z. B. <c>"General"</c> für den Abschnitt [General].
        ''' Kann Nothing oder eine leere Zeichenfolge sein, wenn der Kommentar global ist.
        ''' </value>
        Public Property Section As String
            Get
                Return Me._section
            End Get
            Set
                Me._section = Value
            End Set
        End Property

        ''' <summary>
        ''' Initialisiert eine neue Instanz der Klasse <see cref="CommentEditEventArgs"/>.
        ''' </summary>
        ''' <param name="Section">Der Name des Abschnitts (ohne eckige Klammern). Kann Nothing/Leer sein.</param>
        ''' <param name="Comment">Der neue Kommentartext als Zeilenarray. Sollte nicht Nothing sein.</param>
        ''' <remarks>
        ''' Aus Leistungsgründen wird das übergebene <paramref name="Comment"/>-Array als Referenz übernommen.
        ''' Erstellen Sie bei Bedarf vor dem Aufruf eine Kopie.
        ''' </remarks>
        Public Sub New(Section As String, Comment() As String)
            Me._section = Section
            Me._comment = Comment
        End Sub

    End Class

End Namespace