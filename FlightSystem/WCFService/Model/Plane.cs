using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.Model
{
    class Plane
    {
        public int ID  { get; set; }

        public String Name { get; set; }

        public List<Seat> Seats { get; set; }

    }
}
