namespace KedaiAPI.Models
{
    public class CartItemResponse
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Subtotal { get; set; }
    }

    public class CartResponse
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public List<CartItemResponse> Items { get; set; }
    }
}