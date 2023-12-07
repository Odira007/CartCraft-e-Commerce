using System.Collections.Generic;

namespace CartCraft.Domain.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}