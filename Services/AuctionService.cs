using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Repo;

namespace Services
{
    public class AuctionService : IAuctionService
    {
        private IRepository<Auction> auctionRepository;

        public AuctionService(IRepository<Auction> auctionRepository)
        {
            this.auctionRepository = auctionRepository;
        }

        public void DeleteAuction(long id)
        {
            Auction auction = GetAuction(id);
            auctionRepository.Remove(auction);
            auctionRepository.SaveChanges();
        }

        public Auction GetAuction(long id)
        {
            return auctionRepository.Get(id);
        }

        public IEnumerable<Auction> GetAuctions()
        {
            return auctionRepository.GetAll();
        }

        public void InsertAuction(Auction Auction)
        {
            auctionRepository.Insert(Auction);
        }

        public void UpdateAuction(Auction Auction)
        {
            auctionRepository.Update(Auction);
        }
    }
}
