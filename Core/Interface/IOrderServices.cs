using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IOrderServices
    {
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, ShippingAddress address);
        Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail);
        Task<Order> GetOrderByIdAsync(int id,string buyerId);
        Task<IReadOnlyList<DeliverMethod>> GetDeliverMethodsAsync();
    }
}
