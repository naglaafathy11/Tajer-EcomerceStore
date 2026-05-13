using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tajer.BL.DTO;
using Tajer.BL.DTO.CarDTO;
using Tajer.BL.Services.Interfaces;
using Tajer.DAL.Models;
using Tajer.DAL.Repo.Implemntation;
using Tajer.DAL.Repo.Interfaces;

namespace Tajer.BL.Services.Implementation
{
    public class CartService(IUnitOfWork _unit, IMapper _mapper) : ICartService
    {

         private readonly ICartRepo _repo = _unit.CartRepo ;
        public async Task AddToCartAsync(AddToCartDto cartDto, string UserId)
        {
            var Product = await _unit.GetRepo<Product,int>().GetById(cartDto.ProductId) ;

            if (Product is null || !Product.IsActive) 
                throw new Exception("Product Not Found");
            if (Product.StockQuantity <= 0)
                throw new Exception("Product is Out of Stock");
            var CartItem = await _repo.GetAll(c => c.UserId == UserId && c.ProductId == cartDto.ProductId);
            var Item = CartItem.FirstOrDefault();
            if (Item is not null)
            {
                if ((Item.Quantity + cartDto.Quantity) > Product.StockQuantity)
                    throw new Exception("Quantity Exceeds Stock Quantity");
                else
                {
                    Item.Quantity += cartDto.Quantity;
                    _repo.Update(Item);
                    await _unit.SaveAsync();
                }
            }
            else
            {
                var NewCartItem = _mapper.Map<ShoppingCartItem>(cartDto);
                NewCartItem.UserId = UserId;
                await _repo.Add(NewCartItem);
                await _unit.SaveAsync();

            }
        }

        public async Task<bool> ClearCartAsync(string UserId)
        {
            var Cartitems = await _repo.GetAllWithProduct(c => c.UserId == UserId);
            foreach (var cartItem in Cartitems)
            {
                _repo.Delete(cartItem.Id);

            }

            return await _unit.SaveAsync() > 0;

        }

        public async Task<IEnumerable<CartItemDto>> GetCartAsync(string UserId)
        {
            var Cartitems = await _repo.GetAllWithProduct(c=>c.UserId == UserId );

            return  _mapper.Map<IEnumerable<CartItemDto>>(Cartitems);

           
        }

        public async Task<int> GetCartCountAsync(string UserId)
        {
            var Cartitems = await _repo.GetAll(p=>p.UserId == UserId );

            
            return Cartitems.Count();

        }

        public async Task<CartSummaryDto> GetCartTotalAsync(string UserId)
        {
            var CartItems = await _repo.GetAllWithProduct(c => c.UserId == UserId);
            var Summary = new CartSummaryDto();
            foreach (var cartItem in CartItems)
            {
                var product = cartItem.Product;
                var finalPrice = product.DiscountPrice ?? product.Price;
                Summary.SubTotal += finalPrice * cartItem.Quantity;
                if (product.DiscountPrice.HasValue)
                {
                    Summary.DiscountAmount +=
                        (product.Price - product.DiscountPrice.Value)
                        * cartItem.Quantity;
                }
            }
            Summary.Total = Summary.SubTotal - Summary.DiscountAmount;
            return Summary;
        }


        public async Task<bool> RemoveFromCartAsync(int cartItemId, string UserId)
        {
            var cartItem = await _repo.GetAll(c=>c.UserId == UserId && c.Id == cartItemId);
            if (cartItem is null || !cartItem.Any())
                return false;
             _repo.Delete(cartItem.FirstOrDefault()!.Id);

            return await _unit.SaveAsync() >0;

        }

        public async Task<bool> UpdateQuantityAsync(UpdateCartQuantityDto cartDto, string UserId)
        {
            var CartItem = await _repo.GetAll(c => c.UserId == UserId && c.Id == cartDto.CartItemId);
            if (CartItem is null || !CartItem.Any())
                return false;
            var Item = CartItem.FirstOrDefault()!;
            if (cartDto.Quantity <= 0)
                _repo.Delete(Item.Id);
            var shoppingItem = await _unit.GetRepo<Product , int>().GetById(cartDto.ProductId);
            if (cartDto.Quantity > shoppingItem.StockQuantity)
                throw new Exception("Quantity Exceeds Stock Quantity");
            Item.Quantity = cartDto.Quantity;
            _repo.Update(Item);
            return await _unit.SaveAsync() > 0;
        }
    }
}
