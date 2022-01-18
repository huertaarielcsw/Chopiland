using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repo
{
    public class RepositoryCartAnouncement : IRepositoryCartAnouncement
    {
        private readonly ApplicationContext context;
        private DbSet<ShoppingCartAnouncement> entities;
        string errorMessage = string.Empty;
        public RepositoryCartAnouncement(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<ShoppingCartAnouncement>();
        }
        public void Delete(ShoppingCartAnouncement entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public ShoppingCartAnouncement Get(string idCart, long idAnouncement)
        {
            return entities.SingleOrDefault(s => s.ShoppingCartId == idCart && s.AnouncementId == idAnouncement);
        }

        public IEnumerable<ShoppingCartAnouncement> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(ShoppingCartAnouncement entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Remove(ShoppingCartAnouncement entity)
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

        public void Update(ShoppingCartAnouncement entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }
    }
}
