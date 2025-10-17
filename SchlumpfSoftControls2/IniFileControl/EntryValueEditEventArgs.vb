' *************************************************************************************************
' EntryValueEditEventArgs.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System


Namespace IniFileControl

    ''' <summary>
    ''' Transportiert Kontextinformationen, wenn der Wert eines INI-Eintrags editiert wurde.
    ''' </summary>
    ''' <remarks>
    ''' Typisches Einsatzszenario: Wird als <see cref="EventArgs"/> bei einem Ereignis (z. B. "EntryValueEdited")
    ''' verwendet, um die betroffene Sektion, den Eintragsnamen und den neuen Wert an die Event-Handler zu übergeben.
    ''' </remarks>
    Public Class EntryValueEditEventArgs

        Inherits EventArgs

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

End Namespace