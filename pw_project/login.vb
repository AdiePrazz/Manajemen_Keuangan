Imports MySql.Data.MySqlClient
Public Class login
    Public dbconn, dbconn1 As New MySqlConnection
    Public sql As String
    Public dbcomm As MySqlCommand
    Public dbread As MySqlDataReader
    Dim stat, logstat As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sql = "SELECT `login`.* FROM `login` WHERE login.username = '" & TextBox1.Text & "' AND login.password = '" & TextBox2.Text & "'"

        Try
            dbcomm = New MySqlCommand(sql, dbconn)
            dbread = dbcomm.ExecuteReader()

            While dbread.Read
                If TextBox1.Text = dbread("username") And TextBox2.Text = dbread("password") Then
                    logstat = "green"
                    stat = dbread("status")
                    If stat = "admin" Then
                        MsgBox(" Anda Login sebagai Admin ")
                        Me.Hide()
                        admin_main.Show()

                    ElseIf stat = "karyawan" Then
                        MsgBox(" Anda Login sebagai Karyawan ")
                        Me.Hide()
                        karyawan_main.Show()
                        karyawan_main.Label2.Text = "Hi," + " " + Me.TextBox1.Text
                    End If
                Else
                    MsgBox("Login salah, periksa kembali username dan password")
                    TextBox1.Focus()
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                    Me.Close()
                End If
            End While
            'If logstat = "red" Then
            'MsgBox("Username atau password anda salah")
            'End If
            'dbread.Close()
        Catch ex As Exception
            MsgBox("Login salah, periksa kembali username dan password. ")
            dbread.Close()
            Exit Sub
        End Try

    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        logstat = "red"
        dbconn = New MySqlConnection("Data Source=localhost;user id=root;database=manajemen_keuangan")
        Try
            dbconn.Open()
        Catch ex As Exception
            MsgBox("Error in connection, please check Database and connection server.")
        End Try
    End Sub
End Class