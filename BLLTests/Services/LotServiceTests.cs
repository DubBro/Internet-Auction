using NUnit.Framework;
using System.Collections.Generic;
using AutoFixture;
using BLL.Interfaces;
using DAL.Interfaces;
using AutoMapper;
using NSubstitute;
using BLL.Infrastructure;
using BLL.DTOs;
using BLL.Exceptions;
using DAL.Entities;

namespace BLL.Services.Tests
{
    [TestFixture]
    public class LotServiceTests
    {
        private readonly IFixture fixture = new Fixture();

        private ILotService service;
        private IUnitOfWork uow;
        private IMapper mapper;

        [SetUp]
        protected void SetUp()
        {
            uow = Substitute.For<IUnitOfWork>();
            fixture.Inject(uow);

            mapper = AutoMapperBLL.InitializeAutoMapper();

            service = fixture.Create<LotService>();
        }

        [Test]
        public void AddLot_should_return_exception_if_lot_equals_null()
        {
            // Arrange
            LotDTO lot = null;

            // Act and assert
            Assert.Throws<InvalidLotException>(delegate { service.AddLot(lot); });
        }

        [Test]
        public void AddLot_should_return_exception_if_lot_name_equals_null()
        {
            // Arrange
            LotDTO lot = new LotDTO { Name = null };

            // Act and assert
            Assert.Throws<InvalidLotException>(delegate { service.AddLot(lot); });
        }

        [Test]
        public void AddLot_should_return_exception_if_lot_owner_equals_null()
        {
            // Arrange
            LotDTO lot = new LotDTO { Owner = null };

            // Act and assert
            Assert.Throws<InvalidLotException>(delegate { service.AddLot(lot); });
        }

        [Test]
        public void AddLot_should_return_exception_if_lot_category_equals_null()
        {
            // Arrange
            LotDTO lot = new LotDTO { Category = null };

            // Act and assert
            Assert.Throws<InvalidLotException>(delegate { service.AddLot(lot); });
        }

        [Test]
        public void AddLot_should_return_exception_if_lot_is_sold()
        {
            // Arrange
            LotDTO lot = new LotDTO { Sold = true };

            // Act and assert
            Assert.Throws<InvalidLotException>(delegate { service.AddLot(lot); });
        }

        [Test]
        public void AddLot_should_call_lot_repository_add_method_once()
        {
            // Arrange
            LotDTO lot = new LotDTO { Name = "a", Category = "b", Owner = "c"};
            
            // Act
            service.AddLot(lot);

            // Assert
            uow.Lots.Received(1).Add(Arg.Any<Lot>());
        }

        [Test]
        public void AddLot_should_call_unit_of_work_commit_method_once()
        {
            // Arrange
            LotDTO lot = new LotDTO { Name = "a", Category = "b", Owner = "c"};

            // Act
            service.AddLot(lot);

            // Assert
            uow.Received(1).Commit();
        }

        [Test]
        public void DeleteLot_should_return_exception_if_id_is_less_or_equals_0()
        {
            // Arrange
            int id = -1;

            // Act and assert
            Assert.Throws<InvalidIdException>(delegate { service.DeleteLot(id); });
        }

        [Test]
        public void DeleteLot_should_call_auction_repository_delete_method_once()
        {
            // Arrange
            int id = fixture.Create<int>();

            // Act
            service.DeleteLot(id);

            // Assert
            uow.Auctions.Received(1).Delete(Arg.Any<int>());
        }

        [Test]
        public void DeleteLot_should_call_lot_repository_delete_method_once()
        {
            // Arrange
            int id = fixture.Create<int>();

            // Act
            service.DeleteLot(id);

            // Assert
            uow.Lots.Received(1).Delete(Arg.Any<int>());
        }

        [Test]
        public void DeleteLot_should_call_unit_of_work_commit_method_once()
        {
            // Arrange
            int id = fixture.Create<int>();

            // Act
            service.DeleteLot(id);

            // Assert
            uow.Received(1).Commit();
        }

        [Test]
        public void GetLot_should_return_exception_if_id_is_less_or_equals_0()
        {
            // Arrange
            int id = -1;

            // Act and assert
            Assert.Throws<InvalidIdException>(delegate { service.GetLot(id); });
        }

        [Test]
        public void GetLot_should_return_exception_if_lot_equals_null()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Lots.Get(id).Returns((Lot)null);

            // Act and assert
            Assert.Throws<InvalidIdException>(delegate { service.GetLot(id); });
        }

        [Test]
        public void GetLot_should_return_LotDTO()
        {
            // Arrange
            int id = fixture.Create<int>();
            Lot lot = new Lot();
            LotDTO lotDTO = mapper.Map<Lot, LotDTO>(lot);
            uow.Lots.Get(id).Returns(lot);

            // Act
            var testLotDTO = service.GetLot(id);

            // Assert
            Assert.IsTrue(testLotDTO.GetType() == lotDTO.GetType());
        }

        [Test]
        public void GetLot_should_call_lot_repository_get_method_once()
        {
            // Arrange
            int id = 1;
            uow.Lots.Get(id).Returns(new Lot());

            // Act
            service.GetLot(id);

            // Assert
            uow.Lots.Received(1).Get(Arg.Any<int>());
        }

        [Test]
        public void GetLots_should_call_lot_repository_get_all_method_once()
        {
            // Arrange

            // Act
            service.GetLots();

            // Assert
            uow.Lots.Received(1).GetAll();
        }

