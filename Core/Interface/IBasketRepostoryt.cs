using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IBasketRepostoryt
    {
        Task<CustumerBasket> GetBasketAsync(string Id);

        Task<CustumerBasket> UpdateBasketAsync(CustumerBasket basket);
        Task DeleteBasketAsynbc(string Id);
    }
}
