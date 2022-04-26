using System;
using System.Collections.Generic;

using ConsoleApp_PromotionEngine.Controllers;
using ConsoleApp_PromotionEngine.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestPromotionEngine
{
    [TestClass]
    public class UnitTestPromotionEngine
    {

       static  readonly IEnumerable<SKUPrices> PriceList =
          new List<SKUPrices> {
        new SKUPrices { SKUId = 'A', UnitPrice = 50 },
        new SKUPrices { SKUId = 'B', UnitPrice = 30 },
        new SKUPrices { SKUId = 'C', UnitPrice = 20 },
        new SKUPrices { SKUId = 'D', UnitPrice = 15 } };

       static  readonly IEnumerable<Promotion> Promotions =
          new List<Promotion> {
        new Promotion {
          Products = new List<Product> {
            new Product { SKUId = 'A', Quantity = 3 }},
          TotalAmount = 130 },  
        new Promotion {
          Products = new List<Product> {
            new Product { SKUId = 'B', Quantity = 2 }},
          TotalAmount = 45 },  
        new Promotion {
          Products = new List<Product> {
            new Product { SKUId = 'C', Quantity = 1 },
            new Product { SKUId = 'D', Quantity = 1 }},
          TotalAmount = 30 } };  
       static  readonly EngineController obj = new EngineController(PriceList, Promotions);
        [TestMethod]
        public void GetScenario_A()
        {
            var order =
              new Order
              {
                  Products = new List<Product>
                {
            new Product { SKUId = 'A', Quantity = 1 },
            new Product { SKUId = 'B', Quantity = 1 },
            new Product { SKUId = 'C', Quantity = 1 }}
              };

            obj.WithdrawOrder(order);
            Assert.IsTrue(order.TotalAmount == 100);
        }

        [TestMethod]
        public void GetScenario_B()
        {
            var order =
              new Order
              {
                  Products = new List<Product>
                {
            new Product { SKUId = 'A', Quantity = 5 },
            new Product { SKUId = 'B', Quantity = 5 },
            new Product { SKUId = 'C', Quantity = 1 }}
              };

            obj.WithdrawOrder(order);
            Assert.IsTrue(order.TotalAmount == 370);
        }

        [TestMethod]
        public void GetScenario_C()
        {
            var order =
              new Order
              {
                  Products = new List<Product>
                {
            new Product { SKUId = 'A', Quantity = 3 },
            new Product { SKUId = 'B', Quantity = 5 },
            new Product { SKUId = 'C', Quantity = 1 },
            new Product { SKUId = 'D', Quantity = 1 }}
              };

            obj.WithdrawOrder(order);
            Assert.IsTrue(order.TotalAmount == 280);
        }
    }
}
