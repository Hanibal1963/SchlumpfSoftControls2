' *************************************************************************************************
' ListEditEventArgs.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System

Public Class ListEditEventArgs

    Inherits EventArgs

    Private _SelectedSection As String = String.Empty
    Private _SelectedItem As String = String.Empty
    Private _NewItemName As String = String.Empty

    Public Sub New(SelectedSection As String, SelectedItem As String, NewItemName As String)
        Me._SelectedSection = SelectedSection
        Me._SelectedItem = SelectedItem
        Me._NewItemName = NewItemName
    End Sub

    Public Property SelectedSection As String
        Get
            Return Me._SelectedSection
        End Get
        Set(value As String)
            Me._SelectedSection = value
        End Set
    End Property

    Public Property SelectedItem As String
        Get
            Return Me._SelectedItem
        End Get
        Set
            Me._SelectedItem = Value
        End Set
    End Property

    Public Property NewItemName As String
        Get
            Return Me._NewItemName
        End Get
        Set
            Me._NewItemName = Value
        End Set
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

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
