using System.Runtime.Serialization;

namespace WCFService.WCF.Faults {
    [DataContract]
    public class TimeZoneFault {

        public TimeZoneFault() {
            Result = false;
            Description = "The specified time zone is not a valid system time zone";
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