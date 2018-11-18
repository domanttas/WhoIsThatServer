using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using WhoIsThatServer.Storage.Constants;
using WhoIsThatServer.Storage.Context;
using WhoIsThatServer.Storage.ErrorMessages;
using WhoIsThatServer.Storage.Exceptions;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.UnitTests
{
    public class HistoryModelHelperTests
    {
        [Test]
        public void InsertNewHistoryElement_ShouldReturn()
        {
            //Arrange
            var fakeHistoryList = new List<HistoryModel>()
            {
                new HistoryModel()
                {
                    UserId = 1,
                    TargetId = 2,
                    Status = StatusConstants.TargetHuntedHistory
                }
            };

            var expectedUserId = 5;
            var expectedTargetId = 6;
            var expectedStatus = StatusConstants.TargetNotHuntedHistory;

            var fakeDbSetHistoryElements = UnitTestsUtil.SetupFakeDbSet(fakeHistoryList.AsQueryable());
            var fakeDbContext = A.Fake<DatabaseContext>();

            A.CallTo(() => fakeDbContext.History).Returns(fakeDbSetHistoryElements);
            
            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext())
                .Returns(fakeDbContext);

            var historyModelHelper = new HistoryModelHelper(fakeDbContextGeneration);
            
            //Act
            var result = historyModelHelper.InsertNewHistoryElement(expectedUserId, expectedTargetId, expectedStatus);
            
            //Assert
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappened();
            A.CallTo(() => fakeDbContext.History.Add(result)).MustHaveHappened();
            A.CallTo(() => fakeDbContext.SaveChanges()).MustHaveHappened();
            
            result.UserId.ShouldBe(expectedUserId);
            result.TargetId.ShouldBe(expectedTargetId);
            result.Status.ShouldBe(expectedStatus);
        }

        [Test]
        public void GetHistoryByUserId_ShouldReturnExpected()
        {
            //Arrange
            var expectedUserId = 1;
            var expectedTargetId = 2;
            var expectedStatus = StatusConstants.TargetHuntedHistory;

            var fakeHistoryList = new List<HistoryModel>()
            {
                new HistoryModel()
                {
                    UserId = expectedUserId,
                    TargetId = expectedTargetId,
                    Status = expectedStatus
                }
            }.AsQueryable();
            
            var fakeDbSetHistoryElements = UnitTestsUtil.SetupFakeDbSet(fakeHistoryList.AsQueryable());
            var fakeDbContext = A.Fake<DatabaseContext>();

            A.CallTo(() => fakeDbContext.History).Returns(fakeDbSetHistoryElements);
            
            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).Returns(fakeDbContext);
            
            var historyModelHelper = new HistoryModelHelper(fakeDbContextGeneration);
            
            //Act
            var result = historyModelHelper.GetHistoryByUserId(expectedUserId);
            
            //Assert
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappened();
            
            result.ElementAt(0).UserId.ShouldBe(expectedUserId);
            result.ElementAt(0).TargetId.ShouldBe(expectedTargetId);
            result.ElementAt(0).Status.ShouldBe(expectedStatus);
        }

        [Test]
        public void GetHistoryByUserId_ShouldThrow()
        {
            //Arrange
            var expectedUserId = 1;
            var expectedTargetId = 2;
            var expectedStatus = StatusConstants.TargetHuntedHistory;

            var fakeHistoryList = new List<HistoryModel>()
            {
                new HistoryModel()
                {
                    UserId = expectedUserId,
                    TargetId = expectedTargetId,
                    Status = expectedStatus
                }
            }.AsQueryable();
            
            var fakeDbSetHistoryElements = UnitTestsUtil.SetupFakeDbSet(fakeHistoryList.AsQueryable());
            var fakeDbContext = A.Fake<DatabaseContext>();

            A.CallTo(() => fakeDbContext.History).Returns(fakeDbSetHistoryElements);
            
            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).Returns(fakeDbContext);
            
            var historyModelHelper = new HistoryModelHelper(fakeDbContextGeneration);

            //Act and assert
            Assert.Throws<ManagerException>(() => historyModelHelper.GetHistoryByUserId(500),
                StorageErrorMessages.HistoryElementNotFoundError);
        }

        [Test]
        public void UpdateHistoryModel_ShouldReturnExpected()
        {
            //Arrange
            var expectedUserId = 1;
            var expectedTargetId = 2;
            var expectedStatus = StatusConstants.TargetHuntedHistory;
            
            var expectedElement = new HistoryModel()
            {
                Id = 1,
                UserId = expectedUserId,
                TargetId = expectedTargetId,
                Status = StatusConstants.TargetNotHuntedHistory
            };
            
            var fakeHistoryList = new List<HistoryModel>()
            {
                expectedElement
            }.AsQueryable();
            
            var fakeDbSetHistoryElements = UnitTestsUtil.SetupFakeDbSet(fakeHistoryList.AsQueryable());
            var fakeDbContext = A.Fake<DatabaseContext>();

            A.CallTo(() => fakeDbContext.History).Returns(fakeDbSetHistoryElements);
            
            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).Returns(fakeDbContext);
            
            var historyModelHelper = new HistoryModelHelper(fakeDbContextGeneration);
            
            //Act
            var result = historyModelHelper.UpdateHistoryModel(expectedUserId, expectedTargetId);
            
            //Assert
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappened();
            A.CallTo(() => fakeDbContext.SaveChanges()).MustHaveHappened();
            
            result.Id.ShouldBe(1);
            result.UserId.ShouldBe(expectedUserId);
            result.TargetId.ShouldBe(expectedTargetId);
            result.Status.ShouldBe(expectedStatus);
        }

        [Test]
        public void UpdateHistoryModel_ShouldThrow()
        {
            var historyElement = new HistoryModel()
            {
                Id = 1,
                UserId = 10,
                TargetId = 11,
                Status = StatusConstants.TargetNotHuntedHistory
            };
            
            var fakeHistoryList = new List<HistoryModel>()
            {
                historyElement
            }.AsQueryable();
            
            var fakeDbSetHistoryElements = UnitTestsUtil.SetupFakeDbSet(fakeHistoryList.AsQueryable());
            var fakeDbContext = A.Fake<DatabaseContext>();

            A.CallTo(() => fakeDbContext.History).Returns(fakeDbSetHistoryElements);
            
            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).Returns(fakeDbContext);
            
            var historyModelHelper = new HistoryModelHelper(fakeDbContextGeneration);
            
            //Act and assert
            Assert.Throws<ManagerException>(() => historyModelHelper.UpdateHistoryModel(500, 300),
                StorageErrorMessages.UserDoesNotExistError);
        }
    }
}