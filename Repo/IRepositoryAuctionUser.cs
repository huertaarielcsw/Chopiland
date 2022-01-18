using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repo
{
    public interface IRepositoryAuctionUser
    {
        IEnumerable<AuctionUser> GetAll();
        AuctionUser Get(string idUser, long idAuction);
        void Insert(AuctionUser entity);
        void Update(AuctionUser entity);
        void Delete(AuctionUser entity);
        void Remove(AuctionUser entity);
        void SaveChanges();
    }
}
