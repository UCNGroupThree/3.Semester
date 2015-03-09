using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WCFService.Model {
    [DataContract(IsReference = true)]
    //[KnownType(typeof (SeatState))]
    [KnownType(typeof (Seat))]
    [KnownType(typeof (Flight))]
    public class SeatReservation {
        public SeatReservation(string state, string name, Seat seat, Flight flight) {
            this.State = state;
            this.Name = name;
            this.Seat = seat;
            this.Flight = flight;
        }

        public SeatReservation() {
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DataMember]
        public Flight Flight { get; set; }

        [DataMember]
        public Seat Seat { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string State { get; set; }
    }
}