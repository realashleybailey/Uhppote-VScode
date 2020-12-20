﻿Imports System.Configuration
Imports System.Collections.Specialized
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Xml.Linq
Imports System.Environment

Public Class EditUsers
    Public Function userfileedit(imageexist)
        Try
            If System.IO.File.Exists(CurDir() & "/users/" & ListBox1.SelectedItem & ".xml") Then
                My.Computer.FileSystem.DeleteFile(CurDir() & "/users/" & ListBox1.SelectedItem & ".xml")
                userfileedit("unkown")
            Else

                If PictureBox1.Image Is Nothing And imageexist = "unkown" Then
                    imageexist = "false"
                Else
                    imageexist = "true"
                End If

                Dim config As System.IO.StreamWriter
                config = My.Computer.FileSystem.OpenTextFileWriter(CurDir() & "/users/" & ListBox1.SelectedItem & ".xml", True)
                config.WriteLine("<?xml version=""1.0"" encoding=""utf-8"" ?>")
                config.WriteLine("<!-- DO NOT EDIT THIS FILE AS IT COULD BREAK THE PROGRAM, EDIT SETTINGS THROUGH THE GUI INTERFACE -->")
                config.WriteLine("<config>")
                config.WriteLine("	<user>")
                config.WriteLine("		<name>" & TextBox1.Text & "</name>")
                config.WriteLine("		<address>" & DomainUpDown1.Text & " ^ " & TextBox5.Text & " ^ " & TextBox7.Text & " ^ " & TextBox8.Text & " ^ " & ComboBox1.Text & "</address>")
                config.WriteLine("		<phone>" & DomainUpDown2.Text & " ^ " & TextBox6.Text & "</phone>")
                config.WriteLine("		<email>" & TextBox3.Text & "</email>")
                config.WriteLine("      <imageexist>" & imageexist & "</imageexist>")
                config.WriteLine("      <warn>" & CheckBox1.Enabled & "</warn>")
                config.WriteLine("      <keycode>" & TextBox4.Text & "</keycode>")
                config.WriteLine("	</user>")
                config.WriteLine("</config>")
                config.Close()
            End If
        Catch er As Exception
            Dim msg As String = "Exception: " & er.ToString
            MessageBox.Show(msg, "Error")
        End Try
    End Function

    Function loaduserinfo()
        TextBox4.Text = ListBox1.SelectedItem
        Dim user = ListBox1.SelectedItem

        If System.IO.File.Exists(CurDir() & "/users/" & ListBox1.SelectedItem & ".xml") Then

            Try
                Dim numbersplit As String = GetUserData(user, "phone")
                Dim number = numbersplit.Split(" ^ ")

                Dim addresssplit As String = GetUserData(user, "address")
                Dim address = addresssplit.Split(" ^ ")

                TextBox1.Text = GetUserData(user, "name")
                TextBox3.Text = GetUserData(user, "email")

                DomainUpDown2.Text = number(0)
                TextBox6.Text = number(1)

                DomainUpDown1.Text = address(0)
                TextBox5.Text = address(1)
                TextBox7.Text = address(2)
                TextBox8.Text = address(3)
                ComboBox1.SelectedItem = address(4)

                If GetUserData(user, "imageexist") = "true" Then
                    Dim filename As String = System.IO.Path.Combine(CurDir() & "/users/images/", user)

                    Try
                        Using fs As New System.IO.FileStream(filename, IO.FileMode.Open)
                            PictureBox1.Image = New Bitmap(Image.FromStream(fs))
                        End Using
                    Catch ex As Exception
                        Dim msg As String = "Filename: " & filename &
                            Environment.NewLine & Environment.NewLine &
                            "Exception: " & ex.ToString
                        MessageBox.Show(msg, "Error Opening Image File")
                        userfileedit("false")
                        My.Computer.FileSystem.DeleteFile(CurDir() & "/users/images/" & user)
                    End Try
                End If
            Catch
            End Try
        End If
    End Function
    Private Sub EditUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ListBox1.Items.Clear()

            Dim proc As New Process
            proc.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
            proc.StartInfo.Arguments = "get-cards " & FSGUI.ToolStripLabel2.Text
            proc.StartInfo.CreateNoWindow = True
            proc.StartInfo.UseShellExecute = False
            proc.StartInfo.RedirectStandardOutput = True
            proc.Start()
            proc.WaitForExit()

            Dim output() As String = proc.StandardOutput.ReadToEnd.Split(CChar(vbLf))

            For Each allcards In output
                Dim card As String() = allcards.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                ListBox1.Items.Add(card(0))
            Next
        Catch
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ListBox1.Items.Add("Edit user data")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        TextBox1.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        DomainUpDown2.Text = "44"
        DomainUpDown1.Text = ""
        CheckBox1.Checked = False
        PictureBox1.Image = Nothing

        loaduserinfo()

    End Sub

    Private Sub EditUsers_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        FSGUI.TopMost = True
        Me.Visible = False
        FSGUI.Enabled = True
        FSGUI.GetEvent.Start()
        FSGUI.GetEventIndex.Start()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ListBox1.SelectedIndex > 0 Then
            Dim ofd As New OpenFileDialog
            ofd.Filter = "JPEG|*.jpg"

            If ofd.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
            Try
                Dim bmp As New Bitmap(ofd.FileName)
                If Not IsNothing(PictureBox1.Image) Then PictureBox1.Image.Dispose()
                PictureBox1.Image = bmp
                PictureBox1.Image.Save(CurDir() & "/users/images/" & ListBox1.SelectedItem, System.Drawing.Imaging.ImageFormat.Jpeg)
                userfileedit("unkown")
            Catch er As Exception
                Dim msg As String = "Exception: " & er.ToString
                MessageBox.Show(msg, "Error")
            End Try
        Else
            MsgBox("Please select a user")
        End If


    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Dim user = ListBox1.SelectedItem
            PictureBox1.Image = Nothing
            userfileedit("false")
            My.Computer.FileSystem.DeleteFile(CurDir() & "/users/images/" & user)
        Catch ex As Exception
            Dim msg As String = "Exception: " & ex.ToString
            MessageBox.Show(msg, "Error")
        End Try

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        userfileedit("unkown")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            Dim user = ListBox1.SelectedItem
            PictureBox1.Image = Nothing

            If GetUserData(user, "imageexist") = "true" Then
                My.Computer.FileSystem.DeleteFile(CurDir() & "/users/images/" & user)
            End If

            My.Computer.FileSystem.DeleteFile(CurDir() & "/users/" & user & ".xml")

            loaduserinfo()

        Catch ex As Exception
            Dim msg As String = "Exception: " & ex.ToString
            MessageBox.Show(msg, "Error")
        End Try
    End Sub
End Class