using System.Collections.Generic;

namespace SportStore.Models
{
    public interface IOrdersRepository
    {
         IEnumerable<Order> Orders { get; }
         void SaveOrder(Order order);
    }
}