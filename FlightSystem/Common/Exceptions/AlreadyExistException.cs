using System;

namespace Common.Exceptions {
    public class AlreadyExistException : Exception {
            public AlreadyExistException() { }

            public AlreadyExistException(string message) : base(message) { }

            public AlreadyExistException(string message, Exception inner) : base(message, inner) { }
        } 
    
}