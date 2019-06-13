
namespace MyPlace.Areas.Administrator.Models
{
    using System.Collections.Generic;
  
    public class TagsViewModel
    {
        public List<TagViewModel> Tags { get; set; }
        public TagViewModel AddTag { get; set; }
    }
}
