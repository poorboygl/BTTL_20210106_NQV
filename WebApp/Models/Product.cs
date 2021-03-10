using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public short BrandID { get; set; }
        public short CategoryID { get; set; }
        public short ModelYear { get; set; }
        public decimal Price { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
    }
}
