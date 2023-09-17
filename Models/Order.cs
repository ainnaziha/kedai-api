namespace KedaiAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string OrderNo { get; set; }
        public string? InvoiceNo { get; set; }
        public string? Total { get; set; }
        public bool IsPaid { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Street { get; set; }
        public string? Town { get; set; }

        public ICollection<Cart> Carts { get; set; }

    }
}