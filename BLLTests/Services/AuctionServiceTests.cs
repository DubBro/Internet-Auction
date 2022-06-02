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
using BLL.Services;

namespace BLLTests.Services
{
    [TestFixture]
    public class AuctionServiceTests
    {
        private readonly IFixture fixture = new Fixture();

        private IAuctionService service;
        private IUnitOfWork uow;
        private IMapper mapper;

        [SetUp]
        protected void SetUp()
        {
            uow = Substitute.For<IUnitOfWork>();
            fixture.Inject(uow);

            mapper = AutoMapperBLL.InitializeAutoMapper();

            service = fixture.Create<AuctionService>();
        }

        [Test]
        public void Bet_should_return_exception_if_id_is_less_or_equals_0()
        {
            // Arrange
            int id = -1;
            string name = fixture.Create<string>();
            int bid = fixture.Create<int>();

            // Act and assert
            Assert.Throws<InvalidIdException>(delegate { service.Bet(id, name, bid); });
        }

        [Test]
        public void Bet_should_return_exception_if_customer_name_equals_null()
        {
            // Arrange
            int id = fixture.Create<int>(); ;
            string name = null;
            int bid = fixture.Create<int>();

            // Act and assert
            Assert.Throws<InvalidNameException>(delegate { service.Bet(id, name, bid); });
        }

        [Test]
        public void Bet_should_return_exception_if_auction_not_exist_in_db()
        {
            // Arrange
            int id = fixture.Create<int>(); ;
            string name = fixture.Create<string>();
            int bid = fixture.Create<int>();
            uow.Auctions.Get(id).Returns((Auction)null);

            // Act and assert
            Assert.Throws<InvalidIdException>(delegate { service.Bet(id, name, bid); });
        }

        [Test]
        public void Bet_should_return_exception_if_auction_is_not_started()
        {
            // Arrange
            int id = fixture.Create<int>(); ;
            string name = fixture.Create<string>();
            int bid = fixture.Create<int>();
            Auction auction = new Auction { Started = false, Ended = false};
            uow.Auctions.Get(id).Returns(auction);

            // Act and assert
            Assert.Throws<InvalidAuctionException>(delegate { service.Bet(id, name, bid); });
        }

        [Test]
        public void Bet_should_return_exception_if_auction_is_finished()
        {
            // Arrange
            int id = fixture.Create<int>(); ;
            string name = fixture.Create<string>();
            int bid = fixture.Create<int>();
            Auction auction = new Auction { Started = false, Ended = true };
            uow.Auctions.Get(id).Returns(auction);

            // Act and assert
            Assert.Throws<InvalidAuctionException>(delegate { service.Bet(id, name, bid); });
        }

        [Test]
        public void Bet_should_return_exception_if_bid_is_low()
        {
            // Arrange
            int id = fixture.Create<int>(); ;
            string name = fixture.Create<string>();
            int bid = fixture.Create<int>();
            Auction auction = new Auction { Started = false, Ended = true, Bid = bid + 10 };
            uow.Auctions.Get(id).Returns(auction);

            // Act and assert
            Assert.Throws<InvalidAuctionException>(delegate { service.Bet(id, name, bid); });
        }

        [Test]
        public void Bet_should_call_auction_repository_get_method_once()
        {
            // Arrange
            int id = fixture.Create<int>(); ;
            string name = fixture.Create<string>();
            int bid = fixture.Create<int>();
            uow.Auctions.Get(id).Returns(new Auction { Started = true, Ended = false, Bid = bid - 10 });

            // Act
            service.Bet(id, name, bid);

            // Assert
            uow.Auctions.Received(1).Get(Arg.Any<int>());
        }

        [Test]
        public void Bet_should_call_auction_repository_update_method_once()
        {
            // Arrange
            int id = fixture.Create<int>(); ;
            string name = fixture.Create<string>();
            int bid = fixture.Create<int>();
            uow.Auctions.Get(id).Returns(new Auction { Started = true, Ended = false, Bid = bid - 10 });

            // Act
            service.Bet(id, name, bid);

            // Assert
            uow.Auctions.Received(1).Update(Arg.Any<Auction>());
        }

        [Test]
        public void Bet_should_call_unit_of_work_commit_method_once()
        {
            // Arrange
            int id = fixture.Create<int>(); ;
            string name = fixture.Create<string>();
            int bid = fixture.Create<int>();
            uow.Auctions.Get(id).Returns(new Auction { Started = true, Ended = false, Bid = bid - 10 });

            // Act
            service.Bet(id, name, bid);

            // Assert
            uow.Received(1).Commit();
        }

        [Test]
        public void CloseAuction_should_return_exception_if_id_is_less_or_equals_0()
        {
            // Arrange
            int id = -1;

            // Act and assert
            Assert.Throws<InvalidIdException>(delegate { service.CloseAuction(id); });
        }

        [Test]
        public void CloseAuction_should_return_exception_if_auction_not_exist_in_db()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Auctions.Get(id).Returns((Auction)null);

            // Act and assert
            Assert.Throws<InvalidIdException>(delegate { service.CloseAuction(id); });
        }

        [Test]
        public void CloseAuction_should_return_exception_if_auction_is_not_started()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Auctions.Get(id).Returns(new Auction { Started = false, Ended = false });

