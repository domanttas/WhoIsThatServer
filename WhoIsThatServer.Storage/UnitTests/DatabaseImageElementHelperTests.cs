using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhoIsThatServer.Storage.Context;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.UnitTests
{
    public class DatabaseImageElementHelperTests
    {
        [Test]
        public void GetAllImages_ShouldReturn()
        {
            //Arrange
            var fakeDatabaseImageElementList = new List<DatabaseImageElement>()
            {
                new DatabaseImageElement
                {
                    Id = 1,
                    ImageName = "firstTestName",
                    ImageContentUri = "firstTestUri",
                    PersonFirstName = "firstTestPersonName",
                    PersonLastName = "firstTestPersonLastName"
                },

                new DatabaseImageElement
                {
                    Id = 2,
                    ImageName = "secondTestName",
                    ImageContentUri = "secondTestUri",
                    PersonFirstName = "secondTestPersonName",
                    PersonLastName = "secondTestPersonLastName"
                }
            };

            var fakeDbSetImageElementObjects = UnitTestsUtil.SetupFakeDbSet(fakeDatabaseImageElementList.AsQueryable());
            var fakeDatabaseContext = A.Fake<DatabaseContext>();

            A.CallTo(() => fakeDatabaseContext.DatabaseImageElements)
                .Returns(fakeDbSetImageElementObjects);

            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext())
                .Returns(fakeDatabaseContext);

            var databaseImageElementHelper = new DatabaseImageElementHelper(fakeDbContextGeneration);

            //Act
            var result = databaseImageElementHelper.GetAllImages();

            //Assert
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappened();
            result.ShouldBe(fakeDatabaseImageElementList);
        }
    }
}