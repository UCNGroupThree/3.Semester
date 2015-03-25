using System.Runtime.Serialization;

namespace WCFService.WCF.Faults {
    [DataContract]
    public class DatabaseDeleteFault {

        public DatabaseDeleteFault() {
            Result = false;
            Description = "The database was unable to delete the record";
            Message = Description;
        }

        public DatabaseDeleteFault(string objName) {
            Result = false;
            Description = "The database was unable to delete the " + objName;
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