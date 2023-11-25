namespace AuctionApplication.Core
{
    public class Bid {
        public string Bidder { get; set; }      // User placing the bid
        public double Amount { get; set; }     // Bid amount
        public DateTime BidTime { get; set; }  // Time when the bid was placed
        public int Id { get; }

        public Bid(string bidder, double amount, DateTime bidTime)
        {
            Bidder = bidder;
            Amount = amount;
            BidTime = bidTime;
        }

        public Bid()
        {
        }
    }
}
