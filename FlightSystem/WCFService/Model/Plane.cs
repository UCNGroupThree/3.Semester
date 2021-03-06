﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WCFService.Model {
    [DataContract(IsReference = true)]
    [KnownType(typeof (List<Seat>))]
    public class Plane {

        public Plane() {
            Seats = new List<Seat>();
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<Seat> Seats { get; set; }

        [DataMember]
        public List<Flight> Flights { get; set; }

        [DataMember]
        [NotMapped]
        public int SeatCount { get; set; }
    }
}