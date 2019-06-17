namespace MyPlace.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyPlace.Data;
    using MyPlace.DataModels;
    using System.Threading.Tasks;

    [TestClass]
    public class CategoryServiceTests
    {
        [TestMethod]
        public async Task GetAllCategoriesAsyncShould_SuceedWhenCategoriesExist()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName: "GetAllCategoriesAsyncShould_SuceedWhenCategoriesExist")
                .Options;

            using (var arrangeContext =
                new ApplicationDbContext(TestUtils.GetOptions(nameof(GetAllCategoriesAsyncShould_SuceedWhenCategoriesExist))))
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

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext =
                new ApplicationDbContext(TestUtils.
                GetOptions(nameof(GetAllCategoriesAsyncShould_SuceedWhenCategoriesExist))))
            {
                var sut = new CategoryService(actAndAssertContext);
                var categories = await sut.GetAllCategoriesAsync();
                Assert.AreEqual(2, categories.Count);
                Assert.AreEqual(1, categories[0].CategoryId);
                Assert.AreEqual("Category1", categories[0].Name);
                Assert.AreEqual(2, categories[1].CategoryId);
                Assert.AreEqual("Category2", categories[1].Name);
            }
        }

        [TestMethod]
        public async Task GetAllCategoriesAsyncShould_ReturnsEmptyListWhenNoCategoriesExist()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName: "GetAllCategoriesAsyncShould_ReturnsEmptyListWhenNoCategoriesExist")
                .Options;

            using (var actAndAssertContext =
                new ApplicationDbContext(TestUtils.
                    GetOptions(nameof(GetAllCategoriesAsyncShould_ReturnsEmptyListWhenNoCategoriesExist))))
            {
                var sut = new CategoryService(actAndAssertContext);
                var categories = await sut.GetAllCategoriesAsync();
                Assert.AreEqual(0, categories.Count);
            }
        }

        [TestMethod]
        public async Task AddCategoryAsyncShould_SuccedCreatingNewCategory()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName: "AddCategoryAsyncShould_SuccedCreatingNewCategory")
                .Options;

            using (var actAndAssertContext = 
                new ApplicationDbContext(TestUtils.GetOptions(nameof(AddCategoryAsyncShould_SuccedCreatingNewCategory))))
            {
                var sut = new CategoryService(actAndAssertContext);
                await sut.AddCategoryAsync("categoryName");
                //If no exception is thrown the test has passed.
            }
        }

        [TestMethod]
        public async Task DeleteCategoryAsync()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase(databaseName: "DeleteCategoryAsync")
               .Options;

            using (var arrangeContext =
                new ApplicationDbContext(TestUtils.GetOptions(nameof(DeleteCategoryAsync))))
            {
                await arrangeContext.Categories.AddAsync(new Category()
                {
                    Id = 1,
                    Name = "Category1"
                });
               
                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext =
                new ApplicationDbContext(TestUtils.
                GetOptions(nameof(DeleteCategoryAsync))))
            {
                var sut = new CategoryService(actAndAssertContext);
                await sut.EditCategoryAsync(1, "NewCategoryName");

                var category = await sut.FindCategoryByIdAsync(1); 
                Assert.AreEqual("NewCategoryName", category.Name);               
            }
        }

        [TestMethod]
        public async Task DeleteCategoryAsyncShould_Succed()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase(databaseName: "DeleteCategoryAsyncShould_Succed")
               .Options;

            using (var arrangeContext = new ApplicationDbContext(TestUtils
                    .GetOptions(nameof(DeleteCategoryAsyncShould_Succed))))
            {
                await arrangeContext.Categories.AddAsync(new Category()
                {
                    Id = 1,
                    Name = "Category1"
                });

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext =
                new ApplicationDbContext(TestUtils.
                GetOptions(nameof(DeleteCategoryAsyncShould_Succed))))
            {
                var sut = new CategoryService(actAndAssertContext);
                await sut.DeleteCategoryAsync(1);
                var allCategories = await sut.GetAllCategoriesAsync();
                Assert.AreEqual(0, allCategories.Count);
            }
        }

        [TestMethod]
        public async Task FindCategoryByIdAsyncShould_SuccedWhenCategoryExists()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase(databaseName: "FindCategoryByIdAsyncShould_SuccedWhenCategoryExists")
               .Options;

            using (var arrangeContext = new ApplicationDbContext(TestUtils
                    .GetOptions(nameof(FindCategoryByIdAsyncShould_SuccedWhenCategoryExists))))
            {
                await arrangeContext.Categories.AddAsync(new Category()
                {
                    Id = 1,
                    Name = "Category1"
                });

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext =
                new ApplicationDbContext(TestUtils.
                GetOptions(nameof(FindCategoryByIdAsyncShould_SuccedWhenCategoryExists))))
            {
                var sut = new CategoryService(actAndAssertContext);

                var result = await sut.FindCategoryByIdAsync(1);
                Assert.AreEqual(1, result.CategoryId);
                Assert.AreEqual("Category1", result.Name);
            }
        }

        [TestMethod]
        public async Task FindCategoryByIdAsyncShould_FailWhenCategoryDoesNotExist()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase(databaseName: "FindCategoryByIdAsyncShould_FailWhenCategoryDoesNotExist")
               .Options;

            using (var arrangeContext = new ApplicationDbContext(TestUtils
                    .GetOptions(nameof(FindCategoryByIdAsyncShould_FailWhenCategoryDoesNotExist))))
            {
                await arrangeContext.Categories.AddAsync(new Category()
                {
                    Id = 1,
                    Name = "Category1"
                });

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext =
                new ApplicationDbContext(TestUtils.
                GetOptions(nameof(FindCategoryByIdAsyncShould_FailWhenCategoryDoesNotExist))))
            {
                var sut = new CategoryService(actAndAssertContext);                
                Assert.IsNull(await sut.FindCategoryByIdAsync(2)); 
            }
        }

        [TestMethod]
        public async Task GetAllLogBooksCategoriesAsyncShould_Succeed()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase(databaseName: "GetAllLogBooksCategoriesAsyncShould_Succeed")
               .Options;

            using (var arrangeContext = new ApplicationDbContext(TestUtils
                    .GetOptions(nameof(GetAllLogBooksCategoriesAsyncShould_Succeed))))
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
                GetOptions(nameof(GetAllLogBooksCategoriesAsyncShould_Succeed))))
            {
                var sut = new CategoryService(actAndAssertContext);
                var result = await sut.GetAllLogBooksCategoriesAsync(1);

                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(1, result[0].CategoryId);
                Assert.AreEqual("Category1", result[0].Name);
                Assert.AreEqual(2, result[1].CategoryId);
                Assert.AreEqual("Category2", result[1].Name);
            }
        }

        [TestMethod]
        public async Task GetAllLogBooksCategoriesAsyncShould_ReturnEmptyListIfThereAreNoCategoriesAssigned()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase(databaseName: "GetAllLogBooksCategoriesAsyncShould_ReturnEmptyListIfThereAreNoCategoriesAssigned")
               .Options;
          
            using (var actAndAssertContext =
                new ApplicationDbContext(TestUtils.
                GetOptions(nameof(GetAllLogBooksCategoriesAsyncShould_ReturnEmptyListIfThereAreNoCategoriesAssigned))))
            {
                var sut = new CategoryService(actAndAssertContext);
                var result = await sut.GetAllLogBooksCategoriesAsync(1);
                Assert.AreEqual(0, result.Count);
            }
        }

        [TestMethod]
        public async Task GetAllLogBooksCategoriesAsyncShould_ReturnAllLogBookAndNotLogBookCategories()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase(databaseName: "GetAllLogBooksCategoriesAsyncShould_ReturnAllLogBookAndNotLogBookCategories")
               .Options;

            using (var arrangeContext = new ApplicationDbContext(TestUtils
                  .GetOptions(nameof(GetAllLogBooksCategoriesAsyncShould_ReturnAllLogBookAndNotLogBookCategories))))
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
                GetOptions(nameof(GetAllLogBooksCategoriesAsyncShould_ReturnAllLogBookAndNotLogBookCategories))))
            {
                var sut = new CategoryService(actAndAssertContext);
                var result = await sut.GetAllLogBookAndNotLogBookCategories(1);

                Assert.AreEqual(2, result.EntityCategories.Count);
                Assert.AreEqual(1, result.EntityCategories[0].CategoryId);
                Assert.AreEqual("Category1", result.EntityCategories[0].Name);
                Assert.AreEqual(2, result.EntityCategories[1].CategoryId);
                Assert.AreEqual("Category2", result.EntityCategories[1].Name);

                Assert.AreEqual(1, result.AllNotEntityCategories.Count);
                Assert.AreEqual(3, result.AllNotEntityCategories[0].CategoryId);
                Assert.AreEqual("Category3", result.AllNotEntityCategories[0].Name);
            }
        }

        [TestMethod]
        public async Task GetAllLogBooksCategoriesAsyncShould_ReturnEmptyListsForLogBookAndNotLogBookCategoriesIfTheyDoNotExist()
        {
            var options = new DbContextOptionsBuilder()
               .UseInMemoryDatabase(databaseName: "GetAllLogBooksCategoriesAsyncShould_ReturnEmptyListsForLogBookAndNotLogBookCategoriesIfTheyDoNotExist")
               .Options;
          
            using (var actAndAssertContext =
                new ApplicationDbContext(TestUtils.
                GetOptions(nameof(GetAllLogBooksCategoriesAsyncShould_ReturnEmptyListsForLogBookAndNotLogBookCategoriesIfTheyDoNotExist))))
            {
                var sut = new CategoryService(actAndAssertContext);
                var result = await sut.GetAllLogBookAndNotLogBookCategories(1);
                
                Assert.AreEqual(0, result.EntityCategories.Count);
                Assert.AreEqual(0, result.AllNotEntityCategories.Count);
            }
        }

    }
}
