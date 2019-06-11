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
        public List<CategoryDTO> AllCategories { get; set; }
        public List<MinUserDTO> AllUsers { get; set; }
    }
}
