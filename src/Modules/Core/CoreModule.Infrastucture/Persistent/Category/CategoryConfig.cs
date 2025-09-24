using CoreModule.Domain.Category.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Infrastucture.Persistent.Category;

public class CategoryConfig : IEntityTypeConfiguration<CourseCategory>
{
    public void Configure(EntityTypeBuilder<CourseCategory> builder)
    {
        builder.ToTable("Cateogries");
        builder.HasIndex(x => x.Slug).IsUnique();

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
    }
}
