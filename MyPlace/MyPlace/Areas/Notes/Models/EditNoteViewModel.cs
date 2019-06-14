using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlace.Areas.Notes.Models
{
    public class EditNoteViewModel
    {
        public NoteViewModel Note { get; set; }

        public List<CategoryViewModel> EntityCategories { get; set; }

        public int SelectedCategoryId { get; set; }

        public bool SelectedStatusId;

        public string ErrorMessage { get; set; }
    }
}
