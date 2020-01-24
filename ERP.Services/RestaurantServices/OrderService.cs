using ERP.Data;
using ERP.Data.Entities.Restaurant;
using ERP.Services.RestaurantServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Services.RestaurantServices
{
    public class OrderService:IOrderService
    {
        private readonly ERPDbContext _context;

        public OrderService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<System.Object>> GetOrder()
        {
            var result = (from O in _context.Orders
                         join C in _context.Customers on O.CustomerID equals C.Id
                         select new
                         {
                             O.Id,
                             O.OrderNo,
                             Customer=C.Name,
                             O.PMethod,
                             O.GTotal

                         }).ToListAsync();
            return await result;
        }
        public async Task<System.Object> GetOrderById(int id)
        {
            var order = (from a in _context.Orders
                         where a.Id == id
                         select new
                         {
                             a.Id,
                             a.OrderNo,
                             a.CustomerID,
                             a.PMethod,
                             a.GTotal,
                             DeletedOrderItemsIds=""
                         }).FirstOrDefaultAsync();

            
            return await order;
        }

        public async Task<IEnumerable<System.Object>> GetOrderDetailsById(int id)
        {
            try
            {
                var orderDetails = await (from OI in _context.OrderItems
                                          join I in _context.Items on OI.ItemID equals I.Id
                                          where OI.OrderID == id

                                          select new
                                          {
                                              OI.OrderID,
                                              OI.Id,
                                              OI.ItemID,
                                              ItemName = I.Name,
                                              I.Price,
                                              OI.Quantity,
                                              Total = OI.Quantity * I.Price
                                          }).ToListAsync();
                return orderDetails.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IEnumerable<Order>> GetAllOrderByCustomer(int customerId)
        {
            var result = _context.Orders.Where(x=>x.CustomerID==customerId).AsNoTracking().ToListAsync();
            return await result;
        }

        
        public async Task<long> SaveOrder(Order order)
        {
            try
            {
                Order data = new Order
                {
                    Id =0,
                    OrderNo = order.OrderNo,
                    CustomerID = order.CustomerID,
                    PMethod = order.PMethod,
                    GTotal = order.GTotal
                };
                if (order.Id == 0)
                {
                    _context.Orders.Add(data);
                }
                else
                {
                    data.Id = order.Id;
                    _context.Entry(data).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
                int orderid = data.Id;

                //Item Details
                foreach (var item in order.OrderItems)
                {
                    item.OrderID = orderid;
                    if (item.Id <= 0)
                    {
                        item.Id = 0;
                        _context.OrderItems.Add(item);
                    }
                    else
                    {
                        _context.Entry(item).State = EntityState.Modified;
                    }
                }
                //Delete Order Items
                foreach(var id in order.DeletedOrderItemsIds.Split(',').Where(x => x != ""))
                {
                    OrderItem x = _context.OrderItems.Find(Convert.ToInt32(id));
                    _context.OrderItems.Remove(x);
                }
                await _context.SaveChangesAsync();

                return data.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<Order> DeleteOrderById(int id)
        {
            Order order =await _context.Orders.Include(y => y.OrderItems)
                .SingleOrDefaultAsync(x => x.Id == id);

            foreach(var item in order.OrderItems.ToList())
            {
                _context.OrderItems.Remove(item);
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<OrderItem> GetOrderItemById(int id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemByItemId(int itemId)
        {
            var result = _context.OrderItems.Where(x => x.ItemID == itemId).AsNoTracking().ToListAsync();
            return await result;
        }

        public async Task<bool> SaveOrderItem(List<OrderItem> orderItems)
        {
            try
            {
                 _context.OrderItems.AddRange(orderItems);

                return 1 == await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> SaveOrderItem(OrderItem orderItems)
        {
            try
            {
                if (orderItems.Id > 0)
                    _context.OrderItems.Update(orderItems);
                else
                    _context.OrderItems.Add(orderItems);

               return 1 == await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteOrderItemById(int id)
        {
            _context.OrderItems.Remove(_context.OrderItems.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Customer>> GetAllCustomer()
        {
            var result = _context.Customers.AsNoTracking().ToListAsync();
            return await result;
        }

        public async Task<IEnumerable<Item>> GetAllItem()
        {
            var result = _context.Items.AsNoTracking().ToListAsync();
            return await result;
        }
    }
}
