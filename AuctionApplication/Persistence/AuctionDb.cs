using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionApplication.Persistence
{
    public class AuctionDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Corrected the name and added the setter

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Owner { get; set; } // Added the setter

        [Required]
        public double StartingPrice { get; set; } // Added the setter

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Deadline { get; set; } // Added the setter

        [Required]
        public string Description { get; set; }

        [Required]
        public int AuctionStatus { get; set; }

        [Required]
        public string UserName {  get; set; }
        public IEnumerable<BidDb> BidDbs { get; set; } = new List<BidDb>();
    }
}
