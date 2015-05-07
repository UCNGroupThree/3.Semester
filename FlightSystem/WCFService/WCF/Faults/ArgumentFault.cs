using System;
using System.Runtime.Serialization;

namespace WCFService.WCF.Faults {

    [DataContract]
    public class ArgumentFault {

        public ArgumentFault() {
            Result = false;
            Description = "A Argument Fault!";
            Message = Description;
        }

        public ArgumentFault(string message) {
            Result = false;
            Description = message;
            Message = Description;
        }

        public ArgumentFault(ArgumentException argumentException) : this (argumentException.Message) {
            
        }

        [DataMember]
        public bool Result { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}