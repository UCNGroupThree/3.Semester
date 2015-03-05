﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WCFService.Model {

    [DataContract(IsReference = true)]
    [KnownType(typeof(User))]
    [KnownType(typeof(List<SeatReservation>))]
    [KnownType(typeof(DateTime))] //TODO behøver denne? samme på Airport med TimeZone
    public class Ticket {

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DataMember]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public string OrderState { get; set; } //TODO string/int/enum?

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public List<SeatReservation> SeatReservations { get; set; }

        [DataMember]
        public byte[] Concurrency { get; set; }
    }
}