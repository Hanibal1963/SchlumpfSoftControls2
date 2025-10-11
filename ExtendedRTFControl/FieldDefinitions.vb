' *************************************************************************************************
' FieldDefinitions.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ExtendedRTFControl

    ''' <summary>
    ''' Definition der Variablen
    ''' </summary>
    ''' <remarks></remarks>
    Module FieldDefinitions

        ''' <summary>
        ''' Zähler für geschachtelte Update-Blöcke.
        ''' </summary>
        ''' <remarks>
        ''' Redraw-Unterdrückung
        ''' </remarks>
        Friend _updateNesting As Integer = 0

        ''' <summary>
        ''' Flag zur Unterdrückung von "OnSelectionChanged", wenn intern temporär
        ''' per-Zeichen-Selektionen durchgeführt werden.
        ''' </summary>
        ''' <remarks>
        ''' Mischzustandsanalyse
        ''' </remarks>
        Friend _suppressSelectionEvents As Boolean = False

    End Module

End Namespace

