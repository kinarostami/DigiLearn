using Common.Infrastructure;
using CoreModule.Domain.Category.Models;
using CoreModule.Domain.Course.Models;
using CoreModule.Domain.Order.Models;
using CoreModule.Domain.Teacher.Models;
using CoreModule.Infrastucture.Persistent.Course;
using CoreModule.Infrastucture.Persistent.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Infrastucture.Persistent;

public class CoreMoudelEfContext : BaseEfContext<CoreMoudelEfContext>
{
    public CoreMoudelEfContext(DbContextOptions<CoreMoudelEfContext> options, IMediator mediator) : base(options, mediator)
    {
    }

    public DbSet<Domain.Course.Models.Course> Courses { get; set; }
    public DbSet<Domain.Teacher.Models.Teacher> Teachers { get; set; }
    public DbSet<CourseCategory> Categories { get; set; }
    public DbSet<Domain.Order.Models.Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfig).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
