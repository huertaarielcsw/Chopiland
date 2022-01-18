using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ShoppingCart
    {
        public string Id { get; set; }
        public virtual User User { get; set;}
        public ICollection<ShoppingCartAnouncement> ShoppingCartAnouncements { get; set; }
        public float TotalPrice { get; set; }

    }
}
