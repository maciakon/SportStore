using System.Collections.Generic;

namespace SportStore.Models
{
    public class FakeProductsRepository : IProductsRepository
    {
        public IEnumerable<Product> Products => new List<Product>
        {
            new Product { Name = "Footbal", Price = 25 },
            new Product { Name = "Surf board", Price = 179 },
            new Product { Name = "Running shoes", Price = 95 }
        };
    }
}