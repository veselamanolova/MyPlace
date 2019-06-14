
namespace MyPlace.Infrastructure
{
    using MyPlace.Common;

    public partial class DatabaseLogger
    {
        public DatabaseLogger WARN()
        {
            _eventLog.Type = GlobalConstants.WARN;
            return this;
        }
    }
}
