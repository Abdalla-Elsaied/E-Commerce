using DomainLayer.Entities.Order_Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Data.Config
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner()); //becouse it is not table 
            builder.Property(o=>o.Status)
                .HasConversion(
                Ostatus=> Ostatus.ToString(),
                Ostatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), Ostatus));
            builder.Property(o=>o.Subtotal).HasColumnType("decimal(18,2)");
       
        }
    }
}
