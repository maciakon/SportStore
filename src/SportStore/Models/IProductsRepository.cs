using System.Collections.Generic;

namespace SportStore.Models
{
    public interface IProductsRepository
    {
         IEnumerable<Product> Products { get; }
         void Save(Product product);
    }
}