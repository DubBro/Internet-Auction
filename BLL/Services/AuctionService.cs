﻿using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;

namespace BLL.Services
{
    public class AuctionService : Service, IAuctionService
    {
        public AuctionService(IUnitOfWork database):base(database)
        {
        }

        public void Bet(int id, string customerName, int bid)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }
            
            if (customerName == null)
            {
                throw new InvalidNameException();
            }

            var auction = database.Auctions.Get(id);

            if (auction == null)
            {
                throw new InvalidIdException();
            }

            if (auction.Started == false)
            {
                throw new InvalidAuctionException("ERROR: Auction has not started yet");
            }

            if (auction.Ended == true)
            {
                throw new InvalidAuctionException("ERROR: Auction is over");
            }

            if (auction.Bid >= bid)
            {
                throw new InvalidAuctionException("ERROR: Invalid bid");
            }

            auction.Bid = bid;
            auction.Leader = customerName;

            database.Auctions.Update(auction);
            database.Commit();
        }

        public void CloseAuction(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var auction = database.Auctions.Get(id);

            if (auction == null)
            {
                throw new InvalidIdException();
            }

            if (auction.Started == false)
            {
                throw new InvalidAuctionException("ERROR: Auction has not started yet");
            }

            if (auction.Ended == true)
            {
                throw new InvalidAuctionException("ERROR: Auction has already finished");
            }

            if (auction.Leader != null)
            {
                auction.Lot.Sold = true;
                database.Lots.Update(auction.Lot);
            }

            auction.Ended = true;

            database.Auctions.Update(auction);
            database.Commit();
        }

        public AuctionDTO GetAuction(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var auction = mapper.Map<Auction, AuctionDTO>(database.Auctions.Get(id));

            if (auction == null)
            {
                throw new InvalidIdException();
            }

            return auction;
        }

        public IEnumerable<AuctionDTO> GetAuctions()
        {
            return mapper.Map<IEnumerable<Auction>, IEnumerable<AuctionDTO>>(database.Auctions.GetAll());
        }

        public void OpenAuction(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException();
            }

            var auction = database.Auctions.Get(id);

            if (auction == null)
            {
                throw new InvalidIdException();
            }

            if (auction.Ended == true)
            {
                throw new InvalidAuctionException("ERROR: Auction has already finished");
            }

            if (auction.Started == true)
            {
                throw new InvalidAuctionException("ERROR: Auction has already started");
            }

            auction.Started = true;

            database.Auctions.Update(auction);
            database.Commit();
        }
    }
}
