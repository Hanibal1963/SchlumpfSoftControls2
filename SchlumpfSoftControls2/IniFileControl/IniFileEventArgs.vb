' *************************************************************************************************
' IniFileEventArgs.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace IniFileControl

    ''' <summary>
    ''' Repräsentiert die Ereignisdaten für das Bearbeiten von Kommentartexten in einer
    ''' INI-Datei.
    ''' </summary>
    ''' <remarks>
    ''' <para> Der Kommentar wird als Array von Zeilen (<see cref="String"/>)
    ''' gespeichert.<br/>
    ''' Dies erlaubt eine genaue Trennung und spätere Ausgabe, ohne Zeilenumbrüche neu
    ''' zusammensetzen zu müssen. </para>
    ''' <para> Beachten Sie, dass das übergebene Array als Referenz gespeichert
    ''' wird.<br/>
    ''' Wenn Unveränderlichkeit gewünscht ist, übergeben Sie eine Kopie (z. B. mittels <c>Comment.ToArray()</c>) oder erstellen Sie innerhalb des Aufrufers eine neue Arrayinstanz. </para>
    ''' </remarks>
    ''' <example>
    ''' <code><![CDATA[' Beispiel: Verwendung von CommentEditEventArgs
    ''' Imports IniFileControl
    '''  
    ''' Sub OnCommentEdited(section As String, lines As String())
    '''     Dim args As New CommentEditEventArgs(section, lines)
    '''     ' Zugriff auf Eigenschaften
    '''     Console.WriteLine($"Sektion: {args.Section}")
    '''     If args.Comment IsNot Nothing Then
    '''         For Each line In args.Comment
    '''             Console.WriteLine(line)
    '''         Next
    '''     End If
    ''' End Sub]]></code>
    ''' </example>
    Public Class CommentEditEventArgs : Inherits System.EventArgs

        ''' <summary>
        ''' Speichert den neuen Kommentartext als Array von Zeilen.
        ''' </summary>
        ''' <remarks>
        ''' Diese Eigenschaft hält eine Referenz auf das Array.<br/>
        ''' Externe Änderungen am Array werden somit in dieser Instanz sichtbar.<br/>
        ''' Wenn das unerwünscht ist, verwenden Sie eine Kopie.
        ''' </remarks>
        ''' <value>
        ''' Ein Array von <see cref="String"/>-Zeilen, das den Kommentartext in seiner
        ''' ursprünglichen Zeilenstruktur enthält.<br/>
        ''' Kann leer sein, aber sollte nicht Nothing sein.
        ''' </value>
        Public Property Comment As String()

        ''' <summary>
        ''' Speichert den Namen des aktuellen Abschnitts.
        ''' </summary>
        ''' <value>
        ''' Der Abschnittsname ohne eckige Klammern, z. B. <c>"General"</c> für den Abschnitt [General].<br/>
        ''' Kann Nothing oder eine leere Zeichenfolge sein, wenn der Kommentar global ist.
        ''' </value>
        Public Property Section As String

        ''' <summary>
        ''' Initialisiert eine neue Instanz der Klasse <see cref="CommentEditEventArgs"/>.
        ''' </summary>
        ''' <remarks>
        ''' Aus Leistungsgründen wird das übergebene <paramref name="Comment"/>-Array als
        ''' Referenz übernommen.<br/>
        ''' Erstellen Sie bei Bedarf vor dem Aufruf eine Kopie.
        ''' </remarks>
        ''' <param name="Section">Der Name des Abschnitts (ohne eckige Klammern).<br/>
        ''' Kann Nothing/Leer sein.</param>
        ''' <param name="Comment">Der neue Kommentartext als Zeilenarray.<br/>
        ''' Sollte nicht Nothing sein.</param>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Instanziierung von CommentEditEventArgs
        ''' Dim sectionName As String = "General"
        ''' Dim newComment As String() = {"Dies ist eine Zeile", "Noch eine Zeile"}
        ''' Dim args As New IniFileControl.CommentEditEventArgs(sectionName, newComment)
        ''' ' Weiterverarbeitung von args]]></code>
        ''' </example>
        Public Sub New(Section As String, Comment() As String)
            Me.Section = Section
            Me.Comment = Comment
        End Sub

    End Class

    ''' <summary>
    ''' Transportiert Kontextinformationen, wenn der Wert eines INI-Eintrags editiert
    ''' wurde.
    ''' </summary>
    ''' <remarks>
    ''' Typisches Einsatzszenario:<br/>
    ''' Wird als <see cref="System.EventArgs"/> bei einem Ereignis (z. B.
    ''' "EntryValueEdited") verwendet, um die betroffene Sektion, den Eintragsnamen und
    ''' den neuen Wert an die Event-Handler zu übergeben.
    ''' </remarks>
    ''' <example>
    ''' <code><![CDATA[' Beispiel: Verwendung von EntryValueEditEventArgs
    ''' Imports IniFileControl
    '''  
    ''' Sub OnEntryValueEdited(section As String, entry As String, value As String)
    '''     Dim args As New EntryValueEditEventArgs(section, entry, value)
    '''     Console.WriteLine($"[{args.SelectedSection}] {args.SelectedEntry} = {args.NewValue}")
    ''' End Sub]]></code>
    ''' </example>
    Public Class EntryValueEditEventArgs : Inherits System.EventArgs

        ''' <summary>
        ''' Gibt die aktuell ausgewählte Sektion an, in der der Eintrag liegt.
        ''' </summary>
        ''' <value>
        ''' Der Name der INI-Sektion (z. B. "General").
        ''' </value>
        Public Property SelectedSection As String

        ''' <summary>
        ''' Gibt den Namen des ausgewählten Eintrags innerhalb der Sektion an.
        ''' </summary>
        ''' <value>
        ''' Der Eintrags- bzw. Schlüsselname (z. B. "Theme").
        ''' </value>
        Public Property SelectedEntry As String

        ''' <summary>
        ''' Enthält den neuen Wert, der für den Eintrag gesetzt wurde.
        ''' </summary>
        ''' <value>
        ''' Der neue Wert/Text des Eintrags.
        ''' </value>
        Public Property NewValue As String

        ''' <summary>
        ''' Erstellt eine neue Instanz und initialisiert alle kontextrelevanten
        ''' Informationen.
        ''' </summary>
        ''' <param name="SelectedSection">Name der INI-Sektion.</param>
        ''' <param name="SelectedEntry">Eintragsname innerhalb der Sektion.</param>
        ''' <param name="NewValue">Neuer Wert des Eintrags.</param>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Instanziierung von EntryValueEditEventArgs
        ''' Dim args As New IniFileControl.EntryValueEditEventArgs("General", "Theme", "Dark")
        ''' Console.WriteLine(args.NewValue)]]></code>
        ''' </example>
        Public Sub New(SelectedSection As String, SelectedEntry As String, NewValue As String)
            Me.SelectedSection = SelectedSection
            Me.SelectedEntry = SelectedEntry
            Me.NewValue = NewValue
        End Sub

    End Class

    ''' <summary>
    ''' Trägersklasse für Ereignisdaten beim Bearbeiten einer Eintragsliste (z. B.
    ''' Umbenennen, Hinzufügen, Löschen) innerhalb einer INI-Abschnittsstruktur.
    ''' </summary>
    ''' <remarks>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Diese Klasse wird typischerweise als <see
    ''' cref="System.EventArgs"/>-Ableitung in Ereignissen verwendet, die eine
    ''' Listeneintrag-Operation beschreiben.</description>
    '''  </item>
    ''' </list>
    ''' <para><b>Bedeutungen der Felder/Eigenschaften:</b></para>
    ''' <list type="bullet">
    '''  <item>
    '''   <description><see cref="SelectedSection"/>: Der Name des INI-Abschnitts
    ''' (Section), in dem die Operation stattfindet.</description>
    '''  </item>
    '''  <item>
    '''   <description><see cref="SelectedItem"/>: Der aktuell ausgewählte/zu
    ''' bearbeitende Eintrag (z. B. alter Name beim Umbenennen).</description>
    '''  </item>
    '''  <item>
    '''   <description><see cref="NewItemName"/>: Der neue Name für den Eintrag (z. B.
    ''' Zielname beim Umbenennen oder Name beim Hinzufügen).</description>
    '''  </item>
    ''' </list>
    ''' <para><b>Hinweise:</b></para>
    ''' <list type="bullet">
    '''  <item>
    '''   <description>Für Löschvorgänge kann <see cref="NewItemName"/> leer
    ''' bleiben.</description>
    '''  </item>
    '''  <item>
    '''   <description>Für Hinzufüge-Operationen kann <see cref="SelectedItem"/> leer
    ''' sein, während <see cref="NewItemName"/> den neuen Namen enthält.</description>
    '''  </item>
    '''  <item>
    '''   <description>Strings werden standardmäßig auf <see cref="String.Empty"/> initialisiert, können jedoch auch <c>Nothing</c> sein, wenn dies im aufrufenden Code so gesetzt wird.</description>
    '''  </item>
    ''' </list>
    ''' </remarks>
    ''' <example>
    ''' <code><![CDATA[' Beispiel: Verwendung von ListEditEventArgs
    ''' Imports IniFileControl
    '''  
    ''' Sub OnListEditRename()
    '''     Dim args As New ListEditEventArgs("General", "OldName", "NewName")
    '''     Console.WriteLine($"Sektion={args.SelectedSection}, Alt={args.SelectedItem}, Neu={args.NewItemName}")
    ''' End Sub
    '''  
    ''' Sub OnListEditAdd()
    '''     Dim args As New ListEditEventArgs("General", String.Empty, "AddedName")
    ''' End Sub
    '''  
    ''' Sub OnListEditDelete()
    '''     Dim args As New ListEditEventArgs("General", "ObsoleteName", String.Empty)
    ''' End Sub]]></code>
    ''' </example>
    Public Class ListEditEventArgs : Inherits System.EventArgs

        ''' <summary>
        ''' Erstellt eine neue Instanz mit den relevanten Angaben zur Listenbearbeitung.
        ''' </summary>
        ''' <param name="SelectedSection">Der INI-Abschnitt, in dem die Operation
        ''' stattfindet.</param>
        ''' <param name="SelectedItem">Der aktuell ausgewählte/alte Eintragsname (kann leer
        ''' sein, z. B. beim Hinzufügen).</param>
        ''' <param name="NewItemName">Der neue Eintragsname (kann leer sein, z. B. beim
        ''' Löschen).</param>
        ''' <example>
        ''' <code><![CDATA[' Beispiel: Instanziierung von ListEditEventArgs
        ''' Dim args As New IniFileControl.ListEditEventArgs("General", "Old", "New")]]></code>
        ''' </example>
        Public Sub New(SelectedSection As String, SelectedItem As String, NewItemName As String)
            Me.SelectedSection = SelectedSection
            Me.SelectedItem = SelectedItem
            Me.NewItemName = NewItemName
        End Sub

        ''' <summary>
        ''' Name des INI-Abschnitts, in dem die Listenoperation erfolgt.
        ''' </summary>
        ''' <value>
        ''' Ein nicht leerer String für reguläre Operationen; kann leer sein, wenn
        ''' kontextbedingt.
        ''' </value>
        Public Property SelectedSection As String = String.Empty

        ''' <summary>
        ''' Der aktuell ausgewählte/alte Eintrag, auf den sich die Operation bezieht.
        ''' </summary>
        ''' <value>
        ''' Beim Umbenennen/Löschen der bestehende Name; beim Hinzufügen ggf. leer.
        ''' </value>
        Public Property SelectedItem As String = String.Empty

        ''' <summary>
        ''' Zielname bzw. neuer Eintragsname.
        ''' </summary>
        ''' <value>
        ''' Beim Umbenennen/Hinzufügen der gewünschte neue Name; beim Löschen ggf. leer.
        ''' </value>
        Public Property NewItemName As String = String.Empty

    End Class

End Namespace