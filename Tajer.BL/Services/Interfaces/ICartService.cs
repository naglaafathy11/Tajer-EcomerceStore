using System;
using System.Collections.Generic;
using System.Text;
using Tajer.BL.DTO;
using Tajer.BL.DTO.CarDTO;

namespace Tajer.BL.Services.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemDto>> GetCartAsync(string UserId);
        Task<int> GetCartCountAsync(string UserId);
        Task AddToCartAsync(AddToCartDto cartDto, string UserId);
        Task<bool> UpdateQuantityAsync(UpdateCartQuantityDto cartDto, string UserId);
        Task<bool> RemoveFromCartAsync(int cartItemId, string UserId);
        Task<bool> ClearCartAsync(string UserId);
        Task<CartSummaryDto> GetCartTotalAsync(string UserId);
    }
}
