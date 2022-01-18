using Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chopiland.Models
{
    public class ProductCategoriesViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public SelectList Categories { get; set; }
        public Int64 ProductCategory { get; set; }
        public string SearchString { get; set; }
    }
}
