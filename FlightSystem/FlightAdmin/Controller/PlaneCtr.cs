using System.Collections.Generic;
using System.Runtime.InteropServices;
using FlightAdmin.MainService;

namespace FlightAdmin.Controller {
    public class PlaneCtr {


        public List<Plane> GetAllPlanes() { //TODO Error handeling
            List<Plane> planes = new List<Plane>();


            using (PlaneServiceClient client = new PlaneServiceClient()) {
                planes = client.GetAllPlanes();
            }
           

            return planes;
        }

    }
}