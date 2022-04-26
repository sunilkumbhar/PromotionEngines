using ConsoleApp_PromotionEngine.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_PromotionEngine.Controllers
{
   public class EngineController
    {
        private IEnumerable<SKUPrices> PriceList;
        private IEnumerable<Promotion> Promotions;

        public EngineController(IEnumerable<SKUPrices> priceList, IEnumerable<Promotion> promotions)
        {
            this.PriceList = priceList;
            this.Promotions = promotions;
        }

        public void WithdrawOrder(Order order)
        {
            var foundItems = new List<Product>();
            if (Promotions != null && Promotions.Count() > 0)
                foreach (var promotion in Promotions)
                {
                    var validatedItems = promotion.Valid(order, foundItems);
                    UpdateValidatedItems(foundItems, validatedItems);
                }

            ApplyNormalPrice(order, foundItems);
        }

        private void ApplyNormalPrice(Order order, List<Product> foundItems)
        {
            foreach (var item in order.Products)
            {
                var validateItem = foundItems.FirstOrDefault(x => x.SKUId == item.SKUId) ?? item;
                var quantity = validateItem.Quantity;
                if (quantity > 0)
                    order.TotalAmount += quantity * PriceList.First(x => x.SKUId == item.SKUId).UnitPrice;
            }
        }

        private static void UpdateValidatedItems(List<Product> foundItems, IEnumerable<Product> validatedItems)
        {
            if (validatedItems == null || validatedItems.Count() < 1)
                return;

            foreach (var item in validatedItems)
                if (!foundItems.Any(x => x.SKUId == item.SKUId))
                    foundItems.Add(item);
        }
    }
}
