namespace SportStore.Models
{
    public class CartLine
    {
        public int CartlineId { get; set; }
        public Product Product { get; set; }
        public int Quantinty { get; set; }
    }
}