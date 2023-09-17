namespace KedaiAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int? OrderId { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
        public Order Order { get; set; }
    }
}