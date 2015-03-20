using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCFService.WCF.Faults {

    [DataContract]
    public class OptimisticConcurrencyFault {

        public OptimisticConcurrencyFault() {
            Result = false;
            Description = "Concurrency!"; //TODO Better text for concurrency?
        }

        [DataMember]
        public bool Result { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Description { get; set; }

    }
}