using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using FlightWeb.MainService;

namespace FlightWeb
{
    /// <summary>
    /// Summary description for JSH
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class JSH : System.Web.Services.WebService
    {
        [WebMethod]
        public List<Airport> GetAirportsFromCountry(string country) {
            using (AirportServiceClient client = new AirportServiceClient()) {
                var list = client.GetAirportsByCountry(country);
                return list;
            }
        }

        /*        
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetAirportsFromCountry()
        {
            using (AirportServiceClient client = new AirportServiceClient()) {
                var js = new JavaScriptSerializer();
                var list = client.GetAirportsByCountry("Denmark");
                var abc = js.Serialize(list);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", abc.Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(abc);
            }
        }
        */

    }
}
