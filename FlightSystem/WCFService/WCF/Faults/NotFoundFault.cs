﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Common.Exceptions;

namespace WCFService.WCF.Faults {
    [DataContract]
    public class NotFoundFault {

        public NotFoundFault() {
            Result = false;
            Description = "The entry was not found";
            Message = Description;
        }

        public NotFoundFault(NotFoundException ex) {
            Result = false;
            Message = ex.Message;
            Description = Message;
        }

        [DataMember]
        public bool Result { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
