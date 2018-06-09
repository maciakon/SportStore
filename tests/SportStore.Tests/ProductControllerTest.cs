using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using Xunit;

namespace SportStore.Tests
{
    public class ProductControllerTest
    {
        [Fact]
        public void CanPaginate()
        {
            var fakeProductsRepo = new Mock<IProductsRepository>();
            fakeProductsRepo.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductId = 1, Name = "P1" },
                new Product { ProductId = 2, Name = "P2" },
                new Product { ProductId = 3, Name = "P3" },
                new Product { ProductId = 4, Name = "P4" },
                new Product { ProductId = 5, Name = "P5" },
                new Product { ProductId = 6, Name = "P6" },
                new Product { ProductId = 7, Name = "P7" },
                new Product { ProductId = 8, Name = "P8" }
            });

            var productController = new ProductController(fakeProductsRepo.Object);

            var results = productController.List(2).ViewData.Model as IEnumerable<Product>;
            var resultList = results.ToList();

            Assert.True(resultList.Count == 4);
            var productsOnSecondPage = resultList.Where(product => product.ProductId > 4);
            Assert.True(productsOnSecondPage.Count() == 4);
        }
    }
}
