using Microsoft.EntityFrameworkCore;

using SGMC.Test.DB.Entities;

namespace SGMC.Test.DB;
public sealed class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
{
    public DbSet<Link> Links { get; set; } = null!;
    public DbSet<Nomenclature> Nomenclatures { get; set; } = null!;
    public DbSet<ProductMetaData> ProductMetaData { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
    }
}
