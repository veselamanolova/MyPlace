using System;
using System.Collections.Generic;
using System.Text;

namespace MyPlace.Services.DTOs
{
    /// <summary>
    /// has properties int NotesCount and List<NoteDTO> Notes
    /// </summary>
    public class NotesSearchResultDTO
    {
        /// <summary>
        /// the total number of notes before applying skip and take
        /// </summary>
        public int NotesCount { get; set; }
        public List<NoteDTO> Notes { get; set; }
    }
}
