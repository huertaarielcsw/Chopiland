using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IAuctionUserService
    {
        IEnumerable<AuctionUser> GetAll();
        AuctionUser GetAuctionUser(string idUser, long idAuction);
        void InsertAuctionUser(AuctionUser AuctionUser);
        void UpdateAuctionUser(AuctionUser AuctionUser);
        void DeleteAuctionUser(string idUser, long idAuction);
    }
}
