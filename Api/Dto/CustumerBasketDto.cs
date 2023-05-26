using Core.Entites;

namespace Api.Dto
{
    public class CustumerBasketDto
    {
        public string Id { get; set; }

        public int? DeleiverMethod { get; set; }

        public decimal ShippingPrice { get; set; }

        public List<BasketItemDto> BasketItems { get; set; }
    }
}
