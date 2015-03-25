using System.Runtime.Serialization;

namespace WCFService.WCF.Faults {

    [DataContract]
    public class DatabaseUpdateFault {

        public DatabaseUpdateFault() {
            Result = false;
            Description = "The database was unable to update the record";
            Message = Description;
        }

        public DatabaseUpdateFault(string objName) {
            Result = false;
            Description = "The database was unable to update the " + objName;
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