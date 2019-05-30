using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlace.Areas.Notes.Models
{
    public class SearchNotesViewModel
    {
        public int EntityId { get; set;  }

        public string NoteUserName { get; set; }
       
        public string SearchedStringInText { get; set; }

        public int? SearchCategoryId { get; set; }

        public List<CategoryViewModel> EntityCategories { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime ExactDate { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
