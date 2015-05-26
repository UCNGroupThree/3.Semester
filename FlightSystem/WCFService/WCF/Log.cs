using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.WCF {
    class Log : EventSource {

        public static Log EventLogger = new Log();
        public void MessageMethod(string Message) { if (IsEnabled())  WriteEvent(2, Message); }

    }
}
