using System.Collections.Generic;

namespace CartCraft.Domain.Models
{
    public class Seller : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}