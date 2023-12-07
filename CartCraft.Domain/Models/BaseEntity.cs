using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartCraft.Domain.Models
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
