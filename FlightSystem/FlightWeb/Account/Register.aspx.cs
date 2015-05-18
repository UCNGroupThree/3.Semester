using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FlightWeb.MainService;
using System.Web.Security;

namespace FlightWeb.Account
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {

            if (Page.IsValid) {

                using (var client = new UserServiceClient()) {
                    
                    Postal postal = new Postal() {
                        PostCode = Convert.ToInt32(txtPostal.Text.Trim()),
                        City = txtCity.Text.Trim()
                    };
               //     string EncryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "SHA1");
                    User user = new User() {
                        Name = txtName.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                        Postal = postal,
                        PhoneNumber = txtPhone.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        PasswordPlain = txtPassword.Text
                                               

                    };
                    int returnCode = client.AddUser(user);
                    if (returnCode == -1) {
                        lblMessage.Text = "The Email you provide is already in use, please suply another";
                    }else if (returnCode == -2) {
                        lblMessage.Text = "The Password you provide is not in the right format";
                    }
                    else {
                        Response.Redirect("~/Default.aspx");
                    }
                }

            }
            
        }


    }
}