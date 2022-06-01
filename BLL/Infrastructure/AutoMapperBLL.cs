using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.Infrastructure
{
    public class AutoMapperBLL
    {
        public static Mapper InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Lot, LotDTO>().ReverseMap();
                cfg.CreateMap<Auction, AuctionDTO>().ReverseMap();
            });

            return new Mapper(config);
        }
    }
}
