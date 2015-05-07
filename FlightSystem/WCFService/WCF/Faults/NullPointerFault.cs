using System.Runtime.Serialization;
using Common.Exceptions;

namespace WCFService.WCF.Faults {

    [DataContract]
    public class NullPointerFault {
         
        public NullPointerFault() {
            Result = false;
            Message = "The received object was null";
            Description = "The received object was null";
        }

        public NullPointerFault(string message, string parameterName) {
            Result = false;
            Message = message;
            Description = message;
            ParamenterName = parameterName;
        }

        public NullPointerFault(NullException ex) : this(ex.Message, ex.ParameterName) {
            
        }

        [DataMember]
        public string ParamenterName { get; set; }

        [DataMember]
        public bool Result { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Description { get; set; }

    }
}