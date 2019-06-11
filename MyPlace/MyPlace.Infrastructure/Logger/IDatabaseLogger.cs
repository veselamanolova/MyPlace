
namespace MyPlace.Infrastructure.Logger
{
    using System.Threading.Tasks;

    public interface IDatabaseLogger
    {
        DatabaseLogger Type(string type);

        Task Log(string log);
    }
}
