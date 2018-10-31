using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;
using WhoIsThatServer.Storage.Controllers;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.UnitTests
{
    public class FaceFeaturesControllerTests
    {
        [Test]
        public async Task Post_ShouldCallHelper()
        {
            //Arrange
            var expectedId = 0;
            var expectedPersonId = 1;
            var expectedAge = 20;
            var expectedGender = "male";

            var expectedFeaturesElement = new FaceFeaturesModel()
            {
                Id = expectedId,
                PersonId = expectedPersonId,
                Age = expectedAge,
                Gender = expectedGender
            };

            var fakeFaceFeaturesHelper = A.Fake<IFaceFeaturesHelper>();

            A.CallTo(() =>
                    fakeFaceFeaturesHelper.InsertNewFeaturesModel(expectedPersonId, expectedAge, expectedGender))
                .Returns(expectedFeaturesElement);

            var faceFeaturesController = new FaceFeaturesController()
            {
                FaceFeaturesHelper = fakeFaceFeaturesHelper,
                Request = new HttpRequestMessage()
            };
            
            //Act
            var result = faceFeaturesController.Post(expectedFeaturesElement);
            
            //Assert
            A.CallTo(() => fakeFaceFeaturesHelper.InsertNewFeaturesModel(expectedPersonId, expectedAge, expectedGender))
                .MustHaveHappenedOnceExactly();
            
            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedFeaturesElement);
            
            jsonContent.ShouldBe(expectedJson);
        }

        [Test]
        public async Task GetFeatureById_ShouldCallHelper()
        {
            //Arrange
            var expectedId = 0;
            var expectedPersonId = 1;
            var expectedAge = 20;
            var expectedGender = "male";

            var expectedFeaturesElement = new FaceFeaturesModel()
            {
                Id = expectedId,
                PersonId = expectedPersonId,
                Age = expectedAge,
                Gender = expectedGender
            };

            var fakeFaceFeaturesHelper = A.Fake<IFaceFeaturesHelper>();

            A.CallTo(() => fakeFaceFeaturesHelper.GetFaceFeaturesByPersonId(expectedPersonId)).Returns(expectedFeaturesElement);
            
            var faceFeaturesController = new FaceFeaturesController()
            {
                FaceFeaturesHelper = fakeFaceFeaturesHelper,
                Request = new HttpRequestMessage()
            };
            
            //Act
            var result = faceFeaturesController.GetFeatureById(expectedPersonId);
            
            //Assert
            A.CallTo(() => fakeFaceFeaturesHelper.GetFaceFeaturesByPersonId(expectedPersonId))
                .MustHaveHappenedOnceExactly();
            
            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            var expectedJson = JsonConvert.SerializeObject(expectedFeaturesElement);
            
            jsonContent.ShouldBe(expectedJson);
        }
    }
}