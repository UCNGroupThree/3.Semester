using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCFService.Model {
    [DataContract(IsReference = true)]
    public class Administrator {

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Fortæller at ID skal være IDENTITY
        public int ID { get; private set; }

        [DataMember]
        [Required]
        [MinLength(3), MaxLength(100)]
        public string Username { get; set; }

        [NotMapped]
        [DataMember]
        public string PasswordPlain { private get; set; }

        //[DataMember]
        [Required]
        [MinLength(3)]
        public string PasswordHash { get; set; }

        [DataMember]
        [Timestamp] // Timestamp for ændringer i objektet, til håndtering af concurrency i entityframework
        public byte[] Concurrency { get; set; }

        /// <exception cref="NullReferenceException" />
        /// <exception cref="PasswordFormatException"/>
        public void GenerateHash() {
            ValidatePasswordFormat();

            PasswordHash = "Hash" + PasswordPlain; //TODO SALT AND HASH!
            PasswordPlain = null;
        }

        /// <exception cref="NullReferenceException" />
        /// <exception cref="PasswordFormatException"/>
        private void ValidatePasswordFormat() {
            if (PasswordPlain == null) {
                throw new NullReferenceException("No new Password to generate hash!");
            }
            //TODO Validation af passwordformatet!
            bool valid = false;
            if (valid) {
                throw new PasswordFormatException();
            }
        }

        public bool ValidateLogin() { //TODO SALT AND HASH! CHECK
            return true;
        }
    }

}