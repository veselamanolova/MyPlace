namespace MyPlace.Areas.Administrator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class EntityViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Entity name must be between 3 and 50 characters.")]
        public string Title { get; set; }

        [Required]       
        public string Address { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
