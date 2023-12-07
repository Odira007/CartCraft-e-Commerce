using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CartCraft.Models.DTOs.Requests
{
    public class ProductRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        // public ICollection<ReviewRequestDto> Reviews { get; set; } = new List<ReviewRequestDto>();
    }
}