            // Act and assert
            Assert.Throws<InvalidAuctionException>(delegate { service.CloseAuction(id); });
        }

        [Test]
        public void CloseAuction_should_return_exception_if_auction_is_finished()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Auctions.Get(id).Returns(new Auction { Started = true, Ended = true });

            // Act and assert
            Assert.Throws<InvalidAuctionException>(delegate { service.CloseAuction(id); });
        }

        [Test]
        public void CloseAuction_should_call_lot_repository_update_method_once_if_auction_leader_is_not_null()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Auctions.Get(id).Returns(new Auction { Started = true, Ended = false, Leader = "a", Lot = new Lot() });

            // Act
            service.CloseAuction(id);

            // Assert
            uow.Lots.Received(1).Update(Arg.Any<Lot>());
        }

        [Test]
        public void CloseAuction_should_call_auction_repository_update_method_once()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Auctions.Get(id).Returns(new Auction { Started = true, Ended = false });

            // Act
            service.CloseAuction(id);

            // Assert
            uow.Auctions.Received(1).Update(Arg.Any<Auction>());
        }

        [Test]
        public void CloseAuction_should_call_unit_of_work_commit_method_once()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Auctions.Get(id).Returns(new Auction { Started = true, Ended = false });

            // Act
            service.CloseAuction(id);

            // Assert
            uow.Received(1).Commit();
        }

        [Test]
        public void CloseAuction_should_call_auction_repository_get_method_once()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Auctions.Get(id).Returns(new Auction { Started = true, Ended = false });

            // Act
            service.CloseAuction(id);

            // Assert
            uow.Auctions.Received(1).Get(Arg.Any<int>());
        }

        [Test]
        public void GetAuction_should_return_exception_if_id_is_less_or_equals_0()
        {
            // Arrange
            int id = -1;

            // Act and assert
            Assert.Throws<InvalidIdException>(delegate { service.GetAuction(id); });
        }

        [Test]
        public void GetAuction_should_return_exception_if_auction_not_exist_in_db()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Auctions.Get(id).Returns((Auction)null);

            // Act and assert
            Assert.Throws<InvalidIdException>(delegate { service.GetAuction(id); });
        }

        [Test]
        public void GetAuction_should_return_AuctionDTO()
        {
            // Arrange
            int id = fixture.Create<int>();
            Auction auction = new Auction();
            AuctionDTO auctionDTO = mapper.Map<Auction, AuctionDTO>(auction);
            uow.Auctions.Get(id).Returns(auction);

            // Act
            var testAuctionDTO = service.GetAuction(id);

            // Assert
            Assert.IsTrue(testAuctionDTO.GetType() == auctionDTO.GetType());
        }

        [Test]
        public void GetAuction_should_call_auction_reository_get_method_once()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Auctions.Get(id).Returns(new Auction());

            // Act
            service.GetAuction(id);

            // Assert
            uow.Auctions.Received(1).Get(Arg.Any<int>());
        }

        [Test]
        public void GetAuctions_should_call_auction_repository_get_all_method_once()
        {
            // Arrange

            // Act
            service.GetAuctions();

            // Assert
            uow.Auctions.Received(1).GetAll();
        }

        [Test]
        public void GetAuctions_should_return_IEnumerable_AuctionDTO()
        {
            // Arrange
            IEnumerable<AuctionDTO> auctionDTOs = new List<AuctionDTO>();

            // Act
            var testAuctionDTOs = service.GetAuctions();

            // Assert
            Assert.IsTrue(testAuctionDTOs.GetType() == auctionDTOs.GetType());
        }

        [Test]
        public void OpenAuction_should_return_exception_if_id_is_less_or_equals_0()
        {
            // Arrange
            int id = -1;

            // Act and assert
            Assert.Throws<InvalidIdException>(delegate { service.OpenAuction(id); });
        }

        [Test]
        public void OpenAuction_should_return_exception_if_auction_not_exist_in_db()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Auctions.Get(id).Returns((Auction)null);

            // Act and assert
            Assert.Throws<InvalidIdException>(delegate { service.OpenAuction(id); });
        }

        [Test]
        public void OpenAuction_should_return_exception_if_auction_is_started()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Auctions.Get(id).Returns(new Auction { Started = true });

            // Act and assert
            Assert.Throws<InvalidAuctionException>(delegate { service.OpenAuction(id); });
        }

        [Test]
        public void OpenAuction_should_return_exception_if_auction_is_finished()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Auctions.Get(id).Returns(new Auction { Ended = true });

            // Act and assert
            Assert.Throws<InvalidAuctionException>(delegate { service.OpenAuction(id); });
        }

        [Test]
        public void OpenAuction_should_call_auction_repository_get_method_once()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Auctions.Get(id).Returns(new Auction());

            // Act
            service.OpenAuction(id);

            // Assert
            uow.Auctions.Received(1).Get(Arg.Any<int>());
        }

        [Test]
        public void OpenAuction_should_call_auction_repository_update_method_once()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Auctions.Get(id).Returns(new Auction());

            // Act
            service.OpenAuction(id);

            // Assert
            uow.Auctions.Received(1).Update(Arg.Any<Auction>());
        }

        [Test]
        public void OpenAuction_should_call_unit_of_work_commit_method_once()
        {
            // Arrange
            int id = fixture.Create<int>();
            uow.Auctions.Get(id).Returns(new Auction());

            // Act
            service.OpenAuction(id);

            // Assert
            uow.Received(1).Commit();
        }
    }
}
