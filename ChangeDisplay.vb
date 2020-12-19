Public Class ChangeDisplay
    Private lvi As String
    Function getscreen()
        ListView1.Clear()

        For index As Integer = 0 To Screen.AllScreens.Length - 1
            Dim display = Screen.AllScreens(index)
            Dim Current_Screen As Screen = Screen.FromControl(FSGUI)
            Dim currentscreen = 0

            If Current_Screen.DeviceName = display.DeviceName Then
                currentscreen = 1
            End If

            Dim lvi As ListViewItem = New ListViewItem With {
                .Text = display.DeviceName,
                .ImageIndex = currentscreen
            }


            ListView1.Items.Add(lvi)

            Console.WriteLine("Name: {0}", display.DeviceName)
            Console.WriteLine("Bounds: {0}", display.Bounds)
            Console.WriteLine("Working Area: {0}", display.WorkingArea)
            Console.WriteLine("Primary Screen: {0}", display.Primary)
            Console.WriteLine("Type: {0}", display.[GetType]())
        Next
    End Function


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ListView1.SelectedItems.Count = 1 Then
            Dim index As Integer = ListView1.FocusedItem.Index
            FSGUI.WindowState = FormWindowState.Normal
            Me.Location = Screen.AllScreens(index).Bounds.Location
            DoorControl.Location = Screen.AllScreens(index).Bounds.Location
            FSGUI.Location = Screen.AllScreens(index).Bounds.Location
            FSGUI.WindowState = FormWindowState.Maximized
            getscreen()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListView1.SelectedItems.Count = 1 Then
            Dim index As Integer = ListView1.FocusedItem.Index
            FSGUI.WindowState = FormWindowState.Normal
            Me.Location = Screen.AllScreens(index).Bounds.Location
            DoorControl.Location = Screen.AllScreens(index).Bounds.Location
            FSGUI.Location = Screen.AllScreens(index).Bounds.Location
            FSGUI.WindowState = FormWindowState.Maximized
            FSGUI.TopMost = True
            Me.Visible = False
            FSGUI.Enabled = True
        Else
            FSGUI.TopMost = True
            Me.Visible = False
            FSGUI.Enabled = True
        End If

    End Sub

    Private Sub ChangeDisplay_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
    End Sub

    Private Sub ChangeDisplay_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ChangeDisplay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            Button1.PerformClick()
            e.Handled = True
        End If
    End Sub

    Private Sub ChangeDisplay_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        FSGUI.TopMost = True
        Me.Visible = False
        FSGUI.Enabled = True
    End Sub
End Class