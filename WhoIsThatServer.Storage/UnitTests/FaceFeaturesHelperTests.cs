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
    public class FaceFeaturesHelperTests
    {
        [Test]
        public void InsertNewFeaturesModel_ShouldReturn()
        {
            //Arrange
            var fakeFeaturesList = new List<FaceFeaturesModel>();
            var fakeDbSet = UnitTestsUtil.SetupFakeDbSet(fakeFeaturesList.AsQueryable());

            var fakeDbContext = A.Fake<DatabaseContext>();
            A.CallTo(() => fakeDbContext.FaceFeatures).Returns(fakeDbSet);

            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).Returns(fakeDbContext);

            var faceFeaturesHelper = new FaceFeaturesHelper(fakeDbContextGeneration);
            
            var expectedId = 0;
            var expectedPersonId = 1;
            var expectedAge = 20;
            var expectedGender = "male";
            
            //Act
            var result = faceFeaturesHelper.InsertNewFeaturesModel(expectedPersonId, expectedAge, expectedGender);
            
            //Assert
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappened();
            A.CallTo(() => fakeDbSet.Add(result)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeDbContext.SaveChanges()).MustHaveHappenedOnceExactly();
            
            result.Id.ShouldBe(expectedId);
            result.PersonId.ShouldBe(expectedPersonId);
            result.Age.ShouldBe(expectedAge);
            result.Gender.ShouldBe(expectedGender);
        }

        [Test]
        public void GetFaceFeaturesByPersonId_WhenIdExists_ShouldReturn()
        {
            //Arrange
            var expectedId = 0;
            var expectedPersonId = 1;
            var expectedAge = 20;
            var expectedGender = "male";

            var fakeIQueryable = new List<FaceFeaturesModel>()
            {
                new FaceFeaturesModel()
                {
                    Id = expectedId,
                    PersonId = expectedPersonId,
                    Age = expectedAge,
                    Gender = expectedGender
                }
            }.AsQueryable();

            var fakeDbSet = UnitTestsUtil.SetupFakeDbSet(fakeIQueryable);
            
            var fakeDbContext = A.Fake<DatabaseContext>();
            A.CallTo(() => fakeDbContext.FaceFeatures).Returns(fakeDbSet);
            
            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).Returns(fakeDbContext);
            
            var faceFeaturesHelper = new FaceFeaturesHelper(fakeDbContextGeneration);
            
            //Act
            var result = faceFeaturesHelper.GetFaceFeaturesByPersonId(expectedPersonId);
            
            //Assert
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappenedOnceExactly();
            
            result.Id.ShouldBe(expectedId);
            result.Age.ShouldBe(expectedAge);
            result.Gender.ShouldBe(expectedGender);
            result.PersonId.ShouldBe(expectedPersonId);
        }

        [Test]
        public void GetFaceFeaturesByPersonId_WhenIdDoesNotExists_ShouldThrow()
        {
            //Arrange
            var expectedId = 0;
            var expectedPersonId = 1;
            var expectedAge = 20;
            var expectedGender = "male";

            var fakeIQueryable = new List<FaceFeaturesModel>()
            {
                new FaceFeaturesModel()
                {
                    Id = expectedId,
                    PersonId = expectedPersonId,
                    Age = expectedAge,
                    Gender = expectedGender
                }
            }.AsQueryable();

            var fakeDbSet = UnitTestsUtil.SetupFakeDbSet(fakeIQueryable);
            
            var fakeDbContext = A.Fake<DatabaseContext>();
            A.CallTo(() => fakeDbContext.FaceFeatures).Returns(fakeDbSet);
            
            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).Returns(fakeDbContext);
            
            var faceFeaturesHelper = new FaceFeaturesHelper(fakeDbContextGeneration);
            
            //Act and assert
            Assert.Throws<ManagerException>(() => faceFeaturesHelper.GetFaceFeaturesByPersonId(100),
                StorageErrorMessages.UserDoesNotExistError);

            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappenedOnceExactly();
        }
    }
}