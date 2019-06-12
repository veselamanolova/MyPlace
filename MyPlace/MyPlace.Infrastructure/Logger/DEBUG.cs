
namespace MyPlace.Infrastructure
{
    using MyPlace.Common;

    public partial class DatabaseLogger
    {
        public DatabaseLogger DEBUG()
        {
            _eventLog.Type = GlobalConstants.INFO;
            return this;
        }
    }
}

