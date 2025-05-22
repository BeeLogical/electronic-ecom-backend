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
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<SalesTransaction, SalesTransactionDto>().ReverseMap();
    }
}
