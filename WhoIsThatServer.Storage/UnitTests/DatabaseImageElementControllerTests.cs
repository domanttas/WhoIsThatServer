using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Routing;
using FakeItEasy;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;
using WhoIsThatServer.Storage.Controllers;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.UnitTests
{
    public class DatabaseImageElementControllerTests
    {
        [Test]
        public async Task GetAllImages_ShouldCallHelper()
        {
            //Arrange
            var fakeDatabaseImageElementHelper = A.Fake<IDatabaseImageElementHelper>();
            
            var expectedObjectsList = new List<DatabaseImageElement>
            {
                new DatabaseImageElement()
                {
                    Id = 1,
                    ImageContentUri = "https://whoisthatserverimages.blob.core.windows.net/images/Domantas_test.jpg",
                    ImageName = "testImageName",
                    PersonFirstName = "testFirstName",
                    PersonLastName = "testLastName",
                    DescriptiveSentence = "t1",
                    Score = 1
                }
            };

            A.CallTo(() => fakeDatabaseImageElementHelper.GetAllImages()).Returns(expectedObjectsList);

            var databaseImageElementController = new DatabaseImageElementController()
            {
                DatabaseImageElementHelper = fakeDatabaseImageElementHelper,
                Request = new HttpRequestMessage()
            };
            
            //Act
            var result = databaseImageElementController.GetAllImages();
            
            //Assert
            A.CallTo(() => fakeDatabaseImageElementHelper.GetAllImages()).MustHaveHappenedOnceExactly();
            
            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedObjectsList);
            
            jsonContent.ShouldBe(expectedJson);
        }

        [Test]
        public async Task Post_ShouldCallHelper()
        {
            //Arrange
            var expectedId = 1;
            var expectedImageName = "testImageName";
            var expectedImageContentUri = "https://whoisthatserverimages.blob.core.windows.net/images/Domantas_test.jpg";
            var expectedPersonFirstName = "testFirstName";
            var expectedPersonLastName = "testLastName";
            var expectedDescriptiveSentence = "t1";
            var expectedScore = 1;

            var expectedDatabaseImageElement = new DatabaseImageElement()
            {
                Id = expectedId,
                ImageContentUri = expectedImageContentUri,
                ImageName = expectedImageName,
                PersonFirstName = expectedPersonFirstName,
                PersonLastName = expectedPersonLastName,
                DescriptiveSentence = expectedDescriptiveSentence,
                Score = expectedScore
            };
            
            var fakeDatabaseImageElementHelper = A.Fake<IDatabaseImageElementHelper>();

            A.CallTo(() => fakeDatabaseImageElementHelper.InsertNewImageElement(expectedId, expectedImageName,
                    expectedImageContentUri, expectedPersonFirstName, expectedPersonLastName, expectedDescriptiveSentence, expectedScore))
                .Returns(expectedDatabaseImageElement);
            
            var databaseImageElementController = new DatabaseImageElementController()
            {
                DatabaseImageElementHelper = fakeDatabaseImageElementHelper,
                Request = new HttpRequestMessage()
            };
            
            //Act
            var result = databaseImageElementController.Post(expectedDatabaseImageElement);
            
            //Assert
            A.CallTo(() => fakeDatabaseImageElementHelper.InsertNewImageElement(expectedId, expectedImageName,
                    expectedImageContentUri, expectedPersonFirstName, expectedPersonLastName, expectedDescriptiveSentence, expectedScore))
                .MustHaveHappenedOnceExactly();
            
            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedDatabaseImageElement);
            
            jsonContent.ShouldBe(expectedJson);
        }

        [Test]
        public async Task UpdateScore_ShouldCallHelper()
        {
            //Arrange
            var expectedScore = 1;
            
            var expectedElement = new DatabaseImageElement()
            {
                Id = 1,
                DescriptiveSentence = "t",
                ImageContentUri = "t",
                ImageName = "t",
                PersonFirstName = "t",
                PersonLastName = "t",
                Score = 0
            };

            var fakeDatabaseImageElementHelper = A.Fake<IDatabaseImageElementHelper>();

            A.CallTo(() => fakeDatabaseImageElementHelper.UpdateScore(expectedElement.Id)).Returns(expectedElement);
            
            var databaseImageElementController = new DatabaseImageElementController()
            {
                DatabaseImageElementHelper = fakeDatabaseImageElementHelper,
                Request = new HttpRequestMessage()
            };
            
            //Act
            var result = databaseImageElementController.UpdateScore(expectedElement.Id);
            
            //Assert
            A.CallTo(() => fakeDatabaseImageElementHelper.UpdateScore(expectedElement.Id))
                .MustHaveHappenedOnceExactly();
            
            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();
            
            var expectedJson = JsonConvert.SerializeObject(expectedElement);
            
            jsonContent.ShouldBe(expectedJson);
        }

        [Test]
        public async Task GetUserById_ShouldCallHelper()
        {
            //Arrange
            var expectedId = 1;
            
            var expectedElement = new DatabaseImageElement()
            {
                Id = 1,
                DescriptiveSentence = "t",
                ImageContentUri = "t",
                ImageName = "t",
                PersonFirstName = "t",
                PersonLastName = "t",
                Score = 0
            };
            
            var fakeDatabaseImageElementHelper = A.Fake<IDatabaseImageElementHelper>();

            A.CallTo(() => fakeDatabaseImageElementHelper.GetUserById(expectedId)).Returns(expectedElement);
            
            var databaseImageElementController = new DatabaseImageElementController()
            {
                DatabaseImageElementHelper = fakeDatabaseImageElementHelper,
                Request = new HttpRequestMessage()
            };
            
            //Act
            var result = databaseImageElementController.GetUserById(expectedId);
            
            //Assert
            A.CallTo(() => fakeDatabaseImageElementHelper.GetUserById(expectedId)).MustHaveHappenedOnceExactly();
            
            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();
            
            var expectedJson = JsonConvert.SerializeObject(expectedElement);
            
            jsonContent.ShouldBe(expectedJson);
        }
    }
}