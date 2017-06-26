using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectSazan.Web.Models.PhilatelyViewModels
{
    public class PhilatelicItemViewModel
    {
        public string Catalogue { get; set; }
        [Required]
        public string Area { get; set; }
        public string Number { get; set; }
        public int Year { get; set; }
        [Required]
        public string Description { get; set; }
        public string Condition { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public DateTime Acquired { get; set; } 
    }
}