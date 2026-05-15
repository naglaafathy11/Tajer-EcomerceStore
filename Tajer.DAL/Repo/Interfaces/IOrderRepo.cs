using System;
using System.Collections.Generic;
using System.Text;

namespace Tajer.DAL.Repo.Interfaces
{
    public interface IOrderRepo :IGenaricRepo<Order , int>
    {
        Task<Order> GetOrderWithOrderitemsAndProduct(int orderId , string UserId);
        Task<Order> GetOrderWithOrderitemsAndProduct(int orderId);

        Task<IEnumerable<Order>> GetAllOrderswithUser(string UserId);
        Task<IEnumerable<Order>> GetAllOrderswithUser();




    }
}
