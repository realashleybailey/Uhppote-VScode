Imports System.Collections.Specialized
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Xml.Linq
Imports System.Environment
Imports System.Net
Imports System.ComponentModel



Public Class FSGUI
    Dim eventindex As Integer
    Dim globalserialnumber


    Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Long
    Private Declare Function SetWindowPos Lib "user32" (ByVal hWnd As Long, ByVal hWndInsertAfter As Long, ByVal X As Long, ByVal Y As Long, ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long
    Private Declare Function ShowWindow Lib "user32" (ByVal hWnd As Long, ByVal nCmdShow As Long) As Long
    Private Const SWP_NOSIZE = &H1
    Private Const SWP_NOMOVE = &H2
    Private Const SWP_NOZORDER = &H4
    Private Const SWP_FRAMECHANGED = &H20 '  The frame changed: send WM_NCCALCSIZE
    Private Const SWP_SHOWWINDOW = &H40
    Private Const SWP_HIDEWINDOW = &H80

    Function hidetaskbar()
        Dim TaskBarhWnd As Long
        Dim StartButtonhWnd As Long
        Dim l As Long
        '
        TaskBarhWnd = FindWindow("Shell_TrayWnd", "")
        l = SetWindowPos(TaskBarhWnd, 0, 0, 0, 0, 0, SWP_HIDEWINDOW)
        l = ShowWindow(TaskBarhWnd, SWP_HIDEWINDOW)
        StartButtonhWnd = FindWindow(vbNullString, "Start")
        l = SetWindowPos(StartButtonhWnd, 0, 0, 0, 0, 0, SWP_HIDEWINDOW)
        l = ShowWindow(StartButtonhWnd, SWP_HIDEWINDOW)
    End Function

    Function showtaskbar()
        Dim TaskBarhWnd As Long
        Dim StartButtonhWnd As Long
        Dim l As Long
        '
        TaskBarhWnd = FindWindow("Shell_TrayWnd", "")
        l = SetWindowPos(TaskBarhWnd, 0, 0, 0, 0, 0, SWP_SHOWWINDOW)
        l = ShowWindow(TaskBarhWnd, SWP_SHOWWINDOW)
        l = SetWindowPos(TaskBarhWnd, 0, 0, 0, 0, 0, SWP_FRAMECHANGED)
        StartButtonhWnd = FindWindow(vbNullString, "Start")
        l = SetWindowPos(StartButtonhWnd, 0, 0, 0, 0, 0, SWP_SHOWWINDOW)
        l = ShowWindow(StartButtonhWnd, SWP_SHOWWINDOW)
        l = SetWindowPos(StartButtonhWnd, 0, 0, 0, 0, 0, SWP_FRAMECHANGED)
    End Function

    Function listcards()

        Dim sn = DataGridView1.SelectedCells(1).Value

        DataGridView2.Rows.Clear()

        Dim proc As New Process
        proc.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
        proc.StartInfo.Arguments = "get-cards " & sn
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.RedirectStandardOutput = True
        proc.Start()
        proc.WaitForExit()

        Dim i As Integer = 0
        Dim output() As String = proc.StandardOutput.ReadToEnd.Split(CChar(vbLf))

        For Each allcards In output
            If allcards = "" Then

            Else
                i += 1
                Dim card As String() = allcards.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                DataGridView2.Rows.Add(i, sn, TimeOfDay.ToString("h:mm:ss tt"), "Loading User: " & card(0), "Created: " & card(1) & " | Expires: " & card(2))
            End If
        Next
    End Function
    Public Function get_time(datetime As String)
        Dim proc1 As New Process
        proc1.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
        proc1.StartInfo.Arguments = "get-time " & ToolStripLabel1.Text
        proc1.StartInfo.CreateNoWindow = True
        proc1.StartInfo.UseShellExecute = False
        proc1.StartInfo.RedirectStandardOutput = True
        proc1.Start()
        proc1.WaitForExit()

        Dim output1() As String = proc1.StandardOutput.ReadToEnd.Split(CChar(vbLf))
        Dim time() = output1(0).Remove(0, 66).Split(" ")

        If datetime = "date" Then
            Return time(1)
        ElseIf datetime = "time" Then
            Return time(2)
        End If
    End Function
    Public Function reset()
        find()

        GroupBox1.Text = "Connect Controller:"
        DataGridView2.Visible = False
        DataGridView1.Visible = True

        Dim old As Padding = Panel1.Padding
        Panel1.Padding = New Padding(old.Left, 35, old.Right, old.Bottom)

        Panel2.Visible = True
    End Function

    Function expand()
        If DataGridView1.Visible = False Then
            find()

            GroupBox1.Text = "Connect Controller:"
            DataGridView2.Visible = False
            DataGridView1.Visible = True

            Dim old As Padding = Panel1.Padding
            Panel1.Padding = New Padding(old.Left, 35, old.Right, old.Bottom)

            Panel2.Visible = True

        ElseIf DataGridView1.Visible = True Then
            DataGridView2.Rows.Clear()

            GroupBox1.Text = "Information:"
            DataGridView2.Visible = True
            DataGridView1.Visible = False

            Dim old As Padding = Panel1.Padding
            Panel1.Padding = New Padding(old.Left, 0, old.Right, old.Bottom)

            Panel2.Visible = False
        Else
            MsgBox("Error 0x34993")
        End If
    End Function

    Function find()
        DataGridView1.Rows.Clear()

        Dim gateway As String = "192.168.0.1"

        Dim pcip As String = Dns.GetHostEntry(Dns.GetHostName()).AddressList _
            .Where(Function(a As IPAddress) Not a.IsIPv6LinkLocal AndAlso Not a.IsIPv6Multicast AndAlso Not a.IsIPv6SiteLocal) _
            .First() _
            .ToString()

        Try
            Dim SetupPath As String = Application.StartupPath & "\uhppoted-cli.exe"
            Using sCreateMSIFile As New FileStream(SetupPath, FileMode.Create)
                sCreateMSIFile.Write(My.Resources.uhppoted_cli, 0, My.Resources.uhppoted_cli.Length)
            End Using
        Catch
        End Try

        Dim proc As New Process
        proc.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
        proc.StartInfo.Arguments = "get-devices"
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.RedirectStandardOutput = True
        proc.Start()
        proc.WaitForExit()

        Dim output() As String = proc.StandardOutput.ReadToEnd.Split(CChar(vbLf))

        Dim controllers As String() = output(0).Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
        DataGridView1.Rows.Add("1", controllers(0), controllers(1), controllers(2), gateway, controllers(4), pcip)

        Label1.Text = "Controllers: " + DataGridView1.Rows.Count.ToString
    End Function

    Function starttimers()
        ' Enable Timers
        GetEvent.Enabled = True
        GetEventIndex.Enabled = True
        'Start Timers
        GetEventIndex.Start()
        GetEvent.Start()
    End Function

    Public Function serialnumber()
        Dim sn As Integer = DataGridView1.SelectedCells(1).Value
        globalserialnumber = DataGridView1.SelectedCells(1).Value
        ToolStripLabel2.Text = sn
        Loader.Visible = True
    End Function

    Private Sub FSGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        Me.TopMost = False
        Me.Cursor = Cursors.WaitCursor
        Me.Enabled = False
        LoadingFSGUI.Visible = True
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub FSGUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        showtaskbar()
        Environment.Exit(0)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        showtaskbar()
        Environment.Exit(0)
    End Sub
    Private Sub ExitFullScreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        Me.Hide()
        showtaskbar()
        DoorControl.ShowInTaskbar = True
        DoorControl.WindowState = FormWindowState.Normal
        DoorControl.TopMost = True
        GetEvent.Stop()
        GetEventIndex.Stop()
    End Sub

    Private Sub NewControllerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click

        If ToolStripLabel2.Text = "Controller" Then
            MsgBox("Please select a controller")
        Else
            ToolStripLabel2.Text = "Controller"
            expand()
        End If

    End Sub

    Private Sub ToolStripMenuItemDisplay_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemDisplay.Click
        Me.TopMost = False
        ChangeDisplay.getscreen()
        ChangeDisplay.Visible = True
        Me.Enabled = False
    End Sub

    Private Sub ToolStripMenuItemUsers_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem6.Click
        Me.TopMost = False
        EditUsers.Visible = True
        Me.Enabled = False
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        find()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.TopMost = False
        Me.Cursor = Cursors.WaitCursor
        Me.Enabled = False


        serialnumber()
    End Sub



    Private Sub Clock_Tick(sender As Object, e As EventArgs) Handles Clock.Tick

        Try
            ToolStripLabel3.Text = TimeOfDay.ToString("h:mm:ss tt")

            If ToolStripLabel2.Text = "Controller" Then
            Else
                Dim proc As New Process
                proc.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
                proc.StartInfo.Arguments = "get-status " & ToolStripLabel2.Text
                proc.StartInfo.CreateNoWindow = True
                proc.StartInfo.UseShellExecute = False
                proc.StartInfo.RedirectStandardOutput = True
                proc.Start()
                proc.WaitForExit()

                Dim output() As String = proc.StandardOutput.ReadToEnd.Split(CChar(vbLf))
                Dim result() = output(0).Remove(0, 66).Split(" ")

                Dim code As Integer = result(0)


                If code = 24 Then
                    If ToolStripLabel1.Text = "Door 1 is closed" Then

                    Else
                        ToolStripLabel1.Text = "Door 1 is closed"
                    End If

                    For Each listItem As ListViewItem In ListView1.Items
                        If listItem.SubItems.Item(0).Text = "Door 1" Then
                            listItem.ImageIndex = 0
                        End If
                    Next
                End If

                If code = 23 Then
                    If ToolStripLabel1.Text = "Door is open" Then

                    Else
                        ToolStripLabel1.Text = "Door 1 is open"
                    End If

                    For Each listItem As ListViewItem In ListView1.Items
                        If listItem.SubItems.Item(0).Text = "Door 1" Then
                            listItem.ImageIndex = 1
                        End If
                    Next
                End If

            End If
        Catch
        End Try

    End Sub

    Private Sub ListView1_MouseClick(sender As Object, e As MouseEventArgs) Handles ListView1.MouseClick
        If e.Button = MouseButtons.Right Then
            If ListView1.FocusedItem.Bounds.Contains(e.Location) Then
                ContextMenuStrip1.Show(Cursor.Position)
            End If
        End If
    End Sub

    Private Sub Panel7_Paint(sender As Object, e As PaintEventArgs) Handles Panel7.Paint

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        hidetaskbar()
        find()
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        LoadingFSGUI.ProgressBar1.Value = 90
    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged

    End Sub

    Private Sub FSGUI_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub TableLayoutPanel1_DoubleClick(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GetEventIndex_Tick(sender As Object, e As EventArgs) Handles GetEventIndex.Tick
        Try
            Dim proc1 As New Process
            proc1.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
            proc1.StartInfo.Arguments = "get-events " & ToolStripLabel2.Text
            proc1.StartInfo.CreateNoWindow = True
            proc1.StartInfo.UseShellExecute = False
            proc1.StartInfo.RedirectStandardOutput = True
            proc1.Start()
            proc1.WaitForExit()

            Dim output1() As String = proc1.StandardOutput.ReadToEnd.Split(CChar(vbLf))

            Try
                If output1(0).Length > 0 Then
                    Dim result1() = output1(0).Split("  ")
                    If result1(2).Length > 0 Then
                        eventindex = result1(2) - 1
                    End If
                End If
            Catch
            End Try

        Catch
        End Try

    End Sub

    Private Sub GetEvent_Tick(sender As Object, e As EventArgs) Handles GetEvent.Tick


        Try
            Dim proc As New Process
            proc.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
            proc.StartInfo.Arguments = "get-event " & ToolStripLabel2.Text
            proc.StartInfo.CreateNoWindow = True
            proc.StartInfo.UseShellExecute = False
            proc.StartInfo.RedirectStandardOutput = True
            proc.Start()
            proc.WaitForExit()

            Dim output() As String = proc.StandardOutput.ReadToEnd.Split(CChar(vbLf))

            Try
                If output(0).Length > 0 Then
                    Dim result() = output(0).Split(" ")
                    If result(7) > 10 Then
                        Label3.Text = result(7)
                    End If
                End If
            Catch
            End Try

        Catch
        End Try

    End Sub

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim user = Label3.Text

        If System.IO.File.Exists(CurDir() & "/users/" & user & ".xml") Then

            Try

                If Label10.Text = GetUserData(user, "name") Then

                Else
                    Label10.Text = GetUserData(user, "name")

                    If GetUserData(user, "imageexist") = "true" Then
                        Dim filename As String = System.IO.Path.Combine(CurDir() & "/users/images/", user)
                        Try
                            Using fs As New System.IO.FileStream(filename, IO.FileMode.Open)
                                PictureBox1.Image = New Bitmap(Image.FromStream(fs))
                            End Using
                        Catch
                        End Try
                    End If

                End If

            Catch
            End Try

        Else

            If Label10.Text = "User not found" Then

            Else
                Label10.Text = "User not found"
                PictureBox1.Image = Nothing
            End If

        End If

    End Sub

    Private Sub ToolStripLabel1_TextChanged(sender As Object, e As EventArgs) Handles ToolStripLabel1.TextChanged
        If ToolStripLabel1.Text = "Door 1 is open" Then

        ElseIf ToolStrip1.Text = "Door 1 was closed" Then

        End If
    End Sub

End Class