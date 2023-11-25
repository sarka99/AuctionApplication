
using Auction = AuctionApplication.Core.Auction;
    namespace AuctionApplication.ViewModels{
    public class AuctionDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public double StartingPrice { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public int AuctionStatus { get; set; }

        public List<BidVM> BidVMs { get; set; } = new();
        public static AuctionDetailsVM FromAuction(Auction auction)
        {
              var detailsVM = new AuctionDetailsVM
            {
                Id = auction.Id,
                Name = auction.Name,
                Owner = auction.Owner,
                StartingPrice = auction.StartingPrice,
                Deadline = auction.Deadline,
                Description = auction.Description,
                AuctionStatus = (int)auction.AuctionStatus,
                
            };
        
         

            foreach(var bid in auction.Bids)
            {
                detailsVM.BidVMs.Add(BidVM.FromBid(bid));
            }
            return detailsVM;
        }
    }
}


