namespace AuctionApplication.Core
{
    public class Auction {
        // Fields that are immutable
        public string Name { get; set; }        // Name of the auction
        public string Owner { get; set; }       // Owner of the auction
        public double StartingPrice { get; set; } // Starting price of the auction
        public DateTime Deadline { get; set; }  // Deadline of the auction

        // Fields that are mutable
        public string Description { get; set; } // Description of the auction

        // List to store all the bids placed on the auction
        public List<Bid> Bids { get;}
        public Status AuctionStatus;
        public int Id { get; }
        public Auction(string name, string owner, double startingPrice, DateTime deadline, Status status, string description)
        {
            Name = name;
            Owner = owner;
            StartingPrice = startingPrice;
            Deadline = deadline;
            Bids = new List<Bid>();
            AuctionStatus = status;
            Description = description;
        }

        public Auction()
        {
        }

        public bool addBid(Bid bid)
        {
            Bids.Add(bid);
            if (Bids.Contains(bid))
            {
                return true;

            }
            return false;
        }
        //add another method for converting auction vm object to auction object
        
       }

}

