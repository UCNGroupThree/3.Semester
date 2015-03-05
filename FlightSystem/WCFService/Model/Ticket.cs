using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.Model
{
    class Ticket
    {
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public String OrderState { get; set; }
        public User User { get; set; }
        public List<SeatReservation> SeatReservation { get; set; }

    }
}
