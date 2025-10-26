using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionModule.Domain;

namespace TransactionModule.Context.Mapping;

public class UserTransactionMapping : IEntityTypeConfiguration<UserTransaction>
{
    public void Configure(EntityTypeBuilder<UserTransaction> builder)
    {
        builder.ToTable("Transactions", "dbo");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Authority)
            .IsRequired(false)
            .IsUnicode(false)
            .HasMaxLength(500);

        builder.Property(b => b.CardPan)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder.Property(b => b.PaymentGateway)
            .HasMaxLength(400);
    }
}
