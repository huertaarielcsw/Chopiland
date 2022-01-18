using Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chopiland.Models
{
    public class ShoppingCartViewModel
    {
        public List<ItemCartViewModel> Items { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }
        [Required]
        [StringLength(100,MinimumLength = 3)]
        public string NameCard { get; set; }
        [Required]
        [Range(1000,9999,ErrorMessage = "The code must have 4 digits")]
        public int SecurityCode { get; set; }
    }
}
