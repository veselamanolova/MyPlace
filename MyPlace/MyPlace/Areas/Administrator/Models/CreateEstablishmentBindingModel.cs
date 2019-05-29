
namespace MyPlace.Areas.Administrator.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateEstablishmentBindingModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
