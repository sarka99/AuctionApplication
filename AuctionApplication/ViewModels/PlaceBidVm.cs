using System.ComponentModel.DataAnnotations;
namespace AuctionApplication.ViewModels
{
    public class PlaceBidVm
    {
        [Required]
        public double Amount { get; set; }

    }
}
