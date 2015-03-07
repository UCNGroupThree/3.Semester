using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WCFService.Model
{
    [DataContract(IsReference = true)]
    class SeatReservation
    {
        private SeatState state; 
        private string name;
        private Seat seat;
        private Flight flight;

        [DataMember]
        public SeatReservation(SeatState state, string name, Seat seat, Flight flight)
        {
            this.state = state;
            this.name = name;
            this.seat = seat;
            this.flight = flight;

        }

        public SeatReservation()
        {
            
        }

        [DataMember]
        public Flight MyProperty
        {
            get { return flight; }
            set { flight = value; }
        }
        
        [DataMember]
        public Seat MyProperty
        {
            get { return seat; }
            set { seat = value; }
        }
        
        [DataMember]
        public string MyProperty
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public SeatState MyProperty
        {
            get { return state; }
            set { state = value; }
        }
        
    }
}
