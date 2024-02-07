using Api.Dto;
using Api.Helpers;
using Api.ResponseModule;
using AutoMapper;
using Core.Entites;
using Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock_Application.ResponseModule;

namespace Api.Controllers
{
    public class ProductController : BaseController
    {
        //private readonly IProductRepostory _productRepostory;
        private readonly IUniterofWork _uniteofWork;

        private readonly IMapper _mapper;

        public ProductController(IMapper mapper,IUniterofWork uniteofWork)
        {
            _uniteofWork = uniteofWork;
        }

        [HttpGet("GetStock")]
        public async Task<ActionResult<IReadOnlyList<Stock>>> GetProducts()
        {
            var Stocks = await _uniteofWork.StockRepository.GetAllAsync();

            return Ok(Stocks);
        }

        [HttpGet("UpdateStock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Stock>> UpdateStock([FromQuery] StockResponce re)
        {
            var Stock = new Stock()
            {
                Symbol = re.Symbol,
                Id = Guid.NewGuid(),
                Quantity = re.Quantity,
                Timestamp = DateTime.Now,
            };

            _uniteofWork.StockRepository.Add(Stock);
            await _uniteofWork.Complete();
            return Ok();
        }


    }
}
