using System.Collections.Generic;
using System.Threading.Tasks;
using P3.Models.Entities;
using P3.Models.ViewModels;

namespace P3.Models.Services
{
    public interface IOrderService
    {
        void SaveOrder(OrderViewModel order);
        Task<Order> GetOrder(int id);
        Task<IList<Order>> GetOrders();
    }
}
