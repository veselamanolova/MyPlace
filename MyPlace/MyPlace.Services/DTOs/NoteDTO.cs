using MyPlace.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPlace.Services.DTOs
{
    public class NoteDTO
    {
        public int Id { get; set; }

        public int EntityId { get; set; }       

        public string Text { get; set; }       

        public DateTime Date { get; set; }

        public bool IsCompleted { get; set; }  
        
        public bool HasStatus { get; set; }

        public MinUserDTO NoteUser { get; set; }

        public CategoryDTO Category { get; set; }

    }
}
