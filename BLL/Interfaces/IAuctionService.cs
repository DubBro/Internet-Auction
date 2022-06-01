using BLL.DTOs;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IAuctionService
    {
        AuctionDTO GetAuction(int id);
        IEnumerable<AuctionDTO> GetAuctions();
        void OpenAuction(int id);
        void CloseAuction(int id);
        void Bet(int id, string customerName, int bid);
    }
}
