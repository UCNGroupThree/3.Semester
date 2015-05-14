using System.Runtime.Serialization;

namespace WCFService.WCF.Faults {

    [DataContract]
    public class DeleteFault {
        public DeleteFault(string message) {
            Result = false;
            Description = message;
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