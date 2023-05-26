using Api.Dto;
using Api.Extentions;
using Api.ResponseModule;
using AutoMapper;
using Core.Entites;
using Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    public class OrderController : BaseController
    {
        private readonly IOrderServices orderServices;
        private readonly IMapper mapper;

        public OrderController(IOrderServices orderServices, IMapper mapper)
        {
            this.orderServices = orderServices;
            this.mapper = mapper;
        }

        [HttpPost("CreateOrder")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetriveEmailPrincepal();
            var address = this.mapper.Map<ShippingAddress>(orderDto.Address);
            var order = await this.orderServices.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null)
                return BadRequest(new ApiResponse(400, "Problem when creating Order"));

            return Ok(order);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetialsDto>> GetOrderByIDForUser(int id)
        {
            var email = HttpContext.User.RetriveEmailPrincepal();
            var order = await this.orderServices.GetOrderByIdAsync(id, email);

            if (order is null)
                return NotFound(new ApiResponse(400, "This Order Doesn't Exist"));

            return Ok(this.mapper.Map<OrderDetialsDto>(order));
        }
        [HttpGet("GetAllOrdersForUser")]
        public async Task<ActionResult<IReadOnlyList<OrderDetialsDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetriveEmailPrincepal();
            var order = await this.orderServices.GetOrderForUserAsync( email);

            return Ok(this.mapper.Map<IReadOnlyList<OrderDetialsDto>>(order));
        }
        [HttpGet("DelverMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliverMethod>>> GetDeliveryMethods()
       => Ok(await this.orderServices.GetDeliverMethodsAsync());
    }
}
