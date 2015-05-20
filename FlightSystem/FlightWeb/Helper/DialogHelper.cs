using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightWeb.Helper {
    public class DialogHelper {
        public string Header { get; set; }
        public string Text { get; set; }

        public DialogHelper(string header, string text) {
            Header = header;
            Text = text;
        }
    }
}