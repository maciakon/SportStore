using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportStore.Models
{
    public class EFOrdersRepository : IOrdersRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EFOrdersRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IEnumerable<Order> Orders => _dbContext.Orders.Include(o => o.Lines).ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            _dbContext.AttachRange(order.Lines.Select(l => l.Product));
            if(order.OrderId <= 0)
            {
                _dbContext.Orders.Add(order);
            }
            _dbContext.SaveChanges(); 
        }
    }
}