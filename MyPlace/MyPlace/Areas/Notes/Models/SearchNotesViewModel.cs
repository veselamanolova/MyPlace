namespace MyPlace.Areas.Notes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SearchNotesViewModel
    {
        public int EntityId { get; set;  }

       
        [RegularExpression("^[a-zA-Z0-9]{0,}$",
        ErrorMessage = "Only characters and numbers are allowed.")]

        public string Creator { get; set; }       
        [RegularExpression("^[a-zA-Z0-9,.!?;: ]{0,}$",
        ErrorMessage = "Only characters,numbers and punctuation are allowed.")]
        public string SearchedStringInText { get; set; }

        public int? SearchCategoryId { get; set; }

        public List<CategoryViewModel> EntityCategories { get; set; }       

        public bool IsCompleted { get; set; }
        
        public DateTime? ExactDate { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string SortOption { get; set; }

        public bool SortIsAscending { get; set; }

        public bool IsSearchActive
        {
            get
            {
                return !string.IsNullOrEmpty(Creator)
                    || !string.IsNullOrEmpty(SearchedStringInText); 
            }
        }
    }
}
