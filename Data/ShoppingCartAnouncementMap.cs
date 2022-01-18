using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ShoppingCartAnouncementMap
    {
        public ShoppingCartAnouncementMap(EntityTypeBuilder<ShoppingCartAnouncement> entityBuilder)
        {
            entityBuilder.HasKey(t => new { t.ShoppingCartId, t.AnouncementId});
            entityBuilder.Property(t => t.Quantity);
            entityBuilder.HasOne(t => t.ShoppingCart).WithMany(x => x.ShoppingCartAnouncements).HasForeignKey(u => u.ShoppingCartId);
            entityBuilder.HasOne(t => t.Anouncement).WithMany(x => x.ShoppingCartAnouncements).HasForeignKey(u => u.AnouncementId);
        }
    }
}
