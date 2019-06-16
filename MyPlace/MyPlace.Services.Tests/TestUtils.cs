namespace MyPlace.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Data;

    public static class TestUtils
    {
        public static DbContextOptions<ApplicationDbContext> GetOptions(string databaseName) =>
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
    }
}