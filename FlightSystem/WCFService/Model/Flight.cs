using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WCFService.Model
{
    [DataContract(IsReference = true)]
    class Flight
    {

        private DateTime departureTime;
        private DateTime arivalTime;
        private decimal price;
        private Plane plane;

        [DataMember]
        public Flight(DateTime departureTime, DateTime arivalTime, decimal price, Plane plane)
        {
            this.departureTime = departureTime;
            this.arivalTime = arivalTime;
            this.price = price;
            this.plane = plane;
        }

        public Flight()
        {

        }

        [DataMember]
        public Plane MyProperty
        {
            get { return plane; }
            set { plane = value; }
        }
        
        [DataMember]
        public decimal MyProperty
        {
            get { return price; }
            set { price = value; }
          
        }
        
        [DataMember]
        public DateTime MyProperty
        {
            get { return arivalTime; }
            set { arivalTime = value; }
        }
        
        [DataMember]
        public DateTime MyProperty
        {
            get { return departureTime; }
            set { departureTime = value; }
        }
        
    }
}
