using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_PromotionEngine.Model
{
   public  class Promotion:Order
    {
        public IEnumerable<Product> Valid(Order order, IEnumerable<Product> validItems)
        {
            var foundProducts = new List<Product>();
            if (Products == null || Products.Count < 1)
                return foundProducts;

            foreach (var promotionItem in Products)
            {
                var foundProduct = validItems.FirstOrDefault(x => x.SKUId == promotionItem.SKUId) ??
                  order.Products.FirstOrDefault(x => x.SKUId == promotionItem.SKUId);
                if (foundProduct == null || foundProduct.Quantity < promotionItem.Quantity)
                    return null;

                if (!foundProducts.Any(x => x.SKUId == foundProduct.SKUId))
                    foundProducts.Add(new Product(foundProduct));
            }

            CalculatedRestQuantity(order, foundProducts);

            return foundProducts;
        }
        // apply Promotion and Price calculation
        private void CalculatedRestQuantity(Order order, List<Product> foundItems)
        {
            var found = foundItems.Count() > 0;
            if (found)
            {
                do
                {
                    order.TotalAmount += TotalAmount;
                    foreach (var promotionItem in Products)
                    {
                        var item = foundItems.FirstOrDefault(x => x.SKUId == promotionItem.SKUId);
                        if (item != null)
                        {
                            item.Quantity -= promotionItem.Quantity;
                            if (found)
                                found = item.Quantity >= promotionItem.Quantity;
                        }
                    }
                } while (found);
            }
        }
    }
}
