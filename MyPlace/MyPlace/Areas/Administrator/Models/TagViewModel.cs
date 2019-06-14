
namespace MyPlace.Areas.Administrator.Models
{   
    using System.ComponentModel.DataAnnotations;

    public class TagViewModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a tag name")]        
        [RegularExpression("^[a-zA-Z0-9,.!?;: ]{3,}$",
        ErrorMessage = "Tag should contain at least 3 characters. It can contain words, numbers and punctuation.")]

        public string Name { get; set; }
    }
}
