﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WCFService.Model {

    [DataContract(IsReference = true)]
    [KnownType(typeof (List<Route>))]
    [KnownType(typeof (TimeZone))]
    public class Airport {

        public Airport() {
            Routes = new List<Route>();
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        [MaxLength(200), MinLength(2)]
        public string Name { get; set; }

        [DataMember]
        [MaxLength(3)]
        public string ShortName { get; set; }

        [DataMember]
        [Required]
        [MaxLength(200), MinLength(2)]
        public string City { get; set; }

        [DataMember]
        [Required]
        [MaxLength(200), MinLength(2)]
        public string Country { get; set; }

        [DataMember]
        public decimal Latitude { get; set; }

        [DataMember]
        public decimal Longtitude { get; set; }

        [DataMember]
        public decimal Altitude { get; set; }

        [DataMember]
        public string TimeZone { get; set; }

        [DataMember]
        public List<Route> Routes { get; set; }

        [DataMember]
        [Timestamp]
        public byte[] Concurrency { get; set; }

        public override int GetHashCode() {
            return ID;
        }
        public override bool Equals(object obj) {
            return Equals(obj as Airport);
        }
        public bool Equals(Airport obj) {
            return obj != null && obj.ID == this.ID;
        }

        public Route GetRouteTo(Airport to) {
            Route ret = null;
            if (Routes != null && Routes.Count > 0) {
                foreach (var route in Routes) {
                    if (route.To.Equals(to)) {
                        ret = route;
                        break;
                    }
                }
            } else {
                ret = null;
            }

            return ret;
        }
    }
}