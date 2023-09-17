namespace KedaiAPI.Models
{
    public class CartRequest
    {
        public int? ProductId {  get; set; }
        public int? CartId { get; set; }
        public int? Quantity { get; set; }
    }
}
