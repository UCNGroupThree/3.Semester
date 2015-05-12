using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlightWeb {
    public partial class Reservation : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            Debug.WriteLine("#### Reservation ####");
            Debug.WriteLine("UrlReferrer: " + Request.UrlReferrer);
            Debug.WriteLine("SessionFlights: " + Session["flights"]);
            Debug.WriteLine("#### End ####");
            if (!IsPostBack) {
                if (Request.UrlReferrer != null) ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            }
        }

        protected void Button1_OnClick(object sender, EventArgs e) {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
    }
}