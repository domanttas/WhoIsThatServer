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
    public class TargetElementControllerTests
    {
        [Test]
        public async Task Post_ShouldCallHelper()
        {
            //Arrange
            var expectedId = 1;
            var expectedHunterPersonId = 10;
            var expectedPreyPersonId = 11;
            var expectedIsHunted = false;

            var expectedTargetElement = new TargetElement()
            {
                Id = expectedId,
                HunterPersonId = expectedHunterPersonId,
                PreyPersonId = expectedPreyPersonId
            };
            
            var fakeTargetElementHelper = A.Fake<ITargetElementHelper>();
            A.CallTo(() =>
                fakeTargetElementHelper.InsertNewTargetElement(expectedId, expectedHunterPersonId,
                    expectedPreyPersonId, expectedIsHunted)).Returns(expectedTargetElement);

            var targetElementController = new TargetElementController()
            {
                TargetElementHelper = fakeTargetElementHelper,
                Request = new HttpRequestMessage()
            };
            
            //Act
            var result = targetElementController.Post(expectedTargetElement);
            
            //Assert
            A.CallTo(() => fakeTargetElementHelper.InsertNewTargetElement(expectedId, expectedHunterPersonId,
                expectedPreyPersonId, expectedIsHunted)).MustHaveHappenedOnceExactly();
            
            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();
            
            var expectedJson = JsonConvert.SerializeObject(expectedTargetElement);
            
            jsonContent.ShouldBe(expectedJson);
        }
    }
}