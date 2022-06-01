using BLL.DTOs;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ILotService
    {
        void AddLot(LotDTO lot);
        LotDTO GetLot(int id);
        IEnumerable<LotDTO> GetLots();
        IEnumerable<LotDTO> GetLotsByCategory(string categoryName);
        IEnumerable<LotDTO> GetLotsByName(string name);
        IEnumerable<LotDTO> GetSoldLots();
        IEnumerable<LotDTO> GetNotSoldLots();
        void UpdateLot(LotDTO lotDTO);
        void DeleteLot(int id);
    } 
}
