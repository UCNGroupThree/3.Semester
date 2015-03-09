﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WCFService.Model {
    [DataContract(IsReference = true)]
    [KnownType(typeof (Plane))]
    public class Flight {
        public Flight(DateTime departureTime, DateTime arrivalTime, decimal price, Plane plane) {
            this.DepartureTime = departureTime;
            this.ArrivalTime = arrivalTime;
            this.Price = price;
            this.Plane = plane;
        }

        public Flight() {
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DataMember]
        public Plane Plane { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public DateTime ArrivalTime { get; set; }

        [DataMember]
        public DateTime DepartureTime { get; set; }
    }
}