' *************************************************************************************************
' CommenteditEventArgs.vb
' Copyright (c) 2025 by Andreas Sauer 
' *************************************************************************************************

Imports System

Namespace IniFileControl

    Public Class CommentEditEventArgs

        Inherits EventArgs

        Private _comment() As String
        Private _section As String

        ''' <summary>
        ''' Speichert den neuen Kommentartext.
        ''' </summary>
        Public Property Comment As String()
            Get
                Return Me._comment
            End Get
            Set
                Me._comment = Value
            End Set
        End Property

        ''' <summary>
        ''' speichert den Name des aktuellen Abschnitts
        ''' </summary>
        ''' <returns></returns>
        Public Property Section As String
            Get
                Return Me._section
            End Get
            Set
                Me._section = Value
            End Set
        End Property

        ''' <summary>
        ''' Initialisiert eine neue Instanz der Klasse IniFileCommenteditEventArgs.
        ''' </summary>
        ''' <param name="Section">
        ''' Der Name des Abschnitts.
        ''' </param>
        ''' <param name="Comment">
        ''' Der neue Kommentartext.
        ''' </param>
        Public Sub New(Section As String, Comment() As String)
            Me._section = Section
            Me._comment = Comment
        End Sub

    End Class


End Namespace