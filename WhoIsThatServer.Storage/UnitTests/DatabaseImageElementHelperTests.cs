using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        [Test]
        public void InsertNewImageElement_ShouldReturn()
        {
            //Arrange
            var fakeDatabaseImageElementList = new List<DatabaseImageElement>();
            var fakeDbSet = UnitTestsUtil.SetupFakeDbSet(fakeDatabaseImageElementList.AsQueryable());
            
            var fakeDatabaseContext = A.Fake<DatabaseContext>();
            A.CallTo(() => fakeDatabaseContext.DatabaseImageElements)
                .Returns(fakeDbSet);
            
            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext())
                .Returns(fakeDatabaseContext);
            
            var databaseImageElementHelper = new DatabaseImageElementHelper(fakeDbContextGeneration);
            
            //Act
            var expectedId = 1;
            var expectedImageName = "testImageName";
            var expectedImageContentUri = "testURI";
            var expectedPersonFirstName = "testPersonFirstName";
            var expectedPersonLastName = "testPersonLastName";

            var result = databaseImageElementHelper.InsertNewImageElement(expectedId, expectedImageName,
                expectedImageContentUri, expectedPersonFirstName, expectedPersonLastName);
            
            //Assert
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappened();
            A.CallTo(() => fakeDbSet.Add(result)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeDatabaseContext.SaveChanges()).MustHaveHappenedOnceExactly();
            
            result.Id.ShouldBe(expectedId);
            result.ImageName.ShouldBe(expectedImageName);
            result.ImageContentUri.ShouldBe(expectedImageContentUri);
            result.PersonFirstName.ShouldBe(expectedPersonFirstName);
            result.PersonLastName.ShouldBe(expectedPersonLastName);
        }
    }
}