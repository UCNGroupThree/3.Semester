using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WCFService.Model {
    
    [DataContract(IsReference = true)]
    [KnownType(typeof(Airport))]
    [KnownType(typeof(List<Flight>))]
    public class Route {

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DataMember]
        public Airport LocationOne { get; set; }

        [DataMember]
        public Airport LocationTwo { get; set; }

        [DataMember]
        public List<Flight> Flights { get; set; }

        [DataMember]
        [Timestamp]
        public byte[] Concurrency { get; set; }

    }
}
