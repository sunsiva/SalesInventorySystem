﻿Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel
Public Class frmSalesRecord1
    Dim rdr As SqlDataReader = Nothing
    Dim dtable As DataTable
    Dim con As SqlConnection = Nothing
    Dim adp As SqlDataAdapter
    Dim ds As DataSet
    Dim cmd As SqlCommand = Nothing
    Dim dt As New DataTable
    Dim cs As String = "server=DESKTOP-G8EVIRI; Database=SIM;uid=sa;pwd=Myla"

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        DataGridView1.DataSource = Nothing
        dtpInvoiceDateFrom.Text = Today
        dtpInvoiceDateTo.Text = Today
        GroupBox3.Visible = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            GroupBox3.Visible = True
            con = New SqlConnection(cs)

            con.Open()
            cmd = New SqlCommand("SELECT rtrim(invoiceNo)[Invoice No.],rtrim(BillingDate)[Invoice Date],rtrim(CustomerNo)[Distributor ID],rtrim(CustomerName)[Distributor Name],rtrim(GrandTotal)[Grand Total],rtrim(TotalPayment)[Total Payment],rtrim(PaymentDue)[Payment Due] from billinfo where BillingDate between @date1 and @date2 order by BillingDate", con)
            cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, "BillingDate").Value = dtpInvoiceDateFrom.Value.Date
            cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, "BillingDate").Value = dtpInvoiceDateTo.Value.Date


            Dim myDA As SqlDataAdapter = New SqlDataAdapter(cmd)

            Dim myDataSet As DataSet = New DataSet()

            myDA.Fill(myDataSet, "BillInfo")

            DataGridView1.DataSource = myDataSet.Tables("Billinfo").DefaultView
            Dim sum As Int64 = 0
            Dim sum1 As Int64 = 0
            Dim sum2 As Int64 = 0


            For Each r As DataGridViewRow In Me.DataGridView1.Rows
                sum = sum + r.Cells(4).Value
                sum1 = sum1 + r.Cells(5).Value
                sum2 = sum2 + r.Cells(6).Value
            Next
            TextBox1.Text = sum
            TextBox2.Text = sum1
            TextBox3.Text = sum2

            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    Sub fillCustomerName()

        Try

            Dim CN As New SqlConnection(cs)

            CN.Open()
            adp = New SqlDataAdapter()
            adp.SelectCommand = New SqlCommand("SELECT distinct  RTRIM(CustomerName) FROM Billinfo", CN)
            ds = New DataSet("ds")

            adp.Fill(ds)
            dtable = ds.Tables(0)
            cmbCustomerName.Items.Clear()

            For Each drow As DataRow In dtable.Rows
                cmbCustomerName.Items.Add(drow(0).ToString())
                'DocName.SelectedIndex = -1
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Sub fillInvoiceNo()

        Try

            Dim CN As New SqlConnection(cs)

            CN.Open()
            adp = New SqlDataAdapter()
            adp.SelectCommand = New SqlCommand("SELECT distinct  RTRIM(invoiceno) FROM Billinfo", CN)
            ds = New DataSet("ds")

            adp.Fill(ds)
            dtable = ds.Tables(0)
            cmbInvoiceNo.Items.Clear()

            For Each drow As DataRow In dtable.Rows
                cmbInvoiceNo.Items.Add(drow(0).ToString())
                'DocName.SelectedIndex = -1
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub frmSalesRecord_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        fillInvoiceNo()
        fillCustomerName()
    End Sub



    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If DataGridView1.RowCount = Nothing Then
            MessageBox.Show("Sorry nothing to export into excel sheet.." & vbCrLf & "Please retrieve data in datagridview", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim rowsTotal, colsTotal As Short
        Dim I, j, iC As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Excel.Application

        Try
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Add
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets(1), Excel.Worksheet)
            xlApp.Visible = True

            rowsTotal = DataGridView1.RowCount - 1
            colsTotal = DataGridView1.Columns.Count - 1
            With excelWorksheet
                .Cells.Select()
                .Cells.Delete()
                For iC = 0 To colsTotal
                    .Cells(1, iC + 1).Value = DataGridView1.Columns(iC).HeaderText
                Next
                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + 2, j + 1).value = DataGridView1.Rows(I).Cells(j).Value
                    Next j
                Next I
                .Rows("1:1").Font.FontStyle = "Bold"
                .Rows("1:1").Font.Size = 12

                .Cells.Columns.AutoFit()
                .Cells.Select()
                .Cells.EntireColumn.AutoFit()
                .Cells(1, 1).Select()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'RELEASE ALLOACTED RESOURCES
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If DataGridView3.RowCount = Nothing Then
            MessageBox.Show("Sorry nothing to export into excel sheet.." & vbCrLf & "Please retrieve data in datagridview", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim rowsTotal, colsTotal As Short
        Dim I, j, iC As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Excel.Application

        Try
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Add
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets(1), Excel.Worksheet)
            xlApp.Visible = True

            rowsTotal = DataGridView3.RowCount - 1
            colsTotal = DataGridView3.Columns.Count - 1
            With excelWorksheet
                .Cells.Select()
                .Cells.Delete()
                For iC = 0 To colsTotal
                    .Cells(1, iC + 1).Value = DataGridView3.Columns(iC).HeaderText
                Next
                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + 2, j + 1).value = DataGridView3.Rows(I).Cells(j).Value
                    Next j
                Next I
                .Rows("1:1").Font.FontStyle = "Bold"
                .Rows("1:1").Font.Size = 12

                .Cells.Columns.AutoFit()
                .Cells.Select()
                .Cells.EntireColumn.AutoFit()
                .Cells(1, 1).Select()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'RELEASE ALLOACTED RESOURCES
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        If DataGridView4.RowCount = Nothing Then
            MessageBox.Show("Sorry nothing to export into excel sheet.." & vbCrLf & "Please retrieve data in datagridview", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim rowsTotal, colsTotal As Short
        Dim I, j, iC As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Excel.Application

        Try
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Add
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets(1), Excel.Worksheet)
            xlApp.Visible = True

            rowsTotal = DataGridView4.RowCount - 1
            colsTotal = DataGridView4.Columns.Count - 1
            With excelWorksheet
                .Cells.Select()
                .Cells.Delete()
                For iC = 0 To colsTotal
                    .Cells(1, iC + 1).Value = DataGridView4.Columns(iC).HeaderText
                Next
                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + 2, j + 1).value = DataGridView4.Rows(I).Cells(j).Value
                    Next j
                Next I
                .Rows("1:1").Font.FontStyle = "Bold"
                .Rows("1:1").Font.Size = 12

                .Cells.Columns.AutoFit()
                .Cells.Select()
                .Cells.EntireColumn.AutoFit()
                .Cells(1, 1).Select()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'RELEASE ALLOACTED RESOURCES
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub



    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        DataGridView3.DataSource = Nothing
        cmbCustomerName.Text = ""
        GroupBox4.Visible = False
    End Sub



    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        DataGridView4.DataSource = Nothing
        cmbInvoiceNo.Text = ""
        GroupBox5.Visible = False
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        DataGridView4.DataSource = Nothing
        cmbInvoiceNo.Text = ""
        GroupBox5.Visible = False
        DataGridView3.DataSource = Nothing
        cmbCustomerName.Text = ""
        GroupBox4.Visible = False
        DateTimePicker1.Text = Today
        DateTimePicker2.Text = Today
        DataGridView2.DataSource = Nothing
        GroupBox10.Visible = False
        DataGridView1.DataSource = Nothing
        dtpInvoiceDateFrom.Text = Today
        dtpInvoiceDateTo.Text = Today
        GroupBox3.Visible = False
    End Sub

    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        Try
            Dim dr As DataGridViewRow = DataGridView1.SelectedRows(0)
            Me.Hide()
            frmSales.Show()
            ' or simply use column name instead of index
            'dr.Cells["id"].Value.ToString();
            frmSales.txtInvoiceNo.Text = dr.Cells(0).Value.ToString()
            frmSales.txtTotal.Text = dr.Cells(4).Value.ToString()
            frmSales.txtTotalPayment.Text = dr.Cells(5).Value.ToString()
            frmSales.txtPaymentDue.Text = dr.Cells(6).Value.ToString()
            frmSales.btnUpdate.Enabled = True
            frmSales.Delete.Enabled = True
            frmSales.btnPrint.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub DataGridView3_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView3.RowHeaderMouseClick
        Try
            Dim dr As DataGridViewRow = DataGridView3.SelectedRows(0)
            Me.Hide()
            frmSales.Show()
            ' or simply use column name instead of index
            'dr.Cells["id"].Value.ToString();
            frmSales.txtInvoiceNo.Text = dr.Cells(0).Value.ToString()
            frmSales.txtTotal.Text = dr.Cells(4).Value.ToString()
            frmSales.txtTotalPayment.Text = dr.Cells(5).Value.ToString()
            frmSales.txtPaymentDue.Text = dr.Cells(6).Value.ToString()
            frmSales.btnUpdate.Enabled = True
            frmSales.Delete.Enabled = True
            frmSales.btnPrint.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridView4_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView4.RowHeaderMouseClick
        Try
            Dim dr As DataGridViewRow = DataGridView4.SelectedRows(0)
            Me.Hide()
            frmSales.Show()
            ' or simply use column name instead of index
            'dr.Cells["id"].Value.ToString();
            frmSales.txtInvoiceNo.Text = dr.Cells(0).Value.ToString()
            frmSales.txtTotal.Text = dr.Cells(4).Value.ToString()
            frmSales.txtTotalPayment.Text = dr.Cells(5).Value.ToString()
            frmSales.txtPaymentDue.Text = dr.Cells(6).Value.ToString()
            frmSales.btnUpdate.Enabled = True
            frmSales.Delete.Enabled = True
            frmSales.btnPrint.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub cmbCustomerName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCustomerName.SelectedIndexChanged
        Try

            GroupBox4.Visible = True
            con = New SqlConnection(cs)

            con.Open()
            cmd = New SqlCommand("SELECT rtrim(invoiceNo)[Invoice No.],rtrim(BillingDate)[Invoice Date],rtrim(CustomerNo)[Distributor ID],rtrim(CustomerName)[Distributor Name],rtrim(GrandTotal)[Grand Total],rtrim(TotalPayment)[Total Payment],rtrim(PaymentDue)[Payment Due] from billinfo where customername= '" & cmbCustomerName.Text & "' order by BillingDate", con)


            Dim myDA As SqlDataAdapter = New SqlDataAdapter(cmd)

            Dim myDataSet As DataSet = New DataSet()

            myDA.Fill(myDataSet, "BillInfo")

            DataGridView3.DataSource = myDataSet.Tables("Billinfo").DefaultView
            Dim sum As Int64 = 0
            Dim sum1 As Int64 = 0
            Dim sum2 As Int64 = 0


            For Each r As DataGridViewRow In Me.DataGridView3.Rows
                sum = sum + r.Cells(4).Value
                sum1 = sum1 + r.Cells(5).Value
                sum2 = sum2 + r.Cells(6).Value
            Next
            TextBox6.Text = sum
            TextBox5.Text = sum1
            TextBox4.Text = sum2

            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbInvoiceNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbInvoiceNo.SelectedIndexChanged
        Try

            GroupBox5.Visible = True
            con = New SqlConnection(cs)

            con.Open()
            cmd = New SqlCommand(" select rtrim(invoiceNo)[Invoice No.],rtrim(BillingDate)[Invoice Date],rtrim(CustomerNo)[Distributor ID],rtrim(CustomerName)[Distributor Name],rtrim(GrandTotal)[Grand Total],rtrim(TotalPayment)[Total Payment],rtrim(PaymentDue)[Payment Due] from billinfo where invoiceno = '" & cmbInvoiceNo.Text & "' order by BillingDate", con)


            Dim myDA As SqlDataAdapter = New SqlDataAdapter(cmd)

            Dim myDataSet As DataSet = New DataSet()

            myDA.Fill(myDataSet, "BillInfo")

            DataGridView4.DataSource = myDataSet.Tables("Billinfo").DefaultView
            Dim sum As Int64 = 0
            Dim sum1 As Int64 = 0
            Dim sum2 As Int64 = 0


            For Each r As DataGridViewRow In Me.DataGridView4.Rows
                sum = sum + r.Cells(4).Value
                sum1 = sum1 + r.Cells(5).Value
                sum2 = sum2 + r.Cells(6).Value
            Next
            TextBox9.Text = sum
            TextBox8.Text = sum1
            TextBox7.Text = sum2


            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try

            GroupBox10.Visible = True
            con = New SqlConnection(cs)

            con.Open()
            cmd = New SqlCommand("SELECT rtrim(invoiceNo)[Invoice No.],rtrim(BillingDate)[Invoice Date],rtrim(CustomerNo)[Distributor ID],rtrim(CustomerName)[Distributor Name],rtrim(GrandTotal)[Grand Total],rtrim(TotalPayment)[Total Payment],rtrim(PaymentDue)[Payment Due] from billinfo where BillingDate between @date1 and @date2 and PaymentDue > 0 order by BillingDate", con)
            cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, "BillingDate").Value = DateTimePicker2.Value.Date
            cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, "BillingDate").Value = DateTimePicker1.Value.Date


            Dim myDA As SqlDataAdapter = New SqlDataAdapter(cmd)

            Dim myDataSet As DataSet = New DataSet()

            myDA.Fill(myDataSet, "BillInfo")

            DataGridView2.DataSource = myDataSet.Tables("Billinfo").DefaultView
            Dim sum As Int64 = 0
            Dim sum1 As Int64 = 0
            Dim sum2 As Int64 = 0


            For Each r As DataGridViewRow In Me.DataGridView2.Rows
                sum = sum + r.Cells(4).Value
                sum1 = sum1 + r.Cells(5).Value
                sum2 = sum2 + r.Cells(6).Value
            Next
            TextBox12.Text = sum
            TextBox11.Text = sum1
            TextBox10.Text = sum2

            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If DataGridView2.RowCount = Nothing Then
            MessageBox.Show("Sorry nothing to export into excel sheet.." & vbCrLf & "Please retrieve data in datagridview", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim rowsTotal, colsTotal As Short
        Dim I, j, iC As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Excel.Application

        Try
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Add
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets(1), Excel.Worksheet)
            xlApp.Visible = True

            rowsTotal = DataGridView2.RowCount - 1
            colsTotal = DataGridView2.Columns.Count - 1
            With excelWorksheet
                .Cells.Select()
                .Cells.Delete()
                For iC = 0 To colsTotal
                    .Cells(1, iC + 1).Value = DataGridView2.Columns(iC).HeaderText
                Next
                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + 2, j + 1).value = DataGridView2.Rows(I).Cells(j).Value
                    Next j
                Next I
                .Rows("1:1").Font.FontStyle = "Bold"
                .Rows("1:1").Font.Size = 12

                .Cells.Columns.AutoFit()
                .Cells.Select()
                .Cells.EntireColumn.AutoFit()
                .Cells(1, 1).Select()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'RELEASE ALLOACTED RESOURCES
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        DateTimePicker1.Text = Today
        DateTimePicker2.Text = Today
        DataGridView2.DataSource = Nothing
        GroupBox10.Visible = False
    End Sub

    Private Sub DataGridView2_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView2.RowHeaderMouseClick
        Try
            Dim dr As DataGridViewRow = DataGridView2.SelectedRows(0)
            Me.Hide()
            frmSales.Show()
            ' or simply use column name instead of index
            'dr.Cells["id"].Value.ToString();
            frmSales.txtInvoiceNo.Text = dr.Cells(0).Value.ToString()
            frmSales.txtTotal.Text = dr.Cells(4).Value.ToString()
            frmSales.txtTotalPayment.Text = dr.Cells(5).Value.ToString()
            frmSales.txtPaymentDue.Text = dr.Cells(6).Value.ToString()
            frmSales.btnUpdate.Enabled = True
            frmSales.Delete.Enabled = True
            frmSales.btnPrint.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class