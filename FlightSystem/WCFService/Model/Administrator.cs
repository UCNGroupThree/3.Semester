using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.ServiceModel;
using Common.Exceptions;

namespace WCFService.Model {
    [DataContract(IsReference = true)]
    public class Administrator {

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Fortæller at ID skal være IDENTITY
        public int ID { get; set; }

        [DataMember]
        [Required]
        [MinLength(3), MaxLength(100)]
        public string Username { get; set; }

        [NotMapped]
        [DataMember]
        public string PasswordPlain { get; set; }

        //[DataMember]
        [Required]
        [MinLength(3)]
        public string PasswordHash { get; set; }

        [DataMember]
        [Timestamp] // Timestamp for ændringer i objektet, til håndtering af concurrency i entityframework
        public byte[] Concurrency { get; set; }

    }

}