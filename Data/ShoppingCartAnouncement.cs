using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ShoppingCartAnouncement
    {
        public Int64 AnouncementId { get; set; }
        public virtual Anouncement Anouncement { get; set; }
        public string ShoppingCartId { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
