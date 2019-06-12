using MyPlace.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlace.Areas.Administrator.Models
{
    public class AdministerLogBookViewModel
    {
        public LogBookViewModel LogBook { get; set; }
        public List<SelectableCategoryViewModel> AllUnassignedCategories { get; set; }
        public List<CategoryDTO> LogBookCategories { get; set; }
        public List<SelectableUserViewModel> AllUnassignedUsers { get; set; }
        public List<MinUserDTO> LogBookUsers { get; set; }
    }
}
