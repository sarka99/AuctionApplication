using System;
using System.Collections.Generic;
using System.Linq;
using AuctionApplication.Core;
using AuctionApplication.Core.Interfaces;
using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace AuctionApplication.Persistence
{
    public class AuctionSqlPersistence : IAuctionPersistence
    {
        private  AuctionDbContext _dbContext;
     

        public AuctionSqlPersistence(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
         
        }

        public bool CreateAuction(Auction auction)
        {
            //proper
            try
            {
                var auctionDb = new AuctionDb
                {
                    Name = auction.Name,
                    Owner = auction.Owner,
                    StartingPrice = auction.StartingPrice,
                    Deadline = auction.Deadline,
                    Description = auction.Description,
                    AuctionStatus = (int)Status.ON_GOING // Assuming Status is an enum
                };

                _dbContext.AuctionDbs.Add(auctionDb);
                _dbContext.SaveChanges();
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Auction> GetAllActiveBiddenAuctionsByUser(string userName)
        {
           
            var currentTime = DateTime.Now;

            var auctionDbs = _dbContext.AuctionDbs
                .Include(a => a.BidDbs)
                .Where(a => a.Deadline > currentTime)
                .ToList();
            List<Auction> result = new List<Auction>();

            foreach (AuctionDb adb in auctionDbs)
            {
                if (adb.BidDbs.Any(b => b.Bidder == userName))
                {
                    Auction auction = new Auction(adb.Name, adb.Owner, adb.StartingPrice, adb.Deadline, (Status)adb.AuctionStatus, adb.Description);
                    result.Add(auction);
                }
                         
            }

            return result;
        }
        
        public List<Auction> GetAllTheUsersAuctions(string userName)
        {
            //proper
            var auctionDbs = _dbContext.AuctionDbs
                .Include(a => a.BidDbs)
                .Where(a => a.UserName.Equals(userName))
                .ToList();
          
            List<Auction> result = new List<Auction>();
            
            foreach(AuctionDb adb in auctionDbs)
            {
                Auction auction = new Auction(adb.Name, adb.Owner, adb.StartingPrice, adb.Deadline, (Status)adb.AuctionStatus, adb.Description);
                result.Add(auction);
            }

            return result;
        }

        public Auction GetAuctionById(int id)
        {
            //proper
            var auctionDbs = _dbContext.AuctionDbs.Find(id);

            if (auctionDbs == null)
            {
                // Auction not found, return null or handle the case as needed
                return null;
            }

            var bidsDbs = _dbContext.BidDbs.Where(b => b.AuctionId == id).ToList();
            Auction auction = new Auction(auctionDbs.Name, auctionDbs.Owner, auctionDbs.StartingPrice, auctionDbs.Deadline, (Status)auctionDbs.AuctionStatus, auctionDbs.Description);

            foreach (var bid in bidsDbs)
            {
                auction.addBid(new Bid(bid.Bidder, bid.Amount, bid.BidTime));
            }

            return auction;

        }

        public List<Auction> GetClosedAuctionsWonByUser(string userName)
        {
            //testa o se
           
            var currentTime = DateTime.Now;

            var closedAuctionDbs = _dbContext.AuctionDbs
                .Include(a => a.BidDbs)
                .Where(a => a.Deadline <= currentTime)
                .ToList();

            List<Auction> result = new List<Auction>();

            foreach (var adb in closedAuctionDbs)
            {
                var highestBid = adb.BidDbs.OrderByDescending(b => b.Amount).FirstOrDefault();
                //hitta highest bid j

                if (highestBid != null && highestBid.Bidder == userName)
                {
                   
                    result.Add(new Auction(adb.Name, adb.Owner,adb.StartingPrice,adb.Deadline, (Status)adb.AuctionStatus,adb.Description));
                }
            }

            return result;
        }

        public List<Auction> GetOnGoingAuctions(List<Auction> auctions)
        {
          
            var currentTime = DateTime.Now;

            var ongoingAuctions = auctions.Where(a => a.Deadline > currentTime).ToList();
            return ongoingAuctions;
        }

        public bool PlaceBid(Bid bid)
        {
            //Fel ya hmar
       

            var auctionDb = _dbContext.AuctionDbs.Include(a => a.BidDbs).FirstOrDefault(a => a.Id == bid.Id);
            if (auctionDb == null)
            {
                return false; // Auction not found
            }

            if (auctionDb.Deadline <= DateTime.Now)
            {
                return false; // Auction has already ended
            }

            if (bid.Amount <= auctionDb.StartingPrice)
            {
                return false; // Bid amount must be higher than the starting price
            }

            // Create a new BidDb object
            BidDb newBid = new(bid.Bidder, bid.Amount, bid.BidTime);

            // If the collection is not initialized, create a new list
            if (auctionDb.BidDbs == null)
            {
                auctionDb.BidDbs = new List<BidDb>();
            }

            // Add the newBid to the collection
            _dbContext.BidDbs.Add(newBid);
  
            _dbContext.SaveChanges();

            return true;
        }

            public bool UpdateDescription(Auction auction)
        {
           
            var auctionDb = _dbContext.AuctionDbs.FirstOrDefault(a => a.Id == auction.Id);

            if (auctionDb == null)
            {
                return false; 

            }
            if(auction.Owner == auctionDb.Owner)
            {
                auctionDb.Description = auction.Description;
                _dbContext.SaveChanges();
                return true;
            }

            return false;
            
        }

  
    }
}
