namespace CartCraft.Domain.Models
{
    public class Review : BaseEntity
    {
        public string Header { get; set; }
        public string Content { get; set; }
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
        public Product Product { get; set; }
        public string ProductId { get; set; }
    }
}