using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightWeb.MainService {
    public partial class Airport {

        public override string ToString() {
            var str = Name;
            if (!string.IsNullOrWhiteSpace(ShortName)) {
                str += string.Format(" ({0})", ShortName);
            }
            return str;
        }
    }
}