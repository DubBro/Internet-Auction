using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using System.Linq;

namespace DAL.Repositories
{
    public class AuctionRepository : Repository<Auction>, IAuctionRepository
    {
        private AuctionContext context;

        public AuctionRepository(AuctionContext context) : base(context)
        {
            this.context = context;
        }

        public Auction GetAuctionByLot(Lot lot)
        {
            return context.Auctions.Where(a => a.Lot == lot).Single();
        }
    }
}
