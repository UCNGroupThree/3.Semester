using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightAdmin.Exceptions {
    class TimeZoneException : Exception {
        public TimeZoneException() { }

        public TimeZoneException(string message) : base(message) { }

        public TimeZoneException(string message, Exception inner) : base(message, inner) { }
    }
}
