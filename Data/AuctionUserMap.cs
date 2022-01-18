using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class AuctionUserMap
    {
        public AuctionUserMap(EntityTypeBuilder<AuctionUser> entityBuilder)
        {
            entityBuilder.HasKey(t => new { t.AuctionId, t.UserId });
            entityBuilder.Property(t => t.BidPrice);
            entityBuilder.HasOne(t => t.User).WithMany(x => x.AuctionUsers).HasForeignKey(u => u.UserId);
            entityBuilder.HasOne(t => t.Auction).WithMany(x => x.AuctionUsers).HasForeignKey(u => u.AuctionId);
        }
    }
}
