using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WCFService.Model {

    [DataContract(IsReference = true)]
    [KnownType(typeof (List<Route>))]
    [KnownType(typeof (TimeZone))]
    public class Airport {

        public Airport() {
            Routes = new List<Route>();
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Fortæller at ID skal være IDENTITY
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public decimal Latitude { get; set; }

        [DataMember]
        public decimal Longtitude { get; set; }

        [DataMember]
        public decimal Altitude { get; set; }

        [DataMember]
        public string TimeZone { get; set; }

        [DataMember]
        public List<Route> Routes { get; set; }

        [DataMember]
        [Timestamp]
        public byte[] Concurrency { get; set; }
    }
}