using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightAdmin.MainService
{
    partial class Postal
    {
        public override string ToString() {
            return string.Format("{0} {1}", PostCode, City);
        }
    }
}
