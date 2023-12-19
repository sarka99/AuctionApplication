using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionApplication.Persistence
{
    public class BidDb
    {
        public BidDb(string bidder, double amount, DateTime bidTime)
        {
            Bidder = bidder;
            Amount = amount;
            BidTime = bidTime;
        }
        public BidDb(int id,int auctionId,string bidder, double amount, DateTime bidTime)
        {
            Bidder = bidder;
            Amount = amount;
            BidTime = bidTime;
            AuctionId = auctionId;
            Id = id;
        }
        public BidDb()
        {
            // Parameterless constructor
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Added a setter

        [Required]
        public string Bidder { get; set; } // Added a setter

        [Required]
        public double Amount { get; set; } // Added a setter

        [Required]
        public DateTime BidTime { get; set; } // Added a setter

        [ForeignKey("AuctionId")]
        public AuctionDb AuctionDb { get; set; }

        public int AuctionId { get; set; }
    }
}
