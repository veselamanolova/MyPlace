namespace MyPlace.Services.Tests
{
    using MyPlace.Data;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyPlace.DataModels;

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

        [TestMethod]
        public async Task GetAllEntitiesAsyncShould_RetrunAllEntitiesWithEstablishementIdNull()
        {
            using (var arrangeContext =
                new ApplicationDbContext(TestUtils
                .GetOptions(nameof(GetAllEntitiesAsyncShould_RetrunAllEntitiesWithEstablishementIdNull))))
            {
                
                var entity1 = new Entity()
                {
                    Id = 1,
                    Title = "Entity1",
                    EstablishmentId = null
                };

                var entity2 = new Entity()
                {
                    Id = 2,
                    Title = "Entity2",
                    EstablishmentId = null
                };

                var entity3 = new Entity()
                {
                    Id = 3,
                    Title = "Entity3",
                    EstablishmentId = 1
                };

                arrangeContext.Entities.Add(entity1);
                arrangeContext.Entities.Add(entity2);
                arrangeContext.Entities.Add(entity3);

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(GetAllEntitiesAsyncShould_RetrunAllEntitiesWithEstablishementIdNull))))
            { 
                var sut = new EntityService(actAndAssertContext);
                var result = await sut.GetAllEntitiesAsync();

                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(1, result[0].Id);
                Assert.AreEqual(2, result[1].Id);              ;
                Assert.AreEqual("Entity1", result[0].Title);
                Assert.AreEqual("Entity2", result[1].Title);
            }
        }

        [TestMethod]
        public async Task GetAllEntitiesAsyncShould_RetrunEmptyListIfNoEntitiesWithEstablishementIdNull()
        {            
            using (var actAndAssertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(GetAllEntitiesAsyncShould_RetrunEmptyListIfNoEntitiesWithEstablishementIdNull))))
            {
                var sut = new EntityService(actAndAssertContext);
                var result = await sut.GetAllEntitiesAsync();

                Assert.IsNotNull(result);
                Assert.AreEqual(0, result.Count);                
            }
        }
      

    }
}

