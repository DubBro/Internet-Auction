using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AuctionContext context;

        public CategoryRepository(AuctionContext context) : base(context)
        {
            this.context = context;
        }
    }
}
