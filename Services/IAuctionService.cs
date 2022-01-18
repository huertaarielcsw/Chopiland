using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IAuctionService
    {
        IEnumerable<Auction> GetAuctions();
        Auction GetAuction(long id);
        void InsertAuction(Auction Auction);
        void UpdateAuction(Auction Auction);
        void DeleteAuction(long id);
    }
}
