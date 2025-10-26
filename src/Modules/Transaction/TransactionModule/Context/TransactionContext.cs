using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionModule.Context.Mapping;
using TransactionModule.Domain;

namespace TransactionModule.Context;

public class TransactionContext : DbContext
{
    public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
    {
        
    }

    public DbSet<Domain.UserTransaction> UserTransactions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserTransactionMapping).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
