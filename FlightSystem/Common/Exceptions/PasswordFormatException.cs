using System;

namespace Common.Exceptions {
    public class PasswordFormatException : Exception {

        public PasswordFormatException() { }

        public PasswordFormatException(string message) : base(message) { }

        public PasswordFormatException(string message, Exception inner) : base(message, inner) { }
    }
}