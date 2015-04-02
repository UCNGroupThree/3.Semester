using System;

namespace FlightAdmin.MainService {
    public partial class Airport {
        public override string ToString() {
            return Name;
        }

        /// <summary>
        /// Get or Set TimeZone from/to The TimeZoneId.
        /// </summary>
        /// 
        /// <exception cref="OutOfMemoryException" />
        /// <exception cref="ArgumentException" />
        /// <exception cref="TimeZoneNotFoundException" />
        /// <exception cref="System.Security.SecurityException" />
        /// <exception cref="InvalidTimeZoneException" />
        public TimeZoneInfo TimeZone {
            get { return TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId); }
            set { TimeZoneId = value.Id; }
        }
    }
}