using System;
using System.Windows.Forms;

namespace Common.Exceptions {
    public class SubmitException : Exception {

        /// <summary>
        /// thrown because of this control
        /// </summary>
        public Control Control { get; set; }

        public SubmitException(Control control, string message) : base(message) { }
            
        public SubmitException(string message) : base(message) { }

        public SubmitException(string message, Exception inner) : base(message, inner) { }  
    }
}