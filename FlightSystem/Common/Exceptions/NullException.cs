using System;

namespace Common.Exceptions {
    public class NullException : Exception {

        public string ParameterName { get; set; }

        public NullException() { }

        public NullException(string message) : base(message) { }

        public NullException(string message, string parameterName) : base(message) {
            ParameterName = parameterName;
        }


        public NullException(string message, Exception inner) : base(message, inner) { }
    }
}