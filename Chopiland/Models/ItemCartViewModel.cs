using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chopiland.Models
{
    public class ItemCartViewModel
    {
        [HiddenInput]
        public Int64 Id { get; set; }
        public string AnouncementName { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string ImageUrl { get; set; }
    }
}
