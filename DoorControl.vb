Imports System.Collections.Specialized
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Xml.Linq
Imports System.Environment


Public Class DoorControl

    Dim controller
    Private ReadOnly settingsyaml = CurDir() & "\" & "Config.xml"
    Function Settings(Reset As String)

        If Reset = True Then
            File.Create(settingsyaml).Dispose()
        End If

        If System.IO.File.Exists(settingsyaml) Then

        Else
            File.Create(settingsyaml).Dispose()

        End If
    End Function

    Public Shared Function fullscreen()
        If DoorControl.ShowInTaskbar = True Then
            If FSGUI.DataGridView1.Visible = False Then
                FSGUI.reset()
            End If

            FSGUI.Visible = True

                DoorControl.ShowInTaskbar = False
                DoorControl.WindowState = FormWindowState.Minimized
                DoorControl.TopMost = False
            Else
                DoorControl.ShowInTaskbar = True
            DoorControl.WindowState = FormWindowState.Normal
            DoorControl.TopMost = True
        End If
    End Function

    Function firstrun()
        Dim sTarget
        Dim sLocation
        Dim sName

        Dim oShell As New IWshRuntimeLibrary.WshShell
        Dim oShortcut As IWshRuntimeLibrary.WshShortcut
        Dim oFS As New IWshRuntimeLibrary.FileSystemObject
        Dim Arguments As String

        sTarget = Application.ExecutablePath
        sLocation = Application.StartupPath
        sName = oFS.GetBaseName(sTarget) & " Fullscreen"
        sLocation = sLocation & "\" & sName & ".lnk"
        Arguments = "-fullscreen"

        oShortcut = oShell.CreateShortcut(sLocation)
        oShortcut.TargetPath = sTarget
        oShortcut.Arguments = Arguments
        oShortcut.Save()



    End Function




    Private Sub DoorControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Environment.GetCommandLineArgs.Length > 0 Then
            Dim command() = Environment.GetCommandLineArgs

            For Each cmd As String In command
                If cmd = "-fullscreen" Then
                    fullscreen()


                Else
                    firstrun()
                    Me.Visible = True
                    Settings(False)
                    Me.Text = GetData("user", "name")

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

                    RichTextBox1.Text = output(0) & Environment.NewLine & RichTextBox1.Text

                    On Error Resume Next
                    Dim serial = output(0).Split(" ")
                    Dim ip = output(0).Split(" ")
                    Label1.Text = "Controller: " & serial(0)

                    Label4.Text = "IP Address: " & ip(2)
                    controller = serial(0)



                    Dim proc2 As New Process
                    proc2.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
                    proc2.StartInfo.Arguments = "get-listener " & controller
                    proc2.StartInfo.CreateNoWindow = True
                    proc2.StartInfo.UseShellExecute = False
                    proc2.StartInfo.RedirectStandardOutput = True
                    proc2.Start()
                    proc2.WaitForExit()

                    Dim output2() As String = proc2.StandardOutput.ReadToEnd.Split(CChar(vbLf))
                    RichTextBox1.Text = output2(0) & Environment.NewLine & RichTextBox1.Text

                    Dim udp = output2(0).Split(" ")
                    Label2.Text = "UDP Address: " & udp(2)

                    Cli.Top = Me.Top
                    Cli.Left = Me.Left + Me.Width
                    Cli.Height = Me.Height
                    Cli.Width = 698


                    Cli.Timer1.Stop()
                    Timer1.Start()


                End If
            Next
        End If

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
        RichTextBox1.Text = open(0) & Environment.NewLine & RichTextBox1.Text


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
        RichTextBox1.Text = open(0) & Environment.NewLine & RichTextBox1.Text

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
        RichTextBox1.Text = open(0) & Environment.NewLine & RichTextBox1.Text

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
        RichTextBox1.Text = open(0) & Environment.NewLine & RichTextBox1.Text

    End Sub

    Private Sub DoorControl_HelpButtonClicked(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked
        Form1.Visible = True

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim proc As New Process
        proc.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
        proc.StartInfo.Arguments = ""
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.RedirectStandardOutput = True
        proc.Start()
        proc.WaitForExit()

        Dim output() As String = proc.StandardOutput.ReadToEnd.Split(CChar(vbLf))


        If Cli.Visible = True Then
            Cli.Visible = False
            Button5.Text = "Open Cli App"
            Cli.RichTextBox1.Text = ""

            For i As Integer = output.Count - 1 To 0 Step -1
                Cli.RichTextBox1.Text = output(i) + vbCrLf + Cli.RichTextBox1.Text
            Next i

        ElseIf Cli.Visible = False Then
            Cli.Visible = True
            Button5.Text = "Close Cli App"

            Dim lines As String() = Cli.RichTextBox1.Lines

            If lines(0) = "" Then
                Dim rtb As List(Of String) = Cli.RichTextBox1.Lines.ToList()
                rtb.RemoveAt(0)
                Cli.RichTextBox1.Lines = rtb.ToArray()
                Cli.RichTextBox1.Refresh()
            End If
        End If

        If LocalSettings.Visible = True Then
            LocalSettings.Visible = False
        End If


    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Cli.Top = Me.Top
        Cli.Left = Me.Left + Me.Width
        Cli.Height = Me.Height
        Cli.Width = 698

        LocalSettings.Top = Me.Top
        LocalSettings.Left = Me.Left + Me.Width
        LocalSettings.Height = Me.Height

    End Sub


    Private Sub DoorControl_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        Cli.Timer1.Stop()
        LocalSettings.Timer1.Stop()
        Timer1.Start()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Dim proc As New Process
        proc.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
        proc.StartInfo.Arguments = "get-status " & controller
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.RedirectStandardOutput = True
        proc.Start()
        proc.WaitForExit()

        Dim output() As String = proc.StandardOutput.ReadToEnd.Split(CChar(vbLf))
        Dim result() = output(0).Remove(0, 66).Split(" ")
        If result(0) = 44 Or result(0) = 24 Then
            Label3.Text = "Door 1 is Closed"
            Label5.Visible = True
            Label6.Visible = False
            Label5.Text = 1
            PictureBox1.Image = My.Resources.closed

        ElseIf result(0) = 23 Then
            Label3.Text = "Door 1 is Open"
            Label5.Visible = False
            Label6.Visible = True
            Label6.Text = 1
            PictureBox1.Image = My.Resources.open
        Else
            MsgBox("error")

        End If





    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        If LocalSettings.Visible = True Then
            LocalSettings.Visible = False
        ElseIf LocalSettings.Visible = False Then
            LocalSettings.Visible = True
        End If

        If Cli.Visible = True Then
            Cli.Visible = False
            Button5.Text = "Open Cli App"
        End If

    End Sub


    Private Sub RichTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles RichTextBox1.KeyDown
        e.Handled = True
    End Sub

    Private Sub RichTextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles RichTextBox1.KeyUp
        e.Handled = True
    End Sub

    Private Sub RichTextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles RichTextBox1.KeyPress
        e.Handled = True
    End Sub

    Private Sub DoorControl_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Environment.Exit(0)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        DoorControlSettings.Visible = True
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        fullscreen()
    End Sub
End Class