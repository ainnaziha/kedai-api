namespace KedaiAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int Quantities { get; set; }
        public bool isDeleted   { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }


        public virtual Product Product { get; set; }
        public virtual User User { get; set; }

    }
}
