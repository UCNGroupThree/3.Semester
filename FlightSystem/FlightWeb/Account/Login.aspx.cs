﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FlightWeb.MainService;
using System.Web.Security;

namespace FlightWeb
{
    public partial class Login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void LogIn(object sender, EventArgs e) {
            using (var client = new UserServiceClient()) {

                string EncryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(Password.Text.Trim(),
                    "SHA1");
                if (client.AuthenticateUser(Email.Text.Trim(), EncryptedPassword)) {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

    }
}