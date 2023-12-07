using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartCraft.Domain.Models.DTOs.Responses
{
    public class PaginatedResponse<T>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public List<T> PageItems { get; set; }
    }
}
