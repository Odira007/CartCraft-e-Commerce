using System.ComponentModel.DataAnnotations;

namespace CartCraft.Models.DTOs.Requests
{
    public class ReviewRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string Header { get; set; }
        [Required]
        public string Content { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
    }
}