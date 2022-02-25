using Microsoft.EntityFrameworkCore;
using ProMgr.Models;

namespace ProMgr.Data;

public class MonumentContext : DbContext
{
    public MonumentContext (DbContextOptions<MonumentContext> options)
        : base(options)
    {
    }

    public DbSet<Monument> Monuments => Set<Monument>();
    public DbSet<Epic> Epics => Set<Epic>();
    public DbSet<Story> Stories => Set<Story>();
}