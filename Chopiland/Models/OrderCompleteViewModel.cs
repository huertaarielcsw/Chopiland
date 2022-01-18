using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chopiland.Models
{
    public class OrderCompleteViewModel
    {
        public decimal Change { get; set; }
        public decimal Cash { get; set; }
        public decimal TotalAmount { get; set; }
        public List<ItemCartViewModel> Items { get; set; }
    }
}
