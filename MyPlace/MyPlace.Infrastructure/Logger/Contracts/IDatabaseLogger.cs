
namespace MyPlace.Infrastructure.Logger
{
    using System;
    using System.Threading.Tasks;
    using MyPlace.DataModels;

    public interface IDatabaseLogger
    {
        Task Log(string log);

        DatabaseLogger Type(Action<EventLog> action);
    }
}
