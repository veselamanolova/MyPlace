namespace MyPlace.Areas.Administrator.Models
{
    using System;
    using System.Collections.Generic;
    using MyPlace.Areas.Notes.Models; 
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdministerEntityViewModel
    {
        public EntityViewModel Entity { get; set; }

        public ICollection<LogBookViewModel> LogBooks { get; set; }

        public ICollection<CategoryViewModel> AllCategories { get; set; }

    }
}
