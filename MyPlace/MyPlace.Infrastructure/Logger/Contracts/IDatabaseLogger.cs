
namespace MyPlace.Infrastructure.Logger
{
    using System;
    using System.Threading.Tasks;

    public interface IDatabaseLogger
    {
        // DatabaseLogger Type(Func<Type, string> type);

        DatabaseLogger DEBUG();
        DatabaseLogger INFO();
        DatabaseLogger WARN();
        DatabaseLogger ERROR();

        Task Log(string log);
    }
}
