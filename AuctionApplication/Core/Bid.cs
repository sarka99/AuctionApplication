namespace AuctionApplication.Core
{
    public class Bid {
        public int AuctionId { get; set; }
        public string Bidder { get; set; }      // User placing the bid
        public double Amount { get; set; }     // Bid amount
        public DateTime BidTime { get; set; }  // Time when the bid was placed
        public int Id { get; set; }
       

        public Bid(int id,string bidder, double amount, DateTime bidTime)
        {
            Id = id;
            Bidder = bidder;
            Amount = amount;
            BidTime = bidTime;
        }

        public Bid()
        {
        }
    }
}
