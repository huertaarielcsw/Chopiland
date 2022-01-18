using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chopiland.Models
{
    public class CategoryViewModel 
    {
        [HiddenInput]
        public Int64 Id { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }
        [Display(Name = "MajorCategory")]
        public string MajorCategory { get; set; }
        public SelectList CategoryNameSL { get; set; }
        public Int64 ? CategoryId { get; set; }

    }
}
