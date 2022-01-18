using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chopiland.Models
{
    public class ProductViewModel
    {
        [HiddenInput]
        public Int64 Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        [Display(Name = "Added Date")]
        public DateTime AddedDate { get; set; }
        public SelectList CategoryNameSL { get; set; }
        public Int64 CategoryId { get; set; }
    }
}
