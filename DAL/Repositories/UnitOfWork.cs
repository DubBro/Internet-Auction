using DAL.Context;
using DAL.Interfaces;
using System;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private AuctionContext context;

        private ILotRepository lots;
        private IAuctionRepository auctions;
        private ICategoryRepository categories;

        private bool disposed;

        public UnitOfWork()
        {
            context = new AuctionContext();
        }

        public ILotRepository Lots
        {
            get
            {
                if (lots == null)
                {
                    lots = new LotRepository(context);
                }
                return lots;
            }
        }

        public IAuctionRepository Auctions
        {
            get
            {
                if (auctions == null)
                {
                    auctions = new AuctionRepository(context);
                }
                return auctions;
            }
        }

        public ICategoryRepository Categories
        {
            get
            {
                if (categories == null)
                {
                    categories = new CategoryRepository(context);
                }
                return categories;
            }
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
