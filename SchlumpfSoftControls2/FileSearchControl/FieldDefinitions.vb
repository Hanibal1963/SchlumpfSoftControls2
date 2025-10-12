' *************************************************************************************************
' FieldDefinitiond.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace FileSearchControl

    ''' <summary>
    ''' Definiert die Variablen
    ''' </summary>
    Module FieldDefinitions

        ' Token-Quelle zur Signalisierung eines Abbruchs an den laufenden Such-Task.
        ' Wird bei jedem Start neu erstellt und ersetzt die vorherige Instanz.
        Friend _CancellationSource As System.Threading.CancellationTokenSource

        ' Suchparameter mit Standardwerten.
        Friend _StartPath As String = String.Empty
        Friend _SearchPattern As String = $"*.*"
        Friend _SearchInSubfolders As Boolean = False

    End Module

End Namespace



