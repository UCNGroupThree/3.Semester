﻿using System;

namespace Common.Exceptions {
    public class DeleteException : Exception {
        public DeleteException() { }

        public DeleteException(string message) : base(message) { }

        public DeleteException(string message, Exception inner) : base(message, inner) { } 
    }
}