using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerPro.Core.Entity
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Desription { get; set; }
        public int ImageId { get; set; }
        public virtual Image Image { get; set; }
    }
}
