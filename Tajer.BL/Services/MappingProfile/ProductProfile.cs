using AutoMapper;
using Tajer.BL.DTO;
using Tajer.BL.DTO.CarDTO;
using Tajer.BL.DTO.OrderDTO;
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


            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<ShoppingCartItem, CartItemDto>()
                .ForMember(src => src.ProductName, opt => opt.MapFrom(s => s.Product.Name))
                .ForMember(src => src.ImageUrl, opt => opt.MapFrom(s => s.Product.ImageUrl))
                .ForMember(src => src.Price, opt => opt.MapFrom(s => s.Product.Price))
                .ForMember(src => src.DiscountPrice, opt => opt.MapFrom(s => s.Product.DiscountPrice))
                .ForMember(src => src.Total, opt => opt.MapFrom(s => (s.Product.DiscountPrice ?? s.Product.Price) * s.Quantity))
                .ForMember(src => src.CartItemId, opt => opt.MapFrom(s => s.Id))
                .ForMember(src => src.ProductId, opt => opt.MapFrom(s => s.ProductId))
                .ForMember(src => src.Quantity, opt => opt.MapFrom(s => s.Quantity))
                .ReverseMap();



            CreateMap<ShoppingCartItem, AddToCartDto>();


            CreateMap<Order, OrderListDTO>();

            CreateMap<Order, OrderDetailsDTO>()
            .ForMember(src=> src.UserName , opt=>opt.MapFrom(o=>o.User.UserName));

        }

    }
}


