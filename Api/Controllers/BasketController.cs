using AutoMapper;
using Core.Entites;
using Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IBasketRepostoryt _basketRepostoryt;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepostoryt basketRepostoryt,IMapper mapper)
        {
            _basketRepostoryt = basketRepostoryt;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustumerBasket>> GetBaskerById(string id)
        {
            var basket = await _basketRepostoryt.GetBasketAsync(id);
            return Ok(basket ?? new CustumerBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustumerBasket>> UpdateBasket(CustumerBasket custumerBasket)
        {
            var baket = _mapper.Map<CustumerBasket>(custumerBasket);

            var updatedBasket = await _basketRepostoryt.UpdateBasketAsync(baket);

            return Ok(updatedBasket);
        }
        [HttpDelete]
        public async Task DeleteBasketById(string id)
            => await _basketRepostoryt.DeleteBasketAsynbc(id);
    }
}
