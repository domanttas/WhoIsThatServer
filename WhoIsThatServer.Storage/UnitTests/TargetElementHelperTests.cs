using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using WhoIsThatServer.Storage.Context;
using WhoIsThatServer.Storage.ErrorMessages;
using WhoIsThatServer.Storage.Exceptions;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.UnitTests
{
    public class TargetElementHelperTests
    {
        [Test]
        public void InsertNewTargetElement_ShouldReturn()
        {
            //Arrange
            var fakeTargetsList = new List<TargetElement>()
            {
                new TargetElement
                {
                    Id = 1,
                    HunterPersonId = 56,
                    PreyPersonId = 57,
                    IsHunted = true
                }
            };

            var fakeDbSetTargetElements = UnitTestsUtil.SetupFakeDbSet(fakeTargetsList.AsQueryable());
            var fakeDatabaseContext = A.Fake<DatabaseContext>();

            A.CallTo(() => fakeDatabaseContext.TargetElements).Returns(fakeDbSetTargetElements);
            
            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext())
                .Returns(fakeDatabaseContext);

            var targetElementHelper = new TargetElementHelper(fakeDbContextGeneration);
            
            //Act
            var expectedId = 2;
            var expectedHunterPersonId = 10;
            var expectedPreyPersonId = 11;
            var expectedIsHunter = false;

            var result =
                targetElementHelper.InsertNewTargetElement(expectedId, expectedHunterPersonId, expectedPreyPersonId);
            
            //Assert
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappened();
            A.CallTo(() => fakeDbSetTargetElements.Add(result)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeDatabaseContext.SaveChanges()).MustHaveHappenedOnceExactly();
            
            result.Id.ShouldBe(expectedId);
            result.HunterPersonId.ShouldBe(expectedHunterPersonId);
            result.PreyPersonId.ShouldBe(expectedPreyPersonId);
            result.IsHunted.ShouldBe(expectedIsHunter);
        }

        [Test]
        public void IsPreyHunted_ShouldReturnTrue()
        {
            var elementToRemove = new TargetElement()
            {
                Id = 1,
                HunterPersonId = 100,
                PreyPersonId = 101,
                IsHunted = false
            };
            //Arrange
            var fakeTargetsList = new List<TargetElement>()
            {
                elementToRemove
            };
            
            var fakeDbSetTargetElements = UnitTestsUtil.SetupFakeDbSet(fakeTargetsList.AsQueryable());
            var fakeDatabaseContext = A.Fake<DatabaseContext>();

            A.CallTo(() => fakeDatabaseContext.TargetElements).Returns(fakeDbSetTargetElements);
            
            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext())
                .Returns(fakeDatabaseContext);

            var targetElementHelper = new TargetElementHelper(fakeDbContextGeneration);

            var expectedHunterPersonId = 100;
            var expectedPreyPersonId = 101;
            
            //Act
            var result = targetElementHelper.IsPreyHunted(expectedHunterPersonId, expectedPreyPersonId);
            
            //Assert
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappened();
            A.CallTo(() => fakeDbSetTargetElements.Remove(elementToRemove)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeDatabaseContext.SaveChanges()).MustHaveHappenedOnceExactly();
            
            result.ShouldBe(true);
        }

        [Test]
        public void IsPreyHunter_WrongPreyGoodHunter_ShouldThrow()
        {
            //Arrange
            var fakeTargetsList = new List<TargetElement>()
            {
                new TargetElement
                {
                    Id = 1,
                    HunterPersonId = 56,
                    PreyPersonId = 57,
                    IsHunted = true
                }
            };
            
            var fakeDbSetTargetElements = UnitTestsUtil.SetupFakeDbSet(fakeTargetsList.AsQueryable());
            var fakeDatabaseContext = A.Fake<DatabaseContext>();

            A.CallTo(() => fakeDatabaseContext.TargetElements).Returns(fakeDbSetTargetElements);
            
            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext())
                .Returns(fakeDatabaseContext);

            var targetElementHelper = new TargetElementHelper(fakeDbContextGeneration);

            var expectedHunterPersonId = 56;
            var expectedPreyPersonId = 102;
            
            //Act and assert
            Assert.Throws<ManagerException>(() =>
                targetElementHelper.IsPreyHunted(expectedHunterPersonId, expectedPreyPersonId), StorageErrorMessages.TargetNotFoundError);
            
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappened();
        }
        
        [Test]
        public void IsPreyHunter_GoodPreyWrongHunter_ShouldThrow()
        {
            //Arrange
            var fakeTargetsList = new List<TargetElement>()
            {
                new TargetElement
                {
                    Id = 1,
                    HunterPersonId = 56,
                    PreyPersonId = 57,
                    IsHunted = true
                }
            };
            
            var fakeDbSetTargetElements = UnitTestsUtil.SetupFakeDbSet(fakeTargetsList.AsQueryable());
            var fakeDatabaseContext = A.Fake<DatabaseContext>();

            A.CallTo(() => fakeDatabaseContext.TargetElements).Returns(fakeDbSetTargetElements);
            
            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext())
                .Returns(fakeDatabaseContext);

            var targetElementHelper = new TargetElementHelper(fakeDbContextGeneration);

            var expectedHunterPersonId = 71;
            var expectedPreyPersonId = 57;
            
            //Act and assert      
            Assert.Throws<ManagerException>(() =>
                targetElementHelper.IsPreyHunted(expectedHunterPersonId, expectedPreyPersonId), StorageErrorMessages.TargetNotFoundError);
            
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappened();
        }

        [Test]
        public void AssignRandomTarget_ShouldReturnAssignedId()
        {
            //Arrange
            var expectedPreyId = 2;
            var expectedHunterId = 10;
            
            var dbElementsList = new List<DatabaseImageElement>()
            {
                new DatabaseImageElement()
                {
                    Id = expectedPreyId,
                    DescriptiveSentence = "test",
                    ImageContentUri = "test",
                    ImageName = "test",
                    PersonFirstName = "test",
                    PersonLastName = "test",
                    Score = 1
                },
                
                new DatabaseImageElement()
                {
                    Id = expectedPreyId,
                    DescriptiveSentence = "test",
                    ImageContentUri = "test",
                    ImageName = "test",
                    PersonFirstName = "test",
                    PersonLastName = "test",
                    Score = 1
                }
            };

            var fakeDbSetImageElements = UnitTestsUtil.SetupFakeDbSet(dbElementsList.AsQueryable());
            var fakeDbSetTargetElements = UnitTestsUtil.SetupFakeDbSet(new List<TargetElement>().AsQueryable());
            
            var fakeDatabaseContext = A.Fake<DatabaseContext>();

            A.CallTo(() => fakeDatabaseContext.DatabaseImageElements).Returns(fakeDbSetImageElements);
            A.CallTo(() => fakeDatabaseContext.TargetElements).Returns(fakeDbSetTargetElements);
            
            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext())
                .Returns(fakeDatabaseContext);

            var databaseElementHelper = A.Fake<IDatabaseImageElementHelper>();
            var targetElementHelper = new TargetElementHelper(fakeDbContextGeneration, databaseElementHelper);

            A.CallTo(() => databaseElementHelper.GetAllImages()).Returns(dbElementsList);
            
            //Act
            var result = targetElementHelper.AssignRandomTarget(expectedHunterId);
            
            //Assert
            result.ShouldBe(expectedPreyId);
            
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappened();
        }

        [Test]
        public void AssignRandomTarget_ShouldThrow_TargetAlreadyAssigned()
        {
            //Arrange
            
        }
    }
}