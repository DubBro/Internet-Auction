using DAL.Entities;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ILotRepository : IRepository<Lot>
    {
        IEnumerable<Lot> GetLotsByCategory(string category);
        IEnumerable<Lot> GetLotsByName(string name);
        IEnumerable<Lot> GetSoldLots();
        IEnumerable<Lot> GetNotSoldLots();
    }
}
