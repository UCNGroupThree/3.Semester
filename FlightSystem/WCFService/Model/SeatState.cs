using System.Runtime.Serialization;

namespace WCFService.Model {

    [DataContract(IsReference = true)]
    public enum SeatState {
        Free,
        Taken,
        Occupied
    }
}