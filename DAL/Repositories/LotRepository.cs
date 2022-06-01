using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class LotRepository : Repository<Lot>, ILotRepository
    {
        private AuctionContext context;

        public LotRepository(AuctionContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Lot> GetLotsByCategory(string categoryName)
        {
            return context.Lots.Where(l => l.Category.Name == categoryName).ToList();
        }

        public IEnumerable<Lot> GetLotsByName(string Name)
        {
            return context.Lots.Where(l => l.Name == Name).ToList();
        }

        public IEnumerable<Lot> GetNotSoldLots()
        {
            return context.Lots.Where(l => l.Sold == false).ToList();
        }

        public IEnumerable<Lot> GetSoldLots()
        {
            return context.Lots.Where(l => l.Sold == true).ToList();
        }
    }
}
