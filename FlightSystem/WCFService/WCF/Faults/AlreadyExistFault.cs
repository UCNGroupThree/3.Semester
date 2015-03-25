using System.Runtime.Serialization;

namespace WCFService.WCF.Faults {

    [DataContract]
    public class AlreadyExistFault {

            public AlreadyExistFault() {
                Result = false;
                Description = "The entry already exists";
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