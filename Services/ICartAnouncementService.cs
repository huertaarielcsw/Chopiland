using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface ICartAnouncementService
    {
        IEnumerable<ShoppingCartAnouncement> GetAll();
        ShoppingCartAnouncement GetShoppingCartAnouncement(string idCart, long idAnouncement);
        void InsertShoppingCartAnouncement(ShoppingCartAnouncement ShoppingCartAnouncement);
        void UpdateShoppingCartAnouncement(ShoppingCartAnouncement ShoppingCartAnouncement);
        void DeleteShoppingCartAnouncement(string idCart, long idAnouncement);
    }
}
