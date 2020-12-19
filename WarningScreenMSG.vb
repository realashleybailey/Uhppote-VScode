Imports System.Runtime.InteropServices

Public Class WarningScreenMSG

    Private InitialStyle As Integer
    Dim PercentVisible As Decimal
    Private Sub WarningScreenMSG_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.TopMost = True

        InitialStyle = GetWindowLong(Me.Handle, -20)
        PercentVisible = 0.8
        SetWindowLong(Me.Handle, -20, InitialStyle Or &H80000 Or &H20)
        SetLayeredWindowAttributes(Me.Handle, 0, 255 * PercentVisible, &H2)

    End Sub

    <DllImport("user32.dll", EntryPoint:="GetWindowLong")> Public Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
    End Function
    <DllImport("user32.dll", EntryPoint:="SetWindowLong")> Public Shared Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
    End Function
    <DllImport("user32.dll", EntryPoint:="SetLayeredWindowAttributes")> Public Shared Function SetLayeredWindowAttributes(ByVal hWnd As IntPtr, ByVal crKey As Integer, ByVal alpha As Byte, ByVal dwFlags As Integer) As Boolean
    End Function
End Class