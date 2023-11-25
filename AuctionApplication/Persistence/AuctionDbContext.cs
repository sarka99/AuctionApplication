using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AuctionApplication.Persistence
{
    public class AuctionDbContext : DbContext

    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }
        public DbSet<BidDb> BidDbs { get; set; }
        public DbSet<AuctionDb> AuctionDbs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AuctionDb adb = new AuctionDb
            {
                Id = -1, // from seed data
                Name = "Selling mac",
                Owner = "Sargon Kalo",
                StartingPrice = 50.5,
                Deadline = DateTime.Now,
                Description = "Just a test product to sell",
                AuctionStatus = 1,
                UserName = "sarka0159@gmail.com"
              
            };
            modelBuilder.Entity<AuctionDb>().HasData(adb);

            AuctionDb adb2 = new AuctionDb
            {
                Id = 2, 
                Name = "Selling hp",
                Owner = "mohamad",
                StartingPrice = 50.5,
                Deadline = DateTime.Now,
                Description = "sell",
                AuctionStatus = 1,
                UserName = "josef.nayyak@gmail.com"

            };
            modelBuilder.Entity<AuctionDb>().HasData(adb2);

             BidDb bdb1 = new BidDb(-1,-1,"jOSEF", 67, DateTime.Now); // Provide the constructor arguments
             BidDb bdb2 = new BidDb(-2,-1,"Mohamad", 59.5, DateTime.Now); // Provide the constructor arguments
             modelBuilder.Entity<BidDb>().HasData(bdb1);
             modelBuilder.Entity<BidDb>().HasData(bdb2);
     
        }
    }
}