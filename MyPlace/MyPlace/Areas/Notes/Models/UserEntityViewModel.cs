using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlace.Areas.Notes.Models
{
    public class UserEntityViewModel
    {
        public string UserId { get; set; }

        public int EntityId { get; set; }

        public string Title { get; set; }
    }
}
