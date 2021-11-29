using BusinessLogic.DTO;
using BusinessLogic.Service;
using BusinessLogic.Service.BusinessRules;
using DataAccess.Entities;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PromotionEngineTests.CalculationBusinessLogicTests
{
    public class BundleBusinessRules
    {
        [Fact]
        public void NotHaveItemForProcessing()
        {
            //Arrange
            var orderItems = new List<OrderItem>
                {
                    new OrderItem { Quantity = 1, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 1, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 1, SKU = "C", Price = 20 }
                };

            var promotion = new Promotion { BundleType = Bundle.Bundle, SKUs = new List<string> { "C", "D" }, DiscountType = Discount.FixedPrice, FixedPriceDiscount = 30 };

            var bundleBusinessRules = new BusinessLogic.Service.BusinessRules.BundleBusinessRules();

            //Act
            var analizeOrderItemsDTO = bundleBusinessRules.ApplyBusinessRules(orderItems, promotion);

            //Assert
            Assert.Empty(analizeOrderItemsDTO.ItemForProccessing);
        }

        [Fact]
        public void HaveTwoItemForProcessing()
        {
            //Arrange
            var orderItems = new List<OrderItem>
                {
                    new OrderItem { Quantity = 1, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 1, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 3, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 2, SKU = "D", Price = 20 }
                };

            var promotion = new Promotion { BundleType = Bundle.Bundle, SKUs = new List<string> { "C", "D" }, DiscountType = Discount.FixedPrice, FixedPriceDiscount = 30 };

            var bundleBusinessRules = new BusinessLogic.Service.BusinessRules.BundleBusinessRules();

            //Act
            var analizeOrderItemsDTO = bundleBusinessRules.ApplyBusinessRules(orderItems, promotion);

            //Assert
            Assert.Equal(2, analizeOrderItemsDTO.ItemForProccessing.Count);
        }

        [Fact]
        public void HaveTwoPromotions()
        {
            //Arrange
            var orderItems = new List<OrderItem>
                {
                    new OrderItem { Quantity = 6, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 1, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 3, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 2, SKU = "D", Price = 20 }
                };

            var promotion = new Promotion { BundleType = Bundle.Bundle, SKUs = new List<string> { "C", "D" }, DiscountType = Discount.FixedPrice, FixedPriceDiscount = 30 };

            var bundleBusinessRules = new BusinessLogic.Service.BusinessRules.BundleBusinessRules();

            //Act
            var analizeOrderItemsDTO = bundleBusinessRules.ApplyBusinessRules(orderItems, promotion);

            //Assert
            Assert.Equal(2, analizeOrderItemsDTO.BundleCount);
        }

        [Fact]
        public void HaveFiveNonPromotionItems()
        {
            //Arrange
            var orderItems = new List<OrderItem>
                {
                    new OrderItem { Quantity = 4, SKU = "B", Price = 30 },
                    new OrderItem { Quantity = 2, SKU = "D", Price = 20 },

                    new OrderItem { Quantity = 6, SKU = "A", Price = 50 },
                    new OrderItem { Quantity = 2, SKU = "Nike", Price = 100 },

                    new OrderItem { Quantity = 3, SKU = "C", Price = 20 },
                    new OrderItem { Quantity = 2, SKU = "Adidas", Price = 110 }
                };

            var promotions = new List<Promotion> {
                new Promotion { BundleType = Bundle.Bundle, SKUs = new List<string> { "A", "Nike" }, DiscountType = Discount.FixedPrice, FixedPriceDiscount = 30 },
                new Promotion{ BundleType = Bundle.Bundle, SKUs = new List<string> { "C", "Adidas" }, DiscountType = Discount.FixedPrice, FixedPriceDiscount = 30 }
            };

            var bundleBusinessRules = new BusinessLogic.Service.BusinessRules.BundleBusinessRules();

            //Act
            var analizeOrderItemsDTOList = new List<AnalizeOrderItemsDTO>();
            foreach (var promotion in promotions)
            {
                analizeOrderItemsDTOList.Add(bundleBusinessRules.ApplyBusinessRules(orderItems, promotion));
                //Assert
            }
            Assert.Equal(5, analizeOrderItemsDTOList.SelectMany(x => x.SingleItems).Sum(x => x.ItemCount));
        }
    }
}
