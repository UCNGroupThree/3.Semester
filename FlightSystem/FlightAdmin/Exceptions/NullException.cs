using System;

namespace FlightAdmin.Exceptions {
    public class NullException : Exception {

        public NullException() { }

        public NullException(string message) : base(message) { }

        public NullException(string message, Exception inner) : base(message, inner) { }
    }
}