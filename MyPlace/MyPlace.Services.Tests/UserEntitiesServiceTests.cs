namespace MyPlace.Services.Tests
{
    using Moq;
    using MyPlace.Data;
    using MyPlace.DataModels;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserEntitiesServiceTests
    {
        [TestMethod]
        public async Task GetAllUsersAsyncShould_ReturnAllUsers()
        {
            using (var arrangeContext =
                new ApplicationDbContext(TestUtils.GetOptions(nameof(GetAllUsersAsyncShould_ReturnAllUsers))))
            {
                arrangeContext.Users.Add(new User()
                {
                    Id = "xxxx",
                    UserName = "User1"                    
                });

                arrangeContext.Users.Add(new User()
                {
                    Id = "yyyy",
                    UserName = "User2"
                });

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(GetAllUsersAsyncShould_ReturnAllUsers))))
            {
                var mockUserStore = new Mock<IUserStore<User>>();
                var userManager = new UserManager<User>(mockUserStore.Object, null, null, null, null, null, null, null, null);

                var sut = new UserEntitiesService(actAndAssertContext, userManager);
                var users = await sut.GetAllUsersAsync(); 

                Assert.IsNotNull(users);
                var usersList = users;
                Assert.AreEqual(2, usersList.Count);
                Assert.AreEqual("xxxx", usersList[0].Id);
                Assert.AreEqual("User1", usersList[0].Name); 
                Assert.AreEqual("yyyy", usersList[1].Id);
                Assert.AreEqual("User2", usersList[1].Name);
            }
        }

        [TestMethod]
        public async Task GetAllUsersAsyncShould_ReturnEmptyListIfThereAreNoUsers()
        {           
            using (var actAndAssertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(GetAllUsersAsyncShould_ReturnEmptyListIfThereAreNoUsers))))
            {
                var mockUserStore = new Mock<IUserStore<User>>();
                var userManager = new UserManager<User>(mockUserStore.Object, null, null, null, null, null, null, null, null);

                var sut = new UserEntitiesService(actAndAssertContext, userManager);
                var users = await sut.GetAllUsersAsync();

                Assert.IsNotNull(users);
                Assert.AreEqual(0, users.Count);
            }
        }

        [TestMethod]
        public async Task GetAllUserEntitiesAsyncShould_ReturnAllEntitiesTheUsersIsAssignedTo()
        {
            using (var arrangeContext =
                new ApplicationDbContext(TestUtils.GetOptions(nameof(GetAllUserEntitiesAsyncShould_ReturnAllEntitiesTheUsersIsAssignedTo))))
            {
                arrangeContext.Users.Add(new User()
                {
                    Id = "xxxx",
                    UserName = "User1"
                });

                var entity1 = new Entity()
                {
                    Id = 1,
                    Title = "Entity1",                     
                };

                var entity2 = new Entity()
                {
                    Id = 2,
                    Title = "Entity2",
                };

                arrangeContext.Entities.Add(entity1);
                arrangeContext.Entities.Add(entity2);

                arrangeContext.UsersEntities.Add(new UserEntity()
                {
                    UserId = "xxxx",
                    EntityId = 1,
                    Entity = entity1
                });

                arrangeContext.UsersEntities.Add(new UserEntity()
                {
                    UserId = "xxxx",
                    EntityId = 2,
                    Entity = entity2
                });

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(GetAllUserEntitiesAsyncShould_ReturnAllEntitiesTheUsersIsAssignedTo))))
            {
                var mockUserStore = new Mock<IUserStore<User>>();
                var userManager = new UserManager<User>(mockUserStore.Object, null, null, null, null, null, null, null, null);

                var sut = new UserEntitiesService(actAndAssertContext, userManager);
                var result = await sut.GetAllUserEntitiesAsync("xxxx"); 

                Assert.IsNotNull(result);                
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual("xxxx", result[0].UserId);
                Assert.AreEqual("xxxx", result[1].UserId);
                Assert.AreEqual(1, result[0].EntityId);
                Assert.AreEqual(2, result[1].EntityId);
                Assert.AreEqual("Entity1", result[0].Title);
                Assert.AreEqual("Entity2", result[1].Title);
            }
        }

        [TestMethod]
        public async Task GetAllUserEntitiesAsyncShould_ReturnEmptyListIfUserDoesNotHaveEntities()
        {
            using (var arrangeContext =
                new ApplicationDbContext(TestUtils.GetOptions(nameof(GetAllUserEntitiesAsyncShould_ReturnEmptyListIfUserDoesNotHaveEntities))))
            {
                arrangeContext.Users.Add(new User()
                {
                    Id = "xxxx",
                    UserName = "User1"
                });              

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(GetAllUserEntitiesAsyncShould_ReturnEmptyListIfUserDoesNotHaveEntities))))
            {
                var mockUserStore = new Mock<IUserStore<User>>();
                var userManager = new UserManager<User>(mockUserStore.Object, null, null, null, null, null, null, null, null);

                var sut = new UserEntitiesService(actAndAssertContext, userManager);
                var result = await sut.GetAllUserEntitiesAsync("xxxx");

                Assert.IsNotNull(result);
                Assert.AreEqual(0, result.Count);              
            }
        }



    }
}

