Imports System.Collections.Specialized
Imports System.IO

Public Class DoorControl
    Dim controller

    Private Sub DoorControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim SetupPath As String = Application.StartupPath & "\uhppoted-cli.exe"
        Using sCreateMSIFile As New FileStream(SetupPath, FileMode.Create)
            sCreateMSIFile.Write(My.Resources.uhppoted_cli, 0, My.Resources.uhppoted_cli.Length)
        End Using

        Dim proc As New Process
        proc.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
        proc.StartInfo.Arguments = "get-devices"
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.RedirectStandardOutput = True
        proc.Start()
        proc.WaitForExit()

        Dim output() As String = proc.StandardOutput.ReadToEnd.Split(CChar(vbLf))
        For Each ln As String In output
            RichTextBox1.AppendText(ln)
        Next

        On Error Resume Next
        Dim getserial As String = RichTextBox1.Text
        Label1.Text = "Controller: " & getserial.Substring(0, getserial.IndexOf(" "))
        controller = getserial.Substring(0, getserial.IndexOf(" "))



        Dim proc2 As New Process
        proc2.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
        proc2.StartInfo.Arguments = "get-listener " & controller
        proc2.StartInfo.CreateNoWindow = True
        proc2.StartInfo.UseShellExecute = False
        proc2.StartInfo.RedirectStandardOutput = True
        proc2.Start()
        proc2.WaitForExit()

        Dim output2() As String = proc2.StandardOutput.ReadToEnd.Split(CChar(vbLf))
        For Each ln2 As String In output2
            RichTextBox1.Text = ln2 & Environment.NewLine & RichTextBox1.Text
        Next

        On Error Resume Next
        Dim rtb As List(Of String) = RichTextBox1.Lines.ToList()
        rtb.RemoveAt(0)
        RichTextBox1.Lines = rtb.ToArray()
        RichTextBox1.Refresh()

        Dim ip As List(Of String) = RichTextBox1.Lines.ToList()
        Dim getip As String = ip.FirstOrDefault
        Label2.Text = "IP Address: " & getip.Substring(getip.IndexOf(" "c) + 1)


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim dooropen As New Process
        dooropen.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
        dooropen.StartInfo.Arguments = "open " & controller & " 1"
        dooropen.StartInfo.CreateNoWindow = True
        dooropen.StartInfo.UseShellExecute = False
        dooropen.StartInfo.RedirectStandardOutput = True
        dooropen.Start()
        dooropen.WaitForExit()

        Dim open() As String = dooropen.StandardOutput.ReadToEnd.Split(CChar(vbLf))


        For Each op As String In open
            RichTextBox1.Text = op & Environment.NewLine & RichTextBox1.Text
        Next

        Dim rtb As List(Of String) = RichTextBox1.Lines.ToList()
        rtb.RemoveAt(0)
        RichTextBox1.Lines = rtb.ToArray()
        RichTextBox1.Refresh()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim dooropen As New Process
        dooropen.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
        dooropen.StartInfo.Arguments = "open " & controller & " 2"
        dooropen.StartInfo.CreateNoWindow = True
        dooropen.StartInfo.UseShellExecute = False
        dooropen.StartInfo.RedirectStandardOutput = True
        dooropen.Start()
        dooropen.WaitForExit()

        Dim open() As String = dooropen.StandardOutput.ReadToEnd.Split(CChar(vbLf))


        For Each op As String In open
            RichTextBox1.Text = op & Environment.NewLine & RichTextBox1.Text
        Next

        Dim rtb As List(Of String) = RichTextBox1.Lines.ToList()
        rtb.RemoveAt(0)
        RichTextBox1.Lines = rtb.ToArray()
        RichTextBox1.Refresh()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim dooropen As New Process
        dooropen.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
        dooropen.StartInfo.Arguments = "open " & controller & " 3"
        dooropen.StartInfo.CreateNoWindow = True
        dooropen.StartInfo.UseShellExecute = False
        dooropen.StartInfo.RedirectStandardOutput = True
        dooropen.Start()
        dooropen.WaitForExit()

        Dim open() As String = dooropen.StandardOutput.ReadToEnd.Split(CChar(vbLf))


        For Each op As String In open
            RichTextBox1.Text = op & Environment.NewLine & RichTextBox1.Text
        Next

        Dim rtb As List(Of String) = RichTextBox1.Lines.ToList()
        rtb.RemoveAt(0)
        RichTextBox1.Lines = rtb.ToArray()
        RichTextBox1.Refresh()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim dooropen As New Process
        dooropen.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
        dooropen.StartInfo.Arguments = "open " & controller & " 4"
        dooropen.StartInfo.CreateNoWindow = True
        dooropen.StartInfo.UseShellExecute = False
        dooropen.StartInfo.RedirectStandardOutput = True
        dooropen.Start()
        dooropen.WaitForExit()

        Dim open() As String = dooropen.StandardOutput.ReadToEnd.Split(CChar(vbLf))


        For Each op As String In open
            RichTextBox1.Text = op & Environment.NewLine & RichTextBox1.Text
        Next

        Dim rtb As List(Of String) = RichTextBox1.Lines.ToList()
        rtb.RemoveAt(0)
        RichTextBox1.Lines = rtb.ToArray()
        RichTextBox1.Refresh()

    End Sub

    Private Sub DoorControl_HelpButtonClicked(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked
        Form1.Visible = True

    End Sub
End Class