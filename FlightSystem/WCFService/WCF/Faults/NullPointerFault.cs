using System.Runtime.Serialization;

namespace WCFService.WCF.Faults {

    [DataContract]
    public class NullPointerFault {
         
        public NullPointerFault() {
            Result = false;
            Message = "The received object was null";
            Description = "The received object was null";
        }

        [DataMember]
        public bool Result { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Description { get; set; }

    }
}