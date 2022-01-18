using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class User : IdentityUser
    {
        public virtual ShoppingCart ShoppingCart { get; set; }
        public ICollection<AuctionUser> AuctionUsers { get; set; }
    }
}
