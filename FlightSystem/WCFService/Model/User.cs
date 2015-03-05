using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WCFService.Model {

    [KnownType(typeof(List<Ticket>))]
    [KnownType(typeof(Postal))]
    [DataContract(IsReference = true)]
    public class User {

        public User() {
            Tickets = new List<Ticket>();
        }

        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public byte[] Concurrency { get; set; }

        [DataMember]
        public Postal Postal { get; set; }

        [DataMember]
        public ICollection<Ticket> Tickets { get; set; } 

    }
}