using System.Collections.Generic;

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
    }
}