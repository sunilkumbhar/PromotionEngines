using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_PromotionEngine.Model
{
   public class Product
    {
        public Product() { }
        public Product(Product item)
        {
            SKUId = item.SKUId;
            Quantity = item.Quantity;
        }

        public char SKUId { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{SKUId} {Quantity}";
        }
    }
}
