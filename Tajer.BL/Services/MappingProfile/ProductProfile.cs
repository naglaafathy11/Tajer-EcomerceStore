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
            // ─── Product ────────────────────────────────────────────────────────
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName,
                           opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.Reviews,
                           opt => opt.MapFrom(src => src.Reviews != null
                               ? src.Reviews.Select(r => r.Comment).ToList()
                               : new List<string>()))
                .ForMember(dest => dest.AddedAt,
                           opt => opt.MapFrom(src => src.CreatedAt.ToString("dd MMM yyyy")))
                .ForMember(dest => dest.UpdatedAt,
           opt => opt.MapFrom(src => src.UpdatedAt == default
               ? null
               : src.UpdatedAt.ToString("dd MMM yyyy")));

            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore())
                .ForMember(dest => dest.ShoppingCartItems, opt => opt.Ignore())
                .ForMember(dest => dest.Wishlis, opt => opt.Ignore());

            // ─── Category ───────────────────────────────────────────────────────
            CreateMap<Category, CategoryDTO>().ReverseMap();

            // ─── ShoppingCart ───────────────────────────────────────────────────
            CreateMap<ShoppingCartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(s => s.Product.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(s => s.Product.ImageUrl))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(s => s.Product.Price))
                .ForMember(dest => dest.DiscountPrice, opt => opt.MapFrom(s => s.Product.DiscountPrice))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(s => (s.Product.DiscountPrice ?? s.Product.Price) * s.Quantity))
                .ForMember(dest => dest.CartItemId, opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(s => s.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(s => s.Quantity));

            CreateMap<ShoppingCartItem, AddToCartDto>();

            // ─── Order ──────────────────────────────────────────────────────────
            CreateMap<Order, OrderListDTO>();

            CreateMap<Order, OrderDetailsDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(o => o.User.UserName));
        }
    }
}