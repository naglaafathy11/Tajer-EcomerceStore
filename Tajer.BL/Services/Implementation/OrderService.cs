using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tajer.BL.DTO.OrderDTO;
using Tajer.BL.Services.Interfaces;
using Tajer.DAL.Enums;
using Tajer.DAL.Models;
using Tajer.DAL.Repo.Implemntation;
using Tajer.DAL.Repo.Interfaces;

namespace Tajer.BL.Services.Implementation
{
    internal class OrderService(IUnitOfWork _unit , IMapper _mapper): IOrderService
    {
        private readonly IOrderRepo _repo = _unit.orderRepo;

        public async Task<OrderDetailsDTO> GetOrderByIdAsync(int orderId, string userId)
        {


            var Order = await _repo.GetOrderWithOrderitemsAndProduct(orderId, userId);
            if (Order == null)
            {
                throw new Exception("Order Not Found");
            }
            else
                return _mapper.Map<OrderDetailsDTO>(Order);
        }

        public async Task<IEnumerable<OrderListDTO>> GetUserOrdersAsync(string userId)
        {
            var Orders = await _repo.GetAllOrderswithUser(userId);
            return _mapper.Map<IEnumerable<OrderListDTO>>(Orders);
        }
        public async Task AdminCancelOrderAsync(int orderId)
        {
            var order = await _repo.GetOrderWithOrderitemsAndProduct(orderId);
            if (order == null)
                throw new Exception("Order Not Found");
            if (order.Status == Status.Cancelled)
                throw new Exception("Order Already Canceled");
            if (order.Status != Status.Shipped &&  order.Status != Status.Pending && order.Status != Status.Processing)
                throw new Exception("Only Pending , Processing and Shipped Orders Can Be Canceled");



            foreach (var item in order.OrderItems)
              {
                  item.Product.StockQuantity += item.Quantity;

                  
              }
            order.Status = Status.Cancelled;
            await _unit.SaveAsync();



        }




        

        public  async Task CancelOrderAsync(int orderId, string userId)
        {
            var order =  await _repo.GetOrderWithOrderitemsAndProduct(orderId, userId);
           if(order== null)
                throw new Exception("Order Not Found");

           if(order.Status != Status.Pending)
                throw new Exception("Only Pending Orders Can Be Canceled");


           foreach(var item in order.OrderItems)
            {
                item.Product.StockQuantity += item.Quantity;
            }
           order.Status = Status.Cancelled;


           await _unit.SaveAsync();


        }

        public async Task<IEnumerable<OrderListDTO>> GetAllOrdersForAdminAsync(string? status, string? search)
        {
            var Orders = await _repo.GetAllOrderswithUser();
            var searchOrder = new List<Order>();
            if (Orders is not null)
            {
            
                foreach(var  order in Orders)
                {
                    if (order.User.Email.Contains(search))
                        searchOrder.Add(order);
                }
            }
           
            return _mapper.Map<IEnumerable<OrderListDTO>>(searchOrder);

        }

      

        public async Task<OrderDetailsDTO> PlaceOrderAsync(string userId, CreateOrderDTO dto)
        {
            

          throw new NotImplementedException();
        }

        public async Task UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDTO dto)
        {
            var order =  await _repo.GetOrderWithOrderitemsAndProduct(orderId);

            if(order == null)
                throw new Exception("Order Not Found");

            if(order.Status == Status.Cancelled)
                throw new Exception("Canceled Orders Can't Be Updated");

            if(order.Status == Status.Delivered)
                throw new Exception("Delivered Orders Can't Be Updated");
            bool ValaideTransition = false;

            if (order.Status == Status.Pending && dto.NewStatus == Status.Processing)
            {
                ValaideTransition = true;
            }
            else if(order.Status == Status.Processing && dto.NewStatus == Status.Shipped)
            {
                ValaideTransition = true;
            }
            else if (order.Status == Status.Shipped && dto.NewStatus == Status.Delivered)
            {
                ValaideTransition = true;
            }

            if(!ValaideTransition)
                throw new Exception("Invalid Status Transition");

            if(dto.NewStatus == Status.Shipped)
            {
                if(string.IsNullOrWhiteSpace(dto.TrackingNumber))
                    throw new Exception("Tracking Number Is Required When Shipping An Order");
                order.TrackingNumber = dto.TrackingNumber;

            }

            order.Status = dto.NewStatus;

             await _unit.SaveAsync();



        }
    }
}
