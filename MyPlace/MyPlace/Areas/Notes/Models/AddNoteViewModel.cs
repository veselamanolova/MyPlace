using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlace.Areas.Notes.Models
{
    public class AddNoteViewModel
    {
        public NoteViewModel Note { get; set; }

        public List<CategoryViewModel> EntityCategories { get; set; }
    }
}
