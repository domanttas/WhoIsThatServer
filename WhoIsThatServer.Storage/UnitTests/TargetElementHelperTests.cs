using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using WhoIsThatServer.Storage.Context;
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
    }
}