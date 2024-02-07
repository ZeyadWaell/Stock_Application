using Api.Dto;
using Api.Extentions;
using Api.ResponseModule;
using AutoMapper;
using Core.Entites;
using Core.Entites.Enum;
using Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Stock_Application.RequestModule;
using Stock_Application.Services.Interfaces;
using Twilio.Http;

namespace Api.Controllers
{

    public class OrderController : BaseController
    {
        private readonly IUniterofWork _uniterofWork;
        private readonly ICurrentUserService _currentUserService;


        public OrderController(IUniterofWork uniterofWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _uniterofWork = uniterofWork;
            _currentUserService = currentUserService;
        }

        [HttpPost("CreateOrder")]
        public async Task<ActionResult<Orders>> CreateOrder([FromQuery] CreateOrderRequest request)
        {
            try
            {
                var stock = await _uniterofWork.StockRepository.GetAsync(x => x.Symbol == request.StockSymbol);
                var priceOrder = await _uniterofWork.OrderRepostory.CheckingCurrentPrice(request.Price, request.StockSymbol);

                var order = new Orders
                {
                    Quantity = request.Quantity,
                    Timestamp = DateTime.Now,
                    OrderType = request.OrderType,
                    Status = OrderStatue.Pendding,
                    Stock = stock,
                    UserId = _currentUserService.UserId
                };

                if (order.OrderType == OrderType.Buy && order.Price == stock.CurrentPrice && stock.Quantity != 0)
                {
                    order.Status = OrderStatue.Approved;
                    await BuyingFromStock(stock, request);
                    await _uniterofWork.Complete();
                    return Ok(new ApiResponse(200, "تم تأكيد طلبك وجاري التنفيذ لأقرب عملية"));
                }
                else if (priceOrder != null)
                {
                    order.Status = OrderStatue.Approved;

                    if (priceOrder.OrderType == OrderType.Buy)
                    {
                        await ProcessBuyingOrderMarket(order, stock, request, priceOrder);
                        return Ok(new ApiResponse(200, "تم تأكيد طلبك بالشراء"));
                    }
                    else if (priceOrder.OrderType == OrderType.Sell)
                    {
                        await ProcessSellingOrderMarket(order, stock, request, priceOrder);
                        return Ok(new ApiResponse(200, "تم تأكيد طلبك بالبيع"));
                    }
                }

                _uniterofWork.OrderRepostory.Add(order);
                await _uniterofWork.Complete();
                return Ok(new ApiResponse(200, "تم تأكيد طلبك وجاري التنفيذ لأقرب عملية"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetialsDto>> GetAllOrderForAllUsers()
        {
            var Orders = await _uniterofWork.OrderRepostory.GetAllAsync();

            return Ok(Orders);
        }
        [HttpGet("GetAllOrdersForUser")]
        public async Task<ActionResult<IReadOnlyList<OrderDetialsDto>>> GetOrdersForUser()
        {
          
            return Ok();
        }


        #region Methods
        private async Task BuyingFromStock(Stock stock, CreateOrderRequest request)
        {
            var userOrder = await _uniterofWork.StockHolderRepostory.GetAsync(x => x.UserId == _currentUserService.UserId && x.Stock.Symbol == request.StockSymbol);

            if (userOrder != null)// if he has already this stock Then Increase the Quantity 
            {
                userOrder.Quantity += request.Quantity;
                _uniterofWork.StockHolderRepostory.Update(userOrder);
                stock.Quantity -= request.Quantity;


            }
            else // If not then hass this stockholder to his Boket
            {
                var stockHolder = new StockHolder
                {
                    Stock = stock,
                    Quantity = request.Quantity,
                    UserId = _currentUserService.UserId
                };

                _uniterofWork.StockHolderRepostory.Add(stockHolder);
                stock.Quantity -= request.Quantity;

            }
        }
        private async Task ProcessBuyingOrderMarket(Orders order, Stock stock, CreateOrderRequest request, Orders priceOrder = null)
        {

            if (order.OrderType == OrderType.Buy && priceOrder.OrderType == OrderType.Buy) // if he is Buying
            {
                order.Status = OrderStatue.Approved;//  buy as it exist 
                var stockHolderPlace = await _uniterofWork.StockHolderRepostory.GetAsync(x => x.UserId == priceOrder.UserId && x.Stock.Symbol == request.StockSymbol);
                var userOrder = await _uniterofWork.StockHolderRepostory.GetAsync(x => x.UserId == _currentUserService.UserId && x.Stock.Symbol == request.StockSymbol);

                if (userOrder != null)
                {
                    userOrder.Quantity += request.Quantity;
                    _uniterofWork.StockHolderRepostory.Update(userOrder);

                }
                else
                {
                    var newUserOrder = new StockHolder
                    {
                        Stock = stock,
                        Quantity = request.Quantity,
                        UserId = _currentUserService.UserId
                    };

                    _uniterofWork.StockHolderRepostory.Add(newUserOrder);
                }

                stockHolderPlace.Quantity -= request.Quantity; // Update StockHolder Quantity
                if (stockHolderPlace.Quantity == 0)
                {
                    _uniterofWork.StockHolderRepostory.Remove(stockHolderPlace);
                    await _uniterofWork.Complete();
                }
                _uniterofWork.StockHolderRepostory.Update(stockHolderPlace);
                _uniterofWork.StockRepository.Update(stock);

            }


        }
        private async Task ProcessSellingOrderMarket(Orders order, Stock stock, CreateOrderRequest request, Orders priceOrder = null)
        {

            var stockHolderPlace = await _uniterofWork.StockHolderRepostory.GetAsync(x => x.UserId == priceOrder.UserId && x.Stock.Symbol == request.StockSymbol);
            var userOrder = await _uniterofWork.StockHolderRepostory.GetAsync(x => x.UserId == _currentUserService.UserId && x.Stock.Symbol == request.StockSymbol);

            if (userOrder != null)
            {
                userOrder.Quantity -= request.Quantity;
                _uniterofWork.StockHolderRepostory.Update(userOrder);

            }
            else
            {
                var newUserOrder = new StockHolder
                {
                    Stock = stock,
                    Quantity = request.Quantity,
                    UserId = _currentUserService.UserId
                };

                _uniterofWork.StockHolderRepostory.Add(newUserOrder);
            }

            stockHolderPlace.Quantity += request.Quantity; // Update StockHolder Quantity

            _uniterofWork.StockHolderRepostory.Update(stockHolderPlace);
            _uniterofWork.StockRepository.Update(stock);

        }
        #endregion

    }
}
