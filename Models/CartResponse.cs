namespace KedaiAPI.Models
{
    public class CartItemResponse
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string Subtotal { get; set; }
    }

    public class CartResponse
    {
        public string Total { get; set; }
        public List<CartItemResponse> Items { get; set; }
    }
}