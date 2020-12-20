Imports System.Collections.Specialized
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Xml.Linq


Module XMLparse

    Function GetData(Opt As String, Data As String)
        Try
            Dim xd As XDocument = XDocument.Load(CurDir() & "\" & "Config.xml")
            Dim output = xd.Element("config").Element(Opt).Element(Data).Value
            If output = "" Then
                Return "Can't Load Data"
            Else
                Return output
            End If
        Catch
            MsgBox("Config file does not exist", "Error loading Config")
        End Try


    End Function

    Function GetUserData(User As String, Opt As String)
        Try
            Dim xd As XDocument = XDocument.Load(CurDir() & "\users\" & User & ".xml")
            Dim output = xd.Element("config").Element("user").Element(Opt).Value
            If output = "" Then
                Return "Can't Load Data"
            Else
                Return output
            End If
        Catch
            MsgBox("Config file does not exist", "Error loading Config")
        End Try

    End Function

    Function writedata()
        On Error Resume Next
        Dim xd As XDocument = XDocument.Load(CurDir() & "\" & "Config.xml")
        xd.Element("config").Element("user").Element("address").Value = "hello"
    End Function

End Module

Public Class XML
    Private Sub XML_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class