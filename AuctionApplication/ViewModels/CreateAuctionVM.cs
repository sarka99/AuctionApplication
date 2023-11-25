using System.ComponentModel.DataAnnotations;

namespace AuctionApplication.ViewModels
{
    public class CreateAuctionVM
    {
        [Required]
        [StringLength(30, ErrorMessage = "Max length 30 characters")]
        public string Name { get; set; }

        [Required]
        public double StartingPrice { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "Max length 128 characters")]
        public string Description { get; set; }
    }
}
