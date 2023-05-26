using Api.Dto;
using AutoMapper;
using Core.Entites;
using Core.Entites.Identity;

namespace Api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(des => des.ProductBrand, option => option.MapFrom(src => src.ProductBrand.Name))
                 .ForMember(des => des.ProductType, option => option.MapFrom(src => src.ProductType.Name))
                 .ForMember(des => des.PictureUrl, option => option.MapFrom<ProductUrlResolver>());

            CreateMap<CustumerBasket, CustumerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<Address,AddressDto>().ReverseMap();
            CreateMap<ShippingAddress, ShippingAddressDto>().ReverseMap();
            CreateMap<Order, OrderDetialsDto>()
                .ForMember(des => des.DeliverMethod, option => option.MapFrom(src => src.DeliverMethod.ShortName))
                .ForMember(des => des.ShippingPrice, option => option.MapFrom(src => src.DeliverMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(des => des.ProductId, option => option.MapFrom(src => src.ItemOrdered.ProductItemId))
                .ForMember(des => des.ProductName, option => option.MapFrom(src => src.ItemOrdered.ProductName))
                .ForMember(des => des.PictureUrl, option => option.MapFrom(src => src.ItemOrdered.PictureUrl))
                .ForMember(des => des.PictureUrl, option => option.MapFrom<OrderUrlResolver>());
        }
    }
}
