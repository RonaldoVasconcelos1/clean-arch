using Application.DTOs;
using Application.Products.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductDTO, ProductCreateCommand>().ReverseMap();
        CreateMap<ProductDTO, ProductUpdateCommand>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();

    }
}