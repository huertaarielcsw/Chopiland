using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chopiland.Models
{
    public class BiddingViewModel
    {
        public List<BidsUsersViewModel> BidsUsers { get; set; }
        public decimal NewBid { get; set; }
        [HiddenInput]
        public Int64 Id { get; set; }
        public decimal LastBid { get; set; }
    }
}
