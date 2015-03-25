using System.Runtime.Serialization;

namespace WCFService.WCF.Faults {
    [DataContract]
    public class DatabaseInsertFault {

        public DatabaseInsertFault() {
            Result = false;
            Description = "The database was unable to insert the record";
            Message = Description;
        }

        public DatabaseInsertFault(string objName) {
            Result = false;
            Description = "The database was unable to insert the " + objName;
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