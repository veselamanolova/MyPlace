namespace MyPlace.Services.Tests.NoteServiceTests
{
    using Microsoft.EntityFrameworkCore;

    public static class TestUtils
    {
        public static DbContextOptions GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}