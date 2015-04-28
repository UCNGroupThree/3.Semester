using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WCFService.Model {
    [DataContract(IsReference = true)]
    public class Seat {  //Todo Kim
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DataMember]
        public int PosX { get; set; }

        [DataMember]
        public int PosY { get; set; }

        [DataMember]
        public Plane Plane { get; set; }
    }
}