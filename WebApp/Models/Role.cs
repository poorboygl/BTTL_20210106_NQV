using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Role
    {
        public Guid ID  { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}
