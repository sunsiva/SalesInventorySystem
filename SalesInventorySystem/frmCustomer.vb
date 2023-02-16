﻿Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class frmCustomer
    Dim rdr As SqlDataReader = Nothing
    Dim con As SqlConnection = Nothing
    Dim cmd As SqlCommand = Nothing
    Dim cs As String = "server=DESKTOP-G8EVIRI; Database=SIM;uid=sa;pwd=Myla"


    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            S_name.Text = B_name.Text

            S_Address.Text = B_Address.Text
            S_City.Text = B_City.Text
            S_Landmark.Text = B_Landmark.Text
            S_State.Text = B_State.Text
            S_ZipCode.Text = B_ZipCode.Text
            S_name.ReadOnly = True

            S_Address.ReadOnly = True
            S_Landmark.ReadOnly = True
            S_City.ReadOnly = True
            S_State.Enabled = False
            S_ZipCode.ReadOnly = True
            txtPhone.Focus()
        Else
            S_name.Text = ""
            S_Address.Text = ""
            S_City.Text = ""
            S_Landmark.Text = ""
            S_State.Text = ""
            S_ZipCode.Text = ""

        End If
    End Sub

    Private Sub auto()
        txtCustomerNo.Text = "SD-" & GetUniqueKey(6)

    End Sub
    Public Shared Function GetUniqueKey(ByVal maxSize As Integer) As String
        Dim chars As Char() = New Char(61) {}
        chars = "123456789".ToCharArray()
        Dim data As Byte() = New Byte(0) {}
        Dim crypto As New RNGCryptoServiceProvider()
        crypto.GetNonZeroBytes(data)
        data = New Byte(maxSize - 1) {}
        crypto.GetNonZeroBytes(data)
        Dim result As New StringBuilder(maxSize)
        For Each b As Byte In data
            result.Append(chars(b Mod (chars.Length)))
        Next
        Return result.ToString()
    End Function
    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        If Len(Trim(B_name.Text)) = 0 Then
            MessageBox.Show("Please enter name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            B_name.Focus()
            Exit Sub
        End If

        If Len(Trim(B_Address.Text)) = 0 Then
            MessageBox.Show("Please enter address", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            B_Address.Focus()
            Exit Sub
        End If
        If Len(Trim(B_City.Text)) = 0 Then
            MessageBox.Show("Please enter city", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            B_City.Focus()
            Exit Sub
        End If
        If Len(Trim(B_State.Text)) = 0 Then
            MessageBox.Show("Please select state", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            B_State.Focus()
            Exit Sub
        End If
        If Len(Trim(B_ZipCode.Text)) = 0 Then
            MessageBox.Show("Please enter zip/post code", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            B_ZipCode.Focus()
            Exit Sub
        End If
        If Len(Trim(S_name.Text)) = 0 Then
            MessageBox.Show("Please enter name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            S_name.Focus()
            Exit Sub
        End If

        If Len(Trim(S_Address.Text)) = 0 Then
            MessageBox.Show("Please enter address", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            S_Address.Focus()
            Exit Sub
        End If
        If Len(Trim(S_City.Text)) = 0 Then
            MessageBox.Show("Please enter city", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            S_City.Focus()
            Exit Sub
        End If
        If Len(Trim(S_State.Text)) = 0 Then
            MessageBox.Show("Please select state", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            S_State.Focus()
            Exit Sub
        End If
        If Len(Trim(S_ZipCode.Text)) = 0 Then
            MessageBox.Show("Please enter zip/post code", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            S_ZipCode.Focus()
            Exit Sub
        End If

        If Len(Trim(txtMobileNo.Text)) = 0 Then
            MessageBox.Show("Please enter mobile no.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtMobileNo.Focus()
            Exit Sub
        End If

        Try
            auto()
            con = New SqlConnection(cs)
            con.Open()
            Dim ct As String = "select customerno from customer where customerno=@find"

            cmd = New SqlCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New SqlParameter("@find", System.Data.SqlDbType.NChar, 20, "customerno"))
            cmd.Parameters("@find").Value = txtCustomerNo.Text
            rdr = cmd.ExecuteReader()

            If rdr.Read Then
                MessageBox.Show("Distributor ID Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                If Not rdr Is Nothing Then
                    rdr.Close()
                End If

            Else



                con = New SqlConnection(cs)
                con.Open()

                Dim cb As String = "insert into customer(B_name,b_address,b_landmark,b_city,b_state,b_zipcode,s_name,s_address,s_landmark,s_city,s_state,s_zipcode,customerNo,Phone,email,mobileno,faxno,notes) VALUES (@d1,@d3,@d4,@d5,@d6,@d7,@d8,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19,@d20)"

                cmd = New SqlCommand(cb)

                cmd.Connection = con

                cmd.Parameters.Add(New SqlParameter("@d1", System.Data.SqlDbType.NChar, 100, "b_name"))



                cmd.Parameters.Add(New SqlParameter("@d3", System.Data.SqlDbType.VarChar, 250, "b_address"))

                cmd.Parameters.Add(New SqlParameter("@d4", System.Data.SqlDbType.VarChar, 250, "b_landmark"))

                cmd.Parameters.Add(New SqlParameter("@d5", System.Data.SqlDbType.NChar, 50, "b_city"))

                cmd.Parameters.Add(New SqlParameter("@d6", System.Data.SqlDbType.NChar, 50, "b_state"))

                cmd.Parameters.Add(New SqlParameter("@d7", System.Data.SqlDbType.NChar, 10, "b_zipcode"))

                cmd.Parameters.Add(New SqlParameter("@d8", System.Data.SqlDbType.NChar, 100, "s_name"))


                cmd.Parameters.Add(New SqlParameter("@d10", System.Data.SqlDbType.VarChar, 250, "s_address"))

                cmd.Parameters.Add(New SqlParameter("@d11", System.Data.SqlDbType.VarChar, 250, "s_landmark"))

                cmd.Parameters.Add(New SqlParameter("@d12", System.Data.SqlDbType.NChar, 50, "S_city"))

                cmd.Parameters.Add(New SqlParameter("@d13", System.Data.SqlDbType.NChar, 50, "s_state"))

                cmd.Parameters.Add(New SqlParameter("@d14", System.Data.SqlDbType.NChar, 10, "s_zipcode"))

                cmd.Parameters.Add(New SqlParameter("@d15", System.Data.SqlDbType.NChar, 20, "customerno"))

                cmd.Parameters.Add(New SqlParameter("@d16", System.Data.SqlDbType.NChar, 15, "phone"))

                cmd.Parameters.Add(New SqlParameter("@d17", System.Data.SqlDbType.VarChar, 150, "email"))

                cmd.Parameters.Add(New SqlParameter("@d18", System.Data.SqlDbType.NChar, 15, "mobileno"))

                cmd.Parameters.Add(New SqlParameter("@d19", System.Data.SqlDbType.NChar, 15, "faxno"))

                cmd.Parameters.Add(New SqlParameter("@d20", System.Data.SqlDbType.VarChar, 250, "notes"))

                cmd.Parameters("@d1").Value = B_name.Text



                cmd.Parameters("@d3").Value = B_Address.Text

                cmd.Parameters("@d4").Value = B_Landmark.Text

                cmd.Parameters("@d5").Value = B_City.Text
                cmd.Parameters("@d6").Value = B_State.Text

                cmd.Parameters("@d7").Value = B_ZipCode.Text

                cmd.Parameters("@d8").Value = S_name.Text



                cmd.Parameters("@d10").Value = S_Address.Text


                cmd.Parameters("@d11").Value = S_Landmark.Text
                cmd.Parameters("@d12").Value = S_City.Text
                cmd.Parameters("@d13").Value = S_State.Text

                cmd.Parameters("@d14").Value = S_ZipCode.Text

                cmd.Parameters("@d15").Value = txtCustomerNo.Text

                cmd.Parameters("@d16").Value = txtPhone.Text

                cmd.Parameters("@d17").Value = txtEmail.Text


                cmd.Parameters("@d18").Value = txtMobileNo.Text

                cmd.Parameters("@d19").Value = txtFaxNo.Text


                cmd.Parameters("@d20").Value = txtNotes.Text

                cmd.ExecuteReader()
                MessageBox.Show("Successfully saved", "Distributor Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Save.Enabled = False
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If

                con.Close()
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub clear()
        B_name.Text = ""

        B_Address.Text = ""
        B_City.Text = ""
        B_Landmark.Text = ""
        B_State.Text = ""
        B_ZipCode.Text = ""
        S_name.Text = ""

        S_Address.Text = ""
        S_City.Text = ""
        S_Landmark.Text = ""
        S_State.Text = ""
        S_ZipCode.Text = ""
        txtCustomerNo.Text = ""
        txtEmail.Text = ""
        txtFaxNo.Text = ""
        txtMobileNo.Text = ""
        txtPhone.Text = ""
        txtNotes.Text = ""
    End Sub

    Private Sub frmCustomer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Hide()
        FrmMain.Show()
    End Sub
    Private Sub frmCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub



    Private Sub NewRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewRecord.Click
        clear()
        Update_Record.Enabled = False
        Delete.Enabled = False
        Save.Enabled = True
        CheckBox1.Checked = False
        S_name.ReadOnly = False

        S_Address.ReadOnly = False
        S_Landmark.ReadOnly = False
        S_City.ReadOnly = False
        S_State.Enabled = True
        S_ZipCode.ReadOnly = False
        B_name.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.clear()
        frmCustomersRecord3.DataGridView1.DataSource = Nothing
        frmCustomersRecord3.DataGridView2.DataSource = Nothing
        frmCustomersRecord3.txtName.Text = ""


        frmCustomersRecord3.Show()
    End Sub

    Private Sub Update_Record_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update_Record.Click
        Try
            con = New SqlConnection(cs)
            con.Open()

            Dim cb As String = "update customer set B_name=@d1,b_address=@d3,b_landmark=@d4,b_city=@d5,b_state=@d6,b_zipcode=@d7,s_name=@d8,s_address=@d10,s_landmark=@d11,s_city=@d12,s_state=@d13,s_zipcode=@d14,Phone=@d16,email=@d17,mobileno=@d18,faxno=@d19,notes=@d20 where customerno=@d15"

            cmd = New SqlCommand(cb)

            cmd.Connection = con

            cmd.Parameters.Add(New SqlParameter("@d1", System.Data.SqlDbType.NChar, 100, "b_name"))

            cmd.Parameters.Add(New SqlParameter("@d3", System.Data.SqlDbType.VarChar, 250, "b_address"))

            cmd.Parameters.Add(New SqlParameter("@d4", System.Data.SqlDbType.VarChar, 250, "b_landmark"))

            cmd.Parameters.Add(New SqlParameter("@d5", System.Data.SqlDbType.NChar, 50, "b_city"))

            cmd.Parameters.Add(New SqlParameter("@d6", System.Data.SqlDbType.NChar, 50, "b_state"))

            cmd.Parameters.Add(New SqlParameter("@d7", System.Data.SqlDbType.NChar, 10, "b_zipcode"))

            cmd.Parameters.Add(New SqlParameter("@d8", System.Data.SqlDbType.NChar, 100, "s_name"))


            cmd.Parameters.Add(New SqlParameter("@d10", System.Data.SqlDbType.VarChar, 250, "s_address"))

            cmd.Parameters.Add(New SqlParameter("@d11", System.Data.SqlDbType.VarChar, 250, "s_landmark"))

            cmd.Parameters.Add(New SqlParameter("@d12", System.Data.SqlDbType.NChar, 50, "S_city"))

            cmd.Parameters.Add(New SqlParameter("@d13", System.Data.SqlDbType.NChar, 50, "s_state"))

            cmd.Parameters.Add(New SqlParameter("@d14", System.Data.SqlDbType.NChar, 10, "s_zipcode"))

            cmd.Parameters.Add(New SqlParameter("@d15", System.Data.SqlDbType.NChar, 20, "customerno"))

            cmd.Parameters.Add(New SqlParameter("@d16", System.Data.SqlDbType.NChar, 15, "phone"))

            cmd.Parameters.Add(New SqlParameter("@d17", System.Data.SqlDbType.VarChar, 150, "email"))

            cmd.Parameters.Add(New SqlParameter("@d18", System.Data.SqlDbType.NChar, 15, "mobileno"))

            cmd.Parameters.Add(New SqlParameter("@d19", System.Data.SqlDbType.NChar, 15, "faxno"))

            cmd.Parameters.Add(New SqlParameter("@d20", System.Data.SqlDbType.VarChar, 250, "notes"))

            cmd.Parameters("@d1").Value = B_name.Text


            cmd.Parameters("@d3").Value = B_Address.Text

            cmd.Parameters("@d4").Value = B_Landmark.Text

            cmd.Parameters("@d5").Value = B_City.Text
            cmd.Parameters("@d6").Value = B_State.Text

            cmd.Parameters("@d7").Value = B_ZipCode.Text

            cmd.Parameters("@d8").Value = S_name.Text

            cmd.Parameters("@d10").Value = S_Address.Text


            cmd.Parameters("@d11").Value = S_Landmark.Text
            cmd.Parameters("@d12").Value = S_City.Text
            cmd.Parameters("@d13").Value = S_State.Text

            cmd.Parameters("@d14").Value = S_ZipCode.Text

            cmd.Parameters("@d15").Value = txtCustomerNo.Text

            cmd.Parameters("@d16").Value = txtPhone.Text

            cmd.Parameters("@d17").Value = txtEmail.Text


            cmd.Parameters("@d18").Value = txtMobileNo.Text

            cmd.Parameters("@d19").Value = txtFaxNo.Text


            cmd.Parameters("@d20").Value = txtNotes.Text

            cmd.ExecuteReader()
            MessageBox.Show("Successfully updated", "Distributor Details", MessageBoxButtons.OK, MessageBoxIcon.Information)

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Close()

            Update_Record.Enabled = False

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtFaxNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFaxNo.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then

            e.Handled = True

        End If
    End Sub

    Private Sub txtPhone_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhone.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then

            e.Handled = True

        End If
    End Sub
    Private Sub delete_records()
        Try



            Dim RowsAffected As Integer = 0

            con = New SqlConnection(cs)

            con.Open()
            Dim ct As String = "select CustomerNo from BillInfo where customerNo=@find"


            cmd = New SqlCommand(ct)

            cmd.Connection = con
            cmd.Parameters.Add(New SqlParameter("@find", System.Data.SqlDbType.NChar, 20, "CustomerNo"))


            cmd.Parameters("@find").Value = txtCustomerNo.Text


            rdr = cmd.ExecuteReader()

            If rdr.Read Then
                MessageBox.Show("Unable to delete..Already in use", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                clear()
                B_name.Focus()
                Update_Record.Enabled = False
                Delete.Enabled = False


                If Not rdr Is Nothing Then
                    rdr.Close()
                End If
                Exit Sub
            End If
            con = New SqlConnection(cs)

            con.Open()
            Dim ct1 As String = "select CustomerNo from OrderInfo where customerNo=@find"


            cmd = New SqlCommand(ct1)

            cmd.Connection = con
            cmd.Parameters.Add(New SqlParameter("@find", System.Data.SqlDbType.NChar, 20, "CustomerNo"))


            cmd.Parameters("@find").Value = txtCustomerNo.Text


            rdr = cmd.ExecuteReader()

            If rdr.Read Then
                MessageBox.Show("Unable to delete..Already in use", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                clear()
                B_name.Focus()
                Update_Record.Enabled = False
                Delete.Enabled = False


                If Not rdr Is Nothing Then
                    rdr.Close()
                End If
                Exit Sub
            End If
            con = New SqlConnection(cs)

            con.Open()


            Dim cq As String = "delete from customer where CustomerNo=@DELETE1;"


            cmd = New SqlCommand(cq)

            cmd.Connection = con

            cmd.Parameters.Add(New SqlParameter("@DELETE1", System.Data.SqlDbType.NChar, 30, "CustomerNo"))


            cmd.Parameters("@DELETE1").Value = Trim(txtCustomerNo.Text)
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected > 0 Then

                MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information)

                clear()
                B_name.Focus()
                Update_Record.Enabled = False
                Delete.Enabled = False
            Else
                MessageBox.Show("No record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information)
                clear()
                B_name.Focus()
                Update_Record.Enabled = False
                Delete.Enabled = False



                If con.State = ConnectionState.Open Then

                    con.Close()
                End If

                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete.Click
        Try



            If MessageBox.Show("Do you really want to delete the record?", "Distributor Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                delete_records()



            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtEmail_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtEmail.Validating
        Dim rEMail As New System.Text.RegularExpressions.Regex("^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$")
        If txtEmail.Text.Length > 0 Then
            If Not rEMail.IsMatch(txtEmail.Text) Then
                MessageBox.Show("invalid email address", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                txtEmail.SelectAll()
                e.Cancel = True
            End If
        End If
    End Sub
End Class