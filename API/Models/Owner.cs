using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Owner
    {
        public Owner()
        {
            Properties = new HashSet<Property>();
        }

        public int IdOwner { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public byte[]? Photo { get; set; }
        public DateTime BirthDay { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
