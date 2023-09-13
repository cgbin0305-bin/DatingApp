using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
  public DbSet<AppUser> Users { get; set; } // the name of table in DB is created
  public DataContext(DbContextOptions options) : base(options)
  {
  }
}
