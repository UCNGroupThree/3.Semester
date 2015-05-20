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
            this.Ticket = ticket;
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
        [Required]
        public int Seat_ID { get; set; }
        [DataMember]
        [Required]
        [Index("Index_FlightSeat", 1, IsUnique = true)]
        public int Flight_ID { get; set; }

        [DataMember]
        [Required]
        public int Ticket_ID { get; set; }

        [DataMember]
        [ForeignKey("Flight_ID")]
        public Flight Flight { get; set; }
        
        [DataMember]
        [ForeignKey("Seat_ID")]
        public Seat Seat { get; set; }

        [DataMember]
        public SeatState State { get; set; }

        [DataMember]
        [ForeignKey("Ticket_ID")]
        public Ticket Ticket { get; set; }
    }
}