namespace KedaiAPI.Models
{
    public class OrderNoRequest
    {
        public int CartId {  get; set; }
        public string Total { get; set; }
    }

    public class CompleteOrderRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string InvoiceNo { get; set; }
    }
}
