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
        [RegularExpression("^[a-zA-Z0-9]{3,}$",
        ErrorMessage = "Entity name should contain between 3 and 50 characters and numbers.")]

        public string Title { get; set; }      

        [Required]
        public int EstablishmentId { get; set; }
    }
}
