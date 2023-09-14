namespace KedaiAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool IsCompleted { get; set; }

        public ICollection<CartItem> CartItems { get; } = new List<CartItem>();
    }
}