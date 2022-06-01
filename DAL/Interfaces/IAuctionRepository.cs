﻿using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IAuctionRepository : IRepository<Auction>
    {
        Auction GetAuctionByLot(Lot lot);
    }
}
