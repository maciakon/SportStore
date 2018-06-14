using System.Collections.Generic;
using System.Linq;

namespace SportStore.Models
{
    public class Cart
    {
         
         List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.
            Where(item => item.Product.ProductId == product.ProductId)
            .FirstOrDefault();

            if(line == null)
            {
                lineCollection.Add(new CartLine{Product = product, Quantinty = quantity});
            }
            else
            {
                line.Quantinty++;
            }

        }

        public virtual void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }

        public virtual decimal ComputeTotalValue() => lineCollection.Sum(e => e.Product.Price * e.Quantinty);

        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<CartLine> lines => lineCollection;
    }
}