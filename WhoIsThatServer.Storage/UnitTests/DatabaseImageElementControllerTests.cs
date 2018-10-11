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
                    ImageContentUri = "testURI",
                    ImageName = "testImageName",
                    PersonFirstName = "testFirstName",
                    PersonLastName = "testLastName"
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
            var expectedImageContentUri = "testURI";
            var expectedPersonFirstName = "testFirstName";
            var expectedPersonLastName = "testLastName";

            var expectedDatabaseImageElement = new DatabaseImageElement()
            {
                Id = expectedId,
                ImageContentUri = expectedImageContentUri,
                ImageName = expectedImageName,
                PersonFirstName = expectedPersonFirstName,
                PersonLastName = expectedPersonLastName
            };
            
            var fakeDatabaseImageElementHelper = A.Fake<IDatabaseImageElementHelper>();

            A.CallTo(() => fakeDatabaseImageElementHelper.InsertNewImageElement(expectedId, expectedImageName,
                    expectedImageContentUri, expectedPersonFirstName, expectedPersonLastName))
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
                    expectedImageContentUri, expectedPersonFirstName, expectedPersonLastName))
                .MustHaveHappenedOnceExactly();
            
            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedDatabaseImageElement);
            
            jsonContent.ShouldBe(expectedJson);
        }
    }
}