
namespace MyPlace.Infrastructure
{
    using System;
    using MyPlace.DataModels;

    public partial class DatabaseLogger
    {
        public DatabaseLogger Type(Action<EventLog> action)
        {
            action(_eventLog);
            return this;
        }
    }
}

