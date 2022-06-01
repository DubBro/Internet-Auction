using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class AuctionRepository : Repository<Auction>, IAuctionRepository
    {
        private AuctionContext context;

        public AuctionRepository(AuctionContext context) : base(context)
        {
            this.context = context;
        }
    }
}
