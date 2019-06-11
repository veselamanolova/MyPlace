
namespace MyPlace.Infrastructure.Logger
{
    using System;
    using System.Threading.Tasks;
    using MyPlace.Data;
    using MyPlace.DataModels;

    public class DatabaseLogger : IDatabaseLogger
    {
        private string _type;

        private readonly ApplicationDbContext _context;

        public DatabaseLogger(ApplicationDbContext context) => 
            _context = context;

        public DatabaseLogger Type(string type)
        {
            _type = type;
            return this;
        }

        public async Task Log(string log)
        {
            _context.EventLogs.Add(new EventLog
            {
                Log = log,
                Type = _type,
                CreatedOn = DateTime.Now,                
            });
            await _context.SaveChangesAsync();
        }
    }
}
