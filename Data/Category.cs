using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Category: BaseEntity
    {
        public string CategoryName { get; set; }
        public virtual Category MajorCategory { get; set; }
        public Int64 ? CategoryId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
    }
}
