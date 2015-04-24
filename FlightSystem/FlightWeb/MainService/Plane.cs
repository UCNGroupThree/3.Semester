using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightWeb.MainService
{
    public partial class Plane
    {
        public override string ToString() {
            return string.Format("#{0} - {1}", ID, Name);
        }
    }
}