        [Test]
        public void GetLots_should_return_IEnumerable_LotDTO()
        {
            // Arrange
            IEnumerable<LotDTO> lotDTOs = new List<LotDTO>();

            // Act
            var testLotDTOs = service.GetLots();

            // Assert
            Assert.IsTrue(testLotDTOs.GetType() == lotDTOs.GetType());
        }

        [Test]
        public void GetLotsByCategory_should_return_exception_if_category_equals_null()
        {
            // Arrange
            string category = null;

            // Act and assert
            Assert.Throws<InvalidCategoryException>(delegate { service.GetLotsByCategory(category); });
        }

        [Test]
        public void GetLotsByCategory_should_return_IEnumerable_LotDTO()
        {
            // Arrange
            string category = fixture.Create<string>();
            IEnumerable<LotDTO> lotDTOs = new List<LotDTO>();

            // Act
            var testLotDTOs = service.GetLotsByCategory(category);

            // Assert
            Assert.IsTrue(testLotDTOs.GetType() == lotDTOs.GetType());
        }

        [Test]
        public void GetLotsByName_should_return_exception_if_name_equals_null()
        {
            // Arrange
            string name = null;

            // Act and assert
            Assert.Throws<InvalidNameException>(delegate { service.GetLotsByName(name); });
        }

        [Test]
        public void GetLotsByName_should_return_IEnumerable_LotDTO()
        {
            // Arrange
            string name = fixture.Create<string>();
            IEnumerable<LotDTO> lotDTOs = new List<LotDTO>();

            // Act
            var testLotDTOs = service.GetLotsByName(name);

            // Assert
            Assert.IsTrue(testLotDTOs.GetType() == lotDTOs.GetType());
        }

        [Test]
        public void GetNotSoldLots_should_return_IEnumerable_LotDTO()
        {
            // Arrange
            IEnumerable<LotDTO> lotDTOs = new List<LotDTO>();

            // Act
            var testLotDTOs = service.GetNotSoldLots();

            // Assert
            Assert.IsTrue(testLotDTOs.GetType() == lotDTOs.GetType());
        }

        [Test]
        public void GetNotSoldLots_should_call_lot_repository_get_not_sold_lots_method_once()
        {
            // Arrange

            // Act
            service.GetNotSoldLots();

            // Assert
            uow.Lots.Received(1).GetNotSoldLots();
        }

        [Test]
        public void GetSoldLots_should_return_IEnumerable_LotDTO()
        {
            // Arrange
            IEnumerable<LotDTO> lotDTOs = new List<LotDTO>();

            // Act
            var testLotDTOs = service.GetSoldLots();

            // Assert
            Assert.IsTrue(testLotDTOs.GetType() == lotDTOs.GetType());
        }

        [Test]
        public void GetSoldLots_should_call_lot_repository_get_sold_lots_method_once()
        {
            // Arrange

            // Act
            service.GetSoldLots();

            // Assert
            uow.Lots.Received(1).GetSoldLots();
        }

        [Test]
        public void UpdateLot_should_return_exception_if_lot_equals_null()
        {
            // Arrange
            LotDTO lot = null;

            // Act and assert
            Assert.Throws<InvalidLotException>(delegate { service.UpdateLot(lot); });
        }

        [Test]
        public void UpdateLot_should_return_exception_if_lot_name_equals_null()
        {
            // Arrange
            LotDTO lot = new LotDTO { Name = null };

            // Act and assert
            Assert.Throws<InvalidLotException>(delegate { service.UpdateLot(lot); });
        }

        [Test]
        public void UpdateLot_should_return_exception_if_lot_owner_equals_null()
        {
            // Arrange
            LotDTO lot = new LotDTO { Owner = null};

            // Act and assert
            Assert.Throws<InvalidLotException>(delegate { service.UpdateLot(lot); });
        }

        [Test]
        public void UpdateLot_should_return_exception_if_lot_category_equals_null()
        {
            // Arrange
            LotDTO lot = new LotDTO { Category = null };

            // Act and assert
            Assert.Throws<InvalidLotException>(delegate { service.UpdateLot(lot); });
        }

        [Test]
        public void UpdateLot_should_return_exception_if_lot_not_exist_in_db()
        {
            // Arrange
            LotDTO lot = new LotDTO();
            uow.Lots.Get(lot.ID).Returns((Lot)null);

            // Act and assert
            Assert.Throws<InvalidLotException>(delegate { service.UpdateLot(lot); });
        }

        [Test]
        public void UpdateLot_should_call_lot_repository_get_method_once()
        {
            // Arrange
            LotDTO lot = new LotDTO { Name = "a", Owner = "b", Category = "c" };
            uow.Lots.Get(lot.ID).Returns(mapper.Map<LotDTO, Lot>(lot));

            // Act
            service.UpdateLot(lot);

            // Assert
            uow.Lots.Received(1).Get(Arg.Any<int>());
        }

        [Test]
        public void UpdateLot_should_call_lot_repository_update_method_once()
        {
            // Arrange
            LotDTO lot = new LotDTO { ID = 1, Name = "a", Owner = "b", Category = "c" };

            // Act
            service.UpdateLot(lot);

            // Assert
            uow.Lots.Received(1).Update(Arg.Any<Lot>());
        }

        [Test]
        public void UpdateLot_should_call_unit_of_work_commit_method_once()
        {
            // Arrange
            LotDTO lot = new LotDTO { ID = 1, Name = "a", Owner = "b", Category = "c" };

            // Act
            service.UpdateLot(lot);

            // Assert
            uow.Received(1).Commit();
        }
    }
}