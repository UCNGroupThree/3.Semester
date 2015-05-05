using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WCFService.Model {

    [DataContract(IsReference = true)]
    [KnownType(typeof (User))]
    [KnownType(typeof (List<SeatReservation>))]
    [KnownType(typeof(TicketState))]
    public class Ticket {

        public Ticket() {
            SeatReservations = new List<SeatReservation>();
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DataMember]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public TicketState OrderState { get; set; }

        [DataMember]
        public List<SeatReservation> SeatReservations { get; set; }

        [DataMember]
        [Timestamp]
        public byte[] Concurrency { get; set; }

    }
}