using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Repo;

namespace Services
{
    public class AuctionUserService : IAuctionUserService
    {
        private IRepositoryAuctionUser auctionUserRepository;

        public AuctionUserService(IRepositoryAuctionUser auctionUserRepository)
        {
            this.auctionUserRepository = auctionUserRepository;
        }

        public void DeleteAuctionUser(string idUser, long idAuction)
        {
            AuctionUser auctionUser = GetAuctionUser(idUser, idAuction);
            auctionUserRepository.Remove(auctionUser);
            auctionUserRepository.SaveChanges();
        }

        public IEnumerable<AuctionUser> GetAll()
        {
            return auctionUserRepository.GetAll();
        }

        public AuctionUser GetAuctionUser(string idUser, long idAuction)
        {
            return auctionUserRepository.Get(idUser, idAuction);
        }

        public void InsertAuctionUser(AuctionUser AuctionUser)
        {
            auctionUserRepository.Insert(AuctionUser);
        }

        public void UpdateAuctionUser(AuctionUser AuctionUser)
        {
            auctionUserRepository.Update(AuctionUser);
        }
    }
}
