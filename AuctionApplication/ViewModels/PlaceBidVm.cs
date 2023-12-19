using System.ComponentModel.DataAnnotations;
namespace AuctionApplication.ViewModels
{
    public class PlaceBidVm
    {
        public int AuctionId { get; set; }
        public string bidder { get; set; }
        public int Id { get; set; }
        public DateTime BidTime { get; set; }
        [Required]
        public double Amount { get; set; }

    }
}
