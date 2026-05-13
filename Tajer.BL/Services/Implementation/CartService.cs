using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tajer.BL.DTO;
using Tajer.BL.DTO.CarDTO;
using Tajer.BL.Services.Interfaces;
using Tajer.DAL.Models;
using Tajer.DAL.Repo.Interfaces;

namespace Tajer.BL.Services.Implementation
{
    public class CartService(IUnitOfWork _unit, IMapper _mapper) : ICartService
    {
        public Task AddToCartAsync(AddToCartDto cartDto, string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ClearCartAsync(string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CartItemDto>> GetCartAsync(string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCartCountAsync(string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<CartSummaryDto> GetCartTotalAsync(string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromCartAsync(int cartItemId, string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateQuantityAsync(UpdateCartQuantityDto cartDto, string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
