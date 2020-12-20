Imports System.Runtime.InteropServices

Public Class WarningScreen

    Private InitialStyle As Integer
    Dim PercentVisible As Decimal

    Function Start()
        Me.Visible = True
        WarningScreenMSG.Visible = True
    End Function
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If Me.Opacity = 0.65 Then
            WarningScreenMSG.BackColor = Color.White
            Me.Opacity = 0.00
        Else
            WarningScreenMSG.BackColor = Color.Red
            Me.Opacity = 0.65
        End If

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Label1.Text = Label1.Text + 1

        If Label1.Text = 5 Then
            Timer1.Stop()
            Timer2.Stop()
            Label1.Text = 1
            Me.Opacity = 0.65
            WarningScreenMSG.Visible = False
            WarningScreenMSG.BackColor = Color.Red
            Me.Visible = False
        End If
    End Sub

    Private Sub WarningScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        InitialStyle = GetWindowLong(Me.Handle, -20)
        PercentVisible = 0.8

        SetWindowLong(Me.Handle, -20, InitialStyle Or &H80000 Or &H20)

        SetLayeredWindowAttributes(Me.Handle, 0, 255 * PercentVisible, &H2)



    End Sub

    Private Sub WarningScreen_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        If Me.Visible = True Then
            Timer1.Start()
            Timer2.Start()
        End If
    End Sub

    <DllImport("user32.dll", EntryPoint:="GetWindowLong")> Public Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
    End Function
    <DllImport("user32.dll", EntryPoint:="SetWindowLong")> Public Shared Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
    End Function
    <DllImport("user32.dll", EntryPoint:="SetLayeredWindowAttributes")> Public Shared Function SetLayeredWindowAttributes(ByVal hWnd As IntPtr, ByVal crKey As Integer, ByVal alpha As Byte, ByVal dwFlags As Integer) As Boolean
    End Function
End Class