using BLL.DTOs;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IAuctionService
    {
        void AddAuction(AuctionDTO auction);
        AuctionDTO GetAuction(int id);
        AuctionDTO GetAuctionByLot(LotDTO lot);
        IEnumerable<AuctionDTO> GetAuctions();
        void OpenAuction(int id);
        void CloseAuction(int id);
        void Bet(int id, string customerName, int bid);
        void Delete(int id);
    }
}
