using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeatherApiTeddySmith.Models;

namespace WeatherApiTeddySmith.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) { }
        
        public DbSet<Stock>Stock { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Portfolio>(x=>x.HasKey(p=>new {p.AppUserId, p.StockId}));
            modelBuilder.Entity<Portfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(u => u.AppUserId);

            modelBuilder.Entity<Portfolio>()
                .HasOne(u => u.Stock)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(u => u.StockId);

            modelBuilder.Entity<Stock>().HasData(
            new Stock()
            {
                Id = 1,
                Symbol = "DRO",
                CompanyName = "DroneShield",
                Purchase = 1,
                LastDiv = 0,
                Industry = "Technology",
                MarketCap = 67131234
            },
            new Stock()
            {
                Id = 2,
                Symbol = "LOT",
                CompanyName = "Lotus",
                Purchase = 0.3m,
                LastDiv = 0,
                Industry = "Energy",
                MarketCap = 21321
            },
             new Stock()
             {
                 Id = 3,
                 Symbol = "APE",
                 CompanyName = "Ape",
                 Purchase = 0.9m,
                 LastDiv = 0,
                 Industry = "Crypto",
                 MarketCap = 12321456786
             });
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole()
                {
                    Name = "User",
                    NormalizedName = "USER"
                });
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
