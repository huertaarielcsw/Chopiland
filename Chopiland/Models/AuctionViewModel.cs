using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chopiland.Models
{
    public class AuctionViewModel
    {
        [HiddenInput]
        public Int64 Id { get; set; }
        public string AnouncementName { get; set; }
        public string ProductName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Added Date")]
        public DateTime AddedDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal InitialPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LastBid { get; set; }
        public string LastUserToBid { get; set; }
        public SelectList AnouncementNameSL { get; set; }
        [Required]
        public Int64 AnouncementId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime FinalDate { get; set; }

    }
}
