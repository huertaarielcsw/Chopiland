using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class AnouncementMap
    {
        public AnouncementMap(EntityTypeBuilder<Anouncement> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.AnouncementName).IsRequired();
            entityBuilder.Property(t => t.Description);
            entityBuilder.HasOne(t => t.Product).WithMany(u => u.Anouncements).HasForeignKey(x => x.ProductId);
            entityBuilder.Property(t => t.Price).IsRequired();
            entityBuilder.Property(t => t.Amount).IsRequired();
            entityBuilder.Property(t => t.ImageUrl);
            entityBuilder.Property(t => t.Offer);
            entityBuilder.Property(t => t.OfferPrice);
        }
    }
}