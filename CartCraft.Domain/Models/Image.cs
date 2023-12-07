using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartCraft.Domain.Models
{
    public class Image : BaseEntity
    {
        [NotMapped]
        public IFormFile ImagePath { get; set; }
        public Product Product { get; set; }
        public string ProductId { get; set; }
    }
}
