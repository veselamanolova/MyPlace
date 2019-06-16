
namespace MyPlace.Infrastructure
{
    using System;
    using System.IO;
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

            FileLog(_eventLog);
        }

        private async void FileLog(EventLog eventLog)
        {
            string subPath = GlobalConstants.subPath;
            string path = GlobalConstants.Path;

            if (!Directory.Exists(subPath)) Directory.CreateDirectory(subPath);

            using (StreamWriter sw = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))
            {
                string log = $"\nLog ID: {eventLog.Id}\n Type - {eventLog.Type}\n Log - {eventLog.Log}\n Created - {eventLog.CreatedOn}\n";
                await sw.WriteLineAsync(log);
            }
        }
    }
}


