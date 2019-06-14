
namespace MyPlace.Infrastructure
{
    using MyPlace.Common;

    public partial class DatabaseLogger
    {
        public DatabaseLogger ERROR()
        {
            _eventLog.Type = GlobalConstants.ERROR;
            return this;
        }
    }
}
