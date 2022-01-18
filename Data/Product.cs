using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public virtual Category ProductCategory { get; set; }
        public Int64 CategoryId { get; set; }
        public ICollection<Anouncement> Anouncements { get; set; }
    }
}
