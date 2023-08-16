using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Date
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int,
                               IdentityUserClaim<int>,
                               AppUserRole, IdentityUserLogin<int>,
                               IdentityRoleClaim<int>,
                               IdentityUserToken<int>>
    {

        //constructor 
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        // database table
       // public DbSet <AppUser> Users {get; set;} // // 16identity

        public DbSet <AppExams> Exams {get; set;}

        public DbSet <UserExam> UserExam {get; set;}


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().HasMany(ur => ur.UserRoles).WithOne(u => u.User).HasForeignKey(ur => ur.UserId).IsRequired();
            builder.Entity<AppRole>().HasMany(ur => ur.UserRoles).WithOne(u => u.Role).HasForeignKey(ur => ur.RoleId).IsRequired();
        }
    }
}