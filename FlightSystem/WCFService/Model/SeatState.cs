using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WCFService.Model
{
    [DataContract(IsReference = true)]
    enum SeatState
    {
        Free,
        Taken,
        Occupied,
    }
}
