using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Repo
{
    public class RepositoryAuctionUser : IRepositoryAuctionUser
    {
        private readonly ApplicationContext context;
        private DbSet<AuctionUser> entities;
        string errorMessage = string.Empty;

        public RepositoryAuctionUser(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<AuctionUser>();
        }

        public void Delete(AuctionUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public AuctionUser Get(string idUser, long idAuction)
        {
            return entities.SingleOrDefault(s => s.AuctionId == idAuction && s.UserId == idUser);
        }

        public IEnumerable<AuctionUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(AuctionUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Remove(AuctionUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(AuctionUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }
    }
}
