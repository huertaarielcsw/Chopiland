using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class AuctionUser
    {
        public Int64 AuctionId { get; set; }
        public virtual Auction Auction { get; set; }
        public string UserId { get; set; }
        public virtual User User{ get; set; }
        public decimal BidPrice { get; set; }
    }
}
