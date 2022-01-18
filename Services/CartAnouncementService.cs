using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Repo;

namespace Services
{
    public class CartAnouncementService : ICartAnouncementService
    {
        private IRepositoryCartAnouncement cartAnouncementRepository;

        public CartAnouncementService(IRepositoryCartAnouncement cartAnouncementRepository)
        {
            this.cartAnouncementRepository = cartAnouncementRepository;
        }
        public void DeleteShoppingCartAnouncement(string idCart, long idAnouncement)
        {
            ShoppingCartAnouncement shoppingCartAnouncement = GetShoppingCartAnouncement(idCart, idAnouncement);
            cartAnouncementRepository.Remove(shoppingCartAnouncement);
            cartAnouncementRepository.SaveChanges();
        }

        public IEnumerable<ShoppingCartAnouncement> GetAll()
        {
            return cartAnouncementRepository.GetAll();           
        }

        public ShoppingCartAnouncement GetShoppingCartAnouncement(string idCart, long idAnouncement)
        {
            return cartAnouncementRepository.Get(idCart, idAnouncement);
        }

        public void InsertShoppingCartAnouncement(ShoppingCartAnouncement ShoppingCartAnouncement)
        {
            cartAnouncementRepository.Insert(ShoppingCartAnouncement);
        }

        public void UpdateShoppingCartAnouncement(ShoppingCartAnouncement ShoppingCartAnouncement)
        {
            cartAnouncementRepository.Update(ShoppingCartAnouncement);
        }
    }
}
