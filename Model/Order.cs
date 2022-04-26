using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_PromotionEngine.Model
{
   public class Order
    {
        public List<Product> Products { get; set; }
        public double TotalAmount { get; set; }
    }
}
