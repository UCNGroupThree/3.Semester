using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.ServiceModel.Description;

namespace WCFService.Model {

    [KnownType(typeof(List<User>))]
    [DataContract(IsReference = true)]
    public class Postal {

        public Postal() {
            Users = new List<User>();
        }

        public Postal(string city, int postCode) {
            this.City = city;
            this.PostCode = postCode;
            Users = new List<User>();
        }

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostCode { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public ICollection<User> Users { get; set; }

        [DataMember]
        [Timestamp]
        public byte[] Concurrency { get; set; }
    }
}