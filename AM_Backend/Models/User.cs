using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AM_Backend.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Salt { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }
    }
}