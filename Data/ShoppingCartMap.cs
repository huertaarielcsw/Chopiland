using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ShoppingCartMap
    {
        public ShoppingCartMap(EntityTypeBuilder<ShoppingCart> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.TotalPrice);
        }
    }
}
