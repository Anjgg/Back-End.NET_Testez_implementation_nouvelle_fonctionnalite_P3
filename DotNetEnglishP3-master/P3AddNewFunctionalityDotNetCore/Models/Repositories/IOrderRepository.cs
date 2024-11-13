using System.Collections.Generic;
using System.Threading.Tasks;
using P3.Models.Entities;

namespace P3.Models.Repositories
{
    public interface IOrderRepository
    {
        void Save(Order order);
        Task<Order> GetOrder(int? id);
        Task<IList<Order>> GetOrders();
    }
}
