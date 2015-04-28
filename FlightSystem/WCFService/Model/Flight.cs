﻿using System;
using System.Collections.Generic;
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
        public Route Route { get; set; }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DataMember]
        public Plane Plane { get; set; }

        [DataMember]
        public DateTime ArrivalTime { get; set; }

        [DataMember]
        public DateTime DepartureTime { get; set; }

        [DataMember]
        public List<SeatReservation> SeatReservations { get; set; }
    }
}