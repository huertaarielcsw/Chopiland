using Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repo
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ProductMap(modelBuilder.Entity<Product>());
            new AnouncementMap(modelBuilder.Entity<Anouncement>());
            new CategoryMap(modelBuilder.Entity<Category>());
            new ShoppingCartAnouncementMap(modelBuilder.Entity<ShoppingCartAnouncement>());
            new ShoppingCartMap(modelBuilder.Entity<ShoppingCart>());
            new UserMap(modelBuilder.Entity<User>());
            new AuctionMap(modelBuilder.Entity<Auction>());
            new AuctionUserMap(modelBuilder.Entity<AuctionUser>());
        }

    }
}
