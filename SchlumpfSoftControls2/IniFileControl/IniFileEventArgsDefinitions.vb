' *************************************************************************************************
' IniFileEventArgsDefinitions.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

' TODO: Code noch überarbeiten

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
    Public Class CommentEditEventArgs : Inherits EventArgs

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


    ''' <summary>
    ''' Transportiert Kontextinformationen, wenn der Wert eines INI-Eintrags editiert wurde.
    ''' </summary>
    ''' <remarks>
    ''' Typisches Einsatzszenario: Wird als <see cref="EventArgs"/> bei einem Ereignis (z. B. "EntryValueEdited")
    ''' verwendet, um die betroffene Sektion, den Eintragsnamen und den neuen Wert an die Event-Handler zu übergeben.
    ''' </remarks>
    Public Class EntryValueEditEventArgs : Inherits EventArgs

        ' ---------------------------------------------------------------------------------------------
        ' Private Felder (Backing Fields) für die Properties
        ' ---------------------------------------------------------------------------------------------
        Private _SelectedSection As String   ' Name der INI-Sektion, in der der Eintrag liegt
        Private _SelectedEntry As String     ' Schlüssel/Eintragsname innerhalb der Sektion
        Private _NewValue As String          ' Neuer Text-/Wertinhalt des Eintrags

        ''' <summary>
        ''' Gibt die aktuell ausgewählte Sektion an, in der der Eintrag liegt.
        ''' </summary>
        ''' <value>Der Name der INI-Sektion (z. B. "General").</value>
        Public Property SelectedSection As String
            Get
                Return Me._SelectedSection
            End Get
            Set
                Me._SelectedSection = Value
            End Set
        End Property

        ''' <summary>
        ''' Gibt den Namen des ausgewählten Eintrags innerhalb der Sektion an.
        ''' </summary>
        ''' <value>Der Eintrags- bzw. Schlüsselname (z. B. "Theme").</value>
        Public Property SelectedEntry As String
            Set(value As String)
                Me._SelectedEntry = value
            End Set
            Get
                Return Me._SelectedEntry
            End Get
        End Property

        ''' <summary>
        ''' Enthält den neuen Wert, der für den Eintrag gesetzt wurde.
        ''' </summary>
        ''' <value>Der neue Wert/Text des Eintrags.</value>
        Public Property NewValue As String
            Get
                Return Me._NewValue
            End Get
            Set
                Me._NewValue = Value
            End Set
        End Property

        ''' <summary>
        ''' Erstellt eine neue Instanz und initialisiert alle kontextrelevanten Informationen.
        ''' </summary>
        ''' <param name="SelectedSection">Name der INI-Sektion.</param>
        ''' <param name="SelectedEntry">Eintragsname innerhalb der Sektion.</param>
        ''' <param name="NewValue">Neuer Wert des Eintrags.</param>
        Public Sub New(SelectedSection As String, SelectedEntry As String, NewValue As String)
            Me._SelectedSection = SelectedSection
            Me._SelectedEntry = SelectedEntry
            Me._NewValue = NewValue
        End Sub

        ''' <summary>
        ''' Finalizer. Ruft lediglich die Basisimplementierung auf.
        ''' </summary>
        ''' <remarks>
        ''' In .NET wird ein Finalizer nur selten benötigt. Da hier keine unmanaged Ressourcen verwaltet
        ''' werden, kann diese Überschreibung i. d. R. entfallen.
        ''' </remarks>
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        ''' <summary>
        ''' Gibt eine Zeichenfolgendarstellung der Instanz zurück.
        ''' </summary>
        ''' <returns>Standarddarstellung der Basisklasse.</returns>
        ''' <remarks>
        ''' Für eine aussagekräftigere Ausgabe könnte diese Methode überschrieben werden, z. B.:
        ''' $"[{SelectedSection}] {SelectedEntry} = {NewValue}"
        ''' </remarks>
        Public Overrides Function ToString() As String
            Return MyBase.ToString()
        End Function

        ''' <summary>
        ''' Prüft die Gleichheit mit einem anderen Objekt.
        ''' </summary>
        ''' <param name="obj">Vergleichsobjekt.</param>
        ''' <returns>True, wenn die Basisklasse Gleichheit bestimmt; andernfalls False.</returns>
        ''' <remarks>
        ''' Für semantische Gleichheit dieser EventArgs könnten Sie hier einen Vergleich der Properties
        ''' (Sektion, Eintrag, Wert) implementieren.
        ''' </remarks>
        Public Overrides Function Equals(obj As Object) As Boolean
            Return MyBase.Equals(obj)
        End Function

        ''' <summary>
        ''' Liefert den Hashcode der Instanz.
        ''' </summary>
        ''' <returns>Hashcode der Basisklasse.</returns>
        ''' <remarks>
        ''' Falls <see cref="Equals(Object)"/> semantisch überschrieben wird, sollte hier ein
        ''' konsistenter Hashcode aus den Properties gebildet werden.
        ''' </remarks>
        Public Overrides Function GetHashCode() As Integer
            Return MyBase.GetHashCode()
        End Function

    End Class

    ''' <summary>
    ''' Trägersklasse für Ereignisdaten beim Bearbeiten einer Eintragsliste (z. B. Umbenennen, Hinzufügen, Löschen)
    ''' innerhalb einer INI-Abschnittsstruktur.
    ''' </summary>
    ''' <remarks>
    ''' Diese Klasse wird typischerweise als <see cref="EventArgs"/>-Ableitung in Ereignissen verwendet, 
    ''' die eine Listeneintrag-Operation beschreiben. 
    ''' 
    ''' Bedeutungen der Felder/Eigenschaften:
    ''' - <see cref="SelectedSection"/>: Der Name des INI-Abschnitts (Section), in dem die Operation stattfindet.
    ''' - <see cref="SelectedItem"/>: Der aktuell ausgewählte/zu bearbeitende Eintrag (z. B. alter Name beim Umbenennen).
    ''' - <see cref="NewItemName"/>: Der neue Name für den Eintrag (z. B. Zielname beim Umbenennen oder Name beim Hinzufügen).
    ''' 
    ''' Hinweise:
    ''' - Für Löschvorgänge kann <see cref="NewItemName"/> leer bleiben.
    ''' - Für Hinzufüge-Operationen kann <see cref="SelectedItem"/> leer sein, während <see cref="NewItemName"/> den neuen Namen enthält.
    ''' - Strings werden standardmäßig auf <see cref="String.Empty"/> initialisiert, können jedoch auch <c>Nothing</c> sein,
    '''   wenn dies im aufrufenden Code so gesetzt wird.
    ''' </remarks>
    Public Class ListEditEventArgs : Inherits EventArgs

        ' Der Name des INI-Abschnitts (z. B. "[Allgemein]"), in dem die Listenbearbeitung erfolgt.
        Private _SelectedSection As String = String.Empty

        ' Der aktuell ausgewählte Eintrag, der bearbeitet werden soll (z. B. alter Name beim Umbenennen).
        Private _SelectedItem As String = String.Empty

        ' Der neue Name für den Eintrag (z. B. Zielname beim Umbenennen oder Name eines neuen Eintrags).
        Private _NewItemName As String = String.Empty

        ''' <summary>
        ''' Erstellt eine neue Instanz mit den relevanten Angaben zur Listenbearbeitung.
        ''' </summary>
        ''' <param name="SelectedSection">Der INI-Abschnitt, in dem die Operation stattfindet.</param>
        ''' <param name="SelectedItem">Der aktuell ausgewählte/alte Eintragsname (kann leer sein, z. B. beim Hinzufügen).</param>
        ''' <param name="NewItemName">Der neue Eintragsname (kann leer sein, z. B. beim Löschen).</param>
        Public Sub New(SelectedSection As String, SelectedItem As String, NewItemName As String)
            Me._SelectedSection = SelectedSection
            Me._SelectedItem = SelectedItem
            Me._NewItemName = NewItemName
        End Sub

        ''' <summary>
        ''' Name des INI-Abschnitts, in dem die Listenoperation erfolgt.
        ''' </summary>
        ''' <value>Ein nicht leerer String für reguläre Operationen; kann leer sein, wenn kontextbedingt.</value>
        Public Property SelectedSection As String
            Get
                Return Me._SelectedSection
            End Get
            Set(value As String)
                Me._SelectedSection = value
            End Set
        End Property

        ''' <summary>
        ''' Der aktuell ausgewählte/alte Eintrag, auf den sich die Operation bezieht.
        ''' </summary>
        ''' <value>Beim Umbenennen/Löschen der bestehende Name; beim Hinzufügen ggf. leer.</value>
        Public Property SelectedItem As String
            Get
                Return Me._SelectedItem
            End Get
            Set
                Me._SelectedItem = Value
            End Set
        End Property

        ''' <summary>
        ''' Zielname bzw. neuer Eintragsname.
        ''' </summary>
        ''' <value>Beim Umbenennen/Hinzufügen der gewünschte neue Name; beim Löschen ggf. leer.</value>
        Public Property NewItemName As String
            Get
                Return Me._NewItemName
            End Get
            Set
                Me._NewItemName = Value
            End Set
        End Property

        ' 
        ' TODO: Hinweis: Das Überschreiben von Finalize ohne eigene Ressourcenverwaltung ist in der Regel nicht notwendig
        ' und kann die Garbage Collection negativ beeinflussen. Nur beibehalten, wenn später unmanaged Ressourcen
        ' freigegeben werden sollen. Andernfalls empfiehlt sich das Entfernen dieser Methode.
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        ' Standard-Overrides ohne angepasste Logik. Können entfernt werden, solange keine spezifische Darstellung/
        ' Gleichheitslogik benötigt wird. Beibehalten, um mögliches zukünftiges Customizing zu erleichtern.
        Public Overrides Function ToString() As String
            Return MyBase.ToString()
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            Return MyBase.Equals(obj)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return MyBase.GetHashCode()
        End Function

    End Class



End Namespace