using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chopiland.Models
{
    public class AnouncementViewModel
    {
        [HiddenInput]
        public Int64 Id { get; set; }
        [Required]
        [StringLength(100,MinimumLength = 1)]
        [Display(Name = "Name")]
        public string AnouncementName { get; set; }
        [Required]
        [Display(Name = "ProductName")]
        public string ProductName { get; set; }
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Added Date")]
        public DateTime AddedDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Display(Name = "Amount")]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }
        public SelectList ProductNameSL { get; set; }
        [Required]
        public Int64 ProductId { get; set; }       
        public string ImageUrl { get; set; }
        public bool IsOffer { get; set; }
        [Display(Name = "Offer Price")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OfferPrice { get; set; }
        [Display(Name = "Expiration Date")]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationDate { get; set; }
       
    }
}
