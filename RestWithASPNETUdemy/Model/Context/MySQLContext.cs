using Microsoft.EntityFrameworkCore;
using RestWithASPNETErudio.Model.PersonModel;

namespace RestWithASPNETUdemy.Model.Context
{
    public class MySQLContext : DbContext
    {
    public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}
    public DbSet<PersonModel> Persons { get; set; }
    }
}