using System.Runtime.Serialization;
using Common.Exceptions;

namespace WCFService.WCF.Faults {

    [DataContract]
    public class DijkstraFault {

        public DijkstraFault(string message) {
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