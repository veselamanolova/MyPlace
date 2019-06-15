
namespace MyPlace.Areas.Administrator.Models
{
    using Microsoft.AspNetCore.Http;
    using MyPlace.Infrastructure.Attributes;
    using System.ComponentModel.DataAnnotations;

    public class CreateEntityBindingModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Entity name must be between 3 and 50 characters.")]
        public string Title { get; set; }

        [Required]
        //[ValidLocation(ErrorMessage = "Address must be in format: {street}, {city}, {country}")]
        public string Address { get; set; }

        [Required]
        public string Description { get; set; }

        public IFormFile ImageUrl { get; set; }
    }
}
