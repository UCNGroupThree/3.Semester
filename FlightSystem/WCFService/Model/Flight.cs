using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WCFService.Model {
    [DataContract(IsReference = true)]
    [KnownType(typeof (Plane))] 
    public class Flight {
        public Flight(DateTime departureTime, DateTime arrivalTime,Plane plane) {
            this.DepartureTime = departureTime;
            this.ArrivalTime = arrivalTime;
            this.Plane = plane;
        }

        public Flight() {
            SeatReservations = new List<SeatReservation>();
        }

        [DataMember]
        [Required]
        public int RouteID { get; set; }

        [DataMember]
        [Required]
        public Route Route { get; set; }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public int PlaneID { get; set; }

        [DataMember]
        [Required]
        public Plane Plane { get; set; }

        [DataMember]
        public DateTime ArrivalTime { get; set; }

        [DataMember]
        public DateTime DepartureTime { get; set; }

        [DataMember]
        public List<SeatReservation> SeatReservations { get; set; }
    }
}