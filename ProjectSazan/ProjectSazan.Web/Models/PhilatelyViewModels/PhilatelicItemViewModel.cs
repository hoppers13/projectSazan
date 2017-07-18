using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectSazan.Web.Models.PhilatelyViewModels
{
    public class PhilatelicItemViewModel
    {
        public Guid CollectionId { get; set; }
        public Guid ItemId { get; set; }
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
        public string Acquired { get; set; } 
    }
}