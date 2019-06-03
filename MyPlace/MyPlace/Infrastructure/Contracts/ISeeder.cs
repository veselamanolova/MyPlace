
namespace MyPlace.Infrastructure.Contracts
{
    using System;

    public interface ISeeder
    {
        void SeedRoleAndAdmin(IServiceProvider serviceProvider);
    }
}
