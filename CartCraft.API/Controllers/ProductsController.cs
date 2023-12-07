using CartCraft.Application.Interfaces;
using CartCraft.Domain.Models.DTOs.Responses;
using CartCraft.Models.DTOs.Requests;
using CartCraft.Models.DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CartCraft.API.Controllers
{
    [Route("api/v{version:apiVersion}/products")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> CreateProduct([FromBody]ProductRequestDto dto)
        {
            //using (var cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
            //{
                var response = await _productService.CreateProduct(dto);
                if (response == null || response.Status == false) return BadRequest(response);
                return Ok(response);
            //}
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResponse<ProductResponseDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public IActionResult GetProducts(int pageSize, int pageNumber)
        {
            var response = _productService.GetAllProducts(pageSize, pageNumber);
            if (response == null || response.Status == false) return BadRequest(response);
            return Ok(response);
        }
    }
}
