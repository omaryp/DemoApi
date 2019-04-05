using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DemoApi.Models
{
    [DataContract]
    public class Usuario    
    {
        [DataMember(Name="Id")]
        public int Id { get; set; }
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "Email")]
        public string Email { get; set; }
        [DataMember(Name = "Mobile")]
        public string Mobile { get; set; }
        [DataMember(Name = "Address")]
        public string Address { get; set; }
        [DataMember(Name = "IsActive")]
        public bool IsActive { get; set; }
    }
}
