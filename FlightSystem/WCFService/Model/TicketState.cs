using System.Runtime.Serialization;

namespace WCFService.Model {
    [DataContract]
    public enum TicketState {
        [EnumMember]
        Pending,
        [EnumMember]
        Ordered,
        [EnumMember]
        Payed
    }
}