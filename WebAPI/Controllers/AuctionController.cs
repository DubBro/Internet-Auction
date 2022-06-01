using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.App_Start;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AuctionController : ApiController
    {
        private IAuctionService service;
        private IMapper mapper;

        public AuctionController(IAuctionService service)
        {
            this.service = service;
            mapper = AutoMapperConfig.InitializeAutoMapper();
        }

        [HttpPost]
        [Route("api/auction")]
        public IHttpActionResult AddAuction([FromBody]AuctionViewModel auction)
        {
            try
            {
                service.AddAuction(mapper.Map<AuctionViewModel, AuctionDTO>(auction));
                return GetAuctions();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/auctions")]
        public IHttpActionResult GetAuctions()
        {
            try
            {
                return Ok(mapper.Map<IEnumerable<AuctionDTO>, IEnumerable<AuctionViewModel>>(service.GetAuctions()));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/auction")]
        public IHttpActionResult GetAuctionByLotId(int lotId)
        {
            try
            {
                return Ok(mapper.Map<AuctionDTO, AuctionViewModel>(service.GetAuctionByLotId(lotId)));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("api/auction/open")]
        public IHttpActionResult OpenAuction(int id)
        {
            try
            {
                service.OpenAuction(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("api/auction/close")]
        public IHttpActionResult CloseAuction(int id)
        {
            try
            {
                service.CloseAuction(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("api/auction/bet")]
        public IHttpActionResult Bet(int id, string customerName, int bid)
        {
            try
            {
                service.Bet(id, customerName, bid);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}