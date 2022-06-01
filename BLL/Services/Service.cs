using AutoMapper;
using BLL.Infrastructure;
using DAL.Interfaces;

namespace BLL.Services
{
    public abstract class Service
    {
        protected IUnitOfWork database;
        protected readonly IMapper mapper;

        public Service(IUnitOfWork database)
        {
            this.database = database;
            mapper = AutoMapperBLL.InitializeAutoMapper();
        }
    }
}
