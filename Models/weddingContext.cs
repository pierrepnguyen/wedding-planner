using Microsoft.EntityFrameworkCore;
using wedding.Models;

namespace wedding.Models
{
  public class weddingContext : DbContext
  {
    public weddingContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users {get;set;}
    public DbSet<Wedding> Weddings {get;set;}
    public DbSet<RSVP> RSVPs {get;set;}
  }
}
