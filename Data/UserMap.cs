using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.HasOne(t => t.ShoppingCart).WithOne(u => u.User).HasForeignKey<ShoppingCart>(x => x.Id);
        }
    }
}
