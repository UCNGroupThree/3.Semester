using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WCFService.Model {
    //[KnownType(typeof(List<User>))]
    [DataContract(IsReference = true)]
    public class Postal {
        public Postal() {
            //  Users = new List<User>();
        }

        public Postal(string city, int postCode) {
            City = city;
            PostCode = postCode;
            // Users = new List<User>();
        }

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostCode { get; set; }

        [DataMember]
        public string City { get; set; }

        // [DataMember]
        // public ICollection<User> Users { get; set; }

        [DataMember]
        [Timestamp]
        public byte[] Concurrency { get; set; }
    }
}