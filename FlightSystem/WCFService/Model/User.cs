﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WCFService.Model {
    [KnownType(typeof (List<Ticket>))]
    [KnownType(typeof (Postal))]
    [DataContract(IsReference = true)]
    public class User {
        public User() {
            Tickets = new List<Ticket>();
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string PasswordHash { get; set; }

        [DataMember]
        [Timestamp]
        public byte[] Concurrency { get; set; }

        [DataMember]
        public Postal Postal { get; set; }

        [DataMember]
        public List<Ticket> Tickets { get; set; }
    }
}