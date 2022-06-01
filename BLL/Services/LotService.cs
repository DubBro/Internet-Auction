using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;

namespace BLL.Services
{
    public class LotService : Service, ILotService
    {
        public LotService(IUnitOfWork database) : base(database)
        {
        }

        public void AddLot(LotDTO lot)
        {
            if (lot == null || lot.Sold == true || lot.Name == null || lot.Owner == null || lot.Category == null)
            {
                throw new InvalidLotException();
            }

            database.Lots.Add(mapper.Map<LotDTO, Lot>(lot));
            database.Commit();
        }

        public void DeleteLot(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            database.Lots.Delete(id);
            database.Commit();
        }

        public LotDTO GetLot(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            return mapper.Map<Lot, LotDTO>(database.Lots.Get(id));
        }

        public IEnumerable<LotDTO> GetLots()
        {
            return mapper.Map<IEnumerable<Lot>, IEnumerable<LotDTO>>(database.Lots.GetAll());
        }

        public IEnumerable<LotDTO> GetLotsByCategory(string category)
        {
            if (category == null)
            {
                throw new InvalidCategoryException();
            }

            return mapper.Map<IEnumerable<Lot>, IEnumerable<LotDTO>>(database.Lots.GetLotsByCategory(category));
        }

        public IEnumerable<LotDTO> GetLotsByName(string name)
        {
            if (name == null)
            {
                throw new InvalidNameException();
            }

            var lots = mapper.Map<IEnumerable<Lot>, IEnumerable<LotDTO>>(database.Lots.GetLotsByName(name));

            if (lots == null)
            {
                throw new InvalidNameException();
            }

            return lots;
        }

        public IEnumerable<LotDTO> GetNotSoldLots()
        {
            return mapper.Map<IEnumerable<Lot>, IEnumerable<LotDTO>>(database.Lots.GetNotSoldLots());
        }

        public IEnumerable<LotDTO> GetSoldLots()
        {
            return mapper.Map<IEnumerable<Lot>, IEnumerable<LotDTO>>(database.Lots.GetSoldLots());
        }

        public void UpdateLot(LotDTO lotDTO)
        {
            if (lotDTO == null || lotDTO.Name == null || lotDTO.Owner == null || lotDTO.Category == null)
            {
                throw new InvalidLotException();
            }

            var lot = database.Lots.Get(lotDTO.ID);

            if (lot == null)
            {
                throw new InvalidIdException();
            }

            lot.Name = lotDTO.Name;
            lot.Owner = lotDTO.Owner;
            lot.Sold = lotDTO.Sold;
            lot.Category = lotDTO.Category;
            lot.Details = lotDTO.Details;

            database.Lots.Update(lot);
            database.Commit();
        }
    }
}
