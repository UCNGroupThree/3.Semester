using System.Runtime.Serialization;

namespace WCFService.Model {

    [DataContract(IsReference = false)]
    public enum SeatState {
        Free,
        Taken,
        Occupied
    }
}