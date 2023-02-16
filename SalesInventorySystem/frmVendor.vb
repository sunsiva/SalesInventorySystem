﻿Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class frmVendor
    Dim rdr As SqlDataReader = Nothing
    Dim con As SqlConnection = Nothing
    Dim cmd As SqlCommand = Nothing
    Dim cs As String = "server=DESKTOP-G8EVIRI; Database=SIM;uid=sa;pwd=Myla"

    Sub clear()
        txtAddress.Text = ""
        txtCity.Text = ""
        txtEmail.Text = ""
        txtFaxNo.Text = ""
        txtName.Text = ""
        txtLandmark.Text = ""

        txtMobileNo.Text = ""
        txtNotes.Text = ""
        txtPhone.Text = ""
        txtVendorID.Text = ""
        txtZipCode.Text = ""
        cmbState.Text = ""
    End Sub
    Private Sub NewRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewRecord.Click
        clear()
        Save.Enabled = True
        Update_Record.Enabled = False
        Delete.Enabled = False
        txtName.Focus()
    End Sub
    Private Sub auto()

        txtVendorID.Text = "V-" & GetUniqueKey(6)
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
        If Len(Trim(txtName.Text)) = 0 Then
            MessageBox.Show("Please enter name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtName.Focus()
            Exit Sub
        End If

        If Len(Trim(txtAddress.Text)) = 0 Then
            MessageBox.Show("Please enter address", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtAddress.Focus()
            Exit Sub
        End If
        If Len(Trim(txtCity.Text)) = 0 Then
            MessageBox.Show("Please enter city", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtCity.Focus()
            Exit Sub
        End If
        If Len(Trim(cmbState.Text)) = 0 Then
            MessageBox.Show("Please select state", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbState.Focus()
            Exit Sub
        End If
        If Len(Trim(txtZipCode.Text)) = 0 Then
            MessageBox.Show("Please enter zip/post code", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtZipCode.Focus()
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
            Dim ct As String = "select VendorID from Vendor where VendorID=@find"

            cmd = New SqlCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New SqlParameter("@find", System.Data.SqlDbType.NChar, 20, "VendorID"))
            cmd.Parameters("@find").Value = txtVendorID.Text
            rdr = cmd.ExecuteReader()

            If rdr.Read Then
                MessageBox.Show("Vendor ID Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                If Not rdr Is Nothing Then
                    rdr.Close()
                End If

            Else



                con = New SqlConnection(cs)
                con.Open()

                Dim cb As String = "insert into Vendor(VendorID,name,address,landmark,city,state,zipcode,Phone,email,mobileno,faxno,notes) VALUES (@d1,@d2,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13)"

                cmd = New SqlCommand(cb)

                cmd.Connection = con

                cmd.Parameters.Add(New SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "VendorID"))
                cmd.Parameters.Add(New SqlParameter("@d2", System.Data.SqlDbType.NChar, 100, "name"))



                cmd.Parameters.Add(New SqlParameter("@d4", System.Data.SqlDbType.VarChar, 250, "address"))

                cmd.Parameters.Add(New SqlParameter("@d5", System.Data.SqlDbType.VarChar, 250, "landmark"))

                cmd.Parameters.Add(New SqlParameter("@d6", System.Data.SqlDbType.NChar, 50, "city"))

                cmd.Parameters.Add(New SqlParameter("@d7", System.Data.SqlDbType.NChar, 50, "state"))

                cmd.Parameters.Add(New SqlParameter("@d8", System.Data.SqlDbType.NChar, 10, "zipcode"))

                cmd.Parameters.Add(New SqlParameter("@d9", System.Data.SqlDbType.NChar, 15, "phone"))

                cmd.Parameters.Add(New SqlParameter("@d10", System.Data.SqlDbType.VarChar, 150, "email"))

                cmd.Parameters.Add(New SqlParameter("@d11", System.Data.SqlDbType.NChar, 15, "mobileno"))

                cmd.Parameters.Add(New SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "faxno"))

                cmd.Parameters.Add(New SqlParameter("@d13", System.Data.SqlDbType.VarChar, 250, "notes"))


                cmd.Parameters("@d1").Value = txtVendorID.Text
                cmd.Parameters("@d2").Value = txtName.Text



                cmd.Parameters("@d4").Value = txtAddress.Text

                cmd.Parameters("@d5").Value = txtLandmark.Text

                cmd.Parameters("@d6").Value = txtCity.Text
                cmd.Parameters("@d7").Value = cmbState.Text

                cmd.Parameters("@d8").Value = txtZipCode.Text


                cmd.Parameters("@d9").Value = txtPhone.Text

                cmd.Parameters("@d10").Value = txtEmail.Text


                cmd.Parameters("@d11").Value = txtMobileNo.Text

                cmd.Parameters("@d12").Value = txtFaxNo.Text


                cmd.Parameters("@d13").Value = txtNotes.Text

                cmd.ExecuteReader()
                MessageBox.Show("Successfully saved", "Vendor Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub Update_Record_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update_Record.Click
        Try

            con = New SqlConnection(cs)
            con.Open()

            Dim cb As String = "update Vendor set name=@d2,address=@d4,landmark=@d5,city=@d6,state=@d7,zipcode=@d8,Phone=@d9,email=@d10,mobileno=@d11,faxno=@d12,notes=@d13 where VendorID=@d1"

            cmd = New SqlCommand(cb)

            cmd.Connection = con

            cmd.Parameters.Add(New SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "VendorID"))
            cmd.Parameters.Add(New SqlParameter("@d2", System.Data.SqlDbType.NChar, 100, "name"))



            cmd.Parameters.Add(New SqlParameter("@d4", System.Data.SqlDbType.VarChar, 250, "address"))

            cmd.Parameters.Add(New SqlParameter("@d5", System.Data.SqlDbType.VarChar, 250, "landmark"))

            cmd.Parameters.Add(New SqlParameter("@d6", System.Data.SqlDbType.NChar, 50, "city"))

            cmd.Parameters.Add(New SqlParameter("@d7", System.Data.SqlDbType.NChar, 50, "state"))

            cmd.Parameters.Add(New SqlParameter("@d8", System.Data.SqlDbType.NChar, 10, "zipcode"))

            cmd.Parameters.Add(New SqlParameter("@d9", System.Data.SqlDbType.NChar, 15, "phone"))

            cmd.Parameters.Add(New SqlParameter("@d10", System.Data.SqlDbType.VarChar, 150, "email"))

            cmd.Parameters.Add(New SqlParameter("@d11", System.Data.SqlDbType.NChar, 15, "mobileno"))

            cmd.Parameters.Add(New SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "faxno"))

            cmd.Parameters.Add(New SqlParameter("@d13", System.Data.SqlDbType.VarChar, 250, "notes"))


            cmd.Parameters("@d1").Value = txtVendorID.Text
            cmd.Parameters("@d2").Value = txtName.Text


            cmd.Parameters("@d4").Value = txtAddress.Text

            cmd.Parameters("@d5").Value = txtLandmark.Text

            cmd.Parameters("@d6").Value = txtCity.Text
            cmd.Parameters("@d7").Value = cmbState.Text

            cmd.Parameters("@d8").Value = txtZipCode.Text


            cmd.Parameters("@d9").Value = txtPhone.Text

            cmd.Parameters("@d10").Value = txtEmail.Text


            cmd.Parameters("@d11").Value = txtMobileNo.Text

            cmd.Parameters("@d12").Value = txtFaxNo.Text


            cmd.Parameters("@d13").Value = txtNotes.Text

            cmd.ExecuteReader()
            MessageBox.Show("Successfully updated", "Vendor Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Update_Record.Enabled = False
            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Close()




        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub delete_records()
        Try



            Dim RowsAffected As Integer = 0

           
            con = New SqlConnection(cs)

            con.Open()


            Dim cq As String = "delete from vendor where VendorID=@DELETE1;"


            cmd = New SqlCommand(cq)

            cmd.Connection = con

            cmd.Parameters.Add(New SqlParameter("@DELETE1", System.Data.SqlDbType.NChar, 20, "vendorID"))


            cmd.Parameters("@DELETE1").Value = Trim(txtVendorID.Text)
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected > 0 Then

                MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information)

                clear()
                txtName.Focus()
                Update_Record.Enabled = False
                Delete.Enabled = False
            Else
                MessageBox.Show("No record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information)

                clear()
                txtName.Focus()
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



            If MessageBox.Show("Do you really want to delete the record?", "Vendor Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                delete_records()



            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Me.clear()
        frmVendorRecords.DataGridView1.DataSource = Nothing
        frmVendorRecords.Show()
    End Sub

    Private Sub frmVendor_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Hide()
        FrmMain.Show()
    End Sub

    Private Sub frmVendor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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

    Private Sub txtPhone_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhone.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then

            e.Handled = True

        End If
    End Sub

    Private Sub txtFaxNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFaxNo.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then

            e.Handled = True

        End If
    End Sub
End Class