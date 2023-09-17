namespace KedaiAPI.Models
{
    public class OrderRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string InvoiceNo { get; set; }
        public string Total { get; set; }

        public List<int> CartIds { get; set; }
    }
}
