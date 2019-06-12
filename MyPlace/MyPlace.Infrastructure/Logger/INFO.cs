
namespace MyPlace.Infrastructure
{
    using MyPlace.Common;

    public partial class DatabaseLogger
    {
        public DatabaseLogger INFO()
        {
            _eventLog.Type = GlobalConstants.DEBUG;
            return this;
        }
    }
}
