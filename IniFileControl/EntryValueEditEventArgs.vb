' *************************************************************************************************
' EntryValueEditEventArgs.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System

Public Class EntryValueEditEventArgs

    Inherits EventArgs

    Private _SelectedSection As String
    Private _SelectedEntry As String
    Private _NewValue As String

    Public Property SelectedSection As String
        Get
            Return Me._SelectedSection
        End Get
        Set
            Me._SelectedSection = Value
        End Set
    End Property

    Public Property SelectedEntry As String
        Set(value As String)
            Me._SelectedEntry = value
        End Set
        Get
            Return Me._SelectedEntry
        End Get
    End Property

    Public Property NewValue As String
        Get
            Return Me._NewValue
        End Get
        Set
            Me._NewValue = Value
        End Set
    End Property

    Public Sub New(SelectedSection As String, SelectedEntry As String, NewValue As String)
        Me._SelectedSection = SelectedSection
        Me._SelectedEntry = SelectedEntry
        Me._NewValue = NewValue
    End Sub

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
