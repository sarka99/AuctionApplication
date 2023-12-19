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
        private readonly ILogger<AuctionSqlPersistence> _logger;



        public AuctionSqlPersistence(AuctionDbContext dbContext, ILogger<AuctionSqlPersistence> logger)
        {
            _dbContext = dbContext;
            _logger = logger;

        }

        public bool CreateAuction(Auction auction)
        {
            //proper
           
                var auctionDb = new AuctionDb
                {
                    Name = auction.Name,
                    Owner = auction.Owner,
                    UserName = auction.Owner,
                    StartingPrice = auction.StartingPrice,
                    Deadline = auction.Deadline,
                    Description = auction.Description,
                    AuctionStatus = (int)Status.ON_GOING, // Assuming Status is an enum,
               


                };
                Console.WriteLine("name of product is:" + auction.Name);
                Console.WriteLine("owner is:" + auction.Owner);
            Console.WriteLine("username is:" + auction.Name);

                _dbContext.AuctionDbs.Add(auctionDb);
                _dbContext.SaveChanges();
                
                return true;
            
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
                    Auction auction = new Auction(adb.Id, adb.Name, adb.Owner, adb.StartingPrice, adb.Deadline, (Status)adb.AuctionStatus, adb.Description);
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
                Auction auction = new Auction(adb.Id, adb.Name, adb.Owner, adb.StartingPrice, adb.Deadline, (Status)adb.AuctionStatus, adb.Description);
                result.Add(auction);
            }

            return result;
        }
       public List<Auction> GetActiveOngoingAuctions()
{
    var currentTime = DateTime.Now;

    var auctionDbs = _dbContext.AuctionDbs
        .Include(a => a.BidDbs)
        .Where(a => a.Deadline > currentTime)
         .OrderBy(a => a.Deadline) // Order by the deadline in ascending order (earliest first)

        .ToList();

    List<Auction> result = new List<Auction>();

    foreach (AuctionDb adb in auctionDbs)
    {
        Auction auction = new Auction(adb.Id, adb.Name, adb.Owner, adb.StartingPrice, adb.Deadline, (Status)adb.AuctionStatus, adb.Description);
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
            Auction auction = new Auction(auctionDbs.Id, auctionDbs.Name, auctionDbs.Owner, auctionDbs.StartingPrice, auctionDbs.Deadline, (Status)auctionDbs.AuctionStatus, auctionDbs.Description);

            foreach (var bid in bidsDbs)
            {
                auction.addBid(new Bid(bid.Id,bid.Bidder, bid.Amount, bid.BidTime));
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
                   
                    result.Add(new Auction(adb.Id, adb.Name, adb.Owner,adb.StartingPrice,adb.Deadline, (Status)adb.AuctionStatus,adb.Description));
                }
            }

            return result;
        }

  
            public bool UpdateDescription(int id,string newDescr, string owner)
        {
           
            var auctionDb = _dbContext.AuctionDbs.FirstOrDefault(a => a.Id == id);

            if (auctionDb == null)
            {
                return false; 

            }
            if(owner == auctionDb.Owner)
            {
                auctionDb.Description = newDescr;
                _dbContext.SaveChanges();
                return true;
            }

            return false;
            
        } 
        
        
        public bool PlaceBid(Bid bid)
        {
            var theNewBid = new BidDb
            {
                AuctionId = bid.AuctionId,
                Bidder = bid.Bidder,
                Amount = bid.Amount,
                BidTime = bid.BidTime,
            };
            Console.WriteLine("PlaceBid from persistence" + bid.Amount);
            _dbContext.BidDbs.Add(theNewBid);
            _dbContext.SaveChanges();
            return true;
        }

        public bool IsBidPlaceAble(Bid bid, ref string msg)
        {
            var auctionDb = _dbContext.AuctionDbs
                .Include(a => a.BidDbs)
                .FirstOrDefault(a => a.Id == bid.AuctionId);

            if (auctionDb == null)
            {
                msg = "Auction not found.";
                return false; // Auction not found
            }

            if (auctionDb.Deadline <= DateTime.Now)
            {
                Console.WriteLine("IsBidPlaceAble deadline");
                msg = "Auction deadline is exceeded.";
                return false; // Auction has already ended
            }

            if (bid.Amount <= auctionDb.StartingPrice)
            {
                 Console.WriteLine("IsBidPlaceAble deadline");
                msg = "Bid amount must be higher than or equal to the starting price.";
                return false; // Bid amount must be higher than or equal to the starting price
            }

            if (auctionDb.BidDbs.Any(b => b.Bidder == bid.Bidder))
            {
                // If the bidder has already placed a bid in this auction, check if the new bid is higher
                var highestBid = auctionDb.BidDbs.Max(b => b.Amount);
                if (bid.Amount <= highestBid)
                {
                    msg = "New bid is not higher than the current highest bid.";
                    return false; // New bid is not higher than the current highest bid
                }
            }

            return true;
        }
    }
}
