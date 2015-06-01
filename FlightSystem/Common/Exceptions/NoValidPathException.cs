using System;

namespace Common.Exceptions {
    public class NoValidPathException : Exception {
        public NoValidPathException() { }

        public NoValidPathException(string message) : base(message) { }

        public NoValidPathException(string message, Exception inner) : base(message, inner) { }
    }

}