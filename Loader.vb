Public Class Loader
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Label1.Text = "Please wait." Then
            Label1.Text = "Please wait.."
        ElseIf Label1.Text = "Please wait.." Then
            Label1.Text = "Please wait..."
        ElseIf Label1.Text = "Please wait..." Then
            Label1.Text = "Please wait."

        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Randomize()
        Dim value As Integer = CInt(Int((30 * Rnd()) + 1))

        If ProgressBar1.Value > 20 And FSGUI.DataGridView1.Visible = True Then
            FSGUI.expand()
        End If


        If ProgressBar1.Value > 30 And FSGUI.DataGridView2.Visible = True Then
            FSGUI.listcards()
        End If


        If ProgressBar1.Value > 50 And FSGUI.Panel4.Visible = False Then
            FSGUI.Panel4.Visible = True
        End If


        If ProgressBar1.Value > 80 And FSGUI.Timer1.Enabled = False Then
            FSGUI.Timer1.Enabled = True
            FSGUI.Timer1.Start()
        End If

        If ProgressBar1.Value + value > 100 Then
            value = 0
            Timer2.Stop()
            Timer1.Stop()
            Me.Close()
            FSGUI.TopMost = True
            FSGUI.Cursor = Cursors.Default
            FSGUI.Enabled = True

            If FSGUI.DataGridView1.Visible = True Then
                FSGUI.expand()
            End If


            If FSGUI.DataGridView2.Visible = True Then
                FSGUI.listcards()
            End If


            If FSGUI.Panel4.Visible = False Then
                FSGUI.Panel4.Visible = True
            End If


            If FSGUI.Timer1.Enabled = False Then
                FSGUI.Timer1.Enabled = True
                FSGUI.Timer1.Start()
            End If

        Else
            ProgressBar1.Value = ProgressBar1.Value + value
        End If



    End Sub

    Private Sub Loader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer2.Start()
        Timer1.Start()



    End Sub
End Class