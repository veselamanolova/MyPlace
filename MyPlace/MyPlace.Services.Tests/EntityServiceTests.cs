using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPlace.Data;
using MyPlace.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPlace.Services.Tests
{
    [TestClass]
    public class EntityServiceTests
    {
        [TestMethod]
        public async Task CreateLogBookAsyncShould_SuceedWhenAddingLogBook()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName: "CreateLogBookAsyncShould_SuceedWhenAddingLogBook")
                .Options;           

            using (var actAndAssertContext = 
                new ApplicationDbContext(TestUtils.GetOptions(nameof(CreateLogBookAsyncShould_SuceedWhenAddingLogBook))))
            {
               EntityService sut = new EntityService(actAndAssertContext);
               await sut.CreateLogBookAsync("Logbook", 1); 
               //If no exception is thrown the test has passed.
            }
        }
    }
}

