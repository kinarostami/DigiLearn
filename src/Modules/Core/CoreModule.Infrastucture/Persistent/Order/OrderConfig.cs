using CoreModule.Domain.Order.Models;
using CoreModule.Infrastucture.Persistent.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Infrastucture.Persistent.Order;

internal class OrderConfig : IEntityTypeConfiguration<Domain.Order.Models.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Order.Models.Order> builder)
    {
        builder.ToTable("Orders");
        builder.Property(b => b.DiscountCode)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.OwnsMany(b => b.OrderItems, (t) =>
        {
            t.ToTable("OrderItems");

            t.HasOne<Domain.Course.Models.Course>()
                .WithMany()
                .HasForeignKey(b => b.CourseId);
        });


        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(b => b.UserId);
    }
}
