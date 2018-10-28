using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WhoIsThatServer.Storage.Context;
using WhoIsThatServer.Storage.Exceptions;
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
                    PersonLastName = "firstTestPersonLastName",
                    DescriptiveSentence = "t1",
                    Score = 1
                },

                new DatabaseImageElement
                {
                    Id = 2,
                    ImageName = "secondTestName",
                    ImageContentUri = "https://whoisthatserverimages.blob.core.windows.net/images/Domantas_test.jpg",
                    PersonFirstName = "secondTestPersonName",
                    PersonLastName = "secondTestPersonLastName",
                    DescriptiveSentence = "t2",
                    Score = 1
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
            var expectedDescriptiveSentence = "t";
            var expectedScore = 1;

            var result = databaseImageElementHelper.InsertNewImageElement(expectedId, expectedImageName,
                expectedImageContentUri, expectedPersonFirstName, expectedPersonLastName, expectedDescriptiveSentence, expectedScore);
            
            //Assert
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappened();
            A.CallTo(() => fakeDbSet.Add(result)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeDatabaseContext.SaveChanges()).MustHaveHappenedOnceExactly();
            
            result.Id.ShouldBe(expectedId);
            result.ImageName.ShouldBe(expectedImageName);
            result.ImageContentUri.ShouldBe(expectedImageContentUri);
            result.PersonFirstName.ShouldBe(expectedPersonFirstName);
            result.PersonLastName.ShouldBe(expectedPersonLastName);
            result.DescriptiveSentence.ShouldBe(expectedDescriptiveSentence);
            result.Score.ShouldBe(expectedScore);
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
                    PersonLastName = "testFirstPersonLastName",
                    DescriptiveSentence = "t1",
                    Score = 1
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
            var expectedDescriptiveSentence = "t1";
            var exptectedScore = 1;

            var result = databaseImageElementHelper.InsertNewImageElement(expectedId, expectedImageName,
                expectedImageContentUri, expectedPersonFirstName, expectedPersonLastName, expectedDescriptiveSentence, exptectedScore);
            
            //Assert
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappened();
            result.ShouldBe(null);
        }

        [Test]
        public void InsertNewImageElement_ShouldThrow_BadFileName()
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
                    PersonLastName = "testFirstPersonLastName",
                    DescriptiveSentence = "t1",
                    Score = 1
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
            Assert.Throws<ManagerException>(() =>
                databaseImageElementHelper.InsertNewImageElement(1, "test@@", "https://whoisthatserverimages.blob.core.windows.test.net/images/Domantas_test.jpg", "test", "test", "t1", 1));

            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustNotHaveHappened();
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
                    PersonLastName = "testFirstPersonLastName",
                    DescriptiveSentence = "t1",
                    Score = 1
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
            Assert.Throws<ManagerException>(() =>
                databaseImageElementHelper.InsertNewImageElement(1, "test", "invalidURI", "test", "test", "t1", 1));

            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustNotHaveHappened();
        }

        [Test]
        public void UpdateScore_WhenIdExists_ShouldUpdate()
        {
            //Arrange
            var fakeIQueryable = new List<DatabaseImageElement>()
            {
                new DatabaseImageElement
                {
                    Id = 0,
                    DescriptiveSentence = "t",
                    ImageContentUri = "t",
                    ImageName = "t",
                    PersonFirstName = "t",
                    PersonLastName = "t",
                    Score = 0
                }
            }.AsQueryable();

            var fakeDbSet = UnitTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeDbContext = A.Fake<DatabaseContext>();
            A.CallTo(() => fakeDbContext.DatabaseImageElements).Returns(fakeDbSet);

            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).Returns(fakeDbContext);

            var databaseImageElementHelper = new DatabaseImageElementHelper(fakeDbContextGeneration);
            
            //Act
            var expectedResult = 1;
            var result = databaseImageElementHelper.UpdateScore(fakeIQueryable.ElementAt(0).Id);
            
            //Assert
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeDbContext.SaveChanges()).MustHaveHappenedOnceExactly();
            
            result.Score.ShouldBe(expectedResult);
        }

        [Test]
        public void UpdateScore_WhenIdDoesNotExists_ShouldThrow()
        {
            //Arrange
            var fakeIQueryable = new List<DatabaseImageElement>()
            {
                new DatabaseImageElement
                {
                    Id = 0,
                    DescriptiveSentence = "t",
                    ImageContentUri = "t",
                    ImageName = "t",
                    PersonFirstName = "t",
                    PersonLastName = "t",
                    Score = 0
                }
            }.AsQueryable();

            var fakeDbSet = UnitTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeDbContext = A.Fake<DatabaseContext>();
            A.CallTo(() => fakeDbContext.DatabaseImageElements).Returns(fakeDbSet);

            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).Returns(fakeDbContext);

            var databaseImageElementHelper = new DatabaseImageElementHelper(fakeDbContextGeneration);
            
            //Act and assert
            Assert.Throws<ManagerException>(() => databaseImageElementHelper.UpdateScore(9));
            
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void GetUserById_WhenIdDoesNotExists_ShouldThrow()
        {
            //Arrange
            var fakeIQueryable = new List<DatabaseImageElement>()
            {
                new DatabaseImageElement
                {
                    Id = 0,
                    DescriptiveSentence = "t",
                    ImageContentUri = "t",
                    ImageName = "t",
                    PersonFirstName = "t",
                    PersonLastName = "t",
                    Score = 0
                }
            }.AsQueryable();
            
            var fakeDbSet = UnitTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeDbContext = A.Fake<DatabaseContext>();
            A.CallTo(() => fakeDbContext.DatabaseImageElements).Returns(fakeDbSet);

            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).Returns(fakeDbContext);

            var databaseImageElementHelper = new DatabaseImageElementHelper(fakeDbContextGeneration);
            
            //Act and assert
            Assert.Throws<ManagerException>(() => databaseImageElementHelper.GetUserById(9));
            
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void GetUserById_WhenIdExists_ShouldReturn()
        {
            //Arrange
            var expectedId = 0;
            var expectedDescriptiveSentence = "t";
            var expectedImageContentUri = "t";
            var expectedImageName = "t";
            var expectedPersonFirstName = "t";
            var expectedPersonLastName = "t";
            var expectedScore = 0;
            
            var fakeIQueryable = new List<DatabaseImageElement>()
            {
                new DatabaseImageElement
                {
                    Id = expectedId,
                    DescriptiveSentence = expectedDescriptiveSentence,
                    ImageContentUri = expectedImageContentUri,
                    ImageName = expectedImageName,
                    PersonFirstName = expectedPersonFirstName,
                    PersonLastName = expectedPersonLastName,
                    Score = expectedScore
                }
            }.AsQueryable();
            
            var fakeDbSet = UnitTestsUtil.SetupFakeDbSet(fakeIQueryable);

            var fakeDbContext = A.Fake<DatabaseContext>();
            A.CallTo(() => fakeDbContext.DatabaseImageElements).Returns(fakeDbSet);

            var fakeDbContextGeneration = A.Fake<IDatabaseContextGeneration>();
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).Returns(fakeDbContext);

            var databaseImageElementHelper = new DatabaseImageElementHelper(fakeDbContextGeneration);
            
            //Act
            var result = databaseImageElementHelper.GetUserById(expectedId);
            
            //Assert
            A.CallTo(() => fakeDbContextGeneration.BuildDatabaseContext()).MustHaveHappenedOnceExactly();
            
            result.Id.ShouldBe(expectedId);
            result.DescriptiveSentence.ShouldBe(expectedDescriptiveSentence);
            result.ImageContentUri.ShouldBe(expectedImageContentUri);
            result.ImageName.ShouldBe(expectedImageName);
            result.PersonFirstName.ShouldBe(expectedPersonFirstName);
            result.PersonLastName.ShouldBe(expectedPersonLastName);
            result.Score.ShouldBe(expectedScore);
        }
    }
}