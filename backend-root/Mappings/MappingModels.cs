using System;
using AutoMapper;
using backend_root.DTOs;
using backend_root.Models;

namespace backend_root.Data;

public class MappingModels : Profile
{
    public MappingModels()
    {
        CreateMap<User, UserDto>().ReverseMap();
        //CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Region, RegionDto>()
        .ForMember(dest => dest.Product, opt => opt.Ignore())
        .ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<SalesTransactionDto, SalesTransaction>().ForMember(dest => dest.Product, opt => opt.Ignore()).ReverseMap();
        CreateMap<ProductDto, Product>().ForMember(dest => dest.Region, opt => opt.Ignore())
            .ForMember(dest => dest.SalesTransactions, opt => opt.Ignore())
        .ReverseMap();
    }
}
