using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specfication
{
    public class OrdersWithItemsAndSoecfication : BaseSpecfication<Order>
    {
        public OrdersWithItemsAndSoecfication(string email) : base(order=>order.BuyerEmail==email)
        {
            AddInclude(order => order.OrderItem);
            AddInclude(order => order.DeliverMethod);
            AddOrderByDecending(orderby => orderby.OrderDate);

        }
        public OrdersWithItemsAndSoecfication(int id,string email)
            : base(order => order.BuyerEmail == email && order.Id==id)
        {
            AddInclude(order => order.OrderItem);
            AddInclude(order => order.DeliverMethod);

        }
    }
}
