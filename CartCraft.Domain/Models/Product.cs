using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartCraft.Domain.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductDetail ProductDetail { get; set; }
        public Seller Seller{ get; set; }
        public string SellerId { get; set; }
        public Category Category { get; set; }
        public string CategoryId { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
