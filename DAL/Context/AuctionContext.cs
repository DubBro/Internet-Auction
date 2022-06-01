using DAL.Entities;
using System.Data.Entity;

namespace DAL.Context
{
    public class AuctionContext : DbContext
    {
        public AuctionContext()
        {
            new DropCreateAuctionDatabaseIfModelChanges().InitializeDatabase(this);
        }

        public DbSet<Lot> Lots { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
