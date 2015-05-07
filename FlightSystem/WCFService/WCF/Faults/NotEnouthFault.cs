using System;
using System.Runtime.Serialization;
using Common.Exceptions;

namespace WCFService.WCF.Faults {

    [DataContract]
    public class NotEnouthFault {

        public NotEnouthFault() {
            Result = false;
            Description = "A Argument Fault!";
            Message = Description;
        }

        public NotEnouthFault(NotEnouthException ex) {
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