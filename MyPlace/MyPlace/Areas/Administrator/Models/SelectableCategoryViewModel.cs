using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlace.Areas.Administrator.Models
{
    public class SelectableCategoryViewModel
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public bool isSelected { get; set; }
    }
}
