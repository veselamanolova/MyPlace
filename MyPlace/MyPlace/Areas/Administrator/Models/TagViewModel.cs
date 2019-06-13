
namespace MyPlace.Areas.Administrator.Models
{   
    using System.ComponentModel.DataAnnotations;

    public class TagViewModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a category name")]
        public string Name { get; set; }
    }
}
