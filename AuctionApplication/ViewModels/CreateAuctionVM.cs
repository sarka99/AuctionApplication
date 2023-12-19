using System.ComponentModel.DataAnnotations;

namespace AuctionApplication.ViewModels
{
    public class CreateAuctionVM
    {
        
        public string Name { get; set; }

       
        public double StartingPrice { get; set; }

    
        public string Description { get; set; }

        public DateTime DeadLine { get; set; }
    }
}
