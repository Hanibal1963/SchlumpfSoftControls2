' *************************************************************************************************
' PagesCollectionEditor.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Namespace WizardControl

    ''' <summary>
    ''' Dient zum anzeigen der Seitenstile im Seitendesigner
    ''' </summary>
    Friend Class PagesCollectionEditor : Inherits System.ComponentModel.Design.CollectionEditor

        Private ReadOnly _PageTypes As System.Type()

        Public Sub New(PageType As System.Type)
            MyBase.New(PageType)
            Me._PageTypes = New System.Type(3) {
                GetType(PageWelcome),
                GetType(PageStandard),
                GetType(PageCustom),
                GetType(PageFinish)}
        End Sub

        Protected Overrides Function CreateNewItemTypes() As System.Type()
            Return Me._PageTypes
        End Function

    End Class

End Namespace