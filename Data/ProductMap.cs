using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ProductMap
    {
        public ProductMap(EntityTypeBuilder<Product> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.ProductName);
            entityBuilder.HasMany(t => t.Anouncements).WithOne(u => u.Product).HasForeignKey(x => x.Id);
            entityBuilder.HasOne(t => t.ProductCategory).WithMany(u => u.Products).HasForeignKey(x => x.CategoryId);
        }
    }
}
