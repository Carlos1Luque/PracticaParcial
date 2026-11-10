using Microsoft.EntityFrameworkCore;
using Testing.Data; 

namespace Testing.Data; 
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Auto> autos =>Set<Auto>(); 
     public DbSet<Usuario> usuarios =>Set<Usuario>();
}