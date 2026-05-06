using AutoMapper;
using Tajer.BL.DTO;
using Tajer.DAL.Models;
namespace Tajer.BL.Services.MappingProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }

    }
}


