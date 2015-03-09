using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WCFService.Model {

    [DataContract(IsReference = true)]
    [KnownType(typeof (User))]
    [KnownType(typeof (List<SeatReservation>))]
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
        public string OrderState { get; set; } //TODO string/int/enum?

        [DataMember]
        public List<SeatReservation> SeatReservations { get; set; }

        [DataMember]
        [Timestamp]
        public byte[] Concurrency { get; set; }
    }
}