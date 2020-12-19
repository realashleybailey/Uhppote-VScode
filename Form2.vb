Public Class Form2
    Function fullscreen()
        FSGUI.Visible = True
        DoorControl.Visible = False
    End Function

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Environment.GetCommandLineArgs.Length > 0 Then
            Dim command() = Environment.GetCommandLineArgs

            For Each cmd As String In command
                If cmd = "-fullscreen" Then
                    fullscreen()
                Else
                    DoorControl.Visible = True
                End If
            Next
        End If

    End Sub
End Class