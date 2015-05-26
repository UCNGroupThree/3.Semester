using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WCFService.Logging {
    public class Log {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Message { get; set; }

        public DateTime Time {
            get { return DateTime.Now;}
            set { Time = value; }
        }

    }
}