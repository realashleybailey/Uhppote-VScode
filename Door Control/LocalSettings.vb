Public Class LocalSettings


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        DoorControl.Top = Me.Top
        DoorControl.Left = Me.Left - DoorControl.Width

    End Sub

    Private Sub LocalSettings_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        If Me.Visible = True Then
            Timer1.Start()
            DoorControl.Timer1.Stop()

        End If
    End Sub

    Private Sub LocalSettings_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        DoorControl.Timer1.Stop()
        Timer1.Start()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub LocalSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        If My.Resources.FullScreenWarning = "False" Then
            Button1.Text = "Enable"
        Else
            Button1.Text = "Disable"
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        WarningScreen.Start()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        writedata()
    End Sub
End Class