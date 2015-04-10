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

        public Airport GetCopy() {
            return new Airport {
                ID = ID,
                Name = Name,
                ShortName = ShortName,
                City = City,
                Country = Country,
                Latitude = Latitude,
                Longitude = Longitude,
                Altitude = Altitude,
                TimeZoneId = TimeZoneId
            };
        }

        public void SetToCopy(Airport copy) {
            ID = copy.ID;
            Name = copy.Name;
            ShortName = copy.ShortName;
            City = copy.City;
            Country = copy.Country;
            Latitude = copy.Latitude;
            Longitude = copy.Longitude;
            Altitude = copy.Altitude;
            TimeZoneId = copy.TimeZoneId;
        }
    }
}