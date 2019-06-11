
namespace MyPlace.Infrastructure
{
    using System;
    using System.Threading.Tasks;
    using MyPlace.Common;
    using MyPlace.Data;
    using MyPlace.DataModels;
    using MyPlace.Infrastructure.Logger;

    public partial class DatabaseLogger : IDatabaseLogger
    {
        private EventLog _eventLog = new EventLog();

        private readonly ApplicationDbContext _context;
        public DatabaseLogger(ApplicationDbContext context) =>
            _context = context;

        public async Task Log(string log)
        {
            _eventLog.Log = log;
            _eventLog.CreatedOn = DateTime.Now;
            _context.EventLogs.Add(_eventLog);
            await _context.SaveChangesAsync();
        }

        public DatabaseLogger DEBUG()
        {
            _eventLog.Type = GlobalConstants.DEBUG;
            return this;
        }

        public DatabaseLogger INFO()
        {
            _eventLog.Type = GlobalConstants.INFO;
            return this;
        }

        public DatabaseLogger WARN()
        {
            _eventLog.Type = GlobalConstants.WARN;
            return this;
        }

        public DatabaseLogger ERROR()
        {
            _eventLog.Type = GlobalConstants.ERROR;
            return this;
        }
    }
}


