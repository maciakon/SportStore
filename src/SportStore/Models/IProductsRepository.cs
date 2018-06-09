using System.Collections.Generic;

namespace SportStore.Models
{
    public interface IProductsRepository
    {
         IEnumerable<Product> Products { get; }
    }
}