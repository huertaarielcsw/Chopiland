using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Anouncement : BaseEntity
    {
        public string AnouncementName { get; set; }
        public virtual Product Product { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public Int64 ProductId { get; set; }
        public ICollection<ShoppingCartAnouncement> ShoppingCartAnouncements { get; set; }
        public string ImageUrl { get; set; }
        public bool Offer { get; set; }
        public decimal OfferPrice { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual Auction Auction {get; set;}
    }
}
