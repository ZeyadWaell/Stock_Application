using Api.Dto;
using AutoMapper;
using AutoMapper.Execution;
using Core.Entites;

namespace Api.Helpers
{
    //public class OrderUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    //{
    //    private readonly IConfiguration _configuration;

    //    public OrderUrlResolver(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }
    //    public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
    //    {
    //        if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
    //            return _configuration["ApiUrl"] + source.ItemOrdered.PictureUrl;


    //        return null;
    //    }
    //}
}
