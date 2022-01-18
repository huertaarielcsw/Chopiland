using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repo
{
    public interface IRepositoryCartAnouncement
    {
        IEnumerable<ShoppingCartAnouncement> GetAll();
        ShoppingCartAnouncement Get(string idCart, long idAnouncement);
        void Insert(ShoppingCartAnouncement entity);
        void Update(ShoppingCartAnouncement entity);
        void Delete(ShoppingCartAnouncement entity);
        void Remove(ShoppingCartAnouncement entity);
        void SaveChanges();
    }
}
