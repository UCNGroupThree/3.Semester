using System.Runtime.Serialization;

namespace WCFService.WCF.Faults {
    [DataContract]
    public class LockedFault {

        public LockedFault() {
            Result = false;
            Description = "The Matrix is locked due to entities being updated, removed or created";
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