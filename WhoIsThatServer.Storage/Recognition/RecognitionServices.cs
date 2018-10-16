using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Recognition.Utils;

namespace WhoIsThatServer.Storage.Recognition
{
    public class RecognitionServices
    {
        private const string _subscriptionKey = "b7923707fb7f44d091bb8d86d47a2988";
        private const string _recognitionApi = "https://northeurope.api.cognitive.microsoft.com/face/v1.0";
        private const string _groupId = "People";

        private FaceServiceClient _faceServiceClient;
        private DatabaseImageElementHelper _databaseImageElementHelper;

        public RecognitionServices()
        {
            _faceServiceClient = new FaceServiceClient(_subscriptionKey);
            _databaseImageElementHelper = new DatabaseImageElementHelper();
        }

        public async void CreateGroup()
        {
            await _faceServiceClient.CreatePersonGroupAsync(_groupId, _groupId);

            var people = _databaseImageElementHelper.GetAllImages();

            foreach (var person in people)
            {
                CreatePersonResult result = await _faceServiceClient.CreatePersonAsync(_groupId, person.PersonFirstName);

                await _faceServiceClient.AddPersonFaceAsync(_groupId, result.PersonId, RecUtil.GetStreamFromUri(person.ImageContentUri));
            }

            await _faceServiceClient.TrainPersonGroupAsync(_groupId);

            TrainingStatus trainingStatus = null;
            while (true)
            {
                trainingStatus = await _faceServiceClient.GetPersonGroupTrainingStatusAsync(_groupId);

                if (trainingStatus.Status != Status.Running)
                {
                    break;
                }

                await Task.Delay(1000);
            }
        }

        public async Task<string> Identify()
        {
            var azureBlobHelper = new AzureBlobHelper();
            string takenImageUri = null;

            takenImageUri = azureBlobHelper.GetImageUri("temp");
            if (takenImageUri == null)
                throw new ArgumentException("Photo was not successfully taken!");

            var memoryStream = new MemoryStream();
            memoryStream = RecUtil.GetStreamFromUri(takenImageUri);

            var faces = await _faceServiceClient.DetectAsync(memoryStream);
            var faceIds = faces.Select(face => face.FaceId).ToArray();

            var results = await _faceServiceClient.IdentifyAsync(_groupId, faceIds);

            if (results.Length == 0)
                return "No one was indetified!";

            var candidateId = results[0].Candidates[0].PersonId;
            var person = await _faceServiceClient.GetPersonAsync(_groupId, candidateId);

            return person.Name;
        }
    }
}