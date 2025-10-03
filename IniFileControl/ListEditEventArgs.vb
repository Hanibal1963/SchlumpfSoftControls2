' *************************************************************************************************
' ListEditEventArgs.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System

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
Public Class ListEditEventArgs

    Inherits EventArgs

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
