using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions {
    public class NotEnouthException : Exception {
        public NotEnouthException() { }

        public NotEnouthException(string message) : base(message) { }

        public NotEnouthException(string message, Exception inner) : base(message, inner) { }
    }
}
