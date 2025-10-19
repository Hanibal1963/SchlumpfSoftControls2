' *************************************************************************************************
' VariableDefinitions.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace ColorProgressBarControl

    Module VariableDefinitions

        Friend _ProgressUnit As Integer = 20 ' Der Betrag in Pixeln, um den der Fortschrittsbalken erhöht wird.
        Friend _ProgressValue As Integer = 1 ' Die Menge des ausgefüllten Maximalwerts.
        Friend _MaxValue As Integer = 10 ' Der Maximalwert des Fortschrittsbalkens.
        Friend _ShowBorder As Boolean = True ' Legt fest, ob der Rahmen auf der Fortschrittsanzeige aktiviert ist
        Friend _IsGlossy As Boolean = True ' Legt fest, ob der Glanz auf der Fortschrittsleiste angezeigt wird.
        Friend _BarColor As System.Drawing.Color = System.Drawing.Color.Blue ' Die Farbe des Fortschrittsbalkens
        Friend _EmptyColor As System.Drawing.Color = System.Drawing.Color.LightGray ' Die Farbe des leeren Fortschrittsbalkens.
        Friend _BorderColor As System.Drawing.Color = System.Drawing.Color.Black ' Die Farbe des Rahmens.

    End Module

End Namespace



