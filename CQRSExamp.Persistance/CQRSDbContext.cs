using CQRSExamp.Domain;
using CQRSExamp.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSExamp.Persistance;

public class CQRSDbContext : DbContext
{
    public CQRSDbContext(DbContextOptions<CQRSDbContext> options) : base(options)
    {

    }
    public DbSet<Product> Products { get; set; }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreateDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdateDate = DateTime.Now;
                    break;
            }
        return base.SaveChangesAsync(cancellationToken);
    }
}
