Imports System.Collections.Specialized
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Xml.Linq


Module XMLparse

    Function GetData(Opt As String, Data As String)
        On Error Resume Next
        Dim xd As XDocument = XDocument.Load(CurDir() & "\" & "Config.xml")
        Dim output = xd.Element("config").Element(Opt).Element(Data).Value
        If output = "" Then
            Return "ERROR LOADING CONFIG"
        Else
            Return output
        End If
        On Error Resume Next
    End Function

    Function writedata()
        On Error Resume Next
        Dim xd As XDocument = XDocument.Load(CurDir() & "\" & "Config.xml")
        xd.Element("config").Element("user").Element("address").Value = "hello"
    End Function

End Module

Public Class XML

End Class