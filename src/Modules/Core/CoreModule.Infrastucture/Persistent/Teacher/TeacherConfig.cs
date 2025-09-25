using Common.Domain.Utils;
using CoreModule.Domain.Teacher.Models;
using CoreModule.Infrastucture.Persistent.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Infrastucture.Persistent.Teacher;

public class TeacherConfig : IEntityTypeConfiguration<Domain.Teacher.Models.Teacher>
{
    public void Configure(EntityTypeBuilder<Domain.Teacher.Models.Teacher> builder)
    {
        builder.ToTable("Teachers");
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.UserName);

        builder.Property(x => x.UserName)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(20);

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Domain.Teacher.Models.Teacher>(x => x.UserId);
    }
}
