using AutoMapper;
using CartCraft.Application.Interfaces;
using CartCraft.Common.Helpers;
using CartCraft.Domain.Models;
using CartCraft.Domain.Models.DTOs.Responses;
using CartCraft.Infrastructure.Interfaces;
using CartCraft.Models.DTOs.Requests;
using CartCraft.Models.DTOs.Responses;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CartCraft.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApiResponse> CreateProduct(ProductRequestDto productRequestDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productRequestDto);
                await _unitOfWork.Repository<Product>().AddAsync(product);
                await _unitOfWork.CommitAsync();

                _logger.Information($"A new product with Id: {product.Id} was created");

                return ApiResponse<ProductResponseDto>
                    .Success(_mapper.Map<ProductResponseDto>(product), "New product successfully added");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return ApiResponse.Failure("Could not add product");
            }
        }
        public ApiResponse GetAllProducts(int pageSize, int pageNumber)
        {
            try
            {
                var products = _unitOfWork.Repository<Product>().GetAll().ToList();

                var paginated = Utils.Paginate(products, ref pageSize, ref pageNumber);
                var paginatedResponse = new PaginatedResponse<ProductResponseDto>
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = products.Count(),
                    PageItems = _mapper.Map<List<ProductResponseDto>>(paginated)
                };

                return ApiResponse<PaginatedResponse<ProductResponseDto>>
                    .Success(paginatedResponse, "Products retrieved successfully");
            }
            catch(Exception ex)
            {
                _logger.Information(ex, ex.Message);
                return ApiResponse.Failure("Could not retrieve products");
            }
        }

        //public async Task<ApiResponse> CreateReviewByProductId(string productId, ReviewRequestDto reviewRequestDto)
        //{
        //    try
        //    {
        //        var product = await _unitOfWork.Repository<Product>().GetAsync(x => x.Id == productId);
        //        var review = _mapper.Map<Review>(reviewRequestDto);

        //        product.Reviews.Add(review);
        //        await _unitOfWork.CommitAsync();

        //        var response = _mapper.Map<ReviewResponseDto>(review);
        //        return ApiResponse<ReviewResponseDto>
        //            .Success(response, $"Review for product with id:{product.Id} was created");
        //    }
        //    catch(Exception ex)
        //    {
        //        _logger.Error(ex, ex.Message);
        //        return ApiResponse.Failure("Review could not be created");
        //    }
        //}
    }
}
