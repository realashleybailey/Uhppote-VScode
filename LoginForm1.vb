
Imports System.Security.Cryptography
Imports System.Text
Imports System.Collections.Specialized
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Xml.Linq

Public Class LoginForm1

    Shared Function GetHash(theInput As String) As String

        Using hasher As MD5 = MD5.Create()    ' create hash object

            ' Convert to byte array and get hash
            Dim dbytes As Byte() =
             hasher.ComputeHash(Encoding.UTF8.GetBytes(theInput))

            ' sb to create string from bytes
            Dim sBuilder As New StringBuilder()

            ' convert byte data to hex string
            For n As Integer = 0 To dbytes.Length - 1
                sBuilder.Append(dbytes(n).ToString("X2"))
            Next n

            Return sBuilder.ToString()
        End Using

    End Function


    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See https://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.


    Public Shared Function GetLogin() As Object
        On Error Resume Next
        Dim loginxml = CurDir() & "\" & "Config.xml"
        Dim logintovb As XDocument = XDocument.Load(loginxml)
        Dim username As String = logintovb.Element("config").Element("user").Element("login").Value


    End Function

    Public Shared Function VerifyLogin(user, pass) As Object
        Dim xml = CurDir() & "\" & "Config.xml"
        Dim xmldoc As XDocument = XDocument.Load(xml)

        Dim username = Encryption.HashString(user)
        Dim password = Encryption.HashString(pass)
        Dim userpass = username + password


        Dim login = xmldoc.Element("config").Element("user").Element("login").Value


        If userpass = login Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click

        Dim user = UsernameTextBox.Text
        Dim pass = PasswordTextBox.Text

        If VerifyLogin(user, pass) = True Then
            Me.Visible = False
            DoorControl.Visible = True
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub LoginForm1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LogoPictureBox.Image = My.Resources._512x512bb
    End Sub
End Class
