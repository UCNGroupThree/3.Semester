using System.Runtime.Serialization;

namespace WCFService.WCF.Faults {

    [DataContract]
    public class DatabaseUpdateFault {

        public DatabaseUpdateFault() {
            Result = false;
            Description = "The database was unable to update the record";
        }

        public DatabaseUpdateFault(string objName) {
            Result = false;
            Description = "The database was unable to update the " + objName;
        }

        [DataMember]
        public bool Result { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}