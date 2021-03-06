﻿using System.Runtime.Serialization;
using Common.Exceptions;

namespace WCFService.WCF.Faults {

    [DataContract]
    public class AlreadyExistFault {

        public AlreadyExistFault() {
            Result = false;
            Description = "The entry already exists";
            Message = Description;
        }

        public AlreadyExistFault(string message) {
            Result = false;
            Description = message;
            Message = Description;
        }

        public AlreadyExistFault(AlreadyExistException ex) : this(ex.Message) {
            
        }

        [DataMember]
        public bool Result { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}