Public NotInheritable Class frmAbout

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).
        Me.LabelProductName.Text = "PRODUCT NAME: Sales and Inventory Management System" ' My.Application.Info.ProductName
        Me.LabelVersion.Text = "VERSION: 2.0" ' String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = "COPY RIGHT: C-2023-2024" ' My.Application.Info.Copyright
        Me.LabelCompanyName.Text = "COMPANY: sunsiva info tech" 'My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = "DESCRIPTION : Sri Srinivasa Agencies, Mangalapuram. Established on 31st Aug 2022 By KR Sundaram." ' My.Application.Info.Description
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

End Class
