using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WCFService.Model {
    [DataContract(IsReference = true)]
    [KnownType(typeof (SeatState))]
    [KnownType(typeof (Seat))]
    [KnownType(typeof (Flight))]
    [KnownType(typeof(Ticket))]
    public class SeatReservation {
        public SeatReservation(Ticket ticket, SeatState state, Seat seat, Flight flight) {
            this.State = state;
            this.Seat = seat;
            this.Flight = flight;
        }

        public SeatReservation() {
        }

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Index("Index_FlightSeat", 2, IsUnique = true)]
        [DataMember]
        public int Seat_ID { get; set; }
        [DataMember]
        [Index("Index_FlightSeat", 1, IsUnique = true)]
        public int Flight_ID { get; set; }

        [DataMember]
        [Required]
        [ForeignKey("Flight_ID")]
        public Flight Flight { get; set; }

        [DataMember]
        [Required]
        [ForeignKey("Seat_ID")]
        public Seat Seat { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public SeatState State { get; set; }

        [DataMember]
        [Required]
        public Ticket Ticket { get; set; }
    }
}