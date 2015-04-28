using System.Runtime.Serialization;

namespace WCFService.Model {

    [DataContract]
    public enum SeatState {
        [EnumMember]
        Free,
        [EnumMember]
        Taken,
        [EnumMember]
        Occupied
    }
}