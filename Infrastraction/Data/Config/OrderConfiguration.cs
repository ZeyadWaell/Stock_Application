using Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastraction.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(order => order.ShippmendToAddress, a => a.WithOwner());
            builder.Property(s => s.OrderStatus).HasConversion(
                st => st.ToString(),
                value => (OrderStatus)Enum.Parse(typeof(OrderStatus), value)
                );
            builder.HasMany(order=>order.OrderItem).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
