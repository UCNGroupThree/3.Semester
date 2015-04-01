using System;

namespace FlightAdmin.MainService {
    public partial class Airport {
        public override string ToString() {
            return Name;
        }

        public TimeZoneInfo TimeZone {
            get { return TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId); }
            set { TimeZoneId = value.Id; }
        }
    }
}