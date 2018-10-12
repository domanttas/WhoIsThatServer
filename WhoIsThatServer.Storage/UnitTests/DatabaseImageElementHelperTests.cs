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
                    ImageContentUri = "https://whoisthatserverimages.blob.core.windows.net/images/Domantas_test.jpg",
                    PersonFirstName = "firstTestPersonName",
                    PersonLastName = "firstTestPersonLastName"
                },

                new DatabaseImageElement
                {
                    Id = 2,
                    ImageName = "secondTestName",
                    ImageContentUri = "https://whoisthatserverimages.blob.core.windows.net/images/Domantas_test.jpg",
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
            var expectedImageContentUri = "https://whoisthatserverimages.blob.core.windows.net/images/Domantas_test.jpg";
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

        [Test]
        public void InsertNewImageElement_ShouldReturnNull_SameUri()
        {
            //Arrange
            var fakeDatabaseImageElementList = new List<DatabaseImageElement>()
            {
                new DatabaseImageElement()
                {
                    Id = 1,
                    ImageName = "testImageName",
                    ImageContentUri = "https://whoisthatserverimages.blob.core.windows.net/images/Domantas_test.jpg",
                    PersonFirstName = "testFirstPersonName",
                    PersonLastName = "testFirstPersonLastName"
                }
            };
            
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
            var expectedImageContentUri = "https://whoisthatserverimages.blob.core.windows.net/images/Domantas_test.jpg";
            var expectedPersonFirstName = "testPersonFirstName";
            var expectedPersonLastName = "testPersonLastName";

            var result = databaseImageElementHelper.InsertNewImageElement(expectedId, expectedImageName,
                expectedImageContentUri, expectedPersonFirstName, expectedPersonLastName);
            
            //Assert
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappened();
            result.ShouldBe(null);
        }

        [Test]
        public void InsertNewImageElement_ShouldReturnNull_BadFileName()
        {
            //Arrange
            var fakeDatabaseImageElementList = new List<DatabaseImageElement>()
            {
                new DatabaseImageElement()
                {
                    Id = 1,
                    ImageName = "testImageName",
                    ImageContentUri = "https://whoisthatserverimages.blob.core.windows.test.net/images/Domantas_test.jpg",
                    PersonFirstName = "testFirstPersonName",
                    PersonLastName = "testFirstPersonLastName"
                }
            };
            
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
            var expectedImageName = "testImage@@Name";
            var expectedImageContentUri = "https://whoisthatserverimages.blob.core.windows.net/images/Domantas_test.jpg";
            var expectedPersonFirstName = "testPersonFirstName";
            var expectedPersonLastName = "testPersonLastName";

            var result = databaseImageElementHelper.InsertNewImageElement(expectedId, expectedImageName,
                expectedImageContentUri, expectedPersonFirstName, expectedPersonLastName);
            
            //Assert
            result.ShouldBe(null);
        }

        [Test]
        public void InsertNewImageElement_ShouldThrow_BadUri()
        {
            //Arrange
            var fakeDatabaseImageElementList = new List<DatabaseImageElement>()
            {
                new DatabaseImageElement()
                {
                    Id = 1,
                    ImageName = "testImageName",
                    ImageContentUri = "https://whoisthatserverimages.blob.core.windows.net/images/Domantas_test.jpg",
                    PersonFirstName = "testFirstPersonName",
                    PersonLastName = "testFirstPersonLastName"
                }
            };
            
            var fakeDbSet = UnitTestsUtil.SetupFakeDbSet(fakeDatabaseImageElementList.AsQueryable());
            
            var fakeDatabaseContext = A.Fake<DatabaseContext>();
            A.CallTo(() => fakeDatabaseContext.DatabaseImageElements)
                .Returns(fakeDbSet);
            
            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext())
                .Returns(fakeDatabaseContext);
            
            var databaseImageElementHelper = new DatabaseImageElementHelper(fakeDbContextGeneration);
            
            //Act and assert
            Assert.Throws<UriFormatException>(() =>
                databaseImageElementHelper.InsertNewImageElement(1, "test", "invalidURI", "test", "test"));

            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustNotHaveHappened();
        }
    }
}