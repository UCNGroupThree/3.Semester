using System;

namespace Common.Exceptions {
    public class AirportNotFoundException : Exception {
        public AirportNotFoundException() { }

        public AirportNotFoundException(string message) : base(message) { }

        public AirportNotFoundException(string message, Exception inner) : base(message, inner) { }
    }

}