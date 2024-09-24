using Microsoft.EntityFrameworkCore;

namespace RestWithASPNETErudio.Features.Person
{
   public class MySQLContext : DbContext
   {
    public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}
    public DbSet<PersonModel> Persons { get; set; }
   }
}
