using System;
using System.Collections.Generic;
using System.Text;
using Tajer.DAL.Data;
using Tajer.DAL.Repo.Interfaces;

namespace Tajer.DAL.Repo.Implemntation
{
    public class OrderRepo:GenaricRepo<Order, int> , IOrderRepo
    {
        private readonly AppDbContext _context;
        public OrderRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrderswithUser(string UserId)
        {
           var Orders = await  _context.Orders.Where(o=>o.UserId == UserId).Include(o => o.User).ToListAsync();
            return Orders;
        }

        public async Task<IEnumerable<Order>> GetAllOrderswithUser()
        {
            var Orders = await _context.Orders.Include(o => o.User).ToListAsync();
            return Orders;
        }

        public  async Task<Order> GetOrderWithOrderitemsAndProduct(int orderId , string UserId)
        {
            var Order = await _context.Orders.Include(o => o.OrderItems).ThenInclude(o => o.Product).Include(o => o.User).FirstOrDefaultAsync(o => o.Id == orderId  && o.UserId == UserId);
            if (Order == null)
                return null;
            else
            return Order;
        }

        public async Task<Order> GetOrderWithOrderitemsAndProduct(int orderId)
        {
            var Order = await _context.Orders.Include(o => o.OrderItems).ThenInclude(o => o.Product).FirstOrDefaultAsync(o => o.Id == orderId );
            if (Order == null)
                return null;
            else
                return Order;
        }
    }
}
