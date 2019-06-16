namespace MyPlace.Services.Tests.NoteServiceTests
{
    using System;
    using MyPlace.Data;
    using MyPlace.Services;
    using MyPlace.DataModels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    class AddAsyncShould
    {
        //[TestMethod]
        //public async Task Suceed_WhenAddingNoteWithCorrectParameters()
        //{
        //    //var options = new DbContextOptionsBuilder()
        //    //    .UseInMemoryDatabase(databaseName: "Suceed_WhenUserExists")
        //    //    .Options;

        //    using (var arrangeContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(Suceed_WhenAddingNoteWithCorrectParameters))))
        //    {
        //        arrangeContext.Notes.Add(new Note()
        //        {
        //            EntityId = 1,
        //            UserId = "userId",
        //            Text = "Note1",
        //            CategoryId = 1,
        //            Date = DateTime.Now,
        //            IsCompleted = false
        //        });               

        //        arrangeContext.SaveChanges();
        //    }

        //    using (var actAndAssertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(Suceed_WhenUserAndBookExistsAndTheUserHasNotRatedTheBookBefore))))
        //    {
        //        var sut = new NoteService(actAndAssertContext);

        //        var note = await sut.AddAsync(1, "userId", "Note1", 1, DateTime.Now, false);
        //        //var note = new Note() { }; 
        //        Assert.AreEqual(1, note.EntityId);
        //        Assert.AreEqual("TestTitle", note.UserId);
        //        Assert.AreEqual("Note1", note.Text);
        //       // Assert.AreEqual(DateTime.Now,note.Date);
        //        Assert.AreEqual(false, note.IsCompleted);
        //    }
        }
    }
}

