using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Auction : BaseEntity
    {
        public DateTime FinalDate { get; set; }
        public virtual Anouncement Announcement { get; set; }
        public Int64 AnouncementId { get; set; }
        public ICollection<AuctionUser> AuctionUsers { get; set; }
        public decimal InitialPrice { get; set; }
    }
}
