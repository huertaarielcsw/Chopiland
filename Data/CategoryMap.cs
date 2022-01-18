using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class CategoryMap
    {
        public CategoryMap(EntityTypeBuilder<Category> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.CategoryName);
            entityBuilder.HasMany(t => t.Products).WithOne(u => u.ProductCategory).HasForeignKey(x => x.CategoryId);
            entityBuilder.HasOne(t => t.MajorCategory).WithMany(u => u.SubCategories).HasForeignKey(x => x.CategoryId);
        }
    }
}
