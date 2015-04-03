using System;

namespace FlightAdmin.MainService {
    public partial class Airport {
        public override string ToString() {
            var str = Name;
            if (!string.IsNullOrWhiteSpace(ShortName)) {
                str += string.Format(" ({0})", ShortName);
            }
            return str;
        }

        /// <summary>
        /// Get or Set TimeZone from/to The TimeZoneId.
        /// </summary>
        /// <returns>null if the TimeZoneId not exits in the System</returns>
        public TimeZoneInfo TimeZone {

            get {
                try {
                    return TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
                } catch (Exception) {
                    return null;
                }
            }
            set { TimeZoneId = value.Id; }
        }
    }
}