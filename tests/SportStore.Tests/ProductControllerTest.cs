using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using SportStore.Models.ViewModels;
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

            var results = productController.List(null, 2).ViewData.Model as ProductListViewModel;
            var resultList = results.Products.ToList();

            Assert.True(resultList.Count == 4);
            var productsOnSecondPage = resultList.Where(product => product.ProductId > 4);
            Assert.True(productsOnSecondPage.Count() == 4);
        }

        [Fact]
        public void CanSendPaginationViewModel()
        {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[]{
                    new Product { ProductId = 1, Name = "P1" },
                    new Product { ProductId = 2, Name = "P2" },
                    new Product { ProductId = 3, Name = "P3" },
                    new Product { ProductId = 4, Name = "P4" },
                    new Product { ProductId = 5, Name = "P5" },
                }
            );

            var productController = new ProductController(mock.Object) {PageSize = 3};
            var result = productController.List(null, 2).ViewData.Model as ProductListViewModel;

            Assert.Equal(2, result.PagingInfo.CurrentPage);
            Assert.Equal(2, result.PagingInfo.TotalPages);
            Assert.Equal(5, result.PagingInfo.TotalItems);
            Assert.Equal(3, result.PagingInfo.ItemsPerPage);
        }

        [Fact]
        public void CanFilterProducts()
        {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[]{
                    new Product { ProductId = 1, Name = "P1", Category="C1" },
                    new Product { ProductId = 2, Name = "P2", Category="C2" },
                    new Product { ProductId = 3, Name = "P3", Category="C1" },
                    new Product { ProductId = 4, Name = "P4", Category="C2" },
                    new Product { ProductId = 5, Name = "P5", Category="C3" },
                }
            );

            var productController = new ProductController(mock.Object) {PageSize = 3};
            var result = (productController.List("C1", 1).ViewData.Model as ProductListViewModel).Products.ToArray();

            Assert.True(result[0].Name == "P1");
            Assert.True(result[1].Name == "P3");
            Assert.Equal(2, result.Length);
        }
    }
}
