
using System;

namespace MyPlace.DataModels
{
    public class EventLog
    {
        public int Id { get; set; }

        public string Log { get; set; }

        public string Type { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
