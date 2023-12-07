namespace CartCraft.Domain.Models
{
    public class ProductDetail : BaseEntity
    {
        public Product Product { get; set; }
        public string ProductId { get; set; }
    }
}