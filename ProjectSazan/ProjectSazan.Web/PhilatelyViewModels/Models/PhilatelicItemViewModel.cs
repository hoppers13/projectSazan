using System.ComponentModel.DataAnnotations;

namespace ProjectSazan.Web.Models.PhilatelyViewModels
{
    public class PhilatelicItemViewModel
    {
        [Required]
        public string Number { get; set; }
        public string Description { get; set; }
    }
}