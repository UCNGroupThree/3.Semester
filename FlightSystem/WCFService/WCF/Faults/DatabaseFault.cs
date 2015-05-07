using System.Runtime.Serialization;
using Common.Exceptions;

namespace WCFService.WCF.Faults {
    [DataContract]
    public class DatabaseFault {

        public DatabaseFault() {
            Result = false;
            Description = "The database was unable to handle the request";
            Message = Description;
        }

        public DatabaseFault(string message) {
            Result = false;
            Description = message;
            Message = message;
        }

        [DataMember]
        public bool Result { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}