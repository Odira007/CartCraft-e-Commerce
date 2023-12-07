using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CartCraft.Models.DTOs.Requests
{
    public class ImageRequestDto
    {
        [Required]
        public IFormFile ImagePath { get; set; }
        public string ProductId { get; set; }
    }
}