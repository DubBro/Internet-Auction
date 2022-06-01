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
    public class LotController : ApiController
    {
        private ILotService service;
        private IMapper mapper;

        public LotController(ILotService service)
        {
            this.service = service;
            mapper = AutoMapperConfig.InitializeAutoMapper();
        }

        [HttpGet]
        [Route("api/lot")]
        public IHttpActionResult GetLot(int id)
        {
            try
            {
                return Ok(mapper.Map<LotDTO, LotViewModel>(service.GetLot(id)));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/lots")]
        public IHttpActionResult GetLots()
        {
            try
            {
                return Ok(mapper.Map<IEnumerable<LotDTO>, IEnumerable<LotViewModel>>(service.GetLots()));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/lots")]
        public IHttpActionResult GetLotsByCategory(string category)
        {
            try
            {
                return Ok(mapper.Map<IEnumerable<LotDTO>, IEnumerable<LotViewModel>>(service.GetLotsByCategory(category)));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/lots")]
        public IHttpActionResult GetLotsByName(string name)
        {
            try
            {
                return Ok(mapper.Map<IEnumerable<LotDTO>, IEnumerable<LotViewModel>>(service.GetLotsByName(name)));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/lots/notsold")]
        public IHttpActionResult GetNotSoldLots()
        {
            try
            {
                return Ok(mapper.Map<IEnumerable<LotDTO>, IEnumerable<LotViewModel>>(service.GetNotSoldLots()));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/lots/sold")]
        public IHttpActionResult GetSoldLots()
        {
            try
            {
                return Ok(mapper.Map<IEnumerable<LotDTO>, IEnumerable<LotViewModel>>(service.GetSoldLots()));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/lot")]
        public IHttpActionResult AddLot([FromBody] LotViewModel lot)
        {
            try
            {
                service.AddLot(mapper.Map<LotViewModel, LotDTO>(lot));
                return GetLots();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}