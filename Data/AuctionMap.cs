using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class AuctionMap
    {
        public AuctionMap(EntityTypeBuilder<Auction> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.FinalDate).IsRequired();
            entityBuilder.Property(t => t.AddedDate);
            entityBuilder.Property(t => t.InitialPrice);
            entityBuilder.HasOne(t => t.Announcement).WithOne(u => u.Auction).HasForeignKey<Auction>(x => x.AnouncementId);
        }
    }
}
