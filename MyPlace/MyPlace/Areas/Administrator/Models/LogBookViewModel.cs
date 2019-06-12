namespace MyPlace.Areas.Administrator.Models
{
    using MyPlace.Areas.Notes.Models;
    using MyPlace.Services.DTOs;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LogBookViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Entity name must be between 3 and 50 characters.")]
        public string Title { get; set; }
        
        //public string Address { get; set; }
        
        //public string Description { get; set; }
       
        //public string ImageUrl { get; set; }

        [Required]
        public int EstablishmentId { get; set; }
        
       // public List<CategoryViewModel> Categories { get; set; }
    }
}
