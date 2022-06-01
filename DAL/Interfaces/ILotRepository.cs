using DAL.Entities;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ILotRepository : IRepository<Lot>
    {
        IEnumerable<Lot> GetLotsByCategory(string categoryName);
        IEnumerable<Lot> GetLotsByName(string Name);
        IEnumerable<Lot> GetSoldLots();
        IEnumerable<Lot> GetNotSoldLots();
    }
}
