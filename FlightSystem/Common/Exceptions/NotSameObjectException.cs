using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions {
    public class NotSameObjectException : Exception {
        public NotSameObjectException() { }

        public NotSameObjectException(string message) : base(message) { }

        public NotSameObjectException(string message, Exception inner) : base(message, inner) { }
    }
}
