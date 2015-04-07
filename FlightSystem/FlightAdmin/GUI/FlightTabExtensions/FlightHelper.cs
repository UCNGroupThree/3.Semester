using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI.FlightTabExtensions {
    public class FlightHelper {

        public Flight Flight { get; set; }

        public DateTimePicker ArrivalTime { get; set; }

        public DateTimePicker DepartureTime { get; set; }

        public ComboBox Plane { get; set; }

        public FlightHelper(DateTimePicker arrivalTime, DateTimePicker departureTime, ComboBox plane) {
            ArrivalTime = arrivalTime;
            DepartureTime = departureTime;
            Plane = plane;
        }

        public bool Validate(CreateFlights main) {
            if (DepartureTime.Value.CompareTo(ArrivalTime.Value) <= 0) {
                main.epFlights.SetError(DepartureTime, "Something Wrong!");
                main.epFlights.SetError(ArrivalTime, "Something Wrong!");
                return false;
            }

            if ((int) Plane.SelectedValue < 1) {
                main.epFlights.SetError(Plane, "Something Wrong!");
                return false;
            }


            return true;
        }

    }
}