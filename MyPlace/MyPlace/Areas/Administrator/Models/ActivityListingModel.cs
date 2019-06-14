
namespace MyPlace.Areas.Administrator.Models
{
    using System;

    public class ActivityListingModel
    {
        public int Id { get; set; }

        public string Log { get; set; }

        public string Type { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
