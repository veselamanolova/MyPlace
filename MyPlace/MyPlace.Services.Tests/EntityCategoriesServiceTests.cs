
namespace MyPlace.Services.Tests
{
    using MyPlace.Data;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyPlace.DataModels;

    [TestClass]
    public class EntityCategoriesServiceTests
    {
        [TestMethod]
        public async Task GetAllEntityCategoriesAsyncShould_ReturnAllEntityCategories()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase(databaseName: "GetAllEntityCategoriesAsyncShould_ReturnAllEntityCategories")
               .Options;

            using (var arrangeContext = new ApplicationDbContext(TestUtils
                  .GetOptions(nameof(GetAllEntityCategoriesAsyncShould_ReturnAllEntityCategories))))
            {
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
                    EntityId = 1,
                    CategoryId = 1
                });

                await arrangeContext.EntityCategories.AddAsync(new EntityCategory()
                {
                    EntityId = 1,
                    CategoryId = 2
                });

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext =
                new ApplicationDbContext(TestUtils.
                GetOptions(nameof(GetAllEntityCategoriesAsyncShould_ReturnAllEntityCategories))))
            {
                var sut = new EntityCategoriesService(actAndAssertContext);
                var result = await sut.GetAllEntityCategoriesAsync(1); 

                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(1, result[0].CategoryId);
                Assert.AreEqual("Category1", result[0].Name);
                Assert.AreEqual(2, result[1].CategoryId);
                Assert.AreEqual("Category2", result[1].Name);
            }
        }

        [TestMethod]
        public async Task GetAllEntityCategoriesAsyncShould_ReturnEmptyListIfEntityHasNoCategories()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase(databaseName: "GetAllEntityCategoriesAsyncShould_ReturnEmptyListIfEntityHasNoCategories")
               .Options;          

            using (var actAndAssertContext =
                new ApplicationDbContext(TestUtils.
                GetOptions(nameof(GetAllEntityCategoriesAsyncShould_ReturnEmptyListIfEntityHasNoCategories))))
            {
                var sut = new EntityCategoriesService(actAndAssertContext);
                var result = await sut.GetAllEntityCategoriesAsync(1);
                Assert.AreEqual(0, result.Count);
            }
        }
    }
}
