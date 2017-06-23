using System.ComponentModel.DataAnnotations;

namespace ProjectSazan.Web.Models.PhilatelyViewModels
{
    public class AddCollection
    {
        public string NewCollection { get; set; }
    }

    public class PhilstelicItemViewModel
    {
        [Required]
        public string Number { get; set; }
        public string Description { get; set; }
    }
}