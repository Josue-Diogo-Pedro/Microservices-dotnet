using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Model.Context;

public class MYSQLContext : DbContext
{
    public MYSQLContext() { }

    public MYSQLContext(DbContextOptions<MYSQLContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
}
