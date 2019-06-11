
namespace MyPlace.Areas.Administrator.Models
{
    using System.Collections.Generic;

    public class ActivityIndexModel
    {
        public IEnumerable<ActivityListingModel> ActivityList { get; set; }
    }
}
