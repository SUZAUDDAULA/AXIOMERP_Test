using ERP.Data.Entities.Restaurant;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Services.RestaurantServices.Interfaces
{
    public interface IOrderService
    {
        Task<long> SaveOrder(Order order);
        Task<IEnumerable<System.Object>> GetOrder();
        Task<System.Object> GetOrderById(int id);
        Task<IEnumerable<System.Object>> GetOrderDetailsById(int id);
        Task<IEnumerable<Order>> GetAllOrderByCustomer(int customerId);
        Task<Order> DeleteOrderById(int id);
        Task<bool> SaveOrderItem(List<OrderItem> orderItems);
        Task<bool> SaveOrderItem(OrderItem orderItems);
        Task<OrderItem> GetOrderItemById(int id);
        Task<IEnumerable<OrderItem>> GetAllOrderItemByItemId(int itemId);
        Task<bool> DeleteOrderItemById(int id);


        Task<IEnumerable<Customer>> GetAllCustomer();
        Task<IEnumerable<Item>> GetAllItem();
    }
}
