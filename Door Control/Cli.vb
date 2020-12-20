Imports System.IO
Imports System.Text

Public Class Cli

    Function Search()

        If TextBox1.TextLength = 0 Then

        Else
            Dim proc As New Process
            proc.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
            proc.StartInfo.Arguments = TextBox1.Text
            proc.StartInfo.CreateNoWindow = True
            proc.StartInfo.UseShellExecute = False
            proc.StartInfo.RedirectStandardOutput = True
            proc.Start()
            proc.WaitForExit()

            Dim output() As String = proc.StandardOutput.ReadToEnd.Split(CChar(vbLf))



            For i As Integer = output.Length - 1 To 0 Step -1
                RichTextBox1.Text = output(i) + vbCrLf + RichTextBox1.Text
            Next i

            Dim lines As String() = RichTextBox1.Lines

            If lines(0) = "" Then
                Dim rtb As List(Of String) = RichTextBox1.Lines.ToList()
                rtb.RemoveAt(0)
                RichTextBox1.Lines = rtb.ToArray()
                RichTextBox1.Refresh()
            End If

            TextBox1.Text = ""

        End If

    End Function
    Private Sub Cli_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim fs As FileStream = File.Create(CurDir() & "log.txt")

        Dim proc As New Process
        proc.StartInfo.FileName = CurDir() & "\uhppoted-cli.exe"
        proc.StartInfo.Arguments = ""
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.RedirectStandardOutput = True
        proc.Start()
        proc.WaitForExit()

        Dim output() As String = proc.StandardOutput.ReadToEnd.Split(CChar(vbLf))
        For Each ln As String In output
            RichTextBox1.Text = ln & Environment.NewLine & RichTextBox1.Text
        Next

        Dim c As Char() = {Chr(10)}
        Dim s As String() = RichTextBox1.Text.Split(c,
        StringSplitOptions.RemoveEmptyEntries)
        Array.Reverse(s)
        RichTextBox1.Text = String.Join(c, s)

        Timer1.Stop()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Search()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        DoorControl.Top = Me.Top
        DoorControl.Left = Me.Left - DoorControl.Width

    End Sub

    Private Sub Cli_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        DoorControl.Timer1.Stop()
        Timer1.Start()

    End Sub

    Private Sub Cli_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        If Me.Visible = True Then
            Timer1.Start()
            DoorControl.Timer1.Stop()

        End If
    End Sub

    Private Sub RichTextBox1_Click(sender As Object, e As EventArgs) Handles RichTextBox1.Click

    End Sub

    Private Sub RichTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles RichTextBox1.KeyDown
        e.Handled = True
    End Sub

    Private Sub RichTextBox1_Enter(sender As Object, e As EventArgs) Handles RichTextBox1.Enter

    End Sub

    Private Sub RichTextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles RichTextBox1.KeyUp
        e.Handled = True
    End Sub

    Private Sub RichTextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles RichTextBox1.KeyPress
        e.Handled = True
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Search()
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub
End Class