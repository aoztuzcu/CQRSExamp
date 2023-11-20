using AutoMapper;
using CQRSExamp.Application.Features.Products.Commonds.ProductCreate;
using CQRSExamp.Application.Features.Products.ViewModels;
using CQRSExamp.Domain;

namespace CQRSExamp.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductCreateCommand>().ReverseMap();
        CreateMap<Product, ProductVM>().ReverseMap();

    }
}
