namespace Core.Entites
{
    public class OrderItem : BaseEntity
    {
        public OrderItem(ProductItemOrdereed itemOrdered, decimal price, int quantity)
        {
          
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }

        public int Id { get; set; }

        public ProductItemOrdereed ItemOrdered { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}