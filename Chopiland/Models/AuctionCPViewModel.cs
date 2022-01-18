using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chopiland.Models
{
    public class AuctionCPViewModel
    {
        public List<AuctionViewModel> items { get; set; }
        public SelectList Categories { get; set; }
        public Int64 CategoryId { get; set; }
        public string SearchString { get; set; }
        public Int64 ProductID { get; set; }
        public SelectList Products { get; set; }
    }
}
