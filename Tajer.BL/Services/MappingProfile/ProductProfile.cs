using AutoMapper;
using Tajer.BL.DTO;
using Tajer.DAL.Models;
namespace Tajer.BL.Services.MappingProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(des=>des.Reviews ,opt=>opt.MapFrom(src=>src.Reviews.Select(r=>r.Comment).ToList()))
                .ReverseMap();
               
        }

    }
}


