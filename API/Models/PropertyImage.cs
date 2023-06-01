using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class PropertyImage
    {
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public string File { get; set; } = null!;
        public bool Enable { get; set; }

        public virtual Property IdPropertyNavigation { get; set; } = null!;
    }
}
