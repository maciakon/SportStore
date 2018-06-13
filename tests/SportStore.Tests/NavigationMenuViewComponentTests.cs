using Xunit;
using Moq;
using SportStore.Models;
using SportStore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace SportStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void CanSelectCategories()
        {
        //Given
        Mock<IProductsRepository> repository = new Mock<IProductsRepository>();
        repository.Setup(m => m.Products).Returns(
                new Product[]{
                    new Product { ProductId = 1, Name = "P1", Category="C1" },
                    new Product { ProductId = 2, Name = "P2", Category="C2" },
                    new Product { ProductId = 3, Name = "P3", Category="C1" },
                    new Product { ProductId = 4, Name = "P4", Category="C2" },
                    new Product { ProductId = 5, Name = "P5", Category="C3" },
                }
            );
        NavigationMenuViewComponent target = new NavigationMenuViewComponent(repository.Object);
        //When
        
        string[] results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();
        //Then
        Assert.True(Enumerable.SequenceEqual(new string[]{"C1", "C2", "C3"}, results));
        }
    }
}