Public Class LoadingFSGUI

    Private Sub LoadingFSGUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer2.Start()
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Label1.Text = "Please wait." Then
            Label1.Text = "Please wait.."
            Me.Name = "Loading.."
        ElseIf Label1.Text = "Please wait.." Then
            Label1.Text = "Please wait..."
            Me.Name = "Loading..."
        ElseIf Label1.Text = "Please wait..." Then
            Label1.Text = "Please wait."
            Me.Name = "Loading."
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Randomize()
        Dim value As Integer = CInt(Int((10 * Rnd()) + 1))

        If ProgressBar1.Value + value > 100 Then
            value = 0
            Timer2.Stop()
            Timer1.Stop()
            Me.Close()
            FSGUI.TopMost = True
            FSGUI.Cursor = Cursors.Default
            FSGUI.Enabled = True
        Else
            ProgressBar1.Value = ProgressBar1.Value + value
        End If


    End Sub
End Class