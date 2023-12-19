using AuctionApplication.Persistence;
using System.ComponentModel.DataAnnotations.Schema;
using Bid = AuctionApplication.Core.Bid;
namespace AuctionApplication.ViewModels
{
    public class BidVM
    {
        public int Id { get; set; } // Added a setter

       
        public string Bidder { get; set; } // Added a setter

       
        public double Amount { get; set; } // Added a setter


        public DateTime BidTime { get; set; } // Added a setter


        public AuctionDb AuctionDb { get; set; }

        public int AuctionId { get; set; }

        public static BidVM FromBid(Bid bid)
        {
            return new BidVM()
            {
                Id = bid.Id,
                AuctionId = bid.AuctionId,
                Bidder = bid.Bidder,
                Amount = bid.Amount,
                BidTime = bid.BidTime
            };
        }
    }
 
}

