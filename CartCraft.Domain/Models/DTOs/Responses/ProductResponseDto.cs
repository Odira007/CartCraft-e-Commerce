using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartCraft.Domain.Models.DTOs.Responses
{
    public class ProductResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
