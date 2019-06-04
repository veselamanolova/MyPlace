
namespace MyPlace.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using MyPlace.Infrastructure.CustomMiddlewares;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedRolesAndAdmin(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeederMiddleware>();
        }
    }
}
