using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;
using WhoIsThatServer.Storage.Constants;
using WhoIsThatServer.Storage.Controllers;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.UnitTests
{
    public class HistoryElementControllerTests
    {
        [Test]
        public async Task Post_ShouldCallHelper()
        {
            //Arrange
            var expectedUserId = 1;
            var expectedTargetId = 2;
            var expectedStatus = StatusConstants.TargetHuntedHistory;

            var expectedElement = new HistoryModel()
            {
                UserId = expectedUserId,
                TargetId = expectedTargetId,
                Status = expectedStatus
            };

            var fakeHistoryModelHelper = A.Fake<IHistoryModelHelper>();
            A.CallTo(() =>
                    fakeHistoryModelHelper.InsertNewHistoryElement(expectedUserId, expectedTargetId, expectedStatus))
                .Returns(expectedElement);

            var historyElementController = new HistoryElementController()
            {
                HistoryModelHelper = fakeHistoryModelHelper,
                Request = new HttpRequestMessage()
            };
            
            //Act
            var result = historyElementController.Post(expectedElement);
            
            //Assert
            A.CallTo(() =>
                    fakeHistoryModelHelper.InsertNewHistoryElement(expectedUserId, expectedTargetId, expectedStatus))
                .MustHaveHappened();
            
            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();
            
            var expectedJson = JsonConvert.SerializeObject(expectedElement);
            
            jsonContent.ShouldBe(expectedJson);
        }

        [Test]
        public async Task GetElementById_ShouldCallHelper()
        {
            //Arrange
            var expectedUserId = 1;
            var expectedTargetId = 2;
            var expectedStatus = StatusConstants.TargetHuntedHistory;

            var expectedElement = new HistoryModel()
            {
                UserId = expectedUserId,
                TargetId = expectedTargetId,
                Status = expectedStatus
            };
            
            var fakeHistoryModelHelper = A.Fake<IHistoryModelHelper>();
            A.CallTo(() => fakeHistoryModelHelper.GetHistoryByUserId(expectedUserId)).Returns(expectedElement);
            
            var historyElementController = new HistoryElementController()
            {
                HistoryModelHelper = fakeHistoryModelHelper,
                Request = new HttpRequestMessage()
            };
            
            //Act
            var result = historyElementController.GetElementById(expectedUserId);
            
            //Assert
            A.CallTo(() => fakeHistoryModelHelper.GetHistoryByUserId(expectedUserId)).MustHaveHappened();
            
            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();
            
            var expectedJson = JsonConvert.SerializeObject(expectedElement);
            
            jsonContent.ShouldBe(expectedJson);
        }

        [Test]
        public async Task UpdateHistoryElement_ShouldCallHelper()
        {
            //Arrange
            var expectedUserId = 1;
            var expectedTargetId = 2;
            var expectedStatus = StatusConstants.TargetHuntedHistory;

            var expectedElement = new HistoryModel()
            {
                Id = 50,
                UserId = expectedUserId,
                TargetId = expectedTargetId,
                Status = expectedStatus
            };
            
            var fakeHistoryModelHelper = A.Fake<IHistoryModelHelper>();
            A.CallTo(() => fakeHistoryModelHelper.UpdateHistoryModel(expectedUserId)).Returns(expectedElement);
            
            var historyElementController = new HistoryElementController()
            {
                HistoryModelHelper = fakeHistoryModelHelper,
                Request = new HttpRequestMessage()
            };
            
            //Act
            var result = historyElementController.UpdateHistoryElement(expectedUserId);
            
            //Assert
            A.CallTo(() => fakeHistoryModelHelper.UpdateHistoryModel(expectedUserId)).MustHaveHappened();
            
            var httpResponse = await result.ExecuteAsync(new CancellationToken());
            var jsonContent = await httpResponse.Content.ReadAsStringAsync();
            
            var expectedJson = JsonConvert.SerializeObject(expectedElement);
            
            jsonContent.ShouldBe(expectedJson);
        }
    }
}