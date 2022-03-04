using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
       public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Car> Cars {get; set;}
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<CarInsurance> CarInsurances { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<TechnicalExamination> TechnicalExaminations { get; set; }
        public DbSet<Log> Logs {get; set;}
        public DbSet<Tires> Tires {get; set;}
        public DbSet<Field> Fields { get; set; }

         protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

            builder.Entity<AppRole>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();
        }
    }
}
    
    
