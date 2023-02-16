﻿Imports System.Data.SqlClient
Imports System.Data
Public Class frmRegistration
    Dim rdr As SqlDataReader = Nothing
    Dim dtable As DataTable
    Dim con As SqlConnection = Nothing
    Dim adp As SqlDataAdapter
    Dim ds As DataSet
    Dim cmd As SqlCommand = Nothing
    Dim dt As New DataTable
    Dim cs As String = "server=DESKTOP-G8EVIRI; Database=SIM;uid=sa;pwd=Myla"


    Private Sub Registration_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Hide()
        FrmMain.Show()
    End Sub
    Sub fillcombo()

        Try

            Dim CN As New SqlConnection(cs)

            CN.Open()
            adp = New SqlDataAdapter()
            adp.SelectCommand = New SqlCommand("SELECT distinct  RTRIM(username) FROM registration", CN)
            ds = New DataSet("ds")

            adp.Fill(ds)
            dtable = ds.Tables(0)
            UserName.Items.Clear()

            For Each drow As DataRow In dtable.Rows
                UserName.Items.Add(drow(0).ToString())
                'DocName.SelectedIndex = -1
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Registration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        fillcombo()
        Update_record.Enabled = False
        Delete.Enabled = False
    End Sub

    Private Sub NewRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewRecord.Click
        UserName.Text = ""
        Password.Text = ""
        ContactNo.Text = ""
        txtName.Text = ""
        Save.Enabled = True
        Update_record.Enabled = False
        Delete.Enabled = False
    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        If Len(Trim(UserName.Text)) = 0 Then
            MessageBox.Show("Please enter user name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            UserName.Focus()
            Exit Sub
        End If
        If Len(Trim(Password.Text)) = 0 Then
            MessageBox.Show("Please enter password", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Password.Focus()
            Exit Sub
        End If
        If Len(Trim(txtName.Text)) = 0 Then
            MessageBox.Show("Please enter name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtName.Focus()
            Exit Sub
        End If
        If Len(Trim(ContactNo.Text)) = 0 Then
            MessageBox.Show("Please enter contact no.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ContactNo.Focus()
            Exit Sub
        End If
        Try
            con = New SqlConnection(cs)
            con.Open()
            Dim ct As String = "select username from registration where username=@find"

            cmd = New SqlCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New SqlParameter("@find", System.Data.SqlDbType.NChar, 30, "username"))
            cmd.Parameters("@find").Value = UserName.Text
            rdr = cmd.ExecuteReader()

            If rdr.Read Then
                MessageBox.Show("User Name Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                UserName.Text = ""



                If Not rdr Is Nothing Then
                    rdr.Close()
                End If

            Else



                con = New SqlConnection(cs)
                con.Open()

                Dim cb As String = "insert into registration(username,password,name,contactno) VALUES (@d1,@d2,@d3,@d4)"

                cmd = New SqlCommand(cb)

                cmd.Connection = con

                cmd.Parameters.Add(New SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "username"))

                cmd.Parameters.Add(New SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "password"))

                cmd.Parameters.Add(New SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "name"))

                cmd.Parameters.Add(New SqlParameter("@d4", System.Data.SqlDbType.NChar, 15, "contactno"))


                cmd.Parameters("@d1").Value = Trim(UserName.Text)

                cmd.Parameters("@d2").Value = Trim(Password.Text)

                cmd.Parameters("@d3").Value = Trim(txtName.Text)

                cmd.Parameters("@d4").Value = Trim(ContactNo.Text)




                cmd.ExecuteReader()



                If con.State = ConnectionState.Open Then
                    con.Close()
                End If


                con = New SqlConnection(cs)

                con.Open()



                Dim coz As String = "insert into users(UserName,password) VALUES (@INSERT1,@INSERT2)"

                cmd =
                New SqlCommand(coz)


                cmd.Connection = con

                cmd.Parameters.Add(
                New SqlParameter("@INSERT1", System.Data.SqlDbType.NChar, 30, "Username"))

                cmd.Parameters.Add(
                New SqlParameter("@INSERT2", System.Data.SqlDbType.NChar, 30, "password"))


                cmd.Parameters("@INSERT1").Value = UserName.Text
                cmd.Parameters("@INSERT2").Value = Password.Text




                cmd.ExecuteReader()


                MessageBox.Show("Successfully registered", "User", MessageBoxButtons.OK, MessageBoxIcon.Information)



                Save.Enabled = False

                fillcombo()
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub




    Private Sub delete_records()
        Try



            Dim RowsAffected As Integer = 0

            con = New SqlConnection(cs)

            con.Open()


            Dim cq As String = "delete from registration where username=@DELETE1;"


            cmd = New SqlCommand(cq)

            cmd.Connection = con

            cmd.Parameters.Add(New SqlParameter("@DELETE1", System.Data.SqlDbType.NChar, 30, "username"))


            cmd.Parameters("@DELETE1").Value = Trim(UserName.Text)
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected > 0 Then

                MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
               
            Else
                MsgBox("sorry no record deleted")
                UserName.Text = ""
                Password.Text = ""
                ContactNo.Text = ""
                txtName.Text = ""

                cmd.ExecuteReader()
                If con.State = ConnectionState.Open Then

                    con.Close()
                End If

                con.Close()
            End If

            con = New SqlConnection(cs)
            con.Open()


            Dim cq1 As String = "delete from users where username=@DELETE1;"

            Dim cmd1 As SqlCommand = Nothing
            cmd1 = New SqlCommand(cq1)

            cmd1.Connection = con

            cmd1.Parameters.Add(New SqlParameter("@DELETE1", System.Data.SqlDbType.NChar, 30, "Username"))


            cmd1.Parameters("@DELETE1").Value = UserName.Text
            cmd1.ExecuteReader()
            If con.State = ConnectionState.Open Then

                con.Close()
            End If

            con.Close()
            UserName.Text = ""
            Password.Text = ""
            ContactNo.Text = ""
            txtName.Text = ""

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Update_record_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update_record.Click
        Try

            If UserName.Text = "" Then
                MessageBox.Show("Please select user name", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            con = New SqlConnection(cs)
            con.Open()

            Dim cb As String = "update registration set password=@d2,name=@d3,contactno=@d4 where username=@d1"

            cmd = New SqlCommand(cb)

            cmd.Connection = con

            cmd.Parameters.Add(New SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "username"))

            cmd.Parameters.Add(New SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "password"))

            cmd.Parameters.Add(New SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "name"))

            cmd.Parameters.Add(New SqlParameter("@d4", System.Data.SqlDbType.NChar, 15, "contactno"))


            cmd.Parameters("@d1").Value = Trim(UserName.Text)

            cmd.Parameters("@d2").Value = Trim(Password.Text)

            cmd.Parameters("@d3").Value = Trim(txtName.Text)

            cmd.Parameters("@d4").Value = Trim(ContactNo.Text)




            cmd.ExecuteReader()



            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con = New SqlConnection(cs)

            con.Open()



            Dim coz As String = "update users set password=@d2 where username=@d1"

            cmd =
            New SqlCommand(coz)


            cmd.Connection = con

            cmd.Parameters.Add(
            New SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "username"))

            cmd.Parameters.Add(
            New SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "password"))


            cmd.Parameters("@d1").Value = UserName.Text
            cmd.Parameters("@d2").Value = Password.Text




            cmd.ExecuteReader()



            MessageBox.Show("Successfully Updated", "User details", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Update_record.Enabled = False

            fillcombo()
            con.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete.Click
        Try

            If UserName.Text = "" Then
                MessageBox.Show("Please select user name", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            If UserName.Items.Count > 0 Then
                If MsgBox("Do you really want to delete this record?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    delete_records()

                    Delete.Enabled = False
                    Update_record.Enabled = False
                    fillcombo()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ContactNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then

            e.Handled = True

        End If
    End Sub

    Private Sub UserName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 65) _
Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 90) _
And (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) _
Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            'space accepted
            If (Microsoft.VisualBasic.Asc(e.KeyChar) <> 32) Then
                e.Handled = True
            End If
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then

            e.Handled = False
        End If
    End Sub

    Private Sub GetDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetDetails.Click
        Me.Hide()
        frmRegistrationDetails.Show()
    End Sub

    Private Sub DSE_ID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserName.SelectedIndexChanged
        Try
            Delete.Enabled = True
            Update_record.Enabled = True

            con = New SqlConnection(cs)

            con.Open()


            Dim ct As String = "select * from registration where username=@find"


            cmd = New SqlCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New SqlParameter("@find", System.Data.SqlDbType.NChar, 30, "username"))
            cmd.Parameters("@find").Value = Trim(UserName.Text)
            rdr = cmd.ExecuteReader()
            If rdr.Read Then


                Password.Text = Trim(rdr.GetString(1))
                txtName.Text = Trim(rdr.GetString(2))
                ContactNo.Text = Trim(rdr.GetString(3))


            End If


            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
End Class