using AutoMapper;
using CartCraft.Domain.Models;
using CartCraft.Domain.Models.DTOs.Responses;
using CartCraft.Models.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartCraft.Application.Profiles
{
    public class ProductProfiles : Profile
    {
        public ProductProfiles()
        {
            CreateMap<Product, ProductResponseDto>();
            CreateMap<ProductRequestDto, Product>();
            CreateMap<ProductRequestDto, Product>();
        }
    }
}
