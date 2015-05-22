using System;
using System.Runtime.Serialization;
using Common.Exceptions;

namespace WCFService.WCF.Faults {

    [DataContract]
    public class NotSameObjectFault {

        public NotSameObjectFault() {
            Result = false;
            Description = "Not Same Object Fault!";
            Message = Description;
        }

        public NotSameObjectFault(NotSameObjectException ex) {
            Result = false;
            Description = ex.Message;
            Message = Description;
        }

        [DataMember]
        public bool Result { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}