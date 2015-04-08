using System.Runtime.Serialization;

namespace WCFService.WCF.Faults {

    [DataContract]
    public class PasswordFormatFault {

        public PasswordFormatFault() {
            Result = false;
            Description = "The Password is not valid!";
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