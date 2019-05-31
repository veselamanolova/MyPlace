namespace MyPlace.Areas.Notes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SearchNotesViewModel
    {
        public int EntityId { get; set;  }

        public string NoteUserName { get; set; }
       
        public string SearchedStringInText { get; set; }

        public int? SearchCategoryId { get; set; }

        public List<CategoryViewModel> EntityCategories { get; set; }

        public bool IsCompleted { get; set; }



        [Required]
        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        [DataType(DataType.Date)]
        public DateTime ExactDate { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
