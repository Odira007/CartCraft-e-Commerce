using CartCraft.Domain.Models.DTOs.Responses;
using CartCraft.Models.DTOs.Requests;
using CartCraft.Models.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CartCraft.Application.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponse> CreateProduct(ProductRequestDto productRequestDto);
        ApiResponse GetAllProducts(int pageSize, int pageNumber);
    }
}
