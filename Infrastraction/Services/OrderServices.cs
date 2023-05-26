using Core.Entites;
using Core.Interface;
using Core.Specfication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastraction.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IBasketRepostoryt _baskerrespostory;
        private readonly IUniterofWork _uniteofwork;

        public OrderServices(IBasketRepostoryt baskerrespostory, IUniterofWork uniteofwork)
        {
            _baskerrespostory = baskerrespostory;
            _uniteofwork = uniteofwork;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, ShippingAddress address)
        {
            var basket = await _baskerrespostory.GetBasketAsync(basketId);
            var items = new List<OrderItem>();
            foreach (var item in basket.BasketItems)
            {
                var productitem = await _uniteofwork.Repostory<Product>().GetByIdAsync(item.Id);

                var itemOrdered = new ProductItemOrdereed(productitem.Id, productitem.Name, productitem.PictureUrl);

                var orderItem = new OrderItem(itemOrdered, productitem.Price, item.Quanitity);

                items.Add(orderItem);
            }

            var deleveryMethod = await _uniteofwork.Repostory<DeliverMethod>().GetByIdAsync(deliveryMethodId);
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            //creating teh order now
            var order = new Order(buyerEmail, address, deleveryMethod, items, subtotal);
            _uniteofwork.Repostory<Order>().Add(order);
            var result = await _uniteofwork.Complete();
            if (result <= 0)
                return null;
            //Delete Basket
            await _baskerrespostory.DeleteBasketAsynbc(basketId);   
            return order;
        }

        public async Task<IReadOnlyList<DeliverMethod>> GetDeliverMethodsAsync()
     => await _uniteofwork.Repostory<DeliverMethod>().GetAllAsync();
        public async Task<Order> GetOrderByIdAsync(int id, string buyerId)
        {
            var orderSpec = new OrdersWithItemsAndSoecfication(id, buyerId);
            return await _uniteofwork.Repostory<Order>().GetEntityWithSpecfictions(orderSpec);
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var orderSpec = new OrdersWithItemsAndSoecfication(buyerEmail);
            return await _uniteofwork.Repostory<Order>().ListAsync(orderSpec);
        }
    }
}
