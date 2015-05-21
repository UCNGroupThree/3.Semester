using System.Collections.Generic;
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
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        [NotMapped]
        [DataMember]
        public string PasswordPlain { get; set; }

        [DataMember]
        [Timestamp]
        public byte[] Concurrency { get; set; }

        [DataMember]
        public Postal Postal { get; set; }

        [DataMember]
        public List<Ticket> Tickets { get; set; }
    }
}