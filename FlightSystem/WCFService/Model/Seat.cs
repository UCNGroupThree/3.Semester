using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WCFService.Model {
    [DataContract(IsReference = true)]
    [KnownType(typeof(SeatReservation))]
    [KnownType(typeof(Plane))]
    public class Seat {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DataMember]
        public int PosX { get; set; }

        [DataMember]
        public int PosY { get; set; }

        [DataMember]
        [Required]
        public Plane Plane { get; set; }

        [DataMember]
        public ICollection<SeatReservation> SeatReservations { get; set; }
    }
}