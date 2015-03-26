using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FlightAdmin.MainService;

namespace FlightAdmin.GUI.FlightTabExtensions {
    public class FlightHelper {

        public DateTimePicker ArrivalTime { get; set; }

        public DateTimePicker DepartureTime { get; set; }

        public ComboBox Plane { get; set; }

        public FlightHelper(DateTimePicker arrivalTime, DateTimePicker departureTime, ComboBox plane) {
            ArrivalTime = arrivalTime;
            DepartureTime = departureTime;
            Plane = plane;
        }

        public bool Validate(CreateFlight main) {
            if (DepartureTime.Value.CompareTo(ArrivalTime.Value) <= 0) {
                main.epDate.SetError(DepartureTime, "Something Wrong!");
                main.epDate.SetError(ArrivalTime, "Something Wrong!");
                return false;
            }

            if (Plane == null || (int) Plane.SelectedValue < 1) {
                return false;
            }


            return true;
        }

    }
}