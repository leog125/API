using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OPropertyFilter
    {
        public int IdProperty { get; set; }
        public string Name { get; set; } = null!;
        public short? Year { get; set; }
        public int IdOwner { get; set; }
    }
}
