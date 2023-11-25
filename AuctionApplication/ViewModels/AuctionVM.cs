namespace AuctionApplication.ViewModels;
using Auction = AuctionApplication.Core.Auction;

    public class AuctionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public double StartingPrice { get; set; }        
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public int AuctionStatus { get; set; }

        public static AuctionVM FromAuction(Auction auction)
        {
            return new AuctionVM()
            {
                Id = auction.Id,
                Name = auction.Name,
                Owner = auction.Owner,
                StartingPrice = auction.StartingPrice, 
                Deadline = auction.Deadline,
                Description = auction.Description,
                AuctionStatus = (int)auction.AuctionStatus
               

            };
        }

   }

