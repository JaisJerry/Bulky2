using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky2.Models.Models
{
    public class Product
    {
        public int Id { get; set; }
         [Required]
        public string Title  { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(1, 10000)]
        public double ListPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price 1-50")]
        public double Price50 { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price 50-100")]
        public double Price100 { get; set; }
        [ValidateNever]
        public string? ImageUrl { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
                
        [ValidateNever]
        public Category category { get; set; }
        //[Display(Name = "Cover Type")]
        //public int CoverTypeId { get; set; }
        //[ForeignKey("CoverTypeId")]
        //[ValidateNever]
        //public CoverType coverType { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Special Price")]
        public double Price { get; set; }

    }
}
