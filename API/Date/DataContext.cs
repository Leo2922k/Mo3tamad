using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Date
{
    public class DataContext : DbContext
    {

        //constructor 
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        // database table
        public DbSet <AppUser> Users {get; set;}
    }
}