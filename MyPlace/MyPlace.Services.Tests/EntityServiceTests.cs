namespace MyPlace.Services.Tests
{
    using MyPlace.Data;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyPlace.DataModels;
    using System.Linq;

    [TestClass]
    public class EntityServiceTests
    {     

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

        [TestMethod]
        public async Task GetEntityByIdAsyncShould_RetrunEntityWithLogBooksAndCategoriesAssignedToThem()
        {
            using (var arrangeContext =
                new ApplicationDbContext(TestUtils
                .GetOptions(nameof(GetEntityByIdAsyncShould_RetrunEntityWithLogBooksAndCategoriesAssignedToThem))))
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

                arrangeContext.Entities.Add(entity1);
                arrangeContext.Entities.Add(entity2);

                var logbook1 = new Entity()
                {
                    Id = 3,
                    Title = "Logbook1",
                    EstablishmentId = 1
                };

                var logbook2 = new Entity()
                {
                    Id = 4,
                    Title = "Logbook2",
                    EstablishmentId = 2
                };
                arrangeContext.Entities.Add(logbook1);
                arrangeContext.Entities.Add(logbook2);

                await arrangeContext.Categories.AddAsync(new Category()
                {
                    Id = 1,
                    Name = "Category1"
                });

                await arrangeContext.Categories.AddAsync(new Category()
                {
                    Id = 2,
                    Name = "Category2"
                });

                await arrangeContext.Categories.AddAsync(new Category()
                {
                    Id = 3,
                    Name = "Category3"
                });

                await arrangeContext.EntityCategories.AddAsync(new EntityCategory()
                {
                    EntityId = 3,
                    CategoryId = 1
                });

                await arrangeContext.EntityCategories.AddAsync(new EntityCategory()
                {
                    EntityId = 3,
                    CategoryId = 2
                });

                await arrangeContext.EntityCategories.AddAsync(new EntityCategory()
                {
                    EntityId = 2,
                    CategoryId = 3
                });

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(GetAllEntitiesAsyncShould_RetrunAllEntitiesWithEstablishementIdNull))))
            {
                var sut = new EntityService(actAndAssertContext);
                var result = await sut.GetAllEntitiesAsync();

                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(1, result[0].Id);
                Assert.AreEqual(2, result[1].Id); ;
                Assert.AreEqual("Entity1", result[0].Title);
                Assert.AreEqual("Entity2", result[1].Title);
            }
        }


        [TestMethod]
        public async Task GetEntityByIdAsyncShould_ReturnTheLogBookWithAllEntityCategoriesAssignedToIt()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase(databaseName: "GetEntityByIdAsyncShould_ReturnTheLogBookWithAllEntityCategoriesAssignedToIt")
               .Options;

            using (var arrangeContext = new ApplicationDbContext(TestUtils
                  .GetOptions(nameof(GetEntityByIdAsyncShould_ReturnTheLogBookWithAllEntityCategoriesAssignedToIt))))
            {
                var entity1 = new Entity()
                {
                    Id = 1,
                    Title = "Entity1",
                    EstablishmentId = null
                };

                var logbook2 = new Entity()
                {
                    Id = 2,
                    Title = "Logbook2",
                    EstablishmentId = 1
                };

                var logbook3 = new Entity()
                {
                    Id = 3,
                    Title = "Logbook3",
                    EstablishmentId = 1
                };

                arrangeContext.Entities.Add(entity1);
                arrangeContext.Entities.Add(logbook2);
                arrangeContext.Entities.Add(logbook3);

                await arrangeContext.Categories.AddAsync(new Category()
                {
                    Id = 1,
                    Name = "Category1"
                });

                await arrangeContext.Categories.AddAsync(new Category()
                {
                    Id = 2,
                    Name = "Category2"
                });

                await arrangeContext.Categories.AddAsync(new Category()
                {
                    Id = 3,
                    Name = "Category3"
                });

                await arrangeContext.EntityCategories.AddAsync(new EntityCategory()
                {
                    EntityId = 2,
                    CategoryId = 3
                });

                await arrangeContext.EntityCategories.AddAsync(new EntityCategory()
                {
                    EntityId = 3,
                    CategoryId = 1
                });

                await arrangeContext.EntityCategories.AddAsync(new EntityCategory()
                {
                    EntityId = 3,
                    CategoryId = 2
                });               

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext =
                new ApplicationDbContext(TestUtils.
                GetOptions(nameof(GetEntityByIdAsyncShould_ReturnTheLogBookWithAllEntityCategoriesAssignedToIt))))
            {
                var sut = new EntityService(actAndAssertContext);
                var result = await sut.GetEntityByIdAsync(1);
                var logbooks = result.LogBooks.ToList();

                Assert.AreEqual(1, result.Id);
                Assert.AreEqual("Entity1", result.Title);
                Assert.AreEqual(null, result.EstablishmentId);
                Assert.AreEqual(2, logbooks.Count);
                Assert.AreEqual("Logbook2", logbooks[0].Title);
                Assert.AreEqual("Logbook3", logbooks[1].Title);
                Assert.AreEqual(1, logbooks[0].Categories.ToList().Count);
                Assert.AreEqual(2, logbooks[1].Categories.ToList().Count);
                Assert.AreEqual(1, logbooks[0].Categories.ToList().Count);
                Assert.AreEqual(3, logbooks[0].Categories.ToList()[0].CategoryId);
                Assert.AreEqual("Category3", logbooks[0].Categories.ToList()[0].Name);
            }
        }

        [TestMethod]
        public async Task GetLogBookByIdAsyncShould_ReturnNullIfEntityWasNotFound()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase(databaseName: "GetLogBookByIdAsyncShould_ReturnNullIfEntityWasNotFound")
               .Options;

            using (var actAndAssertContext =
                new ApplicationDbContext(TestUtils.
                GetOptions(nameof(GetLogBookByIdAsyncShould_ReturnNullIfEntityWasNotFound))))
            {
                var sut = new EntityService(actAndAssertContext);
                var result = await sut.GetLogBookByIdAsync(1); 
                Assert.IsNull(result);
            }
        }


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

