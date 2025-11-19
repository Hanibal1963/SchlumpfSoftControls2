' *************************************************************************************************
' PagesCollectionEditor.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

' TODO: Code noch überarbeiten

Imports System
'Imports System.Collections
Imports System.ComponentModel.Design

Namespace WizardControl

    ''' <summary>
    ''' Dient zum anzeigen der Seitenstile im Seitendesigner
    ''' </summary>
    Friend Class PagesCollectionEditor

        Inherits CollectionEditor

        Private ReadOnly _PageTypes As Type()

        Public Sub New(PageType As Type)

            MyBase.New(PageType)
            Me._PageTypes = New Type(3) {
                GetType(PageWelcome),
                GetType(PageStandard),
                GetType(PageCustom),
                GetType(PageFinish)}

        End Sub

        Protected Overrides Function CreateNewItemTypes() As Type()

            Return Me._PageTypes

        End Function

    End Class

End Namespace