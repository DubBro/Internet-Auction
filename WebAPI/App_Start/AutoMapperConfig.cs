using AutoMapper;
using BLL.DTOs;
using WebAPI.Models;

namespace WebAPI.App_Start
{
    public class AutoMapperConfig
    {
        public static Mapper InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LotDTO, LotViewModel>().ReverseMap();
                cfg.CreateMap<AuctionDTO, AuctionViewModel>().ReverseMap();
            });

            return new Mapper(config);
        }
    }
}