using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCFService.Model
{
    [DataContract(IsReference = true)]
    public class Administrator
    {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Fortæller at ID skal være IDENTITY
        public int ID { get; private set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string PasswordHash { get; set; }

        [DataMember]
        [Timestamp] // Timestamp for ændringer i objektet, til håndtering af concurrency i entityframework
        public byte[] Concurrency { get; set; }
    }
}