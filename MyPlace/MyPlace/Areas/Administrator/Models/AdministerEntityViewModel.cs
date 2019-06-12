namespace MyPlace.Areas.Administrator.Models
{
    using MyPlace.Services.DTOs;
    using System.Collections.Generic;

    public class AdministerEntityViewModel
    {
        public EntityViewModel Entity { get; set; }

        public LogBookViewModel NewLogBook { get; set; }

        public ICollection<LogBookViewModel> LogBooks { get; set; }

        public List<SelectableUserViewModel> UnassignedModerators { get; set; }

        public List<MinUserDTO> EntityModerators { get; set; }

    }
}
