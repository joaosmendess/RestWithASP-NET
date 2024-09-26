using Microsoft.EntityFrameworkCore;
using RestWithASPNETErudio.Model.BookModel;
using RestWithASPNETErudio.Model.PersonModel;

namespace RestWithASPNETUdemy.Model.Context
{
    public class MySQLContext : DbContext
    {
    public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}
    public DbSet<PersonModel> Persons { get; set; }
    public DbSet<BookModel> Books {get; set;}
    }
}