using System.Collections.Generic;
using System.Linq;

namespace SportStore.Models
{
    public class EFProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EFProductsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Product> Products => _dbContext.Products;

        public void Save(Product product)
        {
            if(product.ProductId == 0)
            {
                _dbContext.Products.Add(product);
            }
            else
            {
                var existingProduct = _dbContext.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if(existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Category = product.Category;
                    existingProduct.Price = product.Price;
                }
            }
            _dbContext.SaveChanges();
        }

        public Product Delete(int productId)
        {
            var existingProduct = _dbContext.Products.FirstOrDefault(p => p.ProductId == productId);
            if(existingProduct != null)
            {
                _dbContext.Remove(existingProduct);
                _dbContext.SaveChanges();
            }
            return existingProduct;
        }
        
    }
}