Public NotInheritable Class SplashScreen


    Private Sub SplashScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor)
        Copyright.Text = My.Application.Info.Copyright


        If Environment.GetCommandLineArgs.Length > 0 Then

            Dim command() = Environment.GetCommandLineArgs

            For Each cmd As String In command
                If cmd = "-fullscreen" Then
                    DoorControl.fullscreen()
                Else
                    Me.Visible = False
                    DoorControl.Visible = True
                End If
            Next
        End If

    End Sub

    Dim form2 As Form2


    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

    End Sub

End Class